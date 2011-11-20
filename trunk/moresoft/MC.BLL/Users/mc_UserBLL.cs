using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.Model;

namespace MC.BLL
{
    public class mc_UserBLL
    {
        public static PagedList<mc_User> GetListPage(QueryInfo queryInfo)
        {
            return BLLService.GetListPage<mc_User>(queryInfo);
        }
        public static IList<mc_User> GetList(QueryInfo queryInfo)
        {
            return BLLService.GetList<mc_User>(queryInfo);
        }
        public static mc_User GetForget(string _userName, string _email)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            query.Parameters.Add("Email", _email);
            return BLLService.GetItem<mc_User>(query);
        }
        public static bool IsUserExists(string _userName)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            if (BLLService.GetItem<mc_User>(query) == null)
                return false;
            return true;
        }
        public static bool IsEmailExists(string _email)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("Email", _email);
            if (BLLService.GetItem<mc_User>(query) == null)
                return false;
            return true;
        }
        public static mc_User GetUserLogin(string _userName, string _password, string _loginIP)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            query.Parameters.Add("Password", _password);
            query.Parameters.Add("LoginIP", _loginIP);
            query.XmlID = "GetUserLogin";
            return BLLService.GetItem<mc_User>(query);
        }
        public static int UpdatePassword(string ID, string oldPassword, string newPassword)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ID", ID);
            query.Parameters.Add("OldPassword", oldPassword);
            query.Parameters.Add("NewPassword", newPassword);
            query.XmlID = "UpdatePassword";
            query.MappingName = "mc_User";
            return BLLService.Update(query);
        }
        public static mc_User GetItem(string ID)
        {
            return BLLService.GetItem<mc_User>(ID);
        }
        public static mc_User GetItem(int userID)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ID", userID);
            return BLLService.GetItem<mc_User>(query);
        }
        public static int Insert(mc_User item)
        {
            item.SetState(EntityState.Added);
            return BLLService.Save(item);
        }
        public static int Update(mc_User item)
        {
            item.SetState(EntityState.Modified);
            return BLLService.Save(item);
        }
        public static int Delete(mc_User item)
        {
            return BLLService.Delete(item);
        }
        public static int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(mc_User).Name;
            return BLLService.Delete(query);
        }
    }
}
