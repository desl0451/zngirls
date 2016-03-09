using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using DevExpress.XtraSplashScreen;
namespace www_zngirls_com_g
{
    public partial class MainLogin : DevExpress.XtraEditors.XtraForm
    {
        public MainLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            init();
        }
        MainForm mainForm;
        public void init()
        {

            string path = textBox1.Text;
            if (path.IndexOf("zngirl") != -1)
            {
                PageInfo.path = textBox1.Text;
                Hide();
                SplashScreenManager.ShowForm(null, typeof(MainInit), true, true, false, 1000);
                mainForm = new MainForm();
                mainForm.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("目录不合格!");
            }
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
    }
}
