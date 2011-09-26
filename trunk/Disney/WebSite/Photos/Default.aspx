<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %>
<%@ Register src="~/Shared/Header.ascx" tagname="Header" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><uc1:Header ID="Header1" runat="server" nowmodel="photos" />
    <div id="main">
        <div class="nowplace">當前位置：<a href="<%= Application["WebUrl"]%>">主頁</a> > 摄影作品</div>
        <div class="content">
            <div class="photosleft">
                <ul>
                    <li><a href="<%= Application["WebUrl"] %>photos/?id=1" title="服裝介紹 男">服裝介紹 男</a></li>
                    <li><a href="<%= Application["WebUrl"] %>photos/?id=2" title="服裝介紹 女">服裝介紹 女</a></li>
                    <li><a href="<%= Application["WebUrl"] %>photos/?id=3" title="團體照版型">團體照版型</a></li>
                    <li><a href="<%= Application["WebUrl"] %>photos/?id=4" title="同學錄版型">同學錄版型</a></li>
                    <li><a href="<%= Application["WebUrl"] %>photos/?id=5" title="生活小組照版型">生活小組照版型</a></li>
                </ul>
            </div>
            <div class="photosright">
                <ul class="gallery clearfix">
                    <li><p class="pm"><a href="http://localhost:4214/images/graduation/boy/01.jpg" title="1" rel="prettyPhoto[1]"><img src="http://localhost:4214/images/graduation/boy/01.jpg" alt="boy" title="01" /></a></p></li>
                    <li><p class="pm"><a href="http://localhost:4214/images/graduation/boy/02.jpg" title="2" rel="prettyPhoto[1]"><img src="http://localhost:4214/images/graduation/boy/02.jpg" alt="boy" title="01" /></a></p></li>
                    <li><p class="pm"><a href="http://localhost:4214/images/graduation/boy/03.jpg" title="3" rel="prettyPhoto[1]"><img src="http://localhost:4214/images/graduation/boy/03.jpg" alt="boy" title="01" /></a></p></li>
                    <li><p class="pm"><a href="http://localhost:4214/images/graduation/boy/04.jpg" title="4" rel="prettyPhoto[1]"><img src="http://localhost:4214/images/graduation/boy/04.jpg" alt="boy" title="01" /></a></p></li>
                </ul>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <script>
        $(".gallery:first a[rel^='prettyPhoto']").prettyPhoto({ animationSpeed: 'slow', theme: 'light_square', slideshow: 5000, autoplay_slideshow: true });
        $(".gallery:gt(0) a[rel^='prettyPhoto']").prettyPhoto({ animationSpeed: 'fast', slideshow: 10000 });</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="<%= Application["WebCssUrl"]%>prettyPhoto.css" type="text/css" rel="stylesheet" />
    <script src="<%= Application["WebJsUrl"]%>jquery.prettyPhoto.js" type="text/javascript"></script>
</asp:Content>
