using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace 统计图形界面1
{
    public partial class FunctionPlot : Form
    {
        public FunctionPlot()
        {
            InitializeComponent();
        }
        private object ComplierCode(string expression)
        {
            string code = WrapExpression(expression);

            CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();

            //编译的参数
            CompilerParameters compilerParameters = new CompilerParameters();

            compilerParameters.CompilerOptions = "/t:library";
            compilerParameters.GenerateInMemory = true;
            //开始编译
            CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, code);
            if (compilerResults.Errors.Count > 0)
                throw new Exception("编译出错！");

            Assembly assembly = compilerResults.CompiledAssembly;
            Type type = assembly.GetType("ExpressionCalculate");
            MethodInfo method = type.GetMethod("Calculate");
            return method.Invoke(null, null);
        }

        private string WrapExpression(string expression)
        {
            string code = @"
                using System;
                class ExpressionCalculate
                {
            
                    public static object Calculate()
                    {
                        {0};
                    }
                }
            ";

            return code.Replace("{0}", expression);
        }
        public static string StringReplace(string str, string toRep, string strRep)
        {
            StringBuilder sb = new StringBuilder();

            int np = 0, n_ptmp = 0;

            for (; ; )
            {
                string str_tmp = str.Substring(np);
                n_ptmp = str_tmp.IndexOf(toRep);

                if (n_ptmp == -1)
                {
                    sb.Append(str_tmp);
                    break;
                }
                else
                {
                    sb.Append(str_tmp.Substring(0, n_ptmp)).Append(strRep);
                    np += n_ptmp + toRep.Length;
                }
            }
            return sb.ToString();

        }
        void  FuncRead(double StartValue, double EndValue,double Step)
        {
           /* string PlotCode = @"string Answer = "";";
            PlotCode += "for (double i = StartValue;i < EndValue;i = i + Step)";
            PlotCode += "{Answer += ";
            PlotCode +=," + Expression;}";*/
            string PlotCode = @"
                              string Answer = {4};
                               for (double x = {0};x < {1};x = x + {2}){
                               Answer += "","" + ({3}).ToString();}return Answer;
        
            ";
            PlotCode = PlotCode.Replace("{0}", StartValue.ToString());
            PlotCode = PlotCode.Replace("{1}", EndValue.ToString());
            PlotCode = PlotCode.Replace("{2}", Step.ToString());
            PlotCode = PlotCode.Replace("{4}", "\"\"");
            string FunctionExpression = textBox_Function.Text;
            FunctionExpression = FunctionExpression.Trim().ToLower();
            FunctionExpression = StringReplace(FunctionExpression, "sqrt", "Math.Sqrt");
            FunctionExpression = StringReplace(FunctionExpression, "pow", "Math.Pow");
            FunctionExpression = StringReplace(FunctionExpression, "exp", "Math.Exp");
            FunctionExpression = StringReplace(FunctionExpression, "log", "Math.Log10");
            FunctionExpression = StringReplace(FunctionExpression, "sin", "Math.Sin");
            FunctionExpression = StringReplace(FunctionExpression, "cos", "Math.Cos");
            FunctionExpression = StringReplace(FunctionExpression, "tan", "Math.Tan");
            FunctionExpression = StringReplace(FunctionExpression, "pi", "Math.PI");
            //MessageBox.Show("函数表达式 = " + FunctionExpression);
            PlotCode = PlotCode.Replace("{3}", FunctionExpression.ToString());
            //MessageBox.Show("PlotCode = " + PlotCode);
            string Result = "";
            
            try
            {
                string expression = PlotCode.Trim();
                //MessageBox.Show("Expression = " + expression);
                Result = this.ComplierCode(expression).ToString();
                //MessageBox.Show("Result = " + Result);
                Result = Result.Substring(1, Result.Length - 1);
                FuncPlotting(Result);
                

            }
            catch (Exception ex)
            {
                //txtResult.Text = ex.Message;
                MessageBox.Show("出错");
            }
        }
        void FuncPlotting(string Result)
        {
            char[] separator = { ',' };
            string[] AllResults = Result.Split(separator);
            Series series = new Series();
            series.ChartType = SeriesChartType.Spline;
            double StartValue = Convert.ToDouble(textBox_StartValue.Text);
            double EndValue = Convert.ToDouble(textBox_EndValue.Text);
            double Step = Convert.ToDouble(textBox_Step.Text);
            for (double x = StartValue ,i = 0; x < EndValue; x = x + Step,i++)
            {
                series.Points.AddXY(x, Convert.ToDouble(AllResults[Convert.ToInt32(i)]));
            }
            chart_func.Series.Add(series);
        }
        private void button_plot_Click(object sender, EventArgs e)
        {
            FuncRead(Convert.ToDouble(textBox_StartValue.Text), Convert.ToDouble(textBox_EndValue.Text), Convert.ToDouble(textBox_Step.Text));
        }

        private void button_ImportReport_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                chart_func.SaveImage(ms, ChartImageFormat.Jpeg);
                Bitmap m = new Bitmap(ms);
                //复制到粘贴板
                Clipboard.SetImage(m);
            }
            //这种做法会使得粘帖板上原有内容丢失
            if (Form1.S.ReportIsOn == 0)
            {
                Form1.S.rtb.Paste();
            }
            else
            {


                //Form1.S.richTextBox_onForm1.Size = new Size(200, 163);
                try
                {
                    Form1.S.rtb.Rtf = ReportForm.ReportText.richTextBox1.Rtf;
                    Form1.S.rtb.Paste();
                    ReportForm.ReportText.richTextBox1.Rtf = Form1.S.rtb.Rtf;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart_func.Series.Clear();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                chart_func.SaveImage(ms, ChartImageFormat.Jpeg);
                Bitmap m = new Bitmap(ms);
                //复制到粘贴板
                Clipboard.SetImage(m);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
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
                            chart_func.SaveImage(fileName, imgformat);
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
