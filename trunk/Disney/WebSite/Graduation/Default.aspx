<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %>
<%@ Register src="~/Shared/Header.ascx" tagname="Header" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"><uc1:Header ID="Header1" runat="server" nowmodel="graduation" />
    <div id="main">
        <div class="graduation_c">
            <ul>
                <li><img src="<%= Application["WebImageUrl"] %>graduation/image-1_01.jpg" alt="實現夢想，迪士尼主角換你當" /></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=1" title="服裝介紹 男"><img src="<%= Application["WebImageUrl"] %>graduation/image-1_02.jpg" alt="服裝介紹 男" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=2" title="服裝介紹 女"><img src="<%= Application["WebImageUrl"] %>graduation/image-1_03.jpg" alt="服裝介紹 女" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=3" title="團體照版型"><img src="<%= Application["WebImageUrl"] %>graduation/image-1_04.jpg" alt="團體照版型" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=4" title="同學錄版型"><img src="<%= Application["WebImageUrl"] %>graduation/image-1_05.jpg" alt="同學錄版型" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=5" title="生活小組照版型"><img src="<%= Application["WebImageUrl"] %>graduation/image-1_06.jpg" alt="生活小組照版型" /></a></li>
            </ul>
            <div class="clear"></div>
            <div class="contact">
                <div>
                    <p>台灣獨家代理迪士尼畢業寫真專輯</p>
                    <p>02-22324893</p>
                    <p>cskuo@disneyphoto.com.tw</p>
                </div>
                <img src="<%= Application["WebImageUrl"] %>graduation/image-1_07.jpg" alt="聯繫我們" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
