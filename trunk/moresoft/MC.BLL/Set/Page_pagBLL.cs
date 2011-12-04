using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class Page_pagBLL
    {
        public static IList<Page_pag> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<Page_pag>(queryInfo);
        }
        public static Page_pag GetItem(int ID_pag)
        {
            return BLLService.GetItem<Page_pag>(ID_pag);
        }
        public static Page_pag GetItem(string Code_pag)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("Code_pag", Code_pag);
            return BLLService.GetItem<Page_pag>(query);
        }
        public static int Insert(Page_pag item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(Page_pag item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Page_pag).Name;
            return BLLService.Delete(query);
        }
    }
}
