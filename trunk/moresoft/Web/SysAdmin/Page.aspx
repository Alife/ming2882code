<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page.aspx.cs" Inherits="Web.SysAdmin.Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>页面信息管理</title>

    <script type="text/javascript">
        $(function() {
            var Page_Grid = $('#Page_Grid').datagrid({
                title: '页面信息管理', url: 'Page.aspx?type=load', idField: 'ID_pag', rownumbers: true, animate: true, border: false, singleSelect: false,
                remoteSort: false, fit: true,
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '名称', field: 'Name_pag', width: 400}]],
                columns: [[{ title: '编号', field: 'Code_pag', width: 160 }, { title: '排序', field: 'Sort_pag', width: 60}]],
                onDblClickRow: function(rowIndex, row) {
                    addTab('Page-' + row.ID_pag, '编辑页面信息-' + row.Name_pag, 'PageDetail.aspx?id=' + row.ID_pag, 'frame', 'icon icon-Page');
                },
                toolbar: [{
                    id: 'btnAdd_pag',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() {
                        addTab('Page-0', '新增页面信息', 'PageDetail.aspx?id=0', 'frame', 'icon icon-Page');
                    }
                }, '-', {
                    id: 'btnEdit_pag',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        var rows = Page_Grid.datagrid('getSelections');
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
                            addTab('Page-' + rows[0].ID_pag, '编辑页面信息-' + rows[0].Name_pag, 'PageDetail.aspx?id=' + rows[0].ID_pag, 'frame', 'icon icon-Page');
                        }
                    }
                }, '-', {
                    id: 'btnDelete_pag',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        var ids = [];
                        var rows = Page_Grid.datagrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].ID_pag);
                        }
                        if (ids.length > 0) {
                            $.messager.confirm('提示信息', '您确认要删除吗?', function(data) {
                                if (data) {
                                    $.ajax({
                                        url: 'Page.aspx?type=del&id=' + ids.join(','),
                                        type: 'GET',
                                        timeout: 1000,
                                        error: function() {
                                            $.messager.alert('错误', '删除失败!', 'error');
                                        },
                                        success: function(data) {
                                            if (data.success) Page_Grid.datagrid('reload');
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
                    id: 'btnRefresh_pag',
                    text: '刷新',
                    iconCls: 'refresh',
                    handler: function() {
                        Page_Grid.datagrid('reload');
                    }
                }
                ]
            });
        });
    </script>

</head>
<body>
    <div id="Page_Grid">
    </div>
</body>
</html>
