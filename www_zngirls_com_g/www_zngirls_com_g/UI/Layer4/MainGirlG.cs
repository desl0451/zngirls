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
using DevExpress.XtraEditors;
namespace www_zngirls_com_g
{

    public partial class MainGirlG : DevExpress.XtraEditors.XtraForm
    {
        public MainGirlG()
        {
            InitializeComponent();
        }
        string htmlStr = "";
        string RootPath = "";
        string childHtml = "";
        int pageNum = 0;//子页编号
        string url = "";
        private void Execute()
        {
            string s = textBox2.Text;
            s = s + "\\www.zngirls.com\\g";
            int sindex = s.IndexOf("zngirls");
            RootPath = s.Substring(0, sindex + "zngirls".Length);

            string beginNo = textBox3.Text;
            string endNo = textBox4.Text;
            string SavePath = s;
            for (int j = Convert.ToInt32(beginNo); j <= Convert.ToInt32(endNo); j++)
            {

                string path = string.Format("http://www.zngirls.com/g/{0}/", j);
                url = j.ToString();
                htmlStr = DownFile.GetHtmlStr(path, "UTF8");
                SavePath = s;
                SavePath = SavePath + "\\" + j;
                if (htmlStr.IndexOf("抱歉，您查找的页面不存在或已删除") != -1)
                {
                    continue;
                }
                DownFile.CreateDirectory(SavePath);
                //caroufredsel_wrapper 判断是否是单个页面
                if (htmlStr.IndexOf("caroufredsel_wrapper") != -1)
                {
                    styleReplace();//样式替换

                    topnav();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

                    caroufredsel_wrapper();//读取并下载图片

                    album_tags();

                    replaceData_Original();

                    replace_galleryli_div();

                    ZuiJinReMen();//最近热门

                    DownFile.createHtml(htmlStr, SavePath);
                }
                else {

                    replaceALL();//去掉部分内容

                    DownFile.createHtml(htmlStr, SavePath);


                    int num = 0;
                    //循环下载有多少个子页
                    for (int i = 1; i <= 500; i++)
                    {
                        path = string.Format("http://www.zngirls.com/g/{0}/{1}", j, i + ".html");
                        childHtml = DownFile.GetHtmlStr(path, "UTF8");
                        num = hgalleryCount();
                        childReplaceAll();//去掉部分内容

                        if (num == 0)
                        {
                            break;
                        }
                        sindex = s.IndexOf("zngirls");
                        RootPath = s.Substring(0, sindex + "zngirls".Length);
                        SavePath = textBox2.Text + "\\www.zngirls.com\\g" + "\\" + j + "\\" + i + ".html";
                        pageNum = i;
                        DownFile.createChildHtml(childHtml, SavePath);

                        SavePath = "";
                    }
                }
            }
            threadDown.Abort();
        }

        public void ExecuteAdd()
        {

            //  listView1.Items.Add(RootPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 判断网页是否存在
        /// </summary>
        public void checkUrl()
        {

        }
        /// <summary>
        /// 替换内容
        /// </summary>
        public void replaceALL()
        {
            styleReplace();//样式替换


            topnav();//   最新情报  宅男女神专区  美女图片  宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            //search_box();//热门查询：推女郎  推女神  美媛馆  波萝社  魅妍社  优星馆  模范学院
            album_tags();

            hgallery();//写真图片

            pages();

            IndexQiTaTuJi();//index页面其它图集

            IndexZuiJinReMen();//index 最近更新的热门图集

            delScript();//去掉大段script
            replaceData_Original();
        }
        public void rul()
        {
            string strHtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strHtml, "<h3>你可能感兴趣的其他图集</h3><ul id=\"rul\">", "</li></ul>");
            int num = FileSubstring.getLabelCount(oldhtml, "<div class=\'galleryli_div\'><a href=\'");
            List<string> listrul = FileSubstring.getSrc(oldhtml, "<a href=\'", "\'", num);
            string oldh = "";
            string newh = "";
            foreach (string i in listrul)
            {
                oldh = "<a href=\'" + i;
                newh = "<a href=\'" + "../.." + i + "index.htm";
                strHtml = strHtml.Replace(oldh, newh);
            }
            childHtml = strHtml;
        }

