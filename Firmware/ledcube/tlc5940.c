/*
 * tlc5940.c
 *
 * Created: 25.10.2016 13:38:22
 *  Author: lion
 */ 
#include <avr/io.h>
#include "tlc5940.h"


static uint8_t datamem[TLC5940_GS_SIZE];

static uint8_t* packdc(const uint8_t* src, uint8_t* dst) {
	dst[0] = ((src[15] & 0x3F) << 2) | ((src[14] & 0x30) >> 4);
	dst[1] = ((src[14] & 0x0F) << 4) | ((src[13] & 0x3C) >> 2);
	dst[2] = ((src[13] & 0x03) << 6) | (src[12] & 0x3F);	
	dst[3] = ((src[11] & 0x3F) << 2) | ((src[10] & 0x30) >> 4);
	dst[4] = ((src[10] & 0x0F) << 4) | ((src[9] & 0x3C) >> 2);
	dst[5] = ((src[9] & 0x03) << 6) | (src[8] & 0x3F);
	dst[6] = ((src[7] & 0x3F) << 2) | ((src[6] & 0x30) >> 4);
	dst[7] = ((src[6] & 0x0F) << 4) | ((src[5] & 0x3C) >> 2);
	dst[8] = ((src[5] & 0x03) << 6) | (src[4] & 0x3F);
	dst[9] = ((src[3] & 0x3F) << 2) | ((src[2] & 0x30) >> 4);
	dst[10] = ((src[2] & 0x0F) << 4) | ((src[1] & 0x3C) >> 2);
	dst[11] = ((src[1] & 0x03) << 6) | (src[0] & 0x3F);

	return dst;
}

static uint8_t* packgs(uint16_t* src, uint8_t* dst) {
	dst[0] = ((src[15] & 0xFF0) >> 4);
	dst[1] = ((src[15] & 0x00F) << 4) | ((src[14] & 0xF00) >> 8);
	dst[2] = (src[14] & 0x0FF);
	
	dst[3] = ((src[13] & 0xFF0) >> 4);
	dst[4] = ((src[13] & 0x00F) << 4) | ((src[12] & 0xF00) >> 8);
	dst[5] = (src[12] & 0x0FF);

	dst[6] = ((src[11] & 0xFF0) >> 4);
	dst[7] = ((src[11] & 0x00F) << 4) | ((src[10] & 0xF00) >> 8);
	dst[8] = (src[10] & 0x0FF);

	dst[9] = ((src[9] & 0xFF0) >> 4);
	dst[10] = ((src[9] & 0x00F) << 4) | ((src[8] & 0xF00) >> 8);
	dst[11] = (src[8] & 0x0FF);

	dst[12] = ((src[7] & 0xFF0) >> 4);
	dst[13] = ((src[7] & 0x00F) << 4) | ((src[6] & 0xF00) >> 8);
	dst[14] = (src[6] & 0x0FF);

	dst[15] = ((src[5] & 0xFF0) >> 4);
	dst[16] = ((src[5] & 0x00F) << 4) | ((src[4] & 0xF00) >> 8);
	dst[17] = (src[4] & 0x0FF);	

	dst[18] = ((src[3] & 0xFF0) >> 4);
	dst[19] = ((src[3] & 0x00F) << 4) | ((src[2] & 0xF00) >> 8);
	dst[20] = (src[2] & 0x0FF);

	dst[21] = ((src[1] & 0xFF0) >> 4);
	dst[22] = ((src[1] & 0x00F) << 4) | ((src[0] & 0xF00) >> 8);
	dst[23] = (src[0] & 0x0FF);
	
	return dst;
}


void tlc5940_writedc(tlc5940_t* tlc) {
	uint8_t len = TLC5940_DC_SIZE;
	uint8_t *p = packdc(&tlc->dcdata[0], datamem);	
	tlc5940_vprg_hi();
	while(len-- > 0) {
		tlc5940_spixfer(*p++);		
	}
	tlc->mode = TLC5940_MODE_DC;
}

void tlc5940_writegs(tlc5940_t* tlc) {
	uint8_t len = TLC5940_GS_SIZE;
	uint8_t *p = packgs(tlc->gsdata, datamem);	
	tlc5940_vprg_lo();
	while(len-- > 0) {
		tlc5940_spixfer(*p++);
	}
	tlc->mode = TLC5940_MODE_GS;
}

static void xlat_tg(tlc5940_t* tlc) {
	tlc5940_xlat_hi();
	tlc5940_xlat_lo();
	if( tlc->mode == TLC5940_MODE_GS && tlc->lastmode == TLC5940_MODE_DC ) {
		tlc5940_spiclk_toggle();
	}
	tlc->lastmode = tlc->mode;
	tlc->cycles++;
}

void tlc5940_init(tlc5940_t* tlc, const uint8_t *dcdata, uint16_t* gsdata) {

	tlc->cycles = 0;
	tlc->dcdata = dcdata;
	tlc->gsdata = gsdata;
			
	// initial pin config
	tlc5940_blank_hi();
	tlc5940_blank_lo();
	tlc5940_blank_hi();
	
	tlc5940_xlat_lo();
	tlc5940_vprg_hi();
		
	// write dc
	tlc5940_writedc(tlc);
	xlat_tg(tlc);
	tlc5940_writegs(tlc);
	xlat_tg(tlc);

	// start
	tlc5940_blank_lo();
}

void tlc5940_callback(tlc5940_t* tlc, uint8_t latch) {
	tlc5940_blank_hi();
	if(latch) {
		xlat_tg(tlc);
	}
	tlc5940_blank_lo();
}