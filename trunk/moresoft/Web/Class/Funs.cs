using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MC.Model;

namespace Web
{
    public class Sorters
    {
        public string property { get; set; }
        public string direction { get; set; }
    }
    public class Funs
    {
        #region 返回guid
        public static string GetGuid
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        #endregion
        #region 返回排序参数
        public static IDictionary Orderby(string sort)
        {
            var orderby = new Hashtable();
            if (!string.IsNullOrEmpty(sort))
            {
                Sorters sorter = JsonConvert.DeserializeObject<Sorters>(sort.Replace("[", "").Replace("]", ""));
                orderby.Add("sort", sorter.property);
                orderby.Add("dir", sorter.direction.ToLower());
            }
            return orderby;
        }
        #endregion
        #region 返回排序参数2
        public static void Orderby(QueryInfo queryInfo, string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                Sorters sorter = JsonConvert.DeserializeObject<Sorters>(sort.Replace("[", "").Replace("]", ""));
                queryInfo.Parameters.Add("sort", sorter.property);
                queryInfo.Parameters.Add("dir", sorter.direction.ToLower());
            }
        }
        #endregion
        #region 统一传参调用方法
        /// <summary>
        /// 统一传参调用方法
        /// </summary>
        /// <returns></returns>
        public static QueryInfo GetQueryInfo()
        {
            QueryInfo queryInfo = new QueryInfo();
            if (HttpContext.Current != null)
            {
                HttpRequest request = HttpContext.Current.Request;
                string[] arrQuery = request.Form.AllKeys.Concat(request.QueryString.AllKeys)
                    .Where(p => !p.ToLower().Contains("_dc") && !p.ToLower().Contains("page")).ToArray();
                foreach (var query in arrQuery)
                {
                    if (query.Contains("sort"))
                        Orderby(queryInfo, request["sort"]);
                    else if (query.Contains("start") || query.Contains("limit"))
                        queryInfo.Parameters.Add(query, ReqHelper.Get<int>(query));
                    else if (!string.IsNullOrEmpty(request[query]))
                    {
                        Match arr = Regex.Match(query, @"_[a-z]{1}_$", RegexOptions.IgnoreCase);
                        if (arr.Success)
                        {
                            switch (arr.Value)
                            {
                                case "_s_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), request[query]);
                                    break;
                                case "_c_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), request[query]);
                                    break;
                                case "_i_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), ReqHelper.Get<int>(query));
                                    break;
                                case "_d_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), ReqHelper.Get<decimal>(query));
                                    break;
                                case "_u_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), ReqHelper.Get<double>(query));
                                    break;
                                case "_f_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), ReqHelper.Get<float>(query));
                                    break;
                                case "_g_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), ReqHelper.Get<Guid>(query));
                                    break;
                                case "_t_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), ReqHelper.Get<DateTime>(query));
                                    break;
                                case "_b_":
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), ReqHelper.Get<bool>(query));
                                    break;
                                default:
                                    queryInfo.Parameters.Add(query.Replace(arr.Value, string.Empty), request[query]);
                                    break;
                            }
                        }
                        else
                            queryInfo.Parameters.Add(query, request[query]);
                    }
                }
            }
            return queryInfo;
        }
        #endregion
        #region 树下拉时使用
        public static string GetCategroyPath(int path, string icon)
        {
            string str = string.Empty;
            path -= 1;
            if (path != 0)
            {
                for (int i = 0; i < path; i++)
                    str = icon + str;
            }
            return str;
        }
        #endregion
        #region 返回字符串的Ascii字符数量(中文*2英文*1)
        /// <summary>
        /// 返回字符串的Ascii字符数量(中文*2英文*1)
        /// </summary>
        /// <param name="strCode">查询字符串</param>
        /// <returns>Ascii字符数</returns>
        public static int GetStrLen(string strCode)
        {
            int _strlength = strCode.Length;
            int tmpNum = 0;

            byte[] strASCII = ASCIIEncoding.ASCII.GetBytes(strCode);
            for (int i = 0; i < _strlength; i++)
            {
                if ((int)strASCII[i] == 63)
                {
                    tmpNum += 2;
                }
                else
                {
                    tmpNum += 1;
                }
                if (i == _strlength - 1) break;
            }
            return tmpNum;
        }
        #endregion
        #region 截取字符
        /// <summary>
        /// 截取字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="l"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        public static string SubString(string s, int len, string endStr)
        {
            if (string.IsNullOrEmpty(s)) { return string.Empty; }
            string temp = s.Substring(0, (s.Length < len + 1) ? s.Length : len + 1);
            byte[] encodedBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(temp);

            string outputStr = "";
            int count = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                if ((int)encodedBytes[i] == 63)
                    count += 2;
                else
                    count += 1;

                if (count <= len - endStr.Length)
                    outputStr += temp.Substring(i, 1);
                else if (count > len)
                    break;
            }

            if (count <= len)
            {
                outputStr = temp;
                endStr = "";
            }

            outputStr += endStr;

            return outputStr;
        }
        #endregion
        #region 生成count个不同的随机数
        public static string GetRandom()
        {
            return GetRandom(1);
        }
        /// <summary>
        /// 生成count个不同的随机数,默认四位
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetRandom(int count)
        {
            int minValue = 1, maxValue = 10000;
            if (count > 1) maxValue *= 10 * count;
            Random rnd = new Random();
            int length = maxValue - minValue + 1;
            byte[] keys = new byte[length];
            rnd.NextBytes(keys);
            int[] items = new int[length];
            for (int i = 0; i < length; i++)
                items[i] = i + minValue;
            Array.Sort(keys, items);
            int[] result = new int[1];
            Array.Copy(items, result, 1);
            string str = string.Empty;
            for (int i = 0; i < result.Length; i++)
                str += result[i].ToString();
            return str;
        }
        #endregion
        #region 比较时间大小
        /// <summary>
        /// 比较时间大小
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int CompareTo(DateTime? dt)
        {
            int ri = 0;
            if (dt.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                ri = 1;
            else
                ri = DateTime.Compare(dt.Value, DateTime.Now);
            return ri;
        }
        #endregion
        #region 将HTML标签转化为空格
        /// <summary>
        /// 将HTML标签转化为空格
        /// </summary>
        /// <param name="strHtml">带转化的字符串</param>
        /// <returns>经过转化的字符串</returns>
        public static string StripHtml(string strHtml)
        {
            Regex objRegExp = new Regex("<(.|\n)+?>");
            if (string.IsNullOrEmpty(strHtml))
                return strHtml;
            else
            {
                string strOutput = objRegExp.Replace(strHtml, "");
                return strOutput;
            }
        }
        #endregion

        #region 字符串(1,2,3...)转化为List类型
        /*
        User:jrz
        Date:2011-08-25
     */
        public static List<int> ConvertStrToId(string strIds)
        {
            return ConvertStrToId(strIds, ",");
        }
        public static List<int> ConvertStrToId(string strIds, string strSplit)
        {
            List<int> lst = new List<int>();
            if (string.IsNullOrEmpty(strIds) == true)
                return lst;
            string[] s = strIds.Split(new string[] { strSplit }, StringSplitOptions.RemoveEmptyEntries);
            int result;
            foreach (string strId in s)
            {
                if (int.TryParse(strId, out result) == false)
                    continue;
                lst.Add(result);
            }
            return lst;
        }
        #endregion
    }
    public class TreeEntity
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<TreeEntity> children { get; set; }
    }
    public class ReqHelper
    {
        public static T Get<T>(string paramName)
        {
            string value = HttpContext.Current.Request[paramName];
            Type type = typeof(T);
            object result;
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }
    }
}