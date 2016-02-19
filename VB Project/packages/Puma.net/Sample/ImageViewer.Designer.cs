namespace Puma.Net.Sample
{
    partial class ImageViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tempRadioButton = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.recognizeButton = new System.Windows.Forms.Button();
            this.wordButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.notePadButton = new System.Windows.Forms.Button();
            this.wordPadButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.hScrollBar);
            this.groupBox2.Controls.Add(this.scaleLabel);
            this.groupBox2.Controls.Add(this.pictureBox);
            this.groupBox2.Location = new System.Drawing.Point(13, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(518, 397);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Image ";
            // 
            // hScrollBar
            // 
            this.hScrollBar.Location = new System.Drawing.Point(80, 16);
            this.hScrollBar.Minimum = 1;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(426, 16);
            this.hScrollBar.TabIndex = 6;
            this.hScrollBar.Value = 100;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Location = new System.Drawing.Point(9, 16);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(68, 13);
            this.scaleLabel.TabIndex = 5;
            this.scaleLabel.Text = "Scale: 100%";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 35);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(495, 356);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 524);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(543, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(250, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // tempRadioButton
            // 
            this.tempRadioButton.AutoSize = true;
            this.tempRadioButton.Checked = true;
            this.tempRadioButton.Location = new System.Drawing.Point(12, 19);
            this.tempRadioButton.Name = "tempRadioButton";
            this.tempRadioButton.Size = new System.Drawing.Size(131, 17);
            this.tempRadioButton.TabIndex = 5;
            this.tempRadioButton.TabStop = true;
            this.tempRadioButton.Text = "Recognize to temp file";
            this.tempRadioButton.UseVisualStyleBackColor = true;
            this.tempRadioButton.CheckedChanged += new System.EventHandler(this.tempRadioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(149, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(149, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.Text = "Recognize to specified file";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "File name";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Enabled = false;
            this.fileNameTextBox.Location = new System.Drawing.Point(80, 47);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(416, 20);
            this.fileNameTextBox.TabIndex = 8;
            // 
            // recognizeButton
            // 
            this.recognizeButton.Location = new System.Drawing.Point(420, 74);
            this.recognizeButton.Name = "recognizeButton";
            this.recognizeButton.Size = new System.Drawing.Size(75, 23);
            this.recognizeButton.TabIndex = 9;
            this.recognizeButton.Text = "Recognize";
            this.recognizeButton.UseVisualStyleBackColor = true;
            this.recognizeButton.Click += new System.EventHandler(this.recognizeButton_Click);
            // 
            // wordButton
            // 
            this.wordButton.Enabled = false;
            this.wordButton.Location = new System.Drawing.Point(248, 73);
            this.wordButton.Name = "wordButton";
            this.wordButton.Size = new System.Drawing.Size(112, 23);
            this.wordButton.TabIndex = 10;
            this.wordButton.Text = "Open in Word";
            this.wordButton.UseVisualStyleBackColor = true;
            this.wordButton.Click += new System.EventHandler(this.wordButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.notePadButton);
            this.groupBox1.Controls.Add(this.wordPadButton);
            this.groupBox1.Controls.Add(this.wordButton);
            this.groupBox1.Controls.Add(this.recognizeButton);
            this.groupBox1.Controls.Add(this.fileNameTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.tempRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 102);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Action ";
            // 
            // notePadButton
            // 
            this.notePadButton.Enabled = false;
            this.notePadButton.Location = new System.Drawing.Point(12, 73);
            this.notePadButton.Name = "notePadButton";
            this.notePadButton.Size = new System.Drawing.Size(112, 23);
            this.notePadButton.TabIndex = 12;
            this.notePadButton.Text = "Open in NotePad";
            this.notePadButton.UseVisualStyleBackColor = true;
            this.notePadButton.Click += new System.EventHandler(this.notePadButton_Click);
            // 
            // wordPadButton
            // 
            this.wordPadButton.Enabled = false;
            this.wordPadButton.Location = new System.Drawing.Point(130, 73);
            this.wordPadButton.Name = "wordPadButton";
            this.wordPadButton.Size = new System.Drawing.Size(112, 23);
            this.wordPadButton.TabIndex = 11;
            this.wordPadButton.Text = "Open in WordPad";
            this.wordPadButton.UseVisualStyleBackColor = true;
            this.wordPadButton.Click += new System.EventHandler(this.wordPadButton_Click);
            // 
            // timer
            // 
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 546);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "ImageViewer";
            this.ShowInTaskbar = false;
            this.Text = "Image";
            this.VisibleChanged += new System.EventHandler(this.ImageViewer_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageViewer_FormClosing);
            this.Resize += new System.EventHandler(this.ImageViewer_Resize);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.RadioButton tempRadioButton;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button wordButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button notePadButton;
        private System.Windows.Forms.Button wordPadButton;
        protected System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Button recognizeButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Timer timer;

    }
}