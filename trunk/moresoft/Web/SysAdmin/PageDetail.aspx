<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageDetail.aspx.cs" Inherits="Web.SysAdmin.PageDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/themes/default/css/default.css" />

    <script src="../js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../js/jquery.easyui-1.2.3.min.js" type="text/javascript"></script>

    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <script src="../kindeditor/kindeditor-min.js" type="text/javascript" charset="utf-8"></script>

    <script src="../kindeditor/lang/zh_CN.js" type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript">
        //var editor = K.create('textarea[name="Content_pag"]', options);
        //editor.sync();
        $(function() {
            var editor;
            KindEditor.ready(function(K) {
                editor = K.create('#Content_pag', {
                    uploadJson: '../kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '../kindeditor/asp.net/file_manager_json.ashx',
                    allowFileManager: true,
                    afterCreate: function() {
                        var self = this;
                        K.ctrl(document, 13, function() {
                            self.sync();
                            K('form[name=Page_Form]')[0].submit();
                        });
                        K.ctrl(self.edit.doc, 13, function() {
                            self.sync();
                            K('form[name=Page_Form]')[0].submit();
                        });
                    }
                });
            });
            var id = '<%= Web.ReqHelper.Get<int>("id")%>';
            var Page_Dialog = $('#Page_Dialog');
            var Page_Form = Page_Dialog.find('form');
            //if (id > 0)
            //    Page_Form.form('load', 'PageDetail.aspx?type=load&id=' + id);
            $('#btn_Page_Save').click(function() {
                editor.sync();
                if (Page_Form.form('validate')) {
                    Page_Form.form('submit', {
                        url: 'PageDetail.aspx?type=form&action=' + (id == 0 ? 'add' : 'edit'),
                        success: function(data) {
                            data = eval("(" + data + ")");
                            $.messager.alert('系统提示', data.msg, "info", function() {
                                if (data.success) { }
                            });
                        }
                    });
                }
            });
            $('#btn_Page_Cancel').click(function() {
                //Page_Form.form('clear');
            });
        });
    </script>

</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no">
    <div id="Page_Dialog" region="center" border="false" class="easyui-panel" icon="icon icon-nav"
        fit="true" style="background: #fff;" title="文章管理">
        <form id="Page_Form" name="Page_Form" method="post" action=""><% int id = Web.ReqHelper.Get<int>("id"); var item = MC.BLL.Page_pagBLL.GetItem(id);%>
        <table cellpadding="3">
            <tr>
                <td align="right" width="100">
                    标题：
                </td>
                <td>
                    <input name="ID_pag" type="hidden" value="<%= id > 0 ? item.ID_pag : id %>" />
                    <input name="Name_pag" type="text" class="easyui-validatebox frmText" style="width:600px;" required="true"
                        missingmessage="名称必须填写" value="<%= id > 0 ? item.Name_pag : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    编号：
                </td>
                <td>
                    <input name="Code_pag" type="text" class="easyui-validatebox frmText" required="true"
                        missingmessage="编号必须填写" value="<%= id > 0 ? item.Code_pag : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    排序：
                </td>
                <td>
                    <input name="Sort_pag" type="number" class="easyui-numberbox frmText" style="width:60px;" value="<%= id > 0 ? item.Sort_pag : 99 %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    内容：
                </td>
                <td>
                    <textarea id="Content_pag" name="Content_pag" style="width: 850px; height: 400px;
                        visibility: hidden;"><%= id > 0 ? item.Content_pag : string.Empty%></textarea>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
        <a id="btn_Page_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_Page_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
