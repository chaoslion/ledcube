using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace CubeMaker {

    public partial class frmMain : Form {

        [Flags]
        enum FrameOptions {
            None = 0x0,
            CopyLast = 0x1,
            FadeIn = 0x2,
            FadeOut = 0x4,
            Merge = 0x08
        };

        // in mA
        const uint IREF = 5;
        // in Hz
        const uint FRAME_RATE = 20;
        const uint MIN_LENGTH_MS = (uint)(1 / (decimal)FRAME_RATE * 1000);
        const uint MAX_LENGTH_MS = (uint)1000;
        const uint MAX_TICKS = (uint)((decimal)MAX_LENGTH_MS * FRAME_RATE / 1000);
        
        const uint DEMO_ALL_COPIES = 5;

        int cube_layer_spacing;
        int cube_led_width;
        int cube_led_height;
        Point[,,] cube_coordinates;

        Stopwatch play_watch;
        uint[] last_selection;
        bool playing;
        uint play_index;
        CubeFrame active_frame;
        BindingList<CubeFrame> frames;

        public frmMain() {
            this.last_selection = null;
            this.cube_coordinates = new Point[4, 4, 4];
            this.playing = false;
            this.play_index = 0;
            this.active_frame = null;
            this.frames = new BindingList<CubeFrame>();

            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            this.pbCube.MouseWheel += pbCube_MouseWheel;

            this.gbFrames.Tag = this.gbFrames.Text;
            this.lblActualLength.Tag = this.lblActualLength.Text;

            recalcCubeDimensions();

            this.lbFrames.DataSource = this.frames;
            this.lbFrames.DisplayMember = "Length";

            this.tmPlayer.Interval = (int)MIN_LENGTH_MS;

            // initial nup valuechanged events
            this.nupFrameLength.Maximum = MAX_TICKS;
            this.nupFrameLength.Minimum = 1;
            this.nupFrameLength.Value = 1;

            this.nupBrightness.Value = 100;

            addFrameItem();
            selectLastFrameItem();



            // serial
            this.msMainPorts.Items.Add("Simulate");
            this.msMainPorts.SelectedIndex = 0;
            foreach (string port in SerialPort.GetPortNames()) {
                this.msMainPorts.Items.Add(port);
            }

            // load preset patterns                        
            ResourceSet resourceSet = Properties.Patterns.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet) {
                ToolStripMenuItem pattern = new ToolStripMenuItem(entry.Key.ToString());
                this.msMainPatterns.DropDownItems.Add(pattern);
                pattern.Tag = entry.Value;
                pattern.Click += msPattern_Pattern_Click;
            }

            if (this.msMainPatterns.DropDownItems.Count > 0) {
                this.msMainPatterns.DropDownItems.Add(new ToolStripSeparator());
                ToolStripMenuItem pattern = new ToolStripMenuItem("All (may take some time)");
                pattern.Tag = "all";
                this.msMainPatterns.DropDownItems.Add(pattern);
                pattern.Click += msPattern_Pattern_Click;
            }
        }

        private void msPattern_Pattern_Click(object sender, EventArgs e) {
            ToolStripMenuItem pattern = (ToolStripMenuItem)sender;

            if (!this.cbAppendFrame.Checked) {
                this.frames.Clear();
            }

            if ((string)pattern.Tag == "all") {
                for (int i = 0; i < this.msMainPatterns.DropDownItems.Count - 1; i++) {
                    ToolStripItem tsi = this.msMainPatterns.DropDownItems[i];
                    if(tsi.Tag == null) {
                        continue;
                    }
                    loadFramesFromString((string)tsi.Tag, DEMO_ALL_COPIES);
                }
            } else {
                loadFramesFromString((string)pattern.Tag, 1);
            }
            selectFirstFrameItem();
            this.frames.ResetBindings();
            focusFrames();
        }

        private void recalcCubeDimensions() {
            float pw = (float)this.Width / this.MinimumSize.Width;
            float ph = (float)this.Height / this.MinimumSize.Height;

            this.cube_layer_spacing = (int)((decimal)0.1 * this.pbCube.Height);
            this.cube_led_width = this.pbCube.Width / 10;
            this.cube_led_height = (this.pbCube.Height - 3 * this.cube_layer_spacing) / 16;

            for (int layer = 0; layer < 4; layer++) {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 4; y++) {
                        decimal px = 2 * x * this.cube_led_width + y * this.cube_led_width;
                        decimal py = y * this.cube_led_height + layer * (4 * this.cube_led_height + this.cube_layer_spacing);
                        this.cube_coordinates[x, y, layer] = new Point((int)Math.Round(px), (int)Math.Round(py));
                    }
                }
            }
        }

        private void storeFramesToFile(string filename) {
            StreamWriter file = new StreamWriter(filename);

            foreach (CubeFrame frame in this.frames) {
                file.WriteLine(frame.storeToString());
            }
            file.Close();
        }

        private void loadFramesFromFile(string filename, uint copies) {
            StreamReader file = new StreamReader(filename);
            StringBuilder pattern = new StringBuilder();
            while (true) {
                string line = file.ReadLine();
                if (line == null) {
                    break;
                }
                pattern.AppendLine(line);
            }
            file.Close();
            loadFramesFromString(pattern.ToString(), copies);
        }

        private void loadFramesFromString(string pattern, uint copies) {
            for (uint i = 0; i < copies; i++) {
                StringReader strReader = new StringReader(pattern);
                while (true) {
                    string line = strReader.ReadLine();
                    if (line == null) {
                        break;
                    }
                    CubeFrame frame = addFrameItem();
                    frame.loadFromString(line);
                }
            }
        }

        private CubeFrame get_lastFrame() {
            CubeFrame frame = null;
            if (this.frames.Count > 0) {
                frame = (CubeFrame)this.frames[this.frames.Count - 1];
            }

            return frame;
        }


        private CubeFrame addFrameItem(FrameOptions options = FrameOptions.None, int index = -1) {
            CubeFrame frame = new CubeFrame((uint)this.nupFrameLength.Value, FRAME_RATE);

            // copy last frame?
            if (options.HasFlag(FrameOptions.CopyLast)) {
                frame.copyFrom(this.active_frame);

                // also fade last?
                if (options.HasFlag(FrameOptions.FadeIn) || options.HasFlag(FrameOptions.FadeOut)) {
                    uint steps = (uint)this.nupFadeSteps.Value;

                    if (options.HasFlag(FrameOptions.FadeIn)) {
                        frame.fadeIn(steps);
                    } else {
                        frame.fadeOut(steps);
                    }
                }
            }

            if (index >= 0 && index < this.frames.Count && this.frames.Count > 1) {
                if (options.HasFlag(FrameOptions.Merge)) {
                    CubeFrame frame2repl = this.frames[index];
                    frame.mergeFrames(frame2repl);
                    this.frames[index] = frame;
                } else {
                    this.frames.Insert(index, frame);
                }
            } else {
                this.frames.Add(frame);
            }

            this.frames.ResetBindings();
            redrawFramesCount();
            return frame;
        }

        private bool removeFrameItem(int index) {
            // never remove last frame!
            if (this.frames.Count == 1) {
                return false;
            }

            if (index < 0 || index >= this.frames.Count) {
                return false;
            }
            this.frames.RemoveAt(index);

            this.frames.ResetBindings();
            redrawFramesCount();
            return true;
        }

        private void redrawFramesCount() {
            uint frames = (uint)this.frames.Count;
            uint length = getLengthFromTicks(getTotalTicks());
            this.gbFrames.Text = String.Format("{0} ({1}|{2} ms)", (string)this.gbFrames.Tag, frames, length);
        }

        private int findIndexForFrame(CubeFrame frame) {
            for (var i = 0; i < this.frames.Count; i++) {
                CubeFrame other = (CubeFrame)this.frames[i];
                if (Object.ReferenceEquals(frame, other)) {
                    return i;
                }
            }
            return -1;
        }

        private void updateActiveFrame(int index, bool redraw) {
            if (index < 0 || index >= this.frames.Count) {
                return;
            }
            CubeFrame frame = (CubeFrame)this.frames[index];

            // set last one active
            this.active_frame = frame;
            if (!redraw) {
                return;
            }
            //updatePowerStatistics();
            redrawCube();
        }

        private void selectFrameItem(int index, bool redraw) {
            if (index < 0) {
                index = 0;
            }

            // auto select last
            if (index >= this.frames.Count) {
                index = this.frames.Count - 1;
            }

            // deselect all           
            for (var i = 0; i < this.frames.Count; i++) {
                this.lbFrames.SetSelected(i, false);
            }
            this.lbFrames.SetSelected((int)index, true);
            updateActiveFrame(index, redraw);
        }

        private void selectFirstFrameItem() {
            selectFrameItem(0, true);
        }

        private void selectLastFrameItem() {
            selectFrameItem(this.frames.Count - 1, true);
        }

        private void selectPrevFrameItem(int index) {
            selectFrameItem(index - 1, true);
        }

        private void selectNextFrameItem(int index) {
            selectFrameItem(index + 1, true);
        }

        private uint getTotalTicks() {
            uint total_ticks = 0;
            foreach (CubeFrame frame in this.frames) {
                total_ticks += frame.getTicks();
            }
            return total_ticks;
        }

        private void redrawCube() {
            this.pbCube.Invalidate();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.spCube.IsOpen) {
                this.spCube.Close();
            }
        }

        private void focusFrames() {
            this.lbFrames.Focus();
            this.lbFrames.Select();
        }


        private void msMainPorts_SelectedIndexChanged(object sender, EventArgs e) {
            focusFrames();

        }

        private void cbFadeFrame_Click(object sender, EventArgs e) {
            focusFrames();
        }

        private void btnFadeFrame_Click(object sender, EventArgs e) {
            uint steps = (uint)this.nupFadeSteps.Value;
            int selidx = this.lbFrames.SelectedIndex;
            this.active_frame.resetSteps();
            for (int i = 0; i < steps; i++) {
                CubeFrame frame = addFrameItem(FrameOptions.CopyLast | FrameOptions.FadeOut);
                updateActiveFrame(selidx + i + 1, false);
                if (frame.allOff()) {
                    break;
                }
            }
            selectLastFrameItem();
            focusFrames();
        }

        private void btnBrightenFrame_Click(object sender, EventArgs e) {
            uint steps = (uint)this.nupFadeSteps.Value;
            int selidx = this.lbFrames.SelectedIndex;
            this.active_frame.resetSteps();
            for (int i = 0; i < steps; i++) {
                CubeFrame frame = addFrameItem(FrameOptions.CopyLast | FrameOptions.FadeIn);
                updateActiveFrame(selidx + i + 1, false);
                if (frame.allOn()) {
                    break;
                }
            }
            selectLastFrameItem();
            focusFrames();
        }


        private void cbCopyFrame_Click(object sender, EventArgs e) {
            focusFrames();
        }


        private decimal brightnessToPercent(uint brightness) {
            return (decimal)brightness / CubePixel.MAX_BRIGHTNESS * 100;
        }

        private decimal percentToBrightness(uint percent) {
            return (decimal)percent / 100 * CubePixel.MAX_BRIGHTNESS;
        }

        private void nupBrightness_ValueChanged(object sender, EventArgs e) {
            decimal brightness = percentToBrightness((uint)this.nupBrightness.Value);
            decimal iled = Math.Round(IREF * brightness / CubePixel.MAX_BRIGHTNESS, 2);
            this.lblBrightness.Text = String.Format("{0} mA", iled);
            focusFrames();
        }

        private void btnClearFrame_Click(object sender, EventArgs e) {
            this.active_frame.clear();
            redrawCube();
            focusFrames();
        }

        private void btnClearFrames_Click(object sender, EventArgs e) {
            this.frames.Clear();
            addFrameItem();
            selectLastFrameItem();
            focusFrames();
        }

        private void cbAppendFrame_Click(object sender, EventArgs e) {
            focusFrames();
        }

        private bool cubeCoordForPoint(Point pnt, out uint[] coord) {

            for (uint layer = 0; layer < 4; layer++) {
                for (uint x = 0; x < 4; x++) {
                    for (uint y = 0; y < 4; y++) {
                        Point p = this.cube_coordinates[x, y, layer];
                        Rectangle rect = new Rectangle(p, new Size(cube_led_width - 1, cube_led_height - 1));

                        if (rect.Contains(pnt)) {
                            coord = new uint[3] { x, y, layer };
                            return true;
                        }
                    }
                }
            }
            coord = null;
            return false;
        }


        private void pbCube_Paint(object sender, PaintEventArgs e) {
            Point p;
            Rectangle rect;

            // render cube
            for (uint layer = 0; layer < 4; layer++) {
                for (uint x = 0; x < 4; x++) {
                    for (uint y = 0; y < 4; y++) {

                        Color led_hull = Color.Red;
                        Color led_body = Color.Black;

                        uint brightness = this.active_frame.getPixel(x, y, layer).brightness;
                        int alpha = (int)(brightnessToPercent(brightness) / 100 * 255);
                        if (alpha > 0) {
                            led_body = Color.FromArgb(alpha, Color.Red);
                        }
                        p = this.cube_coordinates[x, y, layer];

                        rect = new Rectangle(p, new Size(this.cube_led_width, this.cube_led_height));
                        // body
                        e.Graphics.FillEllipse(new SolidBrush(led_body), rect);

                        // draw lock state
                        if (this.active_frame.getPixel(x, y, layer).locked) {
                            e.Graphics.FillEllipse(
                                new HatchBrush(
                                    HatchStyle.DiagonalCross,
                                    Color.White,
                                    led_body
                                ),
                                rect
                            );
                        }

                        // hull
                        e.Graphics.DrawEllipse(new Pen(led_hull), rect);
                    }
                }
            }

            updatePowerStatistics();

            if (this.last_selection == null) {
                return;
            }

            p = this.cube_coordinates[this.last_selection[0], this.last_selection[1], this.last_selection[2]];
            rect = new Rectangle(p, new Size(this.cube_led_width, this.cube_led_height));
            e.Graphics.DrawRectangle(new Pen(Color.Gray), rect);
        }



        private void pbCube_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e) {
            // Update the drawing based upon the mouse wheel scrolling.

            if (e.Delta > 0) {
                if (this.nupBrightness.Value > 90) {
                    this.nupBrightness.Value = 100;
                } else {
                    this.nupBrightness.Value += 10;
                }
            } else {
                if (this.nupBrightness.Value < 10) {
                    this.nupBrightness.Value = 0;
                } else {
                    this.nupBrightness.Value -= 10;
                }
            }
        }

        private void pbCube_MouseMove(object sender, MouseEventArgs e) {
            bool shift_pressed = ((ModifierKeys & Keys.Shift) == Keys.Shift);


            bool hit = cubeCoordForPoint(e.Location, out this.last_selection);

            // update selection
            redrawCube();

            if (shift_pressed && hit) {
                CubePixel pix = this.active_frame.getPixel(this.last_selection[0], this.last_selection[1], this.last_selection[2]);
                decimal brightness = percentToBrightness((uint)this.nupBrightness.Value);
                pix.updateBrightness((uint)brightness);
            }
            redrawCube();
        }

        private void pbCube_MouseDown(object sender, MouseEventArgs e) {
            // left = brightness, right lock
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) {
                return;
            }


            if (this.last_selection == null) {
                return;
            }

            bool ctrl_pressed = ((ModifierKeys & Keys.Control) == Keys.Control);
            CubePixel pix = this.active_frame.getPixel(this.last_selection[0], this.last_selection[1], this.last_selection[2]);

            // mask all in current layer, if ctrl is pressed
            if (e.Button == MouseButtons.Left) {
                // toggle
                uint brightness = (uint)percentToBrightness((uint)this.nupBrightness.Value);
                if (pix.brightness == brightness) {
                    pix.updateBrightness(0);
                } else {
                    pix.updateBrightness(brightness);
                }
                if (ctrl_pressed) {
                    for (uint x = 0; x < 4; x++) {
                        for (uint y = 0; y < 4; y++) {
                            CubePixel p2 = this.active_frame.getPixel(x, y, this.last_selection[2]);
                            p2.updateBrightness(pix.brightness);
                        }
                    }
                }
            } else {
                pix.locked = !pix.locked;
                if (ctrl_pressed) {
                    for (uint x = 0; x < 4; x++) {
                        for (uint y = 0; y < 4; y++) {
                            CubePixel p2 = this.active_frame.getPixel(x, y, this.last_selection[2]);
                            p2.locked = pix.locked;
                        }
                    }
                }
            }
            redrawCube();
        }

        private void frmMain_Resize(object sender, EventArgs e) {
            recalcCubeDimensions();
            redrawCube();
        }


        // two hacks
        /*
        private void refreshFramesNames() {
            for (int i = 0; i < this.frames.Count; i++) {
                this.frames[i] = this.frames[i];
            }
        }

        private void selectFrames(List<int> indexes) {
            foreach(int index in indexes) {
                this.lbFrames.SetSelected(index, true);
            }
        }*/

        private void lbFrames_KeyDown(object sender, KeyEventArgs e) {
            int selindex = this.lbFrames.SelectedIndex;

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) {
                e.Handled = true;
                e.SuppressKeyPress = true;
                // change time of active frame by +- 100 ms

                // store selected indexes
                foreach (int index in this.lbFrames.SelectedIndices) {
                    CubeFrame frame = (CubeFrame)this.frames[index];
                    uint ticks = frame.getTicks();
                    if (e.KeyCode == Keys.Left) {
                        ticks--;
                    } else {
                        ticks++;
                    }
                    // check bounds
                    if (ticks > MAX_TICKS) {
                        ticks = MAX_TICKS;
                    } else if (ticks == 0) {
                        ticks = 1;
                    }
                    frame.updateTicks(ticks);
                }
                this.frames.ResetBindings();
            } else if(e.KeyCode == Keys.Up) {
                selectFrameItem(selindex, true);
            } else if (e.KeyCode == Keys.Down) {
                if (selindex + 1 >= this.frames.Count) {
                    FrameOptions options = FrameOptions.None;

                    if (cbCopyFrame.Checked) {
                        options |= FrameOptions.CopyLast;
                    }

                    if (rbFadeOut.Checked) {
                        options |= FrameOptions.FadeOut;
                    } else if (rbFadeIn.Checked) {
                        options |= FrameOptions.FadeIn;
                    }

                    addFrameItem(options);
                    selectLastFrameItem();
                }
            } else if (e.KeyCode == Keys.Delete) {
                removeFrameItem(selindex);
                selectFrameItem(selindex, true);
            } else if (e.KeyCode == Keys.Insert) {
                FrameOptions options = FrameOptions.None;
                if (cbCopyFrame.Checked) {
                    options |= FrameOptions.CopyLast;
                }
                if (rbFadeOut.Checked) {
                    options |= FrameOptions.FadeOut;
                } else if (rbFadeIn.Checked) {
                    options |= FrameOptions.FadeIn;
                }
                addFrameItem(options, selindex);
                selectFrameItem(selindex, true);
            }           
        }

        private void msMainPlay_Click(object sender, EventArgs e) {

            if (this.playing == false) {


                if (this.msMainPorts.Text != "Simulate") {

                    try {
                        this.spCube.PortName = this.msMainPorts.Text;
                        this.spCube.Open();
                        this.spCube.DiscardInBuffer();
                        this.spCube.DiscardOutBuffer();
                    } catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                // reset frames list                
                foreach (CubeFrame frame in this.frames) {
                    frame.reset();
                }

                this.pgbPlayIndex.Maximum = this.frames.Count;
                
                // lock/change ui
                this.lblActualLength.Text = "? ms";

                // main menu
                this.msMainPlay.Text = "Stop";
                this.msMainLoad.Enabled = false;
                this.msMainStore.Enabled = false;
                this.msMainPatterns.Enabled = false;
                this.msMainPorts.Enabled = false;

                // cube
                this.pbCube.Enabled = false;
                // editor
                this.gbFrame.Enabled = false;
                this.cbAppendFrame.Enabled = false;
                this.btnSpdUpFrames.Enabled = false;
                this.btnSloDnFrames.Enabled = false;
                this.btnClearFrames.Enabled = false;
                this.gbMotionTrail.Enabled = false;
                this.gbNewFrames.Enabled = false;
                // frames
                this.lbFrames.Enabled = false;
                
                // start
                this.playing = true;
                this.tmPlayer.Enabled = true;
            } else {
                this.tmPlayer.Enabled = false;
                this.play_index = 0;


                // lock/change ui
                this.lblActualLength.Text = (string)this.lblActualLength.Tag;
                this.pgbPlayIndex.Value = 0;

                // main menu
                this.msMainPlay.Text = "Play";
                this.msMainLoad.Enabled = true;
                this.msMainStore.Enabled = true;
                this.msMainPatterns.Enabled = true;
                this.msMainPorts.Enabled = true;

                // cube
                this.pbCube.Enabled = true;
                // editor
                this.gbFrame.Enabled = true;
                this.cbAppendFrame.Enabled = true;
                this.btnSpdUpFrames.Enabled = true;
                this.btnSloDnFrames.Enabled = true;
                this.btnClearFrames.Enabled = true;
                this.gbMotionTrail.Enabled = true;
                this.gbNewFrames.Enabled = true;
                // frames
                this.lbFrames.Enabled = true;

                
                this.playing = false;
                // go back to start
                selectFrameItem(0, false);
                focusFrames();

                if (this.spCube.IsOpen) {
                    this.spCube.Close();
                }
            }
        }

        private void msMainStore_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text File|*.txt";
            dlg.DefaultExt = ".txt";
            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (dlg.ShowDialog() != DialogResult.OK) {
                return;
            }
            storeFramesToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dlg.FileName));
            focusFrames();
        }

        private void msMainLoad_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text File|*.txt";
            dlg.DefaultExt = ".txt";
            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (dlg.ShowDialog() != DialogResult.OK) {
                return;
            }

            if (!this.cbAppendFrame.Checked) {
                this.frames.Clear();
            }

            loadFramesFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dlg.FileName), 1);

            selectFirstFrameItem();
            focusFrames();
        }

        // hack to disable animation
        // http://stackoverflow.com/a/10939890
        private void updatePlayProgess(uint value) {
            if (value == this.pgbPlayIndex.Maximum) {
                this.pgbPlayIndex.Value = (int)value;           
                this.pgbPlayIndex.Value = (int)value - 1;       
            } else {
                this.pgbPlayIndex.Value = (int)value + 1;       
            }
            this.pgbPlayIndex.Value = (int)value;               
        }

        private void tmPlayer_Tick(object sender, EventArgs e) {

            // play sequence
            CubeFrame frame = (CubeFrame)this.frames[(int)this.play_index];

            if (this.play_index == 0) {

                if (this.play_watch == null) {
                    this.play_watch = Stopwatch.StartNew();
                } else {
                    this.play_watch.Stop();
                    decimal error = Math.Abs(getLengthFromTicks(getTotalTicks()) - this.play_watch.ElapsedMilliseconds);
                    this.lblActualLength.Text = String.Format("{0:0.##} ms (Error: {1:0.##} ms)", this.play_watch.ElapsedMilliseconds, error);
                    this.play_watch = null;
                }
            }

            if (this.spCube.IsOpen) {
                Byte[] data = frame.ToBytes();
                this.spCube.Write(data, 0, data.Length);
            }

            // update ui
            updateActiveFrame((int)this.play_index, true);
            updatePlayProgess(this.play_index);

            if (!frame.playFrame()) {
                frame.reset();
                this.play_index = (this.play_index + 1) % (uint)this.frames.Count;
            }
        }

        private void updatePowerStatistics() {
            // update statistics                        
            decimal[] iPeakAverage = this.active_frame.getCurrentConsumption(IREF);

            lblCurrentMean.Text = String.Format("{0:0.##}", iPeakAverage[1]);
            lblCurrentMax.Text = String.Format("{0:0.##}", iPeakAverage[0]);
            lblPowerMean.Text = String.Format("{0:0.##}", 5 * iPeakAverage[1]);
            lblPowerMax.Text = String.Format("{0:0.##}", 5 * iPeakAverage[0]);

            // go through each frame and get length
            uint total_ticks = getTotalTicks();
            decimal power_mean_all = 0;
            decimal current_mean_all = 0;

            foreach (CubeFrame frame in this.frames) {
                iPeakAverage = frame.getCurrentConsumption(IREF);                
                power_mean_all += 5 * iPeakAverage[1] * (decimal)frame.getTicks() / total_ticks;
                current_mean_all += iPeakAverage[1] * (decimal)frame.getTicks() / total_ticks;
            }

            
            lblCurrentTotal.Text = String.Format("{0:0.##}", current_mean_all);
            lblPowerTotal.Text = String.Format("{0:0.##}", power_mean_all);
        }

        private void lbFrames_SelectedIndexChanged(object sender, EventArgs e) {
            int selindex = this.lbFrames.SelectedIndex;
            updateActiveFrame(selindex, true);
            // only enable fading of last item is selected
            bool lastFrameSelected = selindex == this.frames.Count - 1;
            this.gbMotionTrail.Enabled = lastFrameSelected;
            this.gbFadingFrame.Enabled = lastFrameSelected;
            this.gbNewFrames.Enabled = lastFrameSelected;
            this.gbFadingNewFrames.Enabled = lastFrameSelected && this.cbCopyFrame.Checked;
        }

        private void cbCopyFrame_CheckedChanged(object sender, EventArgs e) {
            this.gbFadingNewFrames.Enabled = this.cbCopyFrame.Checked;
            this.nupFrameLength.Enabled = !this.cbCopyFrame.Checked;
        }

        private void rbFadeNone_CheckedChanged(object sender, EventArgs e) {
            focusFrames();
        }

        private void rbFadeIn_CheckedChanged(object sender, EventArgs e) {
            this.active_frame.resetSteps();
            focusFrames();
        }

        private void rbFadeOut_CheckedChanged(object sender, EventArgs e) {
            this.active_frame.resetSteps();
            focusFrames();
        }

        private void nupFadeSteps_ValueChanged(object sender, EventArgs e) {
            this.active_frame.resetSteps();
            focusFrames();
        }

        private void cbTool_SelectedIndexChanged(object sender, EventArgs e) {
            focusFrames();
        }

        private uint findMaxTickinFrames() {
            uint result = 1;
            foreach (CubeFrame frame in this.frames) {
                uint ticks = frame.getTicks();
                if (ticks > result) {
                    result = ticks;
                }
            }
            return result;
        }

        private uint findMinTickinFrames() {
            uint result = MAX_TICKS;
            foreach (CubeFrame frame in this.frames) {
                uint ticks = frame.getTicks();
                if (ticks < result) {
                    result = ticks;
                }
            }
            return result;
        }

        private void btnSpdUpFrames_Click(object sender, EventArgs e) {
            // slow down frames until at most one frame reaches shortest length
            if (findMinTickinFrames() == 1) {
                return;
            }
            foreach (CubeFrame frame in this.frames) {
                frame.updateTicks(frame.getTicks() - 1);
            }
            redrawFramesCount();
            this.frames.ResetBindings();
            focusFrames();
        }

        private void btnSloDnFrames_Click(object sender, EventArgs e) {
            // speed up frames until at most one frame reaches longest length
            if (findMaxTickinFrames() == MAX_TICKS) {
                return;
            }
            foreach (CubeFrame frame in this.frames) {
                frame.updateTicks(frame.getTicks() + 1);
            }
            redrawFramesCount();
            this.frames.ResetBindings();
            focusFrames();
        }

        private uint getLengthFromTicks(uint ticks) {
            return (uint)Math.Round(1 / (decimal)FRAME_RATE * 1000 * ticks);
        }

        // comboboxs index updates

        private void nupFrameLength_ValueChanged(object sender, EventArgs e) {
            this.lbFrameLength.Text = String.Format("{0} ms", getLengthFromTicks((uint)this.nupFrameLength.Value));
            focusFrames();
        }

        private void nupNewFrameCnt_ValueChanged(object sender, EventArgs e) {
            focusFrames();
        }

        private void btnDoubleFrames_Click(object sender, EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            int frame_count = this.frames.Count;
            for (int index = 0; index < frame_count; index++) {
                updateActiveFrame(index * 2, false);
                addFrameItem(FrameOptions.CopyLast, index * 2 + 1);
            }
            this.Cursor = Cursors.Default;
            selectLastFrameItem();
            focusFrames();
        }

        private void btnHalfFrames_Click(object sender, EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            int frame_count = this.frames.Count / 2;
            for (int index = 0; index < frame_count; index++) {
                updateActiveFrame(index, false);
                removeFrameItem(index + 1);
            }
            this.Cursor = Cursors.Default;
            selectLastFrameItem();
            focusFrames();
        }

        private void btnMotionTrail_Click(object sender, EventArgs e) {
            // add trail to ALL frames with current fading settings
            uint steps = (uint)this.nupFadeSteps.Value;
            uint frames_cnt = (uint)this.frames.Count;

            if (this.cbClosedLoop.Checked) {
                if( frames_cnt < steps ) {
                    MessageBox.Show(String.Format("You need at least {0} frames", steps));
                    return;
                }
                // copy first *steps* frames and add to end
                for(uint i=0;i<steps;i++) {
                    updateActiveFrame((int)i, false);
                    addFrameItem(FrameOptions.CopyLast);
                }
                // update to reflect changes
                frames_cnt = (uint)this.frames.Count;
            }

            uint new_frames_cnt = frames_cnt * (steps - 2) + 2;
            for (uint index = 0; index < new_frames_cnt; index++) {
                updateActiveFrame((int)(index), false);
                CubeFrame frame = addFrameItem(FrameOptions.CopyLast | FrameOptions.FadeOut | FrameOptions.Merge, (int)(index + 1));
                if (frame.allOff()) {
                    break;
                }

            }
            if( this.cbClosedLoop.Checked ) {
                // remove *steps * frames from beginning and end
                for (uint i = 0; i < steps; i++) {
                    removeFrameItem(0);
                    removeFrameItem(this.frames.Count-1);
                }
            }
            selectFirstFrameItem();
            focusFrames();
        }
    }
}
