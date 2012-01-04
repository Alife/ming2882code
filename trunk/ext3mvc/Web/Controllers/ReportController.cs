using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Aspose.Cells;

namespace Web.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult down()
        {
            var s = Aspose.Cells.CellsHelper.GetVersion();
            var sql = @"select * from [客户] where 客户ID ='VINET'";
            var dt = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, sql).Tables[0];
            dt.TableName = "Customers";
            var order = DALHelper.DBHelper.ExecuteQuery(CommandType.Text,
                    @"select [订单].*,[订单明细].* from [订单] left join [订单明细] on [订单].订单ID=[订单明细].订单ID
                    where [订单].订单ID=10001").Tables[0];
            order.TableName = "Order";
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/ReportTemplate/Aspose.CellsTemplate1.xls"));
            //数据源 
            designer.SetDataSource(dt);
            designer.SetDataSource(order);
            //报表单位 
            designer.SetDataSource("ReportUtils", "xxxxx有限公司客户信息");
            designer.SetDataSource("ReportAdd", "London");
            //截止日期 
            designer.SetDataSource("ReportDate", DateTime.Now.ToString("yyyy年MM月dd日"));

            designer.Process();
            //designer.Save(Server.MapPath("~/ReportTemplate/report.xls"), FileFormatType.Excel2003);
            designer.Save("_report.xls", SaveType.OpenInExcel, FileFormatType.Excel2003, System.Web.HttpContext.Current.Response);
            Response.Flush();
            Response.Close();
            designer = null;
            Response.End();
            return View();
        }
    }
}
