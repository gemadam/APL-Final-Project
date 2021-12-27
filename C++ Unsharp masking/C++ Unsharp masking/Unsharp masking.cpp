#include<iostream>
#include<fstream>
#include<conio.h>
#include<string.h>
using namespace std;
struct pix
{
	unsigned char b, g, r;
}pixel;
char Header[54];
ifstream in;
ofstream out, out1;
int main()
{
	char infile[] = "c:\\bf.bmp";
	char outfile[] = "c:\\bf - filtered - highboost.bmp";
	char imdata[] = "c:\\imdata.dat";
	int i, j, k, count = 0;
	in.open(infile, ios::in | ios::binary);
	in.read((char*)(&Header), sizeof(Header));
	int height = *(int*)&Header[18];
	int width = *(int*)&Header[22];
	float filter[9] = { -1,-1,-1,-1,8.9,-1,-1,-1,-1 }; //highboost mask
	/*struct pix a[height][width];
	struct pix a1 [height][width];*/
	struct pix a[18][22];
	struct pix a1[18][22];
	cout << "Height = " << height << " Width = " << width;
	for (i = 0; i < height; i++)
		for (j = 0; j < width; j++) {
			a[i][j].r = 0; a1[i][j].r = 0;
			a[i][j].g = 0; a1[i][j].g = 0;
			a[i][j].b = 0; a1[i][j].b = 0;
		}
	out1.open(imdata, ios::out);
	i = 0; j = 0;
	while (!in.eof())
	{
		in.read((char*)(&pixel), sizeof(pixel));
		/* out1 << “ORIGINAL : ” << (int)pixel.r << ”, “ << (int)pixel.g << ”,
		“ << (int)pixel.b << endl;
		cout<<“ORIGINAL : ” <<(int)pixel.r<<” , “<<(int)pixel.g<<” ,
		“ << (int)pixel.b << endl;*/
		if (j == width) {
			j = 0;
			i++;
		}
		a[i][j].r = pixel.r;
		a[i][j].g = pixel.g;
		a[i][j].b = pixel.b;
		/* cout << “PIXEL : “ << (int)a[i][j].r << ”, “ << (int)a[i][j].g << ”, “ << (int)a[i][j].b << endl;*/
		out1 << "PIXEL " << count++ << " : " << (int)a[i][j].r << ", " << (int)a[i][j].g << ", " << (int)a[i][j].b << endl;
		j++;
	}
	out.open(outfile, ios::out | ios::binary);
	out.write((char*)(&Header), sizeof(Header));
	count = 0;
	// to write first line of original image to modified image file (border1)
	for (k = 0; k < width; k++) {
		pixel.r = a[0][k].r;
		pixel.g = a[0][k].g;
		pixel.b = a[0][k].b;
		out1 << "PIXEL WRITTEN " << count++ << " : " << (int)pixel.r << ", " << (int)pixel.g << ", " << (int)pixel.b << endl;
		out.write((char*)(&pixel), sizeof(pixel));
	}
	for (i = 1; i < height - 1; i++) {
		for (j = 1; j < (width - 1); j++) {
				//cout<< (int)a[i-1][j-1].r<<” , ” << filter[0]<<endl;
				a1[i][j].r = (1 / 9.0) * ((int)a[i - 1][j - 1].r * filter[0] + (int)a[i][j -
				1].r * filter[1] + (int)a[i + 1][j - 1].r * filter[2] + (int)a[i - 1][j].r * filter[3] + (int)a[i][j].r * filter[4] +
				(int)a[i + 1][j].r * filter[5] + (int)a[i - 1][j + 1].r * filter[6] + (int)a[i][j + 1].r * filter[7] +
				(int)a[i + 1][j + 1].r * filter[8]);
				//a1[i][j].g = (1/9.0)*((int)a[i-1][j-1].g*filter[0]+(int)a[i][j-1].g* filter[1] + (int)a[i + 1][j - 1].g * filter[2] + (int)a[i - 1][j].g * filter[3] + (int)a[i][j].g * filter[4] + (int)a[i + 1][j].g * filter[5] + (int)a[i - 1][j + 1].g * filter[6] + (int)a[i][j + 1].g * filter[7] + (int)a[i + 1][j + 1].g * filter[8]);
				//a1[i][j].b = (1/9.0)*((int)a[i-1][j-1].b*filter[0]+(int)a[i][j-1].b* filter[1] + (int)a[i + 1][j - 1].b * filter[2] + (int)a[i - 1][j].b * filter[3] + (int)a[i][j].b * filter[4] + (int)a[i + 1][j].b * filter[5] + (int)a[i - 1][j + 1].b * filter[6] + (int)a[i][j + 1].b * filter[7] + (int)a[i + 1][j + 1].b * filter[8]);
				a1[i][j].g = a[i][j].g;
				a1[i][j].b = a[i][j].b;
				out1 << "PIXEL MODIFIED " << " : " << (int)a1[i][j].r << ", " << (int)a1[i][j].g << "," << (int)a1[i][j].b << endl;
		}
		for (k = 0; k < width; k++)
		{
			// To write left and write border
			if (k == 0 || k == width - 1) {
				pixel.r = a[i][k].r;
				pixel.g = a[i][k].g;
				pixel.b = a[i][k].b;
			}
			else //to write middle modified contents 
			{
			pixel.r = a1[i][k].r;
			pixel.g = a1[i][k].g;
			pixel.b = a1[i][k].b;
			}
		out1 << "PIXEL WRITTEN " << count++ << " : " << (int)pixel.r << ", " << (int)pixel.g << " , " << (int)pixel.b << endl;
		out.write((char*)(&pixel), sizeof(pixel));
	}
}
// to write bottom border
	for (k = 0; k < width; k++) 
	{
		pixel.r = a[height - 1][k].r;
		pixel.g = a[height - 1][k].g;
		pixel.b = a[height - 1][k].b;
		out1 << "PIXEL WRITTEN " << count++ << " : " << (int)pixel.r << ", " << (int)pixel.g << " , " << (int)pixel.b << endl;
		out.write((char*)(&pixel), sizeof(pixel));
	}
	in.close();
	out.close();
	out1.close();
	getchar();
}