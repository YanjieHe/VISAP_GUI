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
            Form1.S.ReportIsOn = 1;
            //richTextBox1.SelectedRtf = Form1.S.richTextBox_onForm1.SelectedRtf;
            richTextBox1.Rtf = Form1.S.rtb.Rtf;
        }
        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog ();
            openFileDialog1.Filter = "txt Files|*.txt|所有文件|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                openFileDialog1.FileName = openFileDialog1.FileName;
                if (openFileDialog1.FileName != "")
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            }
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
            //Form1.S.rtb.Rtf = richTextBox1.Rtf;
        }

        private void ReportForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Form1.S.rtb.Rtf = richTextBox1.Rtf;
            Form1.S.ReportIsOn = 0;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "RTF Files|*.rtf|所有文件|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                /*if ((System.IO.Path.GetExtension(openFileDialog1.FileName)).ToLower() == ".txt")
                       richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                   else
                       richTextBox1.LoadFile(openFileDialog1.FileName);*/


                if ((System.IO.Path.GetExtension(saveFileDialog1.FileName)).ToLower() == ".txt")
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                else
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void ReportForm_TextChanged(object sender, EventArgs e)
        {

        }

        private void 蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionColor = Color.Blue;

        }

        private void 红色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionColor = Color.Red;
        }

        private void 绿色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionColor = Color.Green;
        }
        private void ChangeFontStyle(FontStyle style)
        {
            if (style != FontStyle.Bold && style != FontStyle.Italic &&
                style != FontStyle.Underline)
                throw new System.InvalidProgramException("字体格式错误");
            RichTextBox tempRichTextBox = new RichTextBox();  //将要存放被选中文本的副本  
            int curRtbStart = richTextBox1.SelectionStart;
            int len = richTextBox1.SelectionLength;
            int tempRtbStart = 0;
            Font font = richTextBox1.SelectionFont;
            if (len <= 1 && font != null) //与上边的那段代码类似，功能相同  
            {
                if (style == FontStyle.Bold && font.Bold ||
                    style == FontStyle.Italic && font.Italic ||
                    style == FontStyle.Underline && font.Underline)
                {
                    richTextBox1.SelectionFont = new Font(font, font.Style ^ style);
                }
                else if (style == FontStyle.Bold && !font.Bold ||
                         style == FontStyle.Italic && !font.Italic ||
                         style == FontStyle.Underline && !font.Underline)
                {
                    richTextBox1.SelectionFont = new Font(font, font.Style | style);
                }
                return;
            }
            tempRichTextBox.Rtf = richTextBox1.SelectedRtf;
            tempRichTextBox.Select(len - 1, 1); //选中副本中的最后一个文字  
            //克隆被选中的文字Font，这个tempFont主要是用来判断  
            //最终被选中的文字是否要加粗、去粗、斜体、去斜、下划线、去下划线  
            Font tempFont = (Font)tempRichTextBox.SelectionFont.Clone();

            //清空2和3  
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);  //每次选中一个，逐个进行加粗或去粗  
                if (style == FontStyle.Bold && tempFont.Bold ||
                    style == FontStyle.Italic && tempFont.Italic ||
                    style == FontStyle.Underline && tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style ^ style);
                }
                else if (style == FontStyle.Bold && !tempFont.Bold ||
                         style == FontStyle.Italic && !tempFont.Italic ||
                         style == FontStyle.Underline && !tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style | style);
                }
            }
            tempRichTextBox.Select(tempRtbStart, len);
            richTextBox1.SelectedRtf = tempRichTextBox.SelectedRtf; //将设置格式后的副本拷贝给原型  
            richTextBox1.Select(curRtbStart, len);
        }  
        private void 项目符号列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBullet = true;
        }

        private void 粗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Bold);
        }

        private void 斜体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Italic);
        }

        private void 下划线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Underline);
        }

    
       
    }
}
