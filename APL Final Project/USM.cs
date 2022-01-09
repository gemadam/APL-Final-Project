using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APL_Final_Project
{
    public struct USMResult
    {
        public string ExecutionTimeString;
        public Bitmap Image;
    }

    public struct CppBMP
    {
        public int width;
        public int height;

        public IntPtr data;
        public IntPtr outData;
    }

    class USM
    {
        public static Task<USMResult> UnsharpMaskingCs(Bitmap imgInput, int[,] kernel)
        {
            return Task.Run(() =>
            {

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                var imgOutput = new Bitmap(imgInput.Width, imgInput.Height);

                var iMiddle = 3 / 2;

                for (var x = iMiddle; x < imgInput.Width - iMiddle; x++)
                    for (var y = iMiddle; y < imgInput.Height - iMiddle; y++)
                    {
                        var originalPixel = imgInput.GetPixel(x, y);

                        var acc = new double[3] { 0, 0, 0 };

                        for (var a = 0; a < 3; a++)
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

                stopWatch.Stop();

                
                return new USMResult()
                {
                    ExecutionTimeString = String.Format(
                        "{0:00}:{1:00}:{2:00}.{3:00}",
                        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10
                    ),
                    Image = imgOutput
                };
            });
        }


        private static byte[] makeByteArrayFromBitmap(Bitmap bmp)
        {
            byte[] buffer = new byte[3 * bmp.Width * bmp.Height];

            for (var x = 0; x < bmp.Width; x++)
                for (var y = 0; y < bmp.Height; y++)
                {
                    var pixel = bmp.GetPixel(x, y);

                    buffer[x * bmp.Width + y] = pixel.R;
                    buffer[x * bmp.Width + y + 1] = pixel.G;
                    buffer[x * bmp.Width + y + 2] = pixel.B;
                }

            return buffer;
        }

        public static Task<USMResult> UnsharpMaskingCpp(Bitmap imgInput, int[,] kernel)
        {
            return Task.Run(() => {
                
                var myBMP = new CppBMP()
                {
                    width = imgInput.Width,
                    height = imgInput.Height,

                    data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (3 * imgInput.Width * imgInput.Height)),
                    outData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (3 * imgInput.Width * imgInput.Height))
                };

                var imgOutput = new Bitmap(myBMP.width, myBMP.height);
                Stopwatch stopWatch = new Stopwatch();
                try
                {
                    byte[] buffer = makeByteArrayFromBitmap(imgInput);
                    Marshal.Copy(buffer, 0, myBMP.data, buffer.Length);
                    Marshal.Copy(buffer, 0, myBMP.outData, buffer.Length);

                    stopWatch.Start();

                    usmCpp(ref myBMP, kernel);

                    stopWatch.Stop();

                    Marshal.Copy(myBMP.outData, buffer, 0, buffer.Length);

                    for (var x = 0; x < myBMP.width; x++)
                        for (var y = 0; y < myBMP.height; y++)
                        {
                            var pixel = imgInput.GetPixel(x, y);

                            imgOutput.SetPixel(x, y, Color.FromArgb(
                                buffer[x * imgInput.Width + y],
                                buffer[x * imgInput.Width + y + 1],
                                buffer[x * imgInput.Width + y + 2]
                            ));
                        }

                }
                finally
                {
                    Marshal.FreeHGlobal(myBMP.data);
                    Marshal.FreeHGlobal(myBMP.outData);
                }


                return new USMResult()
                {
                    ExecutionTimeString = String.Format(
                        "{0:00}:{1:00}:{2:00}.{3:00}",
                        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10
                    ),
                    Image = imgOutput
                };
            });
        }

        public static Task<USMResult> UnsharpMaskingAsm(Bitmap imgInput, int[,] kernel)
        {
            return Task.Run(() => {

                var myBMP = new CppBMP()
                {
                    width = imgInput.Width,
                    height = imgInput.Height,

                    data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (3 * imgInput.Width * imgInput.Height)),
                    outData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (3 * imgInput.Width * imgInput.Height))
                };

                var imgOutput = new Bitmap(myBMP.width, myBMP.height);
                Stopwatch stopWatch = new Stopwatch();
                try
                {
                    byte[] buffer = makeByteArrayFromBitmap(imgInput);
                    Marshal.Copy(buffer, 0, myBMP.data, buffer.Length);
                    Marshal.Copy(buffer, 0, myBMP.outData, buffer.Length);

                    stopWatch.Start();

                    try
                    {
                        usmAsm(myBMP.data, myBMP.outData, imgInput.Width, imgInput.Height);
                    }
                    catch (Exception ex)
                    {
                        myBMP.outData = myBMP.data;
                    }

                    stopWatch.Stop();

                    Marshal.Copy(myBMP.outData, buffer, 0, buffer.Length);

                    for (var x = 0; x < myBMP.width; x++)
                        for (var y = 0; y < myBMP.height; y++)
                        {
                            var pixel = imgInput.GetPixel(x, y);

                            imgOutput.SetPixel(x, y, Color.FromArgb(
                                buffer[x * imgInput.Width + y],
                                buffer[x * imgInput.Width + y + 1],
                                buffer[x * imgInput.Width + y + 2]
                            ));
                        }

                }
                finally
                {
                    Marshal.FreeHGlobal(myBMP.data);

                    if(myBMP.outData != myBMP.data)
                        Marshal.FreeHGlobal(myBMP.outData);
                }


                return new USMResult()
                {
                    ExecutionTimeString = String.Format(
                        "{0:00}:{1:00}:{2:00}.{3:00}",
                        stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes,
                        stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10
                    ),
                    Image = imgOutput
                };
            });
        }

        [DllImport("USM-Cpp.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCpp")]
        private static extern void usmCpp([In, Out] ref CppBMP cppBMP, [In] int[,] kernel);


        [DllImport("USM-Asm.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMasking")]
        public static extern void usmAsm([In] IntPtr inImg, [In, Out] IntPtr outImg, [In] int width, [In] int height);
    }
}