        public void childReplaceAll()
        {
            stylechildReplace();//样式替换

            topnavchild(); //最新情报 宅男女神专区  美女图片 宅男女神排行榜 女神大PK 倾城·美人榜 巧遇女神 女神速配

            hgallerychild();

            album_child_tags();

            childpages();

            ChildZuiJinReMen();//最近更新的热门图集

            ChildIndexQiTaTuJi();//你可能感兴趣的其他图集

            replaceData_Original_Child();

            childDelScript();//去掉Script
        }
        /// <summary>
        /// 样式替换
        /// </summary>
        public void styleReplace()
        {
            htmlStr = htmlStr.Replace("<link href=\"http://res.zngirls.com/style/g.css\" rel=\"stylesheet\" />", "<link href=\"../../../res.zngirls.com/style/g.css\" rel=\"stylesheet\" />");
            htmlStr = htmlStr.Replace("<script type=\"text/javascript\" src=\"http://apps.bdimg.com/libs/jquery/2.1.1/jquery.min.js\">", "<script type=\"text/javascript\" src=\"../../../apps.bdimg.com/libs/jquery/2.1.1/jquery.min.js\">");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/carouFredSel.js\"></script>", "<script src=\"../../../res.zngirls.com/script/carouFredSel.js\"></script>");
            htmlStr = htmlStr.Replace("<script type=\'text/javascript\' src=\'http://images.sohu.com/cs/jsfile/js/c.js\'></script>", "");

            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.mousewheel.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.mousewheel.min.js\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>");
            htmlStr = htmlStr.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");
            htmlStr = htmlStr.Replace("<script src=\"http://libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>", "<script src=\"../../../libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>");
            htmlStr = htmlStr.Replace("http://www.zngirls.com", "");
            htmlStr = htmlStr.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
            htmlStr = htmlStr.Replace("<script src=\"http://cpro.baidustatic.com/cpro/ui/c.js\" type=\"text/javascript\"></script>", "");
        }
        public void stylechildReplace()
        {
            childHtml = childHtml.Replace("<link href=\"http://res.zngirls.com/style/g.css\" rel=\"stylesheet\" />", "<link href=\"../../../res.zngirls.com/style/g.css\" rel=\"stylesheet\" />");
            childHtml = childHtml.Replace("<script type=\"text/javascript\" src=\"http://apps.bdimg.com/libs/jquery/2.1.1/jquery.min.js\">", "<script type=\"text/javascript\" src=\"../../../apps.bdimg.com/libs/jquery/2.1.1/jquery.min.js\">");
            childHtml = childHtml.Replace("<script src=\"http://res.zngirls.com/script/carouFredSel.js\"></script>", "<script src=\"../../../res.zngirls.com/script/carouFredSel.js\"></script>");
            childHtml = childHtml.Replace("<script type=\'text/javascript\' src=\'http://images.sohu.com/cs/jsfile/js/c.js\'></script>", "");
            childHtml = childHtml.Replace("<script src=\"http://res.zngirls.com/script/nvshen_side.js\" type=\"text/javascript\"></script>", "");
            childHtml = childHtml.Replace("<script src=\"http://res.zngirls.com/script/jquery.mousewheel.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.mousewheel.min.js\"></script>");
            childHtml = childHtml.Replace("<script src=\"http://res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>", "<script src=\"../../../res.zngirls.com/script/jquery.touchSwipe.min.js\"></script>");
            childHtml = childHtml.Replace("<script src=\"http://libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>", "<script src=\"../../../libs.useso.com/js/jquery.lazyload/1.9.1/jquery.lazyload.min.js\"></script>");
            childHtml = childHtml.Replace("<script src=\"http://cpro.baidustatic.com/cpro/ui/c.js\" type=\"text/javascript\"></script>", "");

            childHtml = childHtml.Replace("http://www.zngirls.com", "");
        }

