using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APL_Final_Project
{
    class USM
    {
        private static double[,] kernel = new double[3, 3]
        {
            { 1 / 9, 1 / 9, 1 / 9 },
            { 1 / 9, 1 / 9, 1 / 9 },
            { 1 / 9, 1 / 9, 1 / 9 },
        };


        public static void UnsharpMaskingCs(string sInputFile, string sOutputFile)
        {
            kernel = new double[3, 3]
            {
                { 1d / 9, 1d / 9, 1d / 9 },
                { 1d / 9, 1d / 9, 1d / 9 },
                { 1d / 9, 1d / 9, 1d / 9 },
            };

            var imgInput = (Bitmap)Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), sInputFile));
            var imgOutput = new Bitmap(imgInput.Width, imgInput.Height);

            var iMiddle = 3 / 2;

            for (var x = iMiddle; x < imgInput.Width - iMiddle; x++)
                for(var y = iMiddle; y < imgInput.Height - iMiddle; y++)
                {
                    var originalPixel = imgInput.GetPixel(x, y);

                    var acc = new double[3] { 0, 0, 0 };

                    for(var a = 0; a < 3; a++)
                        for (var b = 0; b < 3; b++)
                        {
                            var xn = x + a - iMiddle;
                            var yn = y + b - iMiddle;

                            var pixel = imgInput.GetPixel(xn, yn);

                            acc[0] += pixel.R * kernel[a, b];
                            acc[1] += pixel.G * kernel[a, b];
                            acc[2] += pixel.B * kernel[a, b];
                        }

                    imgOutput.SetPixel(x, y, Color.FromArgb(
                        (int)Math.Abs(originalPixel.R + (originalPixel.R - acc[0]) * 2) % 255,
                        (int)Math.Abs(originalPixel.G + (originalPixel.G - acc[1]) * 2) % 255,
                        (int)Math.Abs(originalPixel.B + (originalPixel.B - acc[2]) * 2) % 255
                    ));
                }

            imgOutput.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), sOutputFile));
        }


        [DllImport("USM-Cpp.dll", EntryPoint = "UnsharpMasking")]
        public static extern void UnsharpMaskingCpp();


        [DllImport("USM-Asm.dll", EntryPoint = "UnsharpMasking")]
        public static extern void UnsharpMaskingAsm();
    }
}
