using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.IO;
using System.Web;

namespace Common
{
    public class Utils
    {
        /// <summary>
        /// 是否为邮件格式
        /// </summary>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            if (email != null && email != "")
            {
                string unipp = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex re = new Regex(unipp, RegexOptions.IgnoreCase);
                return re.IsMatch(email);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否为整数
        /// </summary>
        /// <param name="number">要验证的数字</param>
        /// <param name="negative">说否包含验证负数</param>
        /// <returns></returns>
        public static bool IsInt(string number, bool negative)
        {
            if (number != null && number != "")
            {
                Regex oRegEx = null;
                if (negative)
                {
                    oRegEx = new Regex(@"^-?\d+$");
                }
                else
                {
                    oRegEx = new Regex(@"^\d+$");
                }
                return oRegEx.IsMatch(number);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为浮点型
        /// </summary>
        /// <param name="number">要验证的数字</param>
        /// <param name="negative">说否包含验证负数</param>
        /// <returns></returns>
        public static bool isNumber(string number, bool negative)
        {
            if (number != null && number != "")
            {
                Regex oRegEx = null;
                if (negative)
                {
                    oRegEx = new Regex(@"^-?\d+(.\d+)?$");
                }
                else
                {
                    oRegEx = new Regex(@"^\d+(.\d+)?$");
                }
                return oRegEx.IsMatch(number);
            }
            else
            {
                return false;
            }
        }
        public static bool IsSpaceUrl(string datavalue)
        {
            if (datavalue != null && datavalue != "")
            {
                Regex oRegEx = new Regex(@"^[\u0391-\uFFE5\w]+$");
                return oRegEx.IsMatch(datavalue);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为日期型 （2007-1-11  2007/11/1  2007 1 11  07-11-1  07/1/11  07 11 1）
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsData(string datavalue)
        {
            if (datavalue != null && datavalue != "")
            {
                Regex oRegEx = new Regex(@"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[0-9])|([1-2][0-3]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$");
                return oRegEx.IsMatch(datavalue);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为时间格式 （5:5:5  05:05:05）
        /// </summary>
        /// <param name="datavalue"></param>
        /// <returns></returns>
        public static bool IsTime(string datavalue)
        {
            if (datavalue != null && datavalue != "")
            {
                Regex oRegEx = new Regex(@"(\s(((0?[0-9])|(2[0-3])|(1[0-9]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$");
                return oRegEx.IsMatch(datavalue);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 验证是否为时间类型
        /// 日期部分：2007-1-11  2007/11/1  2007 1 11  07-11-1  07/1/11  07 11 1
        /// 时间部分：5:5:5  05:05:05
        /// </summary>
        /// <param name="datavalue"></param>
        /// <returns></returns>
        public static bool IsDataTime(string datavalue)
        {
            if (datavalue != null && datavalue != "")
            {
                Regex oRegEx = new Regex(@"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]((((0?[13578])|(1[02]))[\-\/\s]((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]((((0?[13578])|(1[02]))[\-\/\s]((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[0-9])|(2[0-3])|(1[0-9]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$");
                return oRegEx.IsMatch(datavalue);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否为电话格式
        /// </summary>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static bool IsPhone(string phone)
        {
            if (phone != null && phone != "")
            {
                string unipp = @"0\d{2,3}-\d{5,9}|0\d{2,3}-\d{5,9}";
                Regex re = new Regex(unipp);
                return re.IsMatch(phone);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否为手机格式
        /// </summary>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static bool IsMobile(string mobile)
        {
            if (mobile != null && mobile != "")
            {
                string unipp = @"^(13[0-9]|15[0|1|3|6|7|8|9]|18[8|9])\d{8}$";
                Regex re = new Regex(unipp);
                return re.IsMatch(mobile);
            }
            else
            {
                return false;
            }
        }
        public static bool IsTel(string tel)
        {
            if (tel != null && tel != "")
            {
                string unipp = @"^0?[1][35][0-9]{9}$|^\d{3,4}-?\d{7,9}$";
                Regex re = new Regex(unipp);
                return re.IsMatch(tel);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为邮政编码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsZipCode(string number)
        {
            if (number != null && number != "")
            {
                Regex oRegEx = new Regex("\\d{3,6}");
                return oRegEx.IsMatch(number);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否为QQ
        /// </summary>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static bool IsQQ(string qq)
        {
            string unipp = @"^[1-9]{1}\d{4,11}$";
            Regex re = new Regex(unipp);
            return re.IsMatch(qq);
        }

        /// <summary>
        ///该方法是对用户输入的文字进行格式化
        /// </summary>
        /// <param name="text">用户输入文本</param>
        /// <param name="maxLength">允许最大文本长度</param>
        /// <returns>返回格式后的文本</returns>
        public static string InputText(string text, int maxLength)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            if (text.Length > maxLength)
            {
                text = text.Substring(0, maxLength);
            }
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//去掉两个或更多的空格
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//去掉<br>这个格式符号
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//去掉 &nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//去掉 tags
            text = text.Replace("'", "''");//把单引号改成双引号
            return text;
        }
        public static bool gl(string text,string regex)
        {
            string unipp = @"^" + regex + "$";
            Regex re = new Regex(unipp, RegexOptions.IgnoreCase);
            return re.IsMatch(text);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toUser">接收地址</param>
        /// <param name="fromUser">发送地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">内容</param>
        /// <param name="mailFormat">格式</param>
        /// <param name="emailName">发送邮箱</param>
        /// <param name="emailPassword">发送邮箱密码</param>
        public static bool SendMail(string toUser, string fromUser, string subject, string content, string mailFormat, string emailName, string emailPassword)
        {
            string smtp = emailName.Substring(emailName.IndexOf("@") + 1);
            SmtpClient client = new SmtpClient("smtp." + smtp, 25);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(emailName, emailPassword);
            MailAddress from = new MailAddress(fromUser, "萝卜花网");
            MailAddress to = new MailAddress(toUser, "");
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = subject;
            msg.Body = content;
            msg.BodyEncoding = Encoding.Default;
            if (mailFormat.ToUpper() == "HTML")
            {
                msg.IsBodyHtml = true;
            }
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                string path = "/log/" + DateTime.Now.ToShortDateString() + "/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                path = HttpContext.Current.Server.MapPath(path + "log.txt");
                StreamWriter sw;
                if (!System.IO.File.Exists(path))
                    sw = System.IO.File.CreateText(path);
                else
                    sw = System.IO.File.AppendText(path);
                sw.WriteLine("[" + DateTime.Now + "]");
                sw.WriteLine("msg=" + ex.Message);
                sw.WriteLine("1.发送电子邮件失败：" + ex.HelpLink);
                sw.WriteLine("2.发送电子邮件失败：" + ex.Source);
                sw.WriteLine("3.发送电子邮件失败：" + ex.StackTrace);
                sw.WriteLine("4.发送电子邮件失败：" + ex.TargetSite);
                sw.WriteLine("emailName=" + emailName);
                sw.WriteLine("fromUser=" + fromUser);
                sw.WriteLine("toUser=" + toUser);
                sw.Close();
                return false;
            }
            return true;
        }

    }
}
