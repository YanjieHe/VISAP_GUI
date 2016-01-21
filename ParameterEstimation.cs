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
        string FindNAs(string ID)
        {
            int ColNum = 0;
            for (int i = 0; i < Form1.S.dataGridView1.ColumnCount; i++)
            {
                if (Form1.S.dataGridView1.Columns[i].Name == ID)
                {
                    ColNum = i;
                }
            }
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
            string result = "";
            string[] WholeCI = new string[2];
            BigNumber Mean = new BigNumber("0"), Variance = new BigNumber("0");
            string DataNum = "";
            int DataNumCounts = 0;
            char[] separator = { ',' };
            int ColNum = 0;
            BigNumber[][] NumberSeries = new BigNumber[Form1.S.dataGridView1.Rows.Count - 1][];
            //定义了一个锯齿数组
            int BigNumTimes = 0;
            if (comboBox_Method.Text.Trim() == "均值" || comboBox_Method.Text.Trim() == "比例"){
            if (textBox_ColShow.Text.Trim() != "")
            {
                string [] AllColsToRead = textBox_ColShow.Text.Trim().Split(separator);
                result = result + "置信度：95%\r\n"+Form1.S.AdjustStr("变量名") +"\t" +Form1.S.AdjustStr("样本数")+"\t"+Form1.S.AdjustStr("均值") +  "\t" +Form1.S.AdjustStr("标准差") + "\t" + Form1.S.AdjustStr("置信上限") + "\t" + Form1.S.AdjustStr("置信下限") + "\r\n";
                int UseToCount = 0;
                foreach (string SingleCol in AllColsToRead)
                {
                    if (SingleCol.Trim() != "")
                    {
                        DataNumCounts = 0;
                        DataNum = FindNAs(SingleCol.Trim());
                        //MessageBox.Show("黑名单：" + DataNum);
                        foreach (char DataRecord in DataNum)
                        {
                            if (DataRecord == 'N')
                            {
                                DataNumCounts++;
                            }
                        }
                        UseToCount++;
                        for (int i = 0; i < Form1.S.dataGridView1.ColumnCount; i++)
                        {
                            if (Form1.S.dataGridView1.Columns[i].Name == SingleCol)
                            {
                                ColNum = i;
                                break;
                            }
                        }
                        //MessageBox.Show("BigNumTime = " + BigNumTimes + "SingleCol = " + SingleCol);
                        result = result + Form1.S.VariableNamePolish(Form1.S.dataGridView1.Columns[ColNum].HeaderCell.Value.ToString());
                        /*MessageBox.Show("result  = " + result);
                        int ii = 1;
                        foreach (BigNumber Num in VectorReadASBigNum(SingleCol.Trim(), FindNAs(SingleCol.Trim())))
                        {

                            MessageBox.Show("Num = " + Num+ "第"+ii+"次");
                            ii++;
                        }*/
                        NumberSeries[BigNumTimes] = VectorReadASBigNum(SingleCol.Trim(),FindNAs(SingleCol.Trim()));
                       
                        /*ii = 1;
                        foreach (BigNumber Num in NumberSeries[0])
                        {

                            MessageBox.Show("Num = " + Num + "第" + ii + "次");
                            ii++;
                        }*/
                        Mean = Stat.Mean(NumberSeries[BigNumTimes]);
                        result = result + "\t" + NumberSeries[BigNumTimes].Length.ToString().PadLeft(12, ' ') + "\t" + MathV.NumberPolish(Mean.ToString()).ToString().PadLeft(12, ' ') + "\t";
                        if (NumberSeries[BigNumTimes].Length == 1)
                        {
                            result = result + "NA".PadLeft(12, ' ') + "\t" + "NA".PadLeft(12, ' ') + "\t" +"NA".PadLeft(12, ' ');
                        }
                        else
                        {
                            Variance = Stat.Variance(NumberSeries[BigNumTimes]);
                            result = result +MathV.NumberPolish(Stat.Variance(NumberSeries[BigNumTimes]).Power(new BigNumber("0.5"), 30).ToString()).PadLeft(12, ' ');
                            WholeCI = Stat.CI1(Mean,Variance,new BigNumber("-1"),new BigNumber(NumberSeries[BigNumTimes].Length.ToString()),0.95,"two","Mean.Esti").Split(separator);
                            result = result +"\t"+MathV.NumberPolish(WholeCI[0])+"\t"+MathV.NumberPolish(WholeCI[1])+"\r\n";
                         }
                       
                        BigNumTimes++;
                    }
                }
                textBox_result.Text = result;
            }
            }
        }
    }
}
