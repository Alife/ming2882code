namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class MessageBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.MessageData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("Message");
            }
            return num;
        }

        public static MessageList GetList(int _pageIndex, int _pageSize)
        {
            string key = string.Format("Message-{0}-{1}", _pageIndex, _pageSize);
            MessageList data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (MessageList) BaseObject.Cache[key];
            }
            data = DataFactory.MessageData().GetList(_pageIndex, _pageSize);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(Message item)
        {
            int num = DataFactory.MessageData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Message");
            }
            return num;
        }

        public static int Update(Message item)
        {
            int num = DataFactory.MessageData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Message");
            }
            return num;
        }
    }
}
