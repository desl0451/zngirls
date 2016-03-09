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
using System.IO;
using System.Net;
namespace www_zngirls_com_g
{
    public partial class MainDownPic : DevExpress.XtraEditors.XtraForm
    {
        public MainDownPic()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string SavePath = PageInfo.path;
            //XtraMessageBox.Show(SavePath);

            string path = textEdit1.Text;

            if (path.Length > 0)
            {
                int index = path.IndexOf("zngirls");
                path = path.Substring(index + 8);
                //XtraMessageBox.Show(path);

                int beginNo = Convert.ToInt32(textEdit2.Text);
                int endNo = Convert.ToInt32(textEdit3.Text);
                List<string> PicList = new List<string>();
                PicList.Add("http://"+path + "\\cover\\0.jpg");

                PicList.Add("http://" + path + "\\0.jpg");
                for (int i = beginNo; i <= endNo; i++)
                {
                    if (i < 10)
                    {
                        PicList.Add("http://" + path + "/00" + i + ".jpg");
                    }
                    else if (i < 100)
                    {
                        PicList.Add("http://" + path + "/0" + i + ".jpg");
                    }
                    else
                    {
                        PicList.Add("http://" + path + "/" + i + ".jpg");
                    }
                }


                
                DownFile.WebDownPic(PicList, SavePath + "\\");
            }
        }
    }
}