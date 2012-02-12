using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface ILink_lnk
    {
        IList<Link_lnk> GetList(QueryInfo queryInfo);
        PagedIList<Link_lnk> GetPageList(QueryInfo queryInfo);
        Link_lnk GetItem(object ID_lnk);
        int Insert(Link_lnk item);
        int Update(Link_lnk item);
        int Delete(List<string> ids);
    }
}
