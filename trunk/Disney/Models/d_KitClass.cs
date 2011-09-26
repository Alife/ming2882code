using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitClass
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// KitID
        /// </summary>
        public int KitID { get; set; }
        
        /// <summary>
        /// 男生人数
        /// </summary>
        public int BoyNum { get; set; }
        
        /// <summary>
        /// 女生人数
        /// </summary>
        public int GirlNum { get; set; }
        
        /// <summary>
        /// 图照刻印内容
        /// </summary>
        public string Imprint { get; set; }
        #endregion
    }
}
