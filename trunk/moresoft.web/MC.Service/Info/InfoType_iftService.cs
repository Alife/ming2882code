using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class InfoType_iftService : IInfoType_ift
    {
        private readonly IDao _dao;
        public InfoType_iftService(IDao dao) { _dao = dao; }
        public IList<InfoType_ift> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<InfoType_ift>(queryInfo);
        }
        public InfoType_ift GetItem(object ID_ift)
        {
            return _dao.GetItem<InfoType_ift>(ID_ift);
        }
        public int Insert(InfoType_ift item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(InfoType_ift item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(int id)
        {
            QueryInfo query = new QueryInfo();
            query.MapQueryValue = id;
            query.MappingName = typeof(InfoType_ift).Name;
            return _dao.Delete(query);
        }
    }
}
