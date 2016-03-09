using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
namespace www_zngirls_com_g
{
    public partial class MainTag : DevExpress.XtraEditors.XtraForm
    {
        public MainTag()
        {
            InitializeComponent();
        }
        string htmlStr = ""; //整个网页HTML
        string SavePath = "";//路径　
        int pageNo = 0; 　　　//页码
        List<string> TypeDiv = null; //类别DIV

        string previousPageNo = "";
        string nextPage = "";

        string SaveGallery = "";




        private void ExecuteDown()
        {
            string url = "http://www.zngirls.com/tag";
            htmlStr = DownFile.GetHtmlStr(url, "UTF8");
            //MessageBox.Show(strSB.ToString());
            SavePath = PageInfo.path + "\\www.zngirls.com\\tag\\";
            DownFile.CreateDirectory(SavePath);//创建目录
                                               // MessageBox.Show(SavePath.ToString());//保存路径
            IndexPage();//过滤掉不需要的代码

            DownFile.createHtml(htmlStr, SavePath);
            //label4.Text = SavePath + "下载完成!";

            DownTagChildPage();//下载首页的子页

            downTypeDiv();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(ExecuteDown));
            thread.IsBackground = true;
            thread.Start();
        }

        private void DownTagChildPage()
        {
            string childIndexUrl = "http://www.zngirls.com/tag/";
            string childDownUrl = "";
        
            //下载tag目录下其它页面
            for (int i = 1; i < 1000; i++)
            {
                childDownUrl = childIndexUrl + i + ".html";
                htmlStr = DownFile.GetHtmlStr(childDownUrl, "UTF8");//下载首页index.html
                SaveGallery = PageInfo.path + "\\www.zngirls.com\\tag\\" + i + ".html";
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

            tag_div_down_child(); //记录并下载子页面

            tag_div();//类别DIV

            post_entry(); //图片信息和链接

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

        #region 8.tag_div_down_child 记录并下载子页面
        public void tag_div_down_child()
        {
            if (TypeDiv == null)
            {
                string strHtml = htmlStr;
                string oldhtml = FileSubstring.getContent(strHtml, "<div class=\"entry_box_arena\">", "</div><i class=\"lt\"></i><i class=\"rt\"></i></div>");
                int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");

                List<string> listahref = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", count);
                TypeDiv = listahref;
            }
        }
        #endregion

        #region 9.tag_div　类别DIV 演员歌手模特&麻豆车模&赛车女郎配音&声优主播&主持人Cosplayer校花空姐足球宝贝Showgirl选美小姐书法美女美女作家网络美女教师OL学生写真女优推女郎淘女郎体坛美女舞蹈家啦啦队企业家嫩模瑜伽教练
        public void tag_div()
        {
            string strHtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<div class=\"box_entry\" style=\"overflow: hidden;\">", "</div></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a href=\'" + i;
                newh = "<a href=\'" + ".." + i + "/index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
        

            htmlStr = strHtml;
        }
        #endregion

        #region 10.修改链接和下载图片

        public void post_entry()
        {
            string strHtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<ul><li class=\'beautyli\'>", "</ul><div class=\'clear\'></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'beautyli\'>");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a target=\'_blank\' href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a target=\'_blank\' href=\'" + i;
                newh = "<a target=\'_blank\' href=\'" + ".." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            List<string> listgalleryli_title = FileSubstring.getSrc(oldhtml, "<li><strong><a href=\'", "\'", count);

            foreach (string i in listgalleryli_title)
            {
                oldh = "<li><strong><a href=\'" + i;
                newh = "<li><strong><a href=\'" + ".." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            htmlStr = strHtml;

            DownPic(oldhtml, count, strHtml);

        }
        #region 下载图片
        private void DownPic(string oldhtml, int count, string strHtml)
        {
            List<string> downPic = FileSubstring.getSrc(oldhtml, "<img alt=\'\' data-original=\'", "\'", count);

            //MessageBox.Show(count.ToString());
            List<DownFile> downFile = FileSubstring.addFileList(downPic);



            //获取下载地址
            DownFile.WebClientDownFile(downFile, PageInfo.path + "\\");
            htmlStr = strHtml;
        }

        #endregion

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

        #region 15.检测人物数量
        /// <summary>
        /// 检测人物数量
        /// </summary>
        public int PageCheckGalleryLiCount()
        {
            string oldhtml = FileSubstring.getContent(htmlStr, "<div class=\"post_entry\">", "<div class=\'clear\'></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'beautyli\'>");
            return count;
        }
        #endregion











        #region 下载分类
        string childHtmlStr = "";


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
                TagChild();

                DownFile.createHtml(childHtmlStr, SavePath);
                for (int j = 1; j <= 10000; j++)
                {

                    string path = string.Format("http://www.zngirls.com{0}/{1}", TypeDiv[i], j + ".html");
                    previousPageNo = j.ToString();
                    nextPage = string.Format("http://www.zngirls.com{0}/{1}", TypeDiv[i], (j + 1) + ".html");
                    childHtmlStr = DownFile.GetHtmlStr(path, "UTF8");
                    SavePath = PageInfo.path + "\\www.zngirls.com" + TypeDiv[i];
                    SavePath = SavePath + "/" + j + ".html";
                    if (childHtmlStr.IndexOf("抱歉，您查找的页面不存在或已删除") != -1)
                    {
                        SavePath = "";
                        break;
                    }
                    pageNo = j;
                    TagChild();
                    DownFile.createChildHtml(childHtmlStr, SavePath);
                    SavePath = "";
                }
            }
        }
        #endregion


        #region TagChild 子页下载
        public void TagChild()
        {
            xoxoblogrollchild();//友情链接

            styleReplacechild(); //样式替换

            //delhiddenchild();//去掉hidden代码

            topwelcomechild();//访问留言

            topnavchild();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            search_boxchild();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院

            browsechild();//现在位置： 首页 > 宅男女神 

            //tag_div_down_child_child(); //记录并下载子页面

            tag_div_child();//类别DIV

            post_entry_child(); //图片信息和链接

            replaceData_Original_child();//http://res.zngirls.com转换

            parentpagesYY_child();//分页

            footer_top_child();//
        }
        #endregion

        #region 1.xoxoblogrollchild
        public void xoxoblogrollchild()
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


        #region 2.样式替换
        /// <summary>
        /// 样式替换
        /// </summary>
        public void styleReplacechild()
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


        #region 3.topwelcome部分
        public void topwelcomechild()
        {
            childHtmlStr = childHtmlStr.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"../../message/index.htm\">访客留言</a>");
            childHtmlStr = childHtmlStr.Replace("<img src=\"/images/qc_l.jpg\"", "<img src=\"../../images/qc_l.jpg\"");
        }
        #endregion

