//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:36:17
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
	/// 实体类 sys_Role，
	/// </summary>
	[Serializable]
	public class sys_Role : ICloneable
	{
		public sys_Role()
		{ }

		#region 实体属性

		private int _iD;
		private string _description;
		private string _roleName;

		/// <summary>
		/// 角色ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// 介绍
		/// </summary>
		public string Description
		{
			set { _description = value; }
			get { return _description; }
		}

		/// <summary>
		/// 角色名称
		/// </summary>
		public string RoleName
		{
			set { _roleName = value; }
			get { return _roleName; }
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
