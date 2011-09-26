using System;
using System.Collections;

namespace Models
{
    public class ProductCategory
    {
        private int id;
        private string code;
        private string category;
        private int parentid;
        private int path;
        private int orderID;
        private string description;
        private string metakeywords;
        private string metadescription;
        private string _readCategory;
        public ProductCategory()
        { }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public int ParentID
        {
            get { return parentid; }
            set { parentid = value; }
        }
        public int Path
        {
            get { return path; }
            set { path = value; }
        }
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string MetaKeywords
        {
            get { return metakeywords; }
            set { metakeywords = value; }
        }
        public string MetaDescription
        {
            get { return metadescription; }
            set { metadescription = value; }
        }
        /// <summary>
        /// 读取分类
        /// </summary>
        public string ReadCategory
        {
            set { _readCategory = value; }
            get { return _readCategory; }
        }

    }
    public class ProductCategoryPhotoList : CollectionBase
    {
        public int Add(ProductCategory value)
        {
            return base.List.Add(value);
        }

        public ProductCategory this[int index]
        {
            get { return (ProductCategory)base.List[index]; }
            set { base.List[index] = value; }
        }
    }
}
