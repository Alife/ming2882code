<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master"  %>

<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MeteContent" runat="Server">

    <script type="text/javascript">
        $(function() {
            $('.navMenu li').each(function() {
                $(this).click(function() {
                    var key = $(this).attr('key').split(',');
                    var id = key[0];
                    var code = key[1];
                    $('.navMenu li').each(function() { $(this).removeClass('default'); });
                    $(this).addClass('default');
                    var leftframe = self.parent.frames['leftFrame'];
                    var leftMenu = leftframe.$('.leftMenu');
                    if (key == 'MyDesk') {
                        $(leftMenu).html('<li class="default" key="main.aspx"><a href="javascript:;">管理中心</a></li>\                                        <li key="users/user.aspx"><a href="javascript:;">會員管理</a></li>\
                                        <li key="orders/order.aspx"><a href="javascript:;">訂單管理</a></li>\                                        <li key="info/article.aspx"><a href="javascript:;">內容管理</a></li>');
                        self.parent.frames['mainFrame'].location = 'main.aspx';
                    }
                    else if (key == 'Users') {
                        $(leftMenu).html('<li class="default" key="users/user.aspx"><a href="javascript:;">會員管理</a></li>');
                        self.parent.frames['mainFrame'].location = 'users/user.aspx';
                    } else if (key == 'Orders') {
                        $(leftMenu).html('<li key="orders/goods.aspx"><a href="javascript:;">商品管理</a></li>\
                                        <li key="orders/goodscategory.aspx"><a href="javascript:;">商品分類管理</a></li>\
                                        <li key="orders/order.aspx"><a href="javascript:;">訂單管理</a></li>\
                                        <li key="orders/addorder.aspx"><a href="javascript:;">增加訂單</a></li>\
                                        <li key="orders/shipping.aspx"><a href="javascript:;">配送方式</a></li>');
                        self.parent.frames['mainFrame'].location = 'orders/order.aspx';
                    } else if (key == 'Diy') {
                        $(leftMenu).html('<li class="default" key="diy/diy.aspx"><a href="javascript:;">設計管理</a></li>\
                                        <li key="diy/photo.aspx"><a href="javascript:;">圖庫管理</a></li>');
                        self.parent.frames['mainFrame'].location = 'diy/diy.aspx';
                    } else if (key == 'Info') {
                        $(leftMenu).html('<li class="default" key="info/article.aspx"><a href="javascript:;">內容管理</a></li>\
                                        <li key="info/articlecategory.aspx"><a href="javascript:;">內容分類管理</a></li>\
                                        <li key="info/photo.aspx"><a href="javascript:;">攝影作品</a></li>\
                                        <li key="info/photocategory.aspx"><a href="javascript:;">作品分類管理</a></li>\
                                        <li key="info/nav.aspx"><a href="javascript:;">其它內容管理</a></li>');
                        self.parent.frames['mainFrame'].location = 'info/article.aspx';
                    } else if (key == 'Setting') {
                        $(leftMenu).html('<li class="default" key="setting/settings.aspx"><a href="javascript:;">基本設置</a></li>\
                                        <li key="setting/log.aspx"><a href="javascript:;">查看日誌</a></li>');
                        self.parent.frames['mainFrame'].location = 'setting/settings.aspx';
                    }
                    $(leftMenu).children().each(function() {
                        $(this).click(function() {
                            var key = $(this).attr('key');
                            $(leftMenu).children().each(function() { $(this).removeClass('default'); });
                            $(this).addClass('default');
                            var mainFrame = self.parent.frames['mainFrame'];
                            mainFrame.location = key;
                        });
                    });
                });
            });
        });</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="header">
        <div class="logo">
            <label>
                v1.0</label>
            <div class="headerleft">
                <div class="logininfo">
                    admin，歡迎進入管理系統 <a href="/" target="_blank">返回主頁</a> <a href="logout.aspx">退出登錄</a></div>
                <div class="navMenu-bg">
                    <ul class="navMenu">
                        <li class="default" key="MyDesk"><a href="javascript:;">后臺主頁</a></li>
                        <li key="Users"><a href="javascript:;">會員管理</a></li>
                        <li key="Orders"><a href="javascript:;">銷售管理</a></li>
                        <li key="Diy"><a href="javascript:;">DIY管理</a></li>
                        <li key="Info"><a href="javascript:;">內容管理</a></li>
                        <li key="Setting"><a href="javascript:;">系統設置</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
