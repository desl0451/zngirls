using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www_zngirls_com_g
{

    public class Girl
    {

        #region 构造函数
        ///<summary>
        ///无参构造函数
        ///<summary>
        public Girl() { }
        ///<summary>
        ///有参构造函数
        ///<summary>
        public Girl(int id, string name, string aliases, int age, string birthday, string constellation, int height, int weight, string bwh, string born, string occupation, string details, int portrayid, string photopath)
        {
            this.Id = id;
            this.Name = name;
            this.Aliases = aliases;
            this.Age = age;
            this.Birthday = birthday;
            this.Constellation = constellation;
            this.Height = height;
            this.Weight = weight;
            this.Bwh = bwh;
            this.Born = born;
            this.Occupation = occupation;
            this.Details = details;
            this.Portrayid = portrayid;
            this.Photopath = photopath;
        }
        #endregion

        #region 字段
        private int id;
        private string name;
        private string aliases;
        private int age;
        private string birthday;
        private string constellation;
        private int height;
        private int weight;
        private string bwh;
        private string born;
        private string occupation;
        private string details;
        private int portrayid;
        private string photopath;
        #endregion

        #region 属性
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Aliases
        {
            get { return aliases; }
            set { aliases = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        public string Constellation
        {
            get { return constellation; }
            set { constellation = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        public string Bwh
        {
            get { return bwh; }
            set { bwh = value; }
        }
        public string Born
        {
            get { return born; }
            set { born = value; }
        }
        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
        public string Details
        {
            get { return details; }
            set { details = value; }
        }
        public int Portrayid
        {
            get { return portrayid; }
            set { portrayid = value; }
        }
        public string Photopath
        {
            get { return photopath; }
            set { photopath = value; }
        }
        #endregion

    }
}