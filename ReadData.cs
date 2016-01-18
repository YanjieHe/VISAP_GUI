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
        public static Form1 S = null;
        public Form1()
        {
            InitializeComponent();
            S = this;
 
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
        
        //用于新建的datatable
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
        static DataTable table = new DataTable();
        //建立一个数据表格以供使用
        public void CSV(string pCsvPath)
        {
            try
            {
                String line;
                String[] split = null;
                table = new DataTable();
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
        private string[,] ReaddgvAll()
        {
            //读取表格中所有内容
            int RowCounts = dataGridView1.Rows.Count;
            int ColumnCounts = dataGridView1.Columns.Count;
            string[,] data = new string[RowCounts,ColumnCounts];
            int m,n;
            for (m = 0; m < RowCounts-1;m++){
                for (n = 0; n < ColumnCounts;n++){
                    if (dataGridView1.Rows[m].Cells[n].Value != null)
                    {
                        data[m,n] = dataGridView1.Rows[m].Cells[n].Value.ToString();
                    }
                    else
                    {
                        data[m, n] = "";
                    }
                    
                }
            }
            
            return data;
        }
        private string[,] ReaddgvSelected(int StartRow, int StartColumn, int EndRow, int EndColumn)
        {
            //读取表格中的数据
            //StartRow,StartColumn为起始行和列
            //EndRow，EndColumn为结束行和列
            //请注意这里的行和列是从0开始计算的
            int RowCounts = dataGridView1.Rows.Count;
            int ColumnCounts = dataGridView1.Columns.Count;
            string[,] data = new string[RowCounts, ColumnCounts];
            if (StartRow < 0)
            {
                StartRow = 0;
            }
            if (StartColumn < 0)
            {
                StartColumn = 0;
            }
            if (EndRow > RowCounts - 1)
            {
                EndRow = RowCounts - 1;
            }
            if (EndColumn > ColumnCounts)
            {
                EndColumn = ColumnCounts;
            }
            int m, n;
            for (m = StartRow; m < EndRow; m++)
            {
                for (n = StartColumn; n < EndColumn; n++)
                {
                    if (dataGridView1.Rows[m].Cells[n].Value != null)
                    {
                        data[m, n] = dataGridView1.Rows[m].Cells[n].Value.ToString();
                    }
                    else
                    {
                        data[m, n] = "";
                    }

                }
            }

            return data;
        }

        private static BigNumber[][] StringToBigNumber(string[,] str,int n,string Record)
        {
            //这个函数可以将字符串数组转化为BigNumber的锯齿型数组
            //n为变量数
            BigNumber[][] data = new BigNumber[n][];
            int q;
            int i = 0,j = 0;
            //GetLength(0),0代表的行数,1代表的列数
            int data_row = 0;
            int data_column;
            int str_empty_counts;
            //AllEmpty的情况应该事先测试好；
            char[] separator = { ',' };
            string[] RecordNumbers = Record.Split(separator);
            int Empty;
            int data_Counts = 0;
            //data_Counts用来给data计数，因为data的行数不能用i表示
            //i表示字符串数组的行，j表示列
            //先固定j，然后一行一行往下检测
            for (j = 0; j < str.GetLength(1);j++){
                data_column = 0;
                Empty = 0;
                //利用foreach语句确认本列是否为空列，有空列则Empty记为1
                foreach(string FindEmptyColumn in RecordNumbers){
                    if (FindEmptyColumn == j.ToString())
                    {
                        
                        Empty = 1;
                    }
                }
                if (Empty == 0) {
                    //如果Empty为0则不是空列，准备读数据
                    str_empty_counts = 0;
                    for (q = 0; q < str.GetLength(0) - 1; q++)
                    {
                        if (str[q,i].Trim() == "" || str[q,i] == null){
                            str_empty_counts++;
                        }
                    }
                    MessageBox.Show("字符串行数 = " + (str.GetLength(0) - 1).ToString());
                    MessageBox.Show("空格数" + (str_empty_counts + 1).ToString());
                    data[data_Counts] = new BigNumber[str.GetLength(0)- 1 - str_empty_counts];
                    MessageBox.Show("本列数据数：" + (str.GetLength(0) - 2-str_empty_counts).ToString());
                    data_Counts++;
                        // myArray[0] = new int[5] { 1, 3, 5, 7, 9 };
                    //对数组进行初始化
                        for (j = 0; j < str.GetLength(0); j++)
                        {
                            if (str[j, i] != null)
                            {
                                if (str[j, i].Trim() != "")
                                {
                                    MessageBox.Show("j = " + j + "i = " + i + "录入BigNumber的值 = " + str[j, i]);

                                    data[data_row][data_column] = new BigNumber(str[j, i].Trim());
                                    data_column++;
                                }
                            }
                        }
                }
            }
            return data;
        }
        //检测是否有空白列
        private static string IsEmptyColumn (string[,] str){
            int Counts;
            int i,j;
            string[] Record = new string[str.GetLength(1)];
            int RecordTimes = 0;
            string ReturnRecord = "";
            int Find_Not_Empty;
            for (j = 0; j < str.GetLength(1);j++ )
            {
                Counts = 0;
                for (i = 0; i < str.GetLength(0) - 1;i++){
                    if (str[i,j].Trim() == "" || str[i,j] == null)
                    {
                        Counts = Counts + 1;
                    }
                }
                if (Counts == str.GetLength(0) - 1)
                {
                    Record[RecordTimes] = j.ToString();
                    RecordTimes++;
                }
                else
                {
                    Record[RecordTimes] = "NOTEMPTY";
                    RecordTimes++;
                }
            }
            Find_Not_Empty = 0;
            foreach(string SingleRecord in Record){
                if (SingleRecord == "NOTEMPTY")
                {
                    Find_Not_Empty++;
                }
            }
            if (Find_Not_Empty == 0)
            {
                return "AllEmpty";
            }
            else
            {
                foreach (string ColumnNumber in Record)
                {
                    
                    ReturnRecord = ReturnRecord + "," + ColumnNumber;
                    
                }
                MessageBox.Show("ReturnRecord = " + ReturnRecord);
                return ReturnRecord;
            }
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
                using (DataTable dtable = dgv.DataSource as DataTable)
                if (columnindex + columnNum + 1 > dgv.ColumnCount)
                {
                    int mmm = columnNum + columnindex + 1 - dgv.ColumnCount;
                    for (int iii = 0; iii < mmm; iii++)
                    {
                        
                            ((DataTable)dataGridView1.DataSource).Columns.Add();
                        
                        /*dgv.DataBindings.Clear();
                        DataGridViewTextBoxColumn colum = new DataGridViewTextBoxColumn();
                        dgv.Columns.Insert(columnindex + 1, colum);*/
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
            if (dataGridView1.DataSource as DataTable != null)
            { 
            (sourceGridView.DataSource as DataTable).Rows.Add();
            }
            else
            {
                table.Rows.Add("");
            }
        }
        public void AddOneColumn(string ColumnName)
        {
            if (dataGridView1.DataSource as DataTable != null)
            {
                (dataGridView1.DataSource as DataTable).Columns.Add(ColumnName);
            }
            else
            { 
                table.Columns.Add(ColumnName);
                
                //dataGridView1.DataSource = table;
            }
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
                //dataGridView1.Columns[0].HeaderCell.Value = "编号"; 
                AddVariable f2 = new AddVariable();
                f2.ShowDialog();
                
                //AddOneColumn("haha");

                /*int ColumnCounts = dataGridView1.Columns.Count;
                using (DataTable dtable = dataGridView1.DataSource as DataTable)
                {
                    dtable.Columns.Add("Column"+(ColumnCounts + 1).ToString());
                    dataGridView1.DataSource = dtable;
                }*/
            }
        }

        private void 粘帖ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Paste(dataGridView1);
        }

        private void 列标题填充ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show( "将首行作为列标题会覆盖列标题原有内容，您确定要这样做吗？", "提示", MessageBoxButtons.YesNo ) == DialogResult.Yes )
            {
                int RowCounts = dataGridView1.Rows.Count;
                int ColumnCounts = dataGridView1.Columns.Count;
                int m,n;
                //因为系统默认会多加一行，因此读取时行数要减1
                for (n = 0; n < ColumnCounts; n++)
                {
                    dataGridView1.Columns[n].HeaderCell.Value = dataGridView1.Rows[0].Cells[n].Value.ToString();
                }
                for (m = 0; m < RowCounts - 1; m++)
                {
                    for (n = 0; n < ColumnCounts; n++)
                    {
                        dataGridView1.Rows[m].Cells[n].Value = dataGridView1.Rows[m+1].Cells[n].Value;
                    }
                }
            }
        }

        private void 汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string[,] str = ReaddgvAll();
            string Record = IsEmptyColumn(str);
            int counts = 0;
            if (Record != "AllEmpty")
            {
                foreach (char SingleN in Record)
                {
                    if (SingleN == 'N')
                    {
                        counts++;
                    }
                }
                BigNumber[][] NumberSeries = new BigNumber[counts][];
                NumberSeries = StringToBigNumber(str, counts, Record);

                for (int i = 0; i < NumberSeries[i].Length; i++)
                {
                    MessageBox.Show("第" + (i+1).ToString() + "个变量");
                    // 打印一维数组中的元素
                    for (int j = 0; j < NumberSeries[i].Length; j++)
                    {
                        MessageBox.Show(NumberSeries[i][j].ToString());
                    }
                }
            }
               
           
        }
        
       


    }
}
