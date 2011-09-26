namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ProductFileData : DALHelper
    {
        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete d_ProductFile ");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
        private ProductFile GetItem(DbDataReader dr, ProductFile item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.ProductID = DALHelper.DBHelper.GetInt(dr["ProductID"]);
            item.FilePath = DALHelper.DBHelper.GetString(dr["FilePath"]);
            item.FileType = DALHelper.DBHelper.GetString(dr["FileType"]);
            item.FileName = DALHelper.DBHelper.GetString(dr["FileName"]);
            item.FileSize = DALHelper.DBHelper.GetDecimal(dr["FileSize"]);
            item.IsTop = DALHelper.DBHelper.GetBool(dr["IsTop"]);
            return item;
        }

        public ProductFile GetItem(int _id, bool _istop)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ProductID,FilePath,FileType,FileName,FileSize,IsTop from d_ProductFile ");
            if (_istop)
                builder.Append(" where ProductID=@ID and IsTop=1");
            else
                builder.Append(" where ID=@ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id) };
            ProductFile item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ProductFile();
                            GetItem(reader, item);
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

        public List<ProductFile> GetList(int ProductID)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select ID,ProductID,FilePath,FileType,FileName,FileSize,IsTop from d_ProductFile where ProductID={0}", ProductID);
            builder.Append(" Order By ID Desc");
            List<ProductFile> list = new List<ProductFile>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(GetItem(reader, new ProductFile()));
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

        public int Insert(ProductFile item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into d_ProductFile(");
            builder.Append("ProductID,FilePath,FileType,FileName,FileSize,IsTop)");
            builder.Append(" values (");
            builder.Append("@ProductID,@FilePath,@FileType,@FileName,@FileSize,@IsTop)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] {
                DALHelper.DBHelper.CreateInDbParameter("@ProductID", DbType.Int32, 4, item.ProductID), 
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, item.FilePath), 
                DALHelper.DBHelper.CreateInDbParameter("@FileType", DbType.String, item.FileType), 
                DALHelper.DBHelper.CreateInDbParameter("@FileName", DbType.String, item.FileName), 
                DALHelper.DBHelper.CreateInDbParameter("@FileSize", DbType.Decimal, 5, item.FileSize), 
                DALHelper.DBHelper.CreateInDbParameter("@IsTop", DbType.Boolean, 1, item.IsTop)};
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(ProductFile item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update d_ProductFile set ");
            builder.Append("ProductID=@ProductID,");
            builder.Append("FilePath=@FilePath,");
            builder.Append("FileType=@FileType,");
            builder.Append("FileName=@FileName,");
            builder.Append("FileSize=@FileSize,");
            builder.Append("IsTop=@IsTop");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@ProductID", DbType.Int32, 4, item.ProductID), 
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, item.FilePath), 
                DALHelper.DBHelper.CreateInDbParameter("@FileType", DbType.String, item.FileType), 
                DALHelper.DBHelper.CreateInDbParameter("@FileName", DbType.String, item.FileName), 
                DALHelper.DBHelper.CreateInDbParameter("@FileSize", DbType.Decimal, 5, item.FileSize), 
                DALHelper.DBHelper.CreateInDbParameter("@IsTop", DbType.Boolean, 1, item.IsTop)
            };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}
