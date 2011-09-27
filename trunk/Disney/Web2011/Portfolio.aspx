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
    <div id="container">
        <div id="header">
            <div class="logo"></div>
            <div class="menu">
                <ul class="clearfix">
                    <li><a href="index.aspx">關于我們</a></li>
                    <li><a href="portfolio.aspx" class="current">作品集</a></li>
                    <li><a href="album.aspx">相冊介紹</a></li>
                    <li><a href="clothing.aspx">服裝</a></li>
                    <li><a href="version.aspx">版型</a></li>
                    <li><a href="contact.aspx">聯繫我們</a></li>
                </ul>
                <div class="menu_bottom"></div>
            </div>
            <div class="clear"></div>
        </div>
        <div id="main">
            <div class="content">
                <div class="content_left content_left_list">
                    <h2><label>作品集</label></h2>
                    <ul class="list"><%int id = 0; int.TryParse(Request["id"], out id); List<web_PhotoType> list = web_PhotoTypeBLL.GetList("portfolio");if(id==0 && list.Count>0)id=list[0].ID;for(int i=0;i<list.Count;i++){ %>
                        <li<%= id==list[i].ID?" class=\"current\"":""%>><a href="portfolio.aspx?id=<%= list[i].ID%>"><%= list[i].Name%></a></li><%} %>
                    </ul>
                </div>
                <div class="content_right">
                    <ul class="portfolio_list"><%List<web_Photo> oLst = web_PhotoBLL.GetList(id); foreach (web_Photo fileItem in oLst){%>
                        <li><p class="pm"><a href="portfolioshow.aspx?id=<%= fileItem.ID%>" target="_blank" title="<%= fileItem.Name%>"><img src="<%= fileItem.FilePath%>" alt="<%= fileItem.Remark%>" /></a></p><div><%= fileItem.Name%></div></li><%} %>
                    </ul>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function() {
        });
    </script>
</body>
</html>
