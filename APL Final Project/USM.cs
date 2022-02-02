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
        public TimeSpan ExecutionTime;
        public string ExecutionTimeString;
        public Bitmap Image;
    }

    public unsafe struct USMFunctionInput
    {
        public int width;
        public int height;

        public float* kernel;

        public float* inChannelR;
        public float* inChannelG;
        public float* inChannelB;

        public float* outChannelR;
        public float* outChannelG;
        public float* outChannelB;
    }

    class USM
    {
        private static USMResult makeTest(Bitmap imgInput, int[] kernel, Action<USMFunctionInput> fUSM)
        {
            Stopwatch stopWatch = new Stopwatch();

            Bitmap imgOutput = null;

            BmpUtil.splitIntoChannels(imgInput, out var arrR, out var arrG, out var arrB);

            var outChannelR = new float[imgInput.Width * imgInput.Height];
            var outChannelG = new float[imgInput.Width * imgInput.Height];
            var outChannelB = new float[imgInput.Width * imgInput.Height];

            var functionInput = new USMFunctionInput();
            unsafe
            {
                fixed (float* pArrR = arrR, pArrG = arrG, pArrB = arrB, pOutChannelR = outChannelR, pOutChannelG = outChannelG, pOutChannelB = outChannelB)
                    fixed (float* kernelPtr = BmpUtil.Normalize(kernel, kernel.Min(), kernel.Max()))
                    {
                        functionInput.height = imgInput.Height;
                        functionInput.width = imgInput.Width;
                        functionInput.inChannelR = pArrR;
                        functionInput.inChannelG = pArrG;
                        functionInput.inChannelB = pArrB;
                        functionInput.outChannelR = pOutChannelR;
                        functionInput.outChannelG = pOutChannelG;
                        functionInput.outChannelB = pOutChannelB;
                        functionInput.kernel = kernelPtr;

                        stopWatch.Start();

                        fUSM.Invoke(functionInput);

                        stopWatch.Stop();

                        imgOutput = BmpUtil.mergeChannels(imgInput.Width, imgInput.Height, outChannelR, outChannelG, outChannelB);
                    }
            }

            return new USMResult()
            {
                ExecutionTime = stopWatch.Elapsed,
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
            return Task.Run(() => makeTest(imgInput, kernel, usmCpp));
        }

        public static Task<USMResult> UnsharpMaskingCppV2(Bitmap imgInput, int[] kernel)
        {
            return Task.Run(() => makeTest(imgInput, kernel, usmCppV2));
        }

        public static Task<USMResult> UnsharpMaskingAsm(Bitmap imgInput, int[] kernel)
        {
            return Task.Run(() => makeTest(imgInput, kernel, usmAsm));
        }

        #endregion


        #region DLL_FUNCTIONS

        [DllImport("USM-Cpp.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCpp")]
        private static unsafe extern void usmCpp(USMFunctionInput input);

        [DllImport("USM-Cpp-Optimized.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCpp")]
        private static unsafe extern void usmCppV2(USMFunctionInput input);

        [DllImport("USM-Asm.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMasking")]
        public static unsafe extern void usmAsm(USMFunctionInput input);

        #endregion
    }
}
