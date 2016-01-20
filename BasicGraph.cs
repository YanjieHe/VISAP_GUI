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
    public partial class BasicGraph : Form
    {
        public BasicGraph()
        {
            InitializeComponent();
            textBox_ChosenCols.Text = RenewCols();
        }
        static int[] ColumnsSelected()
        {
            
             int selectedCellCount =Form1.S.dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
             
             if (selectedCellCount > 0)
             {
                 int[] ColumnsChosen = new int[selectedCellCount];
                 if (Form1.S.dataGridView1.AreAllCellsSelected(true))
                 {
                     for (int i = 0; i < Form1.S.dataGridView1.Columns.Count; i++)
                     {
                         ColumnsChosen[i] = i;
                     }
                    
                 }
                 else
                 {
                     for (int i = 0;i < selectedCellCount; i++)
                     {
                         ColumnsChosen[i] = Form1.S.dataGridView1.SelectedCells[i].ColumnIndex;
                     }
                     
                 }
                 return ColumnsChosen;
             }
             return new int[] { -1, -1, -1, -1 };
        }
        private float X;
        private float Y;
        private void setTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }
        private void BasicGraph_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(BasicGraph_Resize);//窗体调整大小时引发事件
            X = this.Width;//获取窗体的宽度
            Y = this.Height;//获取窗体的高度
            setTag(this);//调用方法
        }
        void BasicGraph_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X; //窗体宽度缩放比例
            float newy = this.Height / Y;//窗体高度缩放比例
            setControls(newx, newy, this);//随窗体改变控件大小
            //this.Text = this.Width.ToString() + " " + this.Height.ToString();//窗体标题栏文本
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        static string RenewCols()
        {
            int []  ColumnsChosen= new int [Form1.S.dataGridView1.Columns.Count];
            ColumnsChosen = ColumnsSelected();
            string AllSelectedCol = "";
            if (ColumnsChosen != new int[] { -1, -1, -1, -1 })
            {
                Array.Sort(ColumnsChosen);
                for (int i = 0; i < ColumnsChosen.Length; i++)
                {
                    if (i == 0){
                        AllSelectedCol = (ColumnsChosen[i]+1).ToString();
                    }
                    else
                    {
                        //只有第一列有可能为0，此后每列都不可能为0
                        if (ColumnsChosen[i]!= 0)
                        AllSelectedCol = AllSelectedCol + "," + (ColumnsChosen[i]+1).ToString();
                    }
                    
                }
            }
            return AllSelectedCol;
        }
        private void button_refresh_Click(object sender, EventArgs e)
        {
                textBox_ChosenCols.Text = RenewCols();
            } 
        }
    }

