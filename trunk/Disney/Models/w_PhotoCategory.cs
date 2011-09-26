using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class w_PhotoCategory
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// 分類名稱
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 內容
        /// </summary>
        public string Intro { get; set; }
        
        /// <summary>
        /// 拍攝時間
        /// </summary>
        public DateTime ShootingTime { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID { get; set; }
        #endregion
    }
}
