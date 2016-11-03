/*
 * tlc5940.h
 *
 * Created: 25.10.2016 13:38:38
 *  Author: lion
 */ 


#ifndef TLC5940_H_
#define TLC5940_H_

#define TLC5940_GSTICKS 4096
#define TLC5940_CHANNELS 16
#define TLC5940_DC_BIT 5
#define TLC5940_GS_BIT 12
#define TLC5940_DC_SIZE (TLC5940_CHANNELS*TLC5940_DC_BIT/8)
#define TLC5940_GS_SIZE (TLC5940_CHANNELS*TLC5940_GS_BIT/8)
#define TLC5940_MODE_GS 0x01
#define TLC5940_MODE_DC 0x02
#define TLC5940_MAXGS (uint16_t)0xFFF
#define TLC5940_MAXDC (uint8_t)0x3F

typedef const uint8_t tlc5940_dc_t[TLC5940_CHANNELS];
typedef uint16_t tlc5940_gs_t[TLC5940_CHANNELS];

typedef struct {
	const uint8_t *dcdata;
	uint16_t *gsdata;
	uint32_t cycles;	
	volatile uint8_t* ddr_xlat;
	volatile uint8_t* ddr_blank;	
	volatile uint8_t* ddr_vprg;
	uint8_t msk_xlat;
	uint8_t msk_blank;
	uint8_t msk_vprg;
	uint8_t lastmode, mode;	
} tlc5940_t;

void tlc5940_writedc(tlc5940_t* tlc);
void tlc5940_writegs(tlc5940_t* tlc);
void tlc5940_init(tlc5940_t* tlc, const uint8_t *dcdata, uint16_t* gsdata);
void tlc5940_callback(tlc5940_t* tlc, uint8_t latch);


// implementation specific
extern void tlc5940_spixfer(uint8_t data);
extern void tlc5940_spiclk_toggle();
extern inline void tlc5940_blank_hi();
extern inline void tlc5940_blank_lo();
extern inline void tlc5940_xlat_hi();
extern inline void tlc5940_xlat_lo();
extern inline void tlc5940_vprg_hi();
extern inline void tlc5940_vprg_lo();

#endif /* TLC5940_H_ */