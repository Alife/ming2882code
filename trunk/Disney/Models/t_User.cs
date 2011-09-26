using System;
using System.Collections.Generic;

namespace Models
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class t_User
	{
        public t_User()
		{ }
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        
        /// <summary>
        /// 會員卡
        /// </summary>
        public string UserCard { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string TrueName { get; set; }
        
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        
        /// <summary>
        /// 所在城市ID
        /// </summary>
        public int CountryID { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        
        /// <summary>
        /// 出生时间
        /// </summary>
        public string Birthday { get; set; }
        
        /// <summary>
        /// 是否註銷
        /// </summary>
        public bool IsClose { get; set; }
        
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        
        /// <summary>
        /// 用户类型
        /// </summary>
        public int? TypeID { get; set; }
        
        /// <summary>
        /// 部门
        /// </summary>
        public int? DepartmentID { get; set; }
        
        /// <summary>
        /// 賬號余額
        /// </summary>
        public decimal Credit { get; set; }
        
        /// <summary>
        /// 凍結余額
        /// </summary>
        public decimal Freeze { get; set; }
        /// <summary>
        /// 註冊時間
        /// </summary>
        public DateTime RegTime { get; set; }
        
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        
        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIP { get; set; }
        
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginNum { get; set; }

        /// <summary>
        /// 联络人
        /// </summary>
        public string LinkMan { get; set; }
        /// <summary>
        /// 每天完成量
        /// </summary>
        public int DayNum { get; set; }
        #endregion
    }
    public class t_UserList
    {
        public t_UserList()
        {
            data = new List<t_User>();
        }
        public List<t_User> data
        {
            get;
            set;
        }

        public int records
        {
            get;
            set;
        }
    }
}
