using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class Link_lnkBLL
    {
        public static IList<Link_lnk> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<Link_lnk>(queryInfo);
        }
        public static PagedIList<Link_lnk> GetPageList(QueryInfo queryInfo)
        {
            return BLLService.GetIListPage<Link_lnk>(queryInfo);
        }
        public static Link_lnk GetItem(object ID_lnk)
        {
            return BLLService.GetItem<Link_lnk>(ID_lnk);
        }
        public static int Insert(Link_lnk item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(Link_lnk item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Link_lnk).Name;
            return BLLService.Delete(query);
        }
    }
}
