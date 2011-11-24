<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoType.aspx.cs" Inherits="Web.SysAdmin.InfoType1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        $(function() {
            $('#infoTypeGrid').treegrid({
                title: '文章分类',
                width:1000,height:600, rownumbers: true, animate: true, border: false,
                url: 'InfoType.aspx?type=load',
                idField: 'ID_ift',
                treeField: 'Name_ift',
                frozenColumns: [[
                 { title: '分类', field: 'Name_ift', width: 300,
                     formatter: function(value) {
                         return '<span style="color:red">' + value + '</span>';
                     }
                 }
                ]],
                columns: [[
                 { title: '编号', field: 'Code_ift', width: 100, editor: 'text' },
                 { title: '是否禁用', field: 'IsHide_ift', width: 60, editor: { type: 'checkbox', options: { on: true, off: false} },
                     formatter: function(value, row) {
                         return value == true ? '是' : '否';
                     }
                 },
                 { title: '排序', field: 'Sort_ift', width: 60, editor: 'text' },
                 { title: 'Url', field: 'Url_ift', width: 200, editor: 'text' }
                ]], onDblClickRow: function(row) {//运用双击事件实现对一行的编辑
                    var rowIndex = row.id;
                },
                toolbar: [{
                    id: 'btnAdd_ift',
                    text: '添加',
                    iconCls: 'application_add',
                    handler: function() {
                        win.window('open');
                    }
                }, '-', {
                    id: 'btnEdit_ift',
                    text: '编辑',
                    iconCls: 'application_edit',
                    handler: function() {
                        $('#btnsave').linkbutton('enable');
                        alert('cut');
                    }
                }, '-', {
                    id: 'btnDelete_ift',
                    text: '删除',
                    iconCls: 'application_delete',
                    handler: function() {
                        //$('#btnsave').linkbutton('disable');
                    }
                }
                ]
            });
            var win = $('#infoTypeGrid_Win').window({ closed: false, modal: true, shadow: true, resizable: false });
            var form = win.find('form');
        });
    </script>

</head>
<body>
    <div id="infoTypeGrid">
    </div>
    <div id="infoTypeGrid_Win" class="easyui-window" title="文章分类" collapsible="false" minimizable="false"
        maximizable="false" icon="icon icon-users" style="width: 400px; height: 290px; padding: 5px;
        background: #fafafa;">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <form id="frmInfoType" method="post">
                <table cellpadding="3">
                    <tr>
                        <td>
                            分类名称：
                        </td>
                        <td>
                            <input name="Name_ift" type="text" class="easyui-validatebox" required="true" missingmessage="分类名称必须填写" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            排序：
                        </td>
                        <td>
                            <input name="Sort_ift" type="text" class="easyui-validatebox" required="true" missingmessage="分类名称必须填写" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            上级：
                        </td>
                        <td>
                            <input id="Parent_ift" class="easyui-combotree" url="InfoType.aspx?type=load" value="" style="width:200px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            <input name="rememberMe" type="checkbox" value="" /> 是否禁用
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
