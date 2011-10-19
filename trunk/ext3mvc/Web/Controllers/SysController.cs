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
        [AcceptVerbs(HttpVerbs.Post)]
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
        public FileContentResult exportexcel(string exportContent)
        {
            Response.AddHeader("Pragma", "public");
            Response.AddHeader("Expires", " 0");
            Response.AddHeader("Cache-Control", " must-revalidate, post-check=0, pre-check=0");
            Response.AddHeader("Content-Type", "application/force-download");
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + DateTime.Now.ToShortDateString() + ".xls\"");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            return File(System.Text.Encoding.GetEncoding("utf-8").GetBytes(exportContent), "application/vnd.ms-excel");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult wordedit()
        {
            return Json(new { success = true, msg = "成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}
