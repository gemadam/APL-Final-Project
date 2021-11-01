using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APL_Final_Project
{
    class USM
    {
        public static void UnsharpMaskingCs()
        {
            MessageBox.Show("This is message box displayed by UnsharpMasking function in C# version", "Hello there");
        }


        [DllImport("USM-Cpp.dll", EntryPoint = "UnsharpMasking")]
        public static extern void UnsharpMaskingCpp();


        [DllImport("USM-Asm.dll", EntryPoint = "UnsharpMasking")]
        public static extern void UnsharpMaskingAsm();
    }
}
