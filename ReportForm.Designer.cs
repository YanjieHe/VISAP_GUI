namespace 统计图形界面1
{
    partial class ReportForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.字体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.蓝色红色绿色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.蓝色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.红色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绿色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目符号列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字体格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.斜体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下划线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.字体ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(859, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(859, 686);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // 字体ToolStripMenuItem
            // 
            this.字体ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.蓝色红色绿色ToolStripMenuItem,
            this.项目符号列表ToolStripMenuItem,
            this.字体格式ToolStripMenuItem});
            this.字体ToolStripMenuItem.Name = "字体ToolStripMenuItem";
            this.字体ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.字体ToolStripMenuItem.Text = "字体";
            // 
            // 蓝色红色绿色ToolStripMenuItem
            // 
            this.蓝色红色绿色ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.蓝色ToolStripMenuItem,
            this.红色ToolStripMenuItem,
            this.绿色ToolStripMenuItem});
            this.蓝色红色绿色ToolStripMenuItem.Name = "蓝色红色绿色ToolStripMenuItem";
            this.蓝色红色绿色ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.蓝色红色绿色ToolStripMenuItem.Text = "颜色";
            // 
            // 蓝色ToolStripMenuItem
            // 
            this.蓝色ToolStripMenuItem.Name = "蓝色ToolStripMenuItem";
            this.蓝色ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.蓝色ToolStripMenuItem.Text = "蓝色";
            this.蓝色ToolStripMenuItem.Click += new System.EventHandler(this.蓝色ToolStripMenuItem_Click);
            // 
            // 红色ToolStripMenuItem
            // 
            this.红色ToolStripMenuItem.Name = "红色ToolStripMenuItem";
            this.红色ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.红色ToolStripMenuItem.Text = "红色";
            this.红色ToolStripMenuItem.Click += new System.EventHandler(this.红色ToolStripMenuItem_Click);
            // 
            // 绿色ToolStripMenuItem
            // 
            this.绿色ToolStripMenuItem.Name = "绿色ToolStripMenuItem";
            this.绿色ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.绿色ToolStripMenuItem.Text = "绿色";
            this.绿色ToolStripMenuItem.Click += new System.EventHandler(this.绿色ToolStripMenuItem_Click);
            // 
            // 项目符号列表ToolStripMenuItem
            // 
            this.项目符号列表ToolStripMenuItem.Name = "项目符号列表ToolStripMenuItem";
            this.项目符号列表ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.项目符号列表ToolStripMenuItem.Text = "项目符号列表";
            this.项目符号列表ToolStripMenuItem.Click += new System.EventHandler(this.项目符号列表ToolStripMenuItem_Click);
            // 
            // 字体格式ToolStripMenuItem
            // 
            this.字体格式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粗体ToolStripMenuItem,
            this.斜体ToolStripMenuItem,
            this.下划线ToolStripMenuItem});
            this.字体格式ToolStripMenuItem.Name = "字体格式ToolStripMenuItem";
            this.字体格式ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.字体格式ToolStripMenuItem.Text = "字体格式";
            // 
            // 粗体ToolStripMenuItem
            // 
            this.粗体ToolStripMenuItem.Name = "粗体ToolStripMenuItem";
            this.粗体ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.粗体ToolStripMenuItem.Text = "粗体";
            this.粗体ToolStripMenuItem.Click += new System.EventHandler(this.粗体ToolStripMenuItem_Click);
            // 
            // 斜体ToolStripMenuItem
            // 
            this.斜体ToolStripMenuItem.Name = "斜体ToolStripMenuItem";
            this.斜体ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.斜体ToolStripMenuItem.Text = "斜体";
            this.斜体ToolStripMenuItem.Click += new System.EventHandler(this.斜体ToolStripMenuItem_Click);
            // 
            // 下划线ToolStripMenuItem
            // 
            this.下划线ToolStripMenuItem.Name = "下划线ToolStripMenuItem";
            this.下划线ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.下划线ToolStripMenuItem.Text = "下划线";
            this.下划线ToolStripMenuItem.Click += new System.EventHandler(this.下划线ToolStripMenuItem_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 712);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportForm_FormClosing_1);
            this.TextChanged += new System.EventHandler(this.ReportForm_TextChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 字体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 蓝色红色绿色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 蓝色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 红色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绿色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目符号列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字体格式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粗体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 斜体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下划线ToolStripMenuItem;
    }
}