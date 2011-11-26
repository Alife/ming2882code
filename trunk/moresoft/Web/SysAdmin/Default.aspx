<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.SysAdmin.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>摩尔社区优化系统后台</title>
    <link rel="stylesheet" type="text/css" href="../js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/themes/default/css/default.css" />

    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../js/jquery.easyui-1.2.3.min.js" type="text/javascript"></script>

    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <script src="../js/lib/util.js" type="text/javascript"></script>

    <script src="../js/main.js" type="text/javascript"></script>

    <%--<link id="skin" rel="stylesheet" href="../js/jBox/Skins/Default/jbox.css" />    <script type="text/javascript" src="../js/jBox/jquery.jBox-2.3.min.js"></script>    <script type="text/javascript" src="../js/jBox/i18n/jquery.jBox-zh-CN.js"></script>--%>

    <script type="text/javascript">
        var _menus = { "menus": [
						{ "menuid": "1", "icon": "icon-sys", "menuname": "文章管理",
						    "menus": [
									{ "menuid": "2", "menuname": "文章管理", "icon": "icon-info", "url": "info.aspx", "urlType": "load" },
									{ "menuid": "3", "menuname": "文章分类", "icon": "icon-nav", "url": "infotype.aspx", "urlType": "load" }
								]
						}, { "menuid": "4", "icon": "icon-set", "menuname": "系统设置",
						    "menus": [{ "menuid": "5", "menuname": "管理员管理", "icon": "icon-users", "url": "users.aspx", "urlType": "load" },
									{ "menuid": "6", "menuname": "系统设置", "icon": "icon-set", "url": "setting.aspx", "urlType": "load" },
									{ "menuid": "7", "menuname": "日志查看", "icon": "icon-log", "url": "log.aspx", "urlType": "load" }
								]
						}
				]
        };
        function openPwd() {
            $('#changePwd').window({ title: '修改密码', width: 300, modal: true, shadow: true, closed: true, resizable: false, icon: "icon icon-users" });
        }
        function closePwd() {
            $('#changePwd').window('close');
        }
        function serverLogin() {
            var $newpass = $('#txtNewPass');
            var $rePass = $('#txtRePass');
            if ($newpass.val() == '') {
                $.messager.alert('系统提示', '请输入密码！', 'warning');
                return false;
            }
            if ($rePass.val() == '') {
                $.messager.alert('系统提示', '请在一次输入密码！', 'warning');
                return false;
            }
            if ($newpass.val() != $rePass.val()) {
                $.messager.alert('系统提示', '两次密码不一至！请重新输入', 'warning');
                return false;
            }
            $.post('/editpassword.aspx?newpass=' + $newpass.val(), function(msg) {
                $.messager.alert('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + msg, 'info');
                $newpass.val('');
                $rePass.val('');
                closePwd();
            });
        }
        $(function() {
            openPwd();
            $('#editpass').click(function() { $('#changePwd').window('open'); });
            $('#btnEp').click(function() { serverLogin(); });
            $('#btnCa').click(function() { closePwd(); })
            $('#loginOut').click(function() {
                $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function(r) {
                    if (r) { location.href = 'logout.aspx'; }
                });
            })
        });    </script>

</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no">
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px;
            width: 100%; background: white; text-align: center;">
            <img src="../images/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div region="north" split="true" border="false" style="overflow: hidden; height: 30px;
        background: url(../images/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%;
        line-height: 20px; color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <span style="float: right; padding-right: 20px;" class="head">欢迎 admin <a href="#"
            id="editpass">修改密码</a> <a href="#" id="loginOut">安全退出</a></span> <span style="padding-left: 10px;
                font-size: 16px;">
                <img src="../favicon.ico" width="16" height="16" align="absmiddle" />
                摩尔社区优化系统后台</span>
    </div>
    <div region="south" split="true" style="height: 30px; text-align: center">
        <div class="footer">
            copyright@2011 by 摩尔软件</div>
    </div>
    <div region="west" hide="true" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div id="nav" class="easyui-accordion" fit="true" border="false">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="欢迎使用">
            </div>
        </div>
    </div>
    <!--修改密码窗口-->
    <div id="changePwd" class="easyui-window" title="修改密码" collapsible="false" minimizable="false"
        maximizable="false" icon="icon icon-users" style="width: 300px; height: 170px; padding: 5px;
        background: #fafafa;">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <table cellpadding="3">
                    <tr>
                        <td>
                            新密码：
                        </td>
                        <td>
                            <input id="txtNewPass" type="Password" class="txt01" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            确认密码：
                        </td>
                        <td>
                            <input id="txtRePass" type="Password" class="txt01" />
                        </td>
                    </tr>
                </table>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a id="btnEp" class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)">确定</a>
                <a id="btnCa" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">
                    取消</a>
            </div>
        </div>
    </div>
    <div id="mm" class="easyui-menu" style="width: 150px;">
        <div id="mm-tabupdate">
            刷新</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabclose">
            关闭</div>
        <div id="mm-tabcloseall">
            全部关闭</div>
        <div id="mm-tabcloseother">
            除此之外全部关闭</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabcloseright">
            当前页右侧全部关闭</div>
        <div id="mm-tabcloseleft">
            当前页左侧全部关闭</div>
        <div class="menu-sep">
        </div>
        <div id="mm-exit">
            退出</div>
    </div>
</body>
</html>
