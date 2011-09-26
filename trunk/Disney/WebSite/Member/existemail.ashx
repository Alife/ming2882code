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
        string email = context.Request["email"];
        string oldemail = context.Request["oldemail"];
        bool success = false;
        if (!string.IsNullOrEmpty(oldemail) && email.ToLower() == oldemail.ToLower())
            success = true;
        else
        {
            if (t_UserBLL.IsMobileExists(email))
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