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
    public partial class BayesEstimation : Form
    {
        public BayesEstimation()
        {
            InitializeComponent();
            trackBar1.Maximum = 20;
            string a = textBox_a.Text;
            string b = textBox_b.Text;
            if (a.Trim() != "" && b.Trim() != "" && a != null && b != null)
            {
                DrawPrior(Convert.ToDouble(a), Convert.ToDouble(b));
            }
        }
        double Gammln(double value){
        double [] temp = new double [3];
        int i;
        double [] data = new double [] {76.180091729471457, -86.505320329416776, 24.014098240830911, -1.231739572450155, 0.001208650973866179, -0.000005395239384953};
        temp[0] = value;
        temp[1] = value + 5.5;
        temp[1] = temp[1] - (value + 0.5) * Math.Log(temp[1]);
        temp[2] = 1.0000000001900149;
        for (i = 0;i < 6;i++){
            temp[0] += 1;
            temp[2] += data[i] / temp[0];
        }
        temp[0] = Math.Log(2.5066282746310007 * temp[2] / value) - temp[1];
        return temp[0];
    }
    
    double Gamma_Call(double value) {
        if (value < 1 ){
            return Math.Exp(Gammln(value));
        }
        else if(value == 1){
            return 1;
        }
        value -= 1;
        return  value * Gamma_Call(value);
    }
    double Gamma(double value ){
        //概率统计里的gamma函数.注意value的值不要大过171且不能且当value<=0时《sin(value*pi)<>0
        double  ret = 0;
        //bool IsTrue;
        if (value > 171 ){
            //IsTrue = false;
            MessageBox.Show("输入伽玛函数的值不可大于171！");
            }
        else if (value > 0)
        {
            ret = Gamma_Call(value);
            //MessageBox.Show(ret.ToString());
        }
        else
        {
            ret = Math.Sin(Math.PI * value);

            if (ret == 0)
            {
            }
            else
            {
                ret = Math.PI / ret;
                ret = ret / Gamma_Call(1 - value);
            }
        }
        return ret;
    }
        
        void DrawPrior(double a, double b)
        {
            chart_prior.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.MarkerSize = 6;
            series.LegendText = "Beta先验";
            double MaxValue = 0;
            //series.Color = Color.Blue;
            double y = 0;
            //MessageBox.Show("Gamma(a + b)"+Gamma(a + b));
            //MessageBox.Show("Gamma(a)" + Gamma(a) + "Gamma(b)" + Gamma(b));
            double factor = Gamma(a + b) / (Gamma(a) * Gamma(b));
            for (double  x = 0; x <= 1; x = x + 0.01)
            {
                
                y = factor * Math.Pow(x, a - 1) * Math.Pow(1-x, b - 1);
                if (x == 0)
                {
                    MaxValue = y;
                }
                else
                {
                    if (MaxValue < y)
                    {
                        MaxValue = y;
                    }
                }
                series.Points.AddXY(x, y);
                //MessageBox.Show("x = " + x.ToString() + " y = " + y.ToString());
               
            }
                chart_prior.Series.Add(series);
                var XAxis = chart_prior.ChartAreas[0].AxisX;
                XAxis.Maximum = 1;
                XAxis.Minimum =0;
                var YAxis = chart_prior.ChartAreas[0].AxisY;
                YAxis.Maximum = Math.Ceiling(MaxValue);
                YAxis.Minimum = 0;
                textBox_PriorMean.Text = MathV.round((a / (a + b)).ToString(),4,0);
                textBox_PriorVariance.Text = MathV.round(((a * b) / ((a + b) * (a + b) * (a + b + 1))).ToString(),4,0);
        }
        private void BayesEstimation_Load(object sender, EventArgs e)
        {
           
        }

        private void button_GeneratePrior_Click(object sender, EventArgs e)
        {
            string a = textBox_a.Text;
            string b = textBox_b.Text;
            if (a.Trim() != "" && b.Trim() != "" && a != null && b != null)
            {
                DrawPrior(Convert.ToDouble(a), Convert.ToDouble(b));
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            int Position = trackBar1.Value;
            double a = Convert.ToDouble(Position + 1);
            double b = Convert.ToDouble(20-Position + 1);
            textBox_a.Text = a.ToString();
            textBox_b.Text = b.ToString();
            DrawPrior(a, b);
        }
    }
}
