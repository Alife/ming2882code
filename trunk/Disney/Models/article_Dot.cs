//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:33:40
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
	/// 实体类 article_Dot，
	/// </summary>
	[Serializable]
	public class ArticleDot : ICloneable
	{
		public ArticleDot()
		{ }

		/// <summary>
		/// 构造函数 article_Dot
		/// </summary>
		/// <param name="iD">ID</param>
		/// <param name="commentID">CommentID</param>
		/// <param name="userID">UserID</param>
		/// <param name="dot">1赞同，2反对</param>
        public ArticleDot(int iD, int commentID, int userID, int dot)
		{
			_iD = iD;
			_commentID = commentID;
			_userID = userID;
			_dot = dot;
		}

		#region 实体属性

		private int _iD;
		private int _commentID;
		private int _userID;
		private int _dot;

		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// CommentID
		/// </summary>
		public int CommentID
		{
			set { _commentID = value; }
			get { return _commentID; }
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
		/// 1赞同，2反对
		/// </summary>
		public int Dot
		{
			set { _dot = value; }
			get { return _dot; }
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