        #region topnav上部导航部分
        public void topnavchild()
        {
            //宅男女神
            childHtml = childHtml.Replace("<div class=\"fl\"><a href=\"/\">宅男女神</a>美女图片</div>", "<div class=\"fl\"><a href=\"../../tag/index.htm\">宅男女神</a>美女图片</div>");
            //首页
            childHtml = childHtml.Replace("<li><a href=\"/\">首页</a></li>", "<li><a href=\"../../index.htm\">首页</a></li>");

            //巧遇美女
            childHtml = childHtml.Replace("<li><a href=\"/meet/\">巧遇美女</a></li>", "<li><a href=\"../../meet/index.htm\">巧遇美女</a></li>");

            //图片分类
            childHtml = childHtml.Replace("<li><a href=\"/gallery/\"><span>图片分类</span><i></i></a>", "<li><a href=\"../../gallery/index.htm\"><span>图片分类</span><i></i></a>");

            childHtml = childHtml.Replace("<li><a href=\"/gallery/qingchun/\">清纯</a></li>", "<li><a href=\"../../gallery/qingchun/index.htm\">清纯</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/qingxin/\">清新</a></li>", "<li><a href=\"../../gallery/qingxin/index.htm\">清新</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/zhiyu/\">治愈</a></li>", "<li><a href=\"../../gallery/zhiyu/index.htm\">治愈</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/qizhi/\">气质</a></li>", "<li><a href=\"../../gallery/qizhi/index.htm\">气质</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/qizhi/\">养眼</a></li>", "<li><a href=\"../../gallery/qizhi/index.htm\">养眼</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/lengyan/\">冷艳</a></li>", "<li><a href=\"../../gallery/lengyan/index.htm\">冷艳</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/xinggan/\">性感</a></li>", "<li><a href=\"../../gallery/xinggan/index.htm\">性感</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/yexing/\">野性</a></li>", "<li><a href=\"../../gallery/yexing/index.htm\">野性</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/meitui/\">美腿</a></li>", "<li><a href=\"../../gallery/meitui/index.htm\">美腿</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/botao/\">波涛汹涌</a></li>", "<li><a href=\"../../gallery/botao/index.htm\">波涛汹涌</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/xiongqi/\">人间胸器</a></li>", "<li><a href=\"../../gallery/xiongqi/index.htm\">人间胸器</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/bijini/\">比基尼</a></li>", "<li><a href=\"../../gallery/bijini/index.htm\">比基尼</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/shishen/\">湿身</a></li>", "<li><a href=\"../../gallery/shishen/index.htm\">湿身</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/dachidu/\">大尺度</a></li>", "<li><a href=\"../../gallery/dachidu/index.htm\">大尺度</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/jiepai/\">街拍</a></li>", "<li><a href=\"../../gallery/jiepai/index.htm\">街拍</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/jiaju/\">家居</a></li>", "<li><a href=\"../../gallery/jiaju/index.htm\">家居</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/huwai/\">户外</a></li>", "<li><a href=\"../../gallery/huwai/index.htm\">户外</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/youhuo/\">诱惑</a></li>", "<li><a href=\"../../gallery/youhuo/index.htm\">诱惑</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/zhifu/\">制服</a></li>", "<li><a href=\"../../gallery/zhifu/index.htm\">制服</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/hushi/\">护士</a></li>", "<li><a href=\"../../gallery/hushi/index.htm\">护士</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/xiaofu/\">校服</a></li>", "<li><a href=\"../../gallery/xiaofu/index.htm\">校服</a></li>");
            childHtml = childHtml.Replace("<li><a href=\"/gallery/cos/\">角色扮演</a></li>", "<li><a href=\"../../gallery/cos/index.htm\">角色扮演</a></li>");

            //名站写真
            childHtml = childHtml.Replace("<li><a title=\"推女郎\" href=\'/gallery/tuigirl/\'>推女郎</a></li>", "<li><a title=\"推女郎\" href=\'../../gallery/tuigirl/index.htm\'>推女郎</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"美媛馆\" href=\'/gallery/meiyuanguan/\'>美媛馆</a></li>", "<li><a title=\"美媛馆\" href=\'../../gallery/meiyuanguan/index.htm\'>美媛馆</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a></li>", "<li><a title=\"推女神\" href=\'../../gallery/tgod/index.htm\'>推女神</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"秀人XIUREN\" href=\'/gallery/xiuren/\'>秀人XIUREN</a></li>", "<li><a title=\"秀人XIUREN\" href=\'../../gallery/xiuren/index.htm\'>秀人XIUREN</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"尤果Ugirls\" href=\'/gallery/ugirl/\'>尤果Ugirls</a></li>", "<li><a title=\"尤果Ugirls\" href=\'../../gallery/ugirl/index.htm\'>尤果Ugirls</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"波萝社\" href=\'/gallery/bololi/\'>波萝社</a></li>", "<li><a title=\"波萝社\" href=\'../../gallery/bololi/index.htm\'>波萝社</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"魅妍社\" href=\'/gallery/mistar/\'>魅妍社</a></li>", "<li><a title=\"魅妍社\" href=\'../../gallery/mistar/index.htm\'>魅妍社</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a></li>", "<li><a title=\"优星馆\" href=\'../../gallery/uxing/index.htm\'>优星馆</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"爱蜜社\" href=\'/gallery/imiss/\'>爱蜜社</a></li>", "<li><a title=\"爱蜜社\" href=\'../../gallery/imiss/index.htm\'>爱蜜社</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a></li>", "<li><a title=\"模范学院\" href=\'../../gallery/mfstar/index.htm\'>模范学院</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"嗲囡囡\" href=\'/gallery/feilin/\'>嗲囡囡</a></li>", "<li><a title=\"嗲囡囡\" href=\'../../gallery/feilin/index.htm\'>嗲囡囡</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"爱尤物\" href=\'/gallery/aiyouwu/\'>爱尤物</a></li>", "<li><a title=\"爱尤物\" href=\'../../gallery/aiyouwu/index.htm\'>爱尤物</a></li>");

            //日本套图
            childHtml = childHtml.Replace("<li><a title=\"DGC\" href=\'/gallery/dgc/\'>DGC</a></li>", "<li><a title=\"DGC\" href=\'../../gallery/dgc/index.htm\'>DGC</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"YS-Web\" href=\'/gallery/ysweb/\'>YS-Web</a></li>", "<li><a title=\"YS-Web\" href=\'../../gallery/ysweb/index.htm\'>YS-Web</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"RQ-Star\" href=\'/gallery/rqstar/\'>RQ-Star</a></li>", "<li><a title=\"RQ-Star\" href=\'../../gallery/rqstar/index.htm\'>RQ-Star</a></li>");
            childHtml = childHtml.Replace("<li><a title=\"4K-Star\" href=\'/gallery/4kstar/\'>4K-Star</a></li>", "<li><a title=\"4K-Star\" href=\'../../gallery/4kstar/index.htm\'>4K-Star</a></li>");


            //女神大PK
            childHtml = childHtml.Replace("<a href=\"/arena/\">女神大PK</a>", "<a href=\"../../arena/index.htm\">女神大PK</a>");

            //倾城·美人榜
            childHtml = childHtml.Replace("<a href=\"/tag/\">倾城·美人榜</a>", "<a href=\"../../tag/index.htm\">倾城·美人榜</a>");

            //巧遇女神
            childHtml = childHtml.Replace("<a href=\"/meet/\">巧遇女神</a>", "<a href=\"../../meet/index.htm\">巧遇女神</a>");

            //女神速配
            childHtml = childHtml.Replace("<a href=\"/apply/match.aspx\">女神速配</a>", "<a href=\"../../apply/match.aspx\">女神速配</a>");

            //邮箱
            childHtml = childHtml.Replace("邮箱:zngirls520@gmail.com", "邮箱:hoho@163.com");
        }
        #endregion

