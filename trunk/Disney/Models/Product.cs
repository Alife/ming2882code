using System;
using System.Collections;

namespace Models
{
    public class Product
    {
        private int id;
        private string name;
        private string code;
        private int categoryid;
        private decimal price;
        private decimal pricemarket;
        private DateTime createtime;
        private int protype;
        private int _elite;
        private int _top;
        private bool issell = false;
        private int hits;
        private string tags;
        private string content;
        public Product() { }
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public int CategoryID
        {
            get { return categoryid; }
            set { categoryid = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public decimal PriceMarket
        {
            get { return pricemarket; }
            set { pricemarket = value; }
        }
        public DateTime CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }
        public int ProType
        {
            get { return protype; }
            set { protype = value; }
        }
        /// <summary>
        /// 精华
        /// </summary>
        public int Elite
        {
            set { _elite = value; }
            get { return _elite; }
        }

        /// <summary>
        /// 置顶
        /// </summary>
        public int Top
        {
            set { _top = value; }
            get { return _top; }
        }
        public bool IsSell
        {
            get { return issell; }
            set { issell = value; }
        }
        public int Hits
        {
            get { return hits; }
            set { hits = value; }
        }
        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
    public class ProductList : CollectionBase
    {
        private int _recordCount;

        public int Add(Product value)
        {
            return base.List.Add(value);
        }

        public Product this[int index]
        {
            get { return (Product)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
}
