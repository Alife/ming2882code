namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ArticleDotBLL : BaseObject
    {
        public static int Save(ArticleDot item)
        {
            int num = DataFactory.ArticleDotData().Save(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("ArticleComment");
            }
            return num;
        }
        public static int GetCount(int articleID, int dot)
        {
            string key = string.Format("ArticleDot-{0}-{1}", articleID, dot);
            int data = 0;
            if (BaseObject.Cache[key] != null)
            {
                return (int)BaseObject.Cache[key];
            }
            data = DataFactory.ArticleDotData().GetCount(articleID, dot);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}