        public void hgallerychild()
        {
            string oldhtml = FileSubstring.getContent(childHtml, "<ul id=\"hgallery\">", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<img src=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<img src=\'", "\'", count);
            //MessageBox.Show(count.ToString());
            List<DownFile> downFile = FileSubstring.addFileList(listahref);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }



        #region topnav上部导航部分
        public void topnav()
        {
            //宅男女神
            htmlStr = htmlStr.Replace("<div class=\"fl\"><a href=\"/\">宅男女神</a>美女图片</div>", "<div class=\"fl\"><a href=\"../../tag/index.htm\">宅男女神</a>美女图片</div>");

            //首页
            htmlStr = htmlStr.Replace("<li><a href=\"/\">首页</a></li>", "<li><a href=\"../../index.htm\">首页</a></li>");

            //巧遇美女
            htmlStr = htmlStr.Replace("<li><a href=\"/meet/\">巧遇美女</a></li>", "<li><a href=\"../../meet/index.htm\">巧遇美女</a></li>");

            //图片分类
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/\"><span>图片分类</span><i></i></a>", "<li><a href=\"../../gallery/index.htm\"><span>图片分类</span><i></i></a>");

            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/qingchun/\">清纯</a></li>", "<li><a href=\"../../gallery/qingchun/index.htm\">清纯</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/qingxin/\">清新</a></li>", "<li><a href=\"../../gallery/qingxin/index.htm\">清新</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/zhiyu/\">治愈</a></li>", "<li><a href=\"../../gallery/zhiyu/index.htm\">治愈</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/qizhi/\">气质</a></li>", "<li><a href=\"../../gallery/qizhi/index.htm\">气质</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/qizhi/\">养眼</a></li>", "<li><a href=\"../../gallery/qizhi/index.htm\">养眼</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/lengyan/\">冷艳</a></li>", "<li><a href=\"../../gallery/lengyan/index.htm\">冷艳</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/xinggan/\">性感</a></li>", "<li><a href=\"../../gallery/xinggan/index.htm\">性感</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/yexing/\">野性</a></li>", "<li><a href=\"../../gallery/yexing/index.htm\">野性</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/meitui/\">美腿</a></li>", "<li><a href=\"../../gallery/meitui/index.htm\">美腿</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/botao/\">波涛汹涌</a></li>", "<li><a href=\"../../gallery/botao/index.htm\">波涛汹涌</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/xiongqi/\">人间胸器</a></li>", "<li><a href=\"../../gallery/xiongqi/index.htm\">人间胸器</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/bijini/\">比基尼</a></li>", "<li><a href=\"../../gallery/bijini/index.htm\">比基尼</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/shishen/\">湿身</a></li>", "<li><a href=\"../../gallery/shishen/index.htm\">湿身</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/dachidu/\">大尺度</a></li>", "<li><a href=\"../../gallery/dachidu/index.htm\">大尺度</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/jiepai/\">街拍</a></li>", "<li><a href=\"../../gallery/jiepai/index.htm\">街拍</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/jiaju/\">家居</a></li>", "<li><a href=\"../../gallery/jiaju/index.htm\">家居</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/huwai/\">户外</a></li>", "<li><a href=\"../../gallery/huwai/index.htm\">户外</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/youhuo/\">诱惑</a></li>", "<li><a href=\"../../gallery/youhuo/index.htm\">诱惑</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/zhifu/\">制服</a></li>", "<li><a href=\"../../gallery/zhifu/index.htm\">制服</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/hushi/\">护士</a></li>", "<li><a href=\"../../gallery/hushi/index.htm\">护士</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/xiaofu/\">校服</a></li>", "<li><a href=\"../../gallery/xiaofu/index.htm\">校服</a></li>");
            htmlStr = htmlStr.Replace("<li><a href=\"/gallery/cos/\">角色扮演</a></li>", "<li><a href=\"../../gallery/cos/index.htm\">角色扮演</a></li>");

