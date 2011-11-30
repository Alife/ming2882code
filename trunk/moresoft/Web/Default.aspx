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
		    <ol><%var qi = new MC.Model.QueryInfo(); qi.Parameters.Add("TopType_inf", "news"); qi.Parameters.Add("top", "top 18"); qi.Orderby.Add("CreateTime_inf", "desc");
		            var infos = MC.BLL.Info_infBLL.GetList(qi);foreach(var item in infos){ %>
		        <li><a href="<%= item.InfoTypeID_inf%>_<%= item.ID_inf%>_zh.html" title="<%= item.Title_inf%>"><%= item.Title_inf%></a></li><%} %>
		    </ol>
            </div>
                </div>
		        <div class="content">
                    <div id="topten">
                    <h3>10个MES常见问题:</h3><ol><%qi.Parameters.Add("TopType_inf", "comment"); qi.Parameters.Add("top", "top 10"); qi.Orderby.Add("CreateTime_inf", "desc");
                                              infos = MC.BLL.Info_infBLL.GetList(qi); foreach (var item in infos) { %>
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
                    <h3><a name="news_23"></a></h3><div class="block"><p><font size="2"><strong>欢迎来到摩尔社区、</strong> <a href="/bbs/" target="_blank"><strong>MES论坛</strong></a><strong> </strong></font></p><ul><font size="1"><li><font size="2">第一次接触MES，请阅读本基础指南入门，也可以注册论坛：<a href="/bbs/thread-17589-1-1.html" target="_blank">从一句话入门MES开始 </a>   </font></li><li><font size="2">已经意识到MES的重要性，渴望系统掌握并与行业精英们深入交流你可以：
                    <a href="http://www.moresoft.cn/" target="_blank">加入MESWHY成为VIP会员</a></font></li></font></ul><p /><p /><p><font size="2"><strong>什么是MES，MES是什么意思？</strong></font></p><p>MES的中文意思是搜索引擎优化。通俗理解是：通过总结搜索引擎的排名规律，对网站进行合理优化，使你的网站在百度和Google的排名提高，让搜索引擎给你带来客户。深刻理解是：通过MES这样一套基于搜索引擎的营销思路，为网站提供生态式的自我营销解决方案，让网站在行业内占据领先地位，从而获得品牌收益。</p><p>MESWHY是中国在线MES培训第一品牌，从这里开始吧。</p><p /><p><strong><font size="2">MESWHY产品和服务</font></strong></p><ul><li>MES基础指南 <a href="/course/">www.moresoft.cn/course/</a>，系统的MES优化教程，MES新手可以快速入门。 </li><li>MES答疑论坛 <a href="/bbs">www.moresoft.cn/bbs/</a>，在提问、解答中深刻理解MES优化知识。 </li><li>搜外专栏 <a href="/">www.moresoft.cn/</a>，中国MES达人投稿最新研究成果和分享经验。</li><li>MES人才库  <a href="http://job.MESwhy.com/">job.MESwhy.com/</a>，提供MES人才招聘和应聘的平台。</li><li>夫唯学院MES培训课程 <a href="http://www.moresoft.cn/">http://www.moresoft.cn/</a>，提供全面系统的MES系统培训。</li></ul><p /><p /><p><font size="2">我们提供了全面的新手入门教程，请认真对待以下内容，你会得到一个很大的提升。</font></p><p /><p><font size="2"><strong>一定要知道的MES基本概念：</strong></font></p><ul><li><a href="/3_77_zh.html" target="_blank">什么是死链接？</a>  </li><li><a href="/3_78_zh.html" target="_blank">什么是错误链接？</a> </li><li><a href="/3_102_zh.html" target="_blank">什么叫反向链接？</a>  </li><li><a href="/12_103_zh.html" target="_blank">如何查看反向链接更准确？</a></li><li><a href="/37_97_zh.html" target="_blank">black hat-黑帽</a></li><li><a href="/43_62_zh.html" target="_blank">Sandbox-沙盒效应</a></li><li><a href="/37_15_zh.html" target="_blank">Alexa排名是什么？</a></li><li><a href="/37_55_zh.html" target="_blank">ALT-代替属性</a></li><li><a href="/1_100_zh.html" target="_blank">link和domain的区别</a></li><li><a href="/2_76_zh.html" target="_blank">什么是长尾关键词？</a></li><li><a href="/2_74_zh.html" target="_blank">目标关键词是什么意思？</a></li><li><a href="/2_73_zh.html" target="_blank">如何进行关键词分析？</a></li><li><a href="/2_44_zh.html" target="_blank">关键词密度多少比较好？</a></li><li><a href="/2_43_zh.html" target="_blank">写网页内容需要注意些什么？</a></li><li><a href="/1_46_zh.html" target="_blank">几大搜索引擎的网站登录入口</a> </li><li><a href="/1_52_zh.html" target="_blank">做网站该注意哪些基本要素？</a> </li><li><a href="/1_69_zh.html" target="_blank">MES一般有哪些步骤或环节？</a> </li><li><a href="/1_39_zh.html" target="_blank">网站被百度和Google封了，怎么办？</a></li></ul><p /><p><font size="2"><strong>我们推荐的常用MES工具：</strong></font></p><ul><li><a href="/6_57_zh.html" target="_blank">Google网站管理员工具</a>   </li><li><a href="/12_105_zh.html" target="_blank">Googlebot 有哪几种？</a> </li><li><a href="/6_79_zh.html" target="_blank">Xenu-死链接检测工具</a> </li><li><a href="/6_82_zh.html" target="_blank">阿里妈妈站长工具</a> </li><li><a href="/6_81_zh.html" target="_blank">Google网站流量统计</a>    </li><li><a href="/14_95_zh.html" target="_blank">Google网站分析工具功能详解</a></li></ul><p /><p /><p><font size="2"><strong>容易犯的MES作弊错误：</strong></font></p><ul><li><a href="/8_48_zh.html" target="_blank">博客群发会被K掉吗？</a></li><li><a href="/8_50_zh.html" target="_blank">域名轰炸</a></li><li><a href="/8_68_zh.html" target="_blank">关键词叠加</a></li><li><a href="/8_70_zh.html" target="_blank">隐藏文本和链接</a></li></ul><p /><p><strong><font size="2">常用的MES技巧：</font></strong></p><ul><li><a href="/5_71_zh.html" target="_blank">提高关键词排名的28个MES技巧</a></li><li><a href="/5_72_zh.html" target="_blank">增加反向链接的35个技巧</a></li><li><a href="/10_106_zh.html" target="_blank">nofollow属性的介绍和使用</a></li></ul><p /><p><font size="2"><strong>MESWHY论坛，致力于打造最佳MES咨询互助论坛，老会员说：</strong></font></p><ul><li><a href="/bbs/thread-1117-1-1.html" target="_blank">我对MES一点也不了解，我应该从何学起？</a></li><li><a href="/bbs/thread-8366-1-1.html" target="_blank">了解MESWHY，我们将一直往这个方向发展</a></li><li><a href="/bbs/thread-969-1-2.html" target="_blank">会员说：自从来了这个站,其它的MES站再也没去了</a></li><li><a href="/bbs/thread-832-1-1.html" target="_blank">使用MES论坛的会员感受万分</a></li><li><a href="/bbs/thread-1059-1-1.html" target="_blank">夫唯老师分享MES-WHY经验</a></li><li><a href="/bbs/thread-11821-1-1.html" target="_blank">加入MESWHY，系统学习MES，并结识大量MES精英</a></li></ul><p /><p><strong><font size="2">MESWHY成功VIP会员访谈录：</font></strong></p><ul><li><font size="2"><a href="http://www.moresoft.cn/285.html" target="_blank">访谈系列A — 迷茫：我们可以用MES来做什么？</a></font></li><li><a href="http://www.moresoft.cn/275.html" target="_blank">访谈系列B — 思维：偏执，做到极致！探索营销2.0</a></li><li><a href="http://www.moresoft.cn/281.html" target="_blank">访谈系列C — 创业：每只菜鸟都要有鹰的梦想……</a></li><li><a href="http://www.moresoft.cn/393.html" target="_blank">访谈系列D — 就职：我的成功来自选择了做英文MES就业的自我定位</a></li></ul><p />
                    </div><div class="date">2011-10-02</div>
                    <p align="center">当前共108篇MES基础指南，更多精彩请进入<a href="http://www.moresoft.cn/bbs/" >MES论坛</a></p>
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
