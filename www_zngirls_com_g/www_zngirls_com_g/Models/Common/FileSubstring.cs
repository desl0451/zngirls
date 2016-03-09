using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www_zngirls_com_g
{
    public class FileSubstring
    {
        /// <summary>
        /// 从指定内容中截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getContent(string allhtml, string beginstr, string endstr)
        {
            string str = "";
            int begins = allhtml.IndexOf(beginstr);
            if (begins == -1)
            {
                return "";
            }
            int ends = allhtml.IndexOf(endstr, begins);
            int word = endstr.Length;
            int count = ends - begins + word;
            str = allhtml.Substring(begins, count);
            return str;
        }
        /// <summary>
        /// 去掉容器
        /// </summary>
        /// <param name="allhtml"></param>
        /// <param name="beginstr"></param>
        /// <param name="endstr"></param>
        /// <returns></returns>
        public static string getContent(string allhtml, string beginstr, string endstr, bool content, string path)
        {
            string str = "";
            string s = path;
            int begins = allhtml.IndexOf(@beginstr);
            int ends = allhtml.IndexOf(@endstr, begins);
            if (begins == -1)
            {
                return "";
            }
            int word = endstr.Length;
            int count = ends - begins + word;
            if (content == true)
            {
                str = allhtml.Substring(begins + beginstr.Length, count - beginstr.Length - endstr.Length);
            }
            else
            {
                str = allhtml.Substring(begins, count);
            }
            return str;
        }
        /// <summary>
        /// 返回相同标签的数量
        /// </summary>
        public static int getLabelCount(string content, string label)
        {
            int begin = 0;
            int count = 0;
            for (int i = 0; i < content.Length; i++)
            {
                begin = content.IndexOf(@label, i);
                if (begin == -1)
                {
                    return count;
                }
                i = begin;

                count++;
            }
            return count;
        }
        /// <summary>
        /// 提取src中的路径 
        /// </summary>
        public static List<string> getSrc(string content, string beginsrc, string endsrc, int count) //个数
        {
            List<string> SrcPath = new List<string>();
            int index = 0;//位置
            int begin = 0;//结束坐标 
            int end = 0; //开始坐标
            int n = 0;//截取数量
            string str = "";
            for (int i = 1; i <= count; i++)
            {
                begin = content.IndexOf(beginsrc, index);
                if (begin == -1)
                {
                    continue;
                }
                end = content.IndexOf(endsrc, begin + beginsrc.Length);
                if (end == -1)
                {
                    continue;
                }
                n = end - begin - beginsrc.Length;
                str = content.Substring(begin + beginsrc.Length, n);
                index = end;
                SrcPath.Add(str);
            }
            return SrcPath;
        }

        public static List<DownFile> addFileList(List<string> filelist)
        {
            string web = "";
            int begin = 0;
            List<DownFile> downFiles = new List<DownFile>();
            string directory = "";
            int gangindex = 0;
            string r = "";
            foreach (string s in filelist)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    DownFile df = new DownFile();
                    df.Filename = s.Substring(s.LastIndexOf("/") + 1);
                    web = getCheck(s);
                    begin = s.IndexOf(web);
                    df.WebPath = s.Substring(begin);
                    directory = s.Substring(begin);
                    gangindex = directory.LastIndexOf("/");
                    r = s.Substring(begin, gangindex).Replace("/", "\\");
                    df.Path = r;
                    df.SavePath = s.Substring(begin).Replace("/", "\\");
                    downFiles.Add(df);
                }
            }
            return downFiles;
        }
        public static string getCheck(string path)
        {
            string str = "";
            int index = path.IndexOf("t1.zngirls.com");
            if (index != -1)
            {
                str = "t1.zngirls.com";
            }
            index = path.IndexOf("t2.zngirls.com");
            if (index != -1)
            {
                str = "t2.zngirls.com";
            }
            index = path.IndexOf("img.zngirls.com");
            if (index != -1)
            {
                str = "img.zngirls.com";
            }
            return str;
        }


        #region 提取标签内信息

        /// <summary>
        /// 提取标签内信息
        /// </summary>
        /// <param name="content">需要提取的字符串</param>
        /// <param name="beginlabel">开始的字符串</param>
        /// <param name="endlabel">结束字符串</param>
        /// <returns></returns>
        public static string getLabelContent(string content, string beginlabel, string endlabel)
        {
            string strhtml = "";
            int beginindex = 0;
            int endindex = 0;
            int count = 0;
            beginindex = content.IndexOf(beginlabel);
            endindex = content.IndexOf(endlabel, beginindex + beginlabel.Length);
            count = endindex - beginindex - beginlabel.Length;
            strhtml = content.Substring(beginindex + beginlabel.Length, count);
            return strhtml;
        }

        #endregion


        #region  字符串生成 去掉cut部分  添加addString 
        public static string buildHtml(string content,string cut,string addString)
        {
            string strhtml = "";
            int index = content.IndexOf(cut);
            if (index == -1)
            {
                return strhtml;
            }
            strhtml = content.Substring(index+cut.Length);
            strhtml = addString+strhtml;
            return strhtml;
        }
        #endregion


        #region infodiv  去>　和  class='info_td_r'>
        public static List<string> getTdContent(List<string> strListOld)
        {
            List<string> strList = new List<string>();
            string newStrList = "";
            foreach (string item in strListOld)
            {
                newStrList = item;
                newStrList = newStrList.Replace(">", "");
                newStrList = newStrList.Replace(" class=\'info_td_r\'", "");
                strList.Add(newStrList);
            }
            return strList;
        }
        #endregion
    }
}
