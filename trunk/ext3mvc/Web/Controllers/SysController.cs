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
        public JsonResult wordlist(int start, int limit)
        {
            ArrayList lst = new ArrayList();
            for (int i = 0; i < 41; i++)
                lst.Add(new { ID_wod = 1 + i, Code_wod = 1999 + i, Text_wod = "订单状态" + i, Sort_wod = i });
            Hashtable dt = new Hashtable();
            dt.Add("data", lst.ToArray().Skip(start).Take(limit).ToList());
            dt.Add("total", lst.Count);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
    }
}
