using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Unity.Mvc3.Helpers
{
    public static class RegexUnity
    {
        public static bool IsNullString(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public static bool IsPhone(this string value)
        {
            Regex reg = new Regex(@"(-|\s|\d|\*)$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 两位小数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFloat(this string value)
        {
            Regex reg = new Regex(@"\d+(\.\d{1,6})?$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 邮编格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsZip(this string value)
        {
            Regex reg = new Regex(@"^[1-9]\d{5}(?!\d)$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 区号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDistrictNum(this string value)
        {
            Regex reg = new Regex(@"^\d{3,4}$");
            return reg.IsMatch(value);
        }
        /// <summary>
        /// URL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUrl(this string value)
        {
            Regex reg = new Regex(@"^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$");
            return reg.IsMatch(value);
        }

    }
}
