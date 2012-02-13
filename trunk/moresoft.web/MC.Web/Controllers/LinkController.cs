using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MC.Web.Controllers
{
    [HandleError]
    public class LinkController : Controller
    {
        public ActionResult Index(string url)
        {
            if (Url.IsLocalUrl(url))
                return Redirect(url);
            //return Redirect(Url.Action("index", "error", new { error = "操作有误" }));
            return View();
        }
    }
}
