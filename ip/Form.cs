using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ip
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }
        static void straight(byte[,] pixel, int width, int height)
        {
            int[] wavey = new int[] {
                0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4,
                4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0
            };
            const int maxdy = 4;
            int[] wavex = new int[] {
                2, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
                1, 1, 2, 2, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 0, 0, 0, 0, 0, 0, 0
            };
            for (var x = 0; x < width; ++x)
            {
                var dy = wavey[(x + 26) % wavey.Length];
                if (dy > 0)
                {
                    for (var y = height - 1; y >= maxdy; --y)
                    {
                        pixel[x, y] = pixel[x, y - dy];
                    }
                }
            }
            int maxx = width - 8;
            for (var y = 19; y < height; ++y)
            {
                var dx = wavex[(y - 19) % wavex.Length];
                for (var x = 0; x < maxx; ++x)
                {
                    pixel[x, y] = pixel[x + dx, y];
                }
            }
        }
        static void straightSun(byte[,] pixel, int width, int height)
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
            for (var x = 0; x < width; ++x)
            {
                var dy = wavey[x % wavey.Length];
                if (dy > 0)
                {
                    for (var y = height - 1; y >= maxdy; --y)
                    {
                        pixel[x, y] = pixel[x, y - dy];
                    }
                }
            }
            int maxx = width - 4;
            for (var y = 19 + 18; y < height; ++y)
            {
                var dx = wavex[(y - (19 + 18)) % wavex.Length];
                for (var x = 0; x < maxx; ++x)
                {
                    pixel[x, y] = pixel[x + dx, y];
                }
            }
        }
        public static Bitmap Do(string path)
        {
            Bitmap bmp = new Bitmap(path);
            int width = bmp.Width;
            int height = bmp.Height;
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            System.IntPtr scan = bmpData.Scan0;
            byte[,] pixel = new byte[bmp.Width, bmp.Height];
            float Value = 10;
            unsafe
            {
                byte* srcP = (byte*)(void*)scan;
                int srcOffset = bmpData.Stride - width * 3;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        byte blue = srcP[0];
                        byte green = srcP[1];
                        byte red = srcP[2];
                        float Red = red / 255.0f;
                        float Green = green / 255.0f;
                        float Blue = blue / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        red = (byte)(iR > 255 ? 255 : (iR < 0 ? 0 : iR));
                        int iG = (int)Green;
                        green = (byte)(iG > 255 ? 255 : (iG < 0 ? 0 : iG));
                        int iB = (int)Blue;
                        blue = (byte)(iB > 255 ? 255 : (iB < 0 ? 0 : iB));
                        pixel[x, y] = (byte)(.334 * red + .333 * green + .333 * blue);
                        if (pixel[x, y] != 255)
                            pixel[x, y] = 0;
                    }
                    srcP += srcOffset;
                }
                straight(pixel, width, height);
                srcP = (byte*)(void*)scan;
                srcOffset = bmpData.Stride - width * 3;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        srcP[2] = srcP[1] = srcP[0] = pixel[x, y];
                    }
                    srcP += srcOffset;
                }
            }
            bmp.UnlockBits(bmpData);
            bmp.Save("R:\\2.png");
            return bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Do("R:\\1.jpg");
        }
    }
}
