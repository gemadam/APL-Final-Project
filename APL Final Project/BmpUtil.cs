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
        public static void splitIntoChannels(Bitmap bmp, out byte[] arrR, out byte[] arrG, out byte[] arrB)
        {
            arrR = new byte[bmp.Width * bmp.Height];
            arrG = new byte[bmp.Width * bmp.Height];
            arrB = new byte[bmp.Width * bmp.Height];

            var iBufferIterator = 0;
            for (var y = 0; y < bmp.Height; y++)
                for (var x = 0; x < bmp.Width; x++)
                {
                    var pixel = bmp.GetPixel(x, y);

                    arrR[iBufferIterator] = (pixel.R);
                    arrG[iBufferIterator] = (pixel.G);
                    arrB[iBufferIterator++] = (pixel.B);
                }

            //var minVal = Math.Min(arrR1.Min(), Math.Min(arrG1.Min(), arrB1.Min()));
            //var maxVal = Math.Max(arrR1.Max(), Math.Max(arrG1.Max(), arrB1.Max()));
            //arrR = to10(arrR1, minVal, maxVal);
            //arrG = to10(arrG1, minVal, maxVal);
            //arrB = to10(arrB1, minVal, maxVal);
        }

        public static Bitmap mergeChannels(int w, int h, byte[] arrR, byte[] arrG, byte[] arrB)
        {
            Bitmap bmp = new Bitmap(w, h);

            //var normalizedR = to255(arrR);
            //var normalizedG = to255(arrG);
            //var normalizedB = to255(arrB);


            var iBufferIterator = 0;
            for (var y = 0; y < bmp.Height; y++)
                for (var x = 0; x < bmp.Width; x++)
                    bmp.SetPixel(x, y, Color.FromArgb(
                        arrR[iBufferIterator],
                        arrG[iBufferIterator],
                        arrB[iBufferIterator++]
                    ));

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
