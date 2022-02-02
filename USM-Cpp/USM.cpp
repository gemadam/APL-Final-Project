#include "USM.hpp"

#include <Windows.h>
#include <string>


struct CppBMP
{
    int width;
    int height;

	float* kernel;

    float* inChannelR;
	float* inChannelG;
	float* inChannelB;

	float* outChannelR;
	float* outChannelG;
	float* outChannelB;
};

struct Pixel
{
	unsigned char b, g, r;
}pixel;

extern "C" __declspec(dllexport) void UnsharpMaskingCpp(CppBMP &input)
{
	int iBufferIterator = 0;

	// Top border
    for (int x = 0; x < input.width; x++)
    {
		input.outChannelR[iBufferIterator] = input.inChannelR[x];
		input.outChannelG[iBufferIterator] = input.inChannelG[x];
		input.outChannelB[iBufferIterator] = input.inChannelB[x];
		++iBufferIterator;
    }


	int iSum = 0;
	for (auto a = 0; a < 3; a++)
		for (auto b = 0; b < 3; b++)
			iSum += input.kernel[3 * b + a];

	for (int y = 1; y < input.height - 1; y++)
	{
		// Left border
		input.outChannelR[iBufferIterator] = input.inChannelR[y * input.width];
		input.outChannelG[iBufferIterator] = input.inChannelG[y * input.width];
		input.outChannelB[iBufferIterator] = input.inChannelB[y * input.width];
		++iBufferIterator;

		// Center of the image
		for (int x = 1; x < input.width - 1; x++)
		{
			auto newPixelValue = new float[3] { 0, 0, 0 };

			// Process the neigbourhood of the pixel
			for (auto a = 0; a < 3; a++)
				for (auto b = 0; b < 3; b++)
				{
					float kernelValue = (float)input.kernel[3 * a + b];
					int xn = x + b - 1;
					int yn = y + a - 1;

					auto pixelR = input.inChannelR[yn * input.width + xn];
					auto pixelG = input.inChannelG[yn * input.width + xn];
					auto pixelB = input.inChannelB[yn * input.width + xn];

					newPixelValue[0] += ((float)pixelR * kernelValue);
					newPixelValue[1] += ((float)pixelG * kernelValue);
					newPixelValue[2] += ((float)pixelB * kernelValue);
				}

			input.outChannelR[iBufferIterator] = newPixelValue[0];
			input.outChannelG[iBufferIterator] = newPixelValue[1];
			input.outChannelB[iBufferIterator] = newPixelValue[2];
			++iBufferIterator;
		}


		// Right border
		input.outChannelR[iBufferIterator] = input.inChannelR[y * input.width + input.width - 1];
		input.outChannelG[iBufferIterator] = input.inChannelG[y * input.width + input.width - 1];
		input.outChannelB[iBufferIterator] = input.inChannelB[y * input.width + input.width - 1];
		++iBufferIterator;
	}

	// Bottom border
    for (int x = 0; x < input.width; x++)
    {
		input.outChannelR[iBufferIterator] = input.inChannelR[(input.height - 1) * input.width + x];
		input.outChannelG[iBufferIterator] = input.inChannelG[(input.height - 1) * input.width + x];
		input.outChannelB[iBufferIterator] = input.inChannelB[(input.height - 1) * input.width + x];
		++iBufferIterator;
    }
}


extern "C" __declspec(dllexport) void UnsharpMaskingCppV2(CppBMP & input)
{
	int i, j, k, count = 0;

	auto dataIterator = 0, outDataIterator = 0;


	// Highboost input.kernel
	/*float filter[9] = { 
		-1, -1, -1,
		-1, 8.9,-1,
		-1, -1, -1
	};*/


	// Initialize a, a1 buffers
	Pixel** a = new Pixel * [input.height];
	Pixel** a1 = new Pixel * [input.height];
	for (int i = 0; i < input.height; i++)
	{
		a[i] = new Pixel[input.width];
		a1[i] = new Pixel[input.width];

		for (j = 0; j < input.width; j++)
		{
			a[i][j].r = input.inChannelR[dataIterator]; a1[i][j].r = 0;
			a[i][j].g = input.inChannelG[dataIterator]; a1[i][j].g = 0;
			a[i][j].b = input.inChannelB[dataIterator]; a1[i][j].b = 0;
			++dataIterator;
		}
	}

	count = 0;
	// to write first line of original image to modified image file (border1)
	for (k = 0; k < input.width; k++)
	{
		input.outChannelR[outDataIterator] = a[0][k].r;
		input.outChannelG[outDataIterator] = a[0][k].g;
		input.outChannelR[outDataIterator] = a[0][k].b;

		++outDataIterator;
	}


	for (i = 1; i < input.height - 1; i++)
	{
		for (j = 1; j < (input.width - 1); j++)
		{
			a1[i][j].r = (1 / 9.0) * ((int)a[i - 1][j - 1].r * input.kernel[0] + (int)a[i][j -
				1].r * input.kernel[1] + (int)a[i + 1][j - 1].r * input.kernel[2] + (int)a[i - 1][j].r * input.kernel[3] + (int)a[i][j].r * input.kernel[4] +
				(int)a[i + 1][j].r * input.kernel[5] + (int)a[i - 1][j + 1].r * input.kernel[6] + (int)a[i][j + 1].r * input.kernel[7] +
				(int)a[i + 1][j + 1].r * input.kernel[8]);

			//a1[i][j].g = (1/9.0)*((int)a[i-1][j-1].g*input.kernel[0]+(int)a[i][j-1].g* input.kernel[1] + (int)a[i + 1][j - 1].g * input.kernel[2] + (int)a[i - 1][j].g * input.kernel[3] + (int)a[i][j].g * input.kernel[4] + (int)a[i + 1][j].g * input.kernel[5] + (int)a[i - 1][j + 1].g * input.kernel[6] + (int)a[i][j + 1].g * input.kernel[7] + (int)a[i + 1][j + 1].g * input.kernel[8]);
			
			//a1[i][j].b = (1/9.0)*((int)a[i-1][j-1].b*input.kernel[0]+(int)a[i][j-1].b* input.kernel[1] + (int)a[i + 1][j - 1].b * input.kernel[2] + (int)a[i - 1][j].b * input.kernel[3] + (int)a[i][j].b * input.kernel[4] + (int)a[i + 1][j].b * input.kernel[5] + (int)a[i - 1][j + 1].b * input.kernel[6] + (int)a[i][j + 1].b * input.kernel[7] + (int)a[i + 1][j + 1].b * input.kernel[8]);
			
			a1[i][j].g = a[i][j].g;
			a1[i][j].b = a[i][j].b;
		}
		for (k = 0; k < input.width; k++)
		{
			// To write left and write border
			if (k == 0 || k == input.width - 1)
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

			input.outChannelR[outDataIterator] = pixel.r;
			input.outChannelG[outDataIterator] = pixel.g;
			input.outChannelB[outDataIterator] = pixel.b;
			++outDataIterator;
		}
	}

	// To write bottom border
	for (k = 0; k < input.width; k++)
	{
		pixel.r = a[input.height - 1][k].r;
		pixel.g = a[input.height - 1][k].g;
		pixel.b = a[input.height - 1][k].b;

		input.outChannelR[outDataIterator] = pixel.r;
		input.outChannelG[outDataIterator] = pixel.g;
		input.outChannelB[outDataIterator] = pixel.b;
		++outDataIterator;
	}


	for (int i = 0; i < input.height; i++)
	{
		delete[] a[i];
		delete[] a1[i];
	}
	delete[] a;
	delete[] a1;
}

