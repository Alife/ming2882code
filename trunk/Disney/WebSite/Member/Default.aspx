<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MemberSite.Master" %>
<%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %>
<%@ Register src="~/Shared/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Shared/MemberMenu.ascx" tagname="MemberMenu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    會員中心 - <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <uc1:Header ID="Header1" runat="server" nowmodel="default" />
    <div id="main"><%int uid = BizObject.UserID; t_User user = t_UserBLL.GetItem(uid);  %>
        <div class="nowplace">當前位置：<a href="<%= Application["WebUrl"]%>">主頁</a> > 會員中心</div>
        <div class="content">
            <div class="memberleft">
                <uc2:MemberMenu ID="MemberMenu1" runat="server" nowmodel="default" />
            </div>
            <div class="memberright">
                <div class="frm">
                    <fieldset>
                        <legend>會員資料</legend>
                        <dl>
                            <dt for="username">會員帳號:</dt>
                            <dd class="frmtext"><%= user.UserName%></dd>
                            <dt for="email">Email:</dt>
                            <dd class="frmtext"><%= user.Email%></dd>
                            <dt for="email">姓名:</dt>
                            <dd class="frmtext"><%= user.TrueName%></dd>
                            <dt for="city">縣市:</dt>
                            <dd class="frmtext"><%= sys_AreaBLL.GetItem(user.CountryID).Name%></dd>
                        </dl>
                    </fieldset>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
