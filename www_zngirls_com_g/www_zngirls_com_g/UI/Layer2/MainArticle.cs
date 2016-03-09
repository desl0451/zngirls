using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace www_zngirls_com_g
{
    public partial class MainArticle : Form
    {
        public MainArticle()
        {
            InitializeComponent();
        }
        string htmlStr = "";
        string SavePath = "";
        int pageNo = 0; 　　　//页码
        List<string> articleSum = new List<string>(); //类别DIV

        string previousPageNo = "";
        string nextPage = "";

        string SaveGallery = "";
        string childPage = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string url = "http://www.zngirls.com/article/";
            htmlStr = DownFile.GetDownUrlHTML(url, "UTF8");
            SavePath = PageInfo.path + "\\www.zngirls.com\\article\\";
            DownFile.CreateDirectory(SavePath);//创建目录
                                               // MessageBox.Show(SavePath.ToString());//保存路径
            IndexPage();//过滤掉不需要的代码

            DownFile.createHtml(htmlStr, SavePath);

            DownTagChildPage();//下载首页的子页

            DownArticlePage();//下载子每页的子页
        }


        #region 首页代码
        #region 首页和子页代码部分
        void IndexPage()
        {
            xoxoblogroll();//友情链接

            styleReplace(); //样式替换

            delhidden();//去掉hidden代码

            topwelcome();//访问留言

            topnav();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            search_box();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院

            browse();//现在位置： 首页 > 宅男女神 

            other_girlli();//设置情报链接

            ui_tags_type();//热门标签

            //tag_div_down_child(); //记录并下载子页面

            //tag_div();//类别DIV

            //post_entry(); //图片信息和链接

            replaceData_Original();//http://res.zngirls.com转换

            parentpagesYY();//分页

            footer_top();//
        }
        #endregion


        #region 1.xoxoblogroll 
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


        #region 2.样式替换
        /// <summary>
        /// 样式替换
        /// </summary>
        public void styleReplace()
        {
            htmlStr = htmlStr.Replace("<link href=\"http://res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />", "<link href=\"../../res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />");

            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>", "<script src=\"../../res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/carouFredSel.js\"></script>", "<script src=\"../../res.zngirls.com/script/carouFredSel.js\"></script>");
            htmlStr = htmlStr.Replace("<script type=\'text/javascript\' src=\'http://images.sohu.com/cs/jsfile/js/c.js\'></script>", "");

            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.mousewheel.min.js\"></script>", "<script src=\"../../res.zngirls.com/script/jquery.mousewheel.min.js\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>", "<script src=\"../../res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");
            htmlStr = htmlStr.Replace("<script src=\"http://libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>", "<script src=\"../../libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>");
            htmlStr = htmlStr.Replace("http://www.zngirls.com", "");
            htmlStr = htmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
        }
        #endregion


        #region 3.去掉hidden代码
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


        #region 4.topwelcome部分
        public void topwelcome()
        {
            htmlStr = htmlStr.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"../message/index.htm\">访客留言</a>");
            htmlStr = htmlStr.Replace("<img src=\"/images/qc_l.jpg\"", "<img src=\"../images/qc_l.jpg\"");
        }
        #endregion


        #region 5.topnav上部导航部分
        public void topnav()
        {

            htmlStr = htmlStr.Replace("<a href=\'#\'>宅男女神</a>资料", "<a href=\'../index.htm\'>宅男女神</a>资料");

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


        #region 6.search_box  热门查询
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


        #region 7.browse 现在位置： 首页 > 宅男女神 
        public void browse()
        {
            htmlStr = htmlStr.Replace("现在位置： <a title=\"返回首页\" href=\"/\">首页</a>", "现在位置： <a title=\"返回首页\" href=\"../index.htm\">首页</a>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>", "<script src=\"../../res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>");
            htmlStr = htmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
            htmlStr = htmlStr.Replace("http://res.zngirls.com/style/HotNewspro/images/go.gif", "../../res.zngirls.com/style/HotNewspro/images/go.gif");


            htmlStr = htmlStr.Replace("onclick=\"location=\'/find/\'\">找美女</button>", "onclick=\"location=\'../find/index.htm\'\">找美女</button>");
        }
        #endregion


        #region 8.设置情报链接
        public void other_girlli()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\"post_entry\">", "</div></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'other_girlli\'><a target=\'blank\' href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<li class=\'other_girlli\'><a target=\'blank\' href=\'", "\'", count);




            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                articleSum.Add(i);//把数据添加到集合中
                if (i.IndexOf("#") == -1)
                {
                    oldh = "href=\'" + i;
                    newh = "href=\'" + ".." + i + "index.htm";
                    strhtml = strhtml.Replace(oldh, newh);
                }
            }
            htmlStr = strhtml;
        }
        #endregion


        #region 9.热门标签 ui_tags_type
        public void ui_tags_type()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\"favor_div\"><h3>热门标签</h3>", "<div class=\"box-bottom\"><i class=\"lb\">");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'other_tags\'><a");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                if (i.IndexOf("#") == -1)
                {
                    oldh = "href=\'" + i;
                    newh = "href=\'" + ".." + i + "index.htm";
                    strhtml = strhtml.Replace(oldh, newh);
                }
            }


            oldhtml = FileSubstring.getContent(strhtml, "<div class=\"tag_side_div\">", "</a></div>");
            count = FileSubstring.getLabelCount(oldhtml, "<a href=\"");

            listahref = FileSubstring.getSrc(oldhtml, "href=\"", "\"", 1);

            string lasthref = listahref[listahref.Count - 1];
            oldh = "<div class=\"tag_side_div\"><a href=\"" + lasthref;
            newh = "<div class=\"tag_side_div\"><a href=\"" + ".." + lasthref + "index.htm";
            strhtml = strhtml.Replace(oldh, newh);



            htmlStr = strhtml;

        }
        #endregion


        #region 11.replaceData_Original http://t1.zngirls.com/
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


        #region 12.parentpagesYY　分页
        public void parentpagesYY()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\'pagesYY\'><div>", "</div>");
            int count = FileSubstring.getLabelCount(oldhtml, "href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            //foreach (string i in listahref)
            //{
            //    if (i.IndexOf("#") == -1)
            //    {
            //        oldh = "href=\'" + i;
            //        newh = "href=\'" + ".." + i;
            //        strhtml = strhtml.Replace(oldh, newh);
            //    }

            //}
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


        #region 13.返回首页
        public void footer_top()
        {
            string strhtml = htmlStr;
            strhtml = strhtml.Replace("<a href=\"/gallery/\">高清美女图片</a></li>", "<a href=\"../gallery/index.htm\">高清美女图片</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎合集</a></li>", "<a href=\"../gallery/tuigirl/index.htm\">推女郎合集</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆合集</a></li>", "<a href=\"../gallery/meiyuanguan/index.htm\">美媛馆合集</a></li>");
            htmlStr = strhtml;
        }
        #endregion


        #region 14.设置是否是最后一页
        public int checkLast(string countHTML)
        {

            int count = FileSubstring.getLabelCount(countHTML, "<li class=\'galleryli\'>");
            return count;
        }

        #endregion

        #endregion


        #region 子页下载
        public void DownTagChildPage()
        {
            string childIndexUrl = "http://www.zngirls.com/article/";
            string childDownUrl = "";

            //下载tag目录下其它页面
            for (int i = 1; i < 1000; i++)
            {
                childDownUrl = childIndexUrl + i + ".html";
                htmlStr = DownFile.GetHtmlStr(childDownUrl, "UTF8");//下载首页index.html
                SaveGallery = PageInfo.path + "\\www.zngirls.com\\article\\" + i + ".html";
                previousPageNo = i.ToString();
                if (htmlStr.IndexOf("抱歉，您查找的页面不存在或已删除") != -1)
                {
                    break;
                }
                pageNo = i;

                IndexPage();
                DownFile.createChildHtml(htmlStr, SaveGallery);
            }
        }
        #endregion

        #region 下载每页的子页
        public void DownArticlePage()
        {
            string httpurl = "";
            //http://www.zngirls.com/article/10273/
            foreach (string url in articleSum)
            {
                httpurl = "http://www.zngirls.com" + url;
                childPage = DownFile.GetDownUrlHTML(httpurl, "UTF8");
                SavePath = PageInfo.path + "\\www.zngirls.com" + url;
                DownFile.CreateDirectory(SavePath);//创建目录
                ArticlePage();
                DownFile.createHtml(childPage, SavePath);
            }
        }
        #endregion

        #region ArticlePage 文章页面
        public void ArticlePage()
        {
            ArticlePagexoxoblogroll();//友情链接

            ArticlePagestyleReplace();//样式替换

            ArticlePagedelhidden();//去掉hidden代码

            ArticlePagetopwelcome();//topwelcome部分

            ArticlePagetopnav();//导航

            ArticlePagesearch_box();//热门查询

            ArticlePagebrowse();//现在位置： 首页 > 宅男女神 

            ArticlePagetab_inside();//热点资讯

            ArticlePagereplaceData_Original();//replaceData_Original

            ArticlePageArticlePicDown();//文章图片下载

            ArticlePagepost_entry();//相关女神

            ArticlePageArticleFoot();//上一页下一页

            ArticlePagefooter_top();//低


        }
        #endregion

        #region 1.xoxoblogroll 
        public void ArticlePagexoxoblogroll()
        {
            childPage = childPage.Replace("<li><a title=\"宅男女神\" href=\'#\'>宅男女神</a></li>", "<li><a title=\"宅男女神\" href=\'../../index.htm\'>宅男女神</a></li>");
            childPage = childPage.Replace("<li><a title=\"日本宅男女神\" href=\'/tag/riben/\'>日本宅男女神</a></li>", "<li><a title=\"日本宅男女神\" href=\'../../tag/riben/index.htm\'>日本宅男女神</a></li>");
            childPage = childPage.Replace("<li><a title=\"推女神\" href=\"/gallery/tgod/\">推女神</a></li>", "<li><a title=\"推女神\" href=\"../../gallery/tgod/index.htm\">推女神</a></li>");
            childPage = childPage.Replace("<li><a title=\"尤果Ugril\" href=\"/gallery/ugirl/\">尤果Ugril</a></li>", "<li><a title=\"尤果Ugril\" href=\"../../gallery/ugirl/index.htm\">尤果Ugril</a></li>");
            childPage = childPage.Replace("<li><a title=\"波萝社\" href=\"/gallery/bololi/\">美媛馆</a></li>", "<li><a title=\"波萝社\" href=\"../../gallery/bololi/index.htm\">波萝社</a></li>");
            childPage = childPage.Replace("<li><a title=\"模范学院\" href=\"/gallery/mfstar/\">美媛馆</a></li>", "<li><a title=\"模范学院\" href=\"../../gallery/mfstar/index.htm\">模范学院</a></li>");
            childPage = childPage.Replace("<li><a title=\"4K-Star\" href=\"/gallery/4kstar/\">4K-Star</a></li>", "<li><a title=\"4K-Star\" href=\"../../gallery/4kstar/index.htm\">4K-Star</a></li>");
            childPage = childPage.Replace("<li><a title=\"RQ-Star\" href=\"/gallery/rqstar/\">RQ-Star</a></li>", "<li><a title=\"RQ-Star\" href=\"../../gallery/rqstar/index.htm\">RQ-Star</a></li>");

        }
        #endregion


        #region 2.样式替换
        /// <summary>
        /// 样式替换
        /// </summary>
        public void ArticlePagestyleReplace()
        {
            childPage = childPage.Replace("<link href=\"http://res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />", "<link href=\"../../../res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />");

            childPage = childPage.Replace("<script src=\"http://res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>", "<script src=\"../../../res.zngirls.com/script/common.js\" type=\"text/javascript\"></script>");
            childPage = childPage.Replace("<script src=\"http://res.zngirls.com/script/carouFredSel.js\"></script>", "<script src=\"../../../res.zngirls.com/script/carouFredSel.js\"></script>");
            childPage = childPage.Replace("<script type=\'text/javascript\' src=\'http://images.sohu.com/cs/jsfile/js/c.js\'></script>", "");

            childPage = childPage.Replace("<script src=\"http://res.zngirls.com/script/jquery.mousewheel.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.mousewheel.min.js\"></script>");
            childPage = childPage.Replace("<script src=\"http://res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>");
            childPage = childPage.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");
            childPage = childPage.Replace("<script src=\"http://libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>", "<script src=\"../../../libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>");
            childPage = childPage.Replace("http://www.zngirls.com", "");

            childPage = childPage.Replace("<a href=\"/article/\" title=\"查看更多资讯\">最新情报</a>", "<a href=\"../../article/index.htm\" title=\"查看更多资讯\">最新情报</a>");
            childPage = childPage.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
        }
        #endregion


        #region 3.去掉hidden代码
        /// <summary>
        /// 去掉hidden代码
        /// </summary>

        public void ArticlePagedelhidden()
        {
            string strhtml = childPage;
            string oldhtml = FileSubstring.getContent(strhtml, "<input type=\"hidden\" name=\"__VIEWSTATE\"", "\">");
            if (oldhtml.Length > 0)
                childPage = childPage.Replace(oldhtml, "");
        }
        #endregion


        #region 4.topwelcome部分
        public void ArticlePagetopwelcome()
        {
            childPage = childPage.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"../../message/index.htm\">访客留言</a>");
            childPage = childPage.Replace("<img src=\"/images/qc_l.jpg\"", "<img src=\"../../images/qc_l.jpg\"");
        }
        #endregion


        #region 5.topnav上部导航部分
        public void ArticlePagetopnav()
        {

            childPage = childPage.Replace("<a href=\'#\'>宅男女神</a>资料", "<a href=\'../../index.htm\'>宅男女神</a>资料");

            //首页
            childPage = childPage.Replace("<a href=\"/\" title=\"首  页\" class=\"home\"></a>", "<a href=\"../../index.htm\" title=\"首  页\" class=\"home\"></a>");

            //最新情报
            childPage = childPage.Replace("<a href=\"/article/\">最新情报</a>", "<a href=\"../../article/index.htm\">最新情报</a>");

            //宅男女神专区
            childPage = childPage.Replace("<a href=\"/find/\">宅男女神专区</a>", "<a href=\"../../find/index.htm\">宅男女神专区</a>");
            childPage = childPage.Replace("<a href=\"/tag/neidi/\">内地宅男女神</a>", "<a href=\"../../tag/neidi/index.htm\">内地宅男女神</a>");
            childPage = childPage.Replace("<a href=\"/tag/taiwan/\">台湾宅男女神</a>", "<a href=\"../../tag/taiwan/index.htm\">台湾宅男女神</a>");
            childPage = childPage.Replace("<a href=\"/tag/riben/\">日本宅男女神</a>", "<a href=\"../../tag/riben/index.htm\">日本宅男女神</a>");
            childPage = childPage.Replace("<a href=\"/tag/yazhou/\">亚洲宅男女神</a>", "<a href=\"../../tag/yazhou/index.htm\">亚洲宅男女神</a>");

            //美女图片
            childPage = childPage.Replace("<a href=\"/gallery/\">美女图片</a>", "<a href=\"../../gallery/index.htm\">美女图片</a>");
            childPage = childPage.Replace("<a href=\"/gallery/tuigirl/\">推女郎</a>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎</a>");
            childPage = childPage.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            childPage = childPage.Replace("<a href=\"/gallery/tgod/\">推女神</a>", "<a href=\"../../gallery/tgod/index.htm\">推女神</a>");
            childPage = childPage.Replace("<a href=\"/gallery/ugirl/\">尤果Ugirl</a>", "<a href=\"../../gallery/ugirl/index.htm\">尤果Ugirl</a>");
            childPage = childPage.Replace("<a href=\"/gallery/xiuren/\">秀人</a>", "<a href=\"../../gallery/xiuren/index.htm\">秀人</a>");

            //宅男女神排行榜
            childPage = childPage.Replace("<a href=\"/rank/\">宅男女神排行榜</a>", "<a href=\"../../rank/index.htm\">宅男女神排行榜</a>");
            childPage = childPage.Replace("<a href=\"/rank/neidi/\">内地排行榜</a>", "<a href=\"../../rank/neidi/index.htm\">内地排行榜</a>");
            childPage = childPage.Replace("<a href=\"/rank/taiwan/\">台湾排行榜</a>", "<a href=\"../../rank/taiwan/index.htm\">台湾排行榜</a>");
            childPage = childPage.Replace("<a href=\"/rank/riben/\">日本排行榜</a>", "<a href=\"../../rank/riben/index.htm\">日本排行榜</a>");
            childPage = childPage.Replace("<a href=\"/rank/yazhou/\">亚洲排行榜</a>", "<a href=\"../../rank/yazhou/index.htm\">亚洲排行榜</a>");

            //女神大PK
            childPage = childPage.Replace("<a href=\"/arena/\">女神大PK</a>", "<a href=\"../../arena/index.htm\">女神大PK</a>");

            //倾城·美人榜
            childPage = childPage.Replace("<a href=\"/tag/\">倾城·美人榜</a>", "<a href=\"../../tag/index.htm\">倾城·美人榜</a>");

            //巧遇女神
            childPage = childPage.Replace("<a href=\"/meet/\">巧遇女神</a>", "<a href=\"../../meet/index.htm\">巧遇女神</a>");

            //女神速配
            childPage = childPage.Replace("<a href=\"/apply/match.aspx\">女神速配</a>", "<a href=\"../../apply/match.aspx\">女神速配</a>");

        }
        #endregion


        #region 6.search_box  热门查询
        public void ArticlePagesearch_box()
        {
            childPage = childPage.Replace("<a title=\"推女郎\" href=\"/gallery/tuigirl/\">推女郎</a>", "<a title=\"推女郎\" href=\"../../gallery/tuigirl/index.htm\">推女郎</a>");
            childPage = childPage.Replace("<a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a>", "<a title=\"推女神\" href=\"../../gallery/tgod/index.htm\">推女神</a>");
            childPage = childPage.Replace("<a title=\"美媛馆\" href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a title=\"美媛馆\" href=\"../../gallery/meiyuanguan/index.htm\">美媛馆</a>");
            childPage = childPage.Replace("<a title=\"波萝社\" href=\"/gallery/bololi/\">波萝社</a>", "<a title=\"波萝社\" href=\"../../gallery/bololi/index.htm\">波萝社</a>");
            childPage = childPage.Replace("<a title=\"魅妍社\" href=\"/gallery/mistar/\">魅妍社</a>", "<a title=\"魅妍社\" href=\"../../gallery/mistar/index.htm\">魅妍社</a>");
            childPage = childPage.Replace("<a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a>", "<a title=\"优星馆\" href=\"../../gallery/uxing/index.htm\">优星馆</a>");
            childPage = childPage.Replace("<a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a>", "<a title=\"模范学院\" href=\"../../gallery/mfstar/index.htm\">模范学院</a>");
        }
        #endregion


        #region 7.browse 现在位置： 首页 > 宅男女神 
        public void ArticlePagebrowse()
        {
            childPage = childPage.Replace("现在位置： <a title=\"返回首页\" href=\"/\">首页</a>", "现在位置： <a title=\"返回首页\" href=\"../../index.htm\">首页</a>");
            childPage = childPage.Replace("<script src=\"http://res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>", "<script src=\"../../../res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>");
            childPage = childPage.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
            childPage = childPage.Replace("http://res.zngirls.com/style/HotNewspro/images/go.gif", "../../../res.zngirls.com/style/HotNewspro/images/go.gif");


            childPage = childPage.Replace("onclick=\"location=\'/find/\'\">找美女</button>", "onclick=\"location=\'../../find/index.htm\'\">找美女</button>");
        }
        #endregion


        #region 8.文章图片下载
        public void ArticlePageArticlePicDown()
        {
            string strHtml = childPage;
            string oldhtml = FileSubstring.getContent(strHtml, "<div id=\"articleDiv\">", "</div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<img alt=\"\" src=\"");

            List<string> imgahref = FileSubstring.getSrc(oldhtml, "<img alt=\"\" src=\"", "\"", count);
            count = FileSubstring.getLabelCount(oldhtml, "<a href=\"");
            List<string> ahref = FileSubstring.getSrc(oldhtml, "<a href=\"", "\"", count);
            List<DownFile> downFile = FileSubstring.addFileList(imgahref);
            // childPage = childPage.Replace("<p style=\"text-align: center;\"><img alt=\"\" src=\"http://img.zngirls.com/", "<p style=\"text-align:center;\"><img alt=\"\" src=\'../../../img.zngirls.com/");
            string newhtml = oldhtml;

            string oldh = "";
            string newh = "";
            string newhref = "";
            foreach (string i in imgahref)
            {
                newhref = "../../../" + i.Substring(7);
                oldh = "<img alt=\"\" src=\"" + i;
                newh = "<img alt=\"\" src=\"" + newhref;
                newhtml = newhtml.Replace(oldh, newh);
            }
            foreach (string i in ahref)
            {
                oldh = "<a href=\"" + i;
                newh = "<a href=\"" + "../.." + i + "index.htm";
                newhtml = newhtml.Replace(oldh, newh);
            }

            childPage = childPage.Replace(oldhtml, newhtml);


            //获取下载地址
            DownFile.WebClientDownFile(downFile, PageInfo.path + "\\");

        }
        #endregion


        #region 9.相关女神
        public void ArticlePagepost_entry()
        {
            string strHtml = childPage;
            string oldhtml = FileSubstring.getContent(strHtml, "<div class=\"post_entry\">", "<i class=\"lt\"></i><i class=\"rt\">");
            int count = FileSubstring.getLabelCount(oldhtml, "<a class=\'l-tmb-a\' target=\'_blank\' href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a class=\'l-tmb-a\' target=\'_blank\' href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a class=\'l-tmb-a\' target=\'_blank\' href=\'" + i;
                newh = "<a class=\'l-tmb-a\' target=\'_blank\' href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }

            List<string> listhref = FileSubstring.getSrc(oldhtml, "<div><a href=\'", "\'", count);

            foreach (string i in listhref)
            {
                oldh = "<div><a href=\'" + i;
                newh = "<div><a href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            //提取图片
            
            int tuCount = FileSubstring.getLabelCount(oldhtml, "src=\'");//图片数量
            List<string> tuList = FileSubstring.getSrc(oldhtml, "src=\'", "\'", count);
            List<DownFile> downFile = FileSubstring.addFileList(tuList);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, PageInfo.path + "\\");

            childPage = strHtml;
        }
        #endregion


        #region 10.上一页一下页ArticlePageArticleFoot
        public void ArticlePageArticleFoot()
        {
            string strHtml = childPage;
            string oldhtml = FileSubstring.getContent(strHtml, "<div id=\"articleFoot\">", "</div>");
            int count = FileSubstring.getLabelCount(oldhtml, "href=\'");

            List<string> imgahref = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);
            string newhtml = oldhtml;
            string oldh = "";
            string newh = "";
            foreach (string i in imgahref)
            {
                oldh = "href=\'" + i;
                newh = "href=\'" + "../.." + i + "/index.htm";
                newhtml = newhtml.Replace(oldh, newh);
            }
            childPage = childPage.Replace(oldhtml, newhtml);
        }
        #endregion


        #region 11.replaceData_Original http://t1.zngirls.com/
        public void ArticlePagereplaceData_Original()
        {
            childPage = childPage.Replace("data-original=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            childPage = childPage.Replace("data-original=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            childPage = childPage.Replace("data-original=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

            childPage = childPage.Replace("src=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            childPage = childPage.Replace("src=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            childPage = childPage.Replace("src=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

        }
        #endregion


        #region 12.返回首页
        public void ArticlePagefooter_top()
        {
            string strhtml = childPage;
            strhtml = strhtml.Replace("<a href=\"/gallery/\">高清美女图片</a></li>", "<a href=\"../../gallery/index.htm\">高清美女图片</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎合集</a></li>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎合集</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆合集</a></li>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆合集</a></li>");
            childPage = strhtml;
        }
        #endregion

        #region 13.热点资讯
        public void ArticlePagetab_inside()
        {
            string strHtml = childPage;
            string oldhtml = FileSubstring.getContent(strHtml, "<ul id=\"tab-widget1\">", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");
            string newhtml = oldhtml;
            List<string> imgahref = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in imgahref)
            {
                oldh = "<a href=\'" + i;
                newh = "<a href=\'" + "../.." + i + "index.htm";
                newhtml = newhtml.Replace(oldh, newh);
            }
            childPage = childPage.Replace(oldhtml, newhtml);



            List<string> imgahrefdown = FileSubstring.getSrc(oldhtml, "<img src=\'", "\'", count);
            count = FileSubstring.getLabelCount(oldhtml, "<img src=\'");
            List<DownFile> downFile = FileSubstring.addFileList(imgahrefdown);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, PageInfo.path + "\\");
        }
        #endregion
    }
}
