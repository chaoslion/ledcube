namespace CubeMaker {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msMainLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.msMainStore = new System.Windows.Forms.ToolStripMenuItem();
            this.msMainPatterns = new System.Windows.Forms.ToolStripMenuItem();
            this.msMainPorts = new System.Windows.Forms.ToolStripComboBox();
            this.msMainPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.pbCube = new System.Windows.Forms.PictureBox();
            this.nupBrightness = new System.Windows.Forms.NumericUpDown();
            this.btnClearFrames = new System.Windows.Forms.Button();
            this.btnClearFrame = new System.Windows.Forms.Button();
            this.cbAppendFrame = new System.Windows.Forms.CheckBox();
            this.tmPlayer = new System.Windows.Forms.Timer(this.components);
            this.spCube = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbFrames = new System.Windows.Forms.ListBox();
            this.pnEditor = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPowerTotal = new System.Windows.Forms.Label();
            this.lblPowerMean = new System.Windows.Forms.Label();
            this.lblCurrentTotal = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCurrentMax = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentMean = new System.Windows.Forms.Label();
            this.lblPowerMax = new System.Windows.Forms.Label();
            this.gbNewFrames = new System.Windows.Forms.GroupBox();
            this.gbFadingNewFrames = new System.Windows.Forms.GroupBox();
            this.rbFadeNone = new System.Windows.Forms.RadioButton();
            this.rbFadeIn = new System.Windows.Forms.RadioButton();
            this.rbFadeOut = new System.Windows.Forms.RadioButton();
            this.gbLength = new System.Windows.Forms.GroupBox();
            this.lbFrameLength = new System.Windows.Forms.Label();
            this.nupFrameLength = new System.Windows.Forms.NumericUpDown();
            this.cbCopyFrame = new System.Windows.Forms.CheckBox();
            this.gbFrames = new System.Windows.Forms.GroupBox();
            this.gbMotionTrail = new System.Windows.Forms.GroupBox();
            this.cbClosedLoop = new System.Windows.Forms.CheckBox();
            this.btnMotionTrail = new System.Windows.Forms.Button();
            this.lblActualLength = new System.Windows.Forms.Label();
            this.pgbPlayIndex = new System.Windows.Forms.ProgressBar();
            this.btnSloDnFrames = new System.Windows.Forms.Button();
            this.btnSpdUpFrames = new System.Windows.Forms.Button();
            this.gbFrame = new System.Windows.Forms.GroupBox();
            this.gbFadingFrame = new System.Windows.Forms.GroupBox();
            this.btnFadeFrame = new System.Windows.Forms.Button();
            this.nupFadeSteps = new System.Windows.Forms.NumericUpDown();
            this.btnBrightenFrame = new System.Windows.Forms.Button();
            this.gbBrightness = new System.Windows.Forms.GroupBox();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.gpActualLength = new System.Windows.Forms.GroupBox();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBrightness)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnEditor.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbNewFrames.SuspendLayout();
            this.gbFadingNewFrames.SuspendLayout();
            this.gbLength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupFrameLength)).BeginInit();
            this.gbFrames.SuspendLayout();
            this.gbMotionTrail.SuspendLayout();
            this.gbFrame.SuspendLayout();
            this.gbFadingFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupFadeSteps)).BeginInit();
            this.gbBrightness.SuspendLayout();
            this.gpActualLength.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msMainLoad,
            this.msMainStore,
            this.msMainPatterns,
            this.msMainPorts,
            this.msMainPlay});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(624, 27);
            this.msMain.TabIndex = 9;
            this.msMain.Text = "menuStrip1";
            // 
            // msMainLoad
            // 
            this.msMainLoad.Name = "msMainLoad";
            this.msMainLoad.Size = new System.Drawing.Size(54, 23);
            this.msMainLoad.Text = "Load...";
            this.msMainLoad.Click += new System.EventHandler(this.msMainLoad_Click);
            // 
            // msMainStore
            // 
            this.msMainStore.Name = "msMainStore";
            this.msMainStore.Size = new System.Drawing.Size(55, 23);
            this.msMainStore.Text = "Store...";
            this.msMainStore.Click += new System.EventHandler(this.msMainStore_Click);
            // 
            // msMainPatterns
            // 
            this.msMainPatterns.Name = "msMainPatterns";
            this.msMainPatterns.Size = new System.Drawing.Size(62, 23);
            this.msMainPatterns.Text = "Patterns";
            // 
            // msMainPorts
            // 
            this.msMainPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.msMainPorts.Name = "msMainPorts";
            this.msMainPorts.Size = new System.Drawing.Size(121, 23);
            this.msMainPorts.SelectedIndexChanged += new System.EventHandler(this.msMainPorts_SelectedIndexChanged);
            // 
            // msMainPlay
            // 
            this.msMainPlay.Name = "msMainPlay";
            this.msMainPlay.Size = new System.Drawing.Size(41, 23);
            this.msMainPlay.Text = "Play";
            this.msMainPlay.Click += new System.EventHandler(this.msMainPlay_Click);
            // 
            // pbCube
            // 
            this.pbCube.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCube.Location = new System.Drawing.Point(3, 3);
            this.pbCube.Name = "pbCube";
            this.pbCube.Size = new System.Drawing.Size(162, 458);
            this.pbCube.TabIndex = 11;
            this.pbCube.TabStop = false;
            this.pbCube.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCube_Paint);
            this.pbCube.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCube_MouseDown);
            this.pbCube.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCube_MouseMove);
            // 
            // nupBrightness
            // 
            this.nupBrightness.InterceptArrowKeys = false;
            this.nupBrightness.Location = new System.Drawing.Point(6, 19);
            this.nupBrightness.Name = "nupBrightness";
            this.nupBrightness.Size = new System.Drawing.Size(58, 20);
            this.nupBrightness.TabIndex = 13;
            this.nupBrightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nupBrightness.ValueChanged += new System.EventHandler(this.nupBrightness_ValueChanged);
            // 
            // btnClearFrames
            // 
            this.btnClearFrames.Location = new System.Drawing.Point(98, 71);
            this.btnClearFrames.Name = "btnClearFrames";
            this.btnClearFrames.Size = new System.Drawing.Size(75, 23);
            this.btnClearFrames.TabIndex = 18;
            this.btnClearFrames.Text = "Clear";
            this.btnClearFrames.UseVisualStyleBackColor = true;
            this.btnClearFrames.Click += new System.EventHandler(this.btnClearFrames_Click);
            // 
            // btnClearFrame
            // 
            this.btnClearFrame.Location = new System.Drawing.Point(6, 19);
            this.btnClearFrame.Name = "btnClearFrame";
            this.btnClearFrame.Size = new System.Drawing.Size(75, 23);
            this.btnClearFrame.TabIndex = 17;
            this.btnClearFrame.Text = "Clear";
            this.btnClearFrame.UseVisualStyleBackColor = true;
            this.btnClearFrame.Click += new System.EventHandler(this.btnClearFrame_Click);
            // 
            // cbAppendFrame
            // 
            this.cbAppendFrame.AutoSize = true;
            this.cbAppendFrame.Location = new System.Drawing.Point(6, 19);
            this.cbAppendFrame.Name = "cbAppendFrame";
            this.cbAppendFrame.Size = new System.Drawing.Size(86, 17);
            this.cbAppendFrame.TabIndex = 16;
            this.cbAppendFrame.Text = "Append new";
            this.cbAppendFrame.UseVisualStyleBackColor = true;
            this.cbAppendFrame.Click += new System.EventHandler(this.cbAppendFrame_Click);
            // 
            // tmPlayer
            // 
            this.tmPlayer.Interval = 25;
            this.tmPlayer.Tick += new System.EventHandler(this.tmPlayer_Tick);
            // 
            // spCube
            // 
            this.spCube.BaudRate = 250000;
            this.spCube.WriteBufferSize = 64;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.pbCube, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbFrames, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnEditor, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 464);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // lbFrames
            // 
            this.lbFrames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFrames.FormattingEnabled = true;
            this.lbFrames.Location = new System.Drawing.Point(532, 3);
            this.lbFrames.Name = "lbFrames";
            this.lbFrames.Size = new System.Drawing.Size(89, 458);
            this.lbFrames.TabIndex = 23;
            this.lbFrames.SelectedIndexChanged += new System.EventHandler(this.lbFrames_SelectedIndexChanged);
            this.lbFrames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbFrames_KeyDown);
            // 
            // pnEditor
            // 
            this.pnEditor.Controls.Add(this.tableLayoutPanel2);
            this.pnEditor.Controls.Add(this.gbNewFrames);
            this.pnEditor.Controls.Add(this.gbFrames);
            this.pnEditor.Controls.Add(this.gbFrame);
            this.pnEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEditor.Location = new System.Drawing.Point(171, 3);
            this.pnEditor.Name = "pnEditor";
            this.pnEditor.Size = new System.Drawing.Size(355, 458);
            this.pnEditor.TabIndex = 22;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.Controls.Add(this.lblPowerTotal, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblPowerMean, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentTotal, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentMax, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentMean, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPowerMax, 2, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 345);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(349, 110);
            this.tableLayoutPanel2.TabIndex = 23;
            // 
            // lblPowerTotal
            // 
            this.lblPowerTotal.AutoSize = true;
            this.lblPowerTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPowerTotal.Location = new System.Drawing.Point(229, 82);
            this.lblPowerTotal.Name = "lblPowerTotal";
            this.lblPowerTotal.Size = new System.Drawing.Size(116, 27);
            this.lblPowerTotal.TabIndex = 11;
            this.lblPowerTotal.Text = "label12";
            this.lblPowerTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPowerMean
            // 
            this.lblPowerMean.AutoSize = true;
            this.lblPowerMean.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPowerMean.Location = new System.Drawing.Point(229, 55);
            this.lblPowerMean.Name = "lblPowerMean";
            this.lblPowerMean.Size = new System.Drawing.Size(116, 26);
            this.lblPowerMean.TabIndex = 5;
            this.lblPowerMean.Text = "label6";
            this.lblPowerMean.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentTotal
            // 
            this.lblCurrentTotal.AutoSize = true;
            this.lblCurrentTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentTotal.Location = new System.Drawing.Point(108, 82);
            this.lblCurrentTotal.Name = "lblCurrentTotal";
            this.lblCurrentTotal.Size = new System.Drawing.Size(114, 27);
            this.lblCurrentTotal.TabIndex = 10;
            this.lblCurrentTotal.Text = "label11";
            this.lblCurrentTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(4, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 27);
            this.label10.TabIndex = 9;
            this.label10.Text = "All mean";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentMax
            // 
            this.lblCurrentMax.AutoSize = true;
            this.lblCurrentMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentMax.Location = new System.Drawing.Point(108, 28);
            this.lblCurrentMax.Name = "lblCurrentMax";
            this.lblCurrentMax.Size = new System.Drawing.Size(114, 26);
            this.lblCurrentMax.TabIndex = 7;
            this.lblCurrentMax.Text = "label8";
            this.lblCurrentMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(4, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 26);
            this.label7.TabIndex = 6;
            this.label7.Text = "Frame peak";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(108, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current [mA]";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(229, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Power [mW]";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "Frame mean";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentMean
            // 
            this.lblCurrentMean.AutoSize = true;
            this.lblCurrentMean.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentMean.Location = new System.Drawing.Point(108, 55);
            this.lblCurrentMean.Name = "lblCurrentMean";
            this.lblCurrentMean.Size = new System.Drawing.Size(114, 26);
            this.lblCurrentMean.TabIndex = 4;
            this.lblCurrentMean.Text = "label5";
            this.lblCurrentMean.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPowerMax
            // 
            this.lblPowerMax.AutoSize = true;
            this.lblPowerMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPowerMax.Location = new System.Drawing.Point(229, 28);
            this.lblPowerMax.Name = "lblPowerMax";
            this.lblPowerMax.Size = new System.Drawing.Size(116, 26);
            this.lblPowerMax.TabIndex = 8;
            this.lblPowerMax.Text = "label9";
            this.lblPowerMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbNewFrames
            // 
            this.gbNewFrames.Controls.Add(this.gbFadingNewFrames);
            this.gbNewFrames.Controls.Add(this.gbLength);
            this.gbNewFrames.Controls.Add(this.cbCopyFrame);
            this.gbNewFrames.Location = new System.Drawing.Point(179, 3);
            this.gbNewFrames.Name = "gbNewFrames";
            this.gbNewFrames.Size = new System.Drawing.Size(173, 200);
            this.gbNewFrames.TabIndex = 22;
            this.gbNewFrames.TabStop = false;
            this.gbNewFrames.Text = "New Frame";
            // 
            // gbFadingNewFrames
            // 
            this.gbFadingNewFrames.Controls.Add(this.rbFadeNone);
            this.gbFadingNewFrames.Controls.Add(this.rbFadeIn);
            this.gbFadingNewFrames.Controls.Add(this.rbFadeOut);
            this.gbFadingNewFrames.Enabled = false;
            this.gbFadingNewFrames.Location = new System.Drawing.Point(6, 42);
            this.gbFadingNewFrames.Name = "gbFadingNewFrames";
            this.gbFadingNewFrames.Size = new System.Drawing.Size(152, 90);
            this.gbFadingNewFrames.TabIndex = 24;
            this.gbFadingNewFrames.TabStop = false;
            this.gbFadingNewFrames.Text = "Fading";
            // 
            // rbFadeNone
            // 
            this.rbFadeNone.AutoSize = true;
            this.rbFadeNone.Checked = true;
            this.rbFadeNone.Location = new System.Drawing.Point(6, 65);
            this.rbFadeNone.Name = "rbFadeNone";
            this.rbFadeNone.Size = new System.Drawing.Size(71, 17);
            this.rbFadeNone.TabIndex = 2;
            this.rbFadeNone.TabStop = true;
            this.rbFadeNone.Text = "No fading";
            this.rbFadeNone.UseVisualStyleBackColor = true;
            this.rbFadeNone.CheckedChanged += new System.EventHandler(this.rbFadeNone_CheckedChanged);
            // 
            // rbFadeIn
            // 
            this.rbFadeIn.AutoSize = true;
            this.rbFadeIn.Location = new System.Drawing.Point(6, 42);
            this.rbFadeIn.Name = "rbFadeIn";
            this.rbFadeIn.Size = new System.Drawing.Size(60, 17);
            this.rbFadeIn.TabIndex = 1;
            this.rbFadeIn.Text = "Fade in";
            this.rbFadeIn.UseVisualStyleBackColor = true;
            this.rbFadeIn.CheckedChanged += new System.EventHandler(this.rbFadeIn_CheckedChanged);
            // 
            // rbFadeOut
            // 
            this.rbFadeOut.AutoSize = true;
            this.rbFadeOut.Location = new System.Drawing.Point(6, 19);
            this.rbFadeOut.Name = "rbFadeOut";
            this.rbFadeOut.Size = new System.Drawing.Size(67, 17);
            this.rbFadeOut.TabIndex = 0;
            this.rbFadeOut.Text = "Fade out";
            this.rbFadeOut.UseVisualStyleBackColor = true;
            this.rbFadeOut.CheckedChanged += new System.EventHandler(this.rbFadeOut_CheckedChanged);
            // 
            // gbLength
            // 
            this.gbLength.Controls.Add(this.lbFrameLength);
            this.gbLength.Controls.Add(this.nupFrameLength);
            this.gbLength.Location = new System.Drawing.Point(6, 138);
            this.gbLength.Name = "gbLength";
            this.gbLength.Size = new System.Drawing.Size(152, 50);
            this.gbLength.TabIndex = 23;
            this.gbLength.TabStop = false;
            this.gbLength.Text = "Length (ticks)";
            // 
            // lbFrameLength
            // 
            this.lbFrameLength.AutoSize = true;
            this.lbFrameLength.Location = new System.Drawing.Point(70, 26);
            this.lbFrameLength.Name = "lbFrameLength";
            this.lbFrameLength.Size = new System.Drawing.Size(35, 13);
            this.lbFrameLength.TabIndex = 14;
            this.lbFrameLength.Text = "label1";
            // 
            // nupFrameLength
            // 
            this.nupFrameLength.InterceptArrowKeys = false;
            this.nupFrameLength.Location = new System.Drawing.Point(6, 24);
            this.nupFrameLength.Name = "nupFrameLength";
            this.nupFrameLength.Size = new System.Drawing.Size(58, 20);
            this.nupFrameLength.TabIndex = 13;
            this.nupFrameLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nupFrameLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nupFrameLength.ValueChanged += new System.EventHandler(this.nupFrameLength_ValueChanged);
            // 
            // cbCopyFrame
            // 
            this.cbCopyFrame.AutoSize = true;
            this.cbCopyFrame.Location = new System.Drawing.Point(6, 19);
            this.cbCopyFrame.Name = "cbCopyFrame";
            this.cbCopyFrame.Size = new System.Drawing.Size(69, 17);
            this.cbCopyFrame.TabIndex = 15;
            this.cbCopyFrame.Text = "Copy last";
            this.cbCopyFrame.UseVisualStyleBackColor = true;
            this.cbCopyFrame.CheckedChanged += new System.EventHandler(this.cbCopyFrame_CheckedChanged);
            this.cbCopyFrame.Click += new System.EventHandler(this.cbCopyFrame_Click);
            // 
            // gbFrames
            // 
            this.gbFrames.Controls.Add(this.gpActualLength);
            this.gbFrames.Controls.Add(this.gbMotionTrail);
            this.gbFrames.Controls.Add(this.pgbPlayIndex);
            this.gbFrames.Controls.Add(this.btnSloDnFrames);
            this.gbFrames.Controls.Add(this.btnSpdUpFrames);
            this.gbFrames.Controls.Add(this.cbAppendFrame);
            this.gbFrames.Controls.Add(this.btnClearFrames);
            this.gbFrames.Location = new System.Drawing.Point(3, 209);
            this.gbFrames.Name = "gbFrames";
            this.gbFrames.Size = new System.Drawing.Size(349, 134);
            this.gbFrames.TabIndex = 21;
            this.gbFrames.TabStop = false;
            this.gbFrames.Text = "All Frames";
            // 
            // gbMotionTrail
            // 
            this.gbMotionTrail.Controls.Add(this.cbClosedLoop);
            this.gbMotionTrail.Controls.Add(this.btnMotionTrail);
            this.gbMotionTrail.Location = new System.Drawing.Point(255, 19);
            this.gbMotionTrail.Name = "gbMotionTrail";
            this.gbMotionTrail.Size = new System.Drawing.Size(88, 75);
            this.gbMotionTrail.TabIndex = 26;
            this.gbMotionTrail.TabStop = false;
            this.gbMotionTrail.Text = "Motion trail";
            // 
            // cbClosedLoop
            // 
            this.cbClosedLoop.AutoSize = true;
            this.cbClosedLoop.Location = new System.Drawing.Point(6, 48);
            this.cbClosedLoop.Name = "cbClosedLoop";
            this.cbClosedLoop.Size = new System.Drawing.Size(81, 17);
            this.cbClosedLoop.TabIndex = 27;
            this.cbClosedLoop.Text = "Closed loop";
            this.cbClosedLoop.UseVisualStyleBackColor = true;
            // 
            // btnMotionTrail
            // 
            this.btnMotionTrail.Location = new System.Drawing.Point(6, 19);
            this.btnMotionTrail.Name = "btnMotionTrail";
            this.btnMotionTrail.Size = new System.Drawing.Size(75, 23);
            this.btnMotionTrail.TabIndex = 26;
            this.btnMotionTrail.TabStop = false;
            this.btnMotionTrail.Text = "Add";
            this.btnMotionTrail.UseVisualStyleBackColor = true;
            this.btnMotionTrail.Click += new System.EventHandler(this.btnMotionTrail_Click);
            // 
            // lblActualLength
            // 
            this.lblActualLength.AutoSize = true;
            this.lblActualLength.Location = new System.Drawing.Point(6, 16);
            this.lblActualLength.Name = "lblActualLength";
            this.lblActualLength.Size = new System.Drawing.Size(40, 13);
            this.lblActualLength.TabIndex = 23;
            this.lblActualLength.Text = "Actual:";
            // 
            // pgbPlayIndex
            // 
            this.pgbPlayIndex.Location = new System.Drawing.Point(6, 100);
            this.pgbPlayIndex.Maximum = 1;
            this.pgbPlayIndex.Name = "pgbPlayIndex";
            this.pgbPlayIndex.Size = new System.Drawing.Size(337, 23);
            this.pgbPlayIndex.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbPlayIndex.TabIndex = 24;
            // 
            // btnSloDnFrames
            // 
            this.btnSloDnFrames.Location = new System.Drawing.Point(6, 71);
            this.btnSloDnFrames.Name = "btnSloDnFrames";
            this.btnSloDnFrames.Size = new System.Drawing.Size(75, 23);
            this.btnSloDnFrames.TabIndex = 20;
            this.btnSloDnFrames.Text = "Slow down";
            this.btnSloDnFrames.UseVisualStyleBackColor = true;
            this.btnSloDnFrames.Click += new System.EventHandler(this.btnSloDnFrames_Click);
            // 
            // btnSpdUpFrames
            // 
            this.btnSpdUpFrames.Location = new System.Drawing.Point(6, 42);
            this.btnSpdUpFrames.Name = "btnSpdUpFrames";
            this.btnSpdUpFrames.Size = new System.Drawing.Size(75, 23);
            this.btnSpdUpFrames.TabIndex = 19;
            this.btnSpdUpFrames.Text = "Speed up";
            this.btnSpdUpFrames.UseVisualStyleBackColor = true;
            this.btnSpdUpFrames.Click += new System.EventHandler(this.btnSpdUpFrames_Click);
            // 
            // gbFrame
            // 
            this.gbFrame.Controls.Add(this.gbFadingFrame);
            this.gbFrame.Controls.Add(this.btnClearFrame);
            this.gbFrame.Controls.Add(this.gbBrightness);
            this.gbFrame.Location = new System.Drawing.Point(3, 3);
            this.gbFrame.Name = "gbFrame";
            this.gbFrame.Size = new System.Drawing.Size(170, 200);
            this.gbFrame.TabIndex = 20;
            this.gbFrame.TabStop = false;
            this.gbFrame.Text = "Current Frame";
            // 
            // gbFadingFrame
            // 
            this.gbFadingFrame.Controls.Add(this.btnFadeFrame);
            this.gbFadingFrame.Controls.Add(this.nupFadeSteps);
            this.gbFadingFrame.Controls.Add(this.btnBrightenFrame);
            this.gbFadingFrame.Location = new System.Drawing.Point(6, 48);
            this.gbFadingFrame.Name = "gbFadingFrame";
            this.gbFadingFrame.Size = new System.Drawing.Size(158, 80);
            this.gbFadingFrame.TabIndex = 20;
            this.gbFadingFrame.TabStop = false;
            this.gbFadingFrame.Text = "Fading";
            // 
            // btnFadeFrame
            // 
            this.btnFadeFrame.Location = new System.Drawing.Point(6, 19);
            this.btnFadeFrame.Name = "btnFadeFrame";
            this.btnFadeFrame.Size = new System.Drawing.Size(75, 23);
            this.btnFadeFrame.TabIndex = 18;
            this.btnFadeFrame.Text = "Fade out";
            this.btnFadeFrame.UseVisualStyleBackColor = true;
            this.btnFadeFrame.Click += new System.EventHandler(this.btnFadeFrame_Click);
            // 
            // nupFadeSteps
            // 
            this.nupFadeSteps.InterceptArrowKeys = false;
            this.nupFadeSteps.Location = new System.Drawing.Point(87, 22);
            this.nupFadeSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupFadeSteps.Name = "nupFadeSteps";
            this.nupFadeSteps.Size = new System.Drawing.Size(58, 20);
            this.nupFadeSteps.TabIndex = 22;
            this.nupFadeSteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nupFadeSteps.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nupFadeSteps.ValueChanged += new System.EventHandler(this.nupFadeSteps_ValueChanged);
            // 
            // btnBrightenFrame
            // 
            this.btnBrightenFrame.Location = new System.Drawing.Point(6, 48);
            this.btnBrightenFrame.Name = "btnBrightenFrame";
            this.btnBrightenFrame.Size = new System.Drawing.Size(75, 23);
            this.btnBrightenFrame.TabIndex = 20;
            this.btnBrightenFrame.Text = "Fade in";
            this.btnBrightenFrame.UseVisualStyleBackColor = true;
            this.btnBrightenFrame.Click += new System.EventHandler(this.btnBrightenFrame_Click);
            // 
            // gbBrightness
            // 
            this.gbBrightness.Controls.Add(this.lblBrightness);
            this.gbBrightness.Controls.Add(this.nupBrightness);
            this.gbBrightness.Location = new System.Drawing.Point(6, 134);
            this.gbBrightness.Name = "gbBrightness";
            this.gbBrightness.Size = new System.Drawing.Size(158, 50);
            this.gbBrightness.TabIndex = 19;
            this.gbBrightness.TabStop = false;
            this.gbBrightness.Text = "Brightness (%)";
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Location = new System.Drawing.Point(70, 21);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(35, 13);
            this.lblBrightness.TabIndex = 14;
            this.lblBrightness.Text = "label2";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(6, 29);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(32, 13);
            this.lblError.TabIndex = 27;
            this.lblError.Text = "Error:";
            // 
            // gpActualLength
            // 
            this.gpActualLength.Controls.Add(this.lblActualLength);
            this.gpActualLength.Controls.Add(this.lblError);
            this.gpActualLength.Location = new System.Drawing.Point(98, 19);
            this.gpActualLength.Name = "gpActualLength";
            this.gpActualLength.Size = new System.Drawing.Size(148, 46);
            this.gpActualLength.TabIndex = 28;
            this.gpActualLength.TabStop = false;
            this.gpActualLength.Text = "Length";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 491);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.msMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.msMain;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 530);
            this.Name = "frmMain";
            this.Text = "CubeMaker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCube)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBrightness)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnEditor.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbNewFrames.ResumeLayout(false);
            this.gbNewFrames.PerformLayout();
            this.gbFadingNewFrames.ResumeLayout(false);
            this.gbFadingNewFrames.PerformLayout();
            this.gbLength.ResumeLayout(false);
            this.gbLength.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupFrameLength)).EndInit();
            this.gbFrames.ResumeLayout(false);
            this.gbFrames.PerformLayout();
            this.gbMotionTrail.ResumeLayout(false);
            this.gbMotionTrail.PerformLayout();
            this.gbFrame.ResumeLayout(false);
            this.gbFadingFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupFadeSteps)).EndInit();
            this.gbBrightness.ResumeLayout(false);
            this.gbBrightness.PerformLayout();
            this.gpActualLength.ResumeLayout(false);
            this.gpActualLength.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem msMainLoad;
        private System.Windows.Forms.PictureBox pbCube;
        private System.Windows.Forms.ToolStripMenuItem msMainStore;
        private System.Windows.Forms.CheckBox cbAppendFrame;
        private System.Windows.Forms.Timer tmPlayer;
        private System.IO.Ports.SerialPort spCube;
        private System.Windows.Forms.Button btnClearFrames;
        private System.Windows.Forms.Button btnClearFrame;
        private System.Windows.Forms.ToolStripMenuItem msMainPatterns;
        private System.Windows.Forms.NumericUpDown nupBrightness;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbBrightness;
        private System.Windows.Forms.GroupBox gbFrame;
        private System.Windows.Forms.Button btnFadeFrame;
        private System.Windows.Forms.GroupBox gbFrames;
        private System.Windows.Forms.Panel pnEditor;
        private System.Windows.Forms.ToolStripComboBox msMainPorts;
        private System.Windows.Forms.ToolStripMenuItem msMainPlay;
        private System.Windows.Forms.Button btnBrightenFrame;
        private System.Windows.Forms.GroupBox gbNewFrames;
        private System.Windows.Forms.NumericUpDown nupFadeSteps;
        private System.Windows.Forms.CheckBox cbCopyFrame;
        private System.Windows.Forms.GroupBox gbLength;
        private System.Windows.Forms.NumericUpDown nupFrameLength;
        private System.Windows.Forms.Label lbFrameLength;
        private System.Windows.Forms.ListBox lbFrames;
        private System.Windows.Forms.GroupBox gbFadingNewFrames;
        private System.Windows.Forms.RadioButton rbFadeNone;
        private System.Windows.Forms.RadioButton rbFadeIn;
        private System.Windows.Forms.RadioButton rbFadeOut;
        private System.Windows.Forms.GroupBox gbFadingFrame;
        private System.Windows.Forms.Button btnSloDnFrames;
        private System.Windows.Forms.Button btnSpdUpFrames;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblPowerTotal;
        private System.Windows.Forms.Label lblCurrentTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPowerMax;
        private System.Windows.Forms.Label lblCurrentMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPowerMean;
        private System.Windows.Forms.Label lblCurrentMean;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblActualLength;
        private System.Windows.Forms.ProgressBar pgbPlayIndex;
        private System.Windows.Forms.Button btnMotionTrail;
        private System.Windows.Forms.GroupBox gbMotionTrail;
        private System.Windows.Forms.CheckBox cbClosedLoop;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.GroupBox gpActualLength;
    }
}

