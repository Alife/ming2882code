<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Link.aspx.cs" Inherits="Web.SysAdmin.Link" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>友情连接</title>

    <script type="text/javascript">
        $(function() {
            var Link_Grid = $('#Link_Grid').datagrid({
                title: '友情连接', rownumbers: true, animate: true, border: false, singleSelect: false,
                url: 'Link.aspx?type=load',
                idField: 'ID_lnk',
                remoteSort: false, pagination: true, pageSize: 20,
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '名称', field: 'Name_lnk', width: 300 }, { title: '网址', field: 'Url_lnk', width: 400 }
                , { title: '是否隐藏', field: 'IsHide_lnk', width: 140}]],
                columns: [[{ title: '排序', field: 'Sort_lnk', width: 60}]],
                onDblClickRow: function(rowIndex, row) { Link_Form.form('load', row); Link_Form.url = 'Link.aspx?type=form&action=edit'; Link_Dialog.dialog('open'); },
                toolbar: [{
                    id: 'btnAdd_lnk',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() { Link_Dialog.dialog('open'); Link_Form.form('clear'); Link_Form.url = 'Link.aspx?type=form&action=add'; }
                }, '-', {
                    id: 'btnEdit_lnk',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        var rows = Link_Grid.datagrid('getSelections');
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
                            Link_Dialog.dialog('open');
                            Link_Form.form('load', rows[0]);
                            Link_Form.url = 'Link.aspx?type=form&action=edit';
                        }
                    }
                }, '-', {
                    id: 'btnDelete_lnk',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        var ids = [];
                        var rows = Link_Grid.datagrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].ID_lnk);
                        }
                        if (ids.length > 0) {
                            $.messager.confirm('提示信息', '您确认要删除吗?', function(data) {
                                if (data) {
                                    $.ajax({
                                        url: 'Link.aspx?type=del&id=' + ids.join(','),
                                        type: 'GET',
                                        timeout: 1000,
                                        error: function() {
                                            $.messager.alert('错误', '删除失败!', 'error');
                                        },
                                        success: function(data) {
                                            if (data.success) Link_Grid.datagrid('reload');
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
                    id: 'btnRefresh_lnk',
                    text: '刷新',
                    iconCls: 'refresh',
                    handler: function() {
                        Link_Grid.datagrid('reload');
                    }
                }
                ]
            });
            var Link_Dialog = $('#Link_Dialog');
            var Link_Form = Link_Dialog.find('form');
            var Link_Form_Action = '';
            $('#btn_Link_Save').click(function() {
                if (Link_Form.form('validate')) {
                    Link_Form.form('submit', {
                        url: Link_Form.url,
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "info", function() {
                                if (data.success) { Link_Form.form('clear'); Link_Dialog.dialog('close'); Link_Grid.datagrid('reload'); }
                            });
                        }
                    });
                }
            });
            $('#btn_Link_Cancel').click(function() {
                Link_Form.form('clear');
                Link_Dialog.dialog('close');
            });
        });
    </script>

</head>
<body>
    <div id="Link_Grid">
    </div>
    <div id="Link_Dialog" class="easyui-dialog" icon="icon icon-nav" closed="true" modal="true" 
        style="padding: 10px 30px;width:500px;" title="友情连接" buttons="#Link_buttons">
        <form id="Link_Form" method="post" action="">
        <table cellpadding="3">
            <tr>
                <td align="right">
                    名称：
                </td>
                <td>
                    <input name="ID_lnk" type="hidden" />
                    <input name="Name_lnk" type="text" class="easyui-validatebox frmText" required="true"
                        missingmessage="名称必须填写" style="width:300px;" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    URL：
                </td>
                <td>
                    <input name="Url_lnk" type="text" class="easyui-validatebox frmText" required="true"
                        missingmessage="URL必须填写" style="width:300px;" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    是否隐藏：
                </td>
                <td>
                    <input name="IsHide_lnk" type="checkbox" class="easyui-validatebox frmText" value="true" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    排序：
                </td>
                <td>
                    <input name="Sort_lnk" type="number" class="easyui-numberbox frmText" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="Link_buttons">
        <a id="btn_Link_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_Link_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
