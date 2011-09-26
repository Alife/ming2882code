using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    [Serializable]
    public class ArticleTop : ICloneable
    {
        public ArticleTop()
        {
        }

        #region 实体属性

        private int _iD;
        private int _articleID;
        private string _title;
        private string _intro;
        private string _url;
        private string _filePath;

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
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Intro
        {
            set { _intro = value; }
            get { return _intro; }
        }

        /// <summary>
        /// 连接Url
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }

        public string FilePath
        {
            set { _filePath = value; }
            get { return _filePath; }
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
