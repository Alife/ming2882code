using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MC.DAO;
using MC.Model;
using MC.IBLL;

namespace MC.Service
{
    public class User_usrService : IUser_usr
    {
        private readonly IDao _dao;
        public User_usrService(IDao dao) { _dao = dao; }
        public PagedIList<User_usr> GetPageList(QueryInfo queryInfo)
        {
            return _dao.GetIListPage<User_usr>(queryInfo);
        }
        public IList<User_usr> GetList(QueryInfo queryInfo)
        {
            return _dao.GetList<User_usr>(queryInfo);
        }
        public User_usr GetForget(string _userName, string _email)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            query.Parameters.Add("Email", _email);
            return _dao.GetItem<User_usr>(query);
        }
        public bool IsUserExists(string _userName)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            if (_dao.GetItem<User_usr>(query) == null)
                return false;
            return true;
        }
        public bool IsEmailExists(string _email)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("Email", _email);
            if (_dao.GetItem<User_usr>(query) == null)
                return false;
            return true;
        }
        public User_usr GetUserLogin(string _userName, string _password, string _loginIP)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("UserName", _userName);
            query.Parameters.Add("Password", _password);
            query.Parameters.Add("LoginIP", _loginIP);
            query.XmlID = "GetUserLogin";
            return _dao.GetItem<User_usr>(query);
        }
        public int UpdatePassword(string ID, string oldPassword, string newPassword)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ID", ID);
            query.Parameters.Add("OldPassword", oldPassword);
            query.Parameters.Add("NewPassword", newPassword);
            query.XmlID = "UpdatePassword";
            query.MappingName = "User_usr";
            return _dao.Update(query);
        }
        public User_usr GetItem(string ID)
        {
            return _dao.GetItem<User_usr>(ID);
        }
        public User_usr GetItem(int userID)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ID", userID);
            return _dao.GetItem<User_usr>(query);
        }
        public int Insert(User_usr item)
        {
            item.SetState(EntityState.Added);
            return _dao.Save(item);
        }
        public int Update(User_usr item)
        {
            item.SetState(EntityState.Modified);
            return _dao.Save(item);
        }
        public int Delete(User_usr item)
        {
            return _dao.Delete(item);
        }
        public int Delete(List<string> ids)
        {
            QueryInfo query = new QueryInfo();
            query.Parameters.Add("ids", ids);
            query.MappingName = typeof(User_usr).Name;
            return _dao.Delete(query);
        }
    }
}
