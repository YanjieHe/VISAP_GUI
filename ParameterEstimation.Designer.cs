namespace 统计图形界面1
{
    partial class SingleParameterEstimation
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
            this.button_estimate = new System.Windows.Forms.Button();
            this.comboBox_Method = new System.Windows.Forms.ComboBox();
            this.textBox_result = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Cols = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ColShow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_Confidence = new System.Windows.Forms.ComboBox();
            this.comboBox_tail = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button_estimate
            // 
            this.button_estimate.Location = new System.Drawing.Point(799, 310);
            this.button_estimate.Name = "button_estimate";
            this.button_estimate.Size = new System.Drawing.Size(230, 43);
            this.button_estimate.TabIndex = 0;
            this.button_estimate.Text = "估计";
            this.button_estimate.UseVisualStyleBackColor = true;
            this.button_estimate.Click += new System.EventHandler(this.button_estimate_Click);
            // 
            // comboBox_Method
            // 
            this.comboBox_Method.FormattingEnabled = true;
            this.comboBox_Method.Items.AddRange(new object[] {
            "均值",
            "比例"});
            this.comboBox_Method.Location = new System.Drawing.Point(933, 185);
            this.comboBox_Method.Name = "comboBox_Method";
            this.comboBox_Method.Size = new System.Drawing.Size(96, 20);
            this.comboBox_Method.TabIndex = 2;
            // 
            // textBox_result
            // 
            this.textBox_result.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_result.Location = new System.Drawing.Point(12, 12);
            this.textBox_result.Multiline = true;
            this.textBox_result.Name = "textBox_result";
            this.textBox_result.Size = new System.Drawing.Size(777, 341);
            this.textBox_result.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(799, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "待估计参数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(795, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "列数：";
            // 
            // textBox_Cols
            // 
            this.textBox_Cols.Location = new System.Drawing.Point(799, 41);
            this.textBox_Cols.Multiline = true;
            this.textBox_Cols.Name = "textBox_Cols";
            this.textBox_Cols.Size = new System.Drawing.Size(230, 48);
            this.textBox_Cols.TabIndex = 7;
            this.textBox_Cols.TextChanged += new System.EventHandler(this.textBox_Cols_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(799, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "变量名：";
            // 
            // textBox_ColShow
            // 
            this.textBox_ColShow.Location = new System.Drawing.Point(799, 125);
            this.textBox_ColShow.Multiline = true;
            this.textBox_ColShow.Name = "textBox_ColShow";
            this.textBox_ColShow.ReadOnly = true;
            this.textBox_ColShow.Size = new System.Drawing.Size(230, 48);
            this.textBox_ColShow.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(799, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "置信度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(799, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "单双尾：";
            // 
            // comboBox_Confidence
            // 
            this.comboBox_Confidence.FormattingEnabled = true;
            this.comboBox_Confidence.Items.AddRange(new object[] {
            "95%",
            "90%",
            "99%"});
            this.comboBox_Confidence.Location = new System.Drawing.Point(933, 229);
            this.comboBox_Confidence.Name = "comboBox_Confidence";
            this.comboBox_Confidence.Size = new System.Drawing.Size(96, 20);
            this.comboBox_Confidence.TabIndex = 14;
            this.comboBox_Confidence.Text = "95%";
            // 
            // comboBox_tail
            // 
            this.comboBox_tail.FormattingEnabled = true;
            this.comboBox_tail.Items.AddRange(new object[] {
            "双尾",
            "左单尾",
            "右单尾"});
            this.comboBox_tail.Location = new System.Drawing.Point(933, 269);
            this.comboBox_tail.Name = "comboBox_tail";
            this.comboBox_tail.Size = new System.Drawing.Size(96, 20);
            this.comboBox_tail.TabIndex = 15;
            this.comboBox_tail.Text = "双尾";
            // 
            // SingleParameterEstimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 388);
            this.Controls.Add(this.comboBox_tail);
            this.Controls.Add(this.comboBox_Confidence);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_ColShow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Cols);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_result);
            this.Controls.Add(this.comboBox_Method);
            this.Controls.Add(this.button_estimate);
            this.Name = "SingleParameterEstimation";
            this.Text = "SingleParameterEstimation";
            this.Load += new System.EventHandler(this.SingleParameterEstimation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_estimate;
        private System.Windows.Forms.ComboBox comboBox_Method;
        private System.Windows.Forms.TextBox textBox_result;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Cols;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_ColShow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_Confidence;
        private System.Windows.Forms.ComboBox comboBox_tail;
    }
}