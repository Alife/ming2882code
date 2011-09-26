<%@ Application Language="C#" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="System.IO" %>
<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        Application["WebName"] = Setting.Instance.GetSetting("WebName").Value;
        Application["WebUrl"] = Setting.Instance.GetSetting("WebUrl").Value;
        Application["WebCssUrl"] = Setting.Instance.GetSetting("WebCssUrl").Value;
        Application["WebJsUrl"] = Setting.Instance.GetSetting("WebJsUrl").Value;
        Application["WebImageUrl"] = Setting.Instance.GetSetting("WebImageUrl").Value;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        Exception ex = Server.GetLastError().GetBaseException();
        string path = "/log/" + DateTime.Now.ToShortDateString() + "/";
        if (!Directory.Exists(Server.MapPath(path)))
            Directory.CreateDirectory(Server.MapPath(path));
        path = Server.MapPath(path + "log.txt");
        StreamWriter sw;
        if (!System.IO.File.Exists(path))
            sw = System.IO.File.CreateText(path);
        else
            sw = System.IO.File.AppendText(path);
        sw.WriteLine(DateTime.Now);
        sw.WriteLine(Request.Url.ToString());
        sw.WriteLine(ex.Message);
        sw.Close();
    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
