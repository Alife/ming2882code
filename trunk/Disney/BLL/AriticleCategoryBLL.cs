namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ArticleCategoryBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.ArticleCategoryData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleCategory");
            }
            return num;
        }
        public static int Exists(string _Code)
        {
            return DataFactory.ArticleCategoryData().Exists(_Code);
        }

        public static ArticleCategory GetItem(int ID)
        {
            string key = string.Format("ArticleCategory-{0}-{1}", ID, 0);
            ArticleCategory data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleCategory) BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCategoryData().GetItem(ID.ToString(), 0);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static ArticleCategory GetItem(string ID)
        {
            string key = string.Format("ArticleCategory-{0}-{1}", ID, 2);
            ArticleCategory data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleCategory) BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCategoryData().GetItem(ID, 2);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<ArticleCategory> GetList()
        {
            return GetList(string.Empty);
        }
        public static List<ArticleCategory> GetList(string _unQuery)
        {
            string key = "ArticleCategory-all-" + _unQuery;
            List<ArticleCategory> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ArticleCategory>)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCategoryData().GetList(_unQuery);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<ArticleCategory> GetList(int ID)
        {
            string key = "ArticleCategory-" + ID;
            List<ArticleCategory> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ArticleCategory>) BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCategoryData().GetList(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<ArticleCategory> GetListByChild(int parent)
        {
            string key = "ArticleCategory-Child-" + parent;
            List<ArticleCategory> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ArticleCategory>) BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCategoryData().GetListByChild(parent);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(ArticleCategory item)
        {
            int num = DataFactory.ArticleCategoryData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleCategory");
            }
            return num;
        }

        public static int Update(ArticleCategory item)
        {
            int num = DataFactory.ArticleCategoryData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleCategory");
            }
            return num;
        }

        public static int Update(List<ArticleCategory> list)
        {
            int num = DataFactory.ArticleCategoryData().Update(list);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleCategory");
            }
            return num;
        }
    }
}
