using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DataController : Controller
    {
        public ActionResult viewBaseGrid()
        {
            return View();
        }
        public JsonResult datagrid_data(int? page, int rows, string sort, string order)
        {
            //page = (page ?? 0) == 1 ? 0 : page;
            datagridDataList lst = new datagridDataList();
            lst.total = 100;
            for (int i = 0; i < 100; i++)
                lst.rows.Add(new datagridData { id = i, code = "code" + i, addr = "address" + i, name = "name" + i, col4 = "col4" + i });
            lst.rows = lst.rows.Skip((page.Value - 1) * rows).Take(rows).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult loaduserinfo()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.users.name = "asb";
            userInfo.users.email = "est@tes.com";
            userInfo.users.subject = "你好";
            userInfo.infos.language = 1;
            userInfo.infos.message = "呵呵";
            return Json(userInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adduserinfo()
        {
            UserInfo userInfo = new UserInfo();
            TryUpdateModel(userInfo.users, Request.Form.AllKeys);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult combobox_data()
        {
            List<comboboxData> lst = new List<comboboxData>();
            lst.Add(new comboboxData { id = 1, text = "中文" });
            lst.Add(new comboboxData { id = 2, text = "英文" });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
    public class datagridDataList
    {
        public datagridDataList() { total = 0; rows = new List<datagridData>(); }
        public int total { get; set; }
        public List<datagridData> rows { get; set; }
    }
    public class datagridData
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string addr { get; set; }
        public string col4 { get; set; }
    }
    public class comboboxData
    {
        public int id { get; set; }
        public string text { get; set; }
    }
    public class Users
    {
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
    }
    public class Infos
    {
        public string message { get; set; }
        public int language { get; set; }
    }
    public class UserInfo
    {
        public UserInfo() { users = new Users(); infos = new Infos(); }
        public Users users { get; set; }
        public Infos infos { get; set; }
    }
}
