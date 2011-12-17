<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexTag.aspx.cs" Inherits="Web.SysAdmin.IndexTag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主页分类</title>

    <script type="text/javascript">
        $(function() {
            var IndexTag_Grid = $('#IndexTag_Grid').datagrid({
                title: '主页分类',rownumbers: true, animate: true, border: false, singleSelect: false,
                url: 'IndexTag.aspx?type=load',
                idField: 'ID_itg',
                remoteSort: false, pagination: true, pageSize: 20,
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '分类', field: 'Name_itg', width: 400}]],
                columns: [[{ title: '排序', field: 'Sort_itg', width: 60}]],
                onDblClickRow: function(rowIndex, row) { IndexTag_Form.form('load', row); IndexTag_Form.url = 'IndexTag.aspx?type=form&action=edit'; IndexTag_Dialog.dialog('open'); },
                toolbar: [{
                    id: 'btnAdd_itg',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() { IndexTag_Dialog.dialog('open'); IndexTag_Form.form('clear'); IndexTag_Form.url = 'IndexTag.aspx?type=form&action=add'; }
                }, '-', {
                    id: 'btnEdit_itg',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        var rows = IndexTag_Grid.datagrid('getSelections');
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
                            IndexTag_Dialog.dialog('open');
                            IndexTag_Form.form('load', rows[0]);
                            IndexTag_Form.url = 'IndexTag.aspx?type=form&action=edit';
                        }
                    }
                }, '-', {
                    id: 'btnDelete_itg',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        var ids = [];
                        var rows = IndexTag_Grid.datagrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].ID_itg);
                        }
                        if (ids.length > 0) {
                            $.messager.confirm('提示信息', '您确认要删除吗?', function(data) {
                                if (data) {
                                    $.ajax({
                                        url: 'IndexTag.aspx?type=del&id=' + ids.join(','),
                                        type: 'GET',
                                        timeout: 1000,
                                        error: function() {
                                            $.messager.alert('错误', '删除失败!', 'error');
                                        },
                                        success: function(data) {
                                            if (data.success) IndexTag_Grid.datagrid('reload');
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
                    id: 'btnRefresh_itg',
                    text: '刷新',
                    iconCls: 'refresh',
                    handler: function() {
                        IndexTag_Grid.datagrid('reload');
                    }
                }
                ]
            });
            var IndexTag_Dialog = $('#IndexTag_Dialog');
            var IndexTag_Form = IndexTag_Dialog.find('form');
            var IndexTag_Form_Action = '';
            $('#btn_IndexTag_Save').click(function() {
                if (IndexTag_Form.form('validate')) {
                    IndexTag_Form.form('submit', {
                        url: IndexTag_Form.url,
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "info", function() {
                                if (data.success) { IndexTag_Form.form('clear'); IndexTag_Dialog.dialog('close'); IndexTag_Grid.datagrid('reload'); }
                            });
                        }
                    });
                }
            });
            $('#btn_IndexTag_Cancel').click(function() {
                IndexTag_Form.form('clear');
                IndexTag_Dialog.dialog('close');
            });
        });
    </script>

</head>
<body>
    <div id="IndexTag_Grid">
    </div>
    <div id="IndexTag_Dialog" class="easyui-dialog" icon="icon icon-nav" closed="true"
        modal="true" style="padding: 10px 30px;" title="主页分类"
        buttons="#IndexTag_buttons">
        <form id="IndexTag_Form" method="post" action="">
        <table cellpadding="3">
            <tr>
                <td align="right">
                    名称：
                </td>
                <td>
                    <input name="ID_itg" type="hidden" />
                    <input name="Name_itg" type="text" class="easyui-validatebox frmText" required="true"
                        missingmessage="名称必须填写" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    排序：
                </td>
                <td>
                    <input name="Sort_itg" type="number" class="easyui-numberbox frmText" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="IndexTag_buttons">
        <a id="btn_IndexTag_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_IndexTag_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
