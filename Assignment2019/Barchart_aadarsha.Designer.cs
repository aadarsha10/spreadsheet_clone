namespace Assignment2019
{
    partial class Barchart_aadarsha
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Chart_aadarsha = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_aadarsha)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart_aadarsha
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart_aadarsha.ChartAreas.Add(chartArea1);
            this.Chart_aadarsha.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.Chart_aadarsha.Legends.Add(legend1);
            this.Chart_aadarsha.Location = new System.Drawing.Point(0, 0);
            this.Chart_aadarsha.Name = "Chart_aadarsha";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart_aadarsha.Series.Add(series1);
            this.Chart_aadarsha.Size = new System.Drawing.Size(800, 450);
            this.Chart_aadarsha.TabIndex = 0;
            this.Chart_aadarsha.Text = "chart_ams";
           // this.Chart_aadarsha.Click += new System.EventHandler(this.Chart_aadarsha_Click);
            // 
            // Barchart_aadarsha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Chart_aadarsha);
            this.Name = "Barchart_aadarsha";
            this.Text = "Barchart_aadarsha";
            this.Load += new System.EventHandler(this.Barchart_aadarsha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_aadarsha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_aadarsha;
    }
}