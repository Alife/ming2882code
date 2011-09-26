using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Models;

namespace SqlServerDAL
{
    public class sys_SerialNumberData : DALHelper
    {
        public int Insert(sys_SerialNumber model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_SerialNumber(");
            strSql.Append("ID,Flag,CurrentDate,SerialNumber)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ID,@in_Flag,@in_CurrentDate,@in_SerialNumber)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID),
				DBHelper.CreateInDbParameter("@in_Flag", DbType.Int32, model.Flag),
				DBHelper.CreateInDbParameter("@in_CurrentDate", DbType.String, model.CurrentDate),
				DBHelper.CreateInDbParameter("@in_SerialNumber", DbType.Int32, model.SerialNumber)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 为更新一条数据准备参数
        /// </summary>
        public int Update(sys_SerialNumber model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_SerialNumber SET ");
            strSql.Append("Flag=@in_Flag,");
            strSql.Append("CurrentDate=@in_CurrentDate,");
            strSql.Append("SerialNumber=@in_SerialNumber");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_Flag", DbType.Int32, model.Flag),
				DBHelper.CreateInDbParameter("@in_CurrentDate", DbType.String, model.CurrentDate),
				DBHelper.CreateInDbParameter("@in_SerialNumber", DbType.Int32, model.SerialNumber),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM sys_SerialNumber WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_SerialNumber GetItem(int _flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 * FROM sys_SerialNumber ");
            strSql.Append(" where  Flag=@in_flag ");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_flag", DbType.Int32, _flag)};
            sys_SerialNumber item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_SerialNumber(), dr);
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

        /// <summary>
        /// 由一行数据得到一个实体
        /// </summary>
        private sys_SerialNumber GetItem(sys_SerialNumber model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Flag = DBHelper.GetInt(dr["Flag"]);
            model.CurrentDate = DBHelper.GetDateTime(dr["CurrentDate"]);
            model.SerialNumber = DBHelper.GetInt(dr["SerialNumber"]);
            return model;
        }
    }
}
