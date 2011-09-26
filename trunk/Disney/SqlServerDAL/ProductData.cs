namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ProductData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("delete d_ProductFile where ProductID={0}; ", item);
                builder.AppendFormat("delete d_Product where ID={0}; ", item);
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Exists(string _Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID from d_Product");
            builder.AppendFormat(" where Code='{0}' ", _Code);
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null);
            if (obj2 != null)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        public Product GetItem(string ID, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  ID,[Name],Code,CategoryID,Price,PriceMarket,CreateTime,ProType,IsSell,[Top],Elite,Hits,Tags,[Content] from d_Product ");
            DbParameter[] cmdParms;
            if (type == 1)
            {
                builder.Append(" where ID=@ID");
                cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };
            }
            else
            {
                builder.Append(" where Code=@ID");
                cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.String, ID) };
            }
            Product item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new Product();
                            this.GetItem(reader, item);
                        }
                    }
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            return item;
        }

        private Product GetItem(DbDataReader dr, Product item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.Code = DALHelper.DBHelper.GetString(dr["Code"]);
            item.Name = DALHelper.DBHelper.GetString(dr["Name"]);
            item.CategoryID = DALHelper.DBHelper.GetInt(dr["CategoryID"]);
            item.Price = DALHelper.DBHelper.GetDecimal(dr["Price"]);
            item.PriceMarket = DALHelper.DBHelper.GetDecimal(dr["PriceMarket"]);
            item.CreateTime = DALHelper.DBHelper.GetDateTime(dr["CreateTime"]);
            item.ProType = DALHelper.DBHelper.GetInt(dr["ProType"]);
            item.Elite = DALHelper.DBHelper.GetInt(dr["Elite"]);
            item.Top = DALHelper.DBHelper.GetInt(dr["Top"]);
            item.IsSell = DALHelper.DBHelper.GetBool(dr["IsSell"]);
            item.Hits = DALHelper.DBHelper.GetInt(dr["Hits"]);
            item.Tags = DALHelper.DBHelper.GetString(dr["Tags"]);
            item.Content = DALHelper.DBHelper.GetString(dr["Content"]);
            return item;
        }
        public List<Product> GetList(string categorycode, int _elite, int _top, int num)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select top {0} ID,[Name],Code,CategoryID,Price,PriceMarket,CreateTime,ProType,[Top],Elite,IsSell,Hits,Tags,[Content]", num);
            builder.Append(" from d_Product where 1=1");
            if (categorycode != string.Empty)
            {
                string tempcategorycode = string.Empty;
                foreach (string code in categorycode.Split(','))
                    tempcategorycode += string.Format("'{0}',", code);
                tempcategorycode = tempcategorycode.TrimEnd(',');
                builder.AppendFormat(" and CategoryID in (select id from d_ProductCategory where Code in ({0}))", tempcategorycode);
            }
            if (_elite > 0)
                builder.AppendFormat(" and Elite={0}", _elite);
            if (_top > 0)
                builder.AppendFormat(" and [Top]={0}", _top);
            builder.Append(" Order By CreateTime desc");
            List<Product> list = new List<Product>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(GetItem(reader, new Product()));
                        }
                    }
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            return list;
        }

        public ProductList GetList(int _categoryID, string _unQuery, string _title, int _elite, int _top, int _pageIndex, int _pageSize)
        {
            ProductList list = new ProductList();
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                string query = string.Empty;
                string order = "order by CreateTime desc";
                List<DbParameter> list2 = new List<DbParameter>();
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, _pageSize));
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, _pageIndex));
                if (!string.IsNullOrEmpty(_unQuery))
                {
                    string temsql = "and CategoryID not in (select id FROM d_ProductCategory where code in (";
                    foreach (string temType in _unQuery.Split(','))
                        temsql += string.Format("'{0}',", temType);
                    temsql = temsql.TrimEnd(',');
                    temsql += ")) ";
                    query += temsql;
                }
                if (_categoryID > 0)
                    query += string.Format("and CategoryID={0}", _categoryID);
                if (_title != string.Empty)
                    query += string.Format("and Title like '%{0}%' ", _title);
                if (_elite > 0)
                    query += string.Format("and Elite={0}", _elite);
                if (_top > 0)
                    query += string.Format("and [Top]={0}", _top);
                DbParameter[] cmdParms = list2.ToArray();
                string cmdText = "SELECT COUNT(ID) FROM d_Product where 1=1 " + query;
                object obj2 = DALHelper.DBHelper.ExecuteScalar(connectionString, CommandType.Text, cmdText, cmdParms);
                if (obj2 != null)
                {
                    list.RecordNumber = int.Parse(obj2.ToString());
                }
                else
                {
                    return list;
                }
                if (list.RecordNumber != 0)
                {
                    cmdText = string.Format("SELECT ID,[Name],Code,CategoryID,Price,PriceMarket,CreateTime,ProType,[Top],Elite,IsSell,Hits,Tags,[Content]\r\n" +
                        " FROM \r\n"+
                        " (select ID,[Name],Code,CategoryID,Price,PriceMarket,CreateTime,ProType,[Top],Elite,IsSell,Hits,Tags,[Content]\r\n " +
                        " ,ROW_NUMBER() Over({0}) as rowNum from d_Product where 1=1 {1}) as temptable\r\n"+
                        " WHERE rowNum BETWEEN ((@PageIndex-1)*@PageSize+1) and (@PageIndex)*@PageSize", order, query);
                    DbDataReader reader = DALHelper.DBHelper.ExecuteReader(connectionString, CommandType.Text, cmdText, cmdParms);
                    while (reader.Read())
                    {
                        Product product = new Product();
                        GetItem(reader, product);
                        list.Add(product);
                    }
                }
            }
            finally
            {
                connectionString.Close();
                connectionString.Dispose();
            }
            return list;
        }

        public int Insert(Product item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into d_Product(");
            builder.Append("[Name],Code,CategoryID,Price,PriceMarket,CreateTime,ProType,[Top],Elite,IsSell,Hits,Tags,[Content])");
            builder.Append(" values (");
            builder.Append("@Name,@Code,@CategoryID,@Price,@PriceMarket,@CreateTime,@ProType,@Top,@Elite,@IsSell,@Hits,@Tags,@Content)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name), 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@CategoryID", DbType.Int32, item.CategoryID), 
                DALHelper.DBHelper.CreateInDbParameter("@Price", DbType.Decimal, 5, item.Price), 
                DALHelper.DBHelper.CreateInDbParameter("@PriceMarket", DbType.Decimal, 5, item.PriceMarket), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime), 
                DALHelper.DBHelper.CreateInDbParameter("@ProType", DbType.Int32, item.ProType), 
                DALHelper.DBHelper.CreateInDbParameter("@Top", DbType.Int32, 4, item.Top), 
                DALHelper.DBHelper.CreateInDbParameter("@Elite", DbType.Int32, 4, item.Elite),  
                DALHelper.DBHelper.CreateInDbParameter("@IsSell", DbType.Boolean, 1, item.IsSell), 
                DALHelper.DBHelper.CreateInDbParameter("@Hits", DbType.Int32, 4, item.Hits), 
                DALHelper.DBHelper.CreateInDbParameter("@Tags", DbType.String, 50, item.Tags), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content) };
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
                return 0;
            return Convert.ToInt32(obj2);
        }
        public int Update(Product item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update d_Product set ");
            builder.Append("[Name]=@Name,");
            builder.Append("Code=@Code,");
            builder.Append("CategoryID=@CategoryID,");
            builder.Append("Price=@Price,");
            builder.Append("PriceMarket=@PriceMarket,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("ProType=@ProType,");
            builder.Append("[Top]=@Top,");
            builder.Append("Elite=@Elite,");
            builder.Append("IsSell=@IsSell,");
            builder.Append("Hits=@Hits,");
            builder.Append("Tags=@Tags,");
            builder.Append("[Content]=@Content");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name), 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@CategoryID", DbType.Int32, item.CategoryID), 
                DALHelper.DBHelper.CreateInDbParameter("@Price", DbType.Decimal, 5, item.Price), 
                DALHelper.DBHelper.CreateInDbParameter("@PriceMarket", DbType.Decimal, 5, item.PriceMarket), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime), 
                DALHelper.DBHelper.CreateInDbParameter("@ProType", DbType.Int32, item.ProType), 
                DALHelper.DBHelper.CreateInDbParameter("@Top", DbType.Int32, 4, item.Top), 
                DALHelper.DBHelper.CreateInDbParameter("@Elite", DbType.Int32, 4, item.Elite),  
                DALHelper.DBHelper.CreateInDbParameter("@IsSell", DbType.Boolean, 1, item.IsSell), 
                DALHelper.DBHelper.CreateInDbParameter("@Hits", DbType.Int32, 4, item.Hits), 
                DALHelper.DBHelper.CreateInDbParameter("@Tags", DbType.String, 50, item.Tags), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }

        public int MoveCategory(string ids, int _cid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update d_Product set CategoryID={0}", _cid);
            builder.AppendFormat(" where ID in ({0}) ", ids);
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
    }
}
