using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface ISetting_set
    {
        IList<Setting_set> GetList(QueryInfo queryInfo);
        Setting_set GetItem(object ID_set);
        int Insert(Setting_set item);
        int Update(Setting_set item);
        int Delete(List<string> ids);
    }
}
