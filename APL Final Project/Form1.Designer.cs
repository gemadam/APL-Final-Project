
namespace APL_Final_Project
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUnsharpMaskingCs = new System.Windows.Forms.Button();
            this.lbInputFile = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.btnOpenInputFileDialog = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnUnsharpMaskingCpp = new System.Windows.Forms.Button();
            this.btnUnsharpMaskingAsm = new System.Windows.Forms.Button();
            this.lbBestTimeCs = new System.Windows.Forms.Label();
            this.lbBestTimeCpp = new System.Windows.Forms.Label();
            this.lbBestTimeAsm = new System.Windows.Forms.Label();
            this.picSample = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picCs = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.picCpp = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picAsm = new System.Windows.Forms.PictureBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.numKernel1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCsLiveReload = new System.Windows.Forms.CheckBox();
            this.cbAsmLiveReload = new System.Windows.Forms.CheckBox();
            this.cbCppLiveReload = new System.Windows.Forms.CheckBox();
            this.numKernel2 = new System.Windows.Forms.NumericUpDown();
            this.numKernel3 = new System.Windows.Forms.NumericUpDown();
            this.numKernel6 = new System.Windows.Forms.NumericUpDown();
            this.numKernel5 = new System.Windows.Forms.NumericUpDown();
            this.numKernel4 = new System.Windows.Forms.NumericUpDown();
            this.numKernel9 = new System.Windows.Forms.NumericUpDown();
            this.numKernel8 = new System.Windows.Forms.NumericUpDown();
            this.numKernel7 = new System.Windows.Forms.NumericUpDown();
            this.cbSamleLiveReload = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCpp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAsm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel7)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUnsharpMaskingCs
            // 
            this.btnUnsharpMaskingCs.Location = new System.Drawing.Point(817, 329);
            this.btnUnsharpMaskingCs.Name = "btnUnsharpMaskingCs";
            this.btnUnsharpMaskingCs.Size = new System.Drawing.Size(311, 23);
            this.btnUnsharpMaskingCs.TabIndex = 0;
            this.btnUnsharpMaskingCs.Text = "Unsharp Masking C#";
            this.btnUnsharpMaskingCs.UseVisualStyleBackColor = true;
            this.btnUnsharpMaskingCs.Click += new System.EventHandler(this.btnUnsharpMaskingCs_Click);
            // 
            // lbInputFile
            // 
            this.lbInputFile.AutoSize = true;
            this.lbInputFile.Location = new System.Drawing.Point(12, 9);
            this.lbInputFile.Name = "lbInputFile";
            this.lbInputFile.Size = new System.Drawing.Size(47, 13);
            this.lbInputFile.TabIndex = 1;
            this.lbInputFile.Text = "Input file";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(12, 25);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(360, 20);
            this.txtInputFile.TabIndex = 3;
            this.txtInputFile.TextChanged += new System.EventHandler(this.txtInputFile_TextChanged);
            // 
            // btnOpenInputFileDialog
            // 
            this.btnOpenInputFileDialog.Location = new System.Drawing.Point(378, 22);
            this.btnOpenInputFileDialog.Name = "btnOpenInputFileDialog";
            this.btnOpenInputFileDialog.Size = new System.Drawing.Size(32, 23);
            this.btnOpenInputFileDialog.TabIndex = 5;
            this.btnOpenInputFileDialog.Text = "...";
            this.btnOpenInputFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenInputFileDialog.Click += new System.EventHandler(this.btnOpenInputFileDialog_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "fileDialog";
            // 
            // btnUnsharpMaskingCpp
            // 
            this.btnUnsharpMaskingCpp.Location = new System.Drawing.Point(416, 680);
            this.btnUnsharpMaskingCpp.Name = "btnUnsharpMaskingCpp";
            this.btnUnsharpMaskingCpp.Size = new System.Drawing.Size(311, 23);
            this.btnUnsharpMaskingCpp.TabIndex = 7;
            this.btnUnsharpMaskingCpp.Text = "UnsharpMaskingCpp";
            this.btnUnsharpMaskingCpp.UseVisualStyleBackColor = true;
            this.btnUnsharpMaskingCpp.Click += new System.EventHandler(this.btnUnsharpMaskingCpp_Click);
            // 
            // btnUnsharpMaskingAsm
            // 
            this.btnUnsharpMaskingAsm.Location = new System.Drawing.Point(817, 680);
            this.btnUnsharpMaskingAsm.Name = "btnUnsharpMaskingAsm";
            this.btnUnsharpMaskingAsm.Size = new System.Drawing.Size(311, 23);
            this.btnUnsharpMaskingAsm.TabIndex = 8;
            this.btnUnsharpMaskingAsm.Text = "Unsharp Masking Asm";
            this.btnUnsharpMaskingAsm.UseVisualStyleBackColor = true;
            this.btnUnsharpMaskingAsm.Click += new System.EventHandler(this.btnUnsharpMaskingAsm_Click);
            // 
            // lbBestTimeCs
            // 
            this.lbBestTimeCs.AutoSize = true;
            this.lbBestTimeCs.Location = new System.Drawing.Point(1141, 9);
            this.lbBestTimeCs.Name = "lbBestTimeCs";
            this.lbBestTimeCs.Size = new System.Drawing.Size(71, 13);
            this.lbBestTimeCs.TabIndex = 9;
            this.lbBestTimeCs.Text = "lbBestTimeCs";
            // 
            // lbBestTimeCpp
            // 
            this.lbBestTimeCpp.AutoSize = true;
            this.lbBestTimeCpp.Location = new System.Drawing.Point(733, 360);
            this.lbBestTimeCpp.Name = "lbBestTimeCpp";
            this.lbBestTimeCpp.Size = new System.Drawing.Size(78, 13);
            this.lbBestTimeCpp.TabIndex = 10;
            this.lbBestTimeCpp.Text = "lbBestTimeCpp";
            // 
            // lbBestTimeAsm
            // 
            this.lbBestTimeAsm.AutoSize = true;
            this.lbBestTimeAsm.Location = new System.Drawing.Point(1133, 360);
            this.lbBestTimeAsm.Name = "lbBestTimeAsm";
            this.lbBestTimeAsm.Size = new System.Drawing.Size(79, 13);
            this.lbBestTimeAsm.TabIndex = 11;
            this.lbBestTimeAsm.Text = "lbBestTimeAsm";
            // 
            // picSample
            // 
            this.picSample.Location = new System.Drawing.Point(416, 25);
            this.picSample.Name = "picSample";
            this.picSample.Size = new System.Drawing.Size(395, 298);
            this.picSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSample.TabIndex = 13;
            this.picSample.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sample";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(817, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "C# Output";
            // 
            // picCs
            // 
            this.picCs.Location = new System.Drawing.Point(817, 25);
            this.picCs.Name = "picCs";
            this.picCs.Size = new System.Drawing.Size(395, 298);
            this.picCs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCs.TabIndex = 15;
            this.picCs.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "C++ Output";
            // 
            // picCpp
            // 
            this.picCpp.Location = new System.Drawing.Point(416, 376);
            this.picCpp.Name = "picCpp";
            this.picCpp.Size = new System.Drawing.Size(395, 298);
            this.picCpp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCpp.TabIndex = 17;
            this.picCpp.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(817, 360);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Asm Output";
            // 
            // picAsm
            // 
            this.picAsm.Location = new System.Drawing.Point(817, 376);
            this.picAsm.Name = "picAsm";
            this.picAsm.Size = new System.Drawing.Size(395, 298);
            this.picAsm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAsm.TabIndex = 19;
            this.picAsm.TabStop = false;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(416, 329);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(311, 23);
            this.btnLoadImage.TabIndex = 21;
            this.btnLoadImage.Text = "Load";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // numKernel1
            // 
            this.numKernel1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel1.Location = new System.Drawing.Point(12, 75);
            this.numKernel1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel1.Name = "numKernel1";
            this.numKernel1.Size = new System.Drawing.Size(73, 20);
            this.numKernel1.TabIndex = 22;
            this.numKernel1.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Kernel";
            // 
            // cbCsLiveReload
            // 
            this.cbCsLiveReload.AutoSize = true;
            this.cbCsLiveReload.Checked = true;
            this.cbCsLiveReload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCsLiveReload.Location = new System.Drawing.Point(1134, 333);
            this.cbCsLiveReload.Name = "cbCsLiveReload";
            this.cbCsLiveReload.Size = new System.Drawing.Size(78, 17);
            this.cbCsLiveReload.TabIndex = 32;
            this.cbCsLiveReload.Text = "Live reload";
            this.cbCsLiveReload.UseVisualStyleBackColor = true;
            // 
            // cbAsmLiveReload
            // 
            this.cbAsmLiveReload.AutoSize = true;
            this.cbAsmLiveReload.Checked = true;
            this.cbAsmLiveReload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAsmLiveReload.Location = new System.Drawing.Point(1134, 684);
            this.cbAsmLiveReload.Name = "cbAsmLiveReload";
            this.cbAsmLiveReload.Size = new System.Drawing.Size(78, 17);
            this.cbAsmLiveReload.TabIndex = 33;
            this.cbAsmLiveReload.Text = "Live reload";
            this.cbAsmLiveReload.UseVisualStyleBackColor = true;
            // 
            // cbCppLiveReload
            // 
            this.cbCppLiveReload.AutoSize = true;
            this.cbCppLiveReload.Checked = true;
            this.cbCppLiveReload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCppLiveReload.Location = new System.Drawing.Point(733, 684);
            this.cbCppLiveReload.Name = "cbCppLiveReload";
            this.cbCppLiveReload.Size = new System.Drawing.Size(78, 17);
            this.cbCppLiveReload.TabIndex = 34;
            this.cbCppLiveReload.Text = "Live reload";
            this.cbCppLiveReload.UseVisualStyleBackColor = true;
            // 
            // numKernel2
            // 
            this.numKernel2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel2.Location = new System.Drawing.Point(91, 75);
            this.numKernel2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel2.Name = "numKernel2";
            this.numKernel2.Size = new System.Drawing.Size(73, 20);
            this.numKernel2.TabIndex = 35;
            this.numKernel2.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // numKernel3
            // 
            this.numKernel3.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel3.Location = new System.Drawing.Point(170, 75);
            this.numKernel3.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel3.Name = "numKernel3";
            this.numKernel3.Size = new System.Drawing.Size(73, 20);
            this.numKernel3.TabIndex = 36;
            this.numKernel3.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // numKernel6
            // 
            this.numKernel6.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel6.Location = new System.Drawing.Point(170, 101);
            this.numKernel6.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel6.Name = "numKernel6";
            this.numKernel6.Size = new System.Drawing.Size(73, 20);
            this.numKernel6.TabIndex = 39;
            this.numKernel6.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // numKernel5
            // 
            this.numKernel5.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel5.Location = new System.Drawing.Point(91, 101);
            this.numKernel5.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel5.Name = "numKernel5";
            this.numKernel5.Size = new System.Drawing.Size(73, 20);
            this.numKernel5.TabIndex = 38;
            this.numKernel5.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // numKernel4
            // 
            this.numKernel4.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel4.Location = new System.Drawing.Point(12, 101);
            this.numKernel4.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel4.Name = "numKernel4";
            this.numKernel4.Size = new System.Drawing.Size(73, 20);
            this.numKernel4.TabIndex = 37;
            this.numKernel4.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // numKernel9
            // 
            this.numKernel9.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel9.Location = new System.Drawing.Point(170, 127);
            this.numKernel9.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel9.Name = "numKernel9";
            this.numKernel9.Size = new System.Drawing.Size(73, 20);
            this.numKernel9.TabIndex = 42;
            this.numKernel9.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // numKernel8
            // 
            this.numKernel8.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel8.Location = new System.Drawing.Point(91, 127);
            this.numKernel8.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel8.Name = "numKernel8";
            this.numKernel8.Size = new System.Drawing.Size(73, 20);
            this.numKernel8.TabIndex = 41;
            this.numKernel8.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // numKernel7
            // 
            this.numKernel7.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numKernel7.Location = new System.Drawing.Point(12, 127);
            this.numKernel7.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numKernel7.Name = "numKernel7";
            this.numKernel7.Size = new System.Drawing.Size(73, 20);
            this.numKernel7.TabIndex = 40;
            this.numKernel7.ValueChanged += new System.EventHandler(this.numKernel_ValueChanged);
            // 
            // cbSamleLiveReload
            // 
            this.cbSamleLiveReload.AutoSize = true;
            this.cbSamleLiveReload.Checked = true;
            this.cbSamleLiveReload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSamleLiveReload.Location = new System.Drawing.Point(733, 333);
            this.cbSamleLiveReload.Name = "cbSamleLiveReload";
            this.cbSamleLiveReload.Size = new System.Drawing.Size(78, 17);
            this.cbSamleLiveReload.TabIndex = 43;
            this.cbSamleLiveReload.Text = "Live reload";
            this.cbSamleLiveReload.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 709);
            this.Controls.Add(this.cbSamleLiveReload);
            this.Controls.Add(this.numKernel9);
            this.Controls.Add(this.numKernel8);
            this.Controls.Add(this.numKernel7);
            this.Controls.Add(this.numKernel6);
            this.Controls.Add(this.numKernel5);
            this.Controls.Add(this.numKernel4);
            this.Controls.Add(this.numKernel3);
            this.Controls.Add(this.numKernel2);
            this.Controls.Add(this.cbCppLiveReload);
            this.Controls.Add(this.cbAsmLiveReload);
            this.Controls.Add(this.cbCsLiveReload);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numKernel1);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picAsm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picCpp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picCs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picSample);
            this.Controls.Add(this.lbBestTimeAsm);
            this.Controls.Add(this.lbBestTimeCpp);
            this.Controls.Add(this.lbBestTimeCs);
            this.Controls.Add(this.btnUnsharpMaskingAsm);
            this.Controls.Add(this.btnUnsharpMaskingCpp);
            this.Controls.Add(this.btnOpenInputFileDialog);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.lbInputFile);
            this.Controls.Add(this.btnUnsharpMaskingCs);
            this.Name = "Form1";
            this.Text = "Unsharp Masking Algorithm";
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCpp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAsm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKernel7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnsharpMaskingCs;
        private System.Windows.Forms.Label lbInputFile;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Button btnOpenInputFileDialog;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button btnUnsharpMaskingCpp;
        private System.Windows.Forms.Button btnUnsharpMaskingAsm;
        private System.Windows.Forms.Label lbBestTimeCs;
        private System.Windows.Forms.Label lbBestTimeCpp;
        private System.Windows.Forms.Label lbBestTimeAsm;
        private System.Windows.Forms.PictureBox picSample;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picCs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picCpp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picAsm;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.NumericUpDown numKernel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbCsLiveReload;
        private System.Windows.Forms.CheckBox cbAsmLiveReload;
        private System.Windows.Forms.CheckBox cbCppLiveReload;
        private System.Windows.Forms.NumericUpDown numKernel2;
        private System.Windows.Forms.NumericUpDown numKernel3;
        private System.Windows.Forms.NumericUpDown numKernel6;
        private System.Windows.Forms.NumericUpDown numKernel5;
        private System.Windows.Forms.NumericUpDown numKernel4;
        private System.Windows.Forms.NumericUpDown numKernel9;
        private System.Windows.Forms.NumericUpDown numKernel8;
        private System.Windows.Forms.NumericUpDown numKernel7;
        private System.Windows.Forms.CheckBox cbSamleLiveReload;
    }
}

