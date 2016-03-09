using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.Skins;
using DevExpress.Data.Filtering;

using System.Drawing;
using System.Threading;
using DevExpress.XtraEditors;
namespace www_zngirls_com_g
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 8);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            SkinManager.EnableFormSkins();
            //EnumProcessingHelper.RegisterEnum<TaskStatus>();
            // UnpackHelper.Unpack();

            //Application.Run(new MainLogin());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainLogin());
        }
    }
}
