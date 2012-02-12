using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface IRequire_req
    {
        IList<Require_req> GetList(QueryInfo queryInfo);
        PagedIList<Require_req> GetPageList(QueryInfo queryInfo);
        Require_req GetItem(object ID_req);
        int Insert(Require_req item);
        int Update(Require_req item);
        int Delete(List<string> ids);
    }
}
