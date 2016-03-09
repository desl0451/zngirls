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
    public partial class MainGallery : Form
    {
        public MainGallery()
        {
            InitializeComponent();
        }
        string htmlStr = "";

        string childHtmlStr = "";
        string SavePath = "";

        string SaveGallery = "";

        private void DownChildPage()
        {
            string childIndexUrl = "http://www.zngirls.com/gallery/";
            string childDownUrl = "";
            int num = 0;
            //下载gallery目录下其它页面
            for (int i = 1; i < 1000; i++)
            {
                childDownUrl = childIndexUrl + i + ".html";
                htmlStr = DownFile.GetHtmlStr(childDownUrl, "UTF8");//下载首页index.html
                SaveGallery = PageInfo.path + "\\www.zngirls.com\\gallery\\" + i + ".html";
                previousPageNo = i.ToString();
                num = PageCheckGalleryLiCount();
                if (num == 0)
                {
                    break;
                }
                pageNo = i;
                //DownFile.CreateDirectory(SaveGallery);//创建目录
                SaveGalleryReplace();
                DownFile.createChildHtml(htmlStr, SaveGallery);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string indexUrl = "http://www.zngirls.com/gallery/";
            htmlStr = DownFile.GetHtmlStr(indexUrl, "UTF8");//下载首页index.html
            SavePath = PageInfo.path + "\\www.zngirls.com\\gallery\\";
            DownFile.CreateDirectory(SavePath);//创建目录
            IndexPage(); //过滤掉不需要代码
            DownFile.createHtml(htmlStr, SavePath);
            label4.Text = SavePath + "下载完成!";

            DownChildPage();

            //label4.Text = SavePath + "下载完成!";

            downTypeDiv();
        }
        /// <summary>
        /// 替换信息
        /// </summary>
        public void SaveGalleryReplace()
        {
            xoxoblogroll();//友情链接

            styleReplace(); //样式替换

            delhidden();//去掉hidden代码

            topwelcome();//访问留言

            topnav();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            search_box();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院

            browse();//现在位置： 首页 > 宅男女神 

            tag_div_down_child(); //记录并下载子页面

            tag_div();//类别DIV

            galleryli();

            replaceData_Original();//http://res.zngirls.com转换

            parentpagesYY();//分页

            footer_top();//
        }

        #region  首页部分index.html########################################################################################
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////// 
        public void IndexPage()
        {
            xoxoblogroll();//友情链接

            styleReplace(); //样式替换

            delhidden();//去掉hidden代码

            topwelcome();//访问留言

            topnav();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            search_box();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院

            browse();//现在位置： 首页 > 宅男女神 

            tag_div_down_child(); //记录并下载子页面

            tag_div();//类别DIV

            galleryli();

            replaceData_Original();//http://res.zngirls.com转换

            parentpagesYY();//分页

            footer_top();//
        }

        #region 去掉hidden代码
        /// <summary>
        /// 去掉hidden代码
        /// </summary>

        public void delhidden()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<input type=\"hidden\" name=\"__VIEWSTATE\"", "\">");
            if (oldhtml.Length > 0)
                htmlStr = htmlStr.Replace(oldhtml, "");
        }
        #endregion

        #region 样式替换
        /// <summary>
        /// 样式替换
        /// </summary>
        public void styleReplace()
        {
            htmlStr = htmlStr.Replace("<link href=\"http://res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />", "<link href=\"../../res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />");

            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>", "<script src=\"../../res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/carouFredSel.js\"></script>", "<script src=\"../../../res.zngirls.com/script/carouFredSel.js\"></script>");
            htmlStr = htmlStr.Replace("<script type=\'text/javascript\' src=\'http://images.sohu.com/cs/jsfile/js/c.js\'></script>", "");

            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.mousewheel.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.mousewheel.min.js\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");
            htmlStr = htmlStr.Replace("<script src=\"http://libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>", "<script src=\"../../../libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>");
            htmlStr = htmlStr.Replace("http://www.zngirls.com", "");
            htmlStr = htmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
        }
        #endregion

        #region topwelcome部分
        public void topwelcome()
        {
            htmlStr = htmlStr.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"../message/index.htm\">访客留言</a>");
        }
        #endregion

        #region topnav上部导航部分
        public void topnav()
        {

            htmlStr = htmlStr.Replace("<a href=\'#\'>宅男女神</a>资料", "<a href=\'index.htm\'>宅男女神</a>资料");

            //首页
            htmlStr = htmlStr.Replace("<a href=\"/\" title=\"首  页\" class=\"home\"></a>", "<a href=\"../index.htm\" title=\"首  页\" class=\"home\"></a>");

            //最新情报
            htmlStr = htmlStr.Replace("<a href=\"/article/\">最新情报</a>", "<a href=\"../article/index.htm\">最新情报</a>");

            //宅男女神专区
            htmlStr = htmlStr.Replace("<a href=\"/find/\">宅男女神专区</a>", "<a href=\"../find/index.htm\">宅男女神专区</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/neidi/\">内地宅男女神</a>", "<a href=\"../tag/neidi/index.htm\">内地宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/taiwan/\">台湾宅男女神</a>", "<a href=\"../tag/taiwan/index.htm\">台湾宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/riben/\">日本宅男女神</a>", "<a href=\"../tag/riben/index.htm\">日本宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/yazhou/\">亚洲宅男女神</a>", "<a href=\"../tag/yazhou/index.htm\">亚洲宅男女神</a>");

            //美女图片
            htmlStr = htmlStr.Replace("<a href=\"/gallery/\">美女图片</a>", "<a href=\"../gallery/index.htm\">美女图片</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/tuigirl/\">推女郎</a>", "<a href=\"../gallery/tuigirl/index.htm\">推女郎</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a href=\"../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/tgod/\">推女神</a>", "<a href=\"../gallery/tgod/index.htm\">推女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/ugirl/\">尤果Ugirl</a>", "<a href=\"../gallery/ugirl/index.htm\">尤果Ugirl</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/xiuren/\">秀人</a>", "<a href=\"../gallery/xiuren/index.htm\">秀人</a>");

            //宅男女神排行榜
            htmlStr = htmlStr.Replace("<a href=\"/rank/\">宅男女神排行榜</a>", "<a href=\"../rank/index.htm\">宅男女神排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/neidi/\">内地排行榜</a>", "<a href=\"../rank/neidi/index.htm\">内地排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/taiwan/\">台湾排行榜</a>", "<a href=\"../rank/taiwan/index.htm\">台湾排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/riben/\">日本排行榜</a>", "<a href=\"../rank/riben/index.htm\">日本排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/yazhou/\">亚洲排行榜</a>", "<a href=\"../rank/yazhou/index.htm\">亚洲排行榜</a>");

            //女神大PK
            htmlStr = htmlStr.Replace("<a href=\"/arena/\">女神大PK</a>", "<a href=\"../arena/index.htm\">女神大PK</a>");

            //倾城·美人榜
            htmlStr = htmlStr.Replace("<a href=\"/tag/\">倾城·美人榜</a>", "<a href=\"../tag/index.htm\">倾城·美人榜</a>");

            //巧遇女神
            htmlStr = htmlStr.Replace("<a href=\"/meet/\">巧遇女神</a>", "<a href=\"../meet/index.htm\">巧遇女神</a>");

            //女神速配
            htmlStr = htmlStr.Replace("<a href=\"/apply/match.aspx\">女神速配</a>", "<a href=\"../apply/match.aspx\">女神速配</a>");

        }
        #endregion

        #region search_box
        public void search_box()
        {
            htmlStr = htmlStr.Replace("<a title=\"推女郎\" href=\"/gallery/tuigirl/\">推女郎</a>", "<a title=\"推女郎\" href=\"../gallery/tuigirl/index.htm\">推女郎</a>");
            htmlStr = htmlStr.Replace("<a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a>", "<a title=\"推女神\" href=\"../gallery/tgod/index.htm\">推女神</a>");
            htmlStr = htmlStr.Replace("<a title=\"美媛馆\" href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a title=\"美媛馆\" href=\"../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            htmlStr = htmlStr.Replace("<a title=\"波萝社\" href=\"/gallery/bololi/\">波萝社</a>", "<a title=\"波萝社\" href=\"../gallery/bololi/index.htm\">波萝社</a>");
            htmlStr = htmlStr.Replace("<a title=\"魅妍社\" href=\"/gallery/mistar/\">魅妍社</a>", "<a title=\"魅妍社\" href=\"../gallery/mistar/index.htm\">魅妍社</a>");
            htmlStr = htmlStr.Replace("<a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a>", "<a title=\"优星馆\" href=\"../gallery/uxing/index.htm\">优星馆</a>");
            htmlStr = htmlStr.Replace("<a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a>", "<a title=\"模范学院\" href=\"../gallery/mfstar/index.htm\">模范学院</a>");
        }
        #endregion

        #region browse
        public void browse()
        {
            htmlStr = htmlStr.Replace("现在位置： <a title=\"返回首页\" href=\"/\">首页</a>", "现在位置： <a title=\"返回首页\" href=\"../index.htm\">首页</a>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>", "<script src=\"../../res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>");
            htmlStr = htmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
            htmlStr = htmlStr.Replace("http://res.zngirls.com/style/HotNewspro/images/go.gif", "../../res.zngirls.com/style/HotNewspro/images/go.gif");


            htmlStr = htmlStr.Replace("onclick=\"location=\'/find/\'\">找美女</button>", "onclick=\"location=\'../find/index.htm\'\">找美女</button>");
        }
        #endregion

        int pageNo = 0;

        #region tag_div_down_child 记录并下载子页面
        public void tag_div_down_child()
        {
            string strHtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<div id=\"tdiv\"><div class=\'tag_div\'>", "</div></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", count);
            TypeDiv = listahref;
        }

        string previousPageNo = "";
        string nextPage = "";
        //子类别下载
        #endregion

        List<string> TypeDiv = null;

        #region 子类别下载内容
        public void downTypeDiv()
        {
            //循环下载子页面
            string url = "";
            for (int i = 0; i < TypeDiv.Count; i++)
            {
                url = "http://www.zngirls.com" + TypeDiv[i];
                childHtmlStr = DownFile.GetHtmlStr(url, "UTF8");//下载首页index.html
                SavePath = PageInfo.path + "\\www.zngirls.com" + TypeDiv[i];
                DownFile.CreateDirectory(SavePath);//创建目录
                GalleryChild();

                DownFile.createHtml(childHtmlStr, SavePath);
                int num = 0;
                for (int j = 1; j <= 10000; j++)
                {

                    string path = string.Format("http://www.zngirls.com{0}{1}", TypeDiv[i], j + ".html");
                    previousPageNo = j.ToString();
                    nextPage = string.Format("http://www.zngirls.com{0}{1}", TypeDiv[i], (j + 1) + ".html");
                    childHtmlStr = DownFile.GetHtmlStr(path, "UTF8");
                    SavePath = PageInfo.path + "\\www.zngirls.com" + TypeDiv[i];
                    SavePath = SavePath + "/" + j + ".html";
                    num = CheckGalleryLiCount();
                    if (num == 0)
                    {
                        SavePath = "";
                        break;
                    }
                    pageNo = j;
                    GalleryChild();
                    DownFile.createChildHtml(childHtmlStr, SavePath);
                    SavePath = "";
                }
            }
        }
        #endregion
        
        #region tag_div　类别DIV 中国内地台湾日本韩国马来西亚越南泰国混血其他
        public void tag_div()
        {
            string strHtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<div id=\"tdiv\"><div class=\'tag_div\'>", "</div></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a href=\'" + i;
                newh = "<a href=\'" + ".." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            htmlStr = strHtml;
        }
        #endregion

        #region 计算图片数量
        public void galleryli()
        {

            string strHtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<ul><li class=\'galleryli\'>", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'galleryli\'>");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a class=\'galleryli_link\' href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a class=\'galleryli_link\' href=\'" + i;
                newh = "<a class=\'galleryli_link\' href=\'" + ".." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            List<string> listgalleryli_title = FileSubstring.getSrc(strHtml, "<div class=\'galleryli_title\'><a href=\'", "\'", count);

            foreach (string i in listgalleryli_title)
            {
                oldh = "<div class=\'galleryli_title\'><a href=\'" + i;
                newh = "<div class=\'galleryli_title\'><a href=\'" + ".." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }

            htmlStr = strHtml;
        }
        #endregion

        #region parentpagesYY　分页
        public void parentpagesYY()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\'pagesYY\'><div>", "</div>");
            int count = FileSubstring.getLabelCount(oldhtml, "href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                if (i.IndexOf("#") == -1)
                {
                    oldh = "href=\'" + i;
                    newh = "href=\'" + ".." + i;
                    strhtml = strhtml.Replace(oldh, newh);
                }

            }
            if (listahref[0].IndexOf(".html") == -1)
            {
                oldh = "<div><a class='a1' href=\'" + ".." + listahref[0];
                newh = "<div><a class='a1' href=\'" + ".." + listahref[0] + "1.html";
                strhtml = strhtml.Replace(oldh, newh);
            }


            //设置第一页和最后一页问题
            if (listahref[listahref.Count - 1].IndexOf(".html") == -1)
            {
                string page = nextPage;

                //MessageBox.Show(page);
                string countHTML = DownFile.GetHtmlStr(page, "UTF8");//下载首页index.html
                int NUM = checkLast(countHTML);
                if (NUM == 0)
                {
                    oldh = "<a class='a1' href=\'" + ".." + listahref[listahref.Count - 1];
                    newh = "<a class='a1' href=\'" + ".." + listahref[listahref.Count - 1] + "" + previousPageNo + ".html";
                }
                else
                {
                    oldh = "<a class='a1' href=\'" + ".." + listahref[listahref.Count - 1];
                    newh = "<a class='a1' href=\'" + ".." + listahref[listahref.Count - 1] + "" + (pageNo + 1) + ".html";
                }
                strhtml = strhtml.Replace(oldh, newh);
            }




            htmlStr = strhtml;
        }
        #endregion

        #region 返回首页
        public void footer_top()
        {
            string strhtml = htmlStr;
            strhtml = strhtml.Replace("<a href=\"/gallery/\">高清美女图片</a></li>", "<a href=\"../gallery/index.htm\">高清美女图片</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎合集</a></li>", "<a href=\"../gallery/tuigirl/index.htm\">推女郎合集</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆合集</a></li>", "<a href=\"../gallery/meiyuanguan/index.htm\">美媛馆合集</a></li>");

            htmlStr = strhtml;
        }
        #endregion

        #region replaceData_Original http://t1.zngirls.com/
        public void replaceData_Original()
        {
            htmlStr = htmlStr.Replace("data-original=\'http://t1.zngirls.com/", "src='../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://t2.zngirls.com/", "src='../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://img.zngirls.com/", "src='../../img.zngirls.com/");

            htmlStr = htmlStr.Replace("src=\'http://t1.zngirls.com/", "src='../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://t2.zngirls.com/", "src='../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://img.zngirls.com/", "src='../../img.zngirls.com/");

        }
        #endregion

        #region xoxoblogroll 
        public void xoxoblogroll()
        {
            htmlStr = htmlStr.Replace("<li><a title=\"宅男女神\" href=\'#\'>宅男女神</a></li>", "<li><a title=\"宅男女神\" href=\'../index.htm\'>宅男女神</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"日本宅男女神\" href=\'/tag/riben/\'>日本宅男女神</a></li>", "<li><a title=\"日本宅男女神\" href=\'../tag/riben/index.htm\'>日本宅男女神</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"推女神\" href=\"/gallery/tgod/\">推女神</a></li>", "<li><a title=\"推女神\" href=\"../gallery/tgod/index.htm\">推女神</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"尤果Ugril\" href=\"/gallery/ugirl/\">尤果Ugril</a></li>", "<li><a title=\"尤果Ugril\" href=\"../gallery/ugirl/index.htm\">尤果Ugril</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"波萝社\" href=\"/gallery/bololi/\">美媛馆</a></li>", "<li><a title=\"波萝社\" href=\"../gallery/bololi/index.htm\">波萝社</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"模范学院\" href=\"/gallery/mfstar/\">美媛馆</a></li>", "<li><a title=\"模范学院\" href=\"../gallery/mfstar/index.htm\">模范学院</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"4K-Star\" href=\"/gallery/4kstar/\">4K-Star</a></li>", "<li><a title=\"4K-Star\" href=\"../gallery/4kstar/index.htm\">4K-Star</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"RQ-Star\" href=\"/gallery/rqstar/\">RQ-Star</a></li>", "<li><a title=\"RQ-Star\" href=\"../gallery/rqstar/index.htm\">RQ-Star</a></li>");

        }
        #endregion

        #region 检测人物数量
        /// <summary>
        /// 检测人物数量
        /// </summary>
        public int PageCheckGalleryLiCount()
        {
            string oldhtml = FileSubstring.getContent(htmlStr, "<ul><li class=\'galleryli\'>", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'galleryli\'>");
            return count;
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////// 
        ////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion









        #region  子页部分########################################################################################
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////// 
        // 中国内地台湾日本韩国马来西亚越南泰国混血其他　子页
        // 网址
        public void GalleryChild()
        {
            xoxoblogroll_child();//友情链接

            styleReplaceChild(); //样式替换

            topwelcomechild(); //访客留言

            topnavchild();// 上部导航部分

            search_boxchild();//

            browsechild();//

            tag_div_child();

            galleryli_child();

            pagesYY();//分页

            replaceData_Original_child();

            footer_top_child();//


        }
        #region 样式替换
        /// <summary>
        /// 样式替换
        /// </summary>
        public void styleReplaceChild()
        {
            childHtmlStr = childHtmlStr.Replace("<link href=\"http://res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />", "<link href=\"../../../res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />");

            childHtmlStr = childHtmlStr.Replace("<script src=\"http://res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>", "<script src=\"../../../res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>");
            childHtmlStr = childHtmlStr.Replace("<script src=\"http://res.zngirls.com/script/carouFredSel.js\"></script>", "<script src=\"../../../res.zngirls.com/script/carouFredSel.js\"></script>");
            childHtmlStr = childHtmlStr.Replace("<script type=\'text/javascript\' src=\'http://images.sohu.com/cs/jsfile/js/c.js\'></script>", "");

            childHtmlStr = childHtmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.mousewheel.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.mousewheel.min.js\"></script>");
            childHtmlStr = childHtmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>");
            childHtmlStr = childHtmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");
            childHtmlStr = childHtmlStr.Replace("<script src=\"http://libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>", "<script src=\"../../../libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>");
            childHtmlStr = childHtmlStr.Replace("http://www.zngirls.com", "");
            childHtmlStr = childHtmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
        }
        #endregion

        #region topwelcome部分
        public void topwelcomechild()
        {
            childHtmlStr = childHtmlStr.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"../message/index.htm\">访客留言</a>");
        }
        #endregion


        #region topnavchild上部导航部分
        public void topnavchild()
        {
            childHtmlStr = childHtmlStr.Replace("<a href=\'#\'>宅男女神</a>资料", "<a href=\'../index.htm\'>宅男女神</a>资料");

            //首页
            childHtmlStr = childHtmlStr.Replace("<a href=\"/\" title=\"首  页\" class=\"home\"></a>", "<a href=\"../../index.htm\" title=\"首  页\" class=\"home\"></a>");

            //最新情报
            childHtmlStr = childHtmlStr.Replace("<a href=\"/article/\">最新情报</a>", "<a href=\"../../article/index.htm\">最新情报</a>");

            //宅男女神专区
            childHtmlStr = childHtmlStr.Replace("<a href=\"/find/\">宅男女神专区</a>", "<a href=\"../../find/index.htm\">宅男女神专区</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/tag/neidi/\">内地宅男女神</a>", "<a href=\"../../tag/neidi/index.htm\">内地宅男女神</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/tag/taiwan/\">台湾宅男女神</a>", "<a href=\"../../tag/taiwan/index.htm\">台湾宅男女神</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/tag/riben/\">日本宅男女神</a>", "<a href=\"../../tag/riben/index.htm\">日本宅男女神</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/tag/yazhou/\">亚洲宅男女神</a>", "<a href=\"../../tag/yazhou/index.htm\">亚洲宅男女神</a>");

            //美女图片
            childHtmlStr = childHtmlStr.Replace("<a href=\"/gallery/\">美女图片</a>", "<a href=\"../../gallery/index.htm\">美女图片</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/gallery/tuigirl/\">推女郎</a>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/gallery/tgod/\">推女神</a>", "<a href=\"../../gallery/tgod/index.htm\">推女神</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/gallery/ugirl/\">尤果Ugirl</a>", "<a href=\"../../gallery/ugirl/index.htm\">尤果Ugirl</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/gallery/xiuren/\">秀人</a>", "<a href=\"../../gallery/xiuren/index.htm\">秀人</a>");

            //宅男女神排行榜
            childHtmlStr = childHtmlStr.Replace("<a href=\"/rank/\">宅男女神排行榜</a>", "<a href=\"../../rank/index.htm\">宅男女神排行榜</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/rank/neidi/\">内地排行榜</a>", "<a href=\"../../rank/neidi/index.htm\">内地排行榜</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/rank/taiwan/\">台湾排行榜</a>", "<a href=\"../../rank/taiwan/index.htm\">台湾排行榜</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/rank/riben/\">日本排行榜</a>", "<a href=\"../../rank/riben/index.htm\">日本排行榜</a>");
            childHtmlStr = childHtmlStr.Replace("<a href=\"/rank/yazhou/\">亚洲排行榜</a>", "<a href=\"../../rank/yazhou/index.htm\">亚洲排行榜</a>");

            //女神大PK
            childHtmlStr = childHtmlStr.Replace("<a href=\"/arena/\">女神大PK</a>", "<a href=\"../../arena/index.htm\">女神大PK</a>");

            //倾城·美人榜
            childHtmlStr = childHtmlStr.Replace("<a href=\"/tag/\">倾城·美人榜</a>", "<a href=\"../../tag/index.htm\">倾城·美人榜</a>");

            //巧遇女神
            childHtmlStr = childHtmlStr.Replace("<a href=\"/meet/\">巧遇女神</a>", "<a href=\"../../meet/index.htm\">巧遇女神</a>");

            //女神速配
            childHtmlStr = childHtmlStr.Replace("<a href=\"/apply/match.aspx\">女神速配</a>", "<a href=\"../../apply/match.aspx\">女神速配</a>");

        }
        #endregion

        #region search_box
        public void search_boxchild()
        {
            childHtmlStr = childHtmlStr.Replace("<a title=\"推女郎\" href=\"/gallery/tuigirl/\">推女郎</a>", "<a title=\"推女郎\" href=\"../../gallery/tuigirl/index.htm\">推女郎</a>");
            childHtmlStr = childHtmlStr.Replace("<a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a>", "<a title=\"推女神\" href=\"../../gallery/tgod/index.htm\">推女神</a>");
            childHtmlStr = childHtmlStr.Replace("<a title=\"美媛馆\" href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a title=\"美媛馆\" href=\"../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            childHtmlStr = childHtmlStr.Replace("<a title=\"波萝社\" href=\"/gallery/bololi/\">波萝社</a>", "<a title=\"波萝社\" href=\"../../gallery/bololi/index.htm\">波萝社</a>");
            childHtmlStr = childHtmlStr.Replace("<a title=\"魅妍社\" href=\"/gallery/mistar/\">魅妍社</a>", "<a title=\"魅妍社\" href=\"../../gallery/mistar/index.htm\">魅妍社</a>");
            childHtmlStr = childHtmlStr.Replace("<a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a>", "<a title=\"优星馆\" href=\"../../gallery/uxing/index.htm\">优星馆</a>");
            childHtmlStr = childHtmlStr.Replace("<a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a>", "<a title=\"模范学院\" href=\"../../gallery/mfstar/index.htm\">模范学院</a>");
        }
        #endregion

        #region browse
        public void browsechild()
        {
            childHtmlStr = childHtmlStr.Replace("现在位置： <a title=\"返回首页\" href=\"/\">首页</a>", "现在位置： <a title=\"返回首页\" href=\"../../index.htm\">首页</a>");
            childHtmlStr = childHtmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>", "<script src=\"../../../res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>");
            childHtmlStr = childHtmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
            childHtmlStr = childHtmlStr.Replace("http://res.zngirls.com/style/HotNewspro/images/go.gif", "../../../res.zngirls.com/style/HotNewspro/images/go.gif");


            childHtmlStr = childHtmlStr.Replace("onclick=\"location=\'/find/\'\">找美女</button>", "onclick=\"location=\'../../find/index.htm\'\">找美女</button>");
        }
        #endregion

        #region tag_div　类别DIV 中国内地台湾日本韩国马来西亚越南泰国混血其他
        public void tag_div_child()
        {
            string strHtml = childHtmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<div id=\"tdiv\"><div class=\'tag_div\'>", "</div></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a href=\'" + i;
                newh = "<a href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }

            //加红色内容替换


            string oldred = FileSubstring.getContent(strHtml, "<div id=\"tdiv\"><div class=\'tag_div\'>", "</div></div>");
            int redcount = FileSubstring.getLabelCount(oldred, "<a class=\'cur_tag_a\' href=\'");

            List<string> redhref = FileSubstring.getSrc(oldred, "<a class=\'cur_tag_a\' href=\'", "\'", redcount);

            foreach (string i in redhref)
            {
                oldh = "<a class='cur_tag_a' href='" + i;
                newh = "<a class='cur_tag_a' href='" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }

            childHtmlStr = strHtml;
        }
        #endregion

        #region galleryli_child  图片子页
        public void galleryli_child()
        {
            string strHtml = childHtmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<ul><li class=\'galleryli\'>", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'galleryli\'>");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a class=\'galleryli_link\' href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a class=\'galleryli_link\' href=\'" + i;
                newh = "<a class=\'galleryli_link\' href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }

            List<string> listgalleryli_title = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_title\'><a href=\'", "\'", count);

            foreach (string i in listgalleryli_title)
            {
                oldh = "<div class=\'galleryli_title\'><a href=\'" + i;
                newh = "<div class=\'galleryli_title\'><a href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }

            childHtmlStr = strHtml;
        }
        #endregion

        #region 分页
        public void pagesYY()
        {
            string strhtml = childHtmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\'pagesYY\'><div>", "</div>");
            int count = FileSubstring.getLabelCount(oldhtml, "href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                if (i.IndexOf("#") == -1)
                {
                    oldh = "href=\'" + i;
                    newh = "href=\'" + "../.." + i;
                    strhtml = strhtml.Replace(oldh, newh);
                }
            }
            if (listahref[0].IndexOf(".html") == -1)
            {
                oldh = "<div><a class='a1' href=\'" + "../.." + listahref[0];
                newh = "<div><a class='a1' href=\'" + "../.." + listahref[0] + "1.html";
                strhtml = strhtml.Replace(oldh, newh);
            }


            //设置第一页和最后一页问题
            if (listahref[listahref.Count - 1].IndexOf(".html") == -1)
            {
                string page = nextPage;

                //MessageBox.Show(page);
                string countHTML = DownFile.GetHtmlStr(page, "UTF8");//下载首页index.html
                int NUM = checkLast(countHTML);
                if (NUM == 0)
                {
                    oldh = "<a class='a1' href=\'" + "../.." + listahref[listahref.Count - 1];
                    newh = "<a class='a1' href=\'" + "../.." + listahref[listahref.Count - 1] + "" + previousPageNo + ".html";
                }
                else
                {
                    oldh = "<a class='a1' href=\'" + "../.." + listahref[listahref.Count - 1];
                    newh = "<a class='a1' href=\'" + "../.." + listahref[listahref.Count - 1] + "" + (pageNo + 1) + ".html";
                }
                strhtml = strhtml.Replace(oldh, newh);
            }
            childHtmlStr = strhtml;
        }
        #endregion

        #region 设置是否是最后一页
        public int checkLast(string countHTML)
        {

            int count = FileSubstring.getLabelCount(countHTML, "<li class=\'galleryli\'>");
            return count;
        }

        #endregion

        #region replaceData_Original http://t1.zngirls.com/
        public void replaceData_Original_child()
        {
            childHtmlStr = childHtmlStr.Replace("data-original=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtmlStr = childHtmlStr.Replace("data-original=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtmlStr = childHtmlStr.Replace("data-original=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

            childHtmlStr = childHtmlStr.Replace("src=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtmlStr = childHtmlStr.Replace("src=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtmlStr = childHtmlStr.Replace("src=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

        }
        #endregion

        #region footer_top_child 返回首页子页
        public void footer_top_child()
        {
            string strhtml = childHtmlStr;
            strhtml = strhtml.Replace("<a href=\"/gallery/\">高清美女图片</a></li>", "<a href=\"../../gallery/index.htm\">高清美女图片</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎合集</a></li>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎合集</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆合集</a></li>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆合集</a></li>");

            childHtmlStr = strhtml;
        }
        #endregion

        #region  xoxoblogroll  友情链接
        public void xoxoblogroll_child()
        {
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"宅男女神\" href=\'#\'>宅男女神</a></li>", "<li><a title=\"宅男女神\" href=\'../../index.htm\'>宅男女神</a></li>");
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"日本宅男女神\" href=\'/tag/riben/\'>日本宅男女神</a></li>", "<li><a title=\"日本宅男女神\" href=\'../../tag/riben/index.htm\'>日本宅男女神</a></li>");
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"推女神\" href=\"/gallery/tgod/\">推女神</a></li>", "<li><a title=\"推女神\" href=\"../../gallery/tgod/index.htm\">推女神</a></li>");
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"尤果Ugril\" href=\"/gallery/ugirl/\">尤果Ugril</a></li>", "<li><a title=\"尤果Ugril\" href=\"../../gallery/ugirl/index.htm\">尤果Ugril</a></li>");
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"波萝社\" href=\"/gallery/bololi/\">美媛馆</a></li>", "<li><a title=\"波萝社\" href=\"../../gallery/bololi/index.htm\">波萝社</a></li>");
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"模范学院\" href=\"/gallery/mfstar/\">美媛馆</a></li>", "<li><a title=\"模范学院\" href=\"../../gallery/mfstar/index.htm\">模范学院</a></li>");
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"4K-Star\" href=\"/gallery/4kstar/\">4K-Star</a></li>", "<li><a title=\"4K-Star\" href=\"../../gallery/4kstar/index.htm\">4K-Star</a></li>");
            childHtmlStr = childHtmlStr.Replace("<li><a title=\"RQ-Star\" href=\"/gallery/rqstar/\">RQ-Star</a></li>", "<li><a title=\"RQ-Star\" href=\"../../gallery/rqstar/index.htm\">RQ-Star</a></li>");

        }
        #endregion

        #region 检测人物数量
        /// <summary>
        /// 检测人物数量
        /// </summary>
        public int CheckGalleryLiCount()
        {
            string oldhtml = FileSubstring.getContent(childHtmlStr, "<ul><li class=\'galleryli\'>", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'galleryli\'>");
            return count;
        }
        #endregion


        //////////////////////////////////////////////////////////////////////////////////////////////// 
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////// 









        #endregion

    }
}
