using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class Info_infBLL
    {
        public static IList<Info_inf> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<Info_inf>(queryInfo);
        }
        public static PagedIList<Info_inf> GetPageList(QueryInfo queryInfo)
        {
            return BLLService.GetIListPage<Info_inf>(queryInfo);
        }
        public static Info_inf GetItem(object ID_inf)
        {
            return BLLService.GetItem<Info_inf>(ID_inf);
        }
        public static int Insert(Info_inf item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(Info_inf item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Info_inf).Name;
            return BLLService.Delete(query);
        }
    }
}
