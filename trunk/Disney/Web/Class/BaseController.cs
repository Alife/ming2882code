using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;
using System.IO;
using Common;
using BLL;
using Models;
using Newtonsoft.Json;
using System.Xml;

namespace Web
{
    [HandleError]
    public class BaseController : Controller
    {
        public t_User UserBase { get; set; }
        public t_UserType UserBaseType { get; set; }
        protected override void OnException(ExceptionContext filterContext)
        {
            // 此处进行异常记录，可以记录到数据库或文本，也可以使用其他日志记录组件。
            // 通过filterContext.Exception来获取这个异常。
            string path = "/log/" + DateTime.Now.ToShortDateString() + "/";
            if (!Directory.Exists(Server.MapPath(path)))
                Directory.CreateDirectory(Server.MapPath(path));
            path = Server.MapPath(path + "log.txt");
            StreamWriter sw;
            if (!System.IO.File.Exists(path))
                sw = System.IO.File.CreateText(path);
            else
                sw = System.IO.File.AppendText(path);
            sw.WriteLine(DateTime.Now);
            sw.WriteLine(filterContext.Exception.Message
                + (filterContext.Exception.InnerException != null ? filterContext.Exception.InnerException.Message : ""));
            sw.WriteLine("1.错误：" + filterContext.Exception.HelpLink);
            sw.WriteLine("2.错误：" + filterContext.Exception.Source);
            sw.WriteLine("3.错误：" + filterContext.Exception.StackTrace);
            sw.WriteLine("4.错误：" + filterContext.Exception.TargetSite);
            sw.Close();

            // 执行基类中的OnException
            base.OnException(filterContext);

            // 重定向到异常显示页或执行其他异常处理方法
            var msg = new MessageBox(false, "异常错误，你刷新再试");
            Response.Write(JsonConvert.SerializeObject(msg));
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserBase = t_UserBLL.BaseUser;
            if (UserBase != null && UserBase.TypeID.HasValue)
                UserBaseType = t_UserTypeBLL.GetItem(UserBase.TypeID.Value);
            base.OnActionExecuting(filterContext);
        }
        public bool IsPermission(string AppCode, string OpCode)
        {
            bool isOK = false;
            if (UserBase != null) isOK = true;
            if (!string.IsNullOrEmpty(AppCode) && !string.IsNullOrEmpty(OpCode))
            {
                string[] appCodes = AppCode.Split(',');
                string[] opCodes = OpCode.Split(',');
                int i = 0; int val = 0;
                foreach (var app in appCodes)
                {
                    var item = sys_OperationBLL.GetList(UserBase.ID, app).FirstOrDefault(p => p.Code == opCodes[i]);
                    if (item != null)
                    {
                        val++; isOK = true; break;
                    }
                    i++;
                }
                if (val == 0) isOK = false;
            }
            return isOK;
        }
    }
    [HandleError]
    public class BaseUserController : Controller
    {
        private readonly string _titleFormat = "{0}-" + Setting.Instance.GetSetting("WebKeyword").Value;
        private string _title;

        protected string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public t_User UserBase
        {
            get { return t_UserBLL.GetItem(BizObject.UserID); }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            // 此处进行异常记录，可以记录到数据库或文本，也可以使用其他日志记录组件。
            // 通过filterContext.Exception来获取这个异常。
            string path = "/log/" + DateTime.Now.ToShortDateString() + "/";
            if (!Directory.Exists(Server.MapPath(path)))
                Directory.CreateDirectory(Server.MapPath(path));
            path = Server.MapPath(path + "log.txt");
            StreamWriter sw;
            if (!System.IO.File.Exists(path))
                sw = System.IO.File.CreateText(path);
            else
                sw = System.IO.File.AppendText(path);
            sw.WriteLine(DateTime.Now);
            sw.WriteLine(filterContext.Exception.Message
                + (filterContext.Exception.InnerException != null ? filterContext.Exception.InnerException.Message : ""));
            sw.WriteLine("1.错误：" + filterContext.Exception.HelpLink);
            sw.WriteLine("2.错误：" + filterContext.Exception.Source);
            sw.WriteLine("3.错误：" + filterContext.Exception.StackTrace);
            sw.WriteLine("4.错误：" + filterContext.Exception.TargetSite);
            sw.Close();

            // 执行基类中的OnException
            base.OnException(filterContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["WebName"] = Setting.Instance.GetSetting("WebName").Value;
            ViewData["WebUrl"] = Setting.Instance.GetSetting("WebUrl").Value;
            ViewData["WebCssUrl"] = Setting.Instance.GetSetting("WebCssUrl").Value;
            ViewData["WebJsUrl"] = Setting.Instance.GetSetting("WebJsUrl").Value;
            ViewData["WebImageUrl"] = Setting.Instance.GetSetting("WebImageUrl").Value;
            base.OnActionExecuting(filterContext);
        }
        public bool IsPermission(string AppCode, string OpCode)
        {
            bool isOK = false;
            t_User user = t_UserBLL.BaseUser;
            if (user != null) isOK = true;
            if (!string.IsNullOrEmpty(AppCode) && !string.IsNullOrEmpty(OpCode))
            {
                string[] appCodes = AppCode.Split(',');
                string[] opCodes = OpCode.Split(',');
                int i = 0; int val = 0;
                foreach (var app in appCodes)
                {
                    var item = sys_OperationBLL.GetList(user.ID, app).FirstOrDefault(p => p.Code == opCodes[i]);
                    if (item != null)
                    {
                        val++; isOK = true; break;
                    }
                    i++;
                }
                if (val == 0) isOK = false;
            }
            return isOK;
        }
    }
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (BizObject.UserID == 0)
                httpContext.Response.StatusCode = 403;
            else
                result = true;
            return result;　
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);　
            if (filterContext.HttpContext.Response.StatusCode == 403)
                filterContext.Result = new RedirectResult("/member/login?returnUrl=" + HttpContext.Current.Request.RawUrl);
        }
    }
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        private string _appcode;
        private string _opcode;
        public string AppCode { get { return _appcode; } set { _appcode = value; } }
        public string OpCode { get { return _opcode; } set { _opcode = value; } }
        public AdminAuthorizeAttribute() { }
        public AdminAuthorizeAttribute(string appcode, string opcode)
        {
            _appcode = appcode;
            _opcode = opcode;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isOK = false; 
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            t_User user = t_UserBLL.BaseUser;
            if (user != null) isOK = true;
            if (!string.IsNullOrEmpty(AppCode) && !string.IsNullOrEmpty(OpCode))
            {
                string[] appCodes = AppCode.Split(',');
                string[] opCodes = OpCode.Split(',');
                int i = 0; int val = 0;
                foreach (var app in appCodes)
                {
                    var item = sys_OperationBLL.GetList(user.ID, app).FirstOrDefault(p => p.Code == opCodes[i]);
                    if (item != null)
                    {
                        val++; isOK = true; break;
                    }
                    i++;
                }
                if (val == 0) isOK = false;
            }
            if (!isOK) httpContext.Response.StatusCode = 403;
            return isOK;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                JsonResult json = new JsonResult();
                json.Data = new MessageBox(false, "没有权限");
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
        }
    }
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings SerializerSettings { get; set; }
        public Newtonsoft.Json.Formatting Formatting { get; set; }

        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
              ? ContentType
              : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }
    }
}
