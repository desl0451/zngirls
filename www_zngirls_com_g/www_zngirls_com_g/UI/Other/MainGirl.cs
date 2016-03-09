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
    public partial class MainGirl : Form
    {
        string allhtml = "";//全部网页内容

        string newHtml = "";//最新网页内容

        string CutInfoLeft_ImgDiv = "";//截取

        string archive_more = "";//共几册
        public MainGirl()
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
                DirectoryInfo childs = new DirectoryInfo(@item.FullName);
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
            dataGridView1.DataSource = files;

            int count = 0;//文件数量
            int errorCount = 0;//错误数量
            //文件开始循环
            foreach (PicFile item in files)
            {
                try
                {
                    FileStream fs = new FileStream(item.FilePath, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    allhtml = sr.ReadToEnd();//读取全部网页
                    replaceDataOriginal();        //去掉所有DataOriginal
                    cutInfoLeft_ImgDiv();
                    TppabsReplaceHref();

                    int i=Cutarchive_more();//提取更多图片
                    if (i != -1)
                    {
                        moreTppabsReplacemoreHref();
                    }
                  
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
                    errorCount++;
                }
                moreIndex=0;
            }
            textBox2.Text = newHtml;
            MessageBox.Show("正确数量:" + count + "\t错误数量:" + errorCount);
        }
        #region 读取html
        public void replaceDataOriginal()
        {
            //www.zngirls.com\girl\21950
            newHtml = allhtml.Replace("data-original=\'http://t1.zngirls.com/", "src=\'../../../t1.zngirls.com/");
            newHtml = newHtml.Replace("data-original=\'http://img.zngirls.com/", "src=\'../../../img.zngirls.com/");
            newHtml = newHtml.Replace("data-original=\'http://t2.zngirls.com/", "src=\'../../../t1.zngirls.com/");
            newHtml = newHtml.Replace("data-original='/girl/", "src='../../girl/");
        }
        #endregion

        #region 替换infoleft_imgdiv
        public void cutInfoLeft_ImgDiv()
        {
            string stxt2 = newHtml;
            int index = stxt2.IndexOf("<div class=\'infoleft_imgdiv\'>");
            int endindex = stxt2.IndexOf("</div>", index);
            int result = endindex - index;
            string endtxt = stxt2.Substring(index, result);
            CutInfoLeft_ImgDiv = endtxt;
        }
        #endregion


        #region 
        public void TppabsReplaceHref()
        {
            string ahref = imgdivHref();
            string tppabs = imgdivtppabs();
            if (tppabs.Equals("-1") )
            {
                return;
            }
            int url = "http:/".Length;
            tppabs = tppabs.Substring(url);
            tppabs = "../../.." + tppabs;
            //MessageBox.Show(tppabs);
            string alt = altString();
            string src = srcpath();
            string stppabs = srctppabs();
            if (stppabs.Equals("-1"))
            {
                return;
            }
            stppabs = stppabs.Substring(url);
            stppabs = "../../.." + stppabs;
            //MessageBox.Show(ahref + "====" + tppabs + "====" + src + "=====" + stppabs);

            string olds = string.Format("<div class=\'infoleft_imgdiv\'><a target='_blank' class='imglink' href=\"{0}\"><img alt=\"{1}\" src=\"{2}\"/></a>", tppabs, alt, stppabs);
         
            newHtml = newHtml.Replace(CutInfoLeft_ImgDiv, olds);

        }
        int index = 0;
        public string imgdivHref()
        {
            string infoLeft_ImgDiv = CutInfoLeft_ImgDiv;
            int beginhref = infoLeft_ImgDiv.IndexOf("href=\"");
            int endhref = infoLeft_ImgDiv.IndexOf("\"", beginhref + 6);
            int count = endhref - beginhref - 6;
            string href = CutInfoLeft_ImgDiv.Substring(beginhref + 6, count);
            index = endhref;
            return href;
        }

        public string imgdivtppabs()
        {
            string infoLeft_ImgDiv = CutInfoLeft_ImgDiv;
            int beginTppabs1 = infoLeft_ImgDiv.IndexOf("tppabs=\"", index);

            int endTppabs1 = infoLeft_ImgDiv.IndexOf("\"", beginTppabs1 + 8);
            int count = endTppabs1 - beginTppabs1 - 8;
            string tppabs = CutInfoLeft_ImgDiv.Substring(beginTppabs1 + 8, count);
            index = endTppabs1;
            if (beginTppabs1 == -1)
            {
                return "-1";
            }
            return tppabs;
        }
        public string altString()
        {
            string infoLeft_imgDiv = CutInfoLeft_ImgDiv;
            int beginAlt = infoLeft_imgDiv.IndexOf("alt=\"", index);
            int endAlt = infoLeft_imgDiv.IndexOf("\"", beginAlt + 5);
            int count = endAlt - beginAlt - 5;
            string alt = CutInfoLeft_ImgDiv.Substring(beginAlt + 5, count);
            return alt;
        }
        public string srcpath()
        {
            string infoLeft_imgDiv = CutInfoLeft_ImgDiv;
            int beginSrc = infoLeft_imgDiv.IndexOf("src=\"", index);
            int endSrc = infoLeft_imgDiv.IndexOf("\"", beginSrc + 5);
            int count = endSrc - beginSrc - 5;
            string src = CutInfoLeft_ImgDiv.Substring(beginSrc + 5, count);
            index = endSrc;
            return src;
        }

        public string srctppabs()
        {
            string infoLeft_imgDiv = CutInfoLeft_ImgDiv;
            int beginTppabs = infoLeft_imgDiv.IndexOf("tppabs=\"", index);

            int endTppabs = infoLeft_imgDiv.IndexOf("\"", beginTppabs + 8);
            int count = endTppabs - beginTppabs - 8;
            string srctppabs = CutInfoLeft_ImgDiv.Substring(beginTppabs + 8, count);
            if (beginTppabs == -1)
            {
                return "-1";
            }
            return srctppabs;
        }



        #region 查找<span class='archive_more'>
        public int Cutarchive_more()
        {
            string more = newHtml;
            int duo = "<span class='archive_more'>".Length;
            int index = more.IndexOf("<span class=\'archive_more\'>");
            if (index == -1)
            {
                return -1;
            }
            int endindex = more.IndexOf("</span>", index);
            int result = endindex - index - duo;
            string endtxt = more.Substring(index + duo, result);
            archive_more = endtxt;
            return -2;
            
        }

        public void moreTppabsReplacemoreHref()
        {
            string href = moreHref();
            string tppabs = moreTppabs();
            if (tppabs.Equals("-1"))
            {
                return;
            }
            string a = moreA();
            int url = "http://www.zngirls.com/girl/".Length;
            string newUrl = tppabs.Substring(url);
            newUrl = "../" + newUrl + "index.htm";

            string olds = string.Format("<a style=\'text-decoration: none\' href=\"{0}\" title=\'全部图片\' class=\'title\'>{1}</a>", newUrl,a);

            newHtml = newHtml.Replace(archive_more, olds);
        }



        int moreIndex = 0;


        public string moreHref()
        {
            string a_more = archive_more;
            int beginhref = a_more.IndexOf("href=\"");
            int endhref = a_more.IndexOf("\"", beginhref + 6);
            int count = endhref - beginhref - 6;
            string href = archive_more.Substring(beginhref + 6, count);
            moreIndex = endhref;
            return href;
        }
        public string moreTppabs()
        {
            string t_more = archive_more;
            int beginTppabs1 = t_more.IndexOf("tppabs=\"", moreIndex);
            int endTppabs1 = t_more.IndexOf("\"", beginTppabs1 + 8);

            int count = endTppabs1 - beginTppabs1 - 8;
            string tppabs = archive_more.Substring(beginTppabs1 + 8, count);
            index = endTppabs1;
            if (endTppabs1 == -1)
            {
                return "-1";
            }
            return tppabs;
        }
        public string moreA()
        {
            string a_more = archive_more;
            int begina = a_more.IndexOf(">", moreIndex);
            int enda = a_more.IndexOf("</a>", begina);
            int count = enda - begina - 1;
            string a = a_more.Substring(begina + 1, count);
            return a;
        }
        #endregion





        #endregion


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
