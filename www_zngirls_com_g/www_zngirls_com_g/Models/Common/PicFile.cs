using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www_zngirls_com_g
{
    public class PicFile
    {
        private string filename;
        private string filePath;   
        private string allfilename;
        private string kuozhanname;//扩展名
        private string fileDirectory;
        /// <summary>
        /// 文件名 不包含扩展名
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
        /// 全部名称
        /// </summary>
        public string Allfilename
        {
            get
            {
                return allfilename;
            }

            set
            {
                allfilename = value;
            }
        }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string Kuozhanname
        {
            get
            {
                return kuozhanname;
            }

            set
            {
                kuozhanname = value;
            }
        }
        /// <summary>
        /// 文件所在目录
        /// </summary>
        public string FileDirectory
        {
            get
            {
                return fileDirectory;
            }

            set
            {
                fileDirectory = value;
            }
        }
        /// <summary>
        /// 文件路径 
        /// </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }
        }
    }
}
