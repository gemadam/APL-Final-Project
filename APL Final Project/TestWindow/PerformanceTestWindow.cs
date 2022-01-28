using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace APL_Final_Project.TestWindow
{
    public partial class PerformanceTestWindow : Form
    {
        private IDictionary<int, ICollection<double>> measurementsAsm;
        private IDictionary<int, ICollection<double>> measurementsCpp;

        public PerformanceTestWindow()
        {
            InitializeComponent();

            measurementsAsm = new Dictionary<int, ICollection<double>>();
            measurementsCpp = new Dictionary<int, ICollection<double>>();

            lstTestFiles.Items.Clear();
            lstTestFiles.Items.Add("audi.png");
            lstTestFiles.Items.Add("2021-10-22-07PM-59-16_cb8df913-aa0a-4bcd-999c-24b0259ef41b.jpg");
            lstTestFiles.Items.Add("istockphoto-1289383957-170667a.jpg");
            lstTestFiles.Items.Add("pexels-valdemaras-d-1647962.bmp");
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Please select the file";
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
                this.lstTestFiles.Items.Add(this.openFileDialog1.SafeFileName);
        }

        private async void btnExecuteTests_Click(object sender, EventArgs e)
        {
            int[] kernel = { 
                0, 0, 0, 
                0, 0, 1, 
                0, 0, 0 
            };


            this.lstData.Items.Clear();
            foreach (var testFile in lstTestFiles.Items)
            {
                var image = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), testFile.ToString()));

                var resultAsm = await USM.UnsharpMaskingAsm(new Bitmap(image), kernel);
                var resultCpp = await USM.UnsharpMaskingCpp(new Bitmap(image), kernel);

                if (measurementsAsm.ContainsKey(image.Width * image.Height))
                    measurementsAsm[image.Width * image.Height].Add(resultAsm.ExecutionTime.TotalMilliseconds);
                else
                    measurementsAsm.Add(image.Width * image.Height, new List<double>() { resultAsm.ExecutionTime.TotalMilliseconds });

                if (measurementsCpp.ContainsKey(image.Width * image.Height))
                    measurementsCpp[image.Width * image.Height].Add(resultAsm.ExecutionTime.TotalMilliseconds);
                else
                    measurementsCpp.Add(image.Width * image.Height, new List<double>() { resultCpp.ExecutionTime.TotalMilliseconds });

                this.lstData.Items.Add($"Pixels: {image.Width}x{image.Height}={image.Width*image.Height}, Asm: {resultAsm.ExecutionTimeString}, Cpp: {resultCpp.ExecutionTimeString}");
            }


            this.chartData.Series.Clear();
            Series seriesAsm = this.chartData.Series.Add("Assembly");
            seriesAsm.ChartType = SeriesChartType.Spline;
            foreach(var measurementSet in measurementsAsm.OrderBy(x => x.Key))
                seriesAsm.Points.AddXY($"{measurementSet.Key}", measurementSet.Value.Average());

            Series seriesCpp = this.chartData.Series.Add("C++");
            seriesCpp.ChartType = SeriesChartType.Spline;
            foreach (var measurementSet in measurementsCpp.OrderBy(x => x.Key))
                seriesCpp.Points.AddXY($"{measurementSet.Key}", measurementSet.Value.Average());
        }
    }
}
