#include "USM.hpp"

#include <Windows.h>
#include <string>

struct CppBMP
{
    int width;
    int height;

    unsigned char* data;
    unsigned char* outData;
};

extern "C" __declspec(dllexport) void UnsharpMaskingCpp(CppBMP &img, int kernel[3][3])
{
    int iMiddle = 3 / 2;

    for (int x = iMiddle; x < img.width - iMiddle; x++)
        for (int y = iMiddle; y < img.height - iMiddle; y++)
        {
            auto originalR = img.data[x * img.width + y];
            auto originalG = img.data[x * img.width + y + 1];
            auto originalB = img.data[x * img.width + y + 2];

            double acc[3] = { 0, 0, 0 };

            for (auto a = 0; a < 3; a++)
                for (auto b = 0; b < 3; b++)
                {
                    auto xn = x + a - iMiddle;
                    auto yn = y + b - iMiddle;

                    auto pixelR = img.data[xn * img.width + yn];
                    auto pixelG = img.data[xn * img.width + yn + 1];
                    auto pixelB = img.data[xn * img.width + yn + 2];

                    acc[0] += pixelR * kernel[a][b];
                    acc[1] += pixelG * kernel[a][b];
                    acc[2] += pixelB * kernel[a][b];
                }

            img.outData[x * img.width + y] = (char)(originalR + (originalR - acc[0]) * 2) % 255;
            img.outData[x * img.width + y + 1] = (char)(originalG + (originalG - acc[1]) * 2) % 255;
            img.outData[x * img.width + y + 2] = (char)(originalB + (originalB - acc[2]) * 2) % 255;

        }
}
