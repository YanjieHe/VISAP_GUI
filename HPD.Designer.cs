namespace 统计图形界面1
{
    partial class HPD
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
            this.textBox_result = new System.Windows.Forms.TextBox();
            this.button_HPDsummary = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
            this.textBox_percentile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_result
            // 
            this.textBox_result.Location = new System.Drawing.Point(12, 36);
            this.textBox_result.Multiline = true;
            this.textBox_result.Name = "textBox_result";
            this.textBox_result.Size = new System.Drawing.Size(764, 91);
            this.textBox_result.TabIndex = 0;
            // 
            // button_HPDsummary
            // 
            this.button_HPDsummary.Location = new System.Drawing.Point(568, 7);
            this.button_HPDsummary.Name = "button_HPDsummary";
            this.button_HPDsummary.Size = new System.Drawing.Size(75, 23);
            this.button_HPDsummary.TabIndex = 1;
            this.button_HPDsummary.Text = "汇总";
            this.button_HPDsummary.UseVisualStyleBackColor = true;
            this.button_HPDsummary.Click += new System.EventHandler(this.button_HPDsummary_Click);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(670, 7);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(75, 23);
            this.button_import.TabIndex = 2;
            this.button_import.Text = "导入报告";
            this.button_import.UseVisualStyleBackColor = true;
            // 
            // textBox_percentile
            // 
            this.textBox_percentile.Location = new System.Drawing.Point(133, 7);
            this.textBox_percentile.Name = "textBox_percentile";
            this.textBox_percentile.Size = new System.Drawing.Size(423, 21);
            this.textBox_percentile.TabIndex = 3;
            this.textBox_percentile.Text = "0.025,0.975";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "百分位数：";
            // 
            // HPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 131);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_percentile);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.button_HPDsummary);
            this.Controls.Add(this.textBox_result);
            this.Name = "HPD";
            this.Text = "HPD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_result;
        private System.Windows.Forms.Button button_HPDsummary;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.TextBox textBox_percentile;
        private System.Windows.Forms.Label label1;
    }
}