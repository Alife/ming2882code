using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;
using DBUtility;

namespace SqlServerDAL
{
    public class d_KitCostumeData : DALHelper
    {
        public int Insert(d_KitCostume model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitCostume(");
            strSql.Append("KitChildID,CostumeID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_KitChildID,@in_CostumeID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                DBHelper.CreateInDbParameter("@in_CostumeID", DbType.Int32, model.CostumeID)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Insert(List<d_KitCostume> list)
        {
            int revalue = 0;
            if (list.Count > 0)
            {
                List<CommandInfo> cmdList = new List<CommandInfo>();
                foreach (var model in list)
                {
                    StringBuilder strSql = new StringBuilder();
                    if (model.ID == 0)
                    {
                        strSql.Append("INSERT INTO d_KitCostume(");
                        strSql.Append("KitChildID,CostumeID)");
                        strSql.Append(" VALUES (");
                        strSql.Append("@in_KitChildID,@in_CostumeID)"); 
                        strSql.Append("\r\n");
                    }
                    else
                    {
                        strSql.Append("UPDATE d_KitCostume SET ");
                        strSql.Append("KitChildID=@in_KitChildID,");
                        strSql.Append("CostumeID=@in_CostumeID");
                        strSql.Append(" WHERE ID=@in_ID");
                        strSql.Append("\r\n");
                    }
                    DbParameter[] cmdParms = new DbParameter[]{
                        DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                        DBHelper.CreateInDbParameter("@in_CostumeID", DbType.Int32, model.CostumeID),
                        DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
                    cmdList.Add(new CommandInfo(strSql.ToString(), cmdParms, EffentNextType.ExcuteEffectRows));
                }
                DbConnection conn = DBHelper.CreateConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                revalue = DBHelper.ExecuteNonQuery(tran, CommandType.Text, cmdList);
            }
            return revalue;
        }

        public int Update(d_KitCostume model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitCostume SET ");
            strSql.Append("KitChildID=@in_KitChildID,");
            strSql.Append("CostumeID=@in_CostumeID");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                DBHelper.CreateInDbParameter("@in_CostumeID", DbType.Int32, model.CostumeID),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM d_KitCostume WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitCostume GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitCostume ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitCostume item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_KitCostume> GetList(int KitChildID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitCostume ");
            strSql.Append(" WHERE KitChildID=@KitChildID");
            strSql.Append(" Order by ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@KitChildID", DbType.Int32, KitChildID)};
            List<d_KitCostume> list = new List<d_KitCostume>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private d_KitCostume GetItem(d_KitCostume model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitCostume();
                        GetModel(model, dr);
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
            return model;
        }
        private void GetModel(d_KitCostume model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.CostumeID = DBHelper.GetInt(dr["CostumeID"]);
            model.KitChildID = DBHelper.GetInt(dr["KitChildID"]);
        }
        private List<d_KitCostume> GetItem(List<d_KitCostume> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitCostume model = new d_KitCostume();
                        GetModel(model, dr);
                        list.Add(model);
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
            return list;
        }
        #endregion
    }
}
