using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_ArtistPrice
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 美工ID
        /// </summary>
        public int UserID { get; set; }
        
        /// <summary>
        /// 档图类型
        /// </summary>
        public int KitPhotoTypeID { get; set; }
        
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        #endregion
    }
}
