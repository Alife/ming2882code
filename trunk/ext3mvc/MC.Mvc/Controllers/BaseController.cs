using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MyBatis.Common.Logging;

namespace MC.Mvc.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        protected readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnException(ExceptionContext filterContext)
        {
            _logger.Error(string.Format("\r\n Unhandled exception: {0}.\r\n Stack trace: {1}\r\n{2}----------------"
                , filterContext.Exception.Message, filterContext.Exception.StackTrace, User.Identity.Name));
            base.OnException(filterContext);
        }
    }
}
