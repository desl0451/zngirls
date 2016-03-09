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
using System.Net;
namespace www_zngirls_com_g
{
    public partial class MainDownHead : Form
    {
        public MainDownHead()
        {
            InitializeComponent();
        }

        bool bl = false;
        string url = "http://img.zngirls.com/girl/";
        string downurl = "http://img.zngirls.com/girl/22133/22133.jpg";
        string downurl_s = "";
        private void button1_Click(object sender, EventArgs e)
        {
            string begin = textBox1.Text.Trim();
            string end = textBox2.Text.Trim();
            int begincount = Convert.ToInt32(begin);
            int endcount = Convert.ToInt32(end);
            string path = textBox3.Text;
            WebClient wb = new WebClient();

            bl = true;
            for (int i = begincount; i <= endcount; i++)
            {

                downurl = url + i + "/" + i + ".jpg";
                downurl_s = url + i + "/" + i + "_s.jpg";

                try
                {
                    wb.DownloadFile(downurl, i + ".jpg");
                    File.Move(i + ".jpg", path + "\\" + i + ".jpg");
                    wb.DownloadFile(downurl_s, i + "_s.jpg");
                    File.Move(i + "_s.jpg", path + "\\" + i + "_s.jpg");
                }
                catch (Exception)
                {

                }
                if (bl == false)
                {
                    break;
                }

            }
            wb.Dispose();
            MessageBox.Show("完事");
        }
        List<PicFile> files = new List<PicFile>();
        private void button2_Click(object sender, EventArgs e)
        {

            string path = textBox4.Text.Trim();
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

            //图片来源
            string picPath = textBox4.Text.Trim();

            string exePath = textBox5.Text.Trim();


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

        private void button3_Click(object sender, EventArgs e)
        {
            bl = false;
        }
    }
}
