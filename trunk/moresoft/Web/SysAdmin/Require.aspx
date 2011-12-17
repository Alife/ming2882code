<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Require.aspx.cs" Inherits="Web.SysAdmin.Require" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>需求管理</title>

    <script type="text/javascript">
        $(function() {
            var Require_Grid = $('#Require_Grid').datagrid({
                title: '需求管理', rownumbers: true, animate: true, border: false, singleSelect: false,
                url: 'Require.aspx?type=load',
                idField: 'ID_req',
                remoteSort: false, pagination: true, pageSize: 20,
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '联系人', field: 'Name_req', width: 180}]],
                columns: [[{ title: '公司', field: 'Company_req', width: 250 }, { title: '行业', field: 'Industry_req', width: 200 }, { title: '联系电话', field: 'Tel_req', width: 100 },
                { title: '联系手机', field: 'Mobile_req', width: 100}]],
                onDblClickRow: function(rowIndex, row) { Require_Form.form('load', row); Require_Form.url = 'Require.aspx?type=form&action=edit'; Require_Dialog.dialog('open'); },
                toolbar: [{
                    id: 'btnAdd_req',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() { Require_Dialog.dialog('open'); Require_Form.form('clear'); Require_Form.url = 'Require.aspx?type=form&action=add'; }
                }, '-', {
                    id: 'btnEdit_req',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        var rows = Require_Grid.datagrid('getSelections');
                        var num = rows.length;
                        if (num == 0) {
                            $.messager.alert('提示', '请选择一条记录进行操作!', 'info');
                            return;
                        }
                        else if (num > 1) {
                            $.messager.alert('提示', '您选择了多条记录,只能选择一条记录进行修改!', 'info');
                            return;
                        }
                        else {
                            Require_Dialog.dialog('open');
                            Require_Form.form('load', rows[0]);
                            Require_Form.url = 'Require.aspx?type=form&action=edit';
                        }
                    }
                }, '-', {
                    id: 'btnDelete_req',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        var ids = [];
                        var rows = Require_Grid.datagrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].ID_req);
                        }
                        if (ids.length > 0) {
                            $.messager.confirm('提示信息', '您确认要删除吗?', function(data) {
                                if (data) {
                                    $.ajax({
                                        url: 'Require.aspx?type=del&id=' + ids.join(','),
                                        type: 'GET',
                                        timeout: 1000,
                                        error: function() {
                                            $.messager.alert('错误', '删除失败!', 'error');
                                        },
                                        success: function(data) {
                                            if (data.success) Require_Grid.datagrid('reload');
                                            else $.messager.alert('错误', data.msg, 'error');
                                        }
                                    });
                                }
                            });
                        } else {
                            $.messager.show({
                                title: '警告',
                                msg: '请先选择要删除的记录。'
                            });
                        }
                    }
                }, '-', {
                    id: 'btnRefresh_req',
                    text: '刷新',
                    iconCls: 'refresh',
                    handler: function() {
                        Require_Grid.datagrid('reload');
                    }
                }
                ]
            });
            var Require_Dialog = $('#Require_Dialog');
            var Require_Form = Require_Dialog.find('form');
            var Require_Form_Action = '';
            $('#btn_Require_Save').click(function() {
                if (Require_Form.form('validate')) {
                    Require_Form.form('submit', {
                        url: Require_Form.url,
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "info", function() {
                                if (data.success) { Require_Form.form('clear'); Require_Dialog.dialog('close'); Require_Grid.datagrid('reload'); }
                            });
                        }
                    });
                }
            });
            $('#btn_Require_Cancel').click(function() {
                Require_Form.form('clear');
                Require_Dialog.dialog('close');
            });
        });
    </script>

</head>
<body>
    <div id="Require_Grid">
    </div>
    <div id="Require_Dialog" class="easyui-dialog" icon="icon icon-nav" closed="true"
        modal="true" style="width: 600px; padding: 10px 30px;" title="需求管理" buttons="#Require_buttons">
        <form id="Require_Form" method="post" action="">
        <table cellpadding="3">
            <tr>
                <td align="right">
                    联系人：
                </td>
                <td>
                    <input name="ID_req" type="hidden" />
                    <input name="TrueName_req" type="text" class="easyui-validatebox frmText" required="true"
                        style="width: 200px;" missingmessage="联系人必须填写" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    联系电话：
                </td>
                <td>
                    <input name="Tel_req" type="text" class="easyui-validatebox frmText" required="true"
                        missingmessage="联系电话必须填写" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    联系手机：
                </td>
                <td>
                    <input name="Mobile_req" type="text" class="easyui-validatebox frmText" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    公司名称：
                </td>
                <td>
                    <input name="Company_req" type="text" class="easyui-validatebox frmText" style="width: 200px;" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    行业名称：
                </td>
                <td>
                    <input name="Industry_req" type="text" class="easyui-validatebox frmText" style="width: 200px;" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    备注：
                </td>
                <td>
                    <textarea name="Remark_req" class="easyui-validatebox" cols="70" rows="10"></textarea>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="Require_buttons">
        <a id="btn_Require_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_Require_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
