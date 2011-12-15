using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class Require_reqBLL
    {
        public static IList<Require_req> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<Require_req>(queryInfo);
        }
        public static PagedIList<Require_req> GetPageList(QueryInfo queryInfo)
        {
            return BLLService.GetIListPage<Require_req>(queryInfo);
        }
        public static Require_req GetItem(object ID_req)
        {
            return BLLService.GetItem<Require_req>(ID_req);
        }
        public static int Insert(Require_req item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(Require_req item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Require_req).Name;
            return BLLService.Delete(query);
        }
    }
}
