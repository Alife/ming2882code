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
    /// 角色操作权限
    /// </summary>
    public class sys_PermissionData : DALHelper
    {
        public int Save(List<sys_Permission> list)
        {
            string strSql = string.Empty;
            foreach (sys_Permission item in list)
            {
                if (item.ID == 0)
                    strSql += string.Format("INSERT INTO sys_Permission(RoleID,OperationID)VALUES ('{0}','{1}');\r\n ", item.RoleID, item.OperationID);
                else
                    strSql += string.Format("DELETE sys_Permission where RoleID='{0}' and OperationID='{1}';\r\n ", item.RoleID, item.OperationID);
            }
            if (strSql != string.Empty)
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            return -1;
        }
        public sys_Permission GetItem(int _id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_Permission WHERE  ");
            strSql.AppendFormat("  ID={0}", _id);
            sys_Permission item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            item = new sys_Permission();
                            item = GetItem(item, dr);
                        }
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
                return item;
            }
        }

        public List<sys_Permission> GetList(int _roleID, int _operationID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_Permission WHERE 1=1 ");
            if (_roleID > 0)
                strSql.AppendFormat(" and RoleID={0}", _roleID);
            if (_operationID > 0)
                strSql.AppendFormat(" and OperationID={0}", _operationID);
            List<sys_Permission> list = new List<sys_Permission>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Permission(), dr));
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
        private sys_Permission GetItem(sys_Permission model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.RoleID = DBHelper.GetInt(dr["RoleID"]);
            model.OperationID = DBHelper.GetInt(dr["OperationID"]);
            return model;
        }
    }
}
