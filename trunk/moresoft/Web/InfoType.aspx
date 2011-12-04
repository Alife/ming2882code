<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoType.aspx.cs" Inherits="Web.InfoType" %>
<%MC.Model.InfoType_ift infoType = MC.BLL.InfoType_iftBLL.GetItem(Web.ReqHelper.Get<int>("id")); %>
<%MC.Model.Setting_set setting = (MC.Model.Setting_set)Application["setting"]; %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><%= infoType.Name_ift%>-<%= setting.WebName_set%></title>
    <meta http-equiv="Content-Type" content="application/xhtml+xml; charset=UTF-8" />
    <meta name="title" content="<%= infoType.Name_ift%>-<%= setting.Title_set%>" />
    <meta name="keywords" content="<%= infoType.Keywords_ift%>" />
    <meta name="author" content="<%= setting.Author_set%>" />
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="css/Style.css" rel="stylesheet" type="text/css" media="screen" />
    <link rel="alternate" title="News RSS Feed" type="application/rss+xml" href="feed/news/rss.aspx" />
    <link rel="alternate" title="TopTen RSS Feed" type="application/rss+xml" href="feed/topten/rss.aspx" />
    <link rel="alternate" title="Latest FAQ Records RSS Feed" type="application/rss+xml" href="feed/latest/rss.aspx" />
    <link rel="alternate" title="Open Questions RSS Feed" type="application/rss+xml" href="feed/openquestions/rss.aspx" />
    <!--[if lt IE 7]>
	<script type="text/javascript" src="js/ie_png.js"></script>  
	<script type="text/javascript">ie_png.fix('#header h1 a,#header form input.keyword,#header form input.search,');</script>	
	<![endif]-->
</head>
<body>
    <div id="wrapper1">
    <div id="wrapper2">
    <div class="header" id="header">
        <h1>
            <a title="摩尔社区-国内最专业的MES服务商" href="<%= setting.WebUrl_set%>">摩尔社区-国内最专业的MES服务商</a></h1>
        <ul>
            <li class="frist"></li>
	        <li class="current"><a href="/" target="_blank">MES首页</a></li>
	        <li><a href="/info/"target="_blank">MES资讯</a></li>
	        <li><a href="/bbs/"target="_blank">MES论坛</a></li>
            <li><a href="/job/"target="_blank" >MES人才</a></li>
            <li><a href="/edu/"target="_blank">MES研究院</a></li>
            <li><a href="/push/"target="_blank">我要发布需求</a></li>
            <li class="last"></li>
        </ul>
        <form action="search.aspx" method="get" > 
            <input type="text" name="keyword" class="keyword" size="38" />
            <input type="submit" name="submit" class="search" value="" />
        </form>
    </div>
    <div class="columns">
        <div class="leftcolumn sidebar" id="sidebar-left">
            <div class="leftpadding">
                <div class="content">
                    <div id="categories">
                    <ul>
                        <li class="home"><a href="/">MES基础指南首页</a></li><%var clist = MC.BLL.InfoType_iftBLL.GetList(new MC.Model.QueryInfo());foreach(var citem in clist){ %>
                        <li><a href="category<%= citem.ID_ift%>.html"<%= Web.ReqHelper.Get<int>("id")==citem.ID_ift?" class=\"active\"":""%>><%= citem.Name_ift%></a></li><%if(citem.Parent_ift>0){%> <img src="images/more.gif" width="11" height="11" alt="more MES" style="border: none; vertical-align: middle;" /><%} %><%} %>
                        <li><a href="/sitemap-A_zh.html">Sitemap</a></li>
                    </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="rightcolumn sidebar" id="sidebar-right">
            <div class="rightpadding">
		        <div class="content">
		            <div id="latest">
		            <h3>18个MES新问题:&nbsp;<a href="feed/latest/rss.aspx" target="_blank"><img src="images/rss.png" width="28" height="16" alt="MES RSS" /></a></h3>
		            <ol><%var newqi = new MC.Model.QueryInfo(); newqi.Parameters.Add("TopType_inf", "news"); newqi.Parameters.Add("top", "top 18"); newqi.Orderby.Add("CreateTime_inf", "desc");
		                    var infos = MC.BLL.Info_infBLL.GetList(newqi);foreach(var item in infos){ %>
		                <li><a href="<%= item.InfoTypeID_inf%>_<%= item.ID_inf%>_zh.html" title="<%= item.Title_inf%>"><%= item.Title_inf%></a></li><%} %>
		            </ol>
                    </div>
                </div>
		        <div class="content">
                    <div id="topten">
                    <h3>10个MES常见问题:</h3><ol><%var commonqi = new MC.Model.QueryInfo(); commonqi.Parameters.Add("TopType_inf", "comment"); commonqi.Parameters.Add("top", "top 10"); commonqi.Orderby.Add("CreateTime_inf", "desc");
                                              infos = MC.BLL.Info_infBLL.GetList(commonqi); foreach (var item in infos) { %>
	                    <li><%= item.Hits_inf%> 次阅读: <br /><a href="<%= item.InfoTypeID_inf%>_<%= item.ID_inf%>_zh.html" title="<%= item.Title_inf%>"><%= item.Title_inf%></a></li><%} %>
                    </ol>
                    </div>
                </div>
            </div>
        </div>
        <div class="centercolumn">
            <div class="centerpadding">
                <div class="main-content" id="main">
                    <h2><%= infoType.Name_ift%></h2>
	                <ul class="phpmyfaq_ul"><%var listqi = new MC.Model.QueryInfo(); listqi.Parameters.Add("InfoTypeID_inf", infoType.ID_ift);infos = MC.BLL.Info_infBLL.GetList(listqi); foreach (var item in infos) { %>
	                <li><a href="<%= item.InfoTypeID_inf%>_<%= item.ID_inf%>_zh.html" title="<%= item.Title_inf%>"><%= item.Title_inf%></a><br /><div class="little">(<%= item.Hits_inf%> 次阅读)</div></li><%} %>
	                </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="clearing"></div>
    <div id="footer" class="footer">
        <p id="copyrightnote">
        <a href="about.html">关于我们</a>
        <a href="concact.html">联系我们</a>
        <a href="privacy.html">法律声明</a>
        <a href="help.html">人文关怀</a>
        <a href="http://www.moresoft.com.cn/">摩尔软件</a>
        <a href="bbs/">MES论坛</a>
        <a href="http://www.miibeian.gov.cn/">粤ICP备08003897号</a> </p>
    </div>
    </div>
</div>
</body>
</html>