            //名站写真
            htmlStr = htmlStr.Replace("<li><a title=\"推女郎\" href=\'/gallery/tuigirl/\'>推女郎</a></li>", "<li><a title=\"推女郎\" href=\'../../gallery/tuigirl/index.htm\'>推女郎</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"美媛馆\" href=\'/gallery/meiyuanguan/\'>美媛馆</a></li>", "<li><a title=\"美媛馆\" href=\'../../gallery/meiyuanguan/index.htm\'>美媛馆</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"推女神\" href=\'/gallery/tgod/\'>推女神</a></li>", "<li><a title=\"推女神\" href=\'../../gallery/tgod/index.htm\'>推女神</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"秀人XIUREN\" href=\'/gallery/xiuren/\'>秀人XIUREN</a></li>", "<li><a title=\"秀人XIUREN\" href=\'../../gallery/xiuren/index.htm\'>秀人XIUREN</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"尤果Ugirls\" href=\'/gallery/ugirl/\'>尤果Ugirls</a></li>", "<li><a title=\"尤果Ugirls\" href=\'../../gallery/ugirl/index.htm\'>尤果Ugirls</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"波萝社\" href=\'/gallery/bololi/\'>波萝社</a></li>", "<li><a title=\"波萝社\" href=\'../../gallery/bololi/index.htm\'>波萝社</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"魅妍社\" href=\'/gallery/mistar/\'>魅妍社</a></li>", "<li><a title=\"魅妍社\" href=\'../../gallery/mistar/index.htm\'>魅妍社</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"优星馆\" href=\'/gallery/uxing/\'>优星馆</a></li>", "<li><a title=\"优星馆\" href=\'../../gallery/uxing/index.htm\'>优星馆</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"爱蜜社\" href=\'/gallery/imiss/\'>爱蜜社</a></li>", "<li><a title=\"爱蜜社\" href=\'../../gallery/imiss/index.htm\'>爱蜜社</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"模范学院\" href=\'/gallery/mfstar/\'>模范学院</a></li>", "<li><a title=\"模范学院\" href=\'../../gallery/mfstar/index.htm\'>模范学院</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"嗲囡囡\" href=\'/gallery/feilin/\'>嗲囡囡</a></li>", "<li><a title=\"嗲囡囡\" href=\'../../gallery/feilin/index.htm\'>嗲囡囡</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"爱尤物\" href=\'/gallery/aiyouwu/\'>爱尤物</a></li>", "<li><a title=\"爱尤物\" href=\'../../gallery/aiyouwu/index.htm\'>爱尤物</a></li>");

