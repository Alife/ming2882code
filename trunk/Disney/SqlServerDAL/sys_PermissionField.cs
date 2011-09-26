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
    /// 角色权限字段操作
    /// </summary>
    public class sys_PermissionFieldData : DALHelper
    {
        public int Save(List<sys_PermissionField> list)
        {
            string strSql = string.Empty;
            foreach (sys_PermissionField item in list)
            {
                if (item.ID == 0)
                    strSql += string.Format("INSERT INTO sys_PermissionField(FieldID,PermissionID)VALUES ('{0}','{1}');\r\n ", item.FieldID, item.PermissionID);
                else
                    strSql += string.Format("DELETE sys_PermissionField where FieldID='{0}' and PermissionID='{1}';\r\n ", item.FieldID, item.PermissionID);
            }
            if (strSql != string.Empty)
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            return -1;
        }

        public List<sys_PermissionField> GetList(int _PermissionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_PermissionField ");
            strSql.AppendFormat(" WHERE PermissionID={0}", _PermissionID);
            List<sys_PermissionField> list = new List<sys_PermissionField>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_PermissionField(), dr));
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
        private sys_PermissionField GetItem(sys_PermissionField model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.FieldID = DBHelper.GetInt(dr["FieldID"]);
            model.PermissionID = DBHelper.GetInt(dr["PermissionID"]);
            return model;
        }
    }
}