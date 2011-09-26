using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Models.Enums
{
    public enum PoliticsStatus
    {
        /// <summary>
        ///自由人
        /// </summary>
        [Description("自由人")]
        liberty = 1,
        /// <summary>
        /// 党员
        /// </summary>
        [Description("党员")]
        party = 2,
        /// <summary>
        /// 团员
        /// </summary>
        [Description("团员")]
        group = 3,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        other = 4
    }
}
