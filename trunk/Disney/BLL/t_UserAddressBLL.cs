using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class t_UserAddressBLL : BaseObject
    {
        public static int Insert(t_UserAddress item)
        {
            int num = DataFactory.t_UserAddressData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("t_UserAddress");
            return num;
        }
        public static int Insert(List<t_UserAddress> model)
        {
            int num = DataFactory.t_UserAddressData().Insert(model);
            if (num > 0)
                BaseObject.CacheRemove("t_UserAddress");
            return num;
        }

        public static int Update(t_UserAddress item)
        {
            int num = DataFactory.t_UserAddressData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("t_UserAddress");
            return num;
        }
        public static List<t_UserAddress> GetList(int uid)
        {
            string key = string.Format("t_UserAddress-all-{0}", uid);
            List<t_UserAddress> data = null;
            if (BaseObject.Cache[key] != null)
                data = (List<t_UserAddress>)BaseObject.Cache[key];
            else
            {
                data = DataFactory.t_UserAddressData().GetList(uid);
                BaseObject.CacheData(key, data);
            }
            return data;
        }

        public static int Delete(List<string> ID)
        {
            int num = DataFactory.t_UserAddressData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("t_UserAddress");
            return num;
        }

        public static t_UserAddress GetItem(int id)
        {
            string key = "t_UserAddress-" + id;
            t_UserAddress data = null;
            if (Cache[key] != null)
                data = (t_UserAddress)Cache[key];
            else
            {
                data = DataFactory.t_UserAddressData().GetItem(id);
                CacheData(key, data);
            }
            return data;
        }
        public static t_UserAddress GetItemHas(int uid)
        {
            string key = "t_UserAddress-u-" + uid;
            t_UserAddress data = null;
            if (Cache[key] != null)
                data = (t_UserAddress)Cache[key];
            else
            {
                data = DataFactory.t_UserAddressData().GetItemHas(uid);
                CacheData(key, data);
            }
            return data;
        }
        public static t_UserAddressList GetModelList(int uid)
        {
            string key = "t_UserAddress-list-" + uid;
            t_UserAddressList data = null;
            if (Cache[key] != null)
                data = (t_UserAddressList)Cache[key];
            else
            {
                data = DataFactory.t_UserAddressData().GetModelList(uid);
                CacheData(key, data);
            }
            return data;
        }
    }
}
