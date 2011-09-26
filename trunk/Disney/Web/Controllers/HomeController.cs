using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (UserBase != null)
                return Redirect("work");
            return View();
        }
        public ContentResult index1()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            var list = Common.Setting.Instance.Get("folk");
            foreach (var item in list)
            {
                str.Append("/// <summary> \n ");
                str.AppendFormat("///{0} \n ", item.Text);
                str.Append("/// </summary> \n ");
                str.AppendFormat("[Description(\"{0}\")] \n ", item.Text);
                str.AppendFormat("s{0} = {0}, \n ", item.Value);
            }
            return Content(str.ToString().TrimEnd(','));
        }
        public ContentResult index2()
        {
            string str = Common.DESEncrypt.Decrypt("6775066B52549CD3CE8D49592655979B");
            return Content(str);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ContentResult index3(string title, string url, string excerpt)
        {
            string str = string.Format("{0}\n{1}\n{2}", title, url, excerpt);
            return Content(str);
        }
    }
}
