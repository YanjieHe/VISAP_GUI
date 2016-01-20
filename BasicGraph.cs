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
            textBox_ChosenCols.Text=RenewCol();

        }
        void ImportOriginData()
        {
            DataTable OriginDT = new DataTable();
            DataTable NewDT = new DataTable();
            OriginDT = Form1.S.dataGridView1.DataSource as DataTable ;
            if (OriginDT == null)
            {
                OriginDT = Form1.table;
            } 
            DataColumn AddCol = new DataColumn();
            char [] separator = {','};
            string UseToAddCol = "";
            int AddTimes = 1;
            string[] ColWanted = textBox_ChosenCols.Text.Split(separator);
            if (OriginDT != null)
            { for (int q = 0; q < OriginDT.Rows.Count; q++)
                                {
                                    NewDT.Rows.Add();
                                }
                
                    for (int i = 0; i < OriginDT.Columns.Count; i++)
                    {
                        foreach (string EachCol in ColWanted)
                        {
                            if (Convert.ToInt32(EachCol) - 1 == i)
                            {
                                NewDT.Columns.Add(OriginDT.Columns[i].ColumnName);
                                for (int m = 0; m < OriginDT.Rows.Count; m++)
                                {
                                    //NewDT.Rows.Add();
                                    UseToAddCol = OriginDT.Rows[m].ItemArray[i].ToString();
                                    NewDT.Rows[m][OriginDT.Columns[i].ColumnName] = UseToAddCol;
                                    AddTimes++;
                                }
                                break;
                            }
                        }
                    }


            }
            else 
            {
                MessageBox.Show("null");
            }
            dataGridView_subset.DataSource = NewDT;
        }
        static string RenewCol(){
            int selectedCellCount =Form1.S.dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
            int []  ColumnsChosen= new int [selectedCellCount];
            ColumnsChosen = ColumnsSelected();
            string AllSelectedCol = "";
            int Record_Zero = 0;
            if (ColumnsChosen != new int[] { -1, -1, -1, -1 })
            {
                //MessageBox.Show("selectedCellCount = " + selectedCellCount);
                Array.Sort(ColumnsChosen);
                for (int i = 0; i < selectedCellCount; i++)
                {
                    //MessageBox.Show("ColumnsChosen[i]"+ColumnsChosen[i]);
                    if (ColumnsChosen[i] >= 0)
                    {
                        Record_Zero = i;
                        break;
                    }
                }
                //MessageBox.Show("i = " + Record_Zero );
                    
                    for (int i = Record_Zero; i < selectedCellCount; i++)
                {
                    if (i == Record_Zero)
                    {
                        AllSelectedCol = (ColumnsChosen[i]+1).ToString();

                    }
                    else
                    {
                        AllSelectedCol = AllSelectedCol + "," + (ColumnsChosen[i]+1).ToString();
                    }
                    
                }
                
            }
            return AllSelectedCol;
        }

        static int[] ColumnsSelected()
        {
            int counts = 0;
            int UseToIdentify = 0;
            int countforNew = 0;
             int selectedCellCount =Form1.S.dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
             if (selectedCellCount > 0)
             {
                 int[] ColumnsChosen = new int[selectedCellCount];
                 for (int i = 0; i < selectedCellCount; i++)
                 {
                     ColumnsChosen[i] = -100;
                 }
                     if (Form1.S.dataGridView1.AreAllCellsSelected(true))
                     {
                         for (int i = 0; i < Form1.S.dataGridView1.Columns.Count; i++)
                         {
                             counts = 0;
                             foreach (int EachCol in ColumnsChosen)
                             {
                                 if (EachCol == i)
                                 {
                                     counts++;
                                     break;
                                 }
                             }
                             if (counts == 0)
                             {
                                 ColumnsChosen[i] = i;

                             }
                             else
                             {
                                 ColumnsChosen[i] = -100;
                             }

                         }

                     }
                     else
                     {
                         for (int i = 0; i < selectedCellCount; i++)
                         {
                             UseToIdentify = Form1.S.dataGridView1.SelectedCells[i].ColumnIndex;
                             counts = 0;
                             foreach (int EachCol in ColumnsChosen)
                             {
                                 if (EachCol == UseToIdentify)
                                 {
                                     counts++;
                                     break;
                                 }
                             }
                             if (counts == 0)
                             {
                                 ColumnsChosen[countforNew] = UseToIdentify;
                                 countforNew++;
                             }
                             else
                             {
                                 ColumnsChosen[countforNew] = -100;
                                 countforNew++;
                             }
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

        private void button_refresh_Click(object sender, EventArgs e)
        {
            textBox_ChosenCols.Text = RenewCol();
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            ImportOriginData();
        }
    }
}
