using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using static System.Windows.Forms.ListBox;

namespace APL_Final_Project.TestWindow
{
    public partial class PerformanceTestWindow : Form
    {
        private IDictionary<int, ICollection<double>> measurementsAsm;
        private IDictionary<int, ICollection<double>> measurementsCpp;
        private IDictionary<int, ICollection<double>> measurementsCppOptimized;

        public PerformanceTestWindow(ObjectCollection lstFiles)
        {
            InitializeComponent();

            measurementsAsm = new Dictionary<int, ICollection<double>>();
            measurementsCpp = new Dictionary<int, ICollection<double>>();
            measurementsCppOptimized = new Dictionary<int, ICollection<double>>();

            lstTestFiles.Items.Clear();
            lstTestFiles.Items.AddRange(lstFiles);
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
            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();
            this.chartData.Titles.Clear();
            this.chartData.Titles.Add("Performance test on " + (string)cpu["Name"]);

            decimal[] kernel = { 
                0, 0, 0, 
                0, 1, 0, 
                0, 0, 0 
            };


            this.lstData.Items.Clear();
            foreach (var testFile in lstTestFiles.Items)
            {
                using (var image = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), testFile.ToString())))
                {
                    var resultAsm = await USM.UnsharpMaskingAsm(new Bitmap(image), kernel);
                    var resultCpp = await USM.UnsharpMaskingCpp(new Bitmap(image), kernel);
                    //var resultCppOptimized = await USM.UnsharpMaskingCppV2(new Bitmap(image), kernel);

                    if (measurementsAsm.ContainsKey(image.Width * image.Height))
                        measurementsAsm[image.Width * image.Height].Add(resultAsm.ExecutionTime.TotalMilliseconds);
                    else
                        measurementsAsm.Add(image.Width * image.Height, new List<double>() { resultAsm.ExecutionTime.TotalMilliseconds });

                    if (measurementsCpp.ContainsKey(image.Width * image.Height))
                        measurementsCpp[image.Width * image.Height].Add(resultCpp.ExecutionTime.TotalMilliseconds);
                    else
                        measurementsCpp.Add(image.Width * image.Height, new List<double>() { resultCpp.ExecutionTime.TotalMilliseconds });

                    //if (measurementsCppOptimized.ContainsKey(image.Width * image.Height))
                    //    measurementsCppOptimized[image.Width * image.Height].Add(resultCppOptimized.ExecutionTime.TotalMilliseconds);
                    //else
                    //    measurementsCppOptimized.Add(image.Width * image.Height, new List<double>() { resultCppOptimized.ExecutionTime.TotalMilliseconds });

                    //this.lstData.Items.Add($"Pixels: {image.Width}x{image.Height}={image.Width * image.Height}, Asm: {resultAsm.ExecutionTimeString}, Cpp: {resultCpp.ExecutionTimeString}, Cpp optimized: {resultCppOptimized.ExecutionTimeString}");
                }
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

            Series seriesCppOptimized = this.chartData.Series.Add("C++ Optimized");
            seriesCppOptimized.ChartType = SeriesChartType.Spline;
            foreach (var measurementSet in measurementsCppOptimized.OrderBy(x => x.Key))
                seriesCppOptimized.Points.AddXY($"{measurementSet.Key}", measurementSet.Value.Average());
        }
    }
}
