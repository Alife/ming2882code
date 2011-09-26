//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:35:48
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
	/// 实体类 sys_Field，
	/// </summary>
	[Serializable]
	public class sys_Field : ICloneable
	{
		public sys_Field()
		{ }

		/// <summary>
		/// 构造函数 sys_Field
		/// </summary>
		/// <param name="iD">功能权限可控字段ID</param>
		/// <param name="fieldName">字段名称</param>
		/// <param name="field">字段</param>
		/// <param name="operationID">功能操作ID</param>
		public sys_Field(int iD, string fieldName, string field, int operationID)
		{
			_iD = iD;
			_fieldName = fieldName;
			_field = field;
			_operationID = operationID;
		}

		#region 实体属性

		private int _iD;
		private string _fieldName;
		private string _field;
		private int _operationID;

		/// <summary>
		/// 功能权限可控字段ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// 字段名称
		/// </summary>
		public string FieldName
		{
			set { _fieldName = value; }
			get { return _fieldName; }
		}

		/// <summary>
		/// 字段
		/// </summary>
		public string Field
		{
			set { _field = value; }
			get { return _field; }
		}

		/// <summary>
		/// 功能操作ID
		/// </summary>
		public int OperationID
		{
			set { _operationID = value; }
			get { return _operationID; }
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
