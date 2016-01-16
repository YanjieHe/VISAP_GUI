using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;
using DataStreams.Csv;
namespace 统计图形界面1
{
    public partial class Form1 : Form
    {
        [DllImport("DataStreams.dll")]
        public extern static string DllFun();
        public Form1()
        {
            InitializeComponent();
 
        }
        private string fileToLoad = " ";
        private string fileToSave = " ";
        private float X;
        private float Y;
        string filePath = "";
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
        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X; //窗体宽度缩放比例
            float newy = this.Height / Y;//窗体高度缩放比例
            setControls(newx, newy, this);//随窗体改变控件大小
            //this.Text = this.Width.ToString() + " " + this.Height.ToString();//窗体标题栏文本
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
                this.Resize += new EventHandler(Form1_Resize);//窗体调整大小时引发事件
                X = this.Width;//获取窗体的宽度
                Y = this.Height;//获取窗体的高度
                setTag(this);//调用方法
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "*.*|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = null;
                filePath = openFileDialog1.FileName;
                System.Data.DataTable dt = ImportExcel(filePath);
                dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = dt;
            }
        }
        private System.Data.DataTable ImportExcel(string path)
        {
            DataSet ds = new DataSet();
            string strConn = "";
            if (Path.GetExtension(path) == ".xls")
            {
                strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", path);
            }
            else
            {
                strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", path);
            }
            using (var oledbConn = new OleDbConnection(strConn))
            {
                oledbConn.Open();
                var sheetName = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new[] { null, null, null, "Table" });
                var sheet = new string[sheetName.Rows.Count];
                for (int i = 0, j = sheetName.Rows.Count; i < j; i++)
                {
                    sheet[i] = sheetName.Rows[i]["TABLE_NAME"].ToString();
                }
                var adapter = new OleDbDataAdapter(string.Format("select * from [{0}]", sheet[0]), oledbConn);
                adapter.Fill(ds);
            }
            return ds.Tables[0];
        }

        private void ExportExcel()
        {
            #region
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
                return;
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

            string str = "";
            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dataGridView1.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += dataGridView1.Rows[j].Cells[k].Value.ToString();
                    }
                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
            #endregion
        }
       
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int i;
            for (i = 0; i < 25; i++)
            {
                dt.Columns.Add("");
            }
                //添加列
            for (i = 0; i < 50; i++)
            {
                dt.Rows.Add("");
            }
            //添加行
            //要多少列和行就add多少就好
            dataGridView1.DataSource = dt;//datagridview綁定數據源
            //之後你在datatable dt 中添加修改數據就會在datagridview上顯示出來

        }

        private void excelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                ExportExcel();
        }

        



        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void csv文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            LoadFromCSVFile();
        }
        private void LoadFromCSVFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "*.*|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = null;
                fileToLoad = openFileDialog1.FileName;
                dataGridView1.DataSource = null;
                using (CsvReader reader = new CsvReader(fileToLoad))
                {
                    dataGridView1.DataSource = reader.ReadToEnd();
                }
            }
        }
        
        private void SaveAsCSVFile()
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export csv File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileToSave = saveFileDialog.FileName;
                CsvWriter data = new CsvWriter(fileToSave);
                DataTable dt = GetDgvToTable(dataGridView1);
                data.WriteAll(dt);
            }


        }
        public static DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = dgv.Rows[count].Cells[countsub].Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsCSVFile();
        }
    }
}
