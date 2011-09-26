using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections;
using Models;

namespace SqlServerDAL
{
    public class t_UserInfoData : DALHelper
    {
        public int Insert(t_UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO t_UserInfo(");
            strSql.Append("UserID,IsMarry,BirthPlace,PoliticsStatus,College,Speciality,Education,JobTime,OnDutyTime,DimissionTime,Duty,Nation,IDCard,Address,Zip,IsEmail,Resume)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID,@in_IsMarry,@in_BirthPlace,@in_PoliticsStatus, @in_College, @in_Speciality,@in_Education,@in_JobTime,@in_OnDutyTime,@in_DimissionTime,@in_Duty,@in_Nation,@in_IDCard,@in_Address,@in_Zip,@in_IsEmail,@in_Resume)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_IsMarry", DbType.Boolean, model.IsMarry),
                DBHelper.CreateInDbParameter("@in_BirthPlace", DbType.Int32, model.BirthPlace),
                DBHelper.CreateInDbParameter("@in_PoliticsStatus", DbType.Int32, model.PoliticsStatus),
                DBHelper.CreateInDbParameter("@in_College", DbType.String, model.College),
                DBHelper.CreateInDbParameter("@in_Speciality", DbType.String, model.Speciality),
                DBHelper.CreateInDbParameter("@in_Education", DbType.Int32, model.Education),
                DBHelper.CreateInDbParameter("@in_JobTime", DbType.String, model.JobTime),
                DBHelper.CreateInDbParameter("@in_OnDutyTime", DbType.String, model.OnDutyTime),
                DBHelper.CreateInDbParameter("@in_DimissionTime", DbType.String, model.DimissionTime),
                DBHelper.CreateInDbParameter("@in_Duty", DbType.String, model.Duty),
                DBHelper.CreateInDbParameter("@in_Nation", DbType.Int32, model.Nation),
                DBHelper.CreateInDbParameter("@in_IDCard", DbType.String, model.IDCard),
                DBHelper.CreateInDbParameter("@in_Address", DbType.String, model.Address),
                DBHelper.CreateInDbParameter("@in_Zip", DbType.String, model.Zip),
                DBHelper.CreateInDbParameter("@in_IsEmail", DbType.Boolean, model.IsEmail),
                DBHelper.CreateInDbParameter("@in_Resume", DbType.String, model.Resume)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Update(t_UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE t_UserInfo SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("IsMarry=@in_IsMarry,");
            strSql.Append("BirthPlace=@in_BirthPlace,");
            strSql.Append("PoliticsStatus=@in_PoliticsStatus,");
            strSql.Append("College=@in_College,");
            strSql.Append("Speciality=@in_Speciality,");
            strSql.Append("Education=@in_Education,");
            strSql.Append("JobTime=@in_JobTime,");
            strSql.Append("OnDutyTime=@in_OnDutyTime,");
            strSql.Append("DimissionTime=@in_DimissionTime,");
            strSql.Append("Duty=@in_Duty,");
            strSql.Append("Nation=@in_Nation,");
            strSql.Append("IDCard=@in_IDCard,");
            strSql.Append("Address=@in_Address,");
            strSql.Append("Zip=@in_Zip,");
            strSql.Append("IsEmail=@in_IsEmail,");
            strSql.Append("Resume=@in_Resume");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_IsMarry", DbType.Boolean, model.IsMarry),
                DBHelper.CreateInDbParameter("@in_BirthPlace", DbType.Int32, model.BirthPlace),
                DBHelper.CreateInDbParameter("@in_PoliticsStatus", DbType.Int32, model.PoliticsStatus),
                DBHelper.CreateInDbParameter("@in_College", DbType.String, model.College),
                DBHelper.CreateInDbParameter("@in_Speciality", DbType.String, model.Speciality),
                DBHelper.CreateInDbParameter("@in_Education", DbType.Int32, model.Education),
                DBHelper.CreateInDbParameter("@in_JobTime", DbType.String, model.JobTime),
                DBHelper.CreateInDbParameter("@in_OnDutyTime", DbType.String, model.OnDutyTime),
                DBHelper.CreateInDbParameter("@in_DimissionTime", DbType.String, model.DimissionTime),
                DBHelper.CreateInDbParameter("@in_Duty", DbType.String, model.Duty),
                DBHelper.CreateInDbParameter("@in_Nation", DbType.Int32, model.Nation),
                DBHelper.CreateInDbParameter("@in_IDCard", DbType.String, model.IDCard),
                DBHelper.CreateInDbParameter("@in_Address", DbType.String, model.Address),
                DBHelper.CreateInDbParameter("@in_Zip", DbType.String, model.Zip),
                DBHelper.CreateInDbParameter("@in_IsEmail", DbType.Boolean, model.IsEmail),
                DBHelper.CreateInDbParameter("@in_Resume", DbType.String, model.Resume),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
 
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public t_UserInfo GetItem(int userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserInfo ");
            strSql.Append(" WHERE UserID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, userID)};
            t_UserInfo item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_UserInfo(), dr);
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

        private t_UserInfo GetItem(t_UserInfo model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.IsMarry = DBHelper.GetBool(dr["IsMarry"]);
            model.BirthPlace = DBHelper.GetIntByNull(dr["BirthPlace"]);
            model.PoliticsStatus = DBHelper.GetIntByNull(dr["PoliticsStatus"]);
            model.College = DBHelper.GetString(dr["College"]);
            model.Speciality = DBHelper.GetString(dr["Speciality"]);
            model.Education = DBHelper.GetIntByNull(dr["Education"]);
            model.JobTime = DBHelper.GetString(dr["JobTime"]);
            model.OnDutyTime = DBHelper.GetString(dr["OnDutyTime"]);
            model.DimissionTime = DBHelper.GetString(dr["DimissionTime"]);
            model.Duty = DBHelper.GetString(dr["Duty"]);
            model.Nation = DBHelper.GetIntByNull(dr["Nation"]);
            model.IDCard = DBHelper.GetString(dr["IDCard"]);
            model.Address = DBHelper.GetString(dr["Address"]);
            model.Zip = DBHelper.GetString(dr["Zip"]);
            model.IsEmail = DBHelper.GetBool(dr["IsEmail"]);
            model.Resume = DBHelper.GetString(dr["Resume"]);
            return model;
        }
    }
}
