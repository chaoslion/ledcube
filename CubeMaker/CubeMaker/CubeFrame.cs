using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeMaker {

    class CubePixel {

        public const int MAX_BRIGHTNESS = 4095;

        public uint brightness;
        public uint fadeRef;
        public uint fadeStep;
        public bool locked;

        public CubePixel() {
            this.reset();
        }

        public void copyFrom(CubePixel other) {
            this.brightness = other.brightness;
            this.fadeRef = other.fadeRef;
            this.fadeStep = other.fadeStep;
            this.locked = other.locked;
        }

        public void updateBrightness(uint new_brightness) {

            if (this.locked) {
                return;
            }

            // saturate
            if (new_brightness > MAX_BRIGHTNESS) {
                new_brightness = MAX_BRIGHTNESS;
            }

            this.brightness = new_brightness;
            this.fadeRef = new_brightness;
            this.fadeStep = 1;
        }

        public void resetStep() {
            this.fadeStep = 1;
            this.fadeRef = this.brightness;
        }

        public void reset() {
            this.brightness = 0;
            this.fadeRef = 0;
            this.fadeStep = 1;
            this.locked = false;
        }
    }

    class CubeFrame {
        const byte SOF_BYTE = 0xFF;
        // NOTE: we cant get full brightness
        // must keep 0xFF as a SOF maker
        public uint ticks, counter, framerate;
        public CubePixel[,,] pixel;

        public string Length {
            get {                
                return string.Format("{0} ms", this.getLength());
            }
        }


        public CubeFrame(uint ticks, uint framerate) {
            this.framerate = framerate;
            this.counter = 0;
            this.ticks = ticks;

            // initial zero
            this.pixel = new CubePixel[4, 4, 4];
            for (uint layer = 0; layer < 4; layer++) {
                for (uint x = 0; x < 4; x++) {
                    for (uint y = 0; y < 4; y++) {
                        this.pixel[x, y, layer] = new CubePixel();
                    }
                }
            }
        }

        // strongest colors wins
        public void mergeFrames(CubeFrame other) {
            for (uint layer = 0; layer < 4; layer++) {
                for (uint x = 0; x < 4; x++) {
                    for (uint y = 0; y < 4; y++) {

                        CubePixel mypix = getPixel(x, y, layer);
                        CubePixel otherpix = other.getPixel(x, y, layer);
                        
                        if(otherpix.brightness > mypix.brightness) {
                            mypix.updateBrightness(otherpix.brightness);
                        }

                    }
                }
            }
        }

        public decimal[] getCurrentConsumption(decimal iref) {
            decimal average = 0;
            decimal peak = 0;

            for (int layer = 0; layer < 4; layer++) {
                decimal iLayer = 0;

                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        CubePixel pixel = this.pixel[x, y, layer];
                        iLayer += (decimal)pixel.brightness / CubePixel.MAX_BRIGHTNESS * iref;
                    }
                }
                if(iLayer > peak) {
                    peak = iLayer;
                }
                average += iLayer;
            }
            average /= 4;
            return new decimal[2] { peak, average };
        }
        
            
        public void copyFrom(CubeFrame other) {
            this.ticks = other.getTicks();
            this.framerate = other.getFrameRate();
            this.counter = 0;            
            for (uint layer = 0; layer < 4; layer++) {
                for (uint x = 0; x < 4; x++) {
                    for (uint y = 0; y < 4; y++) {
                        this.pixel[x, y, layer].copyFrom(other.getPixel(x, y, layer));
                    }
                }
            }
        }

        public void clear() {
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        this.pixel[x, y, layer].reset();
                    }
                }
            }
        }


        public void resetSteps() {
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        this.pixel[x, y, layer].resetStep();
                    }
                }
            }
        }

        public uint getFrameRate() {
            return this.framerate;
        }

        public uint getMaxBrightness() {
            uint result = 0;
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        uint brightness = this.pixel[x, y, layer].brightness;
                        if ( brightness > result ) {
                            result = brightness;
                        }
                    }
                }
            }
            return result;
        }

        public bool allOn() {
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        if (this.pixel[x, y, layer].brightness != CubePixel.MAX_BRIGHTNESS) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool allOff() {            
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        if( this.pixel[x, y, layer].brightness != 0 ) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        // in ms
        public decimal getLength() {
            return (decimal)this.ticks * 1000  / this.framerate;
        }

        public bool playFrame() {
            return this.counter++ < this.ticks-1;
        }

        public void reset() {
            this.counter = 0;
        }



        public void updateTicks(uint ticks) {
            this.ticks = ticks;
        }

        public uint getTicks() {
            return this.ticks;
        }

        public void updateLocked(int x, int y, int layer, bool locked) {
            this.pixel[x, y, layer].locked = locked;
        }

        // by one step out of x steps
        public void fadeOut(uint steps) {
           
            // fade the frame for 1 step out of x steps
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {

                        if (this.pixel[x, y, layer].locked) {
                            continue;
                        }

                        // finished stepping?
                        if(this.pixel[x, y, layer].fadeStep > steps) {
                            continue;
                        }

                        uint bref = this.pixel[x, y, layer].fadeRef;
                        decimal gain = (decimal)(bref - 0) / (steps * steps);
                        // gain*step^2=y                        
                        // we need to flip the curve to fade higher values faster
                        uint step = steps - this.pixel[x, y, layer].fadeStep++;
                        int brightness = (int)Math.Round(bref - (bref - gain * step * step));
                        if (brightness < 0) {
                            brightness = 0;
                        }
                        this.pixel[x, y, layer].brightness = (uint)brightness;
                    }
                }
            }            
        }

        public void fadeIn(uint steps) {

            // fade the frame for 1 step out of x steps
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {

                        if (this.pixel[x, y, layer].locked) {
                            continue;
                        }

                        // finished stepping?
                        if (this.pixel[x, y, layer].fadeStep > steps) {
                            continue;
                        }

                        uint bref = this.pixel[x, y, layer].fadeRef;
                        decimal gain = (decimal)(CubePixel.MAX_BRIGHTNESS - bref) / (steps * steps);
                        // gain*step^2=y                        
                        uint step = this.pixel[x, y, layer].fadeStep++;
                        int brightness = (int)Math.Round(bref + gain * step * step);
                        if(brightness > CubePixel.MAX_BRIGHTNESS) {
                            brightness = CubePixel.MAX_BRIGHTNESS;
                        }
                        this.pixel[x, y, layer].brightness = (uint)brightness;                        
                    }
                }
            }
        }

        public CubePixel getPixel(uint x, uint y, uint layer) {
            return this.pixel[x, y, layer];
        }

        public void loadFromString(string frame) {
            string[] buf = frame.Split(',');
            this.ticks = uint.Parse(buf[0]);

            int count = 1;

            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        this.pixel[x, y, layer].brightness = uint.Parse(buf[count++]);                                                
                    }
                }
            }
        }
        
        public string storeToString() {
            List<uint> storage = new List<uint>();
            string result = "";

            storage.Add(this.ticks);
            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        storage.Add(this.pixel[x, y, layer].brightness);
                    }
                }
            }

            for(int i=0;i<storage.Count;i++) {
                result += String.Format("{0},", storage[i]);
            }

            return result;
        }        

        private uint[,,] FixOrder() {
            uint[,,] res = new uint[4, 4, 4];

            // same for all layers
            for (int layer = 0; layer < 4; layer++) {
                res[0, 3, layer] = this.pixel[3, 0, layer].brightness;
                res[1, 3, layer] = this.pixel[2, 0, layer].brightness;
                res[2, 3, layer] = this.pixel[1, 3, layer].brightness;
                res[3, 3, layer] = this.pixel[0, 3, layer].brightness;

                res[0, 2, layer] = this.pixel[3, 1, layer].brightness;
                res[1, 2, layer] = this.pixel[2, 1, layer].brightness;
                res[2, 2, layer] = this.pixel[1, 2, layer].brightness;
                res[3, 2, layer] = this.pixel[0, 2, layer].brightness;

                res[0, 1, layer] = this.pixel[3, 2, layer].brightness;            
                res[1, 1, layer] = this.pixel[2, 2, layer].brightness;
                res[2, 1, layer] = this.pixel[1, 1, layer].brightness;
                res[3, 1, layer] = this.pixel[0, 1, layer].brightness;

                res[0, 0, layer] = this.pixel[3, 3, layer].brightness;
                res[1, 0, layer] = this.pixel[2, 3, layer].brightness;
                res[2, 0, layer] = this.pixel[1, 0, layer].brightness;
                res[3, 0, layer] = this.pixel[0, 0, layer].brightness;
            }

            return res;
    }

        public Byte[] ToBytes() {
            // *2 => uint16
            Byte[] result = new Byte[2 * 16 * 4 + 2];
            int idx = 0;

            uint[,,] fixedOrder = FixOrder();

            // add SOF byte
            result[idx++] = SOF_BYTE;
            result[idx++] = SOF_BYTE;

            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        UInt16 tlcbrightness = (UInt16)fixedOrder[x, y, layer];

                        result[idx++] = (Byte)((tlcbrightness & 0xFF00) >> 8);
                        result[idx++] = (Byte)((tlcbrightness & 0x00FF) >> 0);
                    }
                }
            }            
            return result;
        }
    }
}
