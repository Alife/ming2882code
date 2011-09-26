using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitCostume
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 服装ID
        /// </summary>
        public int CostumeID { get; set; }

        /// <summary>
        /// 小朋友ID
        /// </summary>
        public int KitChildID { get; set; }
        #endregion
    }
}
