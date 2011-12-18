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
    <link href="css/stylev2.css" rel="stylesheet" type="text/css" media="screen" />
    <link rel="alternate" title="News RSS Feed" type="application/rss+xml" href="feed/news/rss.aspx" />
    <link rel="alternate" title="TopTen RSS Feed" type="application/rss+xml" href="feed/topten/rss.aspx" />
    <link rel="alternate" title="Latest FAQ Records RSS Feed" type="application/rss+xml" href="feed/latest/rss.aspx" />
    <link rel="alternate" title="Open Questions RSS Feed" type="application/rss+xml" href="feed/openquestions/rss.aspx" />
</head>
<body>
    <div id="header">
        <div class="top">
            <h1>
                <a title="<%= setting.Title_set%>" href="<%= setting.WebUrl_set%>"><img src="images/v2/logo_a.gif" alt="<%= setting.Title_set%>" /></a></h1>
            <ul>
	            <li><a href="/">摩尔社区</a></li>
	            <li><a href="bbs/forum.php" target="_blank">摩尔论坛</a></li>
	            <li><a href="http://www.weibo.com/moresoft" target="_blank">新浪微博</a></li>
                <li><a href="http://t.qq.com/moresoft" target="_blank">腾讯微博</a></li>
	            <li><a href="http://www.moresoft.cn/bbs/portal.php" target="_blank">门户网站</a></li>
            </ul>
            <div class="tel"><img src="images/v2/tel.gif" alt="联系电话：400-887-4949" />400-887-4949</div>
        </div>
        <div class="bottom">
            <div class="logo"><a title="<%= setting.Title_set%>" href="<%= setting.WebUrl_set%>"><img src="images/v2/logo.gif" alt="<%= setting.Title_set%>" /></a></div>
            <div class="meun">
            <ul>
	            <li class="current"><em><a href="/">MES首页</a></em></li>
	            <li><em><a href="bbs/portal.php" target="_blank">MES资讯</a></em></li>
	            <li><em><a href="bbs/forum.php" target="_blank">MES论坛</a></em></li>
                <li><em><a href="http://www.moresoft.com.cn/main.aspx?pagetype=SYZP_CODE&logoimg=~/images/qiyezhaopin.swf&subimg=~/images/%C8%CB%B2%C5%D5%D0%C6%B8.jpg&dhName=%C6%F3%D2%B5%D5%D0%C6%B8" target="_blank">MES人才</a></em></li>
                <li><em><a href="bbs/forum.php?mod=forumdisplay&fid=40" target="_blank">MES研究院</a></em></li>
                <li><em><a href="push.html">我要发布需求</a></em></li>
            </ul>
            <form action="search.aspx" method="get" > 
                <input type="text" name="keyword" class="keyword" size="38" />
                <input type="submit" name="submit" class="search" value="搜 索" />
            </form>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <div id="wrapper">
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
                    <h3>10个MES常见问题:</h3><ol><%var commonqi = new MC.Model.QueryInfo(); commonqi.Parameters.Add("TopType_inf", "common"); commonqi.Parameters.Add("top", "top 10"); commonqi.Orderby.Add("CreateTime_inf", "desc");
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
	                <ul class="phpmyfaq_ul"><%var listqi = new MC.Model.QueryInfo(); listqi.Parameters.Add("InfoTypeID_inf", infoType.ID_ift);listqi.Orderby.Add("CreateTime_inf", "desc");infos = MC.BLL.Info_infBLL.GetList(listqi); foreach (var item in infos) { %>
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
        <a href="http://www.miibeian.gov.cn/"><%= setting.ICP_set%></a> </p>
    </div>
    </div>
</body>
</html>

