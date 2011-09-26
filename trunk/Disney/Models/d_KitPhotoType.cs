using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitPhotoType
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类:1封面，2学生团体照，3同学录，4礼服，5生活照
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 美工价格
        /// </summary>
        public decimal ArtPrice { get; set; }

        /// <summary>
        /// 1封面、团照、同学录、生活照：PhotoNum*PeopleNum*Price，2礼服：((PeopleNum-1)*2+PeopleNum)*Price，3团照、同学录、生活照：PeopleNum*Price
        /// </summary>
        public string Formula { get; set; }
        #endregion
    }
}
