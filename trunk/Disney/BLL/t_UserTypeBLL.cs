namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class t_UserTypeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.t_UserTypeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("t_UserType");
            return num;
        }

        public static t_UserType GetItem(int ID)
        {
            string key = "t_UserType-" + ID;
            if (BaseObject.Cache[key] != null)
                return (t_UserType) BaseObject.Cache[key];
            t_UserType data = DataFactory.t_UserTypeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<t_UserType> GetList(string type)
        {
            string key = "t_UserType-all-" + type;
            if (BaseObject.Cache[key] != null)
                return (List<t_UserType>)BaseObject.Cache[key];
            List<t_UserType> data = DataFactory.t_UserTypeData().GetList(type);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(t_UserType item)
        {
            int num = DataFactory.t_UserTypeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("t_UserType");
            return num;
        }

        public static int Update(t_UserType item)
        {
            int num = DataFactory.t_UserTypeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("t_UserType");
            return num;
        }
    }
}
