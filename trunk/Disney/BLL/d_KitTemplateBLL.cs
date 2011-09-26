namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_KitTemplateBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitTemplateData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitTemplate");
            return num;
        }

        public static d_KitTemplate GetItem(int ID)
        {
            string key = "d_KitTemplate-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitTemplate) BaseObject.Cache[key];
            d_KitTemplate data = DataFactory.d_KitTemplateData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_KitTemplate> GetList()
        {
            string key = "d_KitTemplate-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_KitTemplate>)BaseObject.Cache[key];
            List<d_KitTemplate> data = DataFactory.d_KitTemplateData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_KitTemplate item)
        {
            int num = DataFactory.d_KitTemplateData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitTemplate");
            return num;
        }

        public static int Update(d_KitTemplate item)
        {
            int num = DataFactory.d_KitTemplateData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitTemplate");
            return num;
        }
    }
}
