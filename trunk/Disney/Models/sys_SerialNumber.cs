using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class sys_SerialNumber
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 标识，1客户，2会员，3工员(包括美工)，4订单
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentDate { get; set; }

        /// <summary>
        /// 当前流水号
        /// </summary>
        public int SerialNumber { get; set; }
        #endregion
    }
}
