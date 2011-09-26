using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_ArtistPriceData : DALHelper
    {
        public int Insert(d_ArtistPrice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_ArtistPrice(");
            strSql.Append("UserID,KitPhotoTypeID,Price)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID,@in_KitPhotoTypeID,@in_Price)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitPhotoTypeID", DbType.Int32, model.KitPhotoTypeID),
                DBHelper.CreateInDbParameter("@in_Price", DbType.Decimal, model.Price)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_ArtistPrice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_ArtistPrice SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("KitPhotoTypeID=@in_KitPhotoTypeID,");
            strSql.Append("Price=@in_Price");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitPhotoTypeID", DbType.Int32, model.KitPhotoTypeID),
                DBHelper.CreateInDbParameter("@in_Price", DbType.Decimal, model.Price),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM d_ArtistPrice WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_ArtistPrice GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_ArtistPrice ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_ArtistPrice item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public d_ArtistPrice GetItem(int uid, int kitPhotoTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_ArtistPrice ");
            strSql.Append(" WHERE UserID=@UserID and KitPhotoTypeID=@KitPhotoTypeID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@UserID", DbType.Int32, uid),
				DBHelper.CreateInDbParameter("@KitPhotoTypeID", DbType.Int32, kitPhotoTypeID)};
            d_ArtistPrice item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public DataTable GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ap.*,u.TrueName as Arter,kpt.Name as KitPhotoType FROM d_ArtistPrice as ap ");
            strSql.Append("left join d_KitPhotoType as kpt on kpt.ID=ap.KitPhotoTypeID ");
            strSql.Append("left join t_User as u on u.ID=ap.UserID where 1=1 ");
            strSql.Append(" Order by u.ID desc");
            return DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), null).Tables[0];
        }
        #region 私有
        private d_ArtistPrice GetItem(d_ArtistPrice model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_ArtistPrice();
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
        private void GetModel(d_ArtistPrice model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.KitPhotoTypeID = DBHelper.GetInt(dr["KitPhotoTypeID"]);
            model.Price = DBHelper.GetDecimal(dr["Price"]);
        }
        private List<d_ArtistPrice> GetItem(List<d_ArtistPrice> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_ArtistPrice model = new d_ArtistPrice();
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