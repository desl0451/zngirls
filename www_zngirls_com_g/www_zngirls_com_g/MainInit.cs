using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraSplashScreen;
namespace www_zngirls_com_g
{
    public partial class MainInit : DemoSplashScreen
    {
        int dotCount = 0;
        public MainInit()
        {
            InitializeComponent();
            labelControl1.Text = string.Format("{0}{1}", labelControl1.Text, GetYearString());

            pictureEdit2.Image = global::www_zngirls_com_g.Properties.Resources.work;

            Timer tmr = new Timer();
            tmr.Interval = 400;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Start();
        }



        void tmr_Tick(object sender, EventArgs e)
        {
            if (++dotCount > 3) dotCount = 0;
            labelControl2.Text = string.Format("{1}{0}", GetDots(dotCount),www_zngirls_com_g.Properties.Resources.Starting);
        }

        string GetDots(int count)
        {
            string ret = string.Empty;
            for (int i = 0; i < count; i++) ret += ".";
            return ret;
        }

        /// <summary>
        /// 返回当前年份
        /// </summary>
        /// <returns></returns>
        int GetYearString()
        {
            int ret = DateTime.Now.Year;
            return (ret < 2015 ? 2015 : ret);
        }
    }
}