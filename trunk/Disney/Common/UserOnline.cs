using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class OnlineManage
    {
        private static OnlineManage _instance = new OnlineManage();
        //private Dictionary<int, OnlineUserInfo> _onlineUserList = new Dictionary<int, OnlineUserInfo>();
        private Dictionary<string, OnlineUserInfo> _onlineSession = new Dictionary<string, OnlineUserInfo>();

        public const int Timeout_Minutes = 20;  //非登录会员在线用户过期时间
        public const int Timeout_Minutes_Member = 40; //登录会员在线过期时间

        private OnlineManage()
        {
        }

        public void Clear()
        {
            lock (this)
            {
                //_onlineUserList.Clear();
                _onlineSession.Clear();
            }
        }

        /// <summary>
        /// 获取当前在线用户总数(包含登录和未登录的)
        /// </summary>
        public int Count
        {
            get { return _onlineSession.Count; }
        }

        /// <summary>
        /// 在线会员数
        /// </summary>
        public int MemberCount
        {
            get
            {
                int iRet = 0;
                lock (this)
                {
                    foreach (string _sKey in _onlineSession.Keys)
                    {
                        OnlineUserInfo oInfo = _onlineSession[_sKey];
                        if (oInfo == null)
                        {
                            _onlineSession.Remove(_sKey);
                        }
                        else
                        {
                            if (oInfo.UserID > 0)
                            {
                                iRet++;
                            }
                        }
                    }
                }
                return iRet;
            }
        }

        public OnlineUserInfo this[int UserID]
        {
            get
            {
                lock (this)
                {
                    foreach (string _sKey in _onlineSession.Keys)
                    {
                        OnlineUserInfo oInfo = _onlineSession[_sKey];
                        if (oInfo == null)
                        {
                            _onlineSession.Remove(_sKey);
                        }
                        else
                        {
                            if (oInfo.UserID == UserID)
                            {
                                return oInfo;
                            }
                        }
                    }
                    return null;
                }
            }
        }

        public OnlineUserInfo this[string sSessionID]
        {
            get
            {
                if (_onlineSession.ContainsKey(sSessionID))
                    return _onlineSession[sSessionID];
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取所有当前在线用户的ID数组
        /// </summary>
        public int[] OnlineUserIDs
        {
            get
            {
                lock (this)
                {
                    List<int> ids = new List<int>();
                    foreach (string _sKey in _onlineSession.Keys)
                    {
                        OnlineUserInfo oInfo = this[_sKey];
                        if (oInfo != null && oInfo.UserID > 0)
                        {
                            ids.Add(oInfo.UserID);
                        }
                    }
                    return ids.ToArray();
                }
            }
        }

        /// <summary>
        /// 用户有活动，存在异常情况：
        /// 1. Session未结束，但是换了用户登录
        /// </summary>
        /// <param name="UserID"></param>
        public void UserActived(string sSessionID, int iUserID)
        {
            lock (this)
            {
                OnlineUserInfo oInfo = this[sSessionID];
                if (oInfo == null)
                {
                    oInfo = new OnlineUserInfo(sSessionID, iUserID);
                    _onlineSession.Add(sSessionID, oInfo);
                }
                else
                {
                    if (oInfo.UserID != iUserID) //考虑同一个页面先后用别的帐号登录
                    {
                        oInfo.UserID = iUserID;
                        oInfo.LoginTime = DateTime.Now;
                    }
                    oInfo.LastActiveTime = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// 未登录
        /// </summary>
        /// <param name="sSessionID"></param>
        public void UserActived(string sSessionID)
        {
            lock (this)
            {
                OnlineUserInfo oInfo = this[sSessionID];
                if (oInfo == null)
                {
                    oInfo = new OnlineUserInfo(sSessionID);
                    _onlineSession.Add(sSessionID, oInfo);
                }
                else
                    oInfo.LastActiveTime = DateTime.Now;
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="sSessionID"></param>
        public void UserActived(int iUserID, string sSessionID)
        {
            lock (this)
            {
                OnlineUserInfo oInfo = this[sSessionID];
                if (oInfo == null)
                {
                    oInfo = new OnlineUserInfo(sSessionID, iUserID);
                    _onlineSession.Add(sSessionID, oInfo);
                }
                else
                {
                    oInfo.UserID = iUserID;
                    oInfo.LastActiveTime = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// 用户离开（未活动时间超时或者用户注销了）
        /// </summary>
        /// <param name="UserID"></param>
        public void UserLeaved(string sSessionID, int iUserID)
        {
            lock (this)
            {
                _onlineSession.Remove(sSessionID);
            }
        }

        /// <summary>
        /// 用户离开（未活动时间超时或者用户注销了）
        /// </summary>
        /// <param name="sSessionID"></param>
        public void UserLeaved(string sSessionID)
        {
            lock (this)
            {
                _onlineSession.Remove(sSessionID);
            }
        }


        /// <summary>
        /// 检查在线用户状态，如果发现有超时未活动的用户，将其删除
        /// </summary>
        public void CheckOnlineUsers()
        {
            lock (this)
            {
                //1.先查询出需要删除的Key列表
                List<string> _removeKeys = new List<string>();
                foreach (KeyValuePair<string, OnlineUserInfo> kvp in _onlineSession)
                {
                    OnlineUserInfo oInfo = kvp.Value;
                    string _sKey = kvp.Key;
                    if (oInfo == null)
                    {
                        _removeKeys.Add(_sKey);
                    }
                    else
                    {
                        if (oInfo.UserID > 0) //登录会员
                        {
                            if (oInfo.NoActiveMinutes > Timeout_Minutes_Member)
                            {
                                _removeKeys.Add(_sKey);
                            }
                        }
                        else //未登录会员
                        {
                            if (oInfo.NoActiveMinutes > Timeout_Minutes)
                            {
                                _removeKeys.Add(_sKey);
                            }
                        }
                    }
                }

                //2.从容器中删除这些Key
                foreach (string _key in _removeKeys)
                {
                    _onlineSession.Remove(_key);
                }
                _removeKeys.Clear();
            }
        }

        /// <summary>
        /// 获取唯一实例
        /// </summary>
        /// <returns></returns>
        public static OnlineManage GetInstance()
        {
            return _instance;
        }
    }

    /// <summary>
    /// 在线用户信息类
    /// </summary>
    public class OnlineUserInfo
    {
        public int UserID;
        public string SessionID;
        public DateTime LoginTime;
        public DateTime LastActiveTime;

        /// <summary>
        /// 构造函数，用于用户新登录时创建用户在线条目
        /// </summary>
        /// <param name="iUserID">活动的用户ID</param>
        /// <param name="dtLoginTime">用户活动时间</param>
        public OnlineUserInfo(int iUserID, DateTime dtLoginTime)
        {
            UserID = iUserID;
            SessionID = "";
            LoginTime = dtLoginTime;
            LastActiveTime = dtLoginTime;
        }

        /// <summary>
        /// 构造函数，用于用户新登录时创建用户在线条目
        /// </summary>
        /// <param name="iUserID">活动的用户ID</param>
        /// <param name="dtLoginTime">用户活动时间</param>
        public OnlineUserInfo(string sSessionID, int iUserID)
        {
            UserID = iUserID;
            LoginTime = DateTime.Now;
            LastActiveTime = DateTime.Now;
            SessionID = sSessionID;
        }

        /// <summary>
        /// 用户初次进来并创建了新Session
        /// </summary>
        /// <param name="sSessionID"></param>
        public OnlineUserInfo(string sSessionID)
        {
            UserID = 0;
            LoginTime = DateTime.Now;
            LastActiveTime = DateTime.Now;
            SessionID = sSessionID;
        }

        /// <summary>
        /// 获取该用户的未活动时长（分钟数）
        /// </summary>
        public int NoActiveMinutes
        {
            get
            {
                TimeSpan dtSpan = DateTime.Now.Subtract(LastActiveTime);
                return (int)dtSpan.TotalMinutes;
            }
        }

        /// <summary>
        /// 获取该用户的在线时长（分钟数）
        /// </summary>
        public int OnlineMinutes
        {
            get
            {
                TimeSpan dtSpan = DateTime.Now.Subtract(LoginTime);
                return (int)dtSpan.TotalMinutes;
            }
        }
    }
}
