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
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Threading;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;

namespace www_zngirls_com_g
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MainG frm = new MainG();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Form6 frm = new Form6();
            //frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainGallery frm = new MainGallery();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainGDownPicture downpic = new MainGDownPicture();
            downpic.Show();
        }


        private void button12_Click(object sender, EventArgs e)
        {
            MainGalleryPic pic = new MainGalleryPic();
            pic.Show();
        }

        private void 修正首页wwwzngirlscomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainIndex frm = new MainIndex();
            frm.Show();
        }

        private void 下载人物头像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainDownHead fromDownHead = new MainDownHead();
            fromDownHead.Show();
        }



        private void girlToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MainGirl frm = new MainGirl();
            frm.Show();
        }

        private void 下载首页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainDownIndex frmIndex = new MainDownIndex();
            frmIndex.Show();

        }



        private void 倾城美人榜httpwwwzngirlscomtagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTag mainTag = new MainTag();
            mainTag.Show();
        }

        private void 美女图片httpwwwzngirlscomgalleryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainGirlG frmg = new MainGirlG();
            frmg.Show();
        }


        private void httpwwwzngirlscomfindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///下载girl信息
            MainGirlDown down = new MainGirlDown();
            down.Show();
        }
        string htmlStr = "";

        string htmlXieZhen = "";
        private void MainForm_Load(object sender, EventArgs e)
        {
            Thread threadDown = new Thread(new ThreadStart(init));
            threadDown.IsBackground = true;
            threadDown.Start();


            InitData();

            //加载新添加的图片
            //  init();
        }
        #region 循环加载新添加的图片
        public void init()
        {

            htmlStr = DownFile.GetHtmlStr("http://www.zngirls.com", "UTF8");
            search_artile();//计算文章数量
            search_girl();//计算美女数量

            htmlXieZhen = DownFile.GetHtmlStr("http://www.zngirls.com/gallery/", "UTF8");
            search_gallery();

            barStaticItem6.Caption = PageInfo.article;
            barStaticItem7.Caption = PageInfo.girl;
            barStaticItem8.Caption = PageInfo.g;
           // label9.Text = DateTime.Now.ToLongDateString();
        }


        #endregion



        #region search_artile
        public void search_artile()
        {
            #region 文章数量
            string articleConent = FileSubstring.getContent(htmlStr, "<div id=\"slider\" class=\"nivoSlider\">", "</div>");
            int num = FileSubstring.getLabelCount(articleConent, "<a href=\'");
            List<string> articleStr = FileSubstring.getSrc(articleConent, "<a href=\'", "\'", num);
            string articleNum = articleStr[0];
            int index = articleNum.LastIndexOf("/");
            articleNum = articleNum.Substring(index + 1);
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


        private void 指定目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainDirectory mainD = new MainDirectory();
            mainD.Show();
        }

        private void 美女图片所有链接WwwzngirlscomgalleryindexhtmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainGallery gallery = new MainGallery();
            gallery.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 美女写真目录下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            MainGallery gallery = new MainGallery();
            gallery.Show();
        }
        /// <summary>
        /// 美女写真下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            MainGirlG frmg = new MainGirlG();
        
            frmg.Show();
        }


        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            ///下载girl信息
            MainGirlDown down = new MainGirlDown();
            down.Show();
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("编号", typeof(int));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("颜色", typeof(string));
            dt.Rows.Add(new object[] { 1, "Howard Snyder", "Red" });
            dt.Rows.Add(new object[] { 2, "Jaime Yorres", "Blue" });
            dt.Rows.Add(new object[] { 3, "Fran Wilson", "Orange" });
            dt.Rows.Add(new object[] { 4, "Liz Nixon", "White" });
            dt.Rows.Add(new object[] { 5, "Liu Wong", "Red" });
            gridControl1.DataSource = dt;
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            MainDownPic downPic = new MainDownPic();
            
            downPic.Show();
        }
        /// <summary>
        /// 美人榜下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            MainTag tag = new MainTag();
            tag.Show();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            MainArticle article = new MainArticle();
            article.Show();
        }


        string imageDir = @"Images\";
        Hashtable images = new Hashtable();
        public string GetFileName(string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                return "null.jpg";
            }
            return color + ".jpg";
        }


        private void layoutView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "layoutViewColumn4" && e.IsGetData)
            {
                GridView view = sender as GridView;
                string colorName = (string)((DataRowView)e.Row)["颜色"];
                string fileName = GetFileName(colorName).ToLower();
                if (!images.ContainsKey(fileName))
                {
                    Image img = null;
                    try
                    {
                        string filePath = DevExpress.Utils.FilesHelper.FindingFileName(Application.StartupPath, imageDir + fileName, false);
                        img = Image.FromFile(filePath);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    images.Add(fileName, img);
                }
                e.Value = images[fileName];
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
