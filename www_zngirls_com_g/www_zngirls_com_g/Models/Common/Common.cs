using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace www_zngirls_com_g
{
    public class Common
    {
        #region 不合格文件过滤
        public static bool checkFile(string file)
        {
            bool bl = false;
            if (file.Equals(".html"))
            {
                bl = true;
            }
            else if (file.Equals(".htm"))
            {
                bl = true;
            }

            return bl;
        }
        #endregion

        /// <summary>
        /// 检测index.html和index.htm是否存在
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool checkIndex(string file)
        {
            bool bl = false;
            if (File.Exists(file))
            {
                bl = true;
            }
            else
            {
                bl = false;
            }
            return bl;
        }

    }
}
