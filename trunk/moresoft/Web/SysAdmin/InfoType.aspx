﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoType.aspx.cs" Inherits="Web.SysAdmin.InfoType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文章分类</title>

    <script type="text/javascript">
        $(function() {
            var infoType_Grid = $('#infoType_Grid').treegrid({
                title: '文章分类',
                fit: true, rownumbers: true, animate: true, border: false, //singleSelect: false,
                url: 'InfoType.aspx?type=load',
                idField: 'ID_ift',
                treeField: 'Name_ift',
                frozenColumns: [[{ field: 'ck', checkbox: true }, { title: '分类', field: 'Name_ift', width: 400, formatter: function(value) { return '<span style="color:red">' + value + '</span>'; } }]],
                columns: [[
                 { title: '编号', field: 'Code_ift', width: 100 },
                 { title: '是否禁用', field: 'IsHide_ift', width: 60, formatter: function(value, row) { return value == true ? '是' : '否'; } },
                 { title: '排序', field: 'Sort_ift', width: 60 },
                 { title: '关键字', field: 'Keywords_ift', width: 200 },
                 { title: 'Url', field: 'Url_ift', width: 200}]],
                onDblClickRow: function(row) { infoType_Form.form('load', row); infoType_Form.url = 'InfoType.aspx?type=form&action=edit'; infoType_Dialog.dialog('open'); },
                toolbar: [{
                    id: 'btnAdd_ift',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() { infoType_Dialog.dialog('open'); infoType_Form.form('clear'); infoType_Form.url = 'InfoType.aspx?type=form&action=add'; }
                }, '-', {
                    id: 'btnEdit_ift',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        var rows = infoType_Grid.treegrid('getSelections');
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
                            infoType_Dialog.dialog('open');
                            infoType_Form.form('load', rows[0]);
                            infoType_Form.url = 'InfoType.aspx?type=form&action=edit';
                        }
                    }
                }, '-', {
                    id: 'btnDelete_ift',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        var ids = [];
                        var rows = infoType_Grid.treegrid('getSelections');
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].ID_ift);
                        }
                        if (ids.length > 0) {
                            $.messager.confirm('提示信息', '您确认要删除吗?', function(data) {
                                if (data) {
                                    $.ajax({
                                        url: 'InfoType.aspx?type=del&id=' + ids.join(','),
                                        type: 'GET',
                                        timeout: 1000,
                                        error: function() {
                                            $.messager.alert('错误', '删除失败!', 'error');
                                        },
                                        success: function(data) {
                                            if (data.success) infoType_Grid.datagrid('reload');
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
                    id: 'btnRefresh_ift',
                    text: '刷新',
                    iconCls: 'refresh',
                    handler: function() {
                        infoType_Grid.treegrid('reload');
                    }
                }
                ]
            });
            var infoType_Dialog = $('#infoType_Dialog');
            var infoType_Form = infoType_Dialog.find('form');
            var infoType_Form_Action = '';
            $('#btn_InfoType_Save').click(function() {
                if (infoType_Form.form('validate')) {
                    infoType_Form.form('submit', {
                        url: infoType_Form.url,
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "info", function() {
                                if (data.success) { infoType_Form.form('clear'); infoType_Dialog.dialog('close'); infoType_Grid.treegrid('reload'); }
                            });
                        }
                    });
                }
            });
            $('#btn_InfoType_Cancel').click(function() {
                infoType_Form.form('clear');
                infoType_Dialog.dialog('close');
            });
        });
    </script>

</head>
<body>
    <div id="infoType_Grid">
    </div>
    <div id="infoType_Dialog" class="easyui-dialog" icon="icon icon-nav" closed="true" modal="true"
        style="width: 400px; height: 250px; padding: 10px 30px;" title="文章分类" buttons="#infoType_buttons">
        <form id="infoType_Form" method="post" action="">
        <table cellpadding="3">
            <tr>
                <td align="right">
                    分类名称：
                </td>
                <td>
                    <input name="ID_ift" type="hidden" />
                    <input name="Name_ift" type="text" class="easyui-validatebox frmText" required="true"
                        missingmessage="分类名称必须填写" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    分类编号：
                </td>
                <td>
                    <input name="Code_ift" type="text" class="frmText" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    关键字：
                </td>
                <td>
                    <input name="Keywords_ift" type="text" class="frmText" style="width:400px" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    排序：
                </td>
                <td>
                    <input name="Sort_ift" type="number" class="easyui-numberbox frmText" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    上级：
                </td>
                <td>
                    <input name="Parent_ift" type="text" class="easyui-combotree" url="InfoType.aspx?type=loadtree"
                        value="" style="width: 200px;" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input name="rememberMe" type="checkbox" value="" />
                    是否禁用
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="infoType_buttons">
        <a id="btn_InfoType_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_InfoType_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
