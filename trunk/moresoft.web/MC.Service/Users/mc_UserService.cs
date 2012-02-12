using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class mc_UserService : Imc_User
    {
        private readonly IDao _dao;
        public mc_UserService(IDao dao) { _dao = dao; }
        public PagedList<mc_User> GetListPage(QueryInfo queryInfo)
        {
            return _dao.GetListPage<mc_User>(queryInfo);
        }
        public IList<mc_User> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<mc_User>(queryInfo);
        }
        public mc_User GetForget(string _userName, string _email)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            query.Parameters.Add("Email", _email);
            return _dao.GetItem<mc_User>(query);
        }
        public bool IsUserExists(string _userName)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            if (_dao.GetItem<mc_User>(query) == null)
                return false;
            return true;
        }
        public bool IsEmailExists(string _email)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("Email", _email);
            if (_dao.GetItem<mc_User>(query) == null)
                return false;
            return true;
        }
        public mc_User GetUserLogin(string _userName, string _password, string _loginIP)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            query.Parameters.Add("Password", _password);
            query.Parameters.Add("LoginIP", _loginIP);
            query.XmlID = "GetUserLogin";
            return _dao.GetItem<mc_User>(query);
        }
        public int UpdatePassword(string ID, string oldPassword, string newPassword)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ID", ID);
            query.Parameters.Add("OldPassword", oldPassword);
            query.Parameters.Add("NewPassword", newPassword);
            query.XmlID = "UpdatePassword";
            query.MappingName = "mc_User";
            return _dao.Update(query);
        }
        public mc_User GetItem(string ID)
        {
            return _dao.GetItem<mc_User>(ID);
        }
        public mc_User GetItem(int userID)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ID", userID);
            return _dao.GetItem<mc_User>(query);
        }
        public int Insert(mc_User item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(mc_User item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(mc_User item)
        {
            return _dao.Delete(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(mc_User).Name;
            return _dao.Delete(query);
        }
    }
}
