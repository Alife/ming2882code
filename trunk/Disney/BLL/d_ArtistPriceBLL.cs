namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_ArtistPriceBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_ArtistPriceData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistPrice");
            return num;
        }

        public static d_ArtistPrice GetItem(int ID)
        {
            string key = "d_ArtistPrice-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_ArtistPrice) BaseObject.Cache[key];
            d_ArtistPrice data = DataFactory.d_ArtistPriceData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static d_ArtistPrice GetItem(int uid, int kitPhotoTypeID)
        {
            string key = string.Format("d_ArtistPrice-{0}-{1}", uid, kitPhotoTypeID);
            if (BaseObject.Cache[key] != null)
                return (d_ArtistPrice)BaseObject.Cache[key];
            d_ArtistPrice data = DataFactory.d_ArtistPriceData().GetItem(uid, kitPhotoTypeID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList()
        {
            string key = "d_ArtistPrice-all";
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.d_ArtistPriceData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int Insert(d_ArtistPrice item)
        {
            int num = DataFactory.d_ArtistPriceData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistPrice");
            return num;
        }

        public static int Update(d_ArtistPrice item)
        {
            int num = DataFactory.d_ArtistPriceData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistPrice");
            return num;
        }
    }
}
