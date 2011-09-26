using System;
using System.Collections;

namespace Models
{
    public class ProductComment
    {
        private int id;
        private int userid;
        private int productid;
        private int parentid;
        private string content;
        private DateTime createtime;
        public ProductComment()
        { }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        public int ProductID
        {
            get { return productid; }
            set { productid = value; }
        }
        public int ParentID
        {
            get { return parentid; }
            set { parentid = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        public DateTime CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }
    }
    public class ProductCommentList : CollectionBase
    {
        private int _recordCount;

        public int Add(ProductComment value)
        {
            return base.List.Add(value);
        }

        public ProductComment this[int index]
        {
            get { return (ProductComment)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
}
