using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitQuestion
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 工作单ID
        /// </summary>
        public int KitWorkID { get; set; }

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
        /// 提交人ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 问题内容
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 问题类型:1仅本张，2男生同服装，3女生同服装，4同服装
        /// </summary>
        public int? QuestionType { get; set; }
        /// <summary>
        /// 解决状态：1待解决，2已处理，3已解决
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 问题时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime? IntroTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 童话世界
        /// </summary>
        public string Tw { get; set; }
        /// <summary>
        /// 是否已补
        /// </summary>
        public bool IsPatch { get; set; }
        #endregion
    }
}
