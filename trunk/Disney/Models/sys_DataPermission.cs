//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:35:39
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;

namespace Models
{
	/// <summary>
	/// 实体类 sys_DataPermission，
	/// </summary>
	[Serializable]
	public class sys_DataPermission : ICloneable
	{
		public sys_DataPermission()
		{ }

		#region 实体属性

		private int _iD;
		private int _roleID;
        private int _confine;
        private int? _resourceID;
        private int _resourceType;

		/// <summary>
		/// 数据权限ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// 角色ID
		/// </summary>
		public int RoleID
		{
			set { _roleID = value; }
			get { return _roleID; }
		}

		/// <summary>
		/// 全局、公司、部门、下属机构(职位)、 拥有(用户)、自己
		/// </summary>
		public int Confine
		{
			set { _confine = value; }
			get { return _confine; }
        }

        /// <summary>
        /// 公司，部门，职务，用户
        /// </summary>
        public int? ResourceID
        {
            set { _resourceID = value; }
            get { return _resourceID; }
        }

        /// <summary>
        /// 资源类型
        /// </summary>
        public int ResourceType
        {
            set { _resourceType = value; }
            get { return _resourceType; }
        }

		#endregion

		#region ICloneable 成员

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}
