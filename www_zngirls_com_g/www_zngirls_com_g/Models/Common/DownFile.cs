using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;
namespace www_zngirls_com_g
{
    public class DownFile
    {
        private string filename;//文件名称
        private string webPath;//互联网路径
        private bool status;//是否下载
        private string savePath;//保存目标路径
        private string path;//路径去文件名
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        /// <summary>
        /// 是否下载
        /// </summary>
        public bool Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
        /// <summary>
        /// 保存目标路径
        /// </summary>
        public string SavePath
        {
            get
            {
                return savePath;
            }

            set
            {
                savePath = value;
            }
        }
        /// <summary>
        /// 互联网路径
        /// </summary>
        public string WebPath
        {
            get
            {
                return webPath;
            }

            set
            {
                webPath = value;
            }
        }
        /// <summary>
        /// 路径去文件名
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }
        public static string GetHtmlStr(string url, string encoding)
        {
            string htmlStr = "";
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    WebRequest request = WebRequest.Create(url); //实例化WebRequest对象
                    WebResponse response = request.GetResponse(); //创建WebResponse对象

                    Stream datastream = response.GetResponseStream();//创建流对象
                    Encoding ec = Encoding.Default;
                    if (encoding == "UTF8")
                    {
                        ec = Encoding.UTF8;
                    }
                    else if (encoding == "Default")
                    {
                        ec = Encoding.Default;
                    }
                    StreamReader reader = new StreamReader(datastream, ec);

                    htmlStr = reader.ReadToEnd();                //读取数据
                    reader.Close();
                    datastream.Close();
                    response.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("文件读取失败!");
                }
                //WebClient myWebClient = new WebClient();

                //Stream myStream = myWebClient.OpenRead(url);

                //StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));

                //htmlStr = sr.ReadToEnd();
                //sr.Close();
                //myStream.Close();
                //myWebClient.Dispose();




            }
            return htmlStr;
        }
        /// <summary>
        /// 读取指定网页的HTML
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadHtml(string path)
        {
            string str = "";
            try
            {
                FileStream fs = new FileStream(path + "\\index.htm", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();

                while (strLine != null)
                {
                    strLine = sr.ReadLine();
                    str = str + strLine;
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("创建文件失败!");
            }
            return str;
        }


        public static string GetDownUrlHTML(string url, string encoding)
        {
            string allhtml = "";
            try
            {

                WebClient MyWebClient = new WebClient();


                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                Byte[] pageData = MyWebClient.DownloadData(url); //从指定网站下载数据

                // string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            

                string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句

                Console.WriteLine(pageHtml);//在控制台输入获取的内容

                allhtml = pageHtml;

                //using (StreamWriter sw = new StreamWriter("c:\\test\\ouput.html"))//将获取的内容写入文本
                //{

                //    sw.Write(pageHtml);
                //}

                Console.ReadLine(); //让控制台暂停,否则一闪而过了             
            }

            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
            }
            return allhtml;
        }



        /// <summary>
        /// 创建网页
        /// </summary>
        /// <param name="htmlStr"></param>
        /// <param name="path"></param>
        public static void createHtml(string htmlStr, string path)
        {
            try
            {
                FileStream fs = new FileStream(path + "\\index.htm", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(htmlStr);
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("创建文件失败!");
                MessageBox.Show("创建文件失败!" + path);
                MessageBox.Show("创建文件失败!" + htmlStr);
            }

        }

        /// <summary>
        /// 创建子页1-N
        /// </summary>
        /// <param name="htmlStr"></param>
        /// <param name="path"></param>
        public static void createChildHtml(string htmlStr, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(htmlStr);
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("创建文件失败!");
            }

        }



        /// <summary>
        /// 加载文件 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<PicFile> LoadFile(string path)
        {
            List<PicFile> list = new List<PicFile>();
            DirectoryInfo dinfo = new DirectoryInfo(@path);
            FileInfo[] finfo = dinfo.GetFiles();
            int index = 0;
            bool cbl = false;
            foreach (FileInfo f in finfo)
            {
                PicFile fpic = new PicFile();
                index = f.Name.IndexOf('.');
                fpic.Kuozhanname = f.Extension;
                cbl = Common.checkFile(fpic.Kuozhanname);
                if (cbl == true)
                {
                    fpic.Filename = f.Name.Substring(0, index);
                    fpic.Allfilename = f.Name;
                    fpic.FilePath = f.FullName;
                    list.Add(fpic);
                }
            }
            return list;
        }

        public static void createFile(List<PicFile> files)
        {
            string allHtml = "";
            string newHtml = "";
            int count = 0;//文件数量
            int errorCount = 0;//错误数量
            StringBuilder filename = new StringBuilder();
            //文件开始循环
            foreach (PicFile item in files)
            {
                try
                {
                    FileStream fs = new FileStream(item.FilePath, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    allHtml = sr.ReadToEnd();//读取全部网页


                    sr.Close();
                    fs.Close();

                    FileStream nfs = new FileStream(item.FilePath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(nfs);
                    sw.Write(newHtml);
                    sw.Close();
                    nfs.Close();
                    count++;
                }
                catch (Exception)
                {
                    filename.Append(item.Filename);
                    errorCount++;
                }
            }
        }
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                dir.Create();
            }
        }

        /// <summary>
        /// 下载单个或多个文件 
        /// </summary>
        /// <param name="listFile"></param>
        /// <param name="path"></param>
        public static void WebClientDownFile(List<DownFile> listFile, string RootPath)
        {
            WebClient webClient = new WebClient();
            string position = "";
            string downfile = "";

            foreach (DownFile f in listFile)
            {
                try
                {
                    webClient = new WebClient();
                    position = RootPath + f.Path;

                    webClient.Proxy = null;

                    downfile = "http://" + f.WebPath;


                    if (position.IndexOf("t2.zngirls.com") != -1)
                    {
                        position = position.Replace("t2.zngirls.com", "t1.zngirls.com");
                    }
                    if (!Directory.Exists(position))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(position);
                        dinfo.Create();
                    }
                    position = position + "\\" + f.Filename;
                    //C: \Users\desl0_000\Desktop\zngirls\t2.zngirls.com\gallery\16569\17892\cover\0.jpg
                    if (!File.Exists(position))
                    {

                        webClient.DownloadFile(new Uri(downfile), @position);

                    }
                    position = "";
                    downfile = "";
                    Helper.downFile = downfile;
                }
                catch (Exception)
                {
                    Console.WriteLine("错误");
                }

            }

            webClient.Dispose();
        }
        public static void WebDownPic(List<string> downList,string RootPath)
        {
            WebClient webClient = new WebClient();
            string position = "";
            string directory = "";
            string downFile = "";
            foreach (string f in downList)
            {
                try
                {
                    webClient = new WebClient();
                    position = RootPath + f.Substring(7);
                    directory = position.Substring(0, position.LastIndexOf("\\"));
                    webClient.Proxy = null;

                    if (position.IndexOf("t2.zngirls.com") != -1)
                    {
                        position = position.Replace("t2.zngirls.com", "t1.zngirls.com");
                    }
                    if (!Directory.Exists(directory))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(directory);
                        dinfo.Create();
                    }
                    position = position.Replace("\\", "/");
                    downFile = f.Replace("\\", "/");
                    //C: \Users\desl0_000\Desktop\zngirls\t2.zngirls.com\gallery\16569\17892\cover\0.jpg
                    if (!File.Exists(position))
                    {

                        webClient.DownloadFile(new Uri(downFile), @position);

                    }
                    position = "";
                    
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("错误");
                }

            }

            webClient.Dispose();
        }

    }
}
