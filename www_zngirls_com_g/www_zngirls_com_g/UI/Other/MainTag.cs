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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }





        List<PicFile> files = new List<PicFile>();
        //List<FileUrl> tag_divList = new List<FileUrl>();  //替换utag
        List<FileUrl> tag_divCount = new List<FileUrl>();//替换hgallery
        Dictionary<string, List<FileUrl>> tag_div = new Dictionary<string, List<FileUrl>>();


        List<string> tag_divList = new List<string>();//保存tag_div中html代码


        string allhtml = "";//读取全部网页内容

        string newHtml = "";//生成后最新网页内容

        string divHTMLtdiv = "";//读取整个div


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

            //dataGridView1.DataSource = files;


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
                    Gettdiv();        //提取< div id = "tdiv" > 
                    replaceTag_div();    //替换utag

                    sr.Close();
                    fs.Close();

                    FileStream nfs = new FileStream(item.FilePath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(nfs);
                    sw.Write(newHtml);
                    sw.Close();
                    nfs.Close();
                    count++;
                    divHTMLtdiv = "";
                }
                catch (Exception)
                {
                    errorCount++;
                }
            }
            MessageBox.Show("正确数量:" + count + "\t错误数量:" + errorCount);
            //textBox2.Text = allhtml;
            //Application.Exit();
        }

        public void Gettdiv()
        {
            string stxt2 = allhtml;
            int index = 0;
            int endindex = 0;
            int result = 0;
            string endtxt = "";

            int shu = getTag_divCount(allhtml);
            int count = 0;
            for (int i = 0; i < allhtml.Length; i++)
            {
                index = allhtml.IndexOf("<div class=\'tag_div\'>", i);//<div id="tdiv"><div class='tag_div'>

                endindex = allhtml.IndexOf("</div>", index);

                result = endindex - index;
                endtxt = allhtml.Substring(index, result + 6);

                i = endindex;
                tag_divList.Add(endtxt);
                divHTMLtdiv = divHTMLtdiv + endtxt;
                count++;
                if (count == shu)
                {
                    break;
                }
            }

        }

        public void replaceTag_div()
        {

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
            int cishu = 0;


            int beginzngirls = 0;

            string strreplace = "";

            int urlCount = 0;//网址数量

            string strLi = "";

            //查找a标签位置
            int begina = 0;
            int enda = 0;
            int straCount = 0;

            string strtitle = "";//标题
            urlCount = "http://www.zngirls.com/tag".Length;
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < tag_divList.Count; c++)
            {
                cishu = getuTag_divListCount(tag_divList[c]);

                sb.Append("<div class=\'tag_div\'><ul><li>");

                for (int i = 0; i < tag_divList[c].Length; i++)
                {
                    //href
                    beginhref = tag_divList[c].IndexOf("href=\"", i);
                    beginhref = beginhref + 6;
                    endhref = tag_divList[c].IndexOf("\"", beginhref);

                    //tppabs
                    begintppabs = tag_divList[c].IndexOf("tppabs=\"", endhref);
                    begintppabs = begintppabs + 8;

                    endtppabs = tag_divList[c].IndexOf("\"", begintppabs);

                    //www.zngirls.com
                    beginzngirls = tag_divList[c].IndexOf("http://www.zngirls.com/tag", i);

                    //查找">
                    begina = tag_divList[c].IndexOf("\">", beginzngirls + 2);
                    begina = begina + 2;
                    enda = tag_divList[c].IndexOf("</a>", begina);

                    if (beginhref > 0 && endhref > 0 && endhref > beginhref && endtppabs > 0)
                    {
                        count++;

                        strhrefCount = endhref - beginhref;

                        strtppabsCount = endtppabs - begintppabs;

                        straCount = enda - begina;

                        strhref = tag_divList[c].Substring(beginhref, strhrefCount);

                        strtppabs = tag_divList[c].Substring(begintppabs, strtppabsCount);

                        strtitle = tag_divList[c].Substring(begina, straCount);


                        beginzngirls = beginzngirls + urlCount;
                        zngirlsCount = endtppabs - beginzngirls;
                        strreplace = tag_divList[c].Substring(beginzngirls, zngirlsCount);
                        strreplace = strreplace + "/index.htm";
                        strreplace = ".." + strreplace;


                        //先找<a>标签
                        strLi = string.Format("<a href=\"{0}\">{1}</a>", strreplace, strtitle);

                        sb.Append(strLi);

                        i = beginzngirls;
                        beginhref = 0;
                        endhref = 0;
                        strreplace = "";
                        strtitle = "";
                    }
                    if (count == cishu)
                    {
                        count = 0;
                        break;
                    }
                }
                sb.Append("</li></ul></div>");

            }

            tag_divList = new List<string>();
            textBox3.Text = sb.ToString();


            string allString = allhtml;
            string oldString = divHTMLtdiv;

            newHtml = allString.Replace(oldString, sb.ToString());
            sb = new StringBuilder();
        }



        #region  统计超连接有多少个

        public int getuTag_divListCount(string s)
        {
            int count = 0;
            int index = 0;
            for (int i = 0; i < s.Length; i++)
            {
                index = s.IndexOf("<a href=\"", i);
                if (index != -1)
                {
                    count++;
                    i = index + 21;
                }
                if (index == -1)
                {
                    break;
                }
            }
            return count;

        }

        #endregion





        #region 统计<div class='tag_div'>个数
        public int getTag_divCount(string s)
        {
            int count = 0;
            int index = 0;
            for (int i = 0; i < s.Length; i++)
            {
                index = s.IndexOf("<div class=\'tag_div\'>", i);
                if (index != -1)
                {
                    count++;
                    i = index + 21;
                }
                if (index == -1)
                {
                    break;
                }
            }
            return count;
        }
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
