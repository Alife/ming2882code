using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport;
using FastReport.Web;

namespace Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebReport1.Prepare();
        }

        protected void WebReport1_StartReport(object sender, EventArgs e)
        {
            Report frport = (sender as WebReport).Report;
        }
    }
}
