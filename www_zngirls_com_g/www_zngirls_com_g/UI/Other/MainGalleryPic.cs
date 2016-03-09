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
    public partial class MainGalleryPic : Form
    {
        public MainGalleryPic()
        {
            InitializeComponent();
        }
        List<PicFile> picList = new List<PicFile>();

        string allhtml = "";

        string cutStr = "";
        string mulu = "";
        WebClient webClient = null;

        string oldpath = "";
        string newpath = "";
        List<string> downList = new List<string>();//下载列表
        List<DownFile> savedownList = new List<DownFile>();//网页下载列表
        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            int ipath = path.IndexOf("www.zngirls.com");

            mulu = path.Substring(0, ipath);
            picList = DownFile.LoadFile(path);

            MessageBox.Show(picList.Count.ToString());

            int count = 0;//文件数量
            int errorCount = 0;//错误数量
            int num = 0;
            StringBuilder filename = new StringBuilder();
            //文件开始循环
            foreach (PicFile item in picList)
            {
                try
                {
                    FileStream fs = new FileStream(item.FilePath, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    allhtml = sr.ReadToEnd();//读取全部网页
                    cutStr = FileSubstring.getContent(allhtml, "<div id=\"listdiv\"", "</ul>");
                    num = FileSubstring.getLabelCount(cutStr, "<li");
                    downList = FileSubstring.getSrc(cutStr, "src=\'", "\'", num); //个数
                    savedownList = FileSubstring.addFileList(downList);
                    down();
                    sr.Close();
                    fs.Close();
                    count++;
                }
                catch (Exception)
                {
                    filename.Append(item.Filename);
                    errorCount++;
                }
            }
        }

        public void down()
        {
            string path = "";
            string downfile = "";
            foreach (DownFile f in savedownList)
            {
                try
                {
                    webClient = new WebClient();
                    path = mulu + f.Path;
                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(path);
                        dinfo.Create();
                    }
                    webClient.Proxy = null;
                    downfile = "http://" + f.WebPath;

                    path = path + "\\" + f.Filename;

                    //MessageBox.Show("存在");

                    if (!File.Exists(path))
                    {

                        webClient.DownloadFileAsync(new Uri(downfile), @path);

                        oldpath = Application.StartupPath.ToString() + "\\" + f.Filename;

                        newpath = path;
                    }

                }
                catch (Exception)
                {

                }

            }
            savedownList.Clear();

        }



    }
}
