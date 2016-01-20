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
        public static int SeriesCounts = 0;
        //用于计算图上序列的数量
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
        public static string ChooseColor(string[] UsedColor)
        {
            string[] AllColor = new string[] { "Azure", "NavajoWhite", "Aquamarine", "WhiteSmoke", "MistyRose", "SpringGreen", "Khaki", "Thistle", "DarkKhaki", "Purple", "Beige", "Gainsboro", "DarkGreen", "Orange", "Turquoise", "Lime", "FloralWhite", "Black", "DarkSeaGreen", "Peru", "PaleTurquoise", "RoyalBlue", "Linen", "Tan", "Tomato", "Yellow", "PowderBlue", "PapayaWhip", "Fuschia", "DarkSalmon", "LightYellow", "DeepPink", "Plum", "LightSteelBlue", "DarkGray", "Cornsilk", "DarkBlue", "Chartreuse", "Gray", "Brown", "OldLace", "LemonChiffon", "MediumAquamarine", "Snow", "MediumSpringGreen", "LawnGreen", "PaleVioletRed", "Chocolate", "DimGray", "Sienna", "Gold", "LightCyan", "BurlyWood", "DarkViolet", "Violet", "Orchid", "Moccasin", "IndianRed", "Red", "SlateBlue", "ForestGreen", "Lavender", "MidnightBlue", "LightGoldenrodYellow", "PaleGreen", "HotPink", "Magenta", "DarkRed", "Teal", "LightSkyBlue", "MediumOrchid", "Firebrick", "GreenYellow", "PaleGoldenrod", "BlueViolet", "LightGreen", "DarkTurquoise", "MediumSlateBlue", "Pink", "Wheat", "LightSalmon", "DarkSlateGray", "MediumBlue", "Honeydew", "LimeGreen", "CornflowerBlue", "SlateGray", "DarkCyan", "Navy", "Bisque", "DarkOrange", "MediumSeaGreen", "OrangeRed", "AliceBlue", "MintCream", "OliveDrab", "CadetBlue", "LavenderBlush", "Blue", "Green", "LightSlateGray", "SandyBrown", "MediumVioletRed", "MediumTurquoise", "Cyan", "White", "Crimson", "DarkGoldenrod", "Seashell", "DarkMagena", "AntiqueWhite", "DarkSlateBlue", "LightBlue", "LightPink", "BlanchedAlmond", "DarkOliveGreen", "DarkOrchid", "Salmon", "SkyBlue", "SaddleBrown", "Silver", "Aqua", "DeepSkyBlue", "RosyBrown", "GhostWhite", "LightGray", "SeaGreen", "Maroon", "PeachPuff", "SteelBlue", "MediumPurple", "Ivory", "DodgerBlue", "Indigo", "Coral", "Olive", "YellowGreen", "LightSeaGreen", "LightCoral", "Goldenrod", };
            //随机扰乱过的所有颜色
            int SameColor;
            string ColorChosen;
            foreach (string EachColor in AllColor)
            {
                SameColor = 0;
                foreach (string EveryColor in UsedColor)
                {
                    if (EachColor == EveryColor)
                    {
                        SameColor++;
                    }
                }
                if (SameColor == 0)
                {
                    ColorChosen = EachColor;
                    return EachColor;
                }
            }
            return "Black";
            //这种情况基本不可能出现。。。
        }
        string[] UsedColor = new string[140];
        //记录所有用过的颜色
        void plot_basic()
        {
            string Var_x = comboBox_x.Text;
            string Var_y = comboBox_y.Text;
            string plot_choose = comboBox_type.Text;
            if (plot_choose == "散点图" || plot_choose == "折线图" || plot_choose == "气泡图")
            {
                Series series = new Series();
                if (plot_choose == "散点图")
                {
                    series = new Series("散点图" + SeriesCounts);
                    series.ChartType = SeriesChartType.Point;
                }
                else if (plot_choose == "折线图")
                {
                    series = new Series("折线图"+SeriesCounts);
                    series.ChartType = SeriesChartType.Line;
                }
                else if (plot_choose == "气泡图")
                {
                    series = new Series("气泡图" + SeriesCounts);
                    series.ChartType = SeriesChartType.Bubble;
                }
                series.BorderWidth = 3;
                series.MarkerSize = 6;
                series.MarkerStyle = MarkerStyle.Circle;
                string ColorToUse = "";
                if (SeriesCounts == 0)
                {
                    ColorToUse = "FireBrick";
                    UsedColor[SeriesCounts] ="FireBrick";
                }
                else if (SeriesCounts == 1)
                {
                    ColorToUse = "MidnightBlue";
                    UsedColor[SeriesCounts] ="MidnightBlue";
                }
                else if (SeriesCounts > 2 & SeriesCounts<140)
                {
                    ColorToUse = ChooseColor(UsedColor);
                    UsedColor[SeriesCounts] = ColorToUse;
                }
                else
                {
                    ColorToUse = "Black";
                }
                
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
                SeriesCounts++;
                var XAxis = chart_basic.ChartAreas[0].AxisX;
                XAxis.Maximum = Convert.ToDouble(MinAndMax[1]);
                XAxis.Minimum = Convert.ToDouble(MinAndMax[0]);
                XAxis.MajorGrid.Enabled = false;
                var YAxis = chart_basic.ChartAreas[0].AxisY;
                YAxis.MajorGrid.Enabled = false;
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
            SeriesCounts = 0;
            for (int i = 0; i < 140; i++)
            {
                UsedColor[i] = " ";
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            bool isSave = true;
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Title = "图片保存";
            saveImageDialog.Filter = @"jpeg|*.jpg|bmp|*.bmp|gif|*.gif";

            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveImageDialog.FileName.ToString();

                if (fileName != "" && fileName != null)
                {
                    string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();

                    System.Drawing.Imaging.ImageFormat imgformat = null;

                    if (fileExtName != "")
                    {
                        switch (fileExtName)
                        {
                            case "jpg":
                                imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case "bmp":
                                imgformat = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                            case "gif":
                                imgformat = System.Drawing.Imaging.ImageFormat.Gif;
                                break;
                            default:
                                MessageBox.Show("只能存取为: jpg,bmp,gif 格式");
                                isSave = false;
                                break;
                        }

                    }

                    //默认保存为JPG格式   
                    if (imgformat == null)
                    {
                        imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    }

                    if (isSave)
                    {
                        try
                        {
                            chart_basic.SaveImage(fileName, imgformat);
                            //MessageBox.Show("图片已经成功保存!");   
                        }
                        catch
                        {
                            MessageBox.Show("保存失败,你还没有截取过图片或已经清空图片!");
                        }
                    }

                }

            }   
        }
    }
}
