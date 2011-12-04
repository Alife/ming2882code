<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Web.SysAdmin.Setting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript">
        $(function() {
            var id = '<%= Web.ReqHelper.Get<int>("id")%>';
            var Setting_Dialog = $('#Setting_Dialog');
            var Setting_Form = Setting_Dialog.find('form');
            $('#btn_Setting_Save').click(function() {
                if (Setting_Form.form('validate')) {
                    Setting_Form.form('submit', {
                        url: 'Setting.aspx?type=form&action=' + (id == 0 ? 'add' : 'edit'),
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "Setting", function() {
                                if (data.success) { }
                            });
                        }
                    });
                }
            });
        });
    </script>

</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no">
    <div id="Setting_Dialog" region="center" border="false" class="easyui-panel" icon="icon icon-nav"
        width="600" style="background: #fff;" title="文章管理">
        <form id="Setting_Form" name="Setting_Form" method="post" action="">
        <% int id = Web.ReqHelper.Get<int>("id"); var item = MC.BLL.Setting_setBLL.GetItem(id);%>
        <table cellpadding="3">
            <tr>
                <td align="right" width="100">
                    网站名称：
                </td>
                <td>
                    <input name="ID_set" type="hidden" value="<%= id > 0 ? item.ID_set : id %>" />
                    <input name="WebName_set" type="text" class="easyui-validatebox frmText" style="width:400px;" required="true"
                        missingmessage="网站名称必须填写" value="<%= id > 0 ? item.WebName_set : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    网址：
                </td>
                <td>
                    <input name="WebUrl_set" type="text" class="easyui-validatebox frmText" style="width:400px;" required="true"
                        missingmessage="网址必须填写" value="<%= id > 0 ? item.WebUrl_set : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    网站SEO标题：
                </td>
                <td>
                    <input name="Title_set" type="text" class="easyui-validatebox frmText" style="width:400px;" value="<%= id > 0 ? item.Title_set : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    网站关键字：
                </td>
                <td>
                    <input name="Keywords_set" type="text" class="easyui-validatebox frmText" style="width:400px;" value="<%= id > 0 ? item.Keywords_set : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    网站作者：
                </td>
                <td>
                    <input name="Author_set" type="text" class="easyui-validatebox frmText" value="<%= id > 0 ? item.Author_set : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    IC：
                </td>
                <td>
                    <input name="ICP_set" type="text" class="easyui-validatebox frmText" value="<%= id > 0 ? item.ICP_set : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <a id="btn_Setting_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
