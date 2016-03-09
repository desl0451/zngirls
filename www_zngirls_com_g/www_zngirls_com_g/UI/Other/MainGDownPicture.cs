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
    public partial class MainGDownPicture : Form
    {

        string htmlfile = string.Empty;

        List<PicFile> filedownList = new List<PicFile>();  //保存需要下载的网页
        FileSubstring fileSub = new FileSubstring();

        List<DownFile> savedownList = new List<DownFile>();//网页下载列表
        string mulu = "";
        int sum = 0;

        public MainGDownPicture()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            int mujingindex = path.IndexOf("zngirls");
            mulu = path.Substring(0, mujingindex);
            //MessageBox.Show(mulu);
            DirectoryInfo root = new DirectoryInfo(@path);
            DirectoryInfo[] childInfo = root.GetDirectories();
            int index = 0;
            foreach (DirectoryInfo dinfo in childInfo)
            {
                FileInfo[] files = dinfo.GetFiles();
                foreach (FileInfo f in files)
                {
                    PicFile furl = new PicFile();
                    furl.Allfilename = f.Name;
                    index = f.Name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    furl.Filename = f.Name.Substring(0, index);
                    furl.Kuozhanname = f.Name.Substring(index);
                    furl.FilePath = f.FullName;
                    filedownList.Add(furl);
                }
            }
            dataGridView1.DataSource = filedownList;

            foreach (PicFile f in filedownList)
            {
                FeadFile(f.FilePath);
                down();
                sum++;
            }
            // MessageBox.Show("成功完成!");
        }
        WebClient webClient = null;
        #region 读取要下载的文件  
        public void FeadFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            htmlfile = sr.ReadToEnd();
            string ssss = FileSubstring.getContent(htmlfile, "<ul id=\"hgallery\">", "</ul>", true, path);
            int shu = FileSubstring.getLabelCount(ssss, "img");//查找img标签有多少个
            List<string> flist = FileSubstring.getSrc(ssss, "<img src=\"", "\"", shu); //个数
            savedownList = FileSubstring.addFileList(flist);
            filename = path;
            sr.Close();
            fs.Close();
        }
        #endregion

        StringBuilder sb = new StringBuilder();
        #region 下载文件 
        public void down()
        {
            string path = "";
            string downfile = "";
            foreach (DownFile f in savedownList)
            {
                try
                {
                    webClient = new WebClient();
                    path = mulu + "zngirls\\" + f.Path;
                    // MessageBox.Show(f.Path);
                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(path);
                        dinfo.Create();
                    }
                    webClient.Proxy = null;
                    downfile = "http://" + f.WebPath;

                    path = path + "\\" + f.Filename;
                    if (TestUrl(downfile))
                    {
                        //MessageBox.Show("存在");

                        if (!File.Exists(path))
                        {

                            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_downLoadProgressChanged);

                            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileComplated);

                            webClient.DownloadFileAsync(new Uri(downfile), @path);

                            oldpath = Application.StartupPath.ToString() + "\\" + f.Filename;

                            newpath = path;

                            //label5.Text = path;
                            //sb.Insert(0, newpath);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("不存在");
                    }

                }
                catch (Exception)
                {

                }

                textBox2.Text = sb.ToString();
            }
            this.progressBar1.Value = 0;
            savedownList.Clear();
        }
        public bool TestUrl(string url)
        {
            try
            {
                //WebRequest request = WebRequest.Create(url);
                //request.Timeout = 10000;
                //WebResponse response = request.GetResponse();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        string oldpath = "";
        string newpath = "";
        private void webClient_downLoadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.label7.Text = e.ProgressPercentage.ToString() + "%";
            this.label8.Text = string.Format("正在下载文件,完成进度{0}/{1}(字节)", e.BytesReceived, e.TotalBytesToReceive);
        }
        private void webClient_DownloadFileComplated(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("下载被取消!");
            }
            else
            {
                //MessageBox.Show("下载完成!");
                //pictureBox1.Image = Image.FromFile(newpath);
                this.webClient.CancelAsync();
                this.webClient.Dispose();
            }
        }


        #endregion
        string filename = "";
    }
}
