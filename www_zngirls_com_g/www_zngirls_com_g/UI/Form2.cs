using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace www_zngirls_com_g
{
    public partial class Form2 : Form
    {
        List<PicFile> files = new List<PicFile>();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string path = folderBrowserDialog1.SelectedPath;
            textBox1.Text = path;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string path = folderBrowserDialog1.SelectedPath;
            textBox2.Text = path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text.Trim();
            DirectoryInfo directoryInfo = new DirectoryInfo(@path);
            FileInfo[] filinfos = directoryInfo.GetFiles();

            int index = 0;
            foreach (FileInfo file in filinfos)
            {
                PicFile pic = new PicFile();
                index = file.Name.IndexOf('.');
                pic.Filename = file.Name.Substring(0, 5);
                pic.Allfilename = file.Name;
                pic.FilePath = file.FullName;
                files.Add(pic);
            }
            dataGridView1.DataSource = files;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //图片来源
            string picPath = textBox1.Text.Trim();

            string exePath = textBox2.Text.Trim();
            string path = "";

            string sourcefilePath = "";
            string destfilePath = "";
            foreach (PicFile f in files)
            {
                path = exePath + "\\" + f.Filename;
                DirectoryInfo dinfo = new DirectoryInfo(path);
                dinfo.Create();
                sourcefilePath = picPath + "\\" + f.Allfilename;
                destfilePath = path + "\\" + f.Allfilename;
                File.Move(sourcefilePath, destfilePath);

            }
            MessageBox.Show("移动成功!");
        }
    }
}