            //日本套图
            htmlStr = htmlStr.Replace("<li><a title=\"DGC\" href=\'/gallery/dgc/\'>DGC</a></li>", "<li><a title=\"DGC\" href=\'../../gallery/dgc/index.htm\'>DGC</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"YS-Web\" href=\'/gallery/ysweb/\'>YS-Web</a></li>", "<li><a title=\"YS-Web\" href=\'../../gallery/ysweb/index.htm\'>YS-Web</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"RQ-Star\" href=\'/gallery/rqstar/\'>RQ-Star</a></li>", "<li><a title=\"RQ-Star\" href=\'../../gallery/rqstar/index.htm\'>RQ-Star</a></li>");
            htmlStr = htmlStr.Replace("<li><a title=\"4K-Star\" href=\'/gallery/4kstar/\'>4K-Star</a></li>", "<li><a title=\"4K-Star\" href=\'../../gallery/4kstar/index.htm\'>4K-Star</a></li>");


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

        #region hgallery 写真图片
        public void hgallery()
        {
            string oldhtml = FileSubstring.getContent(htmlStr, "<ul id=\"hgallery\">", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<img src=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<img src=\'", "\'", count);
            //MessageBox.Show(count.ToString());
            List<DownFile> downFile = FileSubstring.addFileList(listahref);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }
        #endregion


        #region 计算图片数量
        public int hgalleryCount()
        {

            string oldhtml = FileSubstring.getContent(childHtml, "<ul id=\"hgallery\">", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<img src=\'");
            return count;
        }
        #endregion

        #region 
        /// <summary>
        /// utag
        /// </summary>
        public void album_child_tags()
        {
            string strhtml = childHtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<ul id=\"utag\">", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a target=\'_blank\' href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a target=\'_blank\' href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a target=\'_blank\' href=\'" + i;
                newh = "<a target=\'_blank\' href=\'" + "../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);
            }
            childHtml = strhtml;
        }
        #endregion

        #region replaceData_Original http://t1.zngirls.com/
        public void replaceData_Original()
        {
            htmlStr = htmlStr.Replace("data-original=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("data-original=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

            htmlStr = htmlStr.Replace("src=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            htmlStr = htmlStr.Replace("src=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

        }
        #endregion
        private void MainGirlG_Load(object sender, EventArgs e)
        {
            textBox2.Text = PageInfo.path;
            //读取最新文件夹
            ReadLabelNum();
        }
        #region 初始化读取编号
        public void ReadLabelNum()
        {
            string path = PageInfo.path;
            path = path + "/www.zngirls.com/g/";
            DirectoryInfo dinfo = new DirectoryInfo(path);
            DirectoryInfo[] childDinfo = dinfo.GetDirectories();
            int count = childDinfo.Length;
            string name = childDinfo[count - 1].Name;
            textBox3.Text = name;
            textBox4.Text = PageInfo.g;
        }
        #endregion
        /// <summary>
        /// utag
        /// </summary>
        public void album_tags()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<ul id=\"utag\">", "</ul>");
            int count = FileSubstring.getLabelCount(oldhtml, "<a target=\'_blank\' href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<a target=\'_blank\' href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {
                oldh = "<a target=\'_blank\' href=\'" + i;
                newh = "<a target=\'_blank\' href=\'" + "../.." + i + "index.htm";
                strhtml = strhtml.Replace(oldh, newh);
            }
            htmlStr = strhtml;
            string delCut = FileSubstring.getContent(htmlStr, "var _bdhmProtocol", "));");
            htmlStr = htmlStr.Replace(delCut, "");
        }



        /// <summary>
        /// pages
        /// </summary>
        public void pages()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<div id=\"pages\">", "</div>");
            int count = FileSubstring.getLabelCount(oldhtml, "href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);

            string oldh = "";
            string newh = "";



            for (int i = 1; i < listahref.Count; i++)
            {

                oldh = "href=\'" + listahref[i];
                newh = "href=\'" + "../.." + listahref[i];

                strhtml = strhtml.Replace(oldh, newh);

            }

            oldh = "href=\'" + listahref[0];
            newh = "href=\'" + "../.." + listahref[0] + "/index.htm";

            strhtml = strhtml.Replace(oldh, newh);




            htmlStr = strhtml;
        }


