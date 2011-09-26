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
    public class d_KitClassData : DALHelper
    {
        public int Insert(d_KitClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitClass(");
            strSql.Append("Code,Name,KitID,BoyNum,GirlNum,Imprint)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Code,@in_Name,@in_KitID,@in_BoyNum,@in_GirlNum,@in_Imprint)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_KitID", DbType.Int32, model.KitID),
                DBHelper.CreateInDbParameter("@in_BoyNum", DbType.Int32, model.BoyNum),
                DBHelper.CreateInDbParameter("@in_GirlNum", DbType.Int32, model.GirlNum),
                DBHelper.CreateInDbParameter("@in_Imprint", DbType.String, model.Imprint)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Insert(List<d_KitClass> list)
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
                        strSql.Append("INSERT INTO d_KitClass(");
                        strSql.Append("Code,Name,KitID,BoyNum,GirlNum,Imprint)");
                        strSql.Append(" VALUES (");
                        strSql.Append("@in_Code,@in_Name,@in_KitID,@in_BoyNum,@in_GirlNum,@in_Imprint)"); 
                        strSql.Append("\r\n");
                    }
                    else
                    {
                        strSql.Append("UPDATE d_KitClass SET ");
                        strSql.Append("Code=@in_Code,");
                        strSql.Append("Name=@in_Name,");
                        strSql.Append("KitID=@in_KitID,");
                        strSql.Append("BoyNum=@in_BoyNum,");
                        strSql.Append("GirlNum=@in_GirlNum,");
                        strSql.Append("Imprint=@in_Imprint");
                        strSql.Append(" WHERE ID=@in_ID");
                        strSql.Append("\r\n");
                    }
                    DbParameter[] cmdParms = new DbParameter[]{
                        DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                        DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                        DBHelper.CreateInDbParameter("@in_KitID", DbType.Int32, model.KitID),
                        DBHelper.CreateInDbParameter("@in_BoyNum", DbType.Int32, model.BoyNum),
                        DBHelper.CreateInDbParameter("@in_GirlNum", DbType.Int32, model.GirlNum),
                        DBHelper.CreateInDbParameter("@in_Imprint", DbType.String, model.Imprint),
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

        public int Update(d_KitClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitClass SET ");
            strSql.Append("Code=@in_Code,");
            strSql.Append("Name=@in_Name,");
            strSql.Append("KitID=@in_KitID,");
            strSql.Append("BoyNum=@in_BoyNum,");
            strSql.Append("GirlNum=@in_GirlNum,");
            strSql.Append("Imprint=@in_Imprint");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_KitID", DbType.Int32, model.KitID),
                DBHelper.CreateInDbParameter("@in_BoyNum", DbType.Int32, model.BoyNum),
                DBHelper.CreateInDbParameter("@in_GirlNum", DbType.Int32, model.GirlNum),
                DBHelper.CreateInDbParameter("@in_Imprint", DbType.String, model.Imprint),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM d_KitClass WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitClass GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitClass ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitClass item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public d_KitClass GetItem(int kitID, string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitClass ");
            strSql.Append(" WHERE code=@code and kitID=@kitID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@kitID", DbType.Int32, kitID),
				DBHelper.CreateInDbParameter("@code", DbType.String, code)};
            d_KitClass item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_KitClass> GetList(int kitID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitClass ");
            strSql.Append(" WHERE kitID=@kitID");
            strSql.Append(" Order by ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@kitID", DbType.Int32, kitID)};
            List<d_KitClass> list = new List<d_KitClass>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private d_KitClass GetItem(d_KitClass model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitClass();
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
        private void GetModel(d_KitClass model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Code = DBHelper.GetString(dr["Code"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.KitID = DBHelper.GetInt(dr["KitID"]);
            model.BoyNum = DBHelper.GetInt(dr["BoyNum"]);
            model.GirlNum = DBHelper.GetInt(dr["GirlNum"]);
            model.Imprint = DBHelper.GetString(dr["Imprint"]);
        }
        private List<d_KitClass> GetItem(List<d_KitClass> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitClass model = new d_KitClass();
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