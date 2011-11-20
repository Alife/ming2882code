<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.SysAdmin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>摩尔社区优化系统后台</title>
    <link rel="stylesheet" type="text/css" href="../js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/themes/default/css/default.css" />
    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui-1.2.3.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>    <script type="text/javascript">
        $(function() {
            var win = $('#loginWin').window({ closed: true, modal: true, shadow: true, resizable: false });
            win.window('open');
            var form = win.find('form');
            $('#btnSave').click(function() {
                if (form.form('validate')) {
                    form.form('submit', {
                        url: 'login.aspx',
                        success: function(data) {
                            data = eval("(" + data + ")"); ;
                            $.messager.alert('系统提示', data.msg, "info", function() { if (data.success) location.href = '/sysadmin'; });
                        }
                    });
                }
            });
            $('#btnCancel').click(function() {
                form.form('clear');
            });
        });
    </script>
</head>
<body>
    <div id="loginWin" class="easyui-window" title="管理员登录" collapsible="false" minimizable="false"
        maximizable="false" icon="icon icon-users" style="width: 300px; height: 190px; padding: 5px;
        background: #fafafa;">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <form id="loginFrm" method="post">
                <table cellpadding="3">
                    <tr>
                        <td>
                            账号：
                        </td>
                        <td>
                            <input name="userName" type="text" class="easyui-validatebox" required="true" missingmessage="账号必须填写" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            密码：
                        </td>
                        <td>
                            <input name="password" type="password" class="easyui-validatebox" required="true" missingmessage="密码必须填写" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            <input name="rememberMe" type="checkbox" value="" /> 记住登录
                        </td>
                    </tr>
                </table>
                </form>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a id="btnSave" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
                <a id="btnCancel" class="easyui-linkbutton" href="javascript:void(0)">
                    取消</a>
            </div>
        </div>
    </div>
</body>
</html>
