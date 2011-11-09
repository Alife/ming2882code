<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>童話世界</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.ad-gallery.pack.js" type="text/javascript"></script>
    <style type="text/css">body{background:#255583 url(images/d/hhbg_3.jpg) no-repeat center 0;}</style>
    <!--[if lt IE 7]>
	<link rel="stylesheet" type="text/css" href="css/ie_style.css" />
	<script type="text/javascript" src="js/ie_png.js"></script>  
	<script type="text/javascript">ie_png.fix('#header .logo,#header .menu,#header .menu li a:hover,#header .menu li a.current,#main .content .content_left h2,#main .content .content_left h2 label,#main .content .content_left h2.current label,#main .content .content_left ul li,#main .content .content_left ul li a,#main .content .content_left ul li a:hover,#main .content .content_left ul li.current a');</script>	
	<![endif]-->
</head>
<body>
<%int id = 0; int.TryParse(Request["id"], out id);web_Photo fileItem = web_PhotoBLL.GetItem(id);%>
    <img src="<%= fileItem.FilePath%>" alt="<%= fileItem.Remark%>" />
</body>
</html>
