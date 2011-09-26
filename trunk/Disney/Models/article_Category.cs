//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:33:17
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
	/// 实体类 article_Category，
	/// </summary>
	[Serializable]
	public class ArticleCategory : ICloneable
	{
		public ArticleCategory()
		{ }

        #region 实体属性

		private int _iD;
		private string _category;
		private string _code;
		private int _parentID;
		private int _path;
		private int _orderID;
		private string _description;
		private string _metaKeywords;
		private string _metaDescription;
		private string _readCategory;

		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// 分类名称
		/// </summary>
		public string Category
		{
			set { _category = value; }
			get { return _category; }
		}

		/// <summary>
		/// 分类编号
		/// </summary>
		public string Code
		{
			set { _code = value; }
			get { return _code; }
		}

		/// <summary>
		/// 父类ID
		/// </summary>
		public int ParentID
		{
			set { _parentID = value; }
			get { return _parentID; }
		}

		/// <summary>
		/// 阶层
		/// </summary>
		public int Path
		{
			set { _path = value; }
			get { return _path; }
		}

		/// <summary>
		/// 排序
		/// </summary>
		public int OrderID
		{
			set { _orderID = value; }
			get { return _orderID; }
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
		/// 关键字
		/// </summary>
		public string MetaKeywords
		{
			set { _metaKeywords = value; }
			get { return _metaKeywords; }
		}

		/// <summary>
		/// 内容
		/// </summary>
		public string MetaDescription
		{
			set { _metaDescription = value; }
			get { return _metaDescription; }
		}

		/// <summary>
		/// 读取分类
		/// </summary>
		public string ReadCategory
		{
			set { _readCategory = value; }
			get { return _readCategory; }
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
