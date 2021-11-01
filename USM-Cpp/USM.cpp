#include "USM.hpp"

#include <Windows.h>


extern "C" __declspec(dllexport) void UnsharpMasking()
{
    MessageBox(0, L"This is message box displayed by UnsharpMasking function in C++ version\n", L"Hello there", MB_ICONINFORMATION);
}
