using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface IInfo_inf
    {
        IList<Info_inf> GetList(QueryInfo queryInfo);
        PagedIList<Info_inf> GetPageList(QueryInfo queryInfo);
        Info_inf GetItem(object ID_inf);
        int Insert(Info_inf item);
        int Update(Info_inf item);
        int Delete(List<string> ids);
    }
}
