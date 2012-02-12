using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface Imc_User
    {
        PagedList<mc_User> GetListPage(QueryInfo queryInfo);
        IList<mc_User> GetList(QueryInfo queryInfo);
        mc_User GetForget(string _userName, string _email);
        bool IsUserExists(string _userName);
        bool IsEmailExists(string _email);
        mc_User GetUserLogin(string _userName, string _password, string _loginIP);
        int UpdatePassword(string ID, string oldPassword, string newPassword);
        mc_User GetItem(string ID);
        mc_User GetItem(int userID);
        int Insert(mc_User item);
        int Update(mc_User item);
        int Delete(mc_User item);
        int Delete(List<string> ids);
    }
}
