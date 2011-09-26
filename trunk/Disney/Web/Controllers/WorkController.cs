using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Web.Controllers
{
    [HandleError]
    public class WorkController : BaseController
    {
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
