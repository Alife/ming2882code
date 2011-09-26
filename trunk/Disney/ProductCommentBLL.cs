namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ProductCommentBLL : BaseObject
    {
        public static int Delete(int ID)
        {
            int num = DataFactory.ProductCommentData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductComment");
            }
            return num;
        }

        public ProductComment GetItem(int ID)
        {
            string key = "ProductComment-" + ID;
            ProductComment data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ProductComment)BaseObject.Cache[key];
            }
            data = DataFactory.ProductCommentData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public List<ProductComment> GetList(int ID, int _productID)
        {
            string key = string.Format("ProductComment-{0}-{1}", ID, _productID);
            List<ProductComment> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ProductComment>)BaseObject.Cache[key];
            }
            data = DataFactory.ProductCommentData().GetList(ID, _productID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static ProductCommentList GetList(int _productID, int _pageIndex, int _pageSize)
        {
            string key = string.Format("ProductComment-{0}-{1}-{2}", _productID, _pageIndex, _pageSize);
            ProductCommentList data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ProductCommentList) BaseObject.Cache[key];
            }
            data = DataFactory.ProductCommentData().GetList(_productID, _pageIndex, _pageSize);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(ProductComment item)
        {
            int num = DataFactory.ProductCommentData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductComment");
            }
            return num;
        }

        public static int Update(ProductComment item)
        {
            int num = DataFactory.ProductCommentData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductComment");
            }
            return num;
        }
    }
}
