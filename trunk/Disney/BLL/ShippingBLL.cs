namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ShippingBLL : BaseObject
    {
        public static int Delete(List<string> ids)
        {
            int num = DataFactory.ShippingData().Delete(ids);
            if (num > 0)
            {
                BaseObject.CacheRemove("Shipping");
            }
            return num;
        }

        public static Shipping GetItem(int ID)
        {
            string key = "Shipping-" + ID;
            Shipping data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (Shipping) BaseObject.Cache[key];
            }
            data = DataFactory.ShippingData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<Shipping> GetList()
        {
            string key = "Shipping-";
            List<Shipping> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<Shipping>) BaseObject.Cache[key];
            }
            data = DataFactory.ShippingData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(Shipping item)
        {
            int num = DataFactory.ShippingData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Shipping");
            }
            return num;
        }

        public static int Update(Shipping item)
        {
            int num = DataFactory.ShippingData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Shipping");
            }
            return num;
        }
    }
}
