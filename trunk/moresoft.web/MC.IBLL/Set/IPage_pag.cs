using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface IPage_pag
    {
        IList<Page_pag> GetList(QueryInfo queryInfo);
        Page_pag GetItem(int ID_pag);
        Page_pag GetItem(string Code_pag);
        int Insert(Page_pag item);
        int Update(Page_pag item);
        int Delete(List<string> ids);
    }
}
