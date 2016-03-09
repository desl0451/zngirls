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
    public partial class MainGirlAlbum : Form
    {
        public MainGirlAlbum()
        {
            InitializeComponent();
        }

        List<PicFile> files = new List<PicFile>();

        private void button1_Click(object sender, EventArgs e)
        {
            string path1 = textBox1.Text;
            textBox1.Text = path1;
            string path = textBox1.Text.Trim();
            DirectoryInfo directoryInfo = new DirectoryInfo(@path);
            FileInfo[] filinfos = directoryInfo.GetFiles();
            DirectoryInfo[] childDirectoryInfo = directoryInfo.GetDirectories();
            int index = 0;
            bool cbl = false;

            foreach (DirectoryInfo item in childDirectoryInfo)
            {
                DirectoryInfo[] dinfo = item.GetDirectories();
                foreach (DirectoryInfo d in dinfo)
                {
                    if (d.Name.Equals("album"))
                    {
                        DirectoryInfo childs = new DirectoryInfo(d.FullName);
                        FileInfo[] childsinfo = childs.GetFiles();
                        foreach (FileInfo finfo in childsinfo)
                        {
                            PicFile pic = new PicFile();
                            index = finfo.Name.IndexOf('.');
                            pic.Kuozhanname = finfo.Extension;
                            cbl = checkFile(pic.Kuozhanname);
                            if (cbl == true)
                            {
                                pic.Filename = finfo.Name.Substring(0, index);
                                pic.Allfilename = finfo.Name;
                                pic.FilePath = finfo.FullName;
                                files.Add(pic);
                            }
                        }
                    }
                }



            }
            dataGridView1.DataSource = files;
        }

        #region 不合格文件过滤
        public bool checkFile(string file)
        {
            bool bl = false;
            if (file.Equals(".html"))
            {
                bl = true;
            }
            else if (file.Equals(".htm"))
            {
                bl = true;
            }

            return bl;
        }
        #endregion
    }
}
