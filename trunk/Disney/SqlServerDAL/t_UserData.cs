using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Models;
using Models.Enums;

namespace SqlServerDAL
{
    public class t_UserData : DALHelper
    {
        #region 共用
        public int Insert(t_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql = new StringBuilder();
            strSql.Append("INSERT INTO t_User(");
            strSql.Append("UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,Avatar,TypeID,");
            strSql.Append("DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserName,@in_UserCode,@in_UserCard,@in_Password,@in_Email,@in_TrueName,@in_Mobile,@in_Tel,@in_CountryID,@in_Sex, ");
            strSql.Append("@in_Birthday,@in_IsClose,@in_Avatar,@in_TypeID,@in_DepartmentID,@in_Credit,@in_Freeze,@in_RegTime,@in_LoginTime,@in_LoginIP,@in_LoginNum,@in_LinkMan,@in_DayNum)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserName", DbType.String, model.UserName),
                DBHelper.CreateInDbParameter("@in_UserCode", DbType.String, model.UserCode),
                DBHelper.CreateInDbParameter("@in_UserCard", DbType.String, model.UserCard),
                DBHelper.CreateInDbParameter("@in_Password", DbType.String, model.Password),
                DBHelper.CreateInDbParameter("@in_Email", DbType.String, model.Email),
                DBHelper.CreateInDbParameter("@in_TrueName", DbType.String, model.TrueName),
                DBHelper.CreateInDbParameter("@in_Mobile", DbType.String, model.Mobile),
                DBHelper.CreateInDbParameter("@in_Tel", DbType.String, model.Tel),
                DBHelper.CreateInDbParameter("@in_CountryID", DbType.Int32, model.CountryID),
                DBHelper.CreateInDbParameter("@in_Sex", DbType.Int32, model.Sex),
                DBHelper.CreateInDbParameter("@in_Birthday", DbType.String, model.Birthday),
                DBHelper.CreateInDbParameter("@in_IsClose", DbType.Boolean, model.IsClose),
                DBHelper.CreateInDbParameter("@in_Avatar", DbType.String, model.Avatar),
                DBHelper.CreateInDbParameter("@in_TypeID", DbType.Int32, model.TypeID),
                DBHelper.CreateInDbParameter("@in_DepartmentID", DbType.Int32, model.DepartmentID),
                DBHelper.CreateInDbParameter("@in_Credit", DbType.Decimal, model.Credit),
                DBHelper.CreateInDbParameter("@in_Freeze", DbType.Decimal, model.Freeze),
                DBHelper.CreateInDbParameter("@in_RegTime", DbType.DateTime, model.RegTime),
                DBHelper.CreateInDbParameter("@in_LoginTime", DbType.DateTime, model.LoginTime),
                DBHelper.CreateInDbParameter("@in_LoginIP", DbType.String, model.LoginIP),
                DBHelper.CreateInDbParameter("@in_LoginNum", DbType.Int32, model.LoginNum),
                DBHelper.CreateInDbParameter("@in_LinkMan", DbType.String, model.LinkMan),
                DBHelper.CreateInDbParameter("@in_DayNum", DbType.Int32, model.DayNum)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Update(t_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE t_User SET ");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserCode=@in_UserCode,");
            strSql.Append("UserCard=@in_UserCard,");
            if (!string.IsNullOrEmpty(model.Password))
                strSql.AppendFormat("Password='{0}',", model.Password);
            strSql.Append("Email=@in_Email,");
            strSql.Append("TrueName=@in_TrueName,");
            strSql.Append("Mobile=@in_Mobile,");
            strSql.Append("Tel=@in_Tel,");
            strSql.Append("CountryID=@in_CountryID,");
            strSql.Append("Sex=@in_Sex,");
            strSql.Append("Birthday=@in_Birthday,");
            strSql.Append("IsClose=@in_IsClose,");
            strSql.Append("Avatar=@in_Avatar,");
            strSql.Append("TypeID=@in_TypeID,");
            strSql.Append("DepartmentID=@in_DepartmentID,");
            strSql.Append("Credit=@in_Credit,");
            strSql.Append("Freeze=@in_Freeze,");
            strSql.Append("LoginTime=@in_LoginTime,");
            strSql.Append("LoginIP=@in_LoginIP,");
            strSql.Append("LoginNum=@in_LoginNum,");
            strSql.Append("LinkMan=@in_LinkMan,");
            strSql.Append("DayNum=@in_DayNum");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserName", DbType.String, model.UserName),
                DBHelper.CreateInDbParameter("@in_UserCode", DbType.String, model.UserCode),
                DBHelper.CreateInDbParameter("@in_UserCard", DbType.String, model.UserCard),
                DBHelper.CreateInDbParameter("@in_Password", DbType.String, model.Password),
                DBHelper.CreateInDbParameter("@in_Email", DbType.String, model.Email),
                DBHelper.CreateInDbParameter("@in_TrueName", DbType.String, model.TrueName),
                DBHelper.CreateInDbParameter("@in_Mobile", DbType.String, model.Mobile),
                DBHelper.CreateInDbParameter("@in_Tel", DbType.String, model.Tel),
                DBHelper.CreateInDbParameter("@in_CountryID", DbType.Int32, model.CountryID),
                DBHelper.CreateInDbParameter("@in_Sex", DbType.Int32, model.Sex),
                DBHelper.CreateInDbParameter("@in_Birthday", DbType.String, model.Birthday),
                DBHelper.CreateInDbParameter("@in_IsClose", DbType.Boolean, model.IsClose),
                DBHelper.CreateInDbParameter("@in_Avatar", DbType.String, model.Avatar),
                DBHelper.CreateInDbParameter("@in_TypeID", DbType.Int32, model.TypeID),
                DBHelper.CreateInDbParameter("@in_DepartmentID", DbType.Int32, model.DepartmentID),
                DBHelper.CreateInDbParameter("@in_Credit", DbType.Decimal, model.Credit),
                DBHelper.CreateInDbParameter("@in_Freeze", DbType.Decimal, model.Freeze),
                DBHelper.CreateInDbParameter("@in_LoginTime", DbType.DateTime, model.LoginTime),
                DBHelper.CreateInDbParameter("@in_LoginIP", DbType.String, model.LoginIP),
                DBHelper.CreateInDbParameter("@in_LoginNum", DbType.Int32, model.LoginNum),
                DBHelper.CreateInDbParameter("@in_LinkMan", DbType.String, model.LinkMan),
                DBHelper.CreateInDbParameter("@in_DayNum", DbType.Int32, model.DayNum),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
 
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ID)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (string item in ID)
                strSql.AppendFormat("update t_User set IsClose=1 where ID={0};\r\n", item);
            if (ID.Count > 0)
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            return 0;
        }
        public bool IsMobileExists(string _mobile)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from t_User");
            builder.Append(" where Mobile=@Mobile ");
            DbParameter[] cmdParms = new DbParameter[] { DBHelper.CreateInDbParameter("@Mobile", DbType.String, _mobile) };
            if (int.Parse(DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms).ToString()) == 0)
                return false;
            return true;
        }
        public bool IsUserNameExists(string _username)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from t_User");
            builder.Append(" where UserName=@UserName ");
            DbParameter[] cmdParms = new DbParameter[] { DBHelper.CreateInDbParameter("@UserName", DbType.String, _username) };
            if (int.Parse(DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms).ToString()) == 0)
                return false;
            return true;
        }
        public bool IsEmailExists(string _email)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from t_User");
            builder.Append(" where email=@email ");
            DbParameter[] cmdParms = new DbParameter[] { DBHelper.CreateInDbParameter("@email", DbType.String, _email) };
            if (int.Parse(DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms).ToString()) == 0)
                return false;
            return true;
        }
        public t_User GetByCard(string _usercard)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select ");
            strSql.Append("ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum ");
            strSql.Append(" FROM t_User where UserCard=@UserCard ");
            DbParameter[] cmdParms = new DbParameter[] { DBHelper.CreateInDbParameter("@UserCard", DbType.String, _usercard) };

            t_User item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_User(), dr);
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
        public t_User GetByMobile(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select ");
            strSql.Append("ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum ");
            strSql.Append(" FROM t_User where mobile=@mobile ");
            DbParameter[] cmdParms = new DbParameter[] { DBHelper.CreateInDbParameter("@mobile", DbType.String, mobile) };

            t_User item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_User(), dr);
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
        public t_User GetItem(string _userCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select ");
            strSql.Append("ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum ");
            strSql.Append(" FROM t_User where UserCode=@UserCode ");
            DbParameter[] cmdParms = new DbParameter[] { DBHelper.CreateInDbParameter("@UserCode", DbType.String, _userCode) };

            t_User item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_User(), dr);
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
        /// 用户登录时用
        /// </summary>
        /// <param name="_password"></param>
        /// <param name="_lastLoginIP"></param>
        /// <returns></returns>
        public t_User GetUserLogin(string _loginId, int _logintype, string _password, string _lastLoginIP)
        {
            string cmdText = string.Empty;
            StringBuilder strSql = new StringBuilder();
            if (_logintype == 1)//前台登陆
                cmdText = string.Format("(UserName='{0}' or email='{0}' or mobile='{0}')", _loginId);
            else if (_logintype == 2)//后台登陆
                cmdText = string.Format("(UserName='{0}' or UserCode='{0}' or mobile='{0}')", _loginId);
            strSql.Append("declare @id int \r\n ");
            strSql.AppendFormat("if exists (select ID from t_User where {0} and Password=@Password )  \r\n", cmdText);
            strSql.Append("begin  \r\n ");
            strSql.AppendFormat("select @id=ID from t_User where {0} and Password=@Password; \r\n ", cmdText);
            strSql.Append("update t_User set LoginTime=getdate(),LoginNum=LoginNum+1,LoginIP=@LoginIP where id=@id; \r\n ");
            strSql.Append("SELECT ");
            strSql.Append("ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum ");
            strSql.Append(" FROM t_User where id=@id \r\n");
            strSql.Append("end \r\n ");

            DbParameter[] cmdParms = new DbParameter[] { 
                DBHelper.CreateInDbParameter("@LoginIP", DbType.String, _lastLoginIP), 
                DBHelper.CreateInDbParameter("@Password", DbType.String, _password) };
            t_User model = null;
            using (DbDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                            model = GetItem(new t_User(), reader);
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
            return model;
        }
        public t_User GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append("ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum ");
            strSql.Append(" FROM t_User where id=@in_ID");
            DbParameter[] cmdParms = {
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};

            t_User item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_User(), dr);
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
        #endregion
        #region -------- 私有方法，通常情况下无需修改 --------

        /// <summary>
        /// 由一行数据得到一个实体
        /// </summary>
        private t_User GetItem(t_User model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.UserName = DBHelper.GetString(dr["UserName"]);
            model.UserCode = DBHelper.GetString(dr["UserCode"]);
            model.UserCard = DBHelper.GetString(dr["UserCard"]);
            //model.Password = DBHelper.GetString(dr["Password"]);
            model.Email = DBHelper.GetString(dr["Email"]);
            model.TrueName = DBHelper.GetString(dr["TrueName"]);
            model.Mobile = DBHelper.GetString(dr["Mobile"]);
            model.Tel = DBHelper.GetString(dr["Tel"]);
            model.CountryID = DBHelper.GetInt(dr["CountryID"]);
            model.Sex = DBHelper.GetIntByNull(dr["Sex"]);
            model.Birthday = DBHelper.GetString(dr["Birthday"]);
            model.IsClose = DBHelper.GetBool(dr["IsClose"]);
            model.Avatar = DBHelper.GetString(dr["Avatar"]);
            model.TypeID = DBHelper.GetIntByNull(dr["TypeID"]);
            model.DepartmentID = DBHelper.GetIntByNull(dr["DepartmentID"]);
            model.Credit = DBHelper.GetDecimal(dr["Credit"]);
            model.Freeze = DBHelper.GetDecimal(dr["Freeze"]);
            model.RegTime = DBHelper.GetDateTime(dr["RegTime"]);
            model.LoginTime = DBHelper.GetDateTime(dr["LoginTime"]);
            model.LoginIP = DBHelper.GetString(dr["LoginIP"]);
            model.LoginNum = DBHelper.GetInt(dr["LoginNum"]);
            model.LinkMan = DBHelper.GetString(dr["LinkMan"]);
            model.DayNum = DBHelper.GetInt(dr["DayNum"]);
            return model;
        }
        #endregion
        #region 前台
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public int UpdatePass(int _id, string _oldpassword, string _password)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update t_User set ");
            builder.Append("Password=@Password where ");
            builder.Append("ID=@ID and Password=@OldPassword");
            DbParameter[] cmdParms = new DbParameter[] { 
                DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id), 
                DBHelper.CreateInDbParameter("@OldPassword", DbType.String, 50, _oldpassword), 
                DBHelper.CreateInDbParameter("@Password", DbType.String, 50, _password) };
            return DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
        public int UpdateMobile(int _id, string _oldmobile, string _mobile)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update t_User set ");
            builder.Append("mobile=@mobile where ");
            builder.Append("ID=@ID and mobile=@Oldmobile");
            DbParameter[] cmdParms = new DbParameter[] { 
                DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id), 
                DBHelper.CreateInDbParameter("@Oldmobile", DbType.String, 50, _oldmobile), 
                DBHelper.CreateInDbParameter("@mobile", DbType.String, 50, _mobile) };
            return DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
        public t_User GetForget(string _email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append("ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum ");
            strSql.Append(" FROM t_User where Email=@Email ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DBHelper.CreateInDbParameter("@Email", DbType.String, 50, _email) };
            t_User model = null;
            using (DbDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            model = new t_User();
                            GetItem(model, reader);
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
            return model;
        }
        #endregion

        public DataTable GetList(int pageIndex, int pageSize, ref int records, string type, string keyword, int provinceID,
           int countryid, int deptid, string mobile, int close)
        {
            string query = string.Empty;
            string strDeptSql = string.Empty;
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            query += string.Empty;
            if (!string.IsNullOrEmpty(keyword))
            {
                query += " and (TrueName like '%'+@keyword+'%' or UserCode like '%'+@keyword+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@keyword", DbType.String, keyword));
            }
            if (provinceID > 0 && countryid > 0)
            {
                query += " and CountryID=@countryid ";
                para.Add(DBHelper.CreateInDbParameter("@countryid", DbType.String, countryid));
            }
            else if (provinceID > 0 && countryid == 0)
            {
                query += " and CountryID in (select id from sys_area where parentid=@provinceID) ";
                para.Add(DBHelper.CreateInDbParameter("@provinceID", DbType.String, provinceID));
            }
            if (deptid > 0)
            {
                strDeptSql += "declare @lft int \r\n";
                strDeptSql += "declare @rgt int \r\n";
                strDeptSql += "select @lft=Lft,@rgt=Rgt from d_Department where ID=@DepartmentID;\r\n";
                query += " and DepartmentID in (select ID from d_Department where Lft>=@lft AND Rgt<=@rgt) ";
                para.Add(DBHelper.CreateInDbParameter("@DepartmentID", DbType.String, deptid));
            }
            if (!string.IsNullOrEmpty(type))
            {
                string tempquery = string.Empty;
                foreach (string item in type.Split(','))
                    tempquery += string.Format("or type='{0}' ", item);
                query += string.Format(" and ({0})", tempquery.Substring(2));
            }
            if (!string.IsNullOrEmpty(mobile))
            {
                query += " and mobile=@mobile ";
                para.Add(DBHelper.CreateInDbParameter("@mobile", DbType.String, mobile));
            }
            if (close >= 0)
                query += string.Format(" and isclose={0} ", close);
            DbParameter[] cmdParms = para.ToArray();
            string sql = @"select u.ID,UserName,UserCode,UserCard,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,
                                Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum
                                ,d.Name as DeptName,ut.Name as TypeName{0}
                            from t_User as u
	                        left join d_Department as d on d.ID=u.DepartmentID
	                        left join t_UserType as ut on ut.ID=u.TypeID
	                        where 1=1 {1}";
            string strSql = string.Format(@"{1}select count(1) from ({0}) as temptable", string.Format(sql, "", query), strDeptSql);
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            DataTable dt = new DataTable();
            if (obj != null && !obj.Equals(0))
            {
                records = int.Parse(obj.ToString());
                strSql = string.Format(@"{1}SELECT * FROM ({0}) as temptable WHERE rowNum BETWEEN @PageIndex and @PageSize",
                                        string.Format(sql, ",ROW_NUMBER() Over(order by u.ID desc) as rowNum", query), strDeptSql);
                dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            }
            return dt;
        }
        public List<t_User> GetList(int? deptID, string type, Confine path)
        {
            StringBuilder strSql = new StringBuilder();
            if (path == Confine.Company || path == Confine.Dept)
            {
                strSql.Append("declare @lft int \r\n");
                strSql.Append("declare @rgt int \r\n");
                strSql.Append("select @lft=Lft,@rgt=Rgt from d_Department where ID=@DepartmentID;\r\n");
            }
            strSql.Append("SELECT ");
            strSql.Append("u.ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum FROM t_User as u ");
            if (!string.IsNullOrEmpty(type))
                strSql.Append(" left join t_UserType as ut on ut.ID=u.TypeID ");
            strSql.Append(" where 1=1");
            if (deptID.HasValue)
            {
                if (path == Confine.Company || path == Confine.Dept)
                    strSql.Append(" and DepartmentID in (select ID from d_Department where Lft>=@lft AND Rgt<=@rgt)");
                else
                    strSql.Append(" and DepartmentID=@DepartmentID");
            }
            if (!string.IsNullOrEmpty(type))
            {
                string query = string.Empty;
                foreach (string item in type.Split(','))
                    query += string.Format("or type='{0}' ", item);
                strSql.AppendFormat("and ({0})", query.Substring(2));
            }
            DbParameter[] cmdParms = {
				DBHelper.CreateInDbParameter("@DepartmentID", DbType.Int32, deptID)};

            List<t_User> list = new List<t_User>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new t_User(), dr));
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
        public List<t_User> GetList(string type, string keyword, int departmentID)
        {
            List<t_User> list = new List<t_User>();
            StringBuilder strSql = new StringBuilder();
            if (departmentID > 0)
            {
                strSql.Append("declare @lft int \r\n");
                strSql.Append("declare @rgt int \r\n");
                strSql.Append("select @lft=Lft,@rgt=Rgt from d_Department where ID=@DepartmentID;\r\n");
            }
            strSql.Append("SELECT ");
            strSql.Append("u.ID,UserName,UserCode,UserCard,Password,Email,TrueName,Mobile,Tel,CountryID,Sex,Birthday,IsClose,");
            strSql.Append("Avatar,TypeID,DepartmentID,Credit,Freeze,RegTime,LoginTime,LoginIP,LoginNum,LinkMan,DayNum FROM t_User as u ");
            if (!string.IsNullOrEmpty(type))
                strSql.Append("left join t_UserType as ut on ut.ID=u.TypeID ");
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(keyword))
                strSql.Append(" and (TrueName like '%'+@keyword+'%' or UserCode like '%'+@keyword+'%')");
            else
                return list;
            if (departmentID > 0)
                strSql.Append(" and DepartmentID in (select ID from d_Department where Lft>=@lft AND Rgt<=@rgt)");
            if (!string.IsNullOrEmpty(type))
            {
                string query = string.Empty;
                foreach (string item in type.Split(','))
                    query += string.Format("or type='{0}' ", item);
                strSql.AppendFormat("and ({0})", query.Substring(2));
            }
            DbParameter[] cmdParms = {
				DBHelper.CreateInDbParameter("@keyword", DbType.String, keyword),
				DBHelper.CreateInDbParameter("@DepartmentID", DbType.Int32, departmentID)};
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new t_User(), dr));
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
            }
            return list;
        }
    }
}
