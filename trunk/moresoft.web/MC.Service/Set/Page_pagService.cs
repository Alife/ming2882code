using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class Page_pagService : IPage_pag
    {
        private readonly IDao _dao;
        public Page_pagService(IDao dao) { _dao = dao; }
        public IList<Page_pag> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<Page_pag>(queryInfo);
        }
        public PagedIList<Page_pag> GetPageList(QueryInfo queryInfo)
        {
            return _dao.GetIListPage<Page_pag>(queryInfo);
        }
        public Page_pag GetItem(int ID_pag)
        {
            return _dao.GetItem<Page_pag>(ID_pag);
        }
        public Page_pag GetItem(string Code_pag)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("Code_pag", Code_pag);
            return _dao.GetItem<Page_pag>(query);
        }
        public int Insert(Page_pag item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(Page_pag item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Page_pag).Name;
            return _dao.Delete(query);
        }
    }
}
