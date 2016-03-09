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
using System.Threading;
namespace www_zngirls_com_g
{
    public partial class MainGirlDown : Form
    {
        public MainGirlDown()
        {
            InitializeComponent();
            label2.Text = "";
            label4.Text = "";
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                button1.Enabled = true;
                GirlPath = textBox2.Text;
            }
        }


        string GirlPath = "";
        string GirlPathAlbum = "";
        string htmlStr = "";//整个网页字符串
        string RootPath = "";
        private void button1_Click(object sender, EventArgs e)
        {
            //显示下载内容


            string s = textBox2.Text;
            int beginIndex = Convert.ToInt32(textBox3.Text);
            int sindex = s.IndexOf("zngirls");
            RootPath = s.Substring(0, sindex + "zngirls".Length);
            //MessageBox.Show(RootPath);
            GirlPath = GirlPath + @"\www.zngirls.com\girl\";
            string strNo = textBox1.Text;

            int error = 0;
            for (int i = beginIndex; i <= Convert.ToInt32(strNo); i++)
            {
                try
                {
                    string url = string.Format("http://www.zngirls.com/girl/{0}/index.html", i);
                    htmlStr = DownFile.GetHtmlStr(url, "UTF8");
                    error = htmlStr.IndexOf("该页面未找到");
                    if (error == -1)
                    {
                        //提取女神信息
                        extractInfo();
                        GirlPath = GirlPath + i;
                        DirectoryInfo dinfo = new DirectoryInfo(GirlPath);
                        dinfo.Create();//创建目录

                        replaceAll();//替换全部

                        DownFile.createHtml(htmlStr, GirlPath);
                        label2.Text = GirlPath + "下载成功!";
                    }
                }
                catch (Exception)
                {

                }
                GirlPath = textBox2.Text + @"\www.zngirls.com\girl\";
            }
        }
        public void replaceAll()
        {
            styleReplace();//样式替换

            topwelcome();//访问留言

            topnav();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            search_box();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院

            browse();//现在位置： 首页 > 宅男女神 

            replaceImg_zngirls_com();//人物信息



            infoleft_imgdiv();//替换人物头像图片

            post_entry();//最近热门

            photo_ul();//图片写真

            other_girlli();//相关文章

            archive_more();//读取更多内容   下载子页

            ali_img_div();//热门文章图片

            tab_widget1();//热门高清套图<ul id="tab-widget1">

            footer_top();//

            xoxoblogroll();//友情链接

            replaceImg_zngirls_com();//去掉链接

            replaceData_Original();//
        }


        #region 样式替换
        public void styleReplace()
        {
            htmlStr = htmlStr.Replace("href=\"http://res.zngirls.com/style/site.css\"", "href=\"../../../res.zngirls.com/style/site.css\"");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>", "");
            htmlStr = htmlStr.Replace("<script type=\'text/javascript\' src=\'http://images.sohu.com/cs/jsfile/js/c.js\'></script>", "");

            htmlStr = htmlStr.Replace("欢迎访问 <a href=\"#\" title=\"宅男女神\">宅男女神</a>", "欢迎访问 <a href=\"../../index.htm\" title=\"宅男女神\">宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\'#\'>宅男女神</a>资料", "<a href=\'../../tag/index.htm\'>宅男女神</a>资料");



            string strHtml = htmlStr;
            strHtml = FileSubstring.getContent(strHtml, "<script>window._bd_share_config", "</script>");
            htmlStr = htmlStr.Replace(strHtml, "");

            strHtml = FileSubstring.getContent(htmlStr, "<div class=\"div_pk\">", "</div>");
            //MessageBox.Show(strHtml);
            htmlStr = htmlStr.Replace(strHtml, "");
        }
        #endregion

