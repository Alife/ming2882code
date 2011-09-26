<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
            <% 
                Response.Write("原始URL:" + Request.RawUrl + "<br />"); 
                Response.Write("获取的ID:" + Request.QueryString["ID"]);         
            %> 
</body>
</html>
