<%@ Control Language="C#" ClassName="MemberMenu" %>
<script runat="server">
    public string nowmodel { get; set; }
</script>
                <ul>
                    <li><a href="<%= Application["WebUrl"]%>member"<%= nowmodel == "default" ? " class=\"current\"" : ""%>>會員中心</a></li>
                    <li><a href="<%= Application["WebUrl"]%>member/cart.aspx"<%= nowmodel == "cart" ? " class=\"current\"" : ""%>>購物車</a></li>
                    <li><a href="<%= Application["WebUrl"]%>member/order.aspx"<%= nowmodel == "order" ? " class=\"current\"" : ""%>>我的訂單</a></li>
                    <li><a href="<%= Application["WebUrl"]%>member/mydiy.aspx"<%= nowmodel == "mydiy" ? " class=\"current\"" : ""%>>我的DIY</a></li>
                    <li><a href="<%= Application["WebUrl"]%>member/profile.aspx"<%= nowmodel == "profile" ? " class=\"current\"" : ""%>>我的資料</a></li>
                    <li><a href="<%= Application["WebUrl"]%>member/password.aspx"<%= nowmodel == "password" ? " class=\"current\"" : ""%>>修改密碼</a></li>
                    <li><a href="<%= Application["WebUrl"]%>member/logout.aspx">安全退出</a></li>
                </ul>
