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
    public partial class BayesPrediction : Form
    {
        public BayesPrediction()
        {
            InitializeComponent();
        }

        void BayesianPrediction()
        {
            double a_post = Convert.ToDouble(textBox_a.Text);
            double b_post = Convert.ToDouble(textBox_b.Text);
            string AllValues = textBox_values.Text;
            char[] separator = { ',' };
            string[] EachValue = AllValues.Split(separator);
            string AllInformation = "";
            double PredictProb ;
            double n = Convert.ToDouble(textBox_sample.Text);
            double y;
            double factor = 0;
            factor = Stat.Gamma(a_post + b_post) / (Stat.Gamma(a_post) * Stat.Gamma(b_post));
            AllInformation += Form1.S.AdjustStr("成功次数") + "\t" + Form1.S.AdjustStr("预测概率值") + "\r\n";
            for (int i = 0; i < EachValue.Length; i++)
            {
                if (EachValue[i].Trim() != "")
                {
                    y = Convert.ToDouble(EachValue[i].Trim());
                    PredictProb = factor * Stat.Gamma(n + 1) * Stat.Gamma(y + a_post) * Stat.Gamma(n - y + b_post) / (Stat.Gamma(y + 1) * Stat.Gamma(n - y + 1) * Stat.Gamma(a_post + b_post + n));
                    
                    AllInformation += Form1.S.AdjustStr(EachValue[i].Trim()) + "\t" + Form1.S.AdjustStr(MathV.NumberPolish(PredictProb.ToString())) + "\r\n";
                }
            }
            textBox_predict.Text = AllInformation;

        }
        private void button_predict_Click(object sender, EventArgs e)
        {
            BayesianPrediction();
        }
    }
}
