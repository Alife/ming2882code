//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:33:49
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
	/// 实体类 article_File，
	/// </summary>
	[Serializable]
	public class ArticleFile : ICloneable
	{
		public ArticleFile()
		{ }

		#region 实体属性

		private int _iD;
		private int _articleID;
		private string _filePath;
		private string _fileType;
		private string _fileName;
		private decimal _fileSize;
		private bool _isTop;

		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// ArticleID
		/// </summary>
		public int ArticleID
		{
			set { _articleID = value; }
			get { return _articleID; }
		}

		/// <summary>
		/// FilePath
		/// </summary>
		public string FilePath
		{
			set { _filePath = value; }
			get { return _filePath; }
		}

		/// <summary>
		/// FileType
		/// </summary>
		public string FileType
		{
			set { _fileType = value; }
			get { return _fileType; }
		}

		/// <summary>
		/// FileName
		/// </summary>
		public string FileName
		{
			set { _fileName = value; }
			get { return _fileName; }
		}

		/// <summary>
		/// FileSize
		/// </summary>
		public decimal FileSize
		{
			set { _fileSize = value; }
			get { return _fileSize; }
		}

		/// <summary>
		/// IsTop
		/// </summary>
		public bool IsTop
		{
			set { _isTop = value; }
			get { return _isTop; }
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
