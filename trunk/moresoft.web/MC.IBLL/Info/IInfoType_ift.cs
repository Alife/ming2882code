using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface IInfoType_ift
    {
        IList<InfoType_ift> GetList(QueryInfo queryInfo);
        InfoType_ift GetItem(object ID_ift);
        int Insert(InfoType_ift item);
        int Update(InfoType_ift item);
        int Delete(int id);
    }
}
