<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Keywords.aspx.cs" Inherits="Web.SysAdmin.Keywords" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>关键字管理</title>

    <script type="text/javascript">
        $(function() {
            var Keywords_Grid = $('#Keywords_Grid').datagrid({
                title: '关键字管理', rownumbers: true, animate: true, border: false, singleSelect: false,
                url: 'Keywords.aspx?type=load',
                idField: 'ID_key',
                remoteSort: false, pagination: true,
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '关键字', field: 'Name_key', width: 200}]],
                columns: [[{ title: '连接', field: 'Url_key', width: 250 }, { title: '每篇更替个数', field: 'Num_key', width: 100}, { title: '排序', field: 'Sort_key', width: 80}]],
                onDblClickRow: function(rowIndex, row) { row.Name_key_Old = row.Name_key; Keywords_Form.form('load', row); Keywords_Form.url = 'Keywords.aspx?type=form&action=edit'; Keywords_Dialog.dialog('open'); },
                toolbar: [{
                    id: 'btnAdd_key',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() { Keywords_Dialog.dialog('open'); Keywords_Form.form('clear'); Keywords_Form.url = 'Keywords.aspx?type=form&action=add'; }
                }, '-', {
                    id: 'btnEdit_key',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        var rows = Keywords_Grid.datagrid('getSelections');
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
                            Keywords_Dialog.dialog('open');
                            var row = rows[0];
                            row.Name_key_Old = row.Name_key;
                            Keywords_Form.form('load', row);
                            Keywords_Form.url = 'Keywords.aspx?type=form&action=edit';
                        }
                    }
                }, '-', {
                    id: 'btnDelete_key',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        var ids = [];
                        var rows = Keywords_Grid.datagrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].ID_key);
                        }
                        if (ids.length > 0) {
                            $.messager.confirm('提示信息', '您确认要删除吗?', function(data) {
                                if (data) {
                                    $.ajax({
                                        url: 'Keywords.aspx?type=del&id=' + ids.join(','),
                                        type: 'GET',
                                        timeout: 1000,
                                        error: function() {
                                            $.messager.alert('错误', '删除失败!', 'error');
                                        },
                                        success: function(data) {
                                            if (data.success) Keywords_Grid.datagrid('reload');
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
                    id: 'btnRefresh_key',
                    text: '刷新',
                    iconCls: 'refresh',
                    handler: function() {
                        Keywords_Grid.datagrid('reload');
                    }
                }
                ]
            });
            var Keywords_Dialog = $('#Keywords_Dialog');
            var Keywords_Form = Keywords_Dialog.find('form');
            var Keywords_Form_Action = '';
            $('#btn_Keywords_Save').click(function() {
                if (Keywords_Form.form('validate')) {
                    Keywords_Form.form('submit', {
                        url: Keywords_Form.url,
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "info", function() {
                                if (data.success) { Keywords_Form.form('clear'); Keywords_Dialog.dialog('close'); Keywords_Grid.datagrid('reload'); }
                            });
                        }
                    });
                }
            });
            $('#btn_Keywords_Cancel').click(function() {
                Keywords_Form.form('clear');
                Keywords_Dialog.dialog('close');
            });
        });
    </script>

</head>
<body>
    <div id="Keywords_Grid">
    </div>
    <div id="Keywords_Dialog" class="easyui-dialog" icon="icon icon-nav" closed="true"
        modal="true" style="width:600px;padding: 10px 30px;" title="关键字管理"
        buttons="#Keywords_buttons">
        <form id="Keywords_Form" method="post" action="">
        <table cellpadding="3">
            <tr>
                <td align="right">
                    关键字：
                </td>
                <td>
                    <input name="ID_key" type="hidden" />
                    <input name="Name_key_Old" type="hidden" />
                    <input name="Name_key" type="text" class="easyui-validatebox frmText" required="true" style="width:200px;"
                        missingmessage="关键字必须填写" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    URL：
                </td>
                <td>
                    <input name="Url_key" type="text" class="easyui-validatebox frmText" required="true" style="width:400px;"
                        missingmessage="URL必须填写" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    每篇更替个数：
                </td>
                <td>
                    <input name="Num_key" type="number" class="easyui-numberbox frmText" value="0" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    排序：
                </td>
                <td>
                    <input name="Sort_key" type="number" class="easyui-numberbox frmText" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="Keywords_buttons">
        <a id="btn_Keywords_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_Keywords_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
