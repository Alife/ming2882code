using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class w_Photo
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 分類ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件路徑
        /// </summary>
        public string FilePath { get; set; }
        #endregion
    }
    public class w_PhotoList
    {
        public w_PhotoList() { data = new List<w_Photo>(); }

        public List<w_Photo> data { get; set; }
        public int records { get; set; }
    }
}
