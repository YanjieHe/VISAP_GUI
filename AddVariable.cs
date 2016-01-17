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
    public partial class AddVariable : Form
    {
        public AddVariable()
        {
            InitializeComponent();
        }

        public static Form1 form1 = null;
        private void button_add_Click(object sender, EventArgs e)
        {
            try{
                //MessageBox.Show(((DataGridView)Application.OpenForms["Form1"].Controls["dataGridView1"]).Rows.Count + "");
                int ColumnCounts = ((DataGridView)Application.OpenForms["Form1"].Controls["dataGridView1"]).Columns.Count;
                
                using (DataTable dtable = ((DataGridView)Application.OpenForms["Form1"].Controls["dataGridView1"]).DataSource as DataTable)
                {
                    //((DataGridView)Application.OpenForms["Form1"].Controls["dataGridView1"]).Columns.Add(textBox1.Text.ToString());
                    dtable.Columns.Add(textBox1.Text.ToString());
                    ((DataGridView)Application.OpenForms["Form1"].Controls["dataGridView1"]).DataSource = dtable;
                }
                /*if (form1.dataGridView1.DataSource != null)
                {
                    int ColumnCounts = form1.dataGridView1.Columns.Count;
                    using (DataTable dtable = form1.dataGridView1.DataSource as DataTable)
                    {
                        dtable.Columns.Add(textBox1.Text.ToString());
                        form1.dataGridView1.DataSource = dtable;
                    }
                }*/
            }
                catch (Exception ex)
            {

                }
            }
        
    }
}
