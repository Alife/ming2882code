<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" CodeFile="Register.aspx.cs" Inherits="Member_Register" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="~/Shared/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    會員註冊 - <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Header ID="Header1" runat="server" nowmodel="default" />
    <div id="main">
        <div class="nowplace">當前位置：<a href="<%= Application["WebUrl"]%>">主頁</a> > 會員註冊</div>
        <div class="register frm">
            <form id="frm" action="" method="post">
            <fieldset>
                <legend>會員資料</legend>
                <dl>
                    <dt for="username">會員帳號:</dt>
                    <dd>
                        <input id="username" name="username" type="text" value="" />
                    </dd>
                    <dt for="email">Email:</dt>
                    <dd>
                        <input id="email" name="email" type="text" value="" />
                    </dd>
                    <dt for="password">會員密碼:</dt>
                    <dd>
                        <input id="password" name="password" type="password" value="" />
                    </dd>
                    <dt for="confirmPassword">確認密碼:</dt>
                    <dd>
                        <input id="confirmPassword" name="confirmPassword" type="password" value="" />
                    </dd>
                    <dt for="email">姓名:</dt>
                    <dd>
                        <input id="truename" name="truename" type="text" value="" />
                    </dd>
                    <dt for="city">縣市:</dt>
                    <dd>
                        <select id="countryid" name="countryid"><%List<sys_Area> areaList = sys_AreaBLL.GetList(32, 1); foreach (var areaItem in areaList){%>
                            <option value="<%= areaItem.ID%>"><%= areaItem.Name%></option><%} %>
                        </select>
                    </dd>
                    <dt for="sex">性別:</dt>
                    <dd>
                        <input id="sex1" name="sex" type="radio" value="1" />女
                        <input id="sex2" name="sex" type="radio" value="2" />男
                        <label for="sex" generated="true" class="error hide">請選擇性別</label>
                    </dd>
                    <dt for="birthday">生日:</dt>
                    <dd>
                        <input id="birthday" name="birthday" type="text" value="" />
                    </dd>
                    <dt for="tel">移動手機:</dt>
                    <dd>
                        <input id="mobile" name="mobile" type="text" value="" />
                    </dd>
                    <dt for="tel">聯絡電話:</dt>
                    <dd>
                        <input id="tel" name="tel" type="text" value="" style="width:280px" />多個請以,隔開
                        <label for="tel" generated="true" class="error hide">请填写电话号(可以是固定电话或手机)</label>
                    </dd>
                    <dt for="address">通訊地址:</dt>
                    <dd>
                        <input id="address" name="address" type="text" value="" style="width:280px" />  郵編:<input id="zip" name="zip" type="text" value="" style="width:50px;" />
                    </dd>
                    <dt for="tel">訂閱電子報:</dt>
                    <dd>
                        <input id="isemail" name="isemail" type="checkbox" value="true" />是否願收取電子報
                    </dd>
                    <dt for="botton">&nbsp;</dt>
                    <dd>
                        <input name="returnUrl" value="<%= Request["returnUrl"]%>" type="hidden" />
                        <input id="btn" type="submit" value="註冊" class="button" />
                    </dd>
                </dl>
            </fieldset>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        var scope = { _pagename: "web", _pageid: "register", _devMode: 1 };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
