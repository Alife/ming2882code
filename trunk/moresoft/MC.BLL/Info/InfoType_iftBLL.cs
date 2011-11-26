using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class InfoType_iftBLL
    {
        public static IList<InfoType_ift> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<InfoType_ift>(queryInfo);
        }
        public static InfoType_ift GetItem(object ID_ift)
        {
            return BLLService.GetItem<InfoType_ift>(ID_ift);
        }
        public static int Insert(InfoType_ift item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(InfoType_ift item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(int id)
        {
            QueryInfo query = new QueryInfo();
            query.MapQueryValue = id;
            query.MappingName = typeof(InfoType_ift).Name;
            return BLLService.Delete(query);
        }
    }
}
