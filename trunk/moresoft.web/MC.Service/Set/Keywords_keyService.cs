using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class Keywords_keyService : IKeywords_key
    {
        private readonly IDao _dao;
        public Keywords_keyService(IDao dao) { _dao = dao; }
        public IList<Keywords_key> GetList(QueryInfo queryInfo)
        {
            queryInfo.Orderby.Add("Sort_key", null);
            return _dao.GetList<Keywords_key>(queryInfo);
        }
        public PagedIList<Keywords_key> GetPageList(QueryInfo queryInfo)
        {
            return _dao.GetIListPage<Keywords_key>(queryInfo);
        }
        public Keywords_key GetItem(int ID_pag)
        {
            return _dao.GetItem<Keywords_key>(ID_pag);
        }
        public bool IsHasName(string Name_key)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("Name_key", Name_key);
            return _dao.TotalCount<Keywords_key>(query) > 0;
        }
        public int Insert(Keywords_key item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(Keywords_key item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Keywords_key).Name;
            return _dao.Delete(query);
        }
    }
}
