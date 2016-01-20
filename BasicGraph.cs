using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
        static string RegulateAll(double dMin, double dMax, int iMaxAxisNum)
        {
            if (iMaxAxisNum < 1 || dMax < dMin)
                return "NA";

            double dDelta = dMax - dMin;
            if (dDelta < 1.0) //Modify this by your requirement.
            {
                dMax += (1.0 - dDelta) / 2.0;
                dMin -= (1.0 - dDelta) / 2.0;
            }
            dDelta = dMax - dMin;

            int iExp = (int)(Math.Log(dDelta) / Math.Log(10.0)) - 2;
            double dMultiplier = Math.Pow(10, iExp);
            double[] dSolutions = new double[] { 1, 2, 2.5, 5, 10, 20, 25, 50, 100, 200, 250, 500 };
            int i;
            for (i = 0; i < dSolutions.Length; i++)
            {
                double dMultiCal = dMultiplier * dSolutions[i];
                if (((int)(dDelta / dMultiCal) + 1) <= iMaxAxisNum)
                {
                    break;
                }
            }

            double dInterval = dMultiplier * dSolutions[i];

            double dStartPoint = ((int)Math.Ceiling(dMin / dInterval) - 1) * dInterval;
            int iAxisIndex;
            double dEndPoint = 0;
            for (iAxisIndex = 0; ; iAxisIndex++)
            {
                if (dStartPoint + dInterval * iAxisIndex > dMax)
                {
                    dEndPoint = dStartPoint + dInterval * Convert.ToDouble(iAxisIndex);
                    break;
                }
            }
            return dStartPoint.ToString() + "," + dEndPoint.ToString();
        }
        double[] VectorRead(string ID,string WarningNAs)
        {
            //读单列
            int ColNum = 0;
            for(int i = 0; i < dataGridView_subset.ColumnCount;i++){
                if (dataGridView_subset.Columns[i].Name == ID)
                {
                    ColNum = i;
                    break;
                }
            }
            int AllRowCounts = dataGridView_subset.RowCount - 1;
            /*string[] DataReady = new string[AllRowCounts];
            string EmptyRowStr = "";
            int CountEmpty = 0;
            for (int i = 0;i < AllRowCounts ;i++){
                if (dataGridView_subset.Rows[i].Cells[ColNum].Value == null){
                    EmptyRowStr = EmptyRowStr + "," + i.ToString();
                    CountEmpty ++;
                }
                else if (dataGridView_subset.Rows[i].Cells[ColNum].Value.ToString().Trim() == ""){
                    EmptyRowStr = EmptyRowStr + "," + i.ToString();
                    CountEmpty ++;
                }
            }*/
            //int CountEmpty = 0;
            char [] separator = {','};
            int IsNOTEmpty =0;
            string Temp;
            foreach(char EachChar in WarningNAs ){
                if (EachChar == 'N')
                {
                    IsNOTEmpty++;
                }
            }
            //MessageBox.Show("IsNotEmpty = " + IsNOTEmpty);
            //MessageBox.Show("WarningNAs: " + WarningNAs);
            string [] SkipRow = WarningNAs.Split(separator);
            int DataRows_Count = 0;
            int IsEmpty = 0;
            double[] DataChose = new double[IsNOTEmpty];
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
                    Temp = dataGridView_subset.Rows[i].Cells[ColNum].Value.ToString();
                    //MessageBox.Show("第" + i + "行的变量的值为：" + Temp);
                    DataChose[DataRows_Count] = Convert.ToDouble(Temp.Trim());
                   DataRows_Count++;
                }
            }
            return DataChose;
           
        }
        string FindNAs(string ID_x, string ID_y)
        {
            int ColNum_x = 0,ColNum_y = 0;
            for (int i = 0; i < dataGridView_subset.ColumnCount; i++)
            {
                if (dataGridView_subset.Columns[i].Name == ID_x)
                {
                    ColNum_x = i;
                }
                if (dataGridView_subset.Columns[i].Name == ID_y)
                {
                    ColNum_y = i;
                }
            }
            int AllRowCounts = dataGridView_subset.RowCount;
            string WarningNAs = " ";
            int CountNA = 0;
            for (int i = 0; i < AllRowCounts; i++)
            {
                if (dataGridView_subset.Rows[i].Cells[ColNum_x].Value == null)
                {
                    WarningNAs = WarningNAs + "," +i.ToString();
                    CountNA++;
                }
                else if (dataGridView_subset.Rows[i].Cells[ColNum_x].Value.ToString().Trim() == "")
                {
                    WarningNAs = WarningNAs + "," + i.ToString();
                    CountNA++;
                }
                else if (dataGridView_subset.Rows[i].Cells[ColNum_y].Value == null)
                {
                    WarningNAs = WarningNAs + "," + i.ToString();
                    CountNA++;
                }
                else if (dataGridView_subset.Rows[i].Cells[ColNum_y].Value.ToString().Trim() == "")
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
        void plot_basic()
        {
            string Var_x = comboBox_x.Text;
            string Var_y = comboBox_y.Text;
            string plot_choose = comboBox_type.Text;
            if (plot_choose == "散点图")
            {
                Series series = new Series("散点图");
                series.ChartType = SeriesChartType.Point;
                series.BorderWidth = 3;
                series.MarkerSize = 6;
                string ColorToUse = "";
                ColorToUse = "FireBrick";
                series.Color = Color.FromName(ColorToUse);
                string BlackList = FindNAs(Var_x, Var_y);
                double[] X_Points = VectorRead(Var_x,BlackList);
                double[] Y_Points = VectorRead(Var_y,BlackList);
                for (int i = 0; i < X_Points.Length; i++)
                {
                    series.Points.AddXY(X_Points[i], Y_Points[i]);
                }
                char[] separator = { ',' };
                string[] MinAndMax = RegulateAll(MathV.MinDouble(X_Points), MathV.MaxDouble(X_Points), 6).Split(separator);
                chart_basic.Series.Add(series);
                var XAxis = chart_basic.ChartAreas[0].AxisX;
                XAxis.Maximum = Convert.ToDouble(MinAndMax[1]);
                XAxis.Minimum = Convert.ToDouble(MinAndMax[0]);
            }
        }
        void refresh_Combox()
        {
            comboBox_x.Items.Clear();
            comboBox_y.Items.Clear();
            int ColCounts = dataGridView_subset.ColumnCount;
            for (int i = 0 ;i < ColCounts ;i++){
                comboBox_x.Items.Add(dataGridView_subset.Columns[i].Name);
                comboBox_y.Items.Add(dataGridView_subset.Columns[i].Name);
            }
           
        }
        private void button_import_Click(object sender, EventArgs e)
        {
            ImportOriginData();
            refresh_Combox();
           
        }

        private void button_AddPlot_Click(object sender, EventArgs e)
        {
            plot_basic();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            chart_basic.Series.Clear();
        }
    }
}
