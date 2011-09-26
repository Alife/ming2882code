using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class t_UserInfoBLL : BaseObject
    {
        public static int Insert(t_UserInfo info)
        {
            int num = DataFactory.t_UserInfoData().Insert(info);
            if (num > 0)
                BaseObject.CacheRemove("userinfo");
            return num;
        }
        public static int Update(t_UserInfo info)
        {
            int num = DataFactory.t_UserInfoData().Update(info);
            if (num > 0)
                BaseObject.CacheRemove("userinfo");
            return num;
        }
        public static t_UserInfo GetItem(int userid)
        {
            string key = "userinfo-" + userid;
            t_UserInfo data = null;
            if (Cache[key] != null)
                data = (t_UserInfo)Cache[key];
            else
            {
                data = DataFactory.t_UserInfoData().GetItem(userid);
                CacheData(key, data);
            }
            return data;
        }
    }
}
