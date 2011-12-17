<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="Web.SysAdmin.Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文章管理</title>

    <script type="text/javascript">
        $(function() {
            var Info_Grid = $('#Info_Grid').datagrid({
                title: '文章管理', url: 'Info.aspx?type=load', idField: 'ID_inf', rownumbers: true, animate: true, border: false, singleSelect: false,
                remoteSort: false, pagination: true, pageSize: 20, fit: true,
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '标题', field: 'Title_inf', width: 400}]],
                columns: [[{ title: '点击率', field: 'Hits_inf', width: 60 }, { title: '作者', field: 'Author_inf', width: 100 },
                { title: '关键字', field: 'Keywords_inf', width: 300 },
                { title: '发布时间', field: 'CreateTime_inf', width: 130, formatter: function(value, rec) {
                    return value.replace(/T/g, " ");
                }
                }
                ]],
                onDblClickRow: function(rowIndex, row) {
                    addTab('info-' + row.ID_inf, '编辑文章-' + row.Title_inf, 'InfoDetail.aspx?id=' + row.ID_inf, 'frame', 'icon icon-info');
                    //Info_Form.form('load', row); Info_Form.url = 'Info.aspx?type=form&action=edit'; Info_Dialog.dialog('open');
                },
                toolbar: [{
                    id: 'btnAdd_inf',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() {
                        addTab('info-0', '新增文章', 'InfoDetail.aspx?id=0', 'frame', 'icon icon-info');
                        //Info_Dialog.dialog('open'); Info_Form.form('clear'); Info_Form.url = 'Info.aspx?type=form&action=add';
                    }
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
                            addTab('info-' + rows[0].ID_inf, '编辑文章-' + rows[0].Title_inf, 'InfoDetail.aspx?id=' + rows[0].ID_inf, 'frame', 'icon icon-info');
                            //Info_Dialog.dialog('open');
                            //Info_Form.form('load', rows[0]);
                            //Info_Form.url = 'Info.aspx?type=form&action=edit';
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
        });
    </script>

</head>
<body>
    <div id="Info_Grid">
    </div>
</body>
</html>
