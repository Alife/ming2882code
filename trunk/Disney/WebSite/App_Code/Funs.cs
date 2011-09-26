using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class Funs
{
    /// <summary>
    /// 返回头像
    /// </summary>
    /// <param name="avatar"></param>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Avatar(string avatar)
    {
        if (string.IsNullOrEmpty(avatar))
            return "avatar/noavatar.jpg";
        return avatar;
    }
    public static string GetCategroyPath(int path, string icon)
    {
        string str = string.Empty;
        path -= 1;
        if (path != 0)
        {
            for (int i = 0; i < path; i++)
                str += "　";
           // str = icon + str;
        }
        return str;
    }
    public static string GetTags(string tags)
    {
        if (string.IsNullOrEmpty(tags))
            return string.Empty;
        return tags.Replace("，", ",").TrimStart(',').TrimEnd(',').Trim();
    }
    public static string GetFilePath(string path)
    {
        string filename = path.Substring(0, path.IndexOf('.') - 4);
        string filefix = path.Substring(path.IndexOf('.'));
        return filename + filefix;
    }
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
    /// <summary>
    /// 生成count个不同的随机数
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static string GetNum()
    {
        string str = string.Empty;
        //System.Collections.ArrayList temp = new System.Collections.ArrayList(); //定义大小和随需求动态增加的数组
        System.Random rd = new Random();
        str = rd.Next(100, 9999).ToString();
        //while (temp.Count < count) //生成count个不同的随机数
        //{
        //    int a = rd.Next(1, 49);
        //    if (!temp.Contains(a))
        //        temp.Add(a);
        //}

        //for (int i = 0; i < count; i++) //在Textbox中显示数组元素
        //{
        //    str += temp[i].ToString();
        //}

        return str;
    }
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
    public static string GetGoodTime()
    {
        string str = string.Empty;
        DateTime dt = DateTime.Now;
        if (dt.Hour >= 6 && dt.Hour < 12)
            str = "早上好";
        else if (dt.Hour >= 12 && dt.Hour < 18)
            str = "下午好";
        else if (dt.Hour >= 18 && dt.Hour < 24)
            str = "晚上好";
        else if (dt.Hour >= 0 && dt.Hour < 6)
            str = "凌晨了,早点休息哦";
        return str;
    }
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
    #region 验证图片格式
    public static bool IsAllowedExtension(System.IO.Stream st)
    {
        System.IO.BinaryReader r = new System.IO.BinaryReader(st);
        string fileclass = "";
        byte buffer;
        try
        {
            buffer = r.ReadByte();
            fileclass = buffer.ToString();
            buffer = r.ReadByte();
            fileclass += buffer.ToString();
        }
        catch
        {
        }
        /*文件扩展名说明 
        jpg：255216 
        bmp：6677 
        gif：7173 
        xls,doc,ppt：208207 
        rar：8297 
        zip：8075 
        txt：98109 
        pdf：3780 
        */
        if (fileclass == "255216" || fileclass == "7173")//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
            return true;
        else
            return false;
    }
    #endregion
}