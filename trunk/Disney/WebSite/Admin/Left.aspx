<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" %>

<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MeteContent" runat="Server">

    <script type="text/javascript">
        $(function() {
            $('.leftMenu li').each(function() {
                $(this).click(function() {
                    var key = $(this).attr('key');
                    $('.leftMenu li').each(function() { $(this).removeClass('default'); });
                    $(this).addClass('default');
                    var mainFrame = self.parent.frames['mainFrame'];
                    mainFrame.location = key;
                });
            });
        });</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="leftMenu-bg layout">
        <ul class="leftMenu">
            <li class="default" key="main.aspx"><a href="javascript:;">管理中心</a></li>            <li key="users/user.aspx"><a href="javascript:;">會員管理</a></li>
            <li key="orders/order.aspx"><a href="javascript:;">訂單管理</a></li>            <li key="info/article.aspx"><a href="javascript:;">內容管理</a></li>
        </ul>
    </div>
</asp:Content>
