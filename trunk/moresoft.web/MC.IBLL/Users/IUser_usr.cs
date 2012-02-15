using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.IBLL
{
    public interface IUser_usr
    {
        PagedIList<User_usr> GetPageList(QueryInfo queryInfo);
        IList<User_usr> GetList(QueryInfo queryInfo);
        User_usr GetForget(string _userName, string _email);
        bool IsUserExists(string _userName);
        bool IsEmailExists(string _email);
        User_usr GetUserLogin(string _userName, string _password, string _loginIP);
        int UpdatePassword(string ID, string oldPassword, string newPassword);
        User_usr GetItem(string ID);
        User_usr GetItem(int userID);
        int Insert(User_usr item);
        int Update(User_usr item);
        int Delete(User_usr item);
        int Delete(List<string> ids);
    }
}
