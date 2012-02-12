using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class Setting_setService : ISetting_set
    {
        private readonly IDao _dao;
        public Setting_setService(IDao dao) { _dao = dao; }
        public IList<Setting_set> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<Setting_set>(queryInfo);
        }
        public Setting_set GetItem(object ID_set)
        {
            return _dao.GetItem<Setting_set>(ID_set);
        }
        public int Insert(Setting_set item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(Setting_set item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(Setting_set).Name;
            return _dao.Delete(query);
        }
    }
}
