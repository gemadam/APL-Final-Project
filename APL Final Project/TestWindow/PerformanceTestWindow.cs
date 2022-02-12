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
using System.Collections.Concurrent;

namespace APL_Final_Project.TestWindow
{
    public partial class PerformanceTestWindow : Form
    {
        private IDictionary<int, ConcurrentBag<double>> measurementsAsm;
        private IDictionary<int, ConcurrentBag<double>> measurementsCpp;
        private IDictionary<int, ConcurrentBag<double>> measurementsCppOptimized;

        public PerformanceTestWindow(ObjectCollection lstFiles)
        {
            InitializeComponent();

            measurementsAsm = new ConcurrentDictionary<int, ConcurrentBag<double>>();
            measurementsCpp = new ConcurrentDictionary<int, ConcurrentBag<double>>();
            measurementsCppOptimized = new ConcurrentDictionary<int, ConcurrentBag<double>>();

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

        private void btnExecuteTests_Click(object sender, EventArgs e)
        {
            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();
            this.chartData.Titles.Clear();
            this.chartData.Titles.Add("Performance test on " + (string)cpu["Name"]);

            txtCsvData.Text = "Performance test on " + (string)cpu["Name"] + Environment.NewLine;

            decimal[] kernel = { 
                0, 0, 0, 
                0, 1, 0, 
                0, 0, 0 
            };


            foreach (var testFile in lstTestFiles.Items)
            {
                using (var image = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), testFile.ToString())))
                {
                    USMResult? asm = null;
                    USMResult? cpp = null;

                    var taskAsm = USM.UnsharpMaskingAsm(new Bitmap(image), kernel).ContinueWith(x =>
                    {
                        asm = x.Result;

                        if (measurementsAsm.ContainsKey(image.Width * image.Height))
                            measurementsAsm[image.Width * image.Height].Add(x.Result.ExecutionTime.TotalMilliseconds);
                        else
                            measurementsAsm.Add(image.Width * image.Height, new ConcurrentBag<double>() { x.Result.ExecutionTime.TotalMilliseconds });
                    });

                    var taskCpp = USM.UnsharpMaskingCpp(new Bitmap(image), kernel).ContinueWith(x =>
                    {
                        cpp = x.Result;

                        if (measurementsCpp.ContainsKey(image.Width * image.Height))
                            measurementsCpp[image.Width * image.Height].Add(x.Result.ExecutionTime.TotalMilliseconds);
                        else
                            measurementsCpp.Add(image.Width * image.Height, new ConcurrentBag<double>() { x.Result.ExecutionTime.TotalMilliseconds });
                    });

                    var taskCppOp = USM.UnsharpMaskingCppV2(new Bitmap(image), kernel).ContinueWith(x =>
                    {
                        cpp = x.Result;

                        if (measurementsCppOptimized.ContainsKey(image.Width * image.Height))
                            measurementsCppOptimized[image.Width * image.Height].Add(x.Result.ExecutionTime.TotalMilliseconds);
                        else
                            measurementsCppOptimized.Add(image.Width * image.Height, new ConcurrentBag<double>() { x.Result.ExecutionTime.TotalMilliseconds });
                    });


                    taskAsm.Wait();
                    taskCpp.Wait();
                    taskCppOp.Wait();
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


            txtCsvData.Text += "NumberOfPixels;Asm;Cpp;CppOptimized" + Environment.NewLine;
            foreach (var measurementSet in measurementsAsm.OrderBy(x => x.Key))
            {
                var asm = measurementSet.Value;
                var cpp = measurementsCpp[measurementSet.Key];
                var cppV2 = measurementsCppOptimized[measurementSet.Key];


                if(asm != null && cpp != null && cppV2 != null)
                {
                    txtCsvData.Text += $"{measurementSet.Key};{asm.Average()};{cpp.Average()};{cppV2.Average()}" + Environment.NewLine;
                }
            }

        }
    }
}
