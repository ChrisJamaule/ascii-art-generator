using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;

namespace Asciify
{
    public partial class Form1 : Form
    {
        protected string asciiImage = "";
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Event-handler method - Selecting image
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = new Bitmap(openFileDialog1.FileName);

                    Bitmap bitmap = new Bitmap(openFileDialog1.FileName);

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

        }

        //Event-handler method - Asciify button
        private void button2_Click(object sender, EventArgs e)
        {
            //Input Validation
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("An image is needed to create ascii art.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numericUpDown1.Value == 0 || numericUpDown2.Value == 0)
            {
                MessageBox.Show("Select kernel size.");
            }
            else
            {
                //Display ascii image
                Bitmap bitmap = new Bitmap(pictureBox1.Image);

                if(bitmap.Width > 1500 || bitmap.Height > 1500)
                {
                    if (bitmap.Width >= 1000 && bitmap.Height >= 1000)
                    {
                        bitmap = ResizeImage(bitmap, 1000, 1000);
                    }
                }

                //Instance of the BitmapAscii class - passing arguments that represent user input(kernel size)
                BitmapAscii bit = new BitmapAscii((int)numericUpDown1.Value, (int)numericUpDown2.Value);

                asciiImage = bit.Asciitize(bitmap);

                richTextBox1.Text = asciiImage;
            }


            static Bitmap ResizeImage(Image image, int width, int height)
            {
                var destRect = new Rectangle(0, 0, width, height);
                var destImage = new Bitmap(width, height);

                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                return destImage;
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                saveFileDialog1.Filter = "Text Files | *.txt";
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.Filter = "Text Files | *.txt";
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

     
    }//End Class


}//namespace