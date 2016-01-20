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

namespace 绘图1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public double Max(double[] NumberSeries)
        {
            double MaxValue = NumberSeries[0];
            for (int i = 0; i < NumberSeries.Length; i++)
            {
                if (MaxValue < NumberSeries[i])
                {
                    MaxValue = NumberSeries[i];
                }
            }
            return MaxValue;
        }
        public double Min(double[] NumberSeries)
        {
            double MinValue = NumberSeries[0];
            for (int i = 0; i < NumberSeries.Length; i++)
            {
                if (MinValue > NumberSeries[i])
                {
                    MinValue = NumberSeries[i];
                }
            }
            return MinValue;
        }
        private void chart1_Click(object sender, EventArgs e)
        {

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
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_Choose.Text == "1")
            {
                chart1.Series.Clear();
                Series series = new Series("随便画的函数图");
                series.ChartType = SeriesChartType.Spline;
                series.BorderWidth = 3;
                double y;
                for (double i = -5; i < 5; i = i + 0.1)
                {
                    y = (1 / Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(i, 2) / 2);
                    series.Points.AddY(y);
                }

                chart1.Series.Add(series);
            }
            if (textBox_Choose.Text == "2")
            {
                chart1.Series.Clear();
                Series series = new Series("随便画的函数图");
                series.ChartType = SeriesChartType.Point;
                series.BorderWidth = 3;
                series.MarkerSize = 6;
                int SeriesPainted = 0;
                string ColorToUse = "";
                string[] UsedColor = new string[140];
                if (SeriesPainted == 0)
                {
                    ColorToUse = "FireBrick";
                }
                UsedColor[SeriesPainted] = ColorToUse;
                //SeriesPainted++;
                series.Color = Color.FromName(ColorToUse);
                double[] X_Points = new double[100];
                double[] Y_Points = new double[100];
                int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
                Random p1 = new Random(seed);
                for (int i = 0; i < 100; i++)
                {
                    X_Points[i] = p1.NextDouble() * 100;
                }
                for (int i = 0; i < 100; i++)
                {
                    Y_Points[i] = p1.NextDouble() * 100;
                }
                for (int i = 0; i < 100; i++)
                {
                    series.Points.AddXY(X_Points[i], Y_Points[i]);
                }

                char[] separator = { ',' };
                string[] MinAndMax = RegulateAll(Min(X_Points),Max(X_Points),6).Split(separator);
                chart1.Series.Add(series);
                //chart1.Series[0].Points[0].IsValueShownAsLabel = true;
                //chart1.Series[SeriesPainted].= "243";
                var XAxis = chart1.ChartAreas[0].AxisX;
                XAxis.Maximum = Convert.ToDouble(MinAndMax[1]);
                XAxis.Minimum =Convert.ToDouble(MinAndMax[0]);
                // XAxis.CustomLabels.Add("0".ToOADate(), EndPos.ToOADate(), StartMonthPos.ToString("MMMM"), 0, LabelMarkStyle.None)
                //chart1.Series[0].Points[0].Label = "23";
                //chart1.Series[0]. = false ;
                //chart1.ChartAreas["ChartArea0"].AxisX.LabelStyle.Format = "";
                //chart1.Series[0].Points.AddXY("test", 0);
                /*CustomLabel myCustomLabel1 = new CustomLabel();
                myCustomLabel1.Text = "0";
                myCustomLabel1.FromPosition = 0.5;
                myCustomLabel1.ToPosition = 2.5;
                myCustomLabel1.RowIndex = 0;
                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(myCustomLabel1);
                CustomLabel myCustomLabel2 = new CustomLabel();
                myCustomLabel2.Text = "50";
                myCustomLabel2.FromPosition = 1.5;
                myCustomLabel2.ToPosition = 2.5;
                myCustomLabel2.RowIndex = 1;//后面依次递增
                myCustomLabel2.LabelMark = LabelMarkStyle.Box;
                CustomLabel myCustomLabel3 = new CustomLabel();
                myCustomLabel3.Text = "100";
                myCustomLabel3.FromPosition = 2.5;
                myCustomLabel3.ToPosition = 2.5;
                myCustomLabel3.RowIndex = 2;//后面依次递增
                myCustomLabel3.LabelMark = LabelMarkStyle.Box;
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(myCustomLabel1);
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(myCustomLabel2);
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(myCustomLabel3);*/
                /*chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(0, 5, "0");
                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(25, 30, "25");
                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(45,50, "50");
                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(75, 80, "75");
                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(95, 100, "100");*/
                //chart1.ChartAreas[0].AxisX.LabelStyle.Format
            }
        }
    }
}
