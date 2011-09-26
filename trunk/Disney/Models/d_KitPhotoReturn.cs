using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitPhotoReturn
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 档图ID
        /// </summary>
        public int KitPhotoID { get; set; }

        /// <summary>
        /// 提交人ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public int KitClassID { get; set; }

        /// <summary>
        /// 小朋友ID
        /// </summary>
        public int? KitChildID { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 问题内容
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 问题类型:1仅本张，2男生同服装，3女生同服装，4同服装
        /// </summary>
        public int? QuestionType { get; set; }

        /// <summary>
        /// 童话世界
        /// </summary>
        public string Tw { get; set; }
        #endregion
    }
}
