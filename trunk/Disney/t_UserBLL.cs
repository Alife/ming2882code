using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Models.Enums;
using Common;
using System.Data;

namespace BLL
{
    public class t_UserBLL : BaseObject
    {
        #region 共用
        public static t_User BaseUser
        {
            get
            {
                if (Request.Cookies["user"] == null || string.IsNullOrEmpty(Request.Cookies["user"].Value))
                    return null;
                t_User user = null;
                try
                {
                    user = (t_User)SerializeDeserialize.DeserializeObject(DESEncrypt.Decrypt(Request.Cookies["user"].Value));
                }
                catch
                {
                }
                return user;
            }
        }
        public static int Insert(t_User item)
        {
            int num = DataFactory.t_UserData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("t_User");
            return num;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, string type, string keyword, int provinceID,
           int countryid, int deptid, string mobile, int close)
        {
            string key = string.Format("t_User-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}",
                pageIndex, pageSize, records, type, keyword, provinceID, countryid, deptid, mobile, close);
            DataTable data = null;
            if (BaseObject.Cache[key] != null)
            {
                records = (int)BaseObject.Cache[key + "records"];
                data = (DataTable)BaseObject.Cache[key];
            }
            else
            {
                data = DataFactory.t_UserData().GetList(pageIndex, pageSize, ref records, type, keyword, provinceID, countryid, deptid, mobile, close);
                BaseObject.CacheData(key, data);
                BaseObject.CacheData(key + "records", records);
            }
            return data;

        }

        public static int Update(t_User item)
        {
            int num = DataFactory.t_UserData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("t_User");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.t_UserData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("t_User");
            return num;
        }
        public static bool IsMobileExists(string _mobile)
        {
            return DataFactory.t_UserData().IsMobileExists(_mobile);
        }
        public static bool IsUserNameExists(string _username)
        {
            return DataFactory.t_UserData().IsUserNameExists(_username);
        }
        public static bool IsEmailExists(string _mobile)
        {
            return DataFactory.t_UserData().IsEmailExists(_mobile);
        }
        /// <summary>
        /// 用户登录时用
        /// </summary>
        /// <param name="_password"></param>
        /// <param name="_lastLoginIP"></param>
        /// <returns></returns>
        public static t_User GetUserLogin(string _loginId, int _logintype, string _password)
        {
            return DataFactory.t_UserData().GetUserLogin(_loginId, _logintype, _password, Request.UserHostAddress);
        }
        public static t_User GetItem(string userCode)
        {
            string key = "t_User-" + userCode;
            t_User data = null;
            if (Cache[key] != null)
                data = (t_User)Cache[key];
            else
            {
                data = DataFactory.t_UserData().GetItem(userCode);
                CacheData(key, data);
            }
            return data;
        }
        public static t_User GetItem(int ID)
        {
            string key = "t_User-" + ID;
            t_User data = null;
            if (Cache[key] != null)
                data = (t_User)Cache[key];
            else
            {
                data = DataFactory.t_UserData().GetItem(ID);
                CacheData(key, data);
            }
            return data;
        }
        public static t_User GetByCard(string usercard)
        {
            string key = "t_User-c-" + usercard;
            t_User data = null;
            if (Cache[key] != null)
                data = (t_User)Cache[key];
            else
            {
                data = DataFactory.t_UserData().GetByCard(usercard);
                CacheData(key, data);
            }
            return data;
        }
        public static t_User GetByMobile(string mobile)
        {
            string key = "t_User-m-" + mobile;
            t_User data = null;
            if (Cache[key] != null)
                data = (t_User)Cache[key];
            else
            {
                data = DataFactory.t_UserData().GetByMobile(mobile);
                CacheData(key, data);
            }
            return data;
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
        public static int UpdatePass(int uid, string _oldpassword, string _password)
        {
            int num = DataFactory.t_UserData().UpdatePass(uid, _oldpassword, _password);
            if (num > 0)
                BaseObject.CacheRemove("hip_t_User" + uid);
            return num;
        }
        public static int UpdateMobile(int _id, string _oldmobile, string _mobile)
        {
            int num = DataFactory.t_UserData().UpdateMobile(_id, _oldmobile, _mobile);
            if (num > 0)
                BaseObject.CacheRemove("hip_t_User" + _id);
            return num;
        }
        public static t_User GetForget(string _email)
        {
            return DataFactory.t_UserData().GetForget(_email);
        }
        #endregion
        public static List<t_User> GetList(int? deptID, string type, Confine path)
        {
            string key = string.Format("t_User-{0}-{1}-{2}", deptID, type, path.ToString());
            List<t_User> data = null;
            if (Cache[key] != null)
                data = (List<t_User>)Cache[key];
            else
            {
                data = DataFactory.t_UserData().GetList(deptID, type, path);
                CacheData(key, data);
            }
            return data;
        }
        public static List<t_User> GetList(string type, string keyword, int departmentID)
        {
            return DataFactory.t_UserData().GetList(type, keyword, departmentID);
        }
    }
}
