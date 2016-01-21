namespace 统计图形界面1
{
    partial class BasicGraph
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart_basic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip_Graph = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.qToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView_subset = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ChosenCols = new System.Windows.Forms.TextBox();
            this.button_import = new System.Windows.Forms.Button();
            this.comboBox_x = new System.Windows.Forms.ComboBox();
            this.comboBox_y = new System.Windows.Forms.ComboBox();
            this.button_AddPlot = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_copy = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_refresh = new System.Windows.Forms.Button();
            this.button_ImportReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart_basic)).BeginInit();
            this.contextMenuStrip_Graph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_subset)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_basic
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_basic.ChartAreas.Add(chartArea2);
            this.chart_basic.ContextMenuStrip = this.contextMenuStrip_Graph;
            legend2.Name = "Legend1";
            this.chart_basic.Legends.Add(legend2);
            this.chart_basic.Location = new System.Drawing.Point(274, 48);
            this.chart_basic.Name = "chart_basic";
            this.chart_basic.Size = new System.Drawing.Size(677, 428);
            this.chart_basic.TabIndex = 0;
            this.chart_basic.Text = "chart1";
            // 
            // contextMenuStrip_Graph
            // 
            this.contextMenuStrip_Graph.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qToolStripMenuItem,
            this.复制ToolStripMenuItem,
            this.粘贴ToolStripMenuItem});
            this.contextMenuStrip_Graph.Name = "contextMenuStrip_Graph";
            this.contextMenuStrip_Graph.Size = new System.Drawing.Size(101, 70);
            // 
            // qToolStripMenuItem
            // 
            this.qToolStripMenuItem.Name = "qToolStripMenuItem";
            this.qToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.qToolStripMenuItem.Text = "清空";
            this.qToolStripMenuItem.Click += new System.EventHandler(this.qToolStripMenuItem_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem.Text = "保存";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // dataGridView_subset
            // 
            this.dataGridView_subset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_subset.Location = new System.Drawing.Point(12, 186);
            this.dataGridView_subset.Name = "dataGridView_subset";
            this.dataGridView_subset.RowTemplate.Height = 23;
            this.dataGridView_subset.Size = new System.Drawing.Size(220, 287);
            this.dataGridView_subset.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "x:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(15, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "y:";
            // 
            // textBox_ChosenCols
            // 
            this.textBox_ChosenCols.Location = new System.Drawing.Point(18, 119);
            this.textBox_ChosenCols.Name = "textBox_ChosenCols";
            this.textBox_ChosenCols.Size = new System.Drawing.Size(214, 21);
            this.textBox_ChosenCols.TabIndex = 11;
            this.textBox_ChosenCols.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(157, 152);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(75, 23);
            this.button_import.TabIndex = 12;
            this.button_import.Text = "导入数据";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // comboBox_x
            // 
            this.comboBox_x.FormattingEnabled = true;
            this.comboBox_x.Location = new System.Drawing.Point(45, 13);
            this.comboBox_x.Name = "comboBox_x";
            this.comboBox_x.Size = new System.Drawing.Size(187, 20);
            this.comboBox_x.TabIndex = 13;
            // 
            // comboBox_y
            // 
            this.comboBox_y.FormattingEnabled = true;
            this.comboBox_y.Location = new System.Drawing.Point(46, 48);
            this.comboBox_y.Name = "comboBox_y";
            this.comboBox_y.Size = new System.Drawing.Size(186, 20);
            this.comboBox_y.TabIndex = 14;
            // 
            // button_AddPlot
            // 
            this.button_AddPlot.Location = new System.Drawing.Point(274, 13);
            this.button_AddPlot.Name = "button_AddPlot";
            this.button_AddPlot.Size = new System.Drawing.Size(75, 23);
            this.button_AddPlot.TabIndex = 1;
            this.button_AddPlot.Text = "添加";
            this.button_AddPlot.UseVisualStyleBackColor = true;
            this.button_AddPlot.Click += new System.EventHandler(this.button_AddPlot_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(424, 13);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 2;
            this.button_clear.Text = "清空";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // button_copy
            // 
            this.button_copy.Location = new System.Drawing.Point(574, 13);
            this.button_copy.Name = "button_copy";
            this.button_copy.Size = new System.Drawing.Size(75, 23);
            this.button_copy.TabIndex = 3;
            this.button_copy.Text = "复制";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Click += new System.EventHandler(this.button_copy_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(724, 13);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 15;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // comboBox_type
            // 
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "散点图",
            "折线图",
            "气泡图"});
            this.comboBox_type.Location = new System.Drawing.Point(90, 87);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(142, 20);
            this.comboBox_type.TabIndex = 16;
            this.comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(15, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "图表类型:";
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(67, 151);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(75, 23);
            this.button_refresh.TabIndex = 18;
            this.button_refresh.Text = "刷新列数";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // button_ImportReport
            // 
            this.button_ImportReport.Location = new System.Drawing.Point(874, 13);
            this.button_ImportReport.Name = "button_ImportReport";
            this.button_ImportReport.Size = new System.Drawing.Size(75, 23);
            this.button_ImportReport.TabIndex = 19;
            this.button_ImportReport.Text = "导入报告";
            this.button_ImportReport.UseVisualStyleBackColor = true;
            this.button_ImportReport.Click += new System.EventHandler(this.button_ImportReport_Click);
            // 
            // BasicGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 485);
            this.Controls.Add(this.button_ImportReport);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_y);
            this.Controls.Add(this.comboBox_x);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.textBox_ChosenCols);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_subset);
            this.Controls.Add(this.button_copy);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_AddPlot);
            this.Controls.Add(this.chart_basic);
            this.Name = "BasicGraph";
            this.Text = "基本图表";
            this.Load += new System.EventHandler(this.BasicGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_basic)).EndInit();
            this.contextMenuStrip_Graph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_subset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_basic;
        private System.Windows.Forms.DataGridView dataGridView_subset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ChosenCols;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.ComboBox comboBox_x;
        private System.Windows.Forms.ComboBox comboBox_y;
        private System.Windows.Forms.Button button_AddPlot;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_copy;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Graph;
        private System.Windows.Forms.ToolStripMenuItem qToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.Button button_ImportReport;
    }
}