using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface IKeywords_key
    {
        IList<Keywords_key> GetList(QueryInfo queryInfo);
        PagedIList<Keywords_key> GetPageList(QueryInfo queryInfo);
        Keywords_key GetItem(int ID_pag);
        bool IsHasName(string Name_key);
        int Insert(Keywords_key item);
        int Update(Keywords_key item);
        int Delete(List<string> ids);
    }
}
