using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www_zngirls_com_g
{
    //<a target='_blank' href="../../girl/16293/index.htm" tppabs="http://www.zngirls.com/girl/16293/">XXX</a>
    //保存tppabs和href
    // //replaceStr要替换的内容
    // 生成一个li标签
    public class FileUrl
    {
        private string href;
        private string tppabs;

        private string replaceStr;

        private string hrefli;
        public string Href
        {
            get
            {
                return href;
            }

            set
            {
                href = value;
            }
        }

        public string Tppabs
        {
            get
            {
                return tppabs;
            }

            set
            {
                tppabs = value;
            }
        }

        public string ReplaceStr
        {
            get
            {
                return replaceStr;
            }

            set
            {
                replaceStr = value;
            }
        }

        public string Hrefli
        {
            get
            {
                return hrefli;
            }

            set
            {
                hrefli = value;
            }
        }
    }
}
