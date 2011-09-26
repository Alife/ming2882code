<%@ Control Language="C#" ClassName="Header" %><%@ Import Namespace="System.Collections.Generic"%><%@ Import Namespace="System.Linq"%>
<%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %>
<script runat="server">
    public string nowmodel { get; set; }
</script>
            <div id="header">
                <div class="top">
                    <div class="logo">
                        <a href="<%= Application["WebUrl"] %>" title="首页"></a>
                    </div>
                    <div class="topright"><%int uid = BizObject.UserID; %>
                        <ul class="loginstate"><%if(uid==0){ %>
                            <li class="li1">
                                <form id="memloginfrm" action="" method="post">
                                    <ol class="ol1">賬號:</ol>
                                    <ol><input name="username" size="11" maxlength="20" /></ol>
                                    <ol class="ol1">密碼:</ol>
                                    <ol><input name="password" type="password" size="11" maxlength="20" /></ol>
                                    <ol class="ol2"><img id="memlogin" src="<%= Application["WebImageUrl"] %>userlogin.gif" style="cursor: pointer" /></ol>
                                </form>
                            </li>
                            <li>| <a href="<%= Application["WebUrl"] %>member/register.aspx">加入會員</a></li>
                            <li>| <a href="<%= Application["WebUrl"] %>member/login.aspx">會員登錄</a></li>
                            <li>| <a href="<%= Application["WebUrl"] %>member/forgetpwd.aspx">忘記密碼</a></li><%}else{t_User user = t_UserBLL.GetItem(uid); %>
                            <li>歡迎<%= user.TrueName%></li>
                            <li> | <a href="<%= Application["WebUrl"] %>member">管理中心</a></li>
                            <li> | <a href="<%= Application["WebUrl"] %>member/logout.aspx">退出登錄</a></li><%} %>
                        </ul>
                        <div class="clear"></div>
                        <form action="<%= Application["WebUrl"] %>search" method="get" name="search_news" class="topseach">
                            <input class="txtBox" name="keyboard" value="請輸入搜索關鍵詞" type="text">
                            <input class="go" type="submit" name="Submit2" value="GO">
                        </form>
                    </div>
                    <div class="clear"></div>
                    <div class="menu clearfix">
                        <ul>
                            <li><a<%= nowmodel == "default" ? " class=\"current\"" : ""%> href="<%= Application["WebUrl"] %>"><span class="de_1"></span><span class="de_2"></span><span class="de_3"><em>首页</em></span></a></li><%List<sys_Page> pages = sys_PageBLL.GetList(0);foreach(sys_Page item in pages){ %>
                            <li><a<%= nowmodel == item.Code ? " class=\"current\"" : ""%> href="<%= item.Url == string.Empty?Application["WebUrl"].ToString()+item.Code:item.Url%>"><span class="de_1"></span><span class="de_2"></span><span class="de_3"><em><%= item.Name%></em></span></a></li><%} %>
                        </ul>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
<script type="text/javascript">
    $(function() {
        $('#memlogin').click(function() {
            $.ajax({
                type: "post",
                url: '/member/login.aspx',
                dataType: 'json',
                data: $('#memloginfrm').serialize(),
                success: function(json) { jAlert(json.msg, '溫馨提示', function() { if (json.success) location.reload(); }); }
            });
        });
        inputblur('.txtBox');
    })
</script>