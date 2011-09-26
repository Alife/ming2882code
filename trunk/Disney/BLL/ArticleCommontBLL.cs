namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ArticleCommentBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.ArticleCommentData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleComment");
            }
            return num;
        }

        public static ArticleComment GetItem(int ID)
        {
            string key = "ArticleComment-" + ID;
            ArticleComment data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleComment)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCommentData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static ArticleCommentList GetList(int articleID, int pageIndex, int pageSize)
        {
            return GetList(articleID, null, null, pageIndex, pageSize);
        }
        public static ArticleCommentList GetList(int articleID,  DateTime? beginTime, DateTime? endTime, int pageIndex, int pageSize)
        {
            string key = string.Format("ArticleComment-{0}-{1}-{2}-{3}-{4}", articleID, beginTime, endTime, pageIndex, pageSize);
            ArticleCommentList data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (ArticleCommentList)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCommentData().GetList(articleID, beginTime, endTime, pageIndex, pageSize);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<ArticleComment> GetList(int parentID)
        {
            string key = string.Format("ArticleComment-children-{0}", parentID);
            List<ArticleComment> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<ArticleComment>)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleCommentData().GetList(parentID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(ArticleComment item)
        {
            int num = DataFactory.ArticleCommentData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleComment");
            }
            return num;
        }

        public static int Update(ArticleComment item)
        {
            int num = DataFactory.ArticleCommentData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleComment");
            }
            return num;
        }
    }
}