<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" CodeFile="Login.aspx.cs" Inherits="Member_Login" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="~/Shared/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    會員登錄 - <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Header ID="Header1" runat="server" nowmodel="default" />
    <div id="main">
        <div class="nowplace">當前位置：<a href="<%= Application["WebUrl"]%>">主頁</a> > 會員登錄</div>
        <div class="content frm">
            <form id="frm" action="" method="post">
            <fieldset>
                <legend>會員登錄</legend>
                <dl>
                    <dt for="username">會員帳號:</dt>
                    <dd>
                        <input id="username" name="username" type="text" value="" />
                    </dd>
                    <dt for="password">會員密碼:</dt>
                    <dd>
                        <input id="password" name="password" type="password" value="" />
                    </dd>
                    <dt></dt>
                    <dd><input id="rememberMe" name="rememberMe" type="checkbox" value="true" /> 记住登录?</dd>
                    <dt for="botton">&nbsp;</dt>
                    <dd>
                        <input name="returnUrl" value="<%= Request["returnUrl"]%>" type="hidden" />
                        <input id="btn" type="submit" value="登錄" class="button" />
                    </dd>
                </dl>
            </fieldset>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        var scope = { _pagename: "web", _pageid: "login", _devMode: 1 };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
