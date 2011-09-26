namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class MessageData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("delete d_Message where ID={0}; ", item);
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public MessageList GetList(int _pageIndex, int _pageSize)
        {
            MessageList list = new MessageList();
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                string str = string.Empty;
                string str2 = "order by ID desc";
                List<DbParameter> list2 = new List<DbParameter>();
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, _pageSize));
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, _pageIndex));
                DbParameter[] cmdParms = list2.ToArray();
                string cmdText = "SELECT COUNT(ID) FROM d_Message where 1=1 " + str;
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
                    cmdText = string.Format("SELECT ID,TrueName,Address,Mobile,Tel,QQ,[Content],Replay,CreateTime,ReplayTime FROM " +
                        " (select ID,TrueName,Address,Mobile,Tel,QQ,Content,Replay,CreateTime,ReplayTime,ROW_NUMBER() Over({0}) as rowNum from d_Message " +
                        " where 1=1 {1}) as temptable WHERE rowNum BETWEEN ((@PageIndex-1)*@PageSize+1) and (@PageIndex)*@PageSize", str2, str);
                    DbDataReader reader = DALHelper.DBHelper.ExecuteReader(connectionString, CommandType.Text, cmdText, cmdParms);
                    while (reader.Read())
                    {
                        Message message = new Message();
                        message.ID = reader.GetInt32(0);
                        message.TrueName = reader.GetString(1);
                        message.Address = (reader.GetValue(2) != DBNull.Value) ? reader.GetString(2) : string.Empty;
                        message.Mobile = (reader.GetValue(3) != DBNull.Value) ? reader.GetString(3) : string.Empty;
                        message.Tel = (reader.GetValue(4) != DBNull.Value) ? reader.GetString(4) : string.Empty;
                        message.QQ = (reader.GetValue(5) != DBNull.Value) ? reader.GetString(5) : string.Empty;
                        message.Content = (reader.GetValue(6) != DBNull.Value) ? reader.GetString(6) : string.Empty;
                        message.Replay = (reader.GetValue(7) != DBNull.Value) ? reader.GetString(7) : string.Empty;
                        message.CreateTime = reader.GetDateTime(8);
                        message.ReplayTime = DALHelper.DBHelper.GetDateTimeByNull(reader.GetValue(9));
                        list.Add(message);
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

        public int Insert(Message item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into d_Message(");
            builder.Append("TrueName,Address,Mobile,Tel,QQ,[Content],Replay,CreateTime,ReplayTime)");
            builder.Append(" values (");
            builder.Append("@TrueName,@Address,@Mobile,@Tel,@QQ,@Content,@Replay,@CreateTime,@ReplayTime)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@TrueName", DbType.String, 50, item.TrueName), 
                DALHelper.DBHelper.CreateInDbParameter("@Address", DbType.String, 100, item.Address), 
                DALHelper.DBHelper.CreateInDbParameter("@Mobile", DbType.String, 50, item.Mobile), 
                DALHelper.DBHelper.CreateInDbParameter("@Tel", DbType.String, 50, item.Tel), 
                DALHelper.DBHelper.CreateInDbParameter("@QQ", DbType.String, 50, item.QQ), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, 1000, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@Replay", DbType.String, 500, item.Replay), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime), 
                DALHelper.DBHelper.CreateInDbParameter("@ReplayTime", DbType.DateTime, item.ReplayTime) };
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(Message item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update d_Message set ");
            builder.Append("Replay=@Replay,ReplayTime=@ReplayTime");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@Replay", DbType.String, 500, item.Replay) ,
                DALHelper.DBHelper.CreateInDbParameter("@ReplayTime", DbType.DateTime, item.ReplayTime)};
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}
