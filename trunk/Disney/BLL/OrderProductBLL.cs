namespace BLL
{
    using Common;
    using Models;
    using System;

    public class OrderProductBLL : BaseObject
    {
        public static OrderProduct GetItem(int pID)
        {
            string key = "OrderProduct-" + pID;
            OrderProduct data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (OrderProduct)BaseObject.Cache[key];
            }
            data = DataFactory.OrderProductData().GetItem(pID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int Insert(OrderProduct item)
        {
            int num = DataFactory.OrderProductData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("OrderProduct");
            }
            return num;
        }

        public static int Update(OrderProduct item)
        {
            int num = DataFactory.OrderProductData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("OrderProduct");
            }
            return num;
        }
   }
}
