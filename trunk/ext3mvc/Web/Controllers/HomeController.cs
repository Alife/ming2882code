using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getUserButtons(int sysMenuId)
        {
            ArrayList lst = new ArrayList();
            lst.Add(new { nodeId = 1, menuName = "功能测试", actionPath = "" });
            lst.Add(new { nodeId = 2, menuName = "功能测试2", actionPath = "" });
            lst.Add(new { nodeId = 3, menuName = "功能测试2", actionPath = "" });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUserTree(int parantNodeId, string menuName)
        {
            ArrayList lst = new ArrayList();
            lst.Add(new { id = 1, text = "动态GRID", jsUrl = "/js/view/DynamicGrid.js;/js/view/fundyngird.js", leaf = true, path = 1, type = "jsclass", namespace1 = "com.ms.basic.DynamicGrid", mainClass = "com.ms.basic.DynamicGrid" });
            lst.Add(new { id = 2, text = "功能测试2", jsUrl = "/js/view/ResourcePanel.js", leaf = true, path = 1, type = "jsclass", namespace1 = "com.ms.basic.ResourcePanel", mainClass = "com.ms.basic.ResourcePanel" });
            lst.Add(new { id = 3, text = "功能测试3", jsUrl = "/js/view/ResourcePanel1.js", leaf = true, path = 1, type = "jsclass", namespace1 = "com.ms.basic.ResourcePanel1", mainClass = "com.ms.basic.ResourcePanel1" });
            lst.Add(new { id = 4, text = "功能测试4", jsUrl = "", url = "/home/loadxmlGrid", leaf = true, path = 1, type = "loadjs", namespace1 = "wod_Word_Grid_Panel", mainClass = "wod_Word_Grid_Panel" }); 
            if (parantNodeId != 3)
                return Json(lst, JsonRequestBehavior.AllowGet);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ContentResult loadxmlGrid()
        {
            ReaderXml x = new ReaderXml();
            XPathDocument xpathDoc = x.RenderGridXML("wod_Word_Grid", "10000");
            string xslPath = Server.MapPath("/XSLT/Grid.xslt");
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ProhibitDtd = false;
            readerSettings.CloseInput = true;
            XmlReader reader = XmlReader.Create(xslPath, readerSettings);
            XslCompiledTransform transform = new XslCompiledTransform(true);
            XsltArgumentList argsList = new XsltArgumentList();
            XsltSettings setting = new XsltSettings(true, true);
            transform.Load(reader, new XsltSettings(true, true), new XmlUrlResolver());
            transform.Transform(xpathDoc, argsList, Response.Output);
            reader.Close();
            return Content("");
        }
        public ContentResult dynamicGrid()
        {
            string json = @"{
	                'metaData': {
		                'totalProperty': 'total',
		                'root': 'records',
		                'id': 'id',
		                'fields': [
			                {'name': 'id', 'type': 'int'},
			                {'name': 'name', 'type': 'string'},
			                {'name': 'code', 'type': 'string'},
			                {'name': 'dept', 'type': 'string'},
			                {'name': 'email', 'type': 'string'},
			                {'name': 'ip', 'type': 'string'}
		                ]
	                },
	                'success': true,
	                'total': 21,
	                'records': [
		                {'id': '1', 'name': 'AAA','code':'23129','dept':'开发部','email':'sks@llss.cm','ip':'127.39.39.0'},
		                {'id': '2', 'name': 'BBB','code':'2383823','dept':'行政部','email':'sksk@ks.com','ip':'28.32.29.29'}
	                ],
	                'columns': [
		                {'header': 'id', 'dataIndex': 'id',width:30},
		                {'header': 'User', 'dataIndex': 'name',width:80},
		                {'header': 'code', 'dataIndex': 'code',width:60},
		                {'header': 'dept', 'dataIndex': 'dept',width:60},
		                {'header': 'email', 'dataIndex': 'email',width:80},
		                {'header': 'ip', 'dataIndex': 'ip',width:60}
	                ]
                }";
            return Content(json);
        }
        public JsonResult LoginAction()
        {
            return Json(new { realName = "admin", userDeptName = "IT部" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ResourcesList()
        {
            ArrayList lst = new ArrayList();
            lst.Add(new { id = 1, nodeId = 0, parantNodeID = 0, menuName = "动态GRID", actionPath = "", jsClassFile = "/js/view/DynamicGrid.js;/js/view/fundyngird.js", icon = "icon-add", menuOrder = 1, openIcon = "JSClass", description = "com.ms.basic.DynamicGrid", mainClass = "com.ms.basic.DynamicGrid" });
            lst.Add(new { id = 2, nodeId = 0, parantNodeID = 0, menuName = "功能测试2", actionPath = "", jsClassFile = "/js/view/ResourcePanel.js", icon = "icon-edel", menuOrder = 1, openIcon = "JSClass", description = "com.ms.basic.ResourcePanel", mainClass = "com.ms.basic.ResourcePanel" });
            Hashtable dt = new Hashtable();
            dt.Add("data", lst);
            dt.Add("total", 2);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
    }
}
