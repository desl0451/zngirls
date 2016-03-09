using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace www_zngirls_com_g
{
    public partial class MainDownIndex : Form
    {
        public MainDownIndex()
        {
            InitializeComponent();
        }
        string htmlStr = "";
        string htmlXieZhen = "";
        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            htmlStr = DownFile.GetHtmlStr("http://www.zngirls.com", "UTF8");
            //htmlStr= DownFile.GetDownUrlHTML("http://www.zngirls.com", "UTF8");
            ReplaceAll(); //替换内容
            DownFile.createHtml(htmlStr,path);
            label3.Text = "下载成功!";

            //读取html
            //htmlStr = DownFile.ReadHtml(path);
            // 

            htmlXieZhen = DownFile.GetHtmlStr("http://www.zngirls.com/gallery/", "UTF8");
            ReplaceGalleryAll();
            DownFile.createHtml(htmlXieZhen, path+ @"\gallery");
            init();

        }
        /// <summary>
        /// 初始化读取信息
        /// </summary>
        public void init()
        {
            
        }


        public void ReplaceAll()
        {
            styleDataReplace(); //样式替换
            replaceData_Original();
            topwelcome();//访客留言

            topnav();//上部导航

            search_box();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院

            search_artile();//计算文章数量

            search_girl();//计算美女数量

            
        }


        #region 样式数据替换
        public void styleDataReplace()
        {
            htmlStr = htmlStr.Replace("<link href=\"http://res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />", "<link href=\"../res.zngirls.com/style/site.css\" rel=\"stylesheet\" type=\"text/css\" />");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/common.js\"", "<script src=\"../res.zngirls.com/script/common.js\"");
            htmlStr = htmlStr.Replace("http://res.zngirls.com/style/HotNewspro/images/go.gif", "../res.zngirls.com/style/HotNewspro/images/go.gif");
            
            htmlStr = htmlStr.Replace("http://res.zngirls.com/style/nivo-slider-min.css", "../res.zngirls.com/style/nivo-slider-min.css");
            htmlStr = htmlStr.Replace("http://res.zngirls.com/script/jquery.nivo.slider.js", "../res.zngirls.com/script/jquery.nivo.slider.js");
            
            htmlStr = htmlStr.Replace("http://res.zngirls.com/script/nvshen_side.js", "../res.zngirls.com/script/nvshen_side.js");

        }
        #endregion


        #region replaceData_Original
        public void replaceData_Original()
        {
            htmlStr = htmlStr.Replace("data-original=\'http://t1.zngirls.com/", "src='../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://t2.zngirls.com/", "src='../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://img.zngirls.com/", "src='../img.zngirls.com/");

            htmlStr = htmlStr.Replace("src=\'http://t1.zngirls.com/", "src='../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://t2.zngirls.com/", "src='../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://img.zngirls.com/", "src='../img.zngirls.com/");
            
        }

        #endregion

        #region topwelcome部分
        public void topwelcome()
        {
            htmlStr = htmlStr.Replace("<a href=\"/message/\">访客留言</a>", "<a href=\"message/index.htm\">访客留言</a>");
        }
        #endregion

        #region topnav上部导航部分
        public void topnav()
        {
            //首页
            htmlStr = htmlStr.Replace("<a href=\"/\" title=\"首  页\" class=\"home\"></a>", "<a href=\"index.htm\" title=\"首  页\" class=\"home\"></a>");

            //最新情报
            htmlStr = htmlStr.Replace("<a href=\"/article/\">最新情报</a>", "<a href=\"article/index.htm\">最新情报</a>");

            //宅男女神专区
            htmlStr = htmlStr.Replace("<a href=\"/find/\">宅男女神专区</a>", "<a href=\"find/index.htm\">宅男女神专区</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/neidi/\">内地宅男女神</a>", "<a href=\"tag/neidi/index.htm\">内地宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/taiwan/\">台湾宅男女神</a>", "<a href=\"tag/taiwan/index.htm\">台湾宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/riben/\">日本宅男女神</a>", "<a href=\"tag/riben/index.htm\">日本宅男女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/tag/yazhou/\">亚洲宅男女神</a>", "<a href=\"tag/yazhou/index.htm\">亚洲宅男女神</a>");

            //美女图片
            htmlStr = htmlStr.Replace("<a href=\"/gallery/\">美女图片</a>", "<a href=\"gallery/index.htm\">美女图片</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/tuigirl/\">推女郎</a>", "<a href=\"gallery/tuigirl/index.htm\">推女郎</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a href=\"gallery/meiyuanguan/index.htm\">美媛馆</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/tgod/\">推女神</a>", "<a href=\"gallery/tgod/index.htm\">推女神</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/ugirl/\">尤果Ugirl</a>", "<a href=\"gallery/ugirl/index.htm\">尤果Ugirl</a>");
            htmlStr = htmlStr.Replace("<a href=\"/gallery/xiuren/\">秀人</a>", "<a href=\"gallery/xiuren/index.htm\">秀人</a>");

            //宅男女神排行榜
            htmlStr = htmlStr.Replace("<a href=\"/rank/\">宅男女神排行榜</a>", "<a href=\"rank/index.htm\">宅男女神排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/neidi/\">内地排行榜</a>", "<a href=\"rank/neidi/index.htm\">内地排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/taiwan/\">台湾排行榜</a>", "<a href=\"rank/taiwan/index.htm\">台湾排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/riben/\">日本排行榜</a>", "<a href=\"rank/riben/index.htm\">日本排行榜</a>");
            htmlStr = htmlStr.Replace("<a href=\"/rank/yazhou/\">亚洲排行榜</a>", "<a href=\"rank/yazhou/index.htm\">亚洲排行榜</a>");

            //女神大PK
            htmlStr = htmlStr.Replace("<a href=\"/arena/\">女神大PK</a>", "<a href=\"arena/index.htm\">女神大PK</a>");

            //倾城·美人榜
            htmlStr = htmlStr.Replace("<a href=\"/tag/\">倾城·美人榜</a>", "<a href=\"tag/index.htm\">倾城·美人榜</a>");

            //巧遇女神
            htmlStr = htmlStr.Replace("<a href=\"/meet/\">巧遇女神</a>", "<a href=\"meet/index.htm\">巧遇女神</a>");

            //女神速配
            htmlStr = htmlStr.Replace("<a href=\"/apply/match.aspx\">女神速配</a>", "<a href=\"apply/match.aspx\">女神速配</a>");

        }
        #endregion

        #region search_box 热门查询
        public void search_box()
        {
            htmlStr = htmlStr.Replace("<a title=\"推女郎\" href=\"/gallery/tuigirl/\">推女郎</a>", "<a title=\"推女郎\" href=\"gallery/tuigirl/index.htm\">推女郎</a>");
            htmlStr = htmlStr.Replace("<a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a>", "<a title=\"推女神\" href=\"gallery/tgod/index.htm\">推女神</a>");
            htmlStr = htmlStr.Replace("<a title=\"美媛馆\" href=\"/gallery/meiyuanguan/\">美媛馆</a>", "<a title=\"美媛馆\" href=\"gallery/meiyuanguan/index.htm\">美媛馆</a>");
            htmlStr = htmlStr.Replace("<a title=\"波萝社\" href=\"/gallery/bololi/\">波萝社</a>", "<a title=\"波萝社\" href=\"gallery/bololi/index.htm\">波萝社</a>");
            htmlStr = htmlStr.Replace("<a title=\"魅妍社\" href=\"/gallery/mistar/\">魅妍社</a>", "<a title=\"魅妍社\" href=\"gallery/mistar/index.htm\">魅妍社</a>");
            htmlStr = htmlStr.Replace("<a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a>", "<a title=\"优星馆\" href=\"gallery/uxing/index.htm\">优星馆</a>");
            htmlStr = htmlStr.Replace("<a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a>", "<a title=\"模范学院\" href=\"gallery/mfstar/index.htm\">模范学院</a>");
        }
        #endregion

        #region search_artile
        public void search_artile()
        {
            #region 文章数量
            string articleConent = FileSubstring.getContent(htmlStr, "<div id=\"slider\" class=\"nivoSlider\">", "</div>");
            int num = FileSubstring.getLabelCount(articleConent, "<a href=\'");
            List<string> articleStr = FileSubstring.getSrc(articleConent, "<a href=\'", "\'",num);
            string articleNum = articleStr[0];
            int index = articleNum.LastIndexOf("/");
            articleNum = articleNum.Substring(index+1);
            //MessageBox.Show(articleNum);
            PageInfo.article = articleNum;

            #endregion
        }
        #endregion

        #region 美女数量
        public void search_girl()
        {
            #region 美女数量
            string girlConent = FileSubstring.getContent(htmlStr, "<div class=\"post_entry\"><ul><li class=\'girlli\'><div class=\'d-tmb\'>", "</div>");
            int GirlNum = FileSubstring.getLabelCount(girlConent, "<a class=\'d-tmb-a\' target=\'_blank\' href=\'");
            List<string> girlStr = FileSubstring.getSrc(girlConent, "<a class=\'d-tmb-a\' target=\'_blank\' href=\'", "\'", GirlNum);
            string girlNum = girlStr[0];
            int girlindex = girlNum.IndexOf("/girl/");
            girlNum = girlNum.Substring(girlindex + 6);
            girlNum = girlNum.Substring(0, 5);
            PageInfo.girl = girlNum;
            #endregion
        }

        #endregion

        public void ReplaceGalleryAll()
        {
            search_gallery();
        }

        #region 写真数量 
        public void search_gallery()
        {
            #region 写真数量
            string galleryConent = FileSubstring.getContent(htmlXieZhen, "<ul><li class=\'galleryli\'><div class=\'galleryli_div\'>", "</ul>");
            int galleryNum = FileSubstring.getLabelCount(galleryConent, "<a class=\'galleryli_link\' href=\'");
            List<string> galleryStr = FileSubstring.getSrc(galleryConent, "<a class=\'galleryli_link\' href=\'", "\' >", galleryNum);
            string strgalleryNum = galleryStr[0];
            int girlindex = strgalleryNum.IndexOf("/g/");
            strgalleryNum = strgalleryNum.Substring(girlindex + 3);
            strgalleryNum = strgalleryNum.Substring(0, 5);
            PageInfo.g = strgalleryNum;
            #endregion
        }
        #endregion
    }
}
