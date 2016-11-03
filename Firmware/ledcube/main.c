#include <avr/io.h>
#include <avr/builtins.h>
#include <avr/interrupt.h>
#include "tlc5940.h"

#define BIT(X) (1<<X)

#define CUBE_LAYERS 4
#define NEW_FRAME_GET 0xFA
#define FRAME_SOF 0xF0
#define FRAME_EOF 0xFF
#define NEW_FRAME_RATE 20
#define NEW_FRAME_MAXCNT (uint16_t)(16e6/TLC5940_GSTICKS/NEW_FRAME_RATE)
#define SOF_BYTE 0xFF
// *2 => uint16
#define FRAME_BYTES (2*TLC5940_CHANNELS*CUBE_LAYERS)




typedef struct {
	uint8_t last_byte;
	uint8_t bytes;	
	uint16_t word;
	uint16_t* active;
	uint16_t* inactive;
	uint16_t* buffer0;
	uint16_t* buffer1;
	uint16_t buffer[2*TLC5940_CHANNELS*CUBE_LAYERS];
} DoubleBuffer;

tlc5940_t tlc;
DoubleBuffer cubebuffer;
uint8_t layer_active;
uint16_t frame_cnt;
volatile uint8_t sig_newframe;
volatile uint8_t sig_rdyframe;
volatile uint8_t sig_newlayer;

tlc5940_dc_t dcdata = {
	TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC, 
	TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC, 
	TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC, 
	TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC, TLC5940_MAXDC
};

void tlc5940_spixfer(uint8_t data) {
	SPDR = data;
	while(!(SPSR & BIT(SPIF)));
}

void tlc5940_spiclk_toggle() {
	SPCR &= ~BIT(SPE);
	PORTB |= BIT(PB5);
	PORTB &= ~BIT(PB5);
	SPCR |= BIT(SPE);
}

/*
	tlc.msk_xlat = BIT(PD0);
	tlc.msk_blank = BIT(PD1);
	tlc.msk_vprg = BIT(PD2);
*/
inline void tlc5940_xlat_hi() {
	PORTD |= BIT(PD2);
}

inline void tlc5940_xlat_lo() {
	PORTD &= ~BIT(PD2);
}

inline void tlc5940_blank_hi() {
	PORTD |= BIT(PD3);
}

inline void tlc5940_blank_lo() {
	PORTD &= ~BIT(PD3);
}

inline void tlc5940_vprg_hi() {
	PORTD |= BIT(PD4);
}

inline void tlc5940_vprg_lo() {
	PORTD &= ~BIT(PD4);
}

ISR(TIMER1_COMPA_vect) {							
	/*
	// frame logic
	// frame period has passed and new frame is received from uart
	if(!frame_cnt && !sig_newframe) {		
		sig_newframe = 1;
	}	
	// inc frame counter	
	frame_cnt = (frame_cnt+1) % NEW_FRAME_MAXCNT;
	*/
			
	// layer logic
	// turn all layers off
	PORTC = 0x0;	
	sig_newlayer = 1;
	layer_active = (layer_active+1) % CUBE_LAYERS;			
	// turn layer on
	// latch new layer data
	tlc5940_callback(&tlc, 1);
	PORTC |= 1 << layer_active;
}

/*
void usart_tx(uint8_t data) {
	while ( !( UCSR0A & BIT(UDRE0)) );
	UDR0 = data;
}*/

ISR(USART_RX_vect) {
	// receive new frame
	// uart sender must make sure to not send faster then i can shove it into the tlc :> fupd > 0.2s is good			
	uint8_t data = UDR0;


	if(sig_rdyframe) {
		return;
	}


	// resync to SOF
	if( (data == SOF_BYTE) && (cubebuffer.last_byte == SOF_BYTE) ) {		
		// reset counters
		cubebuffer.bytes = 0;
		return;
	}

	cubebuffer.last_byte = data;
		
	if(cubebuffer.bytes % 2 == 0) {		
		cubebuffer.word = data << 8;		
	} else {
		cubebuffer.word |= data;	
		cubebuffer.inactive[cubebuffer.bytes/2] = cubebuffer.word;	
	}				
	if( ++cubebuffer.bytes == FRAME_BYTES ) {				
		cubebuffer.bytes = 0;	
		sig_rdyframe = 1;
	}
}

// UART STATES
#define STATE_IDLE 0
#define STATE_RECEIVE 1
int main() {				
	// hw init
	
	// ports
	DDRB |= BIT(PB5) | BIT(PB3) | BIT(PB2);
	// blank + xlat + vpgr
	DDRD |= BIT(PD2) | BIT(PD3) | BIT(PD4);		
	// layers
	DDRC |= BIT(PC0) | BIT(PC1) | BIT(PC2) | BIT(PC3);
	// spi @ 16e6/4 Hz => takes 48us to tx
	SPCR |= BIT(SPE) | BIT(MSTR);
	// uart	250k baud => takes 40us
	UBRR0 = 0x003;	
	UCSR0B = BIT(RXEN0) /*| BIT(TXEN0)*/ | BIT(RXCIE0);
	// UCSR0C = BIT(UCSZ01) | BIT(UCSZ00);

	// initial configuration	
	cubebuffer.bytes = 0;
	cubebuffer.buffer0 = &cubebuffer.buffer[0*TLC5940_CHANNELS*CUBE_LAYERS];
	cubebuffer.buffer1 = &cubebuffer.buffer[1*TLC5940_CHANNELS*CUBE_LAYERS];
	cubebuffer.active = cubebuffer.buffer0; 
	cubebuffer.inactive = cubebuffer.buffer1;

	sig_newframe = 1;
	sig_rdyframe = 0;
	sig_newlayer = 0;		
	PORTC |= 1;		
		
	tlc5940_init(&tlc, dcdata, cubebuffer.active);
	

	// 16 bit timer for gs clocks
	// CTC: TOP==OCR1A=4096, NO PRESCALE => takes 256us
	OCR1A = TLC5940_GSTICKS-1;
	TIMSK1 |= BIT(OCIE1A);
	TCCR1B |= BIT(WGM12) | BIT(CS10);	
	
	sei();
	while(1) {
		
		// spi takes48us
		// uart takes 40us
		// we have 256-48-40 = 168us headroom
		// 256us per byte => new frame after 24ms < request rate(5hz = 200ms) OK
		/*
		if( sig_newframe ) {
			// request new frame
			usart_tx(NEW_FRAME_GET);
			cubebuffer.bytes = 0;
			sig_newframe = 0;
		}*/
				
		if( sig_newlayer ) {			
			sig_newlayer = 0;			
			// load NEXT layer
			// change buffer? => new frame is ready and last layer is active
			if( sig_rdyframe && layer_active == 3 ) {

				if( cubebuffer.active  == cubebuffer.buffer0 ) {
					cubebuffer.active = cubebuffer.buffer1;
					cubebuffer.inactive = cubebuffer.buffer0;
				} else {
					cubebuffer.active = cubebuffer.buffer0;
					cubebuffer.inactive = cubebuffer.buffer1;
				}											
				sig_rdyframe = 0;								
			}
			tlc.gsdata = cubebuffer.active + 16 * ((layer_active+1) % CUBE_LAYERS);
			tlc5940_writegs(&tlc);			
		}
		
		
	}
	
	return 0;
}