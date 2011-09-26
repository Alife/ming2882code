using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class sys_Link
    {
        private int id;
        private string linkname;
        private string picurl;
        private string url;
        private int orderid;
        public sys_Link() { }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string LinkName
        {
            get { return linkname; }
            set { linkname = value; }
        }
        public string PicUrl
        {
            get { return picurl; }
            set { picurl = value; }
        }
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public int OrderID
        {
            get { return orderid; }
            set { orderid = value; }
        }
    }
}
