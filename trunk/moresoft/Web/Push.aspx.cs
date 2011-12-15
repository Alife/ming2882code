using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using MC.BLL;
using MC.Model;
using CoolCode.Web;

namespace Web
{
    public partial class Push : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.Form.Count > 0)
            {
                Require_req key = new Require_req();
                this.TryUpdateModel(key);
                int v = Require_reqBLL.Insert(key);
                if (v > 0)
                    Response.Write("<script>alert('提交成功');location.href = 'push.html';</script>");
                else
                    Response.Write("<script>alert('提交失败');location.href = 'push.html';</script>");
                Response.End();
            }
        }
    }
}
