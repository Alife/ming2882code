using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_ArtistMonth
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        
        /// <summary>
        /// 档图ID
        /// </summary>
        public int KitPhotoID { get; set; }

        /// <summary>
        /// 总人数量
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 图片数量
        /// </summary>
        public int PhotoNum { get; set; }
        /// <summary>
        /// 老师数量
        /// </summary>
        public int TeacherNum { get; set; }
        
        /// <summary>
        /// 美工价格
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Amt { get; set; }
        
        /// <summary>
        /// 美工月结时间
        /// </summary>
        public DateTime? BalanceTime { get; set; }

        /// <summary>
        /// 结算状态：1美工未结，2美工已结
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion
    }
}
