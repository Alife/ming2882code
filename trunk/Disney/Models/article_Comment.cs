//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:33:32
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace Models
{
	/// <summary>
	/// 实体类 article_Comment，
	/// </summary>
	[Serializable]
	public class ArticleComment : ICloneable
	{
		public ArticleComment()
		{ }

		#region 实体属性

		private int _iD;
		private int _userID;
		private int _articleID;
		private int _parentID;
		private string _content;
		private DateTime _createTime;

		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// UserID
		/// </summary>
		public int UserID
		{
			set { _userID = value; }
			get { return _userID; }
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
		/// ParentID
		/// </summary>
		public int ParentID
		{
			set { _parentID = value; }
			get { return _parentID; }
		}

		/// <summary>
		/// Content
		/// </summary>
		public string Content
		{
			set { _content = value; }
			get { return _content; }
		}

		/// <summary>
		/// CreateTime
		/// </summary>
		public DateTime CreateTime
		{
			set { _createTime = value; }
			get { return _createTime; }
		}

        #endregion
        private int[] _dots = new int[] { 0, 0 };
        public int[] Dots
        {
            get { return _dots; }
            set { _dots = value; }
        }

		#region ICloneable 成员

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
    }
    public class ArticleCommentList : CollectionBase
    {
        private int _recordCount;

        public int Add(ArticleComment value)
        {
            return base.List.Add(value);
        }

        public ArticleComment this[int index]
        {
            get { return (ArticleComment)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
}
