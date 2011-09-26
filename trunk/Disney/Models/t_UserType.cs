using System;
using System.Collections.Generic;

namespace Models
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class t_UserType
	{
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户类型：1超级管理员Admin,2美工Artist,3摄影师Cameraman,3普通會員，4VIP會員,5應聘人員
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        #endregion
    }
}
