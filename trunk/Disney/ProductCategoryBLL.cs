namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ProductCategoryBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.ProductCategoryData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductCategory");
            }
            return num;
        }
        public static int Exists(string _Code)
        {
            return DataFactory.ProductCategoryData().Exists(_Code);
        }

        public static ProductCategory GetItem(int ID)
        {
            string key = string.Format("ProductCategory-{0}-{1}", ID, 0);
            ProductCategory data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ProductCategory) BaseObject.Cache[key];
            }
            data = DataFactory.ProductCategoryData().GetItem(ID.ToString(), 0);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static ProductCategory GetItem(string ID)
        {
            string key = string.Format("ProductCategory-{0}-{1}", ID, 2);
            ProductCategory data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ProductCategory) BaseObject.Cache[key];
            }
            data = DataFactory.ProductCategoryData().GetItem(ID, 2);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<ProductCategory> GetList()
        {
            string key = "ProductCategory-all";
            List<ProductCategory> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ProductCategory>) BaseObject.Cache[key];
            }
            data = DataFactory.ProductCategoryData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<ProductCategory> GetListByChild(int parent)
        {
            string key = "ProductCategory-Child-" + parent;
            List<ProductCategory> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ProductCategory>) BaseObject.Cache[key];
            }
            data = DataFactory.ProductCategoryData().GetListByChild(parent);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<ProductCategory> GetList(int id)
        {
            string key = "ProductCategory-ByP-" + id;
            List<ProductCategory> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ProductCategory>) BaseObject.Cache[key];
            }
            data = DataFactory.ProductCategoryData().GetList(id);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(ProductCategory item)
        {
            int num = DataFactory.ProductCategoryData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductCategory");
            }
            return num;
        }

        public static int Update(ProductCategory item)
        {
            int num = DataFactory.ProductCategoryData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductCategory");
            }
            return num;
        }

        public static int Update(List<ProductCategory> list)
        {
            int num = DataFactory.ProductCategoryData().Update(list);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductCategory");
            }
            return num;
        }
    }
}