        /// <summary>
        /// pages
        /// </summary>
        public void childpages()
        {
            string strhtml = childHtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<div id=\"pages\">", "</div>");
            int count = FileSubstring.getLabelCount(oldhtml, "href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {

                oldh = "href=\'" + i;
                newh = "href=\'" + "../.." + i;

                strhtml = strhtml.Replace(oldh, newh);
            }


            if (listahref[listahref.Count - 1].IndexOf(".html") == -1)
            {
                oldh = "<a class='a1' href=\'" + "../.." + listahref[listahref.Count - 1];
                newh = "<a class='a1' href=\'" + "../.." + listahref[listahref.Count - 1] + "/" + (pageNum + 1) + ".html";

                strhtml = strhtml.Replace(oldh, newh);
            }
            childHtml = strhtml;
        }

        public void replaceData_Original_Child()
        {
            childHtml = childHtml.Replace("data-original=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtml = childHtml.Replace("data-original=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtml = childHtml.Replace("data-original=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

            childHtml = childHtml.Replace("src=\'http://t1.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtml = childHtml.Replace("src=\'http://t2.zngirls.com/", "src='../../../t1.zngirls.com/");
            childHtml = childHtml.Replace("src=\'http://img.zngirls.com/", "src='../../../img.zngirls.com/");

            string delCut = FileSubstring.getContent(childHtml, "var _bdhmProtocol", "));");
            childHtml = childHtml.Replace(delCut, "");
        }

        public void replace_galleryli_div()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<h3>你可能感兴趣的其他图集</h3>", "</div>");

            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'galleryli_div\'><a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_div\'><a href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {

                oldh = "<div class=\'galleryli_div\'><a href=\'" + i;
                newh = "<div class=\'galleryli_div\'><a href=\'" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }


            htmlStr = strhtml;
        }

        public void ZuiJinReMen()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<h3>最近更新的热门图集</h3>", "</div>");

            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'galleryli_div\'><a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_div\'><a href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {

                oldh = "<div class=\'galleryli_div\'><a href=\'" + i;
                newh = "<div class=\'galleryli_div\'><a href=\'" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }
            htmlStr = strhtml;

            htmlStr = htmlStr.Replace("<img src=\"/images/arrowkeys.gif\" alt=\"使用左右键进行导航\" />", "<img src=\"../../images/arrowkeys.gif\" alt=\"使用左右键进行导航\"/>");
        }
        /// <summary>
        /// 下载图片
        /// </summary>
        public void caroufredsel_wrapper()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<div class=\"caroufredsel_wrapper\">", "</div>");

            int count = FileSubstring.getLabelCount(oldhtml, "<img alt=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "src=\'", "\'", count);

            List<DownFile> downFile = FileSubstring.addFileList(listahref);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }




        #region 热门高清多页面
        public void IndexZuiJinReMen()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<h3>你可能感兴趣的其他图集</h3>", "</ul>");

            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'galleryli_div\'><a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_div\'><a href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {

                oldh = "<div class=\'galleryli_div\'><a href=\'" + i;
                newh = "<div class=\'galleryli_div\'><a href=\'" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }

            List<string> galleryli_title = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_title\'><a href=\'", "\'", count);


            foreach (string i in galleryli_title)
            {

                oldh = "<div class='galleryli_title'><a href='" + i;
                newh = "<div class='galleryli_title'><a href='" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }




            htmlStr = strhtml;

            //下载热门高清图片
            List<string> downPic = FileSubstring.getSrc(oldhtml, "<img data-original=\'", "\'", count);

            List<DownFile> downFile = FileSubstring.addFileList(downPic);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }
        #endregion

        #region IndexQiTaTuJi Index其它图集
        public void IndexQiTaTuJi()
        {
            string strhtml = htmlStr;
            string oldhtml = FileSubstring.getContent(strhtml, "<h3>最近更新的热门图集</h3>", "</ul>");

            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'galleryli_div\'><a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_div\'><a href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {

                oldh = "<div class=\'galleryli_div\'><a href=\'" + i;
                newh = "<div class=\'galleryli_div\'><a href=\'" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }

            List<string> galleryli_title = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_title\'><a href=\'", "\'", count);


            foreach (string i in galleryli_title)
            {

                oldh = "<div class='galleryli_title'><a href='" + i;
                newh = "<div class='galleryli_title'><a href='" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }


            htmlStr = strhtml;

            //下载热门高清图片
            List<string> downPic = FileSubstring.getSrc(oldhtml, "<img data-original=\'", "\'", count);

            List<DownFile> downFile = FileSubstring.addFileList(downPic);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }
        #endregion



        #region 子页热门高清多页面
        public void ChildZuiJinReMen()
        {
            string strhtml = childHtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<h3>你可能感兴趣的其他图集</h3>", "</ul>");

            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'galleryli_div\'><a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_div\'><a href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {

                oldh = "<div class=\'galleryli_div\'><a href=\'" + i;
                newh = "<div class=\'galleryli_div\'><a href=\'" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }

            List<string> galleryli_title = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_title\'><a href=\'", "\'", count);


            foreach (string i in galleryli_title)
            {

                oldh = "<div class='galleryli_title'><a href='" + i;
                newh = "<div class='galleryli_title'><a href='" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }

            childHtml = strhtml;

            //下载热门高清图片
            List<string> downPic = FileSubstring.getSrc(oldhtml, "<img data-original=\'", "\'", count);

            List<DownFile> downFile = FileSubstring.addFileList(downPic);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }
        #endregion

        #region ChildIndexQiTaTuJi Index其它图集
        public void ChildIndexQiTaTuJi()
        {
            string strhtml = childHtml;
            string oldhtml = FileSubstring.getContent(strhtml, "<h3>最近更新的热门图集</h3>", "</ul>");

            int count = FileSubstring.getLabelCount(oldhtml, "<div class=\'galleryli_div\'><a href=\'");

            List<string> listahref = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_div\'><a href=\'", "\'", count);

            string oldh = "";
            string newh = "";
            foreach (string i in listahref)
            {

                oldh = "<div class=\'galleryli_div\'><a href=\'" + i;
                newh = "<div class=\'galleryli_div\'><a href=\'" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }

            List<string> galleryli_title = FileSubstring.getSrc(oldhtml, "<div class=\'galleryli_title\'><a href=\'", "\'", count);


            foreach (string i in galleryli_title)
            {

                oldh = "<div class='galleryli_title'><a href='" + i;
                newh = "<div class='galleryli_title'><a href='" + "../.." + i + "index.htm";

                strhtml = strhtml.Replace(oldh, newh);
            }

            childHtml = strhtml;

            //下载热门高清图片
            List<string> downPic = FileSubstring.getSrc(oldhtml, "<img data-original=\'", "\'", count);

            List<DownFile> downFile = FileSubstring.addFileList(downPic);
            //获取下载地址
            DownFile.WebClientDownFile(downFile, RootPath + "\\");
        }
        #endregion

        public void delScript()
        {
            string strAll = htmlStr;
            string oldhtml = FileSubstring.getContent(strAll, "<script type=\"text/javascript\">", "</script>");
            if (oldhtml.IndexOf("$(document)") != -1)
            {
                htmlStr = htmlStr.Replace(oldhtml, "");
            }
        }

        public void childDelScript()
        {

            string strAll = childHtml;
            string oldhtml = FileSubstring.getContent(strAll, "<script type=\"text/javascript\">", "</script>");
            if (oldhtml.IndexOf("$(document)") != -1)
            {
                childHtml = childHtml.Replace(oldhtml, "");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (threadDown.ThreadState == ThreadState.Stopped)
            //{
            //    threadDown
            //}
            //else if (threadDown.ThreadState == ThreadState.Running)
            //{
            //    threadDown.Suspend();
            //}
        }

        private void MainGirlG_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                threadDown.Abort();
            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        Thread threadDown = null;
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            threadDown = new Thread(new ThreadStart(Execute));
            threadDown.IsBackground = true;
            threadDown.Start();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                threadDown.Abort();
            }
            catch (Exception)
            {
                MessageBox.Show("停止线程");
            }
        }
    }
}
