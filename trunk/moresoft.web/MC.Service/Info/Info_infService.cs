using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class Info_infService : IInfo_inf
    {
        private readonly IDao _dao;
        public Info_infService(IDao dao) { _dao = dao; }
        public IList<Info_inf> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<Info_inf>(queryInfo);
        }
        public PagedIList<Info_inf> GetPageList(QueryInfo queryInfo)
        {
            return _dao.GetIListPage<Info_inf>(queryInfo);
        }
        public Info_inf GetItem(object ID_inf)
        {
            return _dao.GetItem<Info_inf>(ID_inf);
        }
        public int Insert(Info_inf item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(Info_inf item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Info_inf).Name;
            return _dao.Delete(query);
        }
    }
}
