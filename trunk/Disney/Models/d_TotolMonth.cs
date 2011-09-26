using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_TotolMonth
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 单号名称
        /// </summary>
        public string OrderName { get;set; }
        /// <summary>
        /// 结算开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        
        /// <summary>
        /// 结束结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 结算状态：1未结，2已结,3美工已结
        /// </summary>
        public int State { get; set; }
        
        /// <summary>
        /// 月结时间
        /// </summary>
        public DateTime? BalanceTime { get; set; }

        /// <summary>
        /// 实际结算金额
        /// </summary>
        public decimal? BalanceAccount { get; set; }
        /// <summary>
        /// 美工月结时间
        /// </summary>
        public DateTime? ArterBalanceTime { get; set; }
        #endregion
    }
}
