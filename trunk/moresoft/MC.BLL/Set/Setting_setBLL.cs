using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class Setting_setBLL
    {
        public static IList<Setting_set> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<Setting_set>(queryInfo);
        }
        public static Setting_set GetItem(object ID_set)
        {
            return BLLService.GetItem<Setting_set>(ID_set);
        }
        public static int Insert(Setting_set item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(Setting_set item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Setting_set).Name;
            return BLLService.Delete(query);
        }
    }
}
