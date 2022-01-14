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

        public IntPtr kernel;

        public IntPtr inChannelR;
        public IntPtr inChannelG;
        public IntPtr inChannelB;

        public IntPtr outChannelR;
        public IntPtr outChannelG;
        public IntPtr outChannelB;
    }

    class USM
    {
        private static float[] to10(byte[] arrIn, float minVal, float maxVal)
        {
            float[] arrOut = new float[arrIn.Length];

            var rangeVal = maxVal - minVal;


            for (var i = 0; i < arrIn.Length; i++)
                arrOut[i] = (arrIn[i] - minVal) / rangeVal;

            return arrOut;
        }

        private static byte[] to255(float[] arrIn)
        {
            byte[] arrOut = new byte[arrIn.Length];

            for (var i = 0; i < arrIn.Length; i++)
                arrOut[i] = (byte)(255 * arrIn[i]);

            return arrOut;
        }


        private static USMResult makeTest(Bitmap imgInput, int[] kernel, Func<CppBMP, bool> fUSM)
        {
            Stopwatch stopWatch = new Stopwatch();

            Bitmap imgOutput = null;
            var functionInput = new CppBMP()
            {
                height = imgInput.Height,
                width = imgInput.Width,
                kernel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * 9),

                inChannelR = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (imgInput.Width * imgInput.Height)),
                inChannelG = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (imgInput.Width * imgInput.Height)),
                inChannelB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (imgInput.Width * imgInput.Height)),

                outChannelR = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (imgInput.Width * imgInput.Height)),
                outChannelG = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (imgInput.Width * imgInput.Height)),
                outChannelB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * (imgInput.Width * imgInput.Height))
            };

            try
            {
                BmpUtil.splitIntoChannels(imgInput, out var arrR, out var arrG, out var arrB);
                BmpUtil.splitIntoChannels(
                    BmpUtil.makeBlackBitmap(imgInput.Width, imgInput.Height), 
                    out var arrBlackR, out var arrBlackG, out var arrBlackB
                );

                Marshal.Copy(kernel, 0, functionInput.kernel, kernel.Length);

                Marshal.Copy(arrR, 0, functionInput.inChannelR, arrR.Length);
                Marshal.Copy(arrG, 0, functionInput.inChannelG, arrG.Length);
                Marshal.Copy(arrB, 0, functionInput.inChannelB, arrB.Length);

                Marshal.Copy(arrBlackR, 0, functionInput.outChannelR, arrBlackR.Length);
                Marshal.Copy(arrBlackG, 0, functionInput.outChannelG, arrBlackG.Length);
                Marshal.Copy(arrBlackB, 0, functionInput.outChannelB, arrBlackB.Length);


                stopWatch.Start();

                fUSM.Invoke(functionInput);

                stopWatch.Stop();


                Marshal.Copy(functionInput.outChannelR, arrR, 0, arrR.Length);
                Marshal.Copy(functionInput.outChannelR, arrG, 0, arrR.Length);
                Marshal.Copy(functionInput.outChannelR, arrB, 0, arrR.Length);

                imgOutput = BmpUtil.mergeChannels(imgInput.Width, imgInput.Height, arrR, arrG, arrB);
            }
            finally
            {
                Marshal.FreeHGlobal(functionInput.inChannelR);
                Marshal.FreeHGlobal(functionInput.inChannelG);
                Marshal.FreeHGlobal(functionInput.inChannelB);
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
        }


        #region DLL_FUNCTIONS_TESTS

        public static Task<USMResult> UnsharpMaskingCpp(Bitmap imgInput, int[] kernel)
        {
            return Task.Run(() => makeTest(imgInput, kernel, (CppBMP input) => { usmCpp(ref input); return true; }));
        }

        public static Task<USMResult> UnsharpMaskingCppV2(Bitmap imgInput, int[] kernel)
        {
            return Task.Run(() => makeTest(imgInput, kernel, (CppBMP input) => { usmCppV2(ref input); return true; }));
        }

        public static Task<USMResult> UnsharpMaskingAsm(Bitmap imgInput, int[] kernel)
        {
            return Task.Run(() => makeTest(imgInput, kernel, (CppBMP input) => { usmAsm(ref input); return true; }));
        }

        #endregion


        #region DLL_FUNCTIONS

        [DllImport("USM-Cpp.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCpp")]
        private static extern void usmCpp([In, Out] ref CppBMP cppBMP);

        [DllImport("USM-Cpp.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCppV2")]
        private static extern void usmCppV2([In, Out] ref CppBMP cppBMP);

        [DllImport("USM-Asm.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMasking")]
        public static extern void usmAsm([In, Out] ref CppBMP input);

        #endregion
    }
}
