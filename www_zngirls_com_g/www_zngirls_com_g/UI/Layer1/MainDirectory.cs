using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace www_zngirls_com_g
{
    public partial class MainDirectory : Form
    {
        public MainDirectory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();


            string path = folderBrowserDialog1.SelectedPath;
            if (path.IndexOf("zngirl") != -1)
            {
                textBox1.Text = path;
                PageInfo.path = textBox1.Text;
            }
            else
            {
                MessageBox.Show("目录不合格!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
