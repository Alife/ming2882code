using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class Link_lnkService : ILink_lnk
    {
        private readonly IDao _dao;
        public Link_lnkService(IDao dao) { _dao = dao; }
        public IList<Link_lnk> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<Link_lnk>(queryInfo);
        }
        public PagedIList<Link_lnk> GetPageList(QueryInfo queryInfo)
        {
            return _dao.GetIListPage<Link_lnk>(queryInfo);
        }
        public Link_lnk GetItem(object ID_lnk)
        {
            return _dao.GetItem<Link_lnk>(ID_lnk);
        }
        public int Insert(Link_lnk item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(Link_lnk item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Link_lnk).Name;
            return _dao.Delete(query);
        }
    }
}
