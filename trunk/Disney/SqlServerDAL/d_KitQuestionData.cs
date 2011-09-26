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
    public class d_KitQuestionData : DALHelper
    {
        public int Insert(d_KitQuestion model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitQuestion(");
            strSql.Append("KitWorkID,KitClassID,KitChildID,FileName,UserID,Intro,State,QuestionType,CreateTime,IntroTime,Remark,Tw)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_KitWorkID,@in_KitClassID,@in_KitChildID,@in_FileName,@in_UserID,@in_Intro,@in_State,@in_QuestionType,@in_CreateTime,@in_IntroTime,@in_Remark,@in_Tw)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitWorkID", DbType.Int32, model.KitWorkID),
                DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                DBHelper.CreateInDbParameter("@in_FileName", DbType.String, model.FileName),
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_Intro", DbType.String, model.Intro),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_QuestionType", DbType.Int32, model.QuestionType),
                DBHelper.CreateInDbParameter("@in_CreateTime", DbType.DateTime, model.CreateTime),
                DBHelper.CreateInDbParameter("@in_IntroTime", DbType.DateTime, model.IntroTime),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@in_Tw", DbType.String, model.Tw)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_KitQuestion model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitQuestion SET ");
            strSql.Append("KitWorkID=@in_KitWorkID,");
            strSql.Append("KitClassID=@in_KitClassID,");
            strSql.Append("KitChildID=@in_KitChildID,");
            strSql.Append("FileName=@in_FileName,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("Intro=@in_Intro,");
            strSql.Append("State=@in_State,");
            strSql.Append("QuestionType=@in_QuestionType,");
            strSql.Append("CreateTime=@in_CreateTime,");
            strSql.Append("IntroTime=@in_IntroTime,");
            strSql.Append("Remark=@in_Remark,");
            strSql.Append("Tw=@in_Tw");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitWorkID", DbType.Int32, model.KitWorkID),
                DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                DBHelper.CreateInDbParameter("@in_FileName", DbType.String, model.FileName),
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_Intro", DbType.String, model.Intro),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_QuestionType", DbType.Int32, model.QuestionType),
                DBHelper.CreateInDbParameter("@in_CreateTime", DbType.DateTime, model.CreateTime),
                DBHelper.CreateInDbParameter("@in_IntroTime", DbType.DateTime, model.IntroTime),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@in_Tw", DbType.String, model.Tw),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM d_KitQuestion WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }
        public int Delete(int workID)
        {
            string strSql = string.Format("DELETE FROM d_KitQuestion WHERE KitWorkID={0};\r\n", workID);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }
        public int Update(List<string> ids, int state, int kitWorkID)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                {
                    //if (state == (int)Models.Enums.KitQuestionState.Solve)
                    //    strSql.AppendFormat("DELETE FROM d_KitQuestion WHERE ID={0};\r\n", id);
                    //else
                        strSql.AppendFormat("update d_KitQuestion set state={1},IntroTime=getdate() WHERE ID={0};\r\n", id, state);
                }
                //if (state == (int)Models.Enums.KitQuestionState.Solve)
                //{
                //    strSql.AppendFormat("if not exists (select id from d_KitQuestion where KitWorkID={0}) \r\n", kitWorkID);
                //    strSql.Append("begin \r\n");
                //    strSql.AppendFormat("update d_kitwork set ProofState={1} WHERE ID={0};\r\n", kitWorkID, (int)Models.Enums.KitProofState.Proof);
                //    strSql.Append("end");
                //}
                if (state == (int)Models.Enums.KitQuestionState.Deal)
                {
                    strSql.AppendFormat("if not exists (select id from d_KitQuestion where KitWorkID={0} and State!={1}) \r\n", kitWorkID, (int)Models.Enums.KitQuestionState.Deal);
                    strSql.Append("begin \r\n");
                    strSql.AppendFormat("update d_kitwork set ProofState={1},ProofBeginTime=null,ProofEndTime=null,ProofTime=getdate() WHERE ID={0};\r\n", kitWorkID, (int)Models.Enums.KitProofState.Deal);
                    strSql.Append("end");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }
        public d_KitQuestion GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitQuestion ");
            strSql.Append(" WHERE ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@ID", DbType.Int32, ID)};
            d_KitQuestion item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public d_KitQuestion GetItem(int kitWorkID, int kitClassID, int? kitChildID, string fileName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitQuestion ");
            strSql.Append(" WHERE KitWorkID=@KitWorkID and KitClassID=@KitClassID and FileName=@FileName ");
            if (kitChildID.HasValue)
                strSql.Append(" and KitChildID=@KitChildID ");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@KitWorkID", DbType.Int32, kitWorkID),
				DBHelper.CreateInDbParameter("@KitClassID", DbType.Int32, kitClassID),
				DBHelper.CreateInDbParameter("@KitChildID", DbType.Int32, kitChildID),
				DBHelper.CreateInDbParameter("@FileName", DbType.String, fileName)};
            d_KitQuestion item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_KitQuestion> GetListByAll(int kitWorkID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitQuestion ");
            strSql.Append(" WHERE kitWorkID=@kitWorkID and State!=3 ");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@kitWorkID", DbType.Int32, kitWorkID)};
            List<d_KitQuestion> list = new List<d_KitQuestion>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                GetItem(list, dr);
            return list;
        }
        public DataTable GetList(int kitWorkID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT q.*,cl.Code as ClassCode,cl.Name as ClassName,ch.Code as ChildCode,ch.TrueName as ChildName FROM d_KitQuestion as q ");
            strSql.Append("left join d_KitClass as cl on cl.ID=q.KitClassID ");
            strSql.Append("left join d_KitChild as ch on ch.ID=q.KitChildID ");
            strSql.Append("left join t_User as u on u.ID=q.UserID ");
            strSql.Append(" WHERE q.KitWorkID=@KitWorkID");
            strSql.Append(" Order by cl.Code,ch.Code");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@KitWorkID", DbType.Int32, kitWorkID)};
            return DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
        }
        #region 私有
        private d_KitQuestion GetItem(d_KitQuestion model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitQuestion();
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
        private void GetModel(d_KitQuestion model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.KitWorkID = DBHelper.GetInt(dr["KitWorkID"]);
            model.KitClassID = DBHelper.GetInt(dr["KitClassID"]);
            model.KitChildID = DBHelper.GetIntByNull(dr["KitChildID"]);
            model.FileName = DBHelper.GetString(dr["FileName"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.Intro = DBHelper.GetString(dr["Intro"]);
            model.State = DBHelper.GetInt(dr["State"]);
            model.QuestionType = DBHelper.GetIntByNull(dr["QuestionType"]);
            model.CreateTime = DBHelper.GetDateTime(dr["CreateTime"]);
            model.IntroTime = DBHelper.GetDateTimeByNull(dr["IntroTime"]);
            model.Remark = DBHelper.GetString(dr["Remark"]);
            model.Tw = DBHelper.GetString(dr["Tw"]);
            model.IsPatch = DBHelper.GetBool(dr["IsPatch"]);
        }
        private List<d_KitQuestion> GetItem(List<d_KitQuestion> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitQuestion model = new d_KitQuestion();
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