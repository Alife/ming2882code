<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" CodeFile="Password.aspx.cs" Inherits="Member_Password" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="~/Shared/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register src="~/Shared/MemberMenu.ascx" tagname="MemberMenu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    修改密碼 - <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Header ID="Header1" runat="server" nowmodel="default" />
    <div id="main">
        <div class="nowplace">當前位置：<a href="<%= Application["WebUrl"]%>">主頁</a> > 修改密碼</div>
        <div class="content">
            <div class="memberleft">
                <uc2:MemberMenu ID="MemberMenu1" runat="server" nowmodel="password" />
            </div>
            <div class="memberright">
                <div class="frm">
                    <form id="frm" action="" method="post">
                    <fieldset>
                        <legend>修改密碼</legend>
                        <dl>
                            <dt>原密碼:</dt>
                            <dd><input id="currentPassword" name="currentPassword" type="password" value="" /></dd>
                            <dt>新密碼:</dt>
                            <dd><input id="newPassword" name="newPassword" type="password" value="" /></dd>
                            <dt>確認密碼:</dt>
                            <dd><input id="confirmPassword" name="confirmPassword" type="password" value="" /></dd>
                            <dt>&nbsp;</dt>
                            <dd>
                                <input id="btn" type="submit" value="保存" class="button" />
                            </dd>
                        </dl>
                    </fieldset>
                    </form>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <script type="text/javascript">
        var scope = { _pagename: "web", _pageid: "password", _devMode: 1 };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
