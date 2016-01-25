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
    public partial class NaiveBayesian : Form
    {
        public NaiveBayesian()
        {
            InitializeComponent();
            textBox_ChosenCols.Text = RenewCol();
        }
        int IdentifyNARow(int RowsNum)
        {
            for (int i = 0; i < dataGridView_subset.Columns.Count; i++)
            {
                if (dataGridView_subset.Rows[RowsNum].Cells[i].Value.ToString().Trim() == "")
                {
                    return 0;
                    //0即为有空格
                }
            }
            return 1;
            //1为没有空格
        }
        static double FindElement(string SingleStr, string[] StrArray)
        {
            double counts = 0;
            foreach (string EachStr in StrArray)
            {
                if (EachStr == SingleStr)
                {
                    counts++;
                }
            }
            return counts;
        }
        static double Search(string Record, string[] Str, double[] Probs)
        {
            for (int i = 0; i < Str.Length; i++)
            {
                if (Str[i] == Record)
                {
                    return Probs[i];
                }
            }
            return -1;
        }
        void BayesClassification()
        {
            string[][] RawData = new string[dataGridView_subset.Columns.Count][];
            string[][] Classification = new string[dataGridView_subset.Columns.Count][];
            int CountNA = 0;
            for (int i = 0; i < dataGridView_subset.Columns.Count; i++)
            {
                RawData[i] = new string[dataGridView_subset.Rows.Count - 1];
                //MessageBox.Show(Data[i].Length.ToString());
            }
            
                for (int i = 0; i < dataGridView_subset.Rows.Count - 1; i++)
                {
                    if (IdentifyNARow(i) == 1)
                    {
                        for (int j = 0; j < dataGridView_subset.Columns.Count;j++)
                        {
                            //MessageBox.Show("i = " + i + "j = " + j);
                            RawData[j][i] = dataGridView_subset.Rows[i].Cells[j].Value.ToString().Trim();
                        }
                    }
                    else
                    {
                        CountNA++;
                        for (int j = 0; j < dataGridView_subset.Columns.Count; j++)
                        {
                            RawData[j][i] = "NA";
                            //缺失值全部记为NA
                        }
                    }
                }
                string[][] Data = new string[dataGridView_subset.Columns.Count][];
                for (int i = 0; i < dataGridView_subset.Columns.Count; i++)
                {
                    Data[i] = new string[RawData[i].Length - CountNA];
                }
                int DataRowsCount = 0;
                for (int i = 0; i < dataGridView_subset.Rows.Count - 1; i++)
                {
                    if (RawData[0][i] != "NA")
                    {
                        for (int j = 0; j < dataGridView_subset.Columns.Count; j++)
                        {
                            //MessageBox.Show("i = " + i.ToString() + "j = " + j.ToString());
                            Data[j][DataRowsCount] = RawData[j][i];
                        }
                             DataRowsCount++;
                    } 
                }
                int ClassCount = 0;
                for (int i = 0; i < dataGridView_subset.Columns.Count; i++)
                {
                    textBox_result.AppendText("第" + (i + 1) + "项特征内的各个类别:\r\n");
                    Classification[i] = Data[i].Distinct().ToArray();
                    foreach (string SingleStr in Classification[i])
                    {
                        textBox_result.AppendText(SingleStr + "  ");
                        ClassCount++;
                    }
                    textBox_result.AppendText("\r\n");
                }
                string[] AllClassi = new string[ClassCount];
                double[] EachProbs = new double[ClassCount];
                ClassCount = 0;
                for (int i = 0; i < 3; i++)
                {
                    foreach (string EachStr in Classification[i])
                    {
                        textBox_result.AppendText("P("+EachStr + ") = " + FindElement(EachStr, Data[i]) / Convert.ToDouble(Data[i].Length)+"\r\n");
                        AllClassi[ClassCount] = EachStr;
                        EachProbs[ClassCount] = FindElement(EachStr, Data[i]) / Convert.ToDouble(Data[i].Length);
                        ClassCount++;
                    }
                }
                double count = 0;
                int TotalTimes = 0;
                int InterestCol = Convert.ToInt32(textBox_Class.Text) - 1;
                for (int j = 0; j < dataGridView_subset.Columns.Count; j++)
                {
                    if (j!= InterestCol)
                    {
                        foreach (string EachStr in Classification[j])
                        {
                            foreach (string EachDis in Classification[InterestCol])
                            {
                                count = 0;
                                for (int i = 0; i < Data[InterestCol].Length; i++)
                                {
                                    if (Data[InterestCol][i].Trim() == EachDis)
                                    {
                                        if (Data[j][i].Trim() == EachStr)
                                        {
                                            count++;
                                        }
                                    }
                                }
                                textBox_result.AppendText("P(" + EachStr + "|" + EachDis + ") = " + (count / FindElement(EachDis, Data[InterestCol])).ToString() + "\r\n");
                                TotalTimes++;
                            }
                        }
                    }
                }
                textBox_result.AppendText("所有类别组合数量总计：" + TotalTimes.ToString());
                string[] str = new string[TotalTimes];
                double[] Probs = new double[TotalTimes];
                int TimesStr = 0;
                for (int j = 0; j < dataGridView_subset.Columns.Count; j++)
                {
                    if (j != InterestCol)
                    {
                        foreach (string EachStr in Classification[j])
                        {
                            foreach (string EachDis in Classification[InterestCol])
                            {
                                count = 0;
                                for (int i = 0; i < Data[InterestCol].Length; i++)
                                {
                                    if (Data[InterestCol][i].Trim() == EachDis)
                                    {
                                        if (Data[j][i].Trim() == EachStr)
                                        {
                                            count++;
                                        }
                                    }
                                }
                                str[TimesStr] = EachStr + "|" + EachDis;
                                Probs[TimesStr] = count / FindElement(EachDis, Data[InterestCol]);
                                TimesStr++;
                            }
                        }
                    }
                }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BayesClassification();
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
        void ImportOriginData()
        {
            DataTable OriginDT = new DataTable();
            DataTable NewDT = new DataTable();
            OriginDT = Form1.S.dataGridView1.DataSource as DataTable;
            if (OriginDT == null)
            {
                OriginDT = Form1.table;
            }
            DataColumn AddCol = new DataColumn();
            char[] separator = { ',' };
            string UseToAddCol = "";
            int AddTimes = 1;
            string[] ColWanted =(textBox_ChosenCols.Text+","+textBox_Class.Text).Split(separator);
            if (OriginDT != null)
            {
                for (int q = 0; q < OriginDT.Rows.Count; q++)
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
        private void button_refresh_Click(object sender, EventArgs e)
        {
            textBox_ChosenCols.Text=RenewCol();
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            ImportOriginData();
        }
    }
}
