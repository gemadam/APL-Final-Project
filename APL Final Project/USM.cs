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
                double [,] k =
                {
                    { 1/9, 1/9, 1/9 },
                    { 1/9, 1/9, 1/9 },
                    { 1/9, 1/9, 1/9 },
                };

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                var imgOutput = new Bitmap(imgInput.Width, imgInput.Height);

                var iMiddle = 3 / 2;

                for (var x = iMiddle; x < imgInput.Width - iMiddle; x++)
                    for (var y = iMiddle; y < imgInput.Height - iMiddle; y++)
                    {
                        var originalPixel = imgInput.GetPixel(x, y);

                        var acc = new int[3] { 0, 0, 0 };

                        for (var a = 0; a < 3; a++)
                            for (var b = 0; b < 3; b++)
                            {
                                var xn = x + a - iMiddle;
                                var yn = y + b - iMiddle;

                                var pixel = imgInput.GetPixel(xn, yn);

                                acc[0] += (int)(pixel.R * k[a, b]);
                                acc[1] += (int)(pixel.G * k[a, b]);
                                acc[2] += (int)(pixel.B * k[a, b]);
                            }

                        int newR = originalPixel.R + (originalPixel.R - acc[0]) * 2;
                        int newG = originalPixel.G + (originalPixel.G - acc[1]) * 2;
                        int newB = originalPixel.B + (originalPixel.B - acc[2]) * 2;


                        imgOutput.SetPixel(x, y, Color.FromArgb(newR % 255, newG % 255, newB % 255));
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

            var iBufferIterator = 0;
            for (var x = 0; x < bmp.Width; x++)
                for (var y = 0; y < bmp.Height; y++)
                {
                    var pixel = bmp.GetPixel(x, y);

                    buffer[iBufferIterator++] = pixel.R;
                    buffer[iBufferIterator++] = pixel.G;
                    buffer[iBufferIterator++] = pixel.B;
                }

            return buffer;
        }


        private static Bitmap makeBitmapFromByteArray(byte[] array, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);

            var iBufferIterator = 0;
            for (var x = 0; x < bmp.Width; x++)
                for (var y = 0; y < bmp.Height; y++)
                {
                    int r = array[iBufferIterator++];
                    int g = array[iBufferIterator++];
                    int b = array[iBufferIterator++];

                    bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }

            return bmp;
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

                    imgOutput = makeBitmapFromByteArray(buffer, imgInput.Width, imgInput.Height);

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

        public static Task<USMResult> UnsharpMaskingCppV2(Bitmap imgInput, int[,] kernel)
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

                    usmCppV2(ref myBMP, kernel);

                    stopWatch.Stop();

                    Marshal.Copy(myBMP.outData, buffer, 0, buffer.Length);

                    imgOutput = makeBitmapFromByteArray(buffer, imgInput.Width, imgInput.Height);

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

                    imgOutput = makeBitmapFromByteArray(buffer, imgInput.Width, imgInput.Height);

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

        [DllImport("USM-Cpp.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCppV2")]
        private static extern void usmCppV2([In, Out] ref CppBMP cppBMP, [In] int[,] kernel);


        [DllImport("USM-Asm.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMasking")]
        public static extern void usmAsm([In] IntPtr inImg, [In, Out] IntPtr outImg, [In] int width, [In] int height);
    }
}
