using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Models.Enums
{
    public enum KitPhotoType
    {
        /// <summary>
        ///封面
        /// </summary>
        [Description("封面")]
        Cover = 1,
        /// <summary>
        /// 学生团体照
        /// </summary>
        [Description("学生团体照")]
        GroupPhoto = 2,
        /// <summary>
        /// 同学录
        /// </summary>
        [Description("同学录")]
        Classmates = 3,
        /// <summary>
        /// 礼服
        /// </summary>
        [Description("礼服")]
        Robe = 4,
        /// <summary>
        /// 生活照
        /// </summary>
        [Description("生活照")]
        Life = 5,
        /// <summary>
        /// 大头贴
        /// </summary>
        [Description("大头贴")]
        Head = 6
    }
}
