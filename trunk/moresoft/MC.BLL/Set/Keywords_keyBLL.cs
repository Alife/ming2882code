using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class Keywords_keyBLL
    {
        public static IList<Keywords_key> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<Keywords_key>(queryInfo);
        }
        public static PagedIList<Keywords_key> GetPageList(QueryInfo queryInfo)
        {
            return BLLService.GetIListPage<Keywords_key>(queryInfo);
        }
        public static Keywords_key GetItem(int ID_pag)
        {
            return BLLService.GetItem<Keywords_key>(ID_pag);
        }
        public static bool IsHasName(string Name_key)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("Name_key", Name_key);
            return BLLService.TotalCount<Keywords_key>(query) > 0;
        }
        public static int Insert(Keywords_key item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(Keywords_key item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Keywords_key).Name;
            return BLLService.Delete(query);
        }
    }
}
