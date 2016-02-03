using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Web.UI; 
//using Microsoft.Office.Interop.Excel;  
using System.Data.OleDb;

namespace 整理生成报表
{
    
        
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
     

 /// <summary>
        /// Excel数据导入方法
        /// 作者:lhxhappy
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dgv"></param>
        public void ExcelToDataGridView(string filePath,DataGridView dgv)
        {
            //根据路径打开一个Excel文件并将数据填充到DataSet中
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + filePath + ";Extended Properties ='Excel 8.0;HDR=NO;IMEX=1'";//导入时包含Excel中的第一行数据，并且将数字和字符混合的单元格视为文本进行导入
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select  * from   [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            //根据DataGridView的列构造一个新的DataTable
            DataTable tb = new DataTable();
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++ )
            {
                DataColumn dc = new DataColumn();
                //dc.DataType = dgvc.ValueType;//若需要限制导入时的数据类型则取消注释，前提是DataGridView必须先绑定一个数据源那怕是空的DataTable
                tb.Columns.Add(dc);
            }

            //根据Excel的行逐一对上面构造的DataTable的列进行赋值
            foreach (DataRow excelRow in ds.Tables[0].Rows)
            {
                int i = 0;
                DataRow dr = tb.NewRow();
                foreach (DataColumn dc in tb.Columns)
                {
                        dr[dc] = excelRow[i];
                    i++;
                }
                tb.Rows.Add(dr);
            }
            //在DataGridView中显示导入的数据
            dgv.DataSource = tb;
        }

        public void ExportDataGridToCSV(DataTable dt)
        {
            string strFile = "";
            string path = "";

            //File info initialization
            SaveFileDialog  ofd = new SaveFileDialog();
            ofd.Title = "csv文件";
            ofd.FileName = "";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//为了获取特定的系统文件夹，可以使用System.Environment类的静态方法GetFolderPath()。该方法接受一个Environment.SpecialFolder枚举，其中可以定义要返回路径的哪个系统目录
            ofd.Filter = "csv文件(*.csv)|*.csv";
            ofd.ValidateNames = true;     //文件有效性验证ValidateNames，验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true;  //验证路径有效性
            ofd.CheckPathExists = true; //验证文件有效性


            string strName = string.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strName = ofd.FileName;
            }

            if (strName == "")
            {
                MessageBox.Show("没有选择Excel文件！无法进行数据导入");
                return;
            }
            strFile = "test";
            strFile = strFile + DateTime.Now.ToString("yyyyMMddhhmmss");
            strFile = strFile + ".csv";
            path = strName;



            System.IO.FileStream fs = new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, new System.Text.UnicodeEncoding());
            //Tabel header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i].ColumnName);
                sw.Write("\t");
            }
            sw.WriteLine("");
            //Table body
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sw.Write(DelQuota(dt.Rows[i][j].ToString()));
                    sw.Write("\t");
                }
                sw.WriteLine("");
            }
            sw.Flush();
            sw.Close();

            DownLoadFile(path);
        }

        private bool DownLoadFile(string _FileName)
        {
            try
            {
                System.IO.FileStream fs = System.IO.File.OpenRead(_FileName);
                byte[] FileData = new byte[fs.Length];
                fs.Read(FileData, 0, (int)fs.Length);
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/notepad");
                string FileName = System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(_FileName));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + System.Convert.ToChar(34) + FileName + System.Convert.ToChar(34));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fs.Length.ToString());
                System.Web.HttpContext.Current.Response.BinaryWrite(FileData);
                fs.Close();
                System.IO.File.Delete(_FileName);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }



        /// <summary>
        /// Delete special symbol
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string DelQuota(string str)
        {
            string result = str;
            string[] strQuota = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "`", ";", "'", ",", ".", "/", ":", "/,", "<", ">", "?" };
            for (int i = 0; i < strQuota.Length; i++)
            {
                if (result.IndexOf(strQuota[i]) > -1)
                    result = result.Replace(strQuota[i], "");
            }
            return result;
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开一个文件选择框
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel文件";
            ofd.FileName = "";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//为了获取特定的系统文件夹，可以使用System.Environment类的静态方法GetFolderPath()。该方法接受一个Environment.SpecialFolder枚举，其中可以定义要返回路径的哪个系统目录
            ofd.Filter = "Excel文件(*.xls)|*.xls";
            ofd.ValidateNames = true;     //文件有效性验证ValidateNames，验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true;  //验证路径有效性
            ofd.CheckPathExists = true; //验证文件有效性


            string strName = string.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strName = ofd.FileName;
            }

            if (strName == "")
            {
                MessageBox.Show("没有选择Excel文件！无法进行数据导入");
                return;
            }
            //调用导入数据方法
           dataGridView1.DataSource = null;
           ExcelToDataGridView(strName, dataGridView1);
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportDataGridToCSV(dataGridView1.DataSource as DataTable);
        }
    }
}
