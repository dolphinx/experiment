using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ip
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        static int[] waveysun = new int[] {
            4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4
        };
        static int[] waveyhei = new int[] {
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0,
            0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0,
            0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 3, 3, 3, 3,
            3, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1,
            0, 0, 0, 0, 0, 0, 1, 1, 1, 2
        };
        static int[] waveyxi = new int[] {
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 1, 1, 0,
            0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 4, 4,
            4, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 1, 1, 1, 0, 0,
            0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4,
            4, 4, 4, 4, 4, 3, 3, 2, 2, 1, 1, 1, 1, 1, 0, 0,
            0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 4, 4, 4,
            4, 4, 4, 4, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 0, 0,
            0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 4, 4,
            4, 4, 4, 3, 3, 2, 2, 1, 1, 1, 1, 0, 0, 0, 0, 0,
            0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4
        };
        static void straight(byte[,] pixel, int width, int height)
        {
            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var dySun = waveysun[x % waveysun.Length];
                    if (pixel[x, y + dySun] == 0)
                    {
                        straightHei(pixel, width, height, y);
                        return;
                    }
                    var dyHei = waveysun[(x + 26) % waveysun.Length];
                    if (pixel[x, y + dyHei] == 0)
                    {
                        straightHei(pixel, width, height, y);
                        return;
                    }
                }
            }
        }

        static void straightHei(byte[,] pixel, int width, int height, int startY)
        {
            for (var x = 0; x < width; ++x)
            {
                var dy = waveyhei[x];
                if (dy > 0)
                {
                    var y = startY;
                    for (; y < height - dy; ++y)
                        pixel[x, y] = pixel[x, y + dy];
                    for (; y < height; ++y)
                        pixel[x, y] = 255;
                }
            }
            int[] wavex = new int[] {
                /*                 15                      */1,
                1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2,
                2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 8, 9, 9, 10, 10,
                12, 12, 12, 12, 12, 12, 0, 0, 0, 0, 0, 0
            };
            for (var y = startY > 15 ? startY : 15; y < height; ++y)
            {
                var dx = wavex[y - 15];
                var x = 0;
                for (; x < width - dx; ++x)
                    pixel[x, y] = pixel[x + dx, y];
                for (; x < width; ++x)
                    pixel[x, y] = 255;
            }
        }

        static void straightSun(byte[,] pixel, int width, int height, int startY)
        {
            for (var x = 0; x < width; ++x)
            {
                var dy = waveysun[x % waveysun.Length];
                if (dy > 0)
                {
                    var y = startY;
                    for (; y < height - dy; ++y)
                        pixel[x, y] = pixel[x, y + dy];
                    for (; y < height; ++y)
                        pixel[x, y] = 255;
                }
            }
            int[] wavex = new int[] {
                //16
                /* 3 + 12*/ 1,
                1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 5, 5, 5, 6, 7,
                7, 8, 8, 8, 8, 10, 0, 0, 0, 0, 0, 0
            };
            for (var y = startY > 19 + 12 ? startY : 19 + 12; y < height; ++y)
            {
                var dx = wavex[y - (19 + 12)];
                var x = 0;
                for (; x < width - dx; ++x)
                    pixel[x, y] = pixel[x + dx, y];
                for (; x < width; ++x)
                    pixel[x, y] = 255;
            }
        }

        static void straightXi(byte[,] pixel, int width, int height, int startY)
        {
            for (var x = 0; x < width; ++x)
            {
                var dy = waveyxi[x];
                if (dy > 0)
                {
                    var y = startY;
                    for (; y < height - dy; ++y)
                        pixel[x, y] = pixel[x, y + dy];
                    for (; y < height; ++y)
                        pixel[x, y] = 255;
                }
            }
            /*int[] wavex = new int[] {
                /*                 16                      *
                4, 3, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1, 0, 0, 1, 1,
                1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 7, 7, 7, 7,
                7, 8, 8, 8, 8, 10, 0, 0, 0, 0, 0, 0
            };
            for (var y = startY > 16 ? startY : 16; y < height; ++y)
            {
                var dx = wavex[y - 16];
                var x = 0;
                for (; x < width - dx; ++x)
                    pixel[x, y] = pixel[x + dx, y];
                for (; x < width; ++x)
                    pixel[x, y] = 255;
            }*/
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
                var lines = new string[height];
                for (int y = 0; y < height; y++)
                {
                    var line = "";
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        srcP[2] = srcP[1] = srcP[0] = pixel[x, y];
                        line += pixel[x, y] == 255 ? " " : "█";
                    }
                    srcP += srcOffset;
                    lines[y] = line;
                }
                File.WriteAllLines(path + ".txt", lines);
            }
            bmp.UnlockBits(bmpData);
            bmp.Save(path + ".png");
            return bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Do("D:\\1.jpg");
        }
    }
}
