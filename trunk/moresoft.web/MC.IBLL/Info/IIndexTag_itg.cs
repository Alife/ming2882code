using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface IIndexTag_itg
    {
        IList<IndexTag_itg> GetList(QueryInfo queryInfo);
        PagedIList<IndexTag_itg> GetPageList(QueryInfo queryInfo);
        IndexTag_itg GetItem(object ID_itg);
        int Insert(IndexTag_itg item);
        int Update(IndexTag_itg item);
        int Delete(List<string> ids);
    }
}
