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
    public partial class MainG : Form
    {
        public MainG()
        {
            InitializeComponent();
        }


        List<PicFile> files = new List<PicFile>();
        List<FileUrl> utagList = new List<FileUrl>();  //替换utag
        List<FileUrl> hgalleryList = new List<FileUrl>();//替换hgallery


        string allhtml = "";//全部网页内容

        string ulhtmlutag = "";//提取后ul内容utag

        string ulhtmlhgallery = "";//提取后ul内容hgallery

        string newHtml = "";//最新网页内容

        string resulttag = "";
        string resulhgallery = "";
        public void init()
        {
            //folderBrowserDialog1.ShowDialog();
            //string path1 = folderBrowserDialog1.SelectedPath;
            //初始化加载所有目录
            initDirectory(); //<ul id="utag">处理



        }
        #region 1.<ul id="utag">处理代码

        #region 执行整个过程
        public void initDirectory()
        {
            string path1 = textBox1.Text;
            textBox1.Text = path1;
            string path = textBox1.Text.Trim();
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
            cbl = false;
            foreach (DirectoryInfo item in childDirectoryInfo)
            {
                DirectoryInfo childs = new DirectoryInfo(@item.FullName);
                FileInfo[] childsinfo = childs.GetFiles();
                foreach (FileInfo finfo in childsinfo)
                {
                    PicFile pic = new PicFile();
                    index = finfo.Name.IndexOf('.');
                    pic.Kuozhanname = finfo.Extension;
                    cbl = Common.checkFile(pic.Kuozhanname);
                    if (cbl == true)
                    {
                        pic.Filename = finfo.Name.Substring(0, index);
                        pic.Allfilename = finfo.Name;
                        pic.FilePath = finfo.FullName;
                        files.Add(pic);
                    }
                }
            }


            int count = 0;//文件数量
            int errorCount = 0;//错误数量
            StringBuilder filename = new StringBuilder();
            //文件开始循环
            foreach (PicFile item in files)
            {
                try
                {
                    FileStream fs = new FileStream(item.FilePath, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    allhtml = sr.ReadToEnd();//读取全部网页

                    getutag();        //提取
                    replaceutag();    //替换utag

                    gethgallery();    //提取
                    replacehgallery();//替换hgallery

                    tongjidataoriginalCount();

                    sr.Close();
                    fs.Close();

                    FileStream nfs = new FileStream(item.FilePath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(nfs);
                    sw.Write(newHtml);
                    sw.Close();
                    nfs.Close();
                    count++;
                }
                catch (Exception)
                {
                    filename.Append(item.Filename);
                    errorCount++;
                }
            }
            MessageBox.Show("错误文件:" + filename);
            MessageBox.Show("正确数量:" + count + "\t错误数量:" + errorCount);


        }
        #endregion

        #region 提取<ul id="utag">后内容
        public void getutag()
        {
            string stxt2 = allhtml;
            int index = stxt2.IndexOf("<ul id=\"utag\">");
            int endindex = stxt2.IndexOf("</ul>", index + 15);
            int result = endindex - index;
            string endtxt = stxt2.Substring(index, result);
            ulhtmlutag = endtxt;
        }
        #endregion

        #region 替换后生成
        public void replaceutag()
        {
            string s = ulhtmlutag;

            int count = 0;
            int beginhref = 0;
            int endhref = 0;
            int zngirlsCount = 0;

            int begintppabs = 0;
            int endtppabs = 0;
            int strhrefCount = 0;
            int strtppabsCount = 0;
            string strhref = "";
            string strtppabs = "";
            int cishu = getutagLiCount(s);


            int beginzngirls = 0;

            string strreplace = "";

            int urlCount = 0;//网址数量

            string strLi = "";

            //查找a标签位置
            int begina = 0;
            int enda = 0;
            int straCount = 0;

            string strtitle = "";//标题
            urlCount = "http://www.zngirls.com/".Length;
            for (int i = 0; i < s.Length; i++)
            {
                //href
                beginhref = s.IndexOf("href=\"", i);
                beginhref = beginhref + 6;
                endhref = s.IndexOf("\"", beginhref);

                //tppabs
                begintppabs = s.IndexOf("tppabs=\"", endhref);
                if (begintppabs == -1)
                {
                    return;
                }
                begintppabs = begintppabs + 8;

                endtppabs = s.IndexOf("\"", begintppabs);

                //www.zngirls.com
                beginzngirls = s.IndexOf("http://www.zngirls.com/", i);

                //查找">
                begina = s.IndexOf("\">", beginzngirls + 2);
                begina = begina + 2;
                enda = s.IndexOf("</a>", begina);

                if (beginhref > 0 && endhref > 0 && endhref > beginhref && endtppabs > 0)
                {
                    count++;

                    strhrefCount = endhref - beginhref;

                    strtppabsCount = endtppabs - begintppabs;

                    straCount = enda - begina;

                    strhref = s.Substring(beginhref, strhrefCount);

                    strtppabs = s.Substring(begintppabs, strtppabsCount);

                    strtitle = s.Substring(begina, straCount);


                    beginzngirls = beginzngirls + urlCount;
                    zngirlsCount = endtppabs - beginzngirls;
                    strreplace = s.Substring(beginzngirls, zngirlsCount);
                    strreplace = strreplace + "index.htm";
                    strreplace = "../../" + strreplace;


                    //先找<a>标签
                    strLi = string.Format("<li><a target='_blank' href=\"{0}\">{1}</a></li>", strreplace, strtitle);



                    FileUrl fileUrl = new FileUrl();
                    fileUrl.Href = strhref;
                    fileUrl.Tppabs = strtppabs;
                    fileUrl.ReplaceStr = strreplace;
                    fileUrl.Hrefli = strLi;
                    utagList.Add(fileUrl);



                    i = beginzngirls;
                    beginhref = 0;
                    endhref = 0;

                }
                if (count == cishu)
                {
                    break;
                }
            }

            //dataGridView2.DataSource = fileList;
            StringBuilder sb = new StringBuilder("<ul id=\"utag\">");
            foreach (FileUrl f in utagList)
            {
                sb.Append(f.Hrefli);
            }

           
            resulttag = sb.ToString();
        }
        #endregion

        #region 替换方法
        public void tiHuanutag(string paramNewString)
        {



        }
        #endregion

        #region 统计li个数
        public int getutagLiCount(string s)
        {
            int count = 0;
            int index = 0;
            for (int i = 0; i < s.Length; i++)
            {
                index = s.IndexOf("<li>", i);
                if (index >= 0)
                {
                    count++;
                    i = index;
                }
                if (index == -1)
                {
                    break;
                }
            }
            return count;
        }
        #endregion

        #endregion



        #region <ul id="hgallery">处理代码
        #region 提取<ul id="utag">后内容
        public void gethgallery()
        {
            string stxt2 = allhtml;
            int index = stxt2.IndexOf("<ul id=\"hgallery\">");
            int endindex = stxt2.IndexOf("</ul>", index + 15);
            int result = endindex - index;
            string endtxt = stxt2.Substring(index, result);
            ulhtmlhgallery = endtxt;
        }
        #endregion

        #region 
        public void replacehgallery()
        {

            string s = ulhtmlhgallery;

            int count = 0;
            int beginhref = 0;
            int endhref = 0;
            int zngirlsCount = 0;

            int begintppabs = 0;
            int endtppabs = 0;
            int strhrefCount = 0;
            int strtppabsCount = 0;
            string strhref = "";
            string strtppabs = "";
            int cishu = gethgalleryLiCount(s);


            int beginzngirls = 0;
            string strreplace = "";

            int urlCount = 0;//网址数量

            string strLi = "";

            //查找a标签位置
            int beginalt = 0;
            int endalt = 0;
            int straCount = 0;

            string stralt = "";//标题
            string url = "";
            for (int i = 0; i < s.Length; i++)
            {
                //href
                beginhref = s.IndexOf("src=\"", i);
                beginhref = beginhref + 5;
                endhref = s.IndexOf("\"", beginhref);

                //tppabs
                begintppabs = s.IndexOf("tppabs=\"", endhref);
                begintppabs = begintppabs + 8;

                endtppabs = s.IndexOf("\"", begintppabs);

                //www.zngirls.com



                ////查找alt">
                beginalt = s.IndexOf("alt=\'", beginzngirls + 5);
                beginalt = beginalt + 5;
                endalt = s.IndexOf("\'", beginalt);

                if (beginhref > 0 && endhref > 0 && endhref > beginhref)
                {
                    count++;

                    strhrefCount = endhref - beginhref;

                    strtppabsCount = endtppabs - begintppabs;

                    straCount = endalt - beginalt;

                    strhref = s.Substring(beginhref, strhrefCount);

                    strtppabs = s.Substring(begintppabs, strtppabsCount);

                    if (strtppabs.Contains("t1.zngirls.com"))
                    {
                        urlCount = "http://t1.zngirls.com/".Length;
                        beginzngirls = s.IndexOf("http://t1.zngirls.com/", i);
                        url = "t1.zngirls.com";
                    }
                    else if (strtppabs.Contains("t2.zngirls.com"))
                    {
                        urlCount = "http://t1.zngirls.com/".Length;
                        beginzngirls = s.IndexOf("http://t2.zngirls.com/", i);
                        url = "t1.zngirls.com";
                    }
                    else
                    {
                        urlCount = "http://img.zngirls.com/".Length;
                        beginzngirls = s.IndexOf("http://img.zngirls.com/", i);
                        url = "img.zngirls.com";
                    }

                    stralt = s.Substring(beginalt, straCount);


                    beginzngirls = beginzngirls + urlCount;
                    zngirlsCount = endtppabs - beginzngirls;
                    strreplace = s.Substring(beginzngirls, zngirlsCount);

                    strreplace = "../../../" + url + "/" + strreplace;


                    //先找<a>标签
                    strLi = string.Format("<img src=\"{0}\" alt='{1}'/>", strreplace, stralt);



                    FileUrl fileUrl = new FileUrl();
                    fileUrl.Href = strhref;
                    fileUrl.Tppabs = strtppabs;
                    fileUrl.ReplaceStr = strreplace;
                    fileUrl.Hrefli = strLi;
                    hgalleryList.Add(fileUrl);



                    i = beginzngirls;
                    beginhref = 0;
                    endhref = 0;

                }
                if (count == cishu)
                {
                    break;
                }
            }

            
            StringBuilder sb = new StringBuilder("<ul id=\"hgallery\">");
            foreach (FileUrl f in hgalleryList)
            {
                sb.Append(f.Hrefli);
            }

         
            resulhgallery = sb.ToString();
            tiHuanhgallery();

        }
        #endregion

        #region 替换方法
        public void tiHuanhgallery()
        {
            string allString = allhtml;


            string oldString = allhtml;
            int index = oldString.IndexOf("<ul id=\"utag\">");
            int endindex = oldString.IndexOf("</ul>", index + 15);
            int result = endindex - index;
            oldString = oldString.Substring(index, result);

            string newString = resulttag;
            allString = allString.Replace(oldString, newString);







            string oldString1 = allString;
            int index1 = oldString1.IndexOf("<ul id=\"hgallery\">");
            int endindex1 = oldString1.IndexOf("</ul>", index1 + 15);
            int result1 = endindex1 - index1;
            oldString1 = oldString1.Substring(index1, result1);

            string newString1 = resulhgallery;
            allString = allString.Replace(oldString1, newString1);
         
            newHtml = allString;


            utagList = new List<FileUrl>();
            hgalleryList = new List<FileUrl>();
        }
        #endregion

        #region 统计li个数
        public int gethgalleryLiCount(string s)
        {
            int count = 0;
            int index = 0;
            for (int i = 0; i < s.Length; i++)
            {
                index = s.IndexOf("<img src=\"", i);
                if (index >= 0)
                {
                    count++;
                    i = index;
                }
            }
            return count;
        }
        #endregion

        #endregion

        #region data-original='http://img.zngirls.com/ 替换
        public void tongjidataoriginalCount()
        {

            int beginoriginal1 = 0;
            int beginoriginal2 = 0;
            int beginoriginal3 = 0;


            beginoriginal1 = newHtml.IndexOf("data-original=\'http://img.zngirls.com/");
            beginoriginal2 = newHtml.IndexOf("data-original=\'http://t1.zngirls.com/");
            beginoriginal3 = newHtml.IndexOf("data-original=\'http://t2.zngirls.com/");
            if (beginoriginal1 > 0)
            {
                newHtml = newHtml.Replace("data-original=\'http://img.zngirls.com/", "src=\'../../../img.zngirls.com/");
            }
            if (beginoriginal2 > 0)
            {
                newHtml = newHtml.Replace("data-original=\'http://t1.zngirls.com/", "src=\'../../../t1.zngirls.com/");
            }
            if (beginoriginal3 > 0)
            {
                newHtml = newHtml.Replace("data-original=\'http://t2.zngirls.com/", "src=\'../../../t1.zngirls.com/");
            }

        }
        #endregion
        private void button5_Click(object sender, EventArgs e)
        {
            init();
            MessageBox.Show("恭喜完成");
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
    }
}
