<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="Web.SysAdmin.Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主页分类</title>

    <script type="text/javascript">
        $(function() {
            var Info_Grid = $('#Info_Grid').datagrid({
                title: '主页分类',rownumbers: true, animate: true, border: false, singleSelect: false,
                url: 'Info.aspx?type=load',
                idField: 'ID_inf',
                remoteSort: false, pagination: true,
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '分类', field: 'Name_inf', width: 400}]],
                columns: [[{ title: '排序', field: 'Sort_inf', width: 60}]],
                onDblClickRow: function(row) { Info_Form.form('load', row); Info_Form.url = 'Info.aspx?type=form&action=edit'; Info_Dialog.dialog('open'); },
                toolbar: [{
                    id: 'btnAdd_inf',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() { Info_Dialog.dialog('open'); Info_Form.form('clear'); Info_Form.url = 'Info.aspx?type=form&action=add'; }
                }, '-', {
                    id: 'btnEdit_inf',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        var rows = Info_Grid.datagrid('getSelections');
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
                            Info_Dialog.dialog('open');
                            Info_Form.form('load', rows[0]);
                            Info_Form.url = 'Info.aspx?type=form&action=edit';
                        }
                    }
                }, '-', {
                    id: 'btnDelete_inf',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        var ids = [];
                        var rows = Info_Grid.datagrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].ID_inf);
                        }
                        if (ids.length > 0) {
                            $.messager.confirm('提示信息', '您确认要删除吗?', function(data) {
                                if (data) {
                                    $.ajax({
                                        url: 'Info.aspx?type=del&id=' + ids.join(','),
                                        type: 'GET',
                                        timeout: 1000,
                                        error: function() {
                                            $.messager.alert('错误', '删除失败!', 'error');
                                        },
                                        success: function(data) {
                                            if (data.success) Info_Grid.datagrid('reload');
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
                    id: 'btnRefresh_inf',
                    text: '刷新',
                    iconCls: 'refresh',
                    handler: function() {
                        Info_Grid.datagrid('reload');
                    }
                }
                ]
            });
            var Info_Dialog = $('#Info_Dialog');
            var Info_Form = Info_Dialog.find('form');
            var Info_Form_Action = '';
            $('#btn_Info_Save').click(function() {
                if (Info_Form.form('validate')) {
                    Info_Form.form('submit', {
                        url: Info_Form.url,
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "info", function() {
                                if (data.success) { Info_Form.form('clear'); Info_Dialog.dialog('close'); Info_Grid.datagrid('reload'); }
                            });
                        }
                    });
                }
            });
            $('#btn_Info_Cancel').click(function() {
                Info_Form.form('clear');
                Info_Dialog.dialog('close');
            });
        });
    </script>

</head>
<body>
    <div id="Info_Grid">
    </div>
    <div id="Info_Dialog" class="easyui-dialog" icon="icon icon-nav" closed="true"
        modal="true" style="padding: 10px 30px;" title="主页分类"
        buttons="#Info_buttons">
        <form id="Info_Form" method="post" action="">
        <table cellpadding="3">
            <tr>
                <td align="right">
                    名称：
                </td>
                <td>
                    <input name="ID_inf" type="hidden" />
                    <input name="Name_inf" type="text" class="easyui-validatebox frmText" required="true"
                        missingmessage="名称必须填写" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    排序：
                </td>
                <td>
                    <input name="Sort_inf" type="number" class="easyui-numberbox frmText" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="Info_buttons">
        <a id="btn_Info_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_Info_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
