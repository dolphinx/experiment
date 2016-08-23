using System;
using System.Drawing;

namespace ip
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[] wavey = new int[] {
                0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4,
                4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0
            };
            const int maxdy = 4;
            int[] wavex = new int[] {
                1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 4, 4,
                0, 0, 0, 0, 0, 0, 0
            };
            var img = Image.FromFile("D:\\authcode7.jpg") as Bitmap;
            var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            unsafe {
                var p = (int*)data.Scan0;
                for (var x = 0; x < img.Width; ++x)
                {
                    var dy = wavey[(x + 28) % wavey.Length];
                    if (dy > 0)
                    {
                        for (var y = img.Height - 1; y >= maxdy; --y)
                        {
                            p[y * data.Stride / 4 + x] = p[(y - dy) * data.Stride / 4 + x];
                        }
                    }
                }
                /*int maxx = img.Width - 4;
                for (var y = 19 + 18; y < img.Height; ++y)
                {
                    var dx = wavex[(y - (19 + 18)) % wavex.Length];
                    for (var x = 0; x < maxx; ++x)
                    {
                        p[y * data.Stride / 4 + x] = p[y * data.Stride / 4 + (x + dx)];
                    }
                }*/
            }
            img.UnlockBits(data);
            img.Save("D:\\1.jpg");
            pictureBox1.Image = img;
        }
    }
}
