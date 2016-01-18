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

        
        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != null)
            {
                Form1.S.AddOneColumn(textBox1.Text.ToString());
            }
            }
        
    }
}
