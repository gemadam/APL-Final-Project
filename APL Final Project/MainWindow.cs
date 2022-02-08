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

            cbAsmLiveReload.Checked = true;
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

        private Task<Bitmap> makeDiffAsync(Bitmap bmp1, Bitmap bmp2)
        {
            return Task<Bitmap>.Run(() =>
            {
                if (bmp1 == null || bmp2 == null)
                    return null;
                else if (bmp1.Height != bmp2.Height || bmp1.Width != bmp2.Width)
                    return null;

                Bitmap bitmap = new Bitmap(bmp1.Width, bmp1.Height);


                for (var y = 0; y < bmp1.Height; y++)
                    for (var x = 0; x < bmp1.Width; x++)
                    {
                        var p1 = bmp1.GetPixel(x, y);
                        var p2 = bmp2.GetPixel(x, y);

                        bitmap.SetPixel(x, y, Color.FromArgb(Math.Abs(p1.R - p2.R), Math.Abs(p1.G - p2.G), Math.Abs(p1.B - p2.B)));
                    }

                return bitmap;
            });
        }

        private void fillTextboxWithUserInput(TextBox txtBox)
        {
            this.fileDialog.Title = "Please select the file";
            this.fileDialog.Multiselect = false;
            this.fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (this.fileDialog.ShowDialog() == DialogResult.OK)
                txtBox.Text = this.fileDialog.SafeFileName;
        }

        private async Task btnUSMClick(Button btnUSM, Label lbResult, PictureBox picBox, CheckBox cbReload, Func<Bitmap, int[], Task<USMResult>> fUSM)
        {
            btnUSM.Enabled = false;
            cbReload.Enabled = false;
            var cbState = cbReload.Checked;
            cbReload.Checked = false;

            lbResult.Text = "Executing...";

            var img = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), txtInputFile.Text));
            var result = await fUSM.Invoke(new Bitmap(img), kernel);

            picBox.Image = ResizeImage((Bitmap)result.Image, picBox.Width, picBox.Height);
            lbResult.Text = result.ExecutionTimeString;

            cbReload.Checked = cbState;
            cbReload.Enabled = true;
            btnUSM.Enabled = true;
        }

        private async void btnUnsharpMaskingCpp_Click(object sender, EventArgs e)
        {
            await btnUSMClick(btnUnsharpMaskingCpp, lbBestTimeCpp, picCpp, cbCppLiveReload, USM.UnsharpMaskingCpp);
        }

        private async void btnUnsharpMaskingAsm_Click(object sender, EventArgs e)
        {
            await btnUSMClick(btnUnsharpMaskingAsm, lbBestTimeAsm, picAsm, cbAsmLiveReload, USM.UnsharpMaskingAsm);
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

        private void setKernel(decimal[] kernel)
        {
            var cbAsmLiveReloadChecked = cbAsmLiveReload.Checked;
            var cbCppLiveReloadChecked = cbCppLiveReload.Checked;
            var cbCppV2LiveReloadChecked = cbCppV2LiveReload.Checked;
            var cbSamleLiveReloadChecked = cbSamleLiveReload.Checked;

            cbAsmLiveReload.Checked = false;
            cbCppLiveReload.Checked = false;
            cbCppV2LiveReload.Checked = false;
            cbSamleLiveReload.Checked = false;

            var i = 0;
            numKernel1.Value = kernel[i++];
            numKernel2.Value = kernel[i++];
            numKernel3.Value = kernel[i++];
            numKernel4.Value = kernel[i++];
            numKernel5.Value = kernel[i++];
            numKernel6.Value = kernel[i++];
            numKernel7.Value = kernel[i++];
            numKernel8.Value = kernel[i++];
            numKernel9.Value = kernel[i++];

            cbAsmLiveReload.Checked = cbAsmLiveReloadChecked;
            cbCppLiveReload.Checked = cbCppLiveReloadChecked;
            cbCppV2LiveReload.Checked = cbCppV2LiveReloadChecked;
            cbSamleLiveReload.Checked = cbSamleLiveReloadChecked;
        }

        private void btnKernel_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            this.setKernel(btn.Text
                .Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToDecimal(x))
                .ToArray()
            );

            liveReload(sender, e);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var diff = await makeDiffAsync((Bitmap)picCpp.Image, (Bitmap)picAsm.Image);
            if (diff != null)
                picDiff.Image = ResizeImage(diff, picCpp.Image.Width, picCpp.Image.Height);
        }
    }
}
