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
    public partial class HPD : Form
    {
        public HPD()
        {
            InitializeComponent();
        }
        void CalHPD (string percentiles,double a,double b){
            //计算Beta函数等尾置信集
            textBox_result.Text = "";
            char[] separator = { ',' };
            string[] EachPercentile = percentiles.Split(separator);
            string AllInformation ="";
            double y;
            double NumPer ;
            AllInformation = "先验分布：" + "(" + BayesEstimation.Bayes.textBox_a.Text + "," + BayesEstimation.Bayes.textBox_b.Text+")"+"\r\n";
            AllInformation += "后验分布：" + "(" + a.ToString() + "," + b.ToString() + ")"+"\r\n";
            AllInformation += "试验成功次数：" +BayesEstimation.Bayes.textBox_success.Text+ "\t" + "试验失败次数：" + BayesEstimation.Bayes.textBox_failure.Text+"\r\n";
            byte[] sarr = System.Text.Encoding.Default.GetBytes("试验成功次数：" + BayesEstimation.Bayes.textBox_success.Text + "\t" + "试验失败次数：" + BayesEstimation.Bayes.textBox_failure.Text);
            for (int m = 0; m < sarr.Length; m++)
            {
                AllInformation += "-";
            }
            AllInformation += "\r\n";
             for (int i = 0; i < EachPercentile.Length; i++)
            {
                 if (EachPercentile[i].Trim() != "")
                  {
                     AllInformation += Form1.S.AdjustStr(EachPercentile[i]+"分位数")+"\t";
                  }
             }
            AllInformation += "\r\n";
            for (int i = 0; i < EachPercentile.Length; i++)
            {
                if (EachPercentile[i].Trim() != "")
                {
                    NumPer = Convert.ToDouble(EachPercentile[i]);
                    y = Stat.BetaUa(1-NumPer, a, b);
                    AllInformation +=   Form1.S.AdjustStr(MathV.NumberPolish(y.ToString()))+"\t";
                }
            }
            textBox_result.Text = AllInformation + "\r\n"; 

        }
        private void button_HPDsummary_Click(object sender, EventArgs e)
        {
            string a = BayesEstimation.Bayes.textBox_a.Text;
            string b = BayesEstimation.Bayes.textBox_b.Text;
            if (a.Trim() != "" && b.Trim() != "" && a != null && b != null)
            {
                string success = BayesEstimation.Bayes.textBox_success.Text;
                string failure = BayesEstimation.Bayes.textBox_failure.Text;
                if (success.Trim() != "" && failure.Trim() != "" && success != null && failure != null)
                {
                    CalHPD(textBox_percentile.Text, Convert.ToDouble(a) + Convert.ToDouble(success), Convert.ToDouble(b) + Convert.ToDouble(failure));
                }
            }
        }
    }
}
