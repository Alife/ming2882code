using System;
using System.Collections;

namespace Models
{
    public class OrderProduct
    {
        private int id;
        private int productid;
        private string productname;
        private string productcode;
        private string filepath;
        private string content;
        public OrderProduct() { }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int ProductID
        {
            get { return productid; }
            set { productid = value; }
        }
        public string ProductName
        {
            get { return productname; }
            set { productname = value; }
        }
        public string ProductCode
        {
            get { return productcode; }
            set { productcode = value; }
        }
        public string FilePath
        {
            get { return filepath; }
            set { filepath = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