        #region 5.topnav上部导航部分
        public void topnavchild()
        {

            childHtmlStr = childHtmlStr.Replace("<a href=\'#\'>宅男女神</a>资料", "<a href=\'../../index.htm\'>宅男女神</a>资料");

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


        #region 6.search_box  热门查询
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


        #region 7.browse 现在位置： 首页 > 宅男女神 
        public void browsechild()
        {
            childHtmlStr = childHtmlStr.Replace("现在位置： <a title=\"返回首页\" href=\"/\">首页</a>", "现在位置： <a title=\"返回首页\" href=\"../../index.htm\">首页</a>");
            childHtmlStr = childHtmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>", "<script src=\"../../../res.zngirls.com/script/nvshen_right.js\" type=\"text/javascript\"></script>");
            childHtmlStr = childHtmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
            childHtmlStr = childHtmlStr.Replace("http://res.zngirls.com/style/HotNewspro/images/go.gif", "../../../res.zngirls.com/style/HotNewspro/images/go.gif");
            childHtmlStr = childHtmlStr.Replace("onclick=\"location=\'/find/\'\">找美女</button>", "onclick=\"location=\'../../find/index.htm\'\">找美女</button>");
        }
        #endregion


        #region 9.tag_div　类别DIV 演员歌手模特&麻豆车模&赛车女郎配音&声优主播&主持人Cosplayer校花空姐足球宝贝Showgirl选美小姐书法美女美女作家网络美女教师OL学生写真女优推女郎淘女郎体坛美女舞蹈家啦啦队企业家嫩模瑜伽教练
        public void tag_div_child()
        {
            string strHtml = childHtmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<div class=\"box_entry\" style=\"overflow: hidden;\">", "</div></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a href=\'" + i;
                newh = "<a href=\'" + "../.." + i + "/index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            //设置选中的键接　
            List<string> redHref = FileSubstring.getSrc(oldhtml, "<a class=\'cur_tag_a\' href=\'", "\'", 1);
            foreach (string i in redHref)
            {
                oldh = "<a class=\'cur_tag_a\' href=\'" + i;
                newh = "<a class=\'cur_tag_a\' href=\'" + "../.." + i + "/index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }


            childHtmlStr = strHtml;
        }
        #endregion


        #region 10.修改链接和下载图片

        public void post_entry_child()
        {
            string strHtml = childHtmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<ul><li class=\'beautyli\'>", "</ul><div class=\'clear\'></div>");
            int count = FileSubstring.getLabelCount(oldhtml, "<li class=\'beautyli\'>");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a target=\'_blank\' href=\'", "\'", count);
            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a target=\'_blank\' href=\'" + i;
                newh = "<a target=\'_blank\' href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            List<string> listgalleryli_title = FileSubstring.getSrc(oldhtml, "<li><strong><a href=\'", "\'", count);

            foreach (string i in listgalleryli_title)
            {
                oldh = "<li><strong><a href=\'" + i;
                newh = "<li><strong><a href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            childHtmlStr = strHtml;

            DownPicChild(oldhtml, count, strHtml);

        }
        #region 下载图片
        private void DownPicChild(string oldhtml, int count, string strHtml)
        {
            List<string> downPic = FileSubstring.getSrc(oldhtml, "<img alt=\'\' data-original=\'", "\'", count);

            //MessageBox.Show(count.ToString());
            List<DownFile> downFile = FileSubstring.addFileList(downPic);



            //获取下载地址
            DownFile.WebClientDownFile(downFile, PageInfo.path + "\\");
            childHtmlStr = strHtml;
        }

        #endregion

        #endregion


        #region 11.replaceData_Original http://t1.zngirls.com/
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


        #region 12.parentpagesYY　分页
        public void parentpagesYY_child()
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
                    newh = "href=\'" + i;
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
            childHtmlStr = strhtml;
        }
        #endregion

        #region 13.返回首页
        public void footer_top_child()
        {
            string strhtml = childHtmlStr;
            strhtml = strhtml.Replace("<a href=\"/gallery/\">高清美女图片</a></li>", "<a href=\"../../gallery/index.htm\">高清美女图片</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/tuigirl/\">推女郎合集</a></li>", "<a href=\"../../gallery/tuigirl/index.htm\">推女郎合集</a></li>");
            strhtml = strhtml.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆合集</a></li>", "<a href=\"../../gallery/meiyuanguan/index.htm\">美媛馆合集</a></li>");
            childHtmlStr = strhtml;
        }
        #endregion

        #endregion
        
    }
}