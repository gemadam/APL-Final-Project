using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APL_Final_Project
{
    public static class BmpUtil
    {
        public static void splitIntoChannels(Bitmap bmp, out float[] arrR, out float[] arrG, out float[] arrB)
        {
            arrR = new float[bmp.Width * bmp.Height];
            arrG = new float[bmp.Width * bmp.Height];
            arrB = new float[bmp.Width * bmp.Height];

            var iBufferIterator = 0;
            for (var y = 0; y < bmp.Height; y++)
                for (var x = 0; x < bmp.Width; x++)
                {
                    var pixel = bmp.GetPixel(x, y);

                    arrR[iBufferIterator] = (pixel.R / 255f);
                    arrG[iBufferIterator] = (pixel.G / 255f);
                    arrB[iBufferIterator++] = (pixel.B / 255f);
                }
        }

        public static float[] Normalize<T>(T[] array, T min, T max)
        {
            var arrayOut = new float[array.Length];

            var fMin = (float)Convert.ToDouble(min);
            var fMax = (float)Convert.ToDouble(max);

            for (var i = 0; i < array.Length; i++)
                arrayOut[i] = ((float)Convert.ToDouble(array[i]) - fMin) / (fMax - fMin);

            return arrayOut;
        }

        public static Bitmap mergeChannels(int w, int h, float[] arrR, float[] arrG, float[] arrB)
        {
            Bitmap bmp = new Bitmap(w, h);

            var normalizedArrR = Normalize(arrR, Math.Min(0, arrR.Min()), Math.Max(1, arrR.Max()));
            var normalizedArrG = Normalize(arrG, Math.Min(0, arrG.Min()), Math.Max(1, arrG.Max()));
            var normalizedArrB = Normalize(arrB, Math.Min(0, arrB.Min()), Math.Max(1, arrB.Max()));

            var iBufferIterator = 0;
            for (var x = 0; x < bmp.Width; x++)
            {
                var r = (int)(arrR[iBufferIterator] * 255);
                var g = (int)(arrG[iBufferIterator] * 255);
                var b = (int)(arrB[iBufferIterator] * 255);

                var color = Color.FromArgb(r, g, b);

                bmp.SetPixel(x, 0, color);

                iBufferIterator++;
            }

            for (var y = 1; y < bmp.Height - 1; y++)
            {
                var r = (int)(arrR[iBufferIterator] * 255);
                var g = (int)(arrG[iBufferIterator] * 255);
                var b = (int)(arrB[iBufferIterator] * 255);

                var color = Color.FromArgb(r, g, b);
                bmp.SetPixel(0, y, color);
                iBufferIterator++;

                for (var x = 1; x < bmp.Width - 1; x++)
                {
                    r = (int)(normalizedArrR[iBufferIterator] * 255);
                    g = (int)(normalizedArrG[iBufferIterator] * 255);
                    b = (int)(normalizedArrB[iBufferIterator] * 255);

                    color = Color.FromArgb(r, g, b);

                    bmp.SetPixel(x, y, color);

                    iBufferIterator++;
                }

                r = (int)(arrR[iBufferIterator] * 255);
                g = (int)(arrR[iBufferIterator] * 255);
                b = (int)(arrR[iBufferIterator] * 255);

                color = Color.FromArgb(r, g, b);
                bmp.SetPixel(w - 1, y, color);

                iBufferIterator++;
            }

            for (var x = 0; x < bmp.Width; x++)
            {
                var r = (int)(arrR[iBufferIterator] * 255);
                var g = (int)(arrG[iBufferIterator] * 255);
                var b = (int)(arrB[iBufferIterator] * 255);

                var color = Color.FromArgb(r, g, b);

                bmp.SetPixel(x, h - 1, color);

                iBufferIterator++;
            }

            return bmp;
        }

        public static Bitmap makeBlackBitmap(int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);

            for (var y = 0; y < bmp.Height; y++)
                for (var x = 0; x < bmp.Width; x++)
                    bmp.SetPixel(x, y, Color.FromArgb(x % 255, x % 255, x % 255));

            return bmp;
        }
    }
}
