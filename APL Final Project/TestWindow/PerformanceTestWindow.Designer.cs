namespace APL_Final_Project.TestWindow
{
    partial class PerformanceTestWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.lstTestFiles = new System.Windows.Forms.ListBox();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnExecuteTests = new System.Windows.Forms.Button();
            this.chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtCsvData = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).BeginInit();
            this.SuspendLayout();
            // 
            // lstTestFiles
            // 
            this.lstTestFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstTestFiles.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTestFiles.FormattingEnabled = true;
            this.lstTestFiles.ItemHeight = 17;
            this.lstTestFiles.Location = new System.Drawing.Point(12, 12);
            this.lstTestFiles.Name = "lstTestFiles";
            this.lstTestFiles.Size = new System.Drawing.Size(244, 463);
            this.lstTestFiles.TabIndex = 0;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFile.Location = new System.Drawing.Point(12, 489);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(244, 32);
            this.btnAddFile.TabIndex = 1;
            this.btnAddFile.Text = "Add test file";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnExecuteTests
            // 
            this.btnExecuteTests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteTests.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteTests.Location = new System.Drawing.Point(262, 489);
            this.btnExecuteTests.Name = "btnExecuteTests";
            this.btnExecuteTests.Size = new System.Drawing.Size(868, 32);
            this.btnExecuteTests.TabIndex = 3;
            this.btnExecuteTests.Text = "Execute tests";
            this.btnExecuteTests.UseVisualStyleBackColor = true;
            this.btnExecuteTests.Click += new System.EventHandler(this.btnExecuteTests_Click);
            // 
            // chartData
            // 
            this.chartData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartData.BorderlineColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chartData.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartData.Legends.Add(legend1);
            this.chartData.Location = new System.Drawing.Point(262, 12);
            this.chartData.Name = "chartData";
            this.chartData.Size = new System.Drawing.Size(868, 350);
            this.chartData.TabIndex = 4;
            this.chartData.Text = "chart1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // txtCsvData
            // 
            this.txtCsvData.Location = new System.Drawing.Point(263, 369);
            this.txtCsvData.Multiline = true;
            this.txtCsvData.Name = "txtCsvData";
            this.txtCsvData.Size = new System.Drawing.Size(867, 106);
            this.txtCsvData.TabIndex = 5;
            // 
            // PerformanceTestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 524);
            this.Controls.Add(this.txtCsvData);
            this.Controls.Add(this.chartData);
            this.Controls.Add(this.btnExecuteTests);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.lstTestFiles);
            this.Name = "PerformanceTestWindow";
            this.Text = "Performance tests";
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstTestFiles;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Button btnExecuteTests;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtCsvData;
    }
}