#pragma once
#include <windows.h>
#ifdef USM_Asm_EXPORTS
#define USM_Asm_API __declspec(dllexport)
#else
#define USM_Asm_API __declspec(dllimport)
#endif
extern "C" USM_Asm_API void UnsharpMasking(unsigned char* bitmapArray, unsigned char* filterArray , unsigned char brightness);