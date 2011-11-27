using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class IndexTag_itgBLL
    {
        public static IList<IndexTag_itg> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<IndexTag_itg>(queryInfo);
        }
        public static PagedIList<IndexTag_itg> GetPageList(QueryInfo queryInfo)
        {
            return BLLService.GetIListPage<IndexTag_itg>(queryInfo);
        }
        public static IndexTag_itg GetItem(object ID_itg)
        {
            return BLLService.GetItem<IndexTag_itg>(ID_itg);
        }
        public static int Insert(IndexTag_itg item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(IndexTag_itg item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(IndexTag_itg).Name;
            return BLLService.Delete(query);
        }
    }
}
