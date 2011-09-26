using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Models.Enums
{
    public enum Education
    {
        /// <summary>
        ///博士
        /// </summary>
        [Description("博士")]
        Doctor = 1,
        /// <summary>
        /// 硕士
        /// </summary>
        [Description("硕士")]
        Master = 2,
        /// <summary>
        /// 本科
        /// </summary>
        [Description("本科")]
        Undergraduate = 3,
        /// <summary>
        /// 大专
        /// </summary>
        [Description("大专")]
        College = 4,
        /// <summary>
        /// 专中
        /// </summary>
        [Description("专中")]
        Secondary = 5,
        /// <summary>
        /// 高中
        /// </summary>
        [Description("高中")]
        Senior = 6,
        /// <summary>
        /// 高中以下
        /// </summary>
        [Description("高中以下")]
        Junior = 7
    }
}
