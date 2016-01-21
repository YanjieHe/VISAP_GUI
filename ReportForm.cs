using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 统计图形界面1
{
    public partial class ReportForm : Form
    {
        public static ReportForm ReportText = null;
        public ReportForm()
        {
            InitializeComponent();
            ReportText = this;
            //richTextBox1.SelectedRtf = Form1.S.richTextBox_onForm1.SelectedRtf;
            richTextBox1.Rtf = Form1.S.rtb.Rtf;
        }
        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*DialogResult dr;
            dr = MessageBox.Show("确定要退出吗？退出之后会丢失现有内容。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                e.Cancel = false; ;
            }
            else
            {
                e.Cancel = true;
            }*/
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Form1.S.rtb.Rtf = richTextBox1.Rtf;
        }

        private void ReportForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Form1.S.rtb.Rtf = richTextBox1.Rtf;
        }

       
    }
}
