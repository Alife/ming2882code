using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net;
using MC.Mvc.Filter;
using MC.Mvc.Helpers;
using MC.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [CompressFilter]
        public ActionResult Index()
        {
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
            //MachineKeySection configSection = (MachineKeySection)config.GetSection("system.web/machineKey");
            //configSection.ValidationKey = CreateKey(2932);
            //configSection.DecryptionKey = CreateKey(2493);
            //configSection.Validation = MachineKeyValidation.SHA1;
            //if (!configSection.SectionInformation.IsLocked)
            //{
            //    config.Save();
            //    Response.Write("写入成功！");
            //}
            //else
            //{
            //    Response.Write("写入失败！段被锁定！");
            //}  
            return View();
        }
        public static string CreateKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];
            rng.GetBytes(buff);
            System.Text.StringBuilder hexString = new System.Text.StringBuilder(64);
            for (int i = 0; i < buff.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", buff[i]));
            }
            return hexString.ToString();
        }      
        [CompressFilter]
        public ContentResult GZIP(string id)
        {
            string filePath = Server.MapPath(id);
            FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //GZipStream gzip = new GZipStream(fsIn, CompressionMode.Compress, true);
            StreamReader Reader = new StreamReader(fsIn, Encoding.UTF8);
            string Html = Reader.ReadToEnd();
            fsIn.Close();
            //gzip.Close();
            Reader.Close();
            Response.AppendHeader("Cache-Control", "public");
            Response.AppendHeader("Content-Type", "application/x-javascript");
            return Content(Html);
        }
        public JsonResult getUserButtons(int sysMenuId)
        {
            ArrayList lst = new ArrayList();
            lst.Add(new { nodeId = 1, menuName = "功能测试", actionPath = "" });
            lst.Add(new { nodeId = 2, menuName = "wcf REST应用", actionPath = "" });
            lst.Add(new { nodeId = 3, menuName = "功能测试2", actionPath = "" });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUserTree(int parantNodeId, string menuName)
        {
            ArrayList lst = new ArrayList();
            if (parantNodeId == 1)
            {
                lst.Add(new { id = 1, text = "动态GRID", jsUrl = "/js/view/DynamicGrid.js;/js/view/fundyngird.js", iconCls = "chart_organisation", leaf = true, path = 1, type = "jsclass", namespace1 = "com.ms.basic.DynamicGrid", mainClass = "com.ms.basic.DynamicGrid" });
                lst.Add(new { id = 2, text = "纯extjs-Grid", jsUrl = "/js/view/ResourcePanel.js", iconCls = "application_xp", leaf = true, path = 1, type = "jsclass", namespace1 = "com.ms.basic.ResourcePanel", mainClass = "com.ms.basic.ResourcePanel" });
                lst.Add(new { id = 3, text = "测试是否同上冲突", jsUrl = "/js/view/ResourcePanel1.js", iconCls = "application_view_icons", leaf = true, path = 1, type = "jsclass", namespace1 = "com.ms.basic.ResourcePanel1", mainClass = "com.ms.basic.ResourcePanel1" });
                lst.Add(new { id = 4, text = "XSLT+XML单表", jsUrl = "", url = "/home/loadxmlGrid", iconCls = "application_view_tile", leaf = true, path = 1, type = "loadjs", namespace1 = "wod_Word_Grid_Panel", mainClass = "wod_Word_Grid_Panel" });
                lst.Add(new { id = 5, text = "测试XSLT单表BUG", jsUrl = "/js/view/JScript1.js", leaf = true, iconCls = "application_view_columns", path = 1, type = "jsclass", namespace1 = "wod_Word_Grid_Panel1", mainClass = "wod_Word_Grid_Panel1" });
                lst.Add(new { id = 6, text = "动态生成WebSerice,击冲代理", url = "/home/dynWebserice", leaf = true, iconCls = "asterisk_yellow", path = 1, type = "iframe" });
                lst.Add(new { id = 7, text = "ajax长轮询的 Comet", url = "/home/LongPolling", leaf = true, iconCls = "book_addresses", path = 1, type = "iframe" });
                lst.Add(new { id = 8, text = "ContentType的推送,可怜只在FF有效", url = "/home/LongPolling", leaf = true, iconCls = "bell", path = 1, type = "iframe" });
                lst.Add(new { id = 9, text = "Websocket", url = "/home/Websocket", leaf = true, iconCls = "brick", path = 1, type = "iframe" });
            }
            else if (parantNodeId == 2)
            {
                lst.Add(new { id = 10, text = "WCF REST", url = "/home/wcfrest/abc", leaf = true, iconCls = "brick", path = 1, type = "iframe" });
                lst.Add(new { id = 11, text = "WCF REST read return xml", url = "/home/wcfread?name=haode&position=china", leaf = true, iconCls = "brick", path = 1, type = "iframe" });
                lst.Add(new { id = 12, text = "WCF REST add return xml", url = "/home/wcfadd", leaf = true, iconCls = "brick", path = 1, type = "iframe" });
                lst.Add(new { id = 13, text = "UploadString add", url = "/home/wcfadd1/1", leaf = true, iconCls = "brick", path = 1, type = "iframe" });
                lst.Add(new { id = 14, text = "WebRequest add", url = "/home/wcfadd1/2", leaf = true, iconCls = "brick", path = 1, type = "iframe" });
            }
            else
                return Json("", JsonRequestBehavior.AllowGet);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public ContentResult loadxmlGrid()
        {
            ReaderXml x = new ReaderXml();
            XPathDocument xpathDoc = x.RenderGridXML("wod_Word_Grid", "10000");
            string xslPath = Server.MapPath("/XSLT/Grid.xslt");
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ProhibitDtd = false;
            readerSettings.CloseInput = true;
            XmlReader reader = XmlReader.Create(xslPath, readerSettings);
            XslCompiledTransform transform = new XslCompiledTransform(true);
            XsltArgumentList argsList = new XsltArgumentList();
            XsltSettings setting = new XsltSettings(true, true);
            transform.Load(reader, new XsltSettings(true, true), new XmlUrlResolver());
            transform.Transform(xpathDoc, argsList, Response.Output);
            reader.Close();
            return Content("");
        }
        #region 动态GRID
        public ContentResult dynamicGrid()
        {
            string json = @"{
	                'metaData': {
		                'totalProperty': 'total',
		                'root': 'records',
		                'id': 'id',
		                'fields': [
			                {'name': 'id', 'type': 'int'},
			                {'name': 'name', 'type': 'string'},
			                {'name': 'code', 'type': 'string'},
			                {'name': 'dept', 'type': 'string'},
			                {'name': 'email', 'type': 'string'},
			                {'name': 'ip', 'type': 'string'}
		                ]
	                },
	                'success': true,
	                'total': 21,
	                'records': [
		                {'id': '1', 'name': 'AAA','code':'23129','dept':'开发部','email':'sks@llss.cm','ip':'127.39.39.0'},
		                {'id': '2', 'name': 'BBB','code':'2383823','dept':'行政部','email':'sksk@ks.com','ip':'28.32.29.29'}
	                ],
	                'columns': [
		                {'header': 'id', 'dataIndex': 'id',width:30},
		                {'header': 'User', 'dataIndex': 'name',width:80},
		                {'header': 'code', 'dataIndex': 'code',width:60},
		                {'header': 'dept', 'dataIndex': 'dept',width:60},
		                {'header': 'email', 'dataIndex': 'email',width:80},
		                {'header': 'ip', 'dataIndex': 'ip',width:60}
	                ]
                }";
            return Content(json);
        }
        #endregion
        public JsonResult loadUserInfo()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Request.Cookies[cookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = null;
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JsonSerializerSettings jsonSs = new JsonSerializerSettings();
                MC.Model.mc_User userInfo = (MC.Model.mc_User)JsonConvert.DeserializeObject(authTicket.UserData, typeof(MC.Model.mc_User), jsonSs);
                return Json(userInfo, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        #region 纯extjs-Grid
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ResourcesList()
        {
            ArrayList lst = new ArrayList();
            lst.Add(new { id = 1, nodeId = 0, parantNodeID = 0, menuName = "动态GRID", actionPath = "", jsClassFile = "/js/view/DynamicGrid.js;/js/view/fundyngird.js", icon = "icon-add", menuOrder = 1, openIcon = "JSClass", description = "com.ms.basic.DynamicGrid", mainClass = "com.ms.basic.DynamicGrid" });
            lst.Add(new { id = 2, nodeId = 0, parantNodeID = 0, menuName = "功能测试2", actionPath = "", jsClassFile = "/js/view/ResourcePanel.js", icon = "icon-edel", menuOrder = 1, openIcon = "JSClass", description = "com.ms.basic.ResourcePanel", mainClass = "com.ms.basic.ResourcePanel" });
            Hashtable dt = new Hashtable();
            dt.Add("data", lst);
            dt.Add("total", 2);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ContentResult icon()
        {
            string[] strs = System.IO.Directory.GetFiles(Server.MapPath("/images"), "*.png");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string item in strs)
            {
                System.IO.FileInfo fl = new System.IO.FileInfo(item);
                sb.AppendLine("." + fl.Name.Replace(fl.Extension, string.Empty) + "{background-image:url(../images/" + fl.Name + ") !important;}");
            }
            //SiteMapNode root = SiteMap.Providers["SecuritySiteMap"].RootNode;
            //if (root != null)
            //{
            //    foreach (SiteMapNode adminNode in root.ChildNodes)
            //    {
            //        if (adminNode.IsAccessibleToUser(HttpContext.Current))
            //        {
            //            if (!Request.RawUrl.ToUpperInvariant().Contains("/ADMIN/") && (adminNode.Url.Contains("xmanager") || adminNode.Url.Contains("PingServices")))
            //                continue;
            //            HtmlAnchor a = new HtmlAnchor();
            //            a.HRef = adminNode.Url;
            //            a.InnerHtml = "<span>" + Translate(adminNode.Title) + "</span>";//"<span>" + Translate(info.Name.Replace(".aspx", string.Empty)) + "</span>";
            //            if (Request.RawUrl.EndsWith(adminNode.Url, StringComparison.OrdinalIgnoreCase))
            //                a.Attributes["class"] = "current";
            //            HtmlGenericControl li = new HtmlGenericControl("li");
            //            li.Controls.Add(a);
            //            ulMenu.Controls.Add(li);
            //        }
            //    }
            //}
            return Content(sb.ToString());
        }
        #region 动态生成WebSerice
        /// <summary>
        /// 动态生成WebSerice
        /// </summary>
        /// <returns></returns>
        public ContentResult dynWebserice()
        {
            string url = "http://www.webservicex.net/globalweather.asmx";
            string[] args = new string[2];
            args[0] = "xiamen";
            args[1] = "China";
            object result = WebServiceHelper.InvokeWebService(url, "GetWeather", args);
            return Content(result.ToString());
        }
        public ContentResult hollo()
        {
            mcServiceReference.mcService mc = new Web.mcServiceReference.mcService();
            return Content(mc.HelloWorld());
        }
        #endregion
        #region ajax长轮询的 Comet
        /// <summary>
        /// ajax长轮询的 Comet
        /// </summary>
        /// <returns></returns>
        public ActionResult LongPolling()
        {
            return View();
        }
        /// <summary>
        /// 服务器推送http://localhost:6501/home/comet
        /// 只有FF可以用
        /// </summary>
        /// <returns></returns>
        public ActionResult multipart()
        {
            //结束标志，这是随机生成的
            string Boundary = "ABCDEFGHIJKLMNOPQRST";
            Response.ContentType = "multipart/x-mixed-replace;boundary=\"" + Boundary + "\"";
            Response.StatusCode = 200;
            Response.Output.Write("--" + Boundary);
            Response.Flush();
            //每隔5秒种向客户端发送一次数据
            while (true)
            {
                //发送给客户端的数据的MIME类型，如果是JSON，就用application/json
                //注意这里一定要用WriteLine()
                Response.Output.WriteLine("Content-Type: plain/text");
                //这句生成空行的代码不能少
                Response.Output.WriteLine();
                Response.Output.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:s.FFF"));
                //发送结束标志，客户端就知道完成了一次发送
                Response.Output.WriteLine("--" + Boundary);
                Response.Flush();
                System.Threading.Thread.Sleep(1000);
            }
        }
        #endregion
        #region ContentType的推送
        /// <summary>
        /// ContentType的推送
        /// </summary>
        /// <returns></returns>
        public ActionResult comet()
        {
            return View();
        }
        #endregion
        #region Websocket
        public ActionResult Websocket()
        {
            return View();
        }
        #endregion
        #region wcf rest
        public ContentResult wcfrest(string id)
        {
            string json = WebClientHelper.Client.DownloadString("http://localhost:1503/Sample/Hello.svc/sample/" + id);
            return Content(json);
        }
        #endregion
        #region wcf read
        public ContentResult wcfread(string name, string position)
        {
            //string json = WebClientHelper.Client.DownloadString("http://localhost:1503/contract/test.svc/User/Get/" + name + "/" + position);
            //return Content(json);
            string serviceUrl = "http://localhost:1503/contract/test.svc/User/Get/" + name + "/" + position;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

            // 获得接口返回值
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            string ReturnXml = HttpUtility.UrlDecode(reader.ReadToEnd());

            reader.Close();
            myResponse.Close();
            return Content(ReturnXml);
        }
        #endregion
        #region wcf add
        public ContentResult wcfadd()
        {
            string serviceUrl = string.Format("{0}/{1}", "http://localhost:1503/contract/test.svc", "User/Create");
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

            string data = "<User xmlns=\"http://rest-server/datacontract/user\"><ID>2</ID><Name>haobuhao</Name><Sex>1</Sex><Position>china fujian</Position><Email>wo@qq.com</Email></User>";
            //转成网络流
            byte[] buf = UnicodeEncoding.UTF8.GetBytes(data);

            //设置
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "text/html";

            // 发送请求
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            // 获得接口返回值
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            string ReturnXml = HttpUtility.HtmlDecode(reader.ReadToEnd());

            reader.Close();
            myResponse.Close();
            return Content(ReturnXml);
        }
        #endregion
        #region wcf add1
        public ContentResult wcfadd1(int id)
        {
            string url = "http://localhost:1503/contract/test.svc/User/Create";
            string reid = string.Empty;
            if (id == 1)
            {
                string data = wcfadddata(3);
                WebClient client = WebClientHelper.Client;
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                reid = client.UploadString(url, "POST", data);
            }
            else if (id == 2)
            {
                string data = wcfadddata(4);
                byte[] buf = UnicodeEncoding.UTF8.GetBytes(data);
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = buf.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(buf, 0, buf.Length);
                newStream.Close();
                WebResponse response = (WebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                reid = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            return Content(reid);
        }
        #endregion
        #region wcf add data
        private string wcfadddata(int id)
        {
            var data = new { ID = id, Name = "haobuhao", Sex = 1, Position = "china fujian", Email = "wo@qq.com" };
            string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings());
            return json;
        }
        #endregion
    }
}
