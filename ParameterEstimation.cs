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
    public partial class SingleParameterEstimation : Form
    {
        public SingleParameterEstimation()
        {
            InitializeComponent();
            textBox_Cols.Text = RenewCol();
        }

        private void SingleParameterEstimation_Load(object sender, EventArgs e)
        {

        }
        static string RenewCol()
        {
            int selectedCellCount = Form1.S.dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
            int[] ColumnsChosen = new int[selectedCellCount];
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
                        AllSelectedCol = (ColumnsChosen[i] + 1).ToString();

                    }
                    else
                    {
                        AllSelectedCol = AllSelectedCol + "," + (ColumnsChosen[i] + 1).ToString();
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
            int selectedCellCount = Form1.S.dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
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
        
        private void textBox_Cols_TextChanged(object sender, EventArgs e)
        {
            string All_Col_Selected = textBox_Cols.Text;
            char[] separator = { ',' };
            string[] Col_Selected = All_Col_Selected.Split(separator);
            string ColToShow = "";
            foreach (string Col in Col_Selected)
            {

                if (MathV.IsStringInt(Col.Trim()) == 1 && Col.Trim()!= "")
                {
                    if (Convert.ToInt32(Col.Trim()) >= 0 && Convert.ToInt32(Col.Trim())<= Form1.S.dataGridView1.Columns.Count)
                    ColToShow = ColToShow+","+Form1.S.dataGridView1.Columns[Convert.ToInt32(Col.Trim())-1].Name;
                }
            }
            if (ColToShow != "")
            {
                ColToShow = ColToShow.Substring(1, ColToShow.Length - 1);
            }
            textBox_ColShow.Text = ColToShow;
        }
        string FindNAs(int ColNum)
        {
            int AllRowCounts = Form1.S.dataGridView1.RowCount;
            string WarningNAs = " ";
            int CountNA = 0;
            for (int i = 0; i < AllRowCounts; i++)
            {
                if (Form1.S.dataGridView1.Rows[i].Cells[ColNum].Value == null)
                {
                    WarningNAs = WarningNAs + "," + i.ToString();
                    CountNA++;
                }
                else if (Form1.S.dataGridView1.Rows[i].Cells[ColNum].Value.ToString().Trim() == "")
                {
                    WarningNAs = WarningNAs + "," + i.ToString();
                    CountNA++;
                }
                else
                {
                    WarningNAs = WarningNAs + ",NOEMPTY";
                }
            }
            return WarningNAs;
        }
        BigNumber [] VectorReadASBigNum(string ID, string WarningNAs)
        {
            //读单列
            int ColNum = 0;
            for (int i = 0; i < Form1.S.dataGridView1.ColumnCount; i++)
            {
                if (Form1.S.dataGridView1.Columns[i].Name == ID)
                {
                    ColNum = i;
                    break;
                }
            }
            int AllRowCounts = Form1.S.dataGridView1.RowCount - 1;
            char[] separator = { ',' };
            int IsNOTEmpty = 0;
            string Temp;
            foreach (char EachChar in WarningNAs)
            {
                if (EachChar == 'N')
                {
                    IsNOTEmpty++;
                }
            }
            string[] SkipRow = WarningNAs.Split(separator);
            int DataRows_Count = 0;
            int IsEmpty = 0;
            BigNumber [] DataChose = new BigNumber [IsNOTEmpty];
            for (int i = 0; i < AllRowCounts; i++)
            {
                IsEmpty = 0;
                foreach (string EachRow in SkipRow)
                {
                    if (EachRow.Trim() == i.ToString())
                    {
                        IsEmpty = 1;
                    }
                }
                if (IsEmpty == 0)
                {
                    Temp =Form1.S.dataGridView1.Rows[i].Cells[ColNum].Value.ToString();
                    //MessageBox.Show("第" + i + "行的变量的值为：" + Temp);
                    DataChose[DataRows_Count] = new BigNumber(Temp.Trim());
                    DataRows_Count++;
                }
            }
            return DataChose;

        }
        private void button_estimate_Click(object sender, EventArgs e)
        {
            string All_Col_Selected = textBox_Cols.Text;
            char[] separator = { ',' };
            string[] Col_Selected = All_Col_Selected.Split(separator);
            int [] Para_Esti_Col  = new int[Col_Selected.Length];
            int Counts = 0;
            foreach (string Col in Col_Selected)
            {

                if (MathV.IsStringInt(Col.Trim()) == 1 && Col.Trim()!= "")
                {
                    if (Convert.ToInt32(Col.Trim()) >= 0 && Convert.ToInt32(Col.Trim())<= Form1.S.dataGridView1.Columns.Count){
                    Para_Esti_Col[Counts] = Convert.ToInt32(Col.Trim());
                        Counts++;
                    }
                    else{
                        Para_Esti_Col[Counts]  = -100;
                        Counts++;
                    }
                }
            }
            for (int i = 0; i < Counts; i++)
            {
                if (Para_Esti_Col[i] >= 0)
                {

                }
            }
        }
    }
}
