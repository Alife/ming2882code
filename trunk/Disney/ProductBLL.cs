namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ProductBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.ProductData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("Product");
            }
            return num;
        }

        public static int Exists(string code)
        {
            return DataFactory.ProductData().Exists(code);
        }

        public static Product GetItem(int ID)
        {
            string key = string.Format("Product-{0}-1", ID);
            Product data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (Product) BaseObject.Cache[key];
            }
            data = DataFactory.ProductData().GetItem(ID.ToString(), 1);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static Product GetItem(string code)
        {
            string key = string.Format("Product-{0}-2", code);
            Product data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (Product)BaseObject.Cache[key];
            }
            data = DataFactory.ProductData().GetItem(code, 2);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<Product> GetList(string categorycode, int _elite, int _top, int num)
        {
            string key = string.Format("Product-{0}-{1}-{2}-{3}", new object[] { categorycode, _elite, _top, num });
            List<Product> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<Product>)BaseObject.Cache[key];
            }
            data = DataFactory.ProductData().GetList(categorycode, _elite, _top, num);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static ProductList GetList(int _categoryID, string _unQuery, string _title, int _elite, int _top, int _pageIndex, int _pageSize)
        {
            string key = string.Format("Product-{0}-{1}-{2}-{3}-{4}-{5}-{6}", new object[] { _categoryID, _unQuery, _title, _elite, _top, _pageIndex, _pageSize });
            ProductList data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ProductList) BaseObject.Cache[key];
            }
            data = DataFactory.ProductData().GetList(_categoryID, _unQuery, _title, _elite, _top, _pageIndex, _pageSize);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int Insert(Product item)
        {
            int num = DataFactory.ProductData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Product");
            }
            return num;
        }

        public static int MoveCategory(string ID, int cid)
        {
            int num = DataFactory.ProductData().MoveCategory(ID, cid);
            if (num > 0)
            {
                BaseObject.CacheRemove("Product");
            }
            return num;
        }

        public static int Update(Product item)
        {
            int num = DataFactory.ProductData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Product");
            }
            return num;
        }
    }
}
