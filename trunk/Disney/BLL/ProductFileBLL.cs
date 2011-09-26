namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ProductFileBLL : BaseObject
    {
        public static int Delete(int ID)
        {
            int num = DataFactory.ProductFileData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductFile");
            }
            return num;
        }

        public static ProductFile GetItem(int ID)
        {
            string key = "ProductFile-" + ID + "-1";
            ProductFile data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ProductFile)BaseObject.Cache[key];
            }
            data = DataFactory.ProductFileData().GetItem(ID, false);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static ProductFile GetItemTop(int ID)
        {
            string key = "ProductFile-" + ID + "-0";
            ProductFile data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ProductFile)BaseObject.Cache[key];
            }
            data = DataFactory.ProductFileData().GetItem(ID, true);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<ProductFile> GetList(int _productID)
        {
            string key = string.Format("ProductFile-{0}", _productID);
            List<ProductFile> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ProductFile>)BaseObject.Cache[key];
            }
            data = DataFactory.ProductFileData().GetList(_productID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(ProductFile item)
        {
            int num = DataFactory.ProductFileData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductFile");
            }
            return num;
        }

        public static int Update(ProductFile item)
        {
            int num = DataFactory.ProductFileData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ProductFile");
            }
            return num;
        }
    }
}
