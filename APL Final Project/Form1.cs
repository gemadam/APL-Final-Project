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

namespace APL_Final_Project
{
    public partial class Form1 : Form
    {
        private static int[] kernel = new int[]
        {
            0, 0, 0,
            0, 1, 0,
            0, 0, 0
        };

        public Form1()
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
            cbCppV2LiveReload.Checked = true;
            cbSamleLiveReload.Checked = true;

            txtInputFile.Text = "audi.png";
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

            var result = await USM.UnsharpMaskingCpp(new Bitmap(picSample.Image), kernel);
            lbBestTimeCpp.Text = result.ExecutionTimeString;
            picCpp.Image = result.Image;
        }

        private async void btnUnsharpMaskingAsm_Click(object sender, EventArgs e)
        {
            lbBestTimeAsm.Text = "Executing...";

            var result = await USM.UnsharpMaskingAsm(new Bitmap(picSample.Image), kernel);
            lbBestTimeAsm.Text = result.ExecutionTimeString;
            picAsm.Image = result.Image;
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

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInputFile.Text))
            {
                MessageBox.Show("Incorrect input file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            picSample.Image = Image.FromFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), txtInputFile.Text));

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

            var result = await USM.UnsharpMaskingCppV2(new Bitmap(picSample.Image), kernel);
            lbBestTimeCppV2.Text = result.ExecutionTimeString;
            picCppV2.Image = result.Image;
        }
    }
}
