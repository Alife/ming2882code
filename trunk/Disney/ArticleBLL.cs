namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ArticleBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.ArticleData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("Article");
            }
            return num;
        }

        public static Article GetItem(int ID)
        {
            string key = "Article-" + ID;
            Article data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (Article)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<Article> GetList(string categorycode, int elite, int top, int num)
        {
            string key = string.Format("Article-new-{0}-{1}-{2}-{3}", categorycode, elite, top, num);
            List<Article> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<Article>)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleData().GetList(categorycode, elite, top, num);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static ArticleList GetList(int _categoryID, string _unQuery, string _title, int _elite, int _top, int _pageIndex, int _pageSize)
        {
            string key = string.Format("Article-{0}-{1}-{2}-{3}-{4}-{5}", _categoryID, _title, _elite, _top, _pageIndex, _pageSize);
            ArticleList data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleList)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleData().GetList(_categoryID, _unQuery, _title, _elite, _top, _pageIndex, _pageSize);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(Article item)
        {
            int num = DataFactory.ArticleData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Article");
            }
            return num;
        }

        public static int Update(Article item)
        {
            int num = DataFactory.ArticleData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Article");
            }
            return num;
        }
    }
}