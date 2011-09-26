<%@ WebHandler Language="C#" Class="existuser" %>

using System;
using System.Web;
using Common;
using BLL;
using Models;
using Newtonsoft.Json;

public class existuser : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        context.Response.ContentType = "text/plain";
        string userName = context.Request["userName"];
        string oldname = context.Request["oldname"];
        bool success = false;
        if (!string.IsNullOrEmpty(oldname) && userName.ToLower() == oldname.ToLower())
            success = true;
        else
        {
            if (t_UserBLL.IsUserNameExists(userName))
                success = false;
            else
                success = true;
        }
        context.Response.Write(success.ToString().ToLower());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}