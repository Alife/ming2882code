using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class IndexTag_itgService : IIndexTag_itg
    {
        private readonly IDao _dao;
        public IndexTag_itgService(IDao dao) { _dao = dao; }
        public IList<IndexTag_itg> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<IndexTag_itg>(queryInfo);
        }
        public PagedIList<IndexTag_itg> GetPageList(QueryInfo queryInfo)
        {
            return _dao.GetIListPage<IndexTag_itg>(queryInfo);
        }
        public IndexTag_itg GetItem(object ID_itg)
        {
            return _dao.GetItem<IndexTag_itg>(ID_itg);
        }
        public int Insert(IndexTag_itg item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(IndexTag_itg item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(IndexTag_itg).Name;
            return _dao.Delete(query);
        }
    }
}
