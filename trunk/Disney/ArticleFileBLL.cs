namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ArticleFileBLL : BaseObject
    {
        public static int Delete(int ID)
        {
            int num = DataFactory.ArticleFileData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleFile");
            }
            return num;
        }

        public static ArticleFile GetItem(int ID)
        {
            string key = "ArticleFile-" + ID + "-1";
            ArticleFile data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleFile)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleFileData().GetItem(ID, false);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static ArticleFile GetItemTop(int ID)
        {
            string key = "ArticleFile-" + ID + "-0";
            ArticleFile data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleFile)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleFileData().GetItem(ID, true);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<ArticleFile> GetList(int _productID)
        {
            string key = string.Format("ArticleFile-{0}", _productID);
            List<ArticleFile> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ArticleFile>)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleFileData().GetList(_productID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(ArticleFile item)
        {
            int num = DataFactory.ArticleFileData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleFile");
            }
            return num;
        }

        public static int Update(ArticleFile item)
        {
            int num = DataFactory.ArticleFileData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleFile");
            }
            return num;
        }
    }
}