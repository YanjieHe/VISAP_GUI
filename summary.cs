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
    public partial class SummaryForm : Form
    {
        public SummaryForm()
        {
            InitializeComponent();
            if (Form1.S.dataGridView1.DataSource != null)
            {
                int Col1, Col2;
                int temp;
                int selectedCellCount = Form1.S.dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
                Col1 = Form1.S.dataGridView1.CurrentRow.Cells.IndexOf(Form1.S.dataGridView1.CurrentCell) + 1;
                Col2 =Form1.S.dataGridView1.SelectedCells[selectedCellCount - 1].ColumnIndex + 1;
                if (Col1 > Col2)
                {
                    temp = Col1;
                    Col1 = Col2;
                    Col2 = temp;
                }
                textBox_StartCol.Text = Col1.ToString();
                textBox_EndCol.Text = Col2.ToString();
                //此处+1是为了调整为用户所看到的列数
            }
            int n;
            if (textBox_EndCol.Text.Trim() == "*")
            {
                n = 0;
            }
            else
            {
                n = Convert.ToInt32(textBox_StartCol.Text);
            }
            label_Var1.Text = Form1.S.dataGridView1.Columns[n-1].HeaderCell.Value.ToString();
            if (textBox_EndCol.Text.Trim() == "*")
            {
                n = Form1.S.dataGridView1.ColumnCount;
            }
            else
            {
                n = Convert.ToInt32(textBox_EndCol.Text);
            }
            label_Var2.Text = Form1.S.dataGridView1.Columns[n-1].HeaderCell.Value.ToString();

        }

        private void SummaryForm_Load(object sender, EventArgs e)
        {

        }

        private void button_summary_Click(object sender, EventArgs e)
        {
            int StartCol, EndCol;
            if (textBox_StartCol.Text.Trim() == "*")
            {
                StartCol = 1;
            }
            else
            {
                StartCol = Convert.ToInt32(textBox_StartCol.Text);
            }
            if (textBox_EndCol.Text.Trim() == "*")
            {
                EndCol = Form1.S.dataGridView1.ColumnCount;
            }
            else
            {
                EndCol = Convert.ToInt32(textBox_EndCol.Text);
            }
            string summary_result = Form1.S.Summary(StartCol -1,EndCol -1);
            textBox_summary.Text = summary_result;
            //Form1.S.ReaddgvSelected(2, 2, 10, 10);
        }

        private void textBox_StartCol_TextChanged(object sender, EventArgs e)
        {
            int n;
            if (textBox_StartCol.Text.Trim() == "*")
            {
                n = 1;
            }
            else
            {
                n = Convert.ToInt32(textBox_StartCol.Text);
            }
            if (n <= Form1.S.dataGridView1.ColumnCount)
            {
                label_Var1.Text = Form1.S.dataGridView1.Columns[n-1].HeaderCell.Value.ToString();

            }
            
        }

        private void textBox_EndCol_TextChanged(object sender, EventArgs e)
        {
            int n;
            if (textBox_EndCol.Text.Trim() == "*")
            {
                n = Form1.S.dataGridView1.ColumnCount;
            }
            else
            {
                n = Convert.ToInt32(textBox_EndCol.Text);
            }
            if (n <= Form1.S.dataGridView1.ColumnCount)
            {
                label_Var2.Text = Form1.S.dataGridView1.Columns[n - 1].HeaderCell.Value.ToString();

            }
        }
    }
}