        #region topwelcome部分
        public void topwelcome()
        {
            htmlStr = htmlStr.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"../../message/index.htm\">访客留言</a>");
        }
        #endregion

        #region topnav上部导航部分
        public void topnav()
        {
            //首页
            htmlStr = htmlStr.Replace("<a href=\"/\" title=\"首  页\" class=\"home\"></a>", "<a href=\"../../index.htm\" title=\"首  页\" class=\"home\"></a>");

            //最新情报
            htmlStr = htmlStr.Replace("<a href=\"/article/\">最新情报</a>", "<a href=\"../../article/index.htm\">最新情报</a>");

            //宅男女神专区
            htmlStr = htmlStr.Replace("<a href=\"/find/\">宅男女神专区</a>", "<a href=\"../../find/index.htm\">宅男女神专区</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/neidi/\">内地宅男女神</a>", "<a href=\"../../tag/neidi/index.htm\">内地宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/taiwan/\">台湾宅男女神</a>", "<a href=\"../../tag/taiwan/index.htm\">台湾宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/riben/\">日本宅男女神</a>", "<a href=\"../../tag/riben/index.htm\">日本宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/yazhou/\">亚洲宅男女神</a>", "<a href=\"../../tag/yazhou/index.htm\">亚洲宅男女神</a>");

            //美女图片
            htmlStr = htmlStr.Replace("<a href=\"/gallery/\">美女图片</a>", "<a href=\"../../gallery/index.htm\">美女图片</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/tuigirl/\">推女郎</a>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/tgod/\">推女神</a>", "<a href=\"../../gallery/tgod/index.htm\">推女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/ugirl/\">尤果Ugirl</a>", "<a href=\"../../gallery/ugirl/index.htm\">尤果Ugirl</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/xiuren/\">秀人</a>", "<a href=\"../../gallery/xiuren/index.htm\">秀人</a>");

            //宅男女神排行榜
            htmlStr = htmlStr.Replace("<a href=\"/rank/\">宅男女神排行榜</a>", "<a href=\"../../rank/index.htm\">宅男女神排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/neidi/\">内地排行榜</a>", "<a href=\"../../rank/neidi/index.htm\">内地排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/taiwan/\">台湾排行榜</a>", "<a href=\"../../rank/taiwan/index.htm\">台湾排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/riben/\">日本排行榜</a>", "<a href=\"../../rank/riben/index.htm\">日本排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/yazhou/\">亚洲排行榜</a>", "<a href=\"../../rank/yazhou/index.htm\">亚洲排行榜</a>");

            //女神大PK
            htmlStr = htmlStr.Replace("<a href=\"/arena/\">女神大PK</a>", "<a href=\"../../arena/index.htm\">女神大PK</a>");

            //倾城·美人榜
            htmlStr = htmlStr.Replace("<a href=\"/tag/\">倾城·美人榜</a>", "<a href=\"../../tag/index.htm\">倾城·美人榜</a>");

            //巧遇女神
            htmlStr = htmlStr.Replace("<a href=\"/meet/\">巧遇女神</a>", "<a href=\"../../meet/index.htm\">巧遇女神</a>");

            //女神速配
            htmlStr = htmlStr.Replace("<a href=\"/apply/match.aspx\">女神速配</a>", "<a href=\"../../apply/match.aspx\">女神速配</a>");

        }
        #endregion

        #region search_box
        public void search_box()
        {
            htmlStr = htmlStr.Replace("<a title=\"推女郎\" href=\"/gallery/tuigirl/\">推女郎</a>", "<a title=\"推女郎\" href=\"../../gallery/tuigirl/index.htm\">推女郎</a>");
            htmlStr = htmlStr.Replace("<a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a>", "<a title=\"推女神\" href=\"../../gallery/tgod/index.htm\">推女神</a>");
            htmlStr = htmlStr.Replace("<a title=\"美媛馆\" href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a title=\"美媛馆\" href=\"../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            htmlStr = htmlStr.Replace("<a title=\"波萝社\" href=\"/gallery/bololi/\">波萝社</a>", "<a title=\"波萝社\" href=\"../../gallery/bololi/index.htm\">波萝社</a>");
            htmlStr = htmlStr.Replace("<a title=\"魅妍社\" href=\"/gallery/mistar/\">魅妍社</a>", "<a title=\"魅妍社\" href=\"../../gallery/mistar/index.htm\">魅妍社</a>");
            htmlStr = htmlStr.Replace("<a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a>", "<a title=\"优星馆\" href=\"../../gallery/uxing/index.htm\">优星馆</a>");
            htmlStr = htmlStr.Replace("<a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a>", "<a title=\"模范学院\" href=\"../../gallery/mfstar/index.htm\">模范学院</a>");
        }
        #endregion

        #region browse
        public void browse()
        {
            htmlStr = htmlStr.Replace("<a title=\"返回首页\" href=\"/\">首页</a> &gt; <a href=\"/girl/\" title=\"查看更多\">宅男女神</a>", "<a title=\"返回首页\" href=\"../../index.htm\">首页</a> &gt; <a href=\"../index.htm\" title=\"查看更多\">宅男女神</a>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/info.js\" type=\"text/javascript\"></script>", "");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\" ></script>", "");
        }
        #endregion

        #region replaceImg_zngirls_com
        public void replaceImg_zngirls_com()
        {
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>", "");
            htmlStr = htmlStr.Replace("bdPic\" : \"http://img.zngirls.com", "bdPic\" : \"../../../img.zngirls.com");

            htmlStr = htmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
        }
        #endregion

        #region replaceData_Original
        public void replaceData_Original()
        {
            htmlStr = htmlStr.Replace("data-original=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

            htmlStr = htmlStr.Replace("src=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");
            //htmlStr = htmlStr.Replace("<a class=\'igalleryli_link\' href=\'/g/18121/'>", "src='../../../img.zngirls.com/");
        }

        #endregion

        #region  <div class='infoleft_imgdiv'> 替换人物头像图片
        public void infoleft_imgdiv()
        {
            string oldhtml = FileSubstring.getContent(htmlStr, "<div class=\'infoleft_imgdiv\'>", "</div>");

            string oldhrefhtml = FileSubstring.getLabelContent(oldhtml, "href=\'", "\'");

            string oldsrchtml = FileSubstring.getLabelContent(oldhtml, "src=\'", "\'");

            string newhrefhtml = FileSubstring.buildHtml(oldhrefhtml, "http://", "../../../");

            string newsrchtml = FileSubstring.buildHtml(oldsrchtml, "http://", "../../../");

            string newhtml = oldhtml.Replace(oldhrefhtml, newhrefhtml);

            newhtml = newhtml.Replace(oldsrchtml, newsrchtml);

            //下载人物头像
            List<string> SrcPath = FileSubstring.getSrc(oldhtml, "src=\'", "\'", 1);
            List<DownFile> downFile = FileSubstring.addFileList(SrcPath);
            string GirPicPath = RootPath + "\\" + downFile[0].Path + "\\";

            DownFile.WebClientDownFile(downFile, RootPath + "\\");


            htmlStr = htmlStr.Replace(oldhtml, newhtml);
        }

        #endregion

        #region <div class="post_entry"> 最近热门
        public void post_entry()
        {
            string strhtml = htmlStr;

            string str = FileSubstring.getContent(strhtml, "<div class=\"post_entry\"><ul>", "</ul></div>");

            int count = FileSubstring.getLabelCount(str, "<a href=\'");
            List<string> listahref = FileSubstring.getSrc(str, "<a href=\'", "</a>", count);
            int index = 0;
            string strindex = "";
            string endindex = "";
            foreach (string i in listahref)
            {
                index = i.IndexOf("\'");
                strindex = i.Substring(0, index);
                endindex = i.Substring(index);
                strhtml = strhtml.Replace(i, "../.." + strindex + endindex);
            }
            List<string> f_tmb_a = FileSubstring.getSrc(strhtml, "<a class=\'f-tmb-a\' target=\'_blank\' href=\'", "\'>", count);
            foreach (string i in f_tmb_a)
            {
                strhtml = strhtml.Replace("<a class=\'f-tmb-a\' target=\'_blank\' href=\'" + i, "<a class=\'f-tmb-a\' target=\'_blank\' href=\'" + "../.." + i + "index.htm");
            }

            List<string> userHtml = FileSubstring.getSrc(strhtml, "<div><a href=\'", "\'", count);
            foreach (string n in userHtml)
            {
                strhtml = strhtml.Replace("<div><a href=\'" + n, "<div><a href=\'" + n + "index.htm");
            }


            //下载文章所需图片  
            string articlePic = FileSubstring.getContent(strhtml, "<div class=\"post_entry\"><ul>", "</ul></div>");


            int num = FileSubstring.getLabelCount(articlePic, "data-original=\'http://");
            List<string> SrcPath = FileSubstring.getSrc(articlePic, "data-original=\'", "\'", num);
            List<DownFile> downFile = FileSubstring.addFileList(SrcPath);

            DownFile.WebClientDownFile(downFile, RootPath + "\\");



            htmlStr = strhtml;
        }
        #endregion

        #region extractInformation 
        public void extractInfo()
        {
            string strhtml = htmlStr;
            //Extract name 
            string content = FileSubstring.getContent(strhtml, "<div class=\"entry_box\">", "<i class=\"lt\"></i><i class=\"rt\"></i></div>");
            string name = FileSubstring.getLabelContent(content, "<h1 style=\"font-size: 15px\">", "</h1>");
            content = FileSubstring.getContent(strhtml, "<div class=\'infoleft_imgdiv\'>", "</div>");
            string photopath = FileSubstring.getLabelContent(content, "class=\'imglink\' href=\'", "\'");
            //MessageBox.Show(photopath);
            content = FileSubstring.getContent(strhtml, "<div class=\"infodiv\">", "</table>");
            int num = FileSubstring.getLabelCount(content, "<tr>");
            Dictionary<string,string> 
            List<string> tdList = FileSubstring.getSrc(content, "</td><td", "</td>", num);
            tdList = FileSubstring.getTdContent(tdList);

            MessageBox.Show(content);
        }
        #endregion

        #region photo_ul  图片写真 url
        public void photo_ul()
        {
            //查找是什么类型

            string strhtml = htmlStr;
            string oldhtml = "";
            int index = 0;
            index = strhtml.IndexOf("<div class=\'igalleryli_div\'>");
            if (index != -1)
            {
                oldhtml = FileSubstring.getContent(strhtml, "<ul class='photo_ul'>", "</ul>");
                int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'igalleryli_div\'>");

                List<string> listahref = FileSubstring.getSrc(oldhtml, "<a class=\'igalleryli_link\' href=\'", "\'", count);
                //MessageBox.Show(count.ToString());

                string oldh = "";
                string newh = "";
                foreach (string i in listahref)
                {
                    oldh = "<a class=\'igalleryli_link\' href=\'" + i;
                    newh = "<a class=\'igalleryli_link\' href=\'" + "../.." + i + "index.htm";
                    strhtml = strhtml.Replace(oldh, newh);
                }

                List<string> listatitle = FileSubstring.getSrc(oldhtml, "<div class=\'igalleryli_title\'><a href=\'", "\'", count);
                foreach (string i in listatitle)
                {
                    oldh = "<div class=\'igalleryli_title\'><a href=\'" + i;
                    newh = "<div class=\'igalleryli_title\'><a href=\'" + "../.." + i + "index.htm";
                    strhtml = strhtml.Replace(oldh, newh);
                }

                List<string> downJpg = FileSubstring.getSrc(oldhtml, "data-original=\'", "\'", count);
                List<DownFile> downFile = FileSubstring.addFileList(downJpg);

                DownFile.WebClientDownFile(downFile, RootPath + "\\");
            }
            else
            {
                oldhtml = FileSubstring.getContent(strhtml, "<ul class='photo_ul'>", "</ul>");
                int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'p-tmb\'>");

                List<string> listahref = FileSubstring.getSrc(oldhtml, "<a target=\'_blank\' href=\'", "\'", count);


                string oldh = "";
                string newh = "";
                foreach (string i in listahref)
                {
                    oldh = "<a target=\'_blank\' href=\'" + i;
                    newh = "<a target=\'_blank\' href=\'" + "../.." + i + "index.htm";
                    strhtml = strhtml.Replace(oldh, newh);
                }
                List<string> downJpg = FileSubstring.getSrc(oldhtml, "data-original=\'", "\'", count);
                List<DownFile> downFile = FileSubstring.addFileList(downJpg);
                DownFile.WebClientDownFile(downFile, RootPath + "\\");

            }

            htmlStr = strhtml;
        }
        #endregion


        List<DownFile> savedownList = new List<DownFile>();//网页下载列表



        string albumhtml = "";

        #region <span class='archive_more'> 图片写真 共几册
        public void archive_more()
        {
            string content = FileSubstring.getContent(htmlStr, "<span class=\'archive_more\'>", "</span>");
            if (content.Equals(""))
            {
                return;
            }

            string albumUrl = FileSubstring.getLabelContent(content, "href=\'", "\'");

            string replaceUrl = "../.." + albumUrl + "index.htm";
            string newcontent = content.Replace(albumUrl, replaceUrl);
            //MessageBox.Show(albumUrl);
            albumUrl = "http://www.zngirls.com" + albumUrl + "index.htm";

            htmlStr = htmlStr.Replace(content, newcontent);
            if (albumUrl.IndexOf("album") != -1)
            {
                GirlPathAlbum = GirlPath + @"\album\";
                if (!Directory.Exists(GirlPathAlbum))
                {
                    DirectoryInfo dinfo = new DirectoryInfo(GirlPathAlbum);
                    dinfo.Create();//创建目录
                }
                albumhtml = DownFile.GetHtmlStr(albumUrl, "UTF8");
                ReplaceAlbumAll();
                DownFile.createHtml(albumhtml, GirlPathAlbum);
            }
            else
            {

                GirlPathAlbum = GirlPath + @"\gallery\";
                if (!Directory.Exists(GirlPathAlbum))
                {
                    DirectoryInfo dinfo = new DirectoryInfo(GirlPathAlbum);
                    dinfo.Create();//创建目录
                }
                albumUrl = albumUrl.Substring(0, albumUrl.Length - 9);
                albumhtml = DownFile.GetHtmlStr(albumUrl, "UTF8");
                styleAlbumReplace();
                pic_other();
                pic_img_gallery_ad_thumbs();
                ad_image_wrapper();
                pic_con_left();
                pic_midd_warp();//导航
                childfooter_top();
                new_pic_right_bg();//下一图集
                DownFile.createHtml(albumhtml, GirlPathAlbum);


            }
            if (!Directory.Exists(GirlPathAlbum))
            {
                DirectoryInfo dinfo = new DirectoryInfo(GirlPathAlbum);
                dinfo.Create();//创建目录
            }
            //http://www.zngirls.com/girl/10005/gallery

            label4.Text = GirlPathAlbum + "下载成功!";

        }
        #endregion


        #region tab_widget1 热门高清套图
        public void tab_widget1()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(htmlStr, "<ul id=\"tab-widget1\">", "</ul>");

            int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<div class='gli_div'><a href='", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<div class=\'gli_div\'><a href=\'" + i;
                newh = "<div class=\'gli_div\'><a href=\'" + "../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);
            }


            foreach (string i in listahref)
            {
                oldh = "<div class=\'gli_title\'><a href=\'" + i;
                newh = "<div class=\'gli_title\'><a href=\'" + "../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);
            }


            List<string> ReMen = FileSubstring.getSrc(oldhtml, "src=\'", "\'", count);
            List<DownFile> downFile = FileSubstring.addFileList(ReMen);
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
            htmlStr = strhtml;

        }
        #endregion

        #region footer_top 返回首页
        public void footer_top()
        {
            string strhtml = htmlStr;
            strhtml = strhtml.Replace("<a href=\"/gallery/\">高清美女图片</a></li>", "<a href=\"../../gallery/index.htm\">高清美女图片</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎合集</a></li>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎合集</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆合集</a></li>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆合集</a></li>");

            htmlStr = strhtml;

        }
        #endregion

        #region 热门文章图片下载ali_img_div
        public void ali_img_div()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(htmlStr, "<div class=\"tab-inside\"><ul>", "</ul>");
            int articlenum = FileSubstring.getLabelCount(oldhtml, "<div class=\'ali_div\'><a href=\'");
            List<string> articleStr = FileSubstring.getSrc(oldhtml, "<div class=\'ali_div\'><a href=\'", "\'", articlenum);
            string oldh = "";
            string newh = "";
            foreach (string i in articleStr)
            {

                oldh = "<div class=\'ali_div\'><a href=\'" + i;
                newh = "<div class=\'ali_div\'><a href=\'" + "../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);

            }
            htmlStr = strhtml;


            //下载文章所需图片
            string articlehtml = FileSubstring.getContent(htmlStr, "<h3>热门文章</h3><div class=\"tab-inside\"><ul><li class=\'ali\'><div class=\'ali_img_div\'>", "</ul>");
            int num = FileSubstring.getLabelCount(oldhtml, "<div class=\'ali_img_div\'><img src=\'");
            List<string> SrcPath = FileSubstring.getSrc(articlehtml, "<div class=\'ali_img_div\'><img src=\'", "\'", num);
            List<DownFile> downFile = FileSubstring.addFileList(SrcPath);
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }
        #endregion

        #region  xoxoblogroll  友情链接
        public void xoxoblogroll()
        {
            htmlStr = htmlStr.Replace("<li><a title=\"日本宅男女神\" href=\'/tag/riben/\'>日本宅男女神</a></li>", "<li><a title=\"日本宅男女神\" href=\'../../tag/riben/index.htm\'>日本宅男女神</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"推女神\" href=\"/gallery/tgod/\">推女神</a></li>", "<li><a title=\"推女神\" href=\"../../gallery/tgod/index.htm\">推女神</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"尤果Ugril\" href=\"/gallery/ugirl/\">尤果Ugril</a></li>", "<li><a title=\"尤果Ugril\" href=\"../../gallery/ugirl/index.htm\">尤果Ugril</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"波萝社\" href=\"/gallery/bololi/\">美媛馆</a></li>", "<li><a title=\"波萝社\" href=\"../../gallery/bololi/index.htm\">波萝社</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"模范学院\" href=\"/gallery/mfstar/\">美媛馆</a></li>", "<li><a title=\"模范学院\" href=\"../../gallery/mfstar/index.htm\">模范学院</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"4K-Star\" href=\"/gallery/4kstar/\">4K-Star</a></li>", "<li><a title=\"4K-Star\" href=\"../../gallery/4kstar/index.htm\">4K-Star</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"RQ-Star\" href=\"/gallery/rqstar/\">RQ-Star</a></li>", "<li><a title=\"RQ-Star\" href=\"../../gallery/rqstar/index.htm\">RQ-Star</a></li>");

        }
        #endregion

        #region other_girlli 相关文章
        public void other_girlli()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(htmlStr, "<div class=\'post_entry\'><ul><li class=\'other_girlli\'>", "</div>");

            //MessageBox.Show(oldhtml);
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'other_girlli\'>");

            List<string> listahrefdan = FileSubstring.getSrc(oldhtml, "<li class=\'other_girlli\'><a href=\'", "</a></li>", count);

            string oldh = "";
            string newh = "";


            int index = 0;
            string beginStr = "";
            string endStr = "";
            foreach (string i in listahrefdan)
            {
                oldh = i;
                index = i.IndexOf("\'");
                beginStr = i.Substring(0, index);
                endStr = i.Substring(index);
                newh = "../.." + (beginStr + "index.htm" + endStr);

                strhtml = strhtml.Replace(oldh, newh);
            }
            htmlStr = strhtml;
        }
        #endregion









        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string path = folderBrowserDialog1.SelectedPath;
            int index = path.IndexOf("zngirls");
            if (index != -1)
            {
                GirlPath = path;
                textBox2.Text = GirlPath;
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                textBox2.Text = "";
                MessageBox.Show("路径错误");
            }

        }

        public void pic_con_left()
        {
            string strhtml = albumhtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\"pic_con_left\">", "</div>");
            //MessageBox.Show(oldhtml);
            int count = FileSubstring.getLabelCount(oldhtml, "href=\"http://");

            List<string> listhrefdan = FileSubstring.getSrc(oldhtml, "href=\"http://", "\"", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listhrefdan)
            {
                oldh = "href=\"http://" + i;
                newh = "href=\"" + "../../../../" + i;
                strhtml = strhtml.Replace(oldh, newh);
            }
            albumhtml = strhtml;
        }

        public void ReplaceAlbumAll()
        {
            styleAlbumReplace();//样式替换

            topAlbumwelcome();//访问留言

            topAlbumnav();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            search_boxAlbum();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院

            browseAlbum();//现在位置： 首页 > 宅男女神 

            photo_ulAlbum();//人物信息

            DownPic();//下载所有图片


            tab_widgetAlbum();//热门高清套图

            childfooter_top();//设置

            blogroll();//友情b链接

            replaceData_OriginalAlbum();//替换路径

            downWidgetAlbum();//下载热门高清图片
        }

        public void DownPic()
        {
            string strhtml = albumhtml;
            //下载文章所需图片
            string articlehtml = FileSubstring.getContent(albumhtml, "<div id=\"photo_list\"><ul class=\'photo_ul\'>", "</ul>");
            int num = FileSubstring.getLabelCount(articlehtml, "<li class=\'igalleryli\'><div class=\'igalleryli_div\'>");
            List<string> SrcPath = FileSubstring.getSrc(articlehtml, "src=\'", "\'", num);
            List<DownFile> downFile = FileSubstring.addFileList(SrcPath);
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }


        public void styleAlbumReplace()
        {
            albumhtml = albumhtml.Replace("<link href=\"/style/gallery-new.css\"", "<link href=\"../../../style/gallery-new.css\"");
            albumhtml = albumhtml.Replace("<link href=\"/style/gallery.css\"", "<link href=\"../../../style/gallery.css\"");
            albumhtml = albumhtml.Replace("<script src=\"/script/jquery-min.js\"", "<script src=\"../../../script/jquery-min.js\"");
            albumhtml = albumhtml.Replace("<script src=\"/script/jquery.effect.min.js\"", "<script src=\"../../../script/jquery.effect.min.js\"");
            albumhtml = albumhtml.Replace("<link href=\"/style/gallery_min.css\"", "<link href=\"../../../style/gallery_min.css\"");
            albumhtml = albumhtml.Replace("<script src=\"/script/gallery_old.js\"", "<script src=\"../../../script/gallery_old.js\"");
            albumhtml = albumhtml.Replace("<script src=\"/script/ad_side.js\"", "<script src=\"../../../script/ad_side.js\"");

            albumhtml = albumhtml.Replace("<link href=\"http://res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />", "<link href=\"../../../../res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />");
            albumhtml = albumhtml.Replace("<script src=\"http://res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>", "<script src=\"../../../../res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>");
            albumhtml = albumhtml.Replace("src=\"http://res.zngirls.com/style/HotNewspro/images/go.gif\"", "src=\"../../../../res.zngirls.com/style/HotNewspro/images/go.gif\"");
            albumhtml = albumhtml.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
        }

        #region topwelcome部分
        public void topAlbumwelcome()
        {
            albumhtml = albumhtml.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"../../../message/index.htm\">访客留言</a>");
        }
        #endregion

        public void topAlbumnav()
        {
            //首页
            albumhtml = albumhtml.Replace("<a href=\"/\" title=\"首  页\" class=\"home\"></a>", "<a href=\"../../../index.htm\" title=\"首  页\" class=\"home\"></a>");

            //最新情报
            albumhtml = albumhtml.Replace("<a href=\"/article/\">最新情报</a>", "<a href=\"../../../article/index.htm\">最新情报</a>");

            //宅男女神专区
            albumhtml = albumhtml.Replace("<a href=\"/find/\">宅男女神专区</a>", "<a href=\"../../../find/index.htm\">宅男女神专区</a>");
            albumhtml = albumhtml.Replace("<a href=\"/tag/neidi/\">内地宅男女神</a>", "<a href=\"../../../tag/neidi/index.htm\">内地宅男女神</a>");
            albumhtml = albumhtml.Replace("<a href=\"/tag/taiwan/\">台湾宅男女神</a>", "<a href=\"../../../tag/taiwan/index.htm\">台湾宅男女神</a>");
            albumhtml = albumhtml.Replace("<a href=\"/tag/riben/\">日本宅男女神</a>", "<a href=\"../../../tag/riben/index.htm\">日本宅男女神</a>");
            albumhtml = albumhtml.Replace("<a href=\"/tag/yazhou/\">亚洲宅男女神</a>", "<a href=\"../../../tag/yazhou/index.htm\">亚洲宅男女神</a>");

            //美女图片
            albumhtml = albumhtml.Replace("<a href=\"/gallery/\">美女图片</a>", "<a href=\"../../../gallery/index.htm\">美女图片</a>");
            albumhtml = albumhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎</a>", "<a href=\"../../../gallery/tuigirl/index.htm\">推女郎</a>");
            albumhtml = albumhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a href=\"../../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            albumhtml = albumhtml.Replace("<a href=\"/gallery/tgod/\">推女神</a>", "<a href=\"../../../gallery/tgod/index.htm\">推女神</a>");
            albumhtml = albumhtml.Replace("<a href=\"/gallery/ugirl/\">尤果Ugirl</a>", "<a href=\"../../../gallery/ugirl/index.htm\">尤果Ugirl</a>");
            albumhtml = albumhtml.Replace("<a href=\"/gallery/xiuren/\">秀人</a>", "<a href=\"../../../gallery/xiuren/index.htm\">秀人</a>");

            //宅男女神排行榜
            albumhtml = albumhtml.Replace("<a href=\"/rank/\">宅男女神排行榜</a>", "<a href=\"../../../rank/index.htm\">宅男女神排行榜</a>");
            albumhtml = albumhtml.Replace("<a href=\"/rank/neidi/\">内地排行榜</a>", "<a href=\"../../../rank/neidi/index.htm\">内地排行榜</a>");
            albumhtml = albumhtml.Replace("<a href=\"/rank/taiwan/\">台湾排行榜</a>", "<a href=\"../../../rank/taiwan/index.htm\">台湾排行榜</a>");
            albumhtml = albumhtml.Replace("<a href=\"/rank/riben/\">日本排行榜</a>", "<a href=\"../../../rank/riben/index.htm\">日本排行榜</a>");
            albumhtml = albumhtml.Replace("<a href=\"/rank/yazhou/\">亚洲排行榜</a>", "<a href=\"../../../rank/yazhou/index.htm\">亚洲排行榜</a>");

            //女神大PK
            albumhtml = albumhtml.Replace("<a href=\"/arena/\">女神大PK</a>", "<a href=\"../../../arena/index.htm\">女神大PK</a>");

            //倾城·美人榜
            albumhtml = albumhtml.Replace("<a href=\"/tag/\">倾城·美人榜</a>", "<a href=\"../../../tag/index.htm\">倾城·美人榜</a>");

            //巧遇女神
            albumhtml = albumhtml.Replace("<a href=\"/meet/\">巧遇女神</a>", "<a href=\"../../../meet/index.htm\">巧遇女神</a>");

            //女神速配
            albumhtml = albumhtml.Replace("<a href=\"/apply/match.aspx\">女神速配</a>", "<a href=\"../../../apply/match.aspx\">女神速配</a>");

        }
        #region search_box
        public void search_boxAlbum()
        {
            albumhtml = albumhtml.Replace("<a title=\"推女郎\" href=\"/gallery/tuigirl/\">推女郎</a>", "<a title=\"推女郎\" href=\"../../../gallery/tuigirl/index.htm\">推女郎</a>");
            albumhtml = albumhtml.Replace("<a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a>", "<a title=\"推女神\" href=\"../../../gallery/tgod/index.htm\">推女神</a>");
            albumhtml = albumhtml.Replace("<a title=\"美媛馆\" href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a title=\"美媛馆\" href=\"../../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            albumhtml = albumhtml.Replace("<a title=\"波萝社\" href=\"/gallery/bololi/\">波萝社</a>", "<a title=\"波萝社\" href=\"../../../gallery/bololi/index.htm\">波萝社</a>");
            albumhtml = albumhtml.Replace("<a title=\"魅妍社\" href=\"/gallery/mistar/\">魅妍社</a>", "<a title=\"魅妍社\" href=\"../../../gallery/mistar/index.htm\">魅妍社</a>");
            albumhtml = albumhtml.Replace("<a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a>", "<a title=\"优星馆\" href=\"../../../gallery/uxing/index.htm\">优星馆</a>");
            albumhtml = albumhtml.Replace("<a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a>", "<a title=\"模范学院\" href=\"../../../gallery/mfstar/index.htm\">模范学院</a>");
        }
        #endregion

        public void browseAlbum()
        {
            albumhtml = albumhtml.Replace("<a title=\"返回首页\" href=\"/\">首页</a> &gt; <a href=\"/girl/\" title=\"查看更多\">宅男女神</a>", "<a title=\"返回首页\" href=\"../../../index.htm\">首页</a> &gt; <a href=\"../../index.htm\" title=\"查看更多\">宅男女神</a>");
            albumhtml = albumhtml.Replace("<script src=\"http://res.zngirls.com/script/info.js\" type=\"text/javascript\"></script>", "");
            albumhtml = albumhtml.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");



            albumhtml = albumhtml.Replace("<a title=\"返回首页\" href=\"/\">首页</a>", "<a title=\"返回首页\" href=\"../../../index.htm\">首页</a>");

            string oldstring = albumhtml;
            string newstringreplace = FileSubstring.getContent(oldstring, "<div class=\"browse\">", "</div>");
            string oldstringreplace = newstringreplace;
            int num = FileSubstring.getLabelCount(newstringreplace, "<a href=\"");
            List<string> list = FileSubstring.getSrc(newstringreplace, "<a href=\"", "\"", num);
            string newstring = "../../.." + list[0] + "index.htm";
            newstringreplace = newstringreplace.Replace(list[0], newstring);

            newstring = list[1].Replace("#", "gallery/index.htm");

            newstringreplace = newstringreplace.Replace(list[1], newstring);
            albumhtml = albumhtml.Replace(oldstringreplace, newstringreplace);

            string old = FileSubstring.getContent(albumhtml, "<button type=\"button\" value=\"她的信息\" class=\"girl_edit\"", "</button>");
            albumhtml = albumhtml.Replace(old, "");

        }

        #region photo_ul 
        public void photo_ulAlbum()
        {
            string strhtml = albumhtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<ul class='photo_ul'>", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'igalleryli_div\'>");
            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a class=\'igalleryli_link\' href=\'", "\'", count);


            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a class=\'igalleryli_link\' href=\'" + i;
                newh = "<a class=\'igalleryli_link\' href=\'" + "../../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);
            }
            albumhtml = strhtml;
        }
        #endregion

        #region replaceData_Original
        public void replaceData_OriginalAlbum()
        {
            albumhtml = albumhtml.Replace("data-original=\'http://t1.zngirls.com/", "src='../../../../t1.zngirls.com/");
            albumhtml = albumhtml.Replace("data-original=\'http://t2.zngirls.com/", "src='../../../../t1.zngirls.com/");
            albumhtml = albumhtml.Replace("data-original=\'http://img.zngirls.com/", "src='../../../../img.zngirls.com/");

            albumhtml = albumhtml.Replace("src=\'http://img.zngirls.com/", "src='../../../../img.zngirls.com/");
            albumhtml = albumhtml.Replace("src=\'http://t1.zngirls.com/", "src='../../../../t1.zngirls.com/");
            albumhtml = albumhtml.Replace("src=\'http://t2.zngirls.com/", "src='../../../../t1.zngirls.com/");
        }

        #endregion

        #region 热门女神写真
        public void pic_other()
        {
            string strhtml = albumhtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<ul class=\'photo_ul\'>", "</ul>");
            //MessageBox.Show(oldhtml);
            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'p-tmb\'>");

            List<string> listhrefdan = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listhrefdan)
            {
                oldh = "<a target=\'_blank\' href=\'" + i;
                newh = "<a target=\'_blank\' href=\'" + "../../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);
            }

            List<string> srchttp = FileSubstring.getSrc(oldhtml, "src=\'http://", "\'", count);
            foreach (string i in srchttp)
            {
                oldh = "src=\'http://" + i;
                newh = "src=\'" + "../../../../" + i;
                strhtml = strhtml.Replace(oldh, newh);
            }
            albumhtml = strhtml;
        }
        #endregion

        #region <div class="pic_img_gallery ad-thumbs">显示和下载图片
        public void pic_img_gallery_ad_thumbs()
        {
            string strhtml = albumhtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<ul class=\"ad-thumb-list\"", "</ul>");
            //MessageBox.Show(oldhtml);
            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'inner\'><a href=\'");

            List<string> listhrefdan = FileSubstring.getSrc(oldhtml, "href=\'http://", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listhrefdan)
            {
                oldh = "<a href=\'http://" + i;
                newh = "<a href=\'" + "../../../../" + i;
                strhtml = strhtml.Replace(oldh, newh);
            }

            List<string> srchttp = FileSubstring.getSrc(oldhtml, "src=\'http://", "\'", count);
            foreach (string i in srchttp)
            {
                oldh = "src=\'http://" + i;
                newh = "src=\'" + "../../../../" + i;
                strhtml = strhtml.Replace(oldh, newh);
            }
            //下载文章所需图片
            List<DownFile> downDa = FileSubstring.addFileList(listhrefdan);
            DownFile.WebClientDownFile(downDa, RootPath + "\\");

            List<DownFile> downFile = FileSubstring.addFileList(srchttp);
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
            albumhtml = strhtml;
        }

        #endregion

        #region ad-image-wrapper 播放
        public void ad_image_wrapper()
        {
            string strhtml = albumhtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\"ad-image-wrapper\"", "</div>");
            string newhtml = "";
            //MessageBox.Show(oldhtml);
            int count = FileSubstring.getLabelCount(oldhtml, "<img src=\"");

            List<string> listhrefdan = FileSubstring.getSrc(oldhtml, "<img src=\"http://", "\"", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listhrefdan)
            {
                oldh = "src=\"http://" + i;
                newh = "src=\"" + "../../../../" + i;
                newhtml = oldhtml.Replace(oldh, newh);
            }
            strhtml = strhtml.Replace(oldhtml, newhtml);
            albumhtml = strhtml;
        }
        #endregion


        #region new_pic_right_bg 下一图集
        public void new_pic_right_bg()
        {
            string strhtml = albumhtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\"new_pic_right_bg\">", "</div>");
            //MessageBox.Show(oldhtml);
            int count = FileSubstring.getLabelCount(oldhtml, "<img src=\"");

            List<string> listhrefdan = FileSubstring.getSrc(oldhtml, "<img src=\"", "\"", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listhrefdan)
            {
                oldh = "<img src=\"" + i;
                newh = "<img src=\"" + "../../../../img.zngirls.com" + i;
                strhtml = strhtml.Replace(oldh, newh);
            }
            List<string> hreflist = FileSubstring.getSrc(oldhtml, "<a href=\"", "\"", count);
            foreach (string i in listhrefdan)
            {
                oldh = "<a href=\"" + i;
                newh = "<a href=\"" + "../../../../img.zngirls.com" + i;
                strhtml = strhtml.Replace(oldh, newh);
            }
            albumhtml = strhtml;
        }
        #endregion

        #region 菜单
        public void pic_midd_warp()
        {
            albumhtml = albumhtml.Replace("<a title=\"返回首页\" href=\"/\">首页</a>", "<a title=\"返回首页\" href=\"../../../index.htm\">首页</a>");
            albumhtml = albumhtml.Replace("<a href=\"/advance/\" title=\"查看更多\">", "<a href=\"../../../advance/index.htm\" title=\"查看更多\">");

        }
        #endregion

        public void childfooter_top()
        {
            albumhtml = albumhtml.Replace("<a href=\"/gallery/\">高清美女图片</a></li>", "<a href=\"../../gallery/index.htm\">高清美女图片</a></li>");
            albumhtml = albumhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎合集</a></li>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎合集</a></li>");
            albumhtml = albumhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆合集</a></li>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆合集</a></li>");



            string delStr = FileSubstring.getContent(albumhtml, "var _bdhmProtocol", "));");
            if (delStr.Length > 0)
            {
                albumhtml = albumhtml.Replace(delStr, "");
            }
        }

        public void blogroll()
        {
            albumhtml = albumhtml.Replace("<li><a title=\"日本宅男女神\" href=\'/tag/riben/\'>日本宅男女神</a></li>", "<li><a title=\"日本宅男女神\" href=\'../../../tag/riben/index.htm\'>日本宅男女神</a></li>");
            albumhtml = albumhtml.Replace("<li><a title=\"推女神\" href=\"/gallery/tgod/\">推女神</a></li>", "<li><a title=\"推女神\" href=\"../../../gallery/tgod/index.htm\">推女神</a></li>");
            albumhtml = albumhtml.Replace("<li><a title=\"尤果Ugril\" href=\"/gallery/ugirl/\">尤果Ugril</a></li>", "<li><a title=\"尤果Ugril\" href=\"../../../gallery/ugirl/index.htm\">尤果Ugril</a></li>");
            albumhtml = albumhtml.Replace("<li><a title=\"波萝社\" href=\"/gallery/bololi/\">美媛馆</a></li>", "<li><a title=\"波萝社\" href=\"../../../gallery/bololi/index.htm\">波萝社</a></li>");
            albumhtml = albumhtml.Replace("<li><a title=\"模范学院\" href=\"/gallery/mfstar/\">美媛馆</a></li>", "<li><a title=\"模范学院\" href=\"../../../gallery/mfstar/index.htm\">模范学院</a></li>");
            albumhtml = albumhtml.Replace("<li><a title=\"4K-Star\" href=\"/gallery/4kstar/\">4K-Star</a></li>", "<li><a title=\"4K-Star\" href=\"../../../gallery/4kstar/index.htm\">4K-Star</a></li>");
            albumhtml = albumhtml.Replace("<li><a title=\"RQ-Star\" href=\"/gallery/rqstar/\">RQ-Star</a></li>", "<li><a title=\"RQ-Star\" href=\"../../../gallery/rqstar/index.htm\">RQ-Star</a></li>");

        }
        /// <summary>
        /// 热门高清套图
        /// </summary>
        public void tab_widgetAlbum()
        {
            //tab - inside
            string strhtml = albumhtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\"tab-inside\">", "</ul>");
            int insidenum = FileSubstring.getLabelCount(oldhtml, "<div class=\'gli_div\'><a href=\'");
            List<string> insideStr = FileSubstring.getSrc(oldhtml, "<div class=\'gli_div\'><a href=\'", "\'", insidenum);
            string oldh = "";
            string newh = "";
            foreach (string i in insideStr)
            {

                oldh = "<div class=\'gli_div\'><a href='" + i;
                newh = "<div class=\'ali_div\'><a href=\'" + "../../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);

            }


            List<string> gli_titleStr = FileSubstring.getSrc(oldhtml, "<div class=\'gli_title\'><a href=\'", "\'", insidenum);
            foreach (string i in gli_titleStr)
            {

                oldh = "<div class=\'gli_title\'><a href=\'" + i;
                newh = "<div class=\'gli_title\'><a href=\'" + "../../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);

            }
            albumhtml = strhtml;
            List<string> picGaoQing = FileSubstring.getSrc(oldhtml, "src=\'", "\'", insidenum);

            //下载热门高清套图
            List<DownFile> downDa = FileSubstring.addFileList(picGaoQing);
            DownFile.WebClientDownFile(downDa, RootPath + "\\");
        }
        public void downWidgetAlbum()
        {
        }

        private void MainGirlDown_Load(object sender, EventArgs e)
        {
            textBox2.Text = PageInfo.path;
            textBox1.Text = PageInfo.girl;
            //读取最新文件夹
            ReadLabelNum();
        }
        #region 初始化读取编号
        public void ReadLabelNum()
        {
            string path = PageInfo.path;
            path = path + "/www.zngirls.com/girl/";
            DirectoryInfo dinfo = new DirectoryInfo(path);
            DirectoryInfo[] childDinfo = dinfo.GetDirectories();
            int count = childDinfo.Length;
            string name = childDinfo[count - 1].Name;
            textBox3.Text = name;

        }
        #endregion
    }
}
