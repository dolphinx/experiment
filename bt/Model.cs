using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace JDCapcha
{
    static partial class Model
    {
        static void clear(byte[,] pixel, int width, int height)
        {
            const byte f = 150;
            const int N = 2;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (pixel[x + 1, y + 1] != 255)
                    {
                        int count = 0;
                        for (int j = 0; j < 3; j++)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (pixel[x + i, y + j] < f && i != 1 && j != 1)
                                {
                                    if (++count >= N)
                                        break;
                                }
                            }
                            if (count >= N)
                                break;
                        }
                        pixel[x + 1, y + 1] = count < N ? (byte)255 : (byte)0;
                    }
                }
            }
        }

        public static string Do(string path, bool outputModel)
        {
            Bitmap bmp = new Bitmap(path);
            int width = bmp.Width;
            int height = bmp.Height;
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            System.IntPtr scan = bmpData.Scan0;
            byte[,] pixel = new byte[width + 2, height + 2];
            float Value = 10;
            unsafe
            {
                byte* srcP = (byte*)(void*)scan;
                int srcOffset = bmpData.Stride - width * 3;
                for (int y = 0; y < height + 2; y++)
                {
                    pixel[0, y] = 255;
                    pixel[width + 1, y] = 255;
                }
                for (int x = 1; x < width + 1; x++)
                {
                    pixel[x, 0] = 255;
                    pixel[x, height + 1] = 255;
                }
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
                        /*srcP[0] = blue;
                        srcP[1] = green;
                        srcP[2] = red;*/
                        pixel[x + 1, y + 1] = (byte)(.334 * red + .333 * green + .333 * blue);//(byte)(.299 * red + .587 * green + .114 * blue);
                    }
                    srcP += srcOffset;
                }
                clear(pixel, width, height);
                clear(pixel, width, height);
                srcP = (byte*)(void*)scan;
                srcOffset = bmpData.Stride - width * 3;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, srcP += 3)
                    {
                        srcP[2] = srcP[1] = srcP[0] = pixel[x + 1, y + 1];
                    }
                    srcP += srcOffset;
                }
            }
            bmp.UnlockBits(bmpData);
            return "";
        }
    }
}
