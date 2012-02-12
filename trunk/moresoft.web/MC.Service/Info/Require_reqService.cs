using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class Require_reqService : IRequire_req
    {
        private readonly IDao _dao;
        public Require_reqService(IDao dao) { _dao = dao; }
        public IList<Require_req> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<Require_req>(queryInfo);
        }
        public PagedIList<Require_req> GetPageList(QueryInfo queryInfo)
        {
            return _dao.GetIListPage<Require_req>(queryInfo);
        }
        public Require_req GetItem(object ID_req)
        {
            return _dao.GetItem<Require_req>(ID_req);
        }
        public int Insert(Require_req item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(Require_req item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Require_req).Name;
            return _dao.Delete(query);
        }
    }
}
