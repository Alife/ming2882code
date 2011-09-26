using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace Web
{
    public class AliPay
    {
        private string gateway = "https://www.alipay.com/cooperate/gateway.do?";//支付网关
        private string service = "trade_create_by_buyer";//接口名
        private string key = "4b7napsxxyqdx1p140v32ga3zjl0d562";//partner账户的支付宝安全校验码
        private string sign_type = "MD5";//加密方式
        private string subject = "元隆商品  ";
        private string _input_charset = "utf-8";//编码
        private string partner = "2088002558231796";//合作商
        private string notify_url = "http://www.jiaoguo.com/member/editorder";//异步返回
        private string return_url = "http://www.jiaoguo.com/member/showorder";//返回路径
        private string show_url = "http://www.jiaoguo.com/";
        private int payment_type = 1;//支付类型 1：商品购买 2：服务购买3：网络拍卖4：捐赠	
        private string seller_email = "";//卖家账号（支付宝注册的Email或注册id）

        #region MD5加密算法
        public string GetMD5(string s, string _input_charset)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        #endregion

        #region 冒泡排序法
        public static string[] BubbleSort(string[] r)
        {
            int i, j; //交换标志 
            string temp;

            bool exchange;

            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假

                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }

                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }

            }
            return r;
        }
        #endregion

        #region 构建路径
        /// <summary>
        /// 构建路径
        /// </summary>
        /// <param name="body">订单号</param>
        /// <param name="total_fee">交易金额</param>
        /// <param name="out_trade_no">支付宝订单号</param>
        /// <returns></returns>
        private string CreatUrl(string body, string total_fee, string out_trade_no)
        {

            int i;
            #region 构造数组
            string[] Oristr ={ 
                "service="+service, 
                "partner=" + partner, 
                "subject=" + subject,
                "body="+body,
                "out_trade_no=" + out_trade_no, 
                "total_fee=" + total_fee, 
                "payment_type=" + payment_type, 
                "seller_email=" + seller_email, 
                "notify_url="+notify_url,
                "show_url="+show_url,
                "_input_charset="+_input_charset,       
                "return_url=" + return_url
                };

            #endregion
            //进行排序
            string[] Sortedstr = BubbleSort(Oristr);
            //构造待md5摘要字符串 
            StringBuilder prestr = new StringBuilder();
            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (i == Sortedstr.Length - 1)
                    prestr.Append(Sortedstr[i]);
                else
                    prestr.Append(Sortedstr[i] + "&");
            }
            prestr.Append(key);
            //生成Md5摘要
            string sign = GetMD5(prestr.ToString(), _input_charset);
            //构造支付Url
            char[] delimiterChars = { '=' };
            StringBuilder parameter = new StringBuilder();
            parameter.Append(gateway);
            for (i = 0; i < Sortedstr.Length; i++)
            {
                parameter.Append(Sortedstr[i].Split(delimiterChars)[0] + "=" + HttpUtility.UrlEncode(Sortedstr[i].Split(delimiterChars)[1]) + "&");
            }

            parameter.Append("sign=" + sign + "&sign_type=" + sign_type);
            //返回支付Url；
            return parameter.ToString();

        }
        #endregion

        #region 提交到支付宝
        /// <summary>
        /// 提交到支付宝
        /// </summary>
        /// <param name="body">订单号</param>
        /// <param name="total_fee">支付金额</param>
        /// <param name="target">目标窗口</param>
        public void SendPay(System.Web.Mvc.Controller page, string body, string total_fee, string target)
        {
            //按时构造订单号；
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string out_trade_no = currentTime.ToString("g");
            out_trade_no = out_trade_no.Replace("-", "");
            out_trade_no = out_trade_no.Replace(":", "");
            out_trade_no = out_trade_no.Replace(" ", "");

            string aliay_url = this.CreatUrl(body, total_fee, out_trade_no);
            string[] Oristr ={ 
                "service="+service, 
                "partner=" + partner, 
                "subject=" + subject,
                "body="+body,
                "out_trade_no=" + out_trade_no, 
                "total_fee=" + total_fee, 
                "payment_type=" + payment_type, 
                "seller_email=" + seller_email, 
                "notify_url="+notify_url,
                "show_url="+show_url,
                "_input_charset="+_input_charset,       
                "return_url=" + return_url
                };
            //进行排序
            string[] Sortedstr = BubbleSort(Oristr);
            //构造待md5摘要字符串 
            StringBuilder prestr = new StringBuilder();
            for (int i = 0; i < Sortedstr.Length; i++)
            {
                if (i == Sortedstr.Length - 1)
                    prestr.Append(Sortedstr[i]);
                else
                    prestr.Append(Sortedstr[i] + "&");
            }
            this.HttpPostHandle(page, gateway, target, prestr.ToString());
        }
        #endregion

        #region 自定义POST提交脚本
        /// <summary>
        /// 自定义POST提交脚本
        /// </summary>
        /// <param name="page">页面引用</param>
        /// <param name="action">提交地址</param>
        /// <param name="target">页面弹出方式</param>
        /// <param name="parameter">提交参数(name1=value1&name2=value2&name3=value3&...)</param>
        private void HttpPostHandle(System.Web.Mvc.Controller page, string action, string target, string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                string[] Paras = parameter.Split('&');

                if (!string.IsNullOrEmpty(target))
                {
                    target = "target='" + target + "'";
                }
               // page.Response.Write(@"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
               //  page.Response.Write(@"<html xmlns='http://www.w3.org/1999/xhtml'>");
                page.Response.Write("<div style=\"display:none\"  >");
                page.Response.Write("<form name='postname'  method='post' " + target + " action='" + action + "'>");

                foreach (string _parameter in Paras)
                {
                    string name = _parameter.Split('=')[0].ToString();
                    string value = _parameter.Split('=')[1].ToString();

                    page.Response.Write("<input type='hidden' name='" + name + "' value='" + value + "'>");
                }
                page.Response.Write("</form>");
                page.Response.Write("<script>");
                page.Response.Write("document.forms['postname'].submit();");
                page.Response.Write("</script>");
                page.Response.Write("</div>");
            }

        }
        #endregion

        #region 获取远程服务器ATN结果
        public static String Get_Http(String a_strUrl, int timeout)
        {
            string strResult;
            try
            {

                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {

                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }
        #endregion

        #region 异步获取支付处理结果
        public void AlipayNotify(System.Web.Mvc.Controller control)
        {
            
            string alipayNotifyURL = gateway;
            alipayNotifyURL = alipayNotifyURL + "service=notify_verify" + "&partner=" + partner + "&notify_id=" + control.Request.Form["notify_id"];
            //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的
            string responseTxt = Get_Http(alipayNotifyURL, 120000);
            int i;
            NameValueCollection coll;
            coll = control.Request.Form;
            
            String[] requestarr = coll.AllKeys;
            //进行排序；
            string[] Sortedstr = BubbleSort(requestarr);
            //构造待md5摘要字符串 ；
            string prestr = "";
            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (control.Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type")
                {
                    if (i == Sortedstr.Length - 1)
                    {
                        prestr = prestr + Sortedstr[i] + "=" + control.Request.Form[Sortedstr[i]];
                    }
                    else
                    {
                        prestr = prestr + Sortedstr[i] + "=" + control.Request.Form[Sortedstr[i]] + "&";
                    }
                }
            }
            prestr = prestr + key;

            string mysign = GetMD5(prestr,_input_charset);
            string sign = control.Request.Form["sign"];
            if (mysign == sign && responseTxt == "true")   //验证支付发过来的消息，签名是否正确
            {
                #region 处理订单
                //Models.t_Order order = BLL.t_OrderBLL.GetItem(control.Request.Form["body"]);
                //order.col_st = (int)Models.Enums.StockOrder.GoodsState.Account;
                //BLL.t_OrderBLL.Update(order);
                #endregion
                control.Response.Write("success");     //返回给支付宝消息，成功
            }
            else
                control.Response.Write("success");
        }
        #endregion

        #region 支付处理结果
        public void AlipayReturn(System.Web.Mvc.Controller control)
        {
            ///当不知道https的时候，请使用http
            //string alipayNotifyURL = "https://www.alipay.com/cooperate/gateway.do?";
            string alipayNotifyURL = "http://notify.alipay.com/trade/notify_query.do?";
            //alipayNotifyURL = alipayNotifyURL + "service=notify_verify" + "&partner=" + partner + "&notify_id=" + Request.QueryString["notify_id"];
            alipayNotifyURL = alipayNotifyURL + "&partner=" + partner + "&notify_id=" + control.Request.QueryString["notify_id"];

            string responseTxt = Get_Http(alipayNotifyURL, 120000);
            int i;
            NameValueCollection coll;
            coll = control.Request.QueryString;
            String[] requestarr = coll.AllKeys;
            //进行排序；
            string[] Sortedstr = BubbleSort(requestarr);
            //构造待md5摘要字符串 
            StringBuilder prestr = new StringBuilder();

            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type")
                {
                    if (i == Sortedstr.Length - 1)
                    {
                        prestr.Append(Sortedstr[i] + "=" + control.Request.QueryString[Sortedstr[i]]);
                    }
                    else
                    {
                        prestr.Append(Sortedstr[i] + "=" + control.Request.QueryString[Sortedstr[i]] + "&");
                    }
                }
            }

            prestr.Append(key);

            //生成Md5摘要
            string mysign = GetMD5(prestr.ToString(),_input_charset);
            string sign = control.Request.QueryString["sign"];
            if (mysign == sign && responseTxt == "true")   //验证支付发过来的消息，签名是否正确
            {
                #region 处理订单
                //Models.t_Order order = BLL.t_OrderBLL.GetItem(control.Request.Form["body"]);
                //order.col_st = (int)Models.Enums.StockOrder.GoodsState.Account;
                //BLL.t_OrderBLL.Update(order);
                #endregion
                control.Response.Write("success");
                control.Response.Redirect("Success.aspx");
            }
            else
            {
                control.Response.Write("fail");
                control.Response.Write("<br>------------------" + control.Request.QueryString["body"]);
            }

        }
        #endregion 
    }
}
