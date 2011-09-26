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
    public class ReportData : DALHelper
    {
        #region 查询月结单明显
        public DataTable totolorderdetail(int totolid, string arter, string beginTime, string endTime)
        {
            string query = string.Format("where kw.State={0}", (int)KitPhotoState.MonthEnd);
            List<DbParameter> para = new List<DbParameter>();
            if (totolid > 0)
            {
                query += " and kw.TotolMonthID=@totolid ";
                para.Add(DBHelper.CreateInDbParameter("@totolid", DbType.Int32, totolid));
            }
            if (!string.IsNullOrEmpty(arter))
            {
                query += " and u.UserCode=@arter ";
                para.Add(DBHelper.CreateInDbParameter("@arter", DbType.String, arter));
            }
            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
            {
                query += " AND kw.FinishTime BETWEEN @beginTime AND @endTime ";
                para.Add(DBHelper.CreateInDbParameter("@beginTime", DbType.DateTime, DateTime.Parse(beginTime)));
                para.Add(DBHelper.CreateInDbParameter("@endTime", DbType.DateTime, Convert.ToDateTime(endTime).AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            string strSql = string.Format(@"select *,Robe+GroupPhoto+Life+
                                            cast(cast((ClassmatesTeacher+ClassmatesPeopleNum+Cover+Head)as decimal(38,2))/2 as decimal(38,2))
                                             as PhotoNum from(select k.Code+k.Custom as Custom,Strength,
                                            sum(case Category when '4' then kp.PhotoNum else 0 end) as 'Robe',
                                            sum(case Category when '2' then kp.PhotoNum else 0 end) as 'GroupPhoto',
                                            sum(case Category when '5' then kp.PhotoNum else 0 end) as 'Life',
                                            sum(case Category when '3' then kp.PhotoNum else 0 end) as 'Classmates',
                                            sum(case Category when '3' then kp.TeacherNum else 0 end) as 'ClassmatesTeacher',
                                            sum(case when(Category=3 and kpt.ID!=3) then kp.PeopleNum 
	                                            when(Category=3 and kpt.ID=3) then kp.PeopleNum*2
	                                            else 0 end) as 'ClassmatesPeopleNum',
                                            sum(case Category when '1' then kp.PhotoNum else 0 end) as 'Cover',
                                            sum(case Category when '6' then kp.PhotoNum else 0 end) as 'Head',
                                            sum(Amount)as Amount,
                                            sum(Amt)as Amt
                                            from d_KitPhoto as kp
                                            inner join d_KitWork as kw on kw.ID=kp.KitWorkID
                                            inner join view_d_Kit as k on kw.KitID=k.ID
                                            inner join t_User as u on u.ID=kp.ArterID
                                            inner join d_KitPhotoType as kpt on kpt.ID=kp.KitPhotoTypeID
                                            {0}
                                            group by k.Code,k.Custom,k.Strength)as t
                                            order by Custom desc", query);
            return DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
        }
        #endregion
        #region 完成明显
        public DataTable finishtotol(string arter, string beginTime, string endTime)
        {
            string query = string.Format("where kw.TotolMonthID is null and kw.State in({0},{1})", (int)KitPhotoState.End, (int)KitPhotoState.Uploaded);
            List<DbParameter> para = new List<DbParameter>();
            if (!string.IsNullOrEmpty(arter))
            {
                query += " and u.UserCode=@arter ";
                para.Add(DBHelper.CreateInDbParameter("@arter", DbType.String, arter));
            }
            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
            {
                query += " AND kw.FinishTime BETWEEN @beginTime AND @endTime ";
                para.Add(DBHelper.CreateInDbParameter("@beginTime", DbType.DateTime, DateTime.Parse(beginTime)));
                para.Add(DBHelper.CreateInDbParameter("@endTime", DbType.DateTime, Convert.ToDateTime(endTime).AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            string strSql = string.Format(@"select *,Robe+GroupPhoto+Life+
                                            cast(cast((ClassmatesTeacher+ClassmatesPeopleNum+Cover+Head)as decimal(38,2))/2 as decimal(38,2))
                                             as PhotoNum from(select k.Code+k.Custom as Custom,Strength,
                                            sum(case Category when '4' then kp.PhotoNum else 0 end) as 'Robe',
                                            sum(case Category when '2' then kp.PhotoNum else 0 end) as 'GroupPhoto',
                                            sum(case Category when '5' then kp.PhotoNum else 0 end) as 'Life',
                                            sum(case Category when '3' then kp.PhotoNum else 0 end) as 'Classmates',
                                            sum(case Category when '3' then kp.TeacherNum else 0 end) as 'ClassmatesTeacher',
                                            sum(case when(Category=3 and kpt.ID!=3) then kp.PeopleNum 
	                                            when(Category=3 and kpt.ID=3) then kp.PeopleNum*2
	                                            else 0 end) as 'ClassmatesPeopleNum',
                                            sum(case Category when '1' then kp.PhotoNum else 0 end) as 'Cover',
                                            sum(case Category when '6' then kp.PhotoNum else 0 end) as 'Head',
                                            sum(Amount)as Amount,
                                            sum(Amt)as Amt
                                            from d_KitPhoto as kp
                                            inner join d_KitWork as kw on kw.ID=kp.KitWorkID
                                            inner join view_d_Kit as k on kw.KitID=k.ID
                                            inner join t_User as u on u.ID=kp.ArterID
                                            inner join d_KitPhotoType as kpt on kpt.ID=kp.KitPhotoTypeID
                                            {0}
                                            group by k.Code,k.Custom,k.Strength)as t
                                            order by Custom desc", query);
            return DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
        }
        #endregion
        #region 美工月结
        public DataTable ArterMonth(int totolid, string arter, string beginTime, string endTime)
        {
            string query = string.Format("where kw.State={0}", (int)KitPhotoState.MonthEnd);
            List<DbParameter> para = new List<DbParameter>();
            if (totolid > 0)
            {
                query += " and kw.TotolMonthID=@totolid ";
                para.Add(DBHelper.CreateInDbParameter("@totolid", DbType.Int32, totolid));
            }
            if (!string.IsNullOrEmpty(arter))
            {
                query += " and u.UserCode=@arter ";
                para.Add(DBHelper.CreateInDbParameter("@arter", DbType.String, arter));
            }
            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
            {
                query += " AND BalanceTime BETWEEN @beginTime AND @endTime ";
                para.Add(DBHelper.CreateInDbParameter("@beginTime", DbType.DateTime, DateTime.Parse(beginTime)));
                para.Add(DBHelper.CreateInDbParameter("@endTime", DbType.DateTime, Convert.ToDateTime(endTime).AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            string strSql = string.Format(@"select *,Robe+GroupPhoto+Life+
                                            cast(cast((ClassmatesTeacher+ClassmatesPeopleNum+Cover+Head)as decimal(38,2))/2 as decimal(38,2))
                                             as PhotoNum from(select u.TrueName,
                                        sum(case Category when '4' then kp.PhotoNum else 0 end) as 'Robe',
                                        sum(case Category when '2' then kp.PhotoNum else 0 end) as 'GroupPhoto',
                                        sum(case Category when '5' then kp.PhotoNum else 0 end) as 'Life',
                                        sum(case Category when '3' then kp.PhotoNum else 0 end) as 'Classmates',
                                        sum(case Category when '3' then kp.TeacherNum else 0 end) as 'ClassmatesTeacher',
                                            sum(case when(Category=3 and kpt.ID!=3) then kp.PeopleNum 
	                                            when(Category=3 and kpt.ID=3) then kp.PeopleNum*2
	                                            else 0 end) as 'ClassmatesPeopleNum',
                                        sum(case Category when '1' then kp.PhotoNum else 0 end) as 'Cover',
                                        sum(case Category when '6' then kp.PhotoNum else 0 end) as 'Head',
                                        sum(Amount)as Amount
                                        from d_KitPhoto as kp
                                        inner join d_KitWork as kw on kw.ID=kp.KitWorkID
                                        inner join t_User as u on u.ID=kp.ArterID
                                        inner join d_KitPhotoType as kpt on kpt.ID=kp.KitPhotoTypeID
                                        {0}
                                        group by u.TrueName)as t", query);
            DataTable dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            
            return dt;
        }
        #endregion
        public int GetMainTotol(string type, string userid)
        {
            string strSql = string.Empty;
            if (type == "proof")//校图
            {
                strSql = string.Format(@"select count(1) from d_KitWork as kw 
                                left join d_Kit as k on k.ID=kw.KitID 
                                where proofState in({0},{1}) and kw.state in({2},{3}) {4}"
                    , (int)KitProofState.UnProof, (int)KitProofState.Deal, (int)KitPhotoState.End, (int)KitPhotoState.MonthEnd 
                    , string.IsNullOrEmpty(userid) ? "" : string.Format(" and CustomID in({0})", userid));
            }
            else if (type == "editphoto")//修图
            {
                strSql = string.Format(@"select count(1) from d_KitWork where proofState={0} and state in({1},{2}){3}"
                    , (int)KitProofState.Finish, (int)KitPhotoState.End, (int)KitPhotoState.MonthEnd 
                    , string.IsNullOrEmpty(userid) ? "" : string.Format(" and ArterID in({0})", userid));
            }
            else if (type == "worker")//工作单
            {
                strSql = string.Format(@"select count(1) from d_KitWork where state={0}{1}"
                    , (int)KitPhotoState.Process, string.IsNullOrEmpty(userid) ? "" : string.Format(" and ArterID in({0})", userid));
            }
            else if (type == "kit")//制程单
            {
                strSql = string.Format(@"select count(1) from d_Kit where state in({0},{1}){2}"
                    , (int)KitState.Stock, (int)KitState.Process, string.IsNullOrEmpty(userid) ? "" : string.Format(" and UserID in({0})", userid));
            }
            else if (type == "kitend")//完成制程
            {
                strSql = string.Format(@"select count(1) from d_KitWork where state in({0},{1}){2}"
                    , (int)KitPhotoState.End, (int)KitPhotoState.MonthEnd, string.IsNullOrEmpty(userid) ? "" : string.Format(" and ArterID in({0})", userid));
            }
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), null);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        #region 工作单统计
        public DataTable workordertotol()
        {
            string strSql = string.Format(@"select *,Robe+GroupPhoto+Life+
                                            cast(cast((ClassmatesTeacher+ClassmatesPeopleNum+Cover+Head)as decimal(38,2))/2 as decimal(38,2))
                                             as PhotoNum from(select k.Code,k.Strength,
                                            sum(case Category when '4' then kp.PhotoNum else 0 end) as 'Robe',
                                            sum(case Category when '2' then kp.PhotoNum else 0 end) as 'GroupPhoto',
                                            sum(case Category when '5' then kp.PhotoNum else 0 end) as 'Life',
                                            sum(case Category when '3' then kp.PhotoNum else 0 end) as 'Classmates',
                                            sum(case Category when '3' then kp.TeacherNum else 0 end) as 'ClassmatesTeacher',
                                                sum(case when(Category=3 and kpt.ID!=3) then kp.PeopleNum 
	                                                when(Category=3 and kpt.ID=3) then kp.PeopleNum*2
	                                                else 0 end) as 'ClassmatesPeopleNum',
                                            sum(case Category when '1' then kp.PhotoNum else 0 end) as 'Cover',
                                            sum(case Category when '6' then kp.PhotoNum else 0 end) as 'Head'
                                            from d_KitPhoto as kp
                                            inner join d_KitWork as kw on kw.ID=kp.KitWorkID
                                            inner join view_d_Kit as k on kw.KitID=k.ID
                                            inner join t_User as u on u.ID=kp.ArterID
                                            inner join d_KitPhotoType as kpt on kpt.ID=kp.KitPhotoTypeID
                                            {0}
                                            group by k.Code,k.Custom,k.Strength)as t
                                            order by Code", "where kw.State=1");
            return DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), null).Tables[0];
        }
        #endregion
    }
}
