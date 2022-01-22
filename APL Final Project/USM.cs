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

    public unsafe struct USMFunctionInput
    {
        public int width;
        public int height;

        public int* kernel;

        public byte* inChannelR;
        public byte* inChannelG;
        public byte* inChannelB;

        public byte* outChannelR;
        public byte* outChannelG;
        public byte* outChannelB;
    }

    class USM
    {
        private static USMResult makeTest(Bitmap imgInput, int[] kernel, Func<USMFunctionInput, USMFunctionInput> fUSM)
        {
            Stopwatch stopWatch = new Stopwatch();

            Bitmap imgOutput = null;

            BmpUtil.splitIntoChannels(imgInput, out var arrR, out var arrG, out var arrB);
            BmpUtil.splitIntoChannels(
                BmpUtil.makeBlackBitmap(imgInput.Width, imgInput.Height),
                out var arrBlackR, out var arrBlackG, out var arrBlackB
            );

            var outChannelR = new byte[imgInput.Width * imgInput.Height];
            var outChannelG = new byte[imgInput.Width * imgInput.Height];
            var outChannelB = new byte[imgInput.Width * imgInput.Height];

            unsafe
            {
                fixed (byte* pArrR = arrR, pArrG = arrG, pArrB = arrB, pOutChannelR = outChannelR, pOutChannelG = outChannelG, pOutChannelB = outChannelB)
                    fixed (int* kernelPtr = kernel)
                    {
                        var functionInput = new USMFunctionInput()
                        {
                            height = imgInput.Height,
                            width = imgInput.Width,
                            inChannelR = pArrR,
                            inChannelG = pArrG,
                            inChannelB = pArrB,
                            outChannelR = pOutChannelR,
                            outChannelG = pOutChannelG,
                            outChannelB = pOutChannelB,
                            kernel = kernelPtr
                        };

                        stopWatch.Start();

                        functionInput = fUSM.Invoke(functionInput);

                        stopWatch.Stop();

                        imgOutput = BmpUtil.mergeChannels(imgInput.Width, imgInput.Height, outChannelR, outChannelG, outChannelB);
                    }
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
            return Task.Run(() => makeTest(imgInput, kernel, (USMFunctionInput input) => { 

                unsafe
                {
                    usmCpp(&input);
                }

                return input;
            }));
        }

        public static Task<USMResult> UnsharpMaskingCppV2(Bitmap imgInput, int[] kernel)
        {
            return Task.Run(() => makeTest(imgInput, kernel, (USMFunctionInput input) => {

                unsafe
                {
                    usmCppV2(&input);
                }

                return input;
            }));
        }

        public static Task<USMResult> UnsharpMaskingAsm(Bitmap imgInput, int[] kernel)
        {
            return Task.Run(() => makeTest(imgInput, kernel, (USMFunctionInput input) => {

                unsafe
                {
                    usmAsm(&input);
                }

                return input;
            }));
        }

        #endregion


        #region DLL_FUNCTIONS

        [DllImport("USM-Cpp.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCpp")]
        private static unsafe extern void usmCpp(USMFunctionInput* input);

        [DllImport("USM-Cpp.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMaskingCppV2")]
        private static unsafe extern void usmCppV2(USMFunctionInput* input);

        [DllImport("USM-Asm.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UnsharpMasking")]
        public static unsafe extern void usmAsm(USMFunctionInput* input);

        #endregion
    }
}
