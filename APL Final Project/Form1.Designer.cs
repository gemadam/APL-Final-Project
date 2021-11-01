
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
            this.lbOutputFile = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.btnOpenInputFileDialog = new System.Windows.Forms.Button();
            this.btnOpenOutputFileDialog = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnUnsharpMaskingCpp = new System.Windows.Forms.Button();
            this.btnUnsharpMaskingAsm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUnsharpMaskingCs
            // 
            this.btnUnsharpMaskingCs.Location = new System.Drawing.Point(12, 107);
            this.btnUnsharpMaskingCs.Name = "btnUnsharpMaskingCs";
            this.btnUnsharpMaskingCs.Size = new System.Drawing.Size(398, 23);
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
            // lbOutputFile
            // 
            this.lbOutputFile.AutoSize = true;
            this.lbOutputFile.Location = new System.Drawing.Point(12, 56);
            this.lbOutputFile.Name = "lbOutputFile";
            this.lbOutputFile.Size = new System.Drawing.Size(55, 13);
            this.lbOutputFile.TabIndex = 2;
            this.lbOutputFile.Text = "Output file";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(12, 25);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(360, 20);
            this.txtInputFile.TabIndex = 3;
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(12, 72);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(360, 20);
            this.txtOutputFile.TabIndex = 4;
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
            // btnOpenOutputFileDialog
            // 
            this.btnOpenOutputFileDialog.Location = new System.Drawing.Point(378, 69);
            this.btnOpenOutputFileDialog.Name = "btnOpenOutputFileDialog";
            this.btnOpenOutputFileDialog.Size = new System.Drawing.Size(32, 23);
            this.btnOpenOutputFileDialog.TabIndex = 6;
            this.btnOpenOutputFileDialog.Text = "...";
            this.btnOpenOutputFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenOutputFileDialog.Click += new System.EventHandler(this.btnOpenOutputFileDialog_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "fileDialog";
            // 
            // btnUnsharpMaskingCpp
            // 
            this.btnUnsharpMaskingCpp.Location = new System.Drawing.Point(12, 136);
            this.btnUnsharpMaskingCpp.Name = "btnUnsharpMaskingCpp";
            this.btnUnsharpMaskingCpp.Size = new System.Drawing.Size(398, 23);
            this.btnUnsharpMaskingCpp.TabIndex = 7;
            this.btnUnsharpMaskingCpp.Text = "UnsharpMaskingCpp";
            this.btnUnsharpMaskingCpp.UseVisualStyleBackColor = true;
            this.btnUnsharpMaskingCpp.Click += new System.EventHandler(this.btnUnsharpMaskingCpp_Click);
            // 
            // btnUnsharpMaskingAsm
            // 
            this.btnUnsharpMaskingAsm.Location = new System.Drawing.Point(12, 165);
            this.btnUnsharpMaskingAsm.Name = "btnUnsharpMaskingAsm";
            this.btnUnsharpMaskingAsm.Size = new System.Drawing.Size(398, 23);
            this.btnUnsharpMaskingAsm.TabIndex = 8;
            this.btnUnsharpMaskingAsm.Text = "Unsharp Masking Asm";
            this.btnUnsharpMaskingAsm.UseVisualStyleBackColor = true;
            this.btnUnsharpMaskingAsm.Click += new System.EventHandler(this.btnUnsharpMaskingAsm_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 450);
            this.Controls.Add(this.btnUnsharpMaskingAsm);
            this.Controls.Add(this.btnUnsharpMaskingCpp);
            this.Controls.Add(this.btnOpenOutputFileDialog);
            this.Controls.Add(this.btnOpenInputFileDialog);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.lbOutputFile);
            this.Controls.Add(this.lbInputFile);
            this.Controls.Add(this.btnUnsharpMaskingCs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnsharpMaskingCs;
        private System.Windows.Forms.Label lbInputFile;
        private System.Windows.Forms.Label lbOutputFile;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Button btnOpenInputFileDialog;
        private System.Windows.Forms.Button btnOpenOutputFileDialog;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button btnUnsharpMaskingCpp;
        private System.Windows.Forms.Button btnUnsharpMaskingAsm;
    }
}

