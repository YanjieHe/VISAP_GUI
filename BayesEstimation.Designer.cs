namespace 统计图形界面1
{
    partial class BayesEstimation
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart_prior = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_posterior = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBox_a = new System.Windows.Forms.TextBox();
            this.textBox_b = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_PriorVariance = new System.Windows.Forms.TextBox();
            this.textBox_PriorMean = new System.Windows.Forms.TextBox();
            this.button_GeneratePrior = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart_prior)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_posterior)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_prior
            // 
            chartArea5.Name = "ChartArea1";
            this.chart_prior.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart_prior.Legends.Add(legend5);
            this.chart_prior.Location = new System.Drawing.Point(3, 7);
            this.chart_prior.Name = "chart_prior";
            this.chart_prior.Size = new System.Drawing.Size(500, 369);
            this.chart_prior.TabIndex = 0;
            this.chart_prior.Text = "chart1";
            // 
            // chart_posterior
            // 
            chartArea6.Name = "ChartArea1";
            this.chart_posterior.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart_posterior.Legends.Add(legend6);
            this.chart_posterior.Location = new System.Drawing.Point(559, 7);
            this.chart_posterior.Name = "chart_posterior";
            this.chart_posterior.Size = new System.Drawing.Size(500, 369);
            this.chart_posterior.TabIndex = 1;
            this.chart_posterior.Text = "chart2";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 402);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(410, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // textBox_a
            // 
            this.textBox_a.Location = new System.Drawing.Point(130, 436);
            this.textBox_a.Name = "textBox_a";
            this.textBox_a.Size = new System.Drawing.Size(82, 21);
            this.textBox_a.TabIndex = 3;
            this.textBox_a.Text = "5";
            // 
            // textBox_b
            // 
            this.textBox_b.Location = new System.Drawing.Point(322, 436);
            this.textBox_b.Name = "textBox_b";
            this.textBox_b.Size = new System.Drawing.Size(82, 21);
            this.textBox_b.TabIndex = 4;
            this.textBox_b.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(78, 438);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "a:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(287, 438);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "b:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(221, 465);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "先验方差:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 465);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "先验均值:";
            // 
            // textBox_PriorVariance
            // 
            this.textBox_PriorVariance.Location = new System.Drawing.Point(322, 463);
            this.textBox_PriorVariance.Name = "textBox_PriorVariance";
            this.textBox_PriorVariance.ReadOnly = true;
            this.textBox_PriorVariance.Size = new System.Drawing.Size(82, 21);
            this.textBox_PriorVariance.TabIndex = 8;
            // 
            // textBox_PriorMean
            // 
            this.textBox_PriorMean.Location = new System.Drawing.Point(130, 463);
            this.textBox_PriorMean.Name = "textBox_PriorMean";
            this.textBox_PriorMean.ReadOnly = true;
            this.textBox_PriorMean.Size = new System.Drawing.Size(82, 21);
            this.textBox_PriorMean.TabIndex = 7;
            // 
            // button_GeneratePrior
            // 
            this.button_GeneratePrior.Location = new System.Drawing.Point(428, 438);
            this.button_GeneratePrior.Name = "button_GeneratePrior";
            this.button_GeneratePrior.Size = new System.Drawing.Size(75, 23);
            this.button_GeneratePrior.TabIndex = 11;
            this.button_GeneratePrior.Text = "生成先验";
            this.button_GeneratePrior.UseVisualStyleBackColor = true;
            this.button_GeneratePrior.Click += new System.EventHandler(this.button_GeneratePrior_Click);
            // 
            // BayesEstimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 492);
            this.Controls.Add(this.button_GeneratePrior);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_PriorVariance);
            this.Controls.Add(this.textBox_PriorMean);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_b);
            this.Controls.Add(this.textBox_a);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.chart_posterior);
            this.Controls.Add(this.chart_prior);
            this.Name = "BayesEstimation";
            this.Text = "BayesEstimation";
            this.Load += new System.EventHandler(this.BayesEstimation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_prior)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_posterior)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_prior;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_posterior;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox textBox_a;
        private System.Windows.Forms.TextBox textBox_b;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_PriorVariance;
        private System.Windows.Forms.TextBox textBox_PriorMean;
        private System.Windows.Forms.Button button_GeneratePrior;
    }
}