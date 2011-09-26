namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ArticleTopBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.ArticleTopData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleTop");
            }
            return num;
        }

        public static ArticleTop GetItem(int ID)
        {
            string key = "ArticleTop-" + ID;
            ArticleTop data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleTop)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleTopData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static ArticleTop GetItemByArticle(int articleID)
        {
            return DataFactory.ArticleTopData().GetItemByArticle(articleID);
        }
        public static List<ArticleTop> GetList(int num)
        {
            string key = string.Format("ArticleTop-new-{0}", num);
            List<ArticleTop> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ArticleTop>)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleTopData().GetList(num);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(ArticleTop item)
        {
            int num = DataFactory.ArticleTopData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleTop");
            }
            return num;
        }

        public static int Update(ArticleTop item)
        {
            int num = DataFactory.ArticleTopData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleTop");
            }
            return num;
        }
    }
}