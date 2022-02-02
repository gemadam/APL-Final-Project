using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APL_Final_Project
{
    internal static class BmpUtil
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

            arrR = Normalize(arrR, arrR.Min(), arrR.Max());
            arrG = Normalize(arrG, arrG.Min(), arrG.Max());
            arrB = Normalize(arrB, arrB.Min(), arrB.Max());

            var iBufferIterator = 0;
            for (var y = 0; y < bmp.Height; y++)
                for (var x = 0; x < bmp.Width; x++)
                {
                    var r = (int)(arrR[iBufferIterator] * 255);
                    var g = (int)(arrG[iBufferIterator] * 255);
                    var b = (int)(arrB[iBufferIterator] * 255);

                    var color = Color.FromArgb(r, g, b);

                    bmp.SetPixel(x, y, color);

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
