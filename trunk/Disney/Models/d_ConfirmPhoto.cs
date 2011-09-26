using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_ConfirmPhoto
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 工作单ID
        /// </summary>
        public int KitWorkID { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime ConfirmTime { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmMan { get; set; }

        /// <summary>
        /// 确认电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 确认内容
        /// </summary>
        public string Remark { get; set; }
        #endregion
    }
}
