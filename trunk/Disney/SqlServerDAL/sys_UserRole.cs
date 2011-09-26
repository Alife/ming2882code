using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    /// <summary>
    /// 用户角色操作
    /// </summary>
    public class sys_UserRoleData : DALHelper
    {
        public int Save(List<sys_UserRole> list)
        {
            string strSql = string.Empty;
            foreach (sys_UserRole item in list)
            {
                if (item.ID == 0)
                    strSql += string.Format("INSERT INTO sys_UserRole(RoleID,UserID)VALUES ('{0}','{1}');\r\n ", item.RoleID, item.UserID);
                else
                    strSql += string.Format("DELETE sys_UserRole where RoleID='{0}' and UserID='{1}';\r\n ", item.RoleID, item.UserID);
            }
            if (strSql != string.Empty)
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            return -1;
        }

        public List<sys_UserRole> GetList(int _userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_UserRole ");
            strSql.AppendFormat(" WHERE UserID={0}", _userID);
            List<sys_UserRole> list = new List<sys_UserRole>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_UserRole(), dr));
                    }
                }
                finally
                {
                    if (dr != null && !dr.IsClosed)
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
                return list;
            }
        }
        private sys_UserRole GetItem(sys_UserRole model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.RoleID = DBHelper.GetInt(dr["RoleID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            return model;
        }
    }
}
