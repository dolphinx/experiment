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
            0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4,
            4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4,
            4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4,
            4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4,
            4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0,
            0, 0, 0, 1, 1, 1, 2, 2, 2, 3
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
            0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 3, 4,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 1, 0, 0,
            0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 1, 1, 0,
            0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 3, 3, 3, 4,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 4,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        static void straight(byte[,] pixel, int width, int height)
        {
            int l0 = getLow(pixel, width, height, waveysun);
            int h0 = getHigh(pixel, width, height, waveysun);
            int l1 = getLow(pixel, width, height, waveyhei);
            int h1 = getHigh(pixel, width, height, waveyhei);
            int l2 = getLow(pixel, width, height, waveyxi);
            int h2 = getHigh(pixel, width, height, waveyxi);
            int l3 = getLow(pixel, width, height, waveyxi2);
            int h3 = getHigh(pixel, width, height, waveyxi2);
            //straightXi(pixel, width, height);
        }

        private static int getHigh(byte[,] pixel, int width, int height, int[] wavey)
        {
            for (var y = height - 5; y > 0; --y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var dySun = wavey[x];
                    if (pixel[x, y + dySun] == 0)
                    {
                        return y;
                    }
                }
            }

            return 0;
        }

        private static int getLow(byte[,] pixel, int width, int height, int[] wavey)
        {
            for (var y = 0; y < height - 4; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var dySun = wavey[x];
                    if (pixel[x, y + dySun] == 0)
                    {
                        return y;
                    }
                }
            }

            return 0;
        }

        static void straightHei(byte[,] pixel, int width, int height)
        {
            for (var x = 0; x < width; ++x)
            {
                var dy = waveyhei[x];
                if (dy > 0)
                {
                    var y = 0;
                    for (; y < height - dy; ++y)
                        pixel[x, y] = pixel[x, y + dy];
                    for (; y < height; ++y)
                        pixel[x, y] = 255;
                }
            }
            /*int[] wavex = new int[] {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2,
                2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 8, 9, 9, 10, 10,
                12, 12, 12, 12, 12, 12, 0, 0, 0, 0, 0, 0
            };
            for (var y = 15; y < height - 6; ++y)
            {
                var dx = wavex[y];
                var x = 0;
                for (; x < width - dx; ++x)
                    pixel[x, y] = pixel[x + dx, y];
                for (; x < width; ++x)
                    pixel[x, y] = 255;
            }*/
        }

        static void straightSun(byte[,] pixel, int width, int height)
        {
            for (var x = 0; x < width; ++x)
            {
                var dy = waveysun[x];
                if (dy > 0)
                {
                    var y = 0;
                    for (; y < height - dy; ++y)
                        pixel[x, y] = pixel[x, y + dy];
                    for (; y < height; ++y)
                        pixel[x, y] = 255;
                }
            }
            /*int[] wavex = new int[] {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 5, 5, 5, 6, 7,
                7, 8, 8, 8, 8, 10, 0, 0, 0, 0, 0, 0
            };
            for (var y = 19 + 12; y < height - 6; ++y)
            {
                var dx = wavex[y];
                var x = 0;
                for (; x < width - dx; ++x)
                    pixel[x, y] = pixel[x + dx, y];
                for (; x < width; ++x)
                    pixel[x, y] = 255;
            }*/
        }

        static void straightXi(byte[,] pixel, int width, int height)
        {
            for (var x = 0; x < width; ++x)
            {
                var dy = waveyxi[x];
                if (dy > 0)
                {
                    var y = 0;
                    for (; y < height - dy; ++y)
                        pixel[x, y] = pixel[x, y + dy];
                    for (; y < height; ++y)
                        pixel[x, y] = 255;
                }
            }
            /*int[] wavex = new int[] {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1,
                1, 1, 2, 2, 3, 3, 3, 4, 4, 5, 5, 6, 7, 8, 9, 9,
                9, 10, 10, 11, 11, 11, 11, 0, 0, 0, 0, 0
            };
            for (var y = 16; y < height; ++y)
            {
                var dx = wavex[y];
                var x = 0;
                for (; x < width - dx; ++x)
                    pixel[x, y] = pixel[x + dx, y];
                for (; x < width; ++x)
                    pixel[x, y] = 255;
            }*/
        }

        static int[] waveyxi2 = new int[] {
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 1, 1, 0,
            0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 4,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 1, 0,
            0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 4, 4,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3,
            4, 4, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1, 1, 1, 1, 1,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        static void straightXi2(byte[,] pixel, int width, int height)
        {
            for (var x = 0; x < width; ++x)
            {
                var dy = waveyxi2[x];
                if (dy > 0)
                {
                    var y = 0;
                    for (; y < height - dy; ++y)
                        pixel[x, y] = pixel[x, y + dy];
                    for (; y < height; ++y)
                        pixel[x, y] = 255;
                }
            }
            /*int[] wavex = new int[] {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                2, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 5, 5,
                6, 6, 6, 6, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0
            };
            for (var y = 16; y < height; ++y)
            {
                var dx = wavex[y];
                var x = 0;
                for (; x < width - dx; ++x)
                    pixel[x, y] = pixel[x + dx, y];
                for (; x < width; ++x)
                    pixel[x, y] = 255;
            }*/
        }

        public static Bitmap Do(string path, int type)
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
                if (type == 0)
                    straight(pixel, width, height);
                if (type == 1)
                    straightSun(pixel, width, height);
                else if (type == 2)
                    straightHei(pixel, width, height);
                else if (type == 3)
                    straightXi(pixel, width, height);
                else if (type == 4)
                    straightXi2(pixel, width, height);
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
                //File.WriteAllLines(path + ".txt", lines);
            }
            bmp.UnlockBits(bmpData);
            bmp.Save(path + type + ".png");
            return bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Do("R:\\1.jpg", 0);
        }
    }
}
