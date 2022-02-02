using APL_Final_Project.TestWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APL_Final_Project
{
    public partial class MainWindow : Form
    {
        private static int[] kernel = new int[]
        {
            0, -1, 0,
            -1, 5, -1,
            0, -1, 0
        };

        public MainWindow()
        {
            InitializeComponent();

            lbBestTimeAsm.Text = "-";
            lbBestTimeCpp.Text = "-";

            cbAsmLiveReload.Checked = false;
            cbCppLiveReload.Checked = false;
            cbCppV2LiveReload.Checked = false;
            cbSamleLiveReload.Checked = false;

            numKernel1.Value = kernel[0];
            numKernel2.Value = kernel[1];
            numKernel3.Value = kernel[2];
            numKernel4.Value = kernel[3];
            numKernel5.Value = kernel[4];
            numKernel6.Value = kernel[5];
            numKernel7.Value = kernel[6];
            numKernel8.Value = kernel[7];
            numKernel9.Value = kernel[8];

            cbAsmLiveReload.Checked = false;
            cbCppLiveReload.Checked = true;
            cbCppV2LiveReload.Checked = false;
            cbSamleLiveReload.Checked = true;

            txtInputFile.Text = "Test2.bmp";

            btnKernel1.Text  = " 0  0  0" + Environment.NewLine;
            btnKernel1.Text += " 0  1  0" + Environment.NewLine;
            btnKernel1.Text += " 0  0  0" + Environment.NewLine;

            btnKernel2.Text  = " 0 -1  0" + Environment.NewLine;
            btnKernel2.Text += "-1  5 -1" + Environment.NewLine;
            btnKernel2.Text += " 0 -1  0" + Environment.NewLine;

            btnKernel3.Text  = " 0  1  0" + Environment.NewLine;
            btnKernel3.Text += " 1  5  1" + Environment.NewLine;
            btnKernel3.Text += " 0  1  0" + Environment.NewLine;

            btnKernel4.Text  = "-2 -1  0" + Environment.NewLine;
            btnKernel4.Text += "-1  5  1" + Environment.NewLine;
            btnKernel4.Text += " 0  1  2" + Environment.NewLine;
        }

        private void btnOpenInputFileDialog_Click(object sender, EventArgs e)
        {
            fillTextboxWithUserInput(this.txtInputFile);
        }

        private void fillTextboxWithUserInput(TextBox txtBox)
        {
            this.fileDialog.Title = "Please select the file";
            this.fileDialog.Multiselect = false;
            this.fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (this.fileDialog.ShowDialog() == DialogResult.OK)
                txtBox.Text = this.fileDialog.SafeFileName;
        }

        private async void btnUnsharpMaskingCpp_Click(object sender, EventArgs e)
        {
            lbBestTimeCpp.Text = "Executing...";

            var img = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), txtInputFile.Text));
            var result = await USM.UnsharpMaskingCpp(new Bitmap(img), kernel);
            result.Image.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Output-cpp.bmp"));
            lbBestTimeCpp.Text = result.ExecutionTimeString;


            picCpp.Image = ResizeImage((Bitmap)result.Image, picCpp.Width, picCpp.Height);
        }

        private async void btnUnsharpMaskingAsm_Click(object sender, EventArgs e)
        {
            lbBestTimeAsm.Text = "Executing...";

            var img = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), txtInputFile.Text));
            var result = await USM.UnsharpMaskingAsm(new Bitmap(img), kernel);
            result.Image.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Output-asm.bmp"));
            lbBestTimeAsm.Text = result.ExecutionTimeString;

            picAsm.Image = ResizeImage((Bitmap)result.Image, picAsm.Width, picAsm.Height);
        }

        private void liveReload(object sender, EventArgs e)
        {
            if (cbCppLiveReload.Checked)
                btnUnsharpMaskingCpp_Click(sender, e);

            if (cbCppV2LiveReload.Checked)
                btnUnsharpMaskingCppV2_Click(sender, e);

            if (cbAsmLiveReload.Checked)
                btnUnsharpMaskingAsm_Click(sender, e);
        }

        private Bitmap ResizeImage(Bitmap inImage, int newWidth, int newHeight)
        {
            Size sz = inImage.Size;
            Bitmap zoomed = new Bitmap(newWidth, newHeight);
            if (zoomed != null) zoomed.Dispose();

            float zoom = (float)(Math.Min(newWidth / inImage.Width, newHeight / inImage.Height) / 4f + 1);
            zoomed = new Bitmap((int)(sz.Width * zoom), (int)(sz.Height * zoom));

            using (Graphics g = Graphics.FromImage(zoomed))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                g.DrawImage(inImage, new Rectangle(Point.Empty, zoomed.Size));
            }

            return zoomed;
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInputFile.Text))
            {
                MessageBox.Show("Incorrect input file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var img = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), txtInputFile.Text));
           
            picSample.Image = ResizeImage((Bitmap)img, picSample.Width, picSample.Height);

            liveReload(sender, e);
        }

        private void numKernel_ValueChanged(object sender, EventArgs e)
        {
            var numericControl = sender as NumericUpDown;

            if (numericControl == null)
                return;

            if (numericControl == numKernel1)
                kernel[0] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel2)
                kernel[1] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel3)
                kernel[2] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel4)
                kernel[3] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel5)
                kernel[4] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel6)
                kernel[5] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel7)
                kernel[6] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel8)
                kernel[7] = Decimal.ToInt32(numericControl.Value);
            else if (numericControl == numKernel9)
                kernel[8] = Decimal.ToInt32(numericControl.Value);
            else
                return;

            liveReload(sender, e);
        }

        private void txtInputFile_TextChanged(object sender, EventArgs e)
        {
            if(cbSamleLiveReload.Checked)
                btnLoadImage_Click(sender, e);
        }

        private async void btnUnsharpMaskingCppV2_Click(object sender, EventArgs e)
        {
            lbBestTimeCppV2.Text = "Executing...";

            var img = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), txtInputFile.Text));
            var result = await USM.UnsharpMaskingCppV2(new Bitmap(img), kernel);
            lbBestTimeCppV2.Text = result.ExecutionTimeString;
            picCppV2.Image = ResizeImage((Bitmap)result.Image, picCppV2.Width, picCppV2.Height);
        }

        private void btnPerformance_Click(object sender, EventArgs e)
        {
            using (PerformanceTestWindow dialog = new PerformanceTestWindow())
                dialog.ShowDialog(this);
        }

        private void btnKernel1_Click(object sender, EventArgs e)
        {

            numKernel1.Value = 0;
            numKernel2.Value = 0;
            numKernel3.Value = 0;
            numKernel4.Value = 0;
            numKernel5.Value = 1;
            numKernel6.Value = 0;
            numKernel7.Value = 0;
            numKernel8.Value = 0;
            numKernel9.Value = 0;
        }

        private void btnKernel2_Click(object sender, EventArgs e)
        {
            numKernel1.Value = 0;
            numKernel2.Value = -1;
            numKernel3.Value = 0;
            numKernel4.Value = -1;
            numKernel5.Value = 5;
            numKernel6.Value = -1;
            numKernel7.Value = 0;
            numKernel8.Value = -1;
            numKernel9.Value = 0;
        }

        private void btnKernel3_Click(object sender, EventArgs e)
        {
            numKernel1.Value = 0;
            numKernel2.Value = 1;
            numKernel3.Value = 0;
            numKernel4.Value = 1;
            numKernel5.Value = 5;
            numKernel6.Value = 1;
            numKernel7.Value = 0;
            numKernel8.Value = 1;
            numKernel9.Value = 0;
        }

        private void btnKernel4_Click(object sender, EventArgs e)
        {
            numKernel1.Value = -2;
            numKernel2.Value = -1;
            numKernel3.Value = 0;
            numKernel4.Value = -1;
            numKernel5.Value = 5;
            numKernel6.Value = 1;
            numKernel7.Value = 0;
            numKernel8.Value = 1;
            numKernel9.Value = 2;
        }
    }
}
