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

struct Pixel
{
	unsigned char b, g, r;
}pixel;

extern "C" __declspec(dllexport) void UnsharpMaskingCpp(CppBMP &img, int kernel[3][3])
{
    int iPixelIterator = 0;

	// USM
	for (int y = 1; y < img.height - 1; y++)
		for (int x = 1; x < img.width - 1; x++)
        {
            int acc[3] = { 0, 0, 0 };

            for (auto a = 0; a < 3; a++)
                for (auto b = 0; b < 3; b++)
                {
                    auto xn = x + a - 1;
                    auto yn = y + b - 1;

                    auto pixelR = img.data[yn * img.width + xn];
                    auto pixelG = img.data[yn * img.width + xn + 1];
                    auto pixelB = img.data[yn * img.width + xn + 2];

                    acc[0] += pixelR * kernel[a][b];
                    acc[1] += pixelG * kernel[a][b];
                    acc[2] += pixelB * kernel[a][b];
                }

            img.outData[iPixelIterator] = acc[0];
            img.outData[iPixelIterator + 1] = acc[1];
            img.outData[iPixelIterator + 2] = acc[2];

            iPixelIterator += 3;
        }
}


extern "C" __declspec(dllexport) void UnsharpMaskingCppV2(CppBMP & img, int kernel[3][3])
{
	int i, j, k, count = 0;

	auto dataIterator = 0, outDataIterator = 0;


	// Highboost filter
	float filter[9] = { 
		-1, -1, -1,
		-1, 8.9,-1,
		-1, -1, -1
	};


	// Initialize a, a1 buffers
	Pixel** a = new Pixel * [img.height];
	Pixel** a1 = new Pixel * [img.height];
	for (int i = 0; i < img.height; i++)
	{
		a[i] = new Pixel[img.width];
		a1[i] = new Pixel[img.width];

		for (j = 0; j < img.width; j++)
		{
			a[i][j].r = img.data[dataIterator++]; a1[i][j].r = 0;
			a[i][j].g = img.data[dataIterator++]; a1[i][j].g = 0;
			a[i][j].b = img.data[dataIterator++]; a1[i][j].b = 0;
		}
	}

	count = 0;
	// to write first line of original image to modified image file (border1)
	for (k = 0; k < img.width; k++)
	{
		img.outData[outDataIterator++] = a[0][k].r;
		img.outData[outDataIterator++] = a[0][k].g;
		img.outData[outDataIterator++] = a[0][k].b;
	}





	for (i = 1; i < img.height - 1; i++)
	{
		for (j = 1; j < (img.width - 1); j++)
		{
			a1[i][j].r = (1 / 9.0) * ((int)a[i - 1][j - 1].r * filter[0] + (int)a[i][j -
				1].r * filter[1] + (int)a[i + 1][j - 1].r * filter[2] + (int)a[i - 1][j].r * filter[3] + (int)a[i][j].r * filter[4] +
				(int)a[i + 1][j].r * filter[5] + (int)a[i - 1][j + 1].r * filter[6] + (int)a[i][j + 1].r * filter[7] +
				(int)a[i + 1][j + 1].r * filter[8]);

			//a1[i][j].g = (1/9.0)*((int)a[i-1][j-1].g*filter[0]+(int)a[i][j-1].g* filter[1] + (int)a[i + 1][j - 1].g * filter[2] + (int)a[i - 1][j].g * filter[3] + (int)a[i][j].g * filter[4] + (int)a[i + 1][j].g * filter[5] + (int)a[i - 1][j + 1].g * filter[6] + (int)a[i][j + 1].g * filter[7] + (int)a[i + 1][j + 1].g * filter[8]);
			
			//a1[i][j].b = (1/9.0)*((int)a[i-1][j-1].b*filter[0]+(int)a[i][j-1].b* filter[1] + (int)a[i + 1][j - 1].b * filter[2] + (int)a[i - 1][j].b * filter[3] + (int)a[i][j].b * filter[4] + (int)a[i + 1][j].b * filter[5] + (int)a[i - 1][j + 1].b * filter[6] + (int)a[i][j + 1].b * filter[7] + (int)a[i + 1][j + 1].b * filter[8]);
			
			a1[i][j].g = a[i][j].g;
			a1[i][j].b = a[i][j].b;
		}
		for (k = 0; k < img.width; k++)
		{
			// To write left and write border
			if (k == 0 || k == img.width - 1)
			{
				pixel.r = a[i][k].r;
				pixel.g = a[i][k].g;
				pixel.b = a[i][k].b;
			}
			// To write middle modified contents 
			else
			{
				pixel.r = a1[i][k].r;
				pixel.g = a1[i][k].g;
				pixel.b = a1[i][k].b;
			}

			img.outData[outDataIterator++] = pixel.r;
			img.outData[outDataIterator++] = pixel.g;
			img.outData[outDataIterator++] = pixel.b;
		}
	}

	// To write bottom border
	for (k = 0; k < img.width; k++)
	{
		pixel.r = a[img.height - 1][k].r;
		pixel.g = a[img.height - 1][k].g;
		pixel.b = a[img.height - 1][k].b;

		img.outData[outDataIterator++] = pixel.r;
		img.outData[outDataIterator++] = pixel.g;
		img.outData[outDataIterator++] = pixel.b;
	}


	for (int i = 0; i < img.height; i++)
	{
		delete[] a[i];
		delete[] a1[i];
	}
	delete[] a;
	delete[] a1;
}

