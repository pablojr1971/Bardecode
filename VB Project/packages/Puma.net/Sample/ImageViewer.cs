using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Puma.Net.Sample
{
    public partial class ImageViewer : Form
    {
        private Font normalFont;
        private Font boldFont;

        public ImageViewer()
        {
            InitializeComponent();

            normalFont = toolStripStatusLabel.Font;
            boldFont = new Font(normalFont, FontStyle.Bold);
        }

        private Bitmap sourceBitmap;
        private Point currentPoint = new Point();

        public Bitmap SourceBitmap
        {
            get { return sourceBitmap; }
            set
            {
                sourceBitmap = value;
                toolStripStatusLabel.Text = "Ready";
            }
        }

        private void ImageViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Scale(e.NewValue);
        }

        private static Point ScaleAxis(int dest, int src, ref int x, float ratio)
        {
            Point point = new Point();
            point.X = 0; // dest
            point.Y = 0; // src
            //x = x // currentPoint coordinate
            float minRatio = (float)dest / src;
            int c = (int)(dest / ratio);

            point.X = ratio >= minRatio ? dest : (int)(src * ratio);

            if ((x < 0) || (src < c)) x = 0;
            else if (x > src - c) x = src - c;

            if (src - x >= c)
            {
                point.Y = c;
            }
            else if (src >= c)
            {
                point.Y = c;
                x -= src - c;
            }
            else
            {
                x = 0;
                point.Y = src;
            }

            return point;
        }

        private void Scale(int percent)
        {
            scaleLabel.Text = string.Format("Scale: {0}%", percent);

            //SizeF scale = new SizeF();
            float ratio = (float)percent / 100;

            //scale.Height = ratio;
            //scale.Width = ratio;

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);

            Graphics graphics = Graphics.FromImage(pictureBox.Image);
            int x;
            x = currentPoint.X;
            Point horiz = ScaleAxis(pictureBox.Width, sourceBitmap.Width, ref x, ratio);
            currentPoint.X = x;
            x = currentPoint.Y;
            Point vert = ScaleAxis(pictureBox.Height, sourceBitmap.Height, ref x, ratio);
            currentPoint.Y = x;
            Rectangle src = new Rectangle(currentPoint.X, currentPoint.Y, horiz.Y, vert.Y);
            Rectangle dest = new Rectangle(0, 0, horiz.X, vert.X);

            graphics.DrawImage(sourceBitmap, dest, src, GraphicsUnit.Pixel);
            graphics.Dispose();
        }

        private void ImageViewer_VisibleChanged(object sender, System.EventArgs e)
        {
            if (Visible)
            {
                Scale(hScrollBar.Value);
                //if (toolStripStatusLabel.Text == "Done") Recognized = true; else Recognized = false;
            }
        }

        public bool Recognized
        {
            set
            {
                if (value == true)
                {
                    EnableButtons();
                    toolStripStatusLabel.Text = "Done";
                    timer.Tag = 0;
                    timer.Enabled = true;
                }
                else
                {
                    DisableButtons();
                    toolStripStatusLabel.Text = "Ready";
                }
            }
        }

        private void DisableButtons()
        {
            wordButton.Enabled = false;
            wordPadButton.Enabled = false;
            notePadButton.Enabled = false;
        }

        private void EnableButtons()
        {
            wordButton.Enabled = true;
            wordPadButton.Enabled = true;
            notePadButton.Enabled = true;
        }

        public string FileName
        {
            get
            {
                return fileNameTextBox.Text;
            }
            set
            {
                fileNameTextBox.Text = value;
            }
        }

        public void SetProgress(int progress)
        {
            toolStripProgressBar.Value = progress;
        }

        private void ImageViewer_Resize(object sender, EventArgs e)
        {
            Scale(hScrollBar.Value);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Left) && imageDrag)
            {
                currentPoint.X += e.X - prevLocation.X;
                currentPoint.Y += e.Y - prevLocation.Y;
                Scale(hScrollBar.Value);
            }
        }

        private Point prevLocation = new Point();
        private bool imageDrag = false;

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                prevLocation = e.Location;
                imageDrag = true;
                pictureBox.Cursor = Cursors.NoMove2D;
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            imageDrag = false;
            pictureBox.Cursor = Cursors.Default;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            imageDrag = false;
            pictureBox.Cursor = Cursors.Default;
        }

        private void tempRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (tempRadioButton.Checked) fileNameTextBox.Enabled = false;
            else fileNameTextBox.Enabled = true;
        }

        public event EventHandler RecognizeClick;

        private void recognizeButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            RecognizeClick(this, new EventArgs());
        }

        private void notePadButton_Click(object sender, EventArgs e)
        {
            Process.Start("notepad", "\"" + fileNameTextBox.Text + "\"");
        }

        private void wordPadButton_Click(object sender, EventArgs e)
        {
            Process.Start("wordpad", "\"" + fileNameTextBox.Text + "\"");
        }

        private void wordButton_Click(object sender, EventArgs e)
        {
            Process.Start("winword","\""+ fileNameTextBox.Text+"\"");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((int)timer.Tag > 9)
            {
                timer.Enabled = false;
                toolStripStatusLabel.Text = "Ready";
            }
            if ((int)timer.Tag % 2 != 0) toolStripStatusLabel.Font = boldFont;
            else toolStripStatusLabel.Font = normalFont;
            timer.Tag = (int) timer.Tag + 1;
        }
    }
}
