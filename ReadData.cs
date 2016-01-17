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
        public void CSV(string pCsvPath)
        {
            try
            {
                String line;
                String[] split = null;
                DataTable table = new DataTable();
                DataRow row = null;
                StreamReader sr = new StreamReader(pCsvPath, System.Text.Encoding.Default);
                //创建与数据源对应的数据列 
                line = sr.ReadLine();
                split = line.Split(',');
                foreach (String colname in split)
                {
                    table.Columns.Add(colname, System.Type.GetType("System.String"));
                }
                //将数据填入数据表 
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    j = 0;
                    row = table.NewRow();
                    split = line.Split(',');
                    foreach (String colname in split)
                    {
                        row[j] = colname;
                        j++;
                    }
                    table.Rows.Add(row);
                }
                sr.Close();
                //显示数据 
                this.dataGridView1.DataSource = table.DefaultView;
            }
            catch (Exception vErr)
            {
                MessageBox.Show(vErr.Message);
            }
            finally
            {
                GC.Collect();
            }
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
                CSV(fileToLoad);
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
        private string[,] Readdgv()
        {
            int RowCounts = dataGridView1.Rows.Count;
            int ColumnCounts = dataGridView1.Columns.Count;
            string[,] data = new string[RowCounts,ColumnCounts];
            int m,n;
            for (m = 0; m < RowCounts-1;m++){
                for (n = 0; n < ColumnCounts-1;n++){
                    data[m,n] = dataGridView1.Rows[m].Cells[n].Value.ToString();
                }
            }
            
            return data;
        }
     

        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label_Row_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            label_Row.Text = (rowIndex+1).ToString();
            int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
            label_Column.Text = (columnIndex + 1).ToString();
        }
        #region 粘贴
        public int Paste(DataGridView dgv)
        {
            string pasteText = "";
            bool b_cut = false;
            int kind = 0;
            try
            {
                if (kind == 0)
                {
                    pasteText = Clipboard.GetText();
                }
                if (string.IsNullOrEmpty(pasteText))
                    return -1;
                int rowNum = 0;
                int columnNum = 0;
                //获得当前剪贴板内容的行、列数
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (pasteText.Substring(i, 1) == "\t")
                    {
                        columnNum++;
                    }
                    if (pasteText.Substring(i, 1) == "\n")
                    {
                        rowNum++;
                    }
                }
                Object[,] data;
                //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
                if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
                {
                    rowNum = rowNum - 1;
                }
                columnNum = columnNum / (rowNum + 1);
                data = new object[rowNum + 1, columnNum + 1];

                String rowStr;
                //对数组赋值
                for (int i = 0; i < (rowNum + 1); i++)
                {
                    for (int colIndex = 0; colIndex < (columnNum + 1); colIndex++)
                    {
                        rowStr = null;
                        //一行中的最后一列
                        if (colIndex == columnNum && pasteText.IndexOf("\r") != -1)
                        {
                            rowStr = pasteText.Substring(0, pasteText.IndexOf("\r"));
                        }
                        //最后一行的最后一列
                        if (colIndex == columnNum && pasteText.IndexOf("\r") == -1)
                        {
                            rowStr = pasteText.Substring(0);
                        }
                        //其他行列
                        if (colIndex != columnNum)
                        {
                            rowStr = pasteText.Substring(0, pasteText.IndexOf("\t"));
                            pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
                        }
                        if (rowStr == string.Empty)
                            rowStr = null;
                        data[i, colIndex] = rowStr;
                    }
                    //截取下一行数据
                    pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);
                }
                /*检测值是否是列头*/
                /*
                //获取当前选中单元格所在的列序号
                int columnindex = dgv.CurrentRow.Cells.IndexOf(dgv.CurrentCell);
                //获取获取当前选中单元格所在的行序号
                int rowindex = dgv.CurrentRow.Index;*/
                int columnindex = -1, rowindex = -1;
                int columnindextmp = -1, rowindextmp = -1;
                if (dgv.SelectedCells.Count != 0)
                {
                    columnindextmp = dgv.SelectedCells[0].ColumnIndex;
                    rowindextmp = dgv.SelectedCells[0].RowIndex;
                }
                //取到最左上角的 单元格编号
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    //dgv.Rows[cell.RowIndex].Selected = true;
                    columnindex = cell.ColumnIndex;
                    if (columnindex > columnindextmp)
                    {
                        //交换
                        columnindex = columnindextmp;
                    }
                    else
                        columnindextmp = columnindex;
                    rowindex = cell.RowIndex;
                    if (rowindex > rowindextmp)
                    {
                        rowindex = rowindextmp;
                        rowindextmp = rowindex;
                    }
                    else
                        rowindextmp = rowindex;
                }
                if (kind == -1)
                {
                    columnindex = 0;
                    rowindex = 0;
                }
                DataSet ds = new DataSet(); 
                int mm;
                int ii;
                //如果行数超过当前列表行数
                while (rowindex + rowNum + 1 > dgv.RowCount)
                {
                    mm = rowNum + rowindex + 1 - dgv.RowCount;
                    for ( ii = 0; ii < mm + 1; ii++)
                    {
                        ImportData(dataGridView1);
                    }
                }

                //如果列数超过当前列表列数
                if (columnindex + columnNum + 1 > dgv.ColumnCount)
                {
                    int mmm = columnNum + columnindex + 1 - dgv.ColumnCount;
                    for (int iii = 0; iii < mmm; iii++)
                    {
                        dgv.DataBindings.Clear();
                        DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                        dgv.Columns.Insert(columnindex + 1, colum);
                    }
                }

                //增加超过的行列
                for (int j = 0; j < (rowNum + 1); j++)
                {
                    for (int colIndex = 0; colIndex < (columnNum + 1); colIndex++)
                    {
                        if (colIndex + columnindex < dgv.Columns.Count)
                        {
                            if (dgv.Columns[colIndex + columnindex].CellType.Name == "DataGridViewTextBoxCell")
                            {
                                if (dgv.Rows[j + rowindex].Cells[colIndex + columnindex].ReadOnly == false)
                                {
                                    dgv.Rows[j + rowindex].Cells[colIndex + columnindex].Value = data[j, colIndex];
                                    dgv.Rows[j + rowindex].Cells[colIndex + columnindex].Selected = true;
                                }
                            }
                        }
                    }
                }
                //清空剪切板内容
                if (b_cut)
                    Clipboard.Clear();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        private void 粘帖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste(dataGridView1);
        }
        void ImportData(DataGridView sourceGridView)
        {
            if (dataGridView1.DataSource != null) { 
            ((DataTable)sourceGridView.DataSource).Rows.Add();
            }
            //  
            //...............  
            //  
        }  
        private void button_addrow_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                ImportData(dataGridView1);
            }

        }
        private void button_addcolumn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                AddVariable f2 = new AddVariable();
                f2.ShowDialog();

                /*int ColumnCounts = dataGridView1.Columns.Count;
                using (DataTable dtable = dataGridView1.DataSource as DataTable)
                {
                    dtable.Columns.Add("Column"+(ColumnCounts + 1).ToString());
                    dataGridView1.DataSource = dtable;
                }*/
            }
        }
        
       


    }
}
