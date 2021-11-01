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
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenInputFileDialog_Click(object sender, EventArgs e)
        {
            fillTextboxWithUserInput(this.txtInputFile);
        }

        private void btnOpenOutputFileDialog_Click(object sender, EventArgs e)
        {
            fillTextboxWithUserInput(this.txtOutputFile);
        }

        private void fillTextboxWithUserInput(TextBox txtBox)
        {
            this.fileDialog.Title = "Please select the file";
            this.fileDialog.Multiselect = false;
            this.fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (this.fileDialog.ShowDialog() == DialogResult.OK)
                txtBox.Text = this.fileDialog.SafeFileName;
        }

        private void btnUnsharpMaskingCs_Click(object sender, EventArgs e)
        {
            USM.UnsharpMaskingCs();
        }

        private void btnUnsharpMaskingCpp_Click(object sender, EventArgs e)
        {
            USM.UnsharpMaskingCpp();
        }

        private void btnUnsharpMaskingAsm_Click(object sender, EventArgs e)
        {
            USM.UnsharpMaskingAsm();
        }
    }
}
