<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>摩尔社区-国内最专业的MES服务商</title>
    <meta http-equiv="Content-Type" content="application/xhtml+xml; charset=UTF-8" />
    <meta name="title" content="摩尔社区-国内最专业的MES服务商" />
    <meta name="keywords" content="摩尔软件|摩尔社区|mes" />
    <meta name="author" content="夫唯" />
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
            <a title="摩尔社区-国内最专业的MES服务商" href="http://mes.moresoft.com.cn/">摩尔社区-国内最专业的MES服务商</a></h1>
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
                        <li><a href="category<%= citem.ID_ift%>.html" class="active"><%= citem.Name_ift%></a></li><%if(citem.Parent_ift>0){%> <img src="images/more.gif" width="11" height="11" alt="more MES" style="border: none; vertical-align: middle;" /><%} %><%} %>
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
                    <div id="news">
                    <h2>学MES,上摩尔社区！</h2>
                    <h3><a name="news_23"></a></h3>
                    <div class="block"><p><font size="2"><strong>欢迎来到摩尔社区、</strong> <a href="/bbs/" target="_blank"><strong>MES论坛</strong></a><strong> </strong></font></p>
                    <ul><font size="1">
                    <li><font size="2">第一次接触MES，请阅读本基础指南入门，也可以注册论坛：<a href="/bbs/thread-17589-1-1.html" target="_blank">从一句话入门MES开始 </a>   </font></li>
                    <li><font size="2">已经意识到MES的重要性，渴望系统掌握并与行业精英们深入交流你可以：<a href="http://www.moresoft.cn/" target="_blank">加入MESWHY成为VIP会员</a></font></li>
                    </font>
                    </ul><p /><p /><p><font size="2">
                    <strong>什么是MES，MES是什么意思？</strong></font></p>
                    <p>MES的中文意思是搜索引擎优化。通俗理解是：通过总结搜索引擎的排名规律，对网站进行合理优化，使你的网站在百度和Google的排名提高，让搜索引擎给你带来客户。深刻理解是：通过MES这样一套基于搜索引擎的营销思路，为网站提供生态式的自我营销解决方案，让网站在行业内占据领先地位，从而获得品牌收益。</p><p>MESWHY是中国在线MES培训第一品牌，从这里开始吧。</p><p />
                    <%var indexTags = MC.BLL.IndexTag_itgBLL.GetList(new MC.Model.QueryInfo());foreach(var tag in indexTags){ %>
                    <p><strong><%= tag.Name_itg%></strong></p>
                    <ul><%var listqi = new MC.Model.QueryInfo();listqi.Parameters.Add("IndexTagID_inf", tag.ID_itg); listqi.Parameters.Add("top", "top 10"); listqi.Orderby.Add("CreateTime_inf", "desc");
                                              infos = MC.BLL.Info_infBLL.GetList(listqi); foreach (var item in infos) {%>
                    <li><a href="<%= item.InfoTypeID_inf%>_<%= item.ID_inf%>_zh.html" title="<%= item.Title_inf%>"><%= item.Title_inf%></a></li><%} %>
                    </ul>
                    <p /><p /><%} %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clearing"></div>
    <div id="footer" class="footer">
        <p id="copyrightnote"><a href="http://www.moresoft.cn/about.html">关于我们</a>  <a href="http://www.moresoft.cn/concact.html">联系我们</a>  <a href="http://www.moresoft.cn/privacy.html">法律声明</a> <a href="http://www.moresoft.cn/help.html">人文关怀</a>    <a href="http://www.moresoft.cn/">MES</a>  <a href="http://www.moresoft.cn/MEScity.html">城市MES</a>  <a href="http://www.moresoft.cn/">会员服务</a>  <a href="http://www.miibeian.gov.cn/">粤ICP备08003897号</a> </p>
    </div>
    </div>
</div>
</body>
</html>
