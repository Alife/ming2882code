using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SysController : Controller
    {
        public JsonResult wordlist()
        {
            ArrayList lst = new ArrayList();
            lst.Add(new { ID_wod = 1, Code_wod = "1999", Text_wod = "订单状态", Sort_wod = 1 });
            lst.Add(new { ID_wod = 2, Code_wod = "3229", Text_wod = "生肖", Sort_wod = 2 });
            Hashtable dt = new Hashtable();
            dt.Add("data", lst);
            dt.Add("total", 2);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
    }
}
