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
    public partial class MainIndex : Form
    {
        public MainIndex()
        {
            InitializeComponent();
        }
        List<PicFile> files = new List<PicFile>();

        string allhtml = "";//读取全部网页内容
        string newHtml = "";//生成后最新网页内容
        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            int indexPath = path.IndexOf("www.zngirls.com");

            bool blfile = false;
            if (indexPath != -1)
            {
                blfile = Common.checkIndex(path + "\\index.htm");
                if (blfile == false)
                {
                    MessageBox.Show("没有此文件index.htm");
                    return;
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(@path);
                FileInfo[] filinfos = directoryInfo.GetFiles();
                DirectoryInfo[] childDirectoryInfo = directoryInfo.GetDirectories();
                int index = 0;
                bool cbl = false;

                foreach (FileInfo file in filinfos)
                {
                    PicFile pic = new PicFile();
                    index = file.Name.IndexOf('.');
                    pic.Kuozhanname = file.Extension;
                    cbl = Common.checkFile(pic.Kuozhanname);
                    if (cbl == true)
                    {
                        pic.Filename = file.Name.Substring(0, index);
                        pic.Allfilename = file.Name;
                        pic.FilePath = file.FullName;
                        files.Add(pic);
                    }
                }

                int count = 0;//文件数量
                int errorCount = 0;//错误数量
                FileStream fs = null;
                StreamReader sr = null;
                FileStream nfs = null;
                StreamWriter sw = null;
                foreach (PicFile item in files)
                {
                    try
                    {
                        fs = new FileStream(item.FilePath, FileMode.Open);
                        sr = new StreamReader(fs);
                        allhtml = sr.ReadToEnd();//读取全部网页
                        replaceDataOriginal();        //提取< div id = "tdiv" > 
                                                      //MessageBox.Show(newHtml);
                        sr.Close();
                        fs.Close();

                        nfs = new FileStream(item.FilePath, FileMode.Create);
                        sw = new StreamWriter(nfs);
                        sw.Write(newHtml);
                        sw.Close();
                        nfs.Close();
                        count++;
                    }
                    catch (Exception)
                    {
                        errorCount++;
                    }
                }
                MessageBox.Show("正确数量:" + count + "\t错误数量:" + errorCount);
            }
            else
            {
                MessageBox.Show("目录不正确!");
            }
        }

        #region 读取html
        public void replaceDataOriginal()
        {
            string txt = allhtml;
            newHtml = allhtml.Replace("data-original=\'http://", "src=\'../");
        }
        #endregion
    }
}
