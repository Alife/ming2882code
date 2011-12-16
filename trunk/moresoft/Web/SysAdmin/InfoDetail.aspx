<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoDetail.aspx.cs" Inherits="Web.SysAdmin.InfoDetail" %>

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
        //var editor = K.create('textarea[name="Content_inf"]', options);
        //editor.sync();
        $(function() {
            var editor;
            KindEditor.ready(function(K) {
                editor = K.create('#Content_inf', {
                    uploadJson: '../kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '../kindeditor/asp.net/file_manager_json.ashx',
                    allowFileManager: true,
                    afterCreate: function() {
                        var self = this;
                        K.ctrl(document, 13, function() {
                            self.sync();
                            K('form[name=Info_Form]')[0].submit();
                        });
                        K.ctrl(self.edit.doc, 13, function() {
                            self.sync();
                            K('form[name=Info_Form]')[0].submit();
                        });
                    }
                });
            });
            var id = '<%= Web.ReqHelper.Get<int>("id")%>';
            var Info_Dialog = $('#Info_Dialog');
            var Info_Form = Info_Dialog.find('form');
            //if (id > 0)
            //    Info_Form.form('load', 'InfoDetail.aspx?type=load&id=' + id);
            $('#btn_Info_Save').click(function() {
                editor.sync();
                if (Info_Form.form('validate')) {
                    Info_Form.form('submit', {
                        url: 'InfoDetail.aspx?type=form&action=' + (id == 0 ? 'add' : 'edit'),
                        success: function(data) {
                            data = eval("(" + data + ")");
                            if (data.success)
                                $.messager.confirm('系统提示', data.msg + ',是否关闭当前表单', "info", function(data) {
                                    if (data) { $('#tabs').tabs('close', subtitle); }
                                });
                            else
                                $.messager.alert('系统提示', data.msg, "info");
                        }
                    });
                }
            });
            $('#btn_Info_Cancel').click(function() {
                //Info_Form.form('clear');
            });
        });
    </script>

</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no">
    <div id="Info_Dialog" region="center" border="false" class="easyui-panel" icon="icon icon-nav"
        fit="true" style="background: #fff;" title="文章管理">
        <form id="Info_Form" name="Info_Form" method="post" action="">
        <% int id = Web.ReqHelper.Get<int>("id"); var item = MC.BLL.Info_infBLL.GetItem(id);%>
        <table cellpadding="3">
            <tr>
                <td align="right" width="100">
                    标题：
                </td>
                <td>
                    <input name="ID_inf" type="hidden" value="<%= id > 0 ? item.ID_inf : id %>" />
                    <input name="Title_inf" type="text" class="easyui-validatebox frmText" style="width: 600px;"
                        required="true" missingmessage="标题必须填写" value="<%= id > 0 ? item.Title_inf : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    分类：
                </td>
                <td>
                    <input name="InfoTypeID_inf" type="text" class="easyui-combotree" url="InfoType.aspx?type=loadtree&hasFrist=1"
                        style="width: 200px;" required="true" missingmessage="分类必须选择" value="<%= id > 0 ? item.InfoTypeID_inf : id %>" />
                    主页分类：
                    <input name="IndexTagID_inf" type="text" class="easyui-combobox" url="IndexTag.aspx?type=loadall"
                        valuefield="id" textfield="text" panelheight="auto" style="width: 200px;" value="<%= id > 0 ? item.IndexTagID_inf : id %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    类型：
                </td>
                <td>
                    <select name="TopType_inf" class="easyui-combobox" multiple="true" panelheight="auto"
                        style="width: 200px;">
                        <option value="news" <%= id > 0 && !string.IsNullOrEmpty(item.TopType_inf) && item.TopType_inf.Contains("news") ? " selected" : string.Empty%>>
                            最新</option>
                        <option value="common" <%= id > 0 && !string.IsNullOrEmpty(item.TopType_inf) && item.TopType_inf.Contains("common") ? " selected" : string.Empty%>>
                            常见(浏览量排序)</option>
                    </select>
                    点击率：<input name="Hits_inf" type="number" class="easyui-numberbox frmText" style="width: 60px;"
                        value="<%= id > 0 ? item.Hits_inf : id %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    发布时间：
                </td>
                <td>
                    <input name="CreateTime_inf_Str" type="text" class="easyui-datetimebox frmText" style="width: 150px;"
                        value="<%= id > 0 ? item.CreateTime_inf_Str : string.Empty%>" />
                    作者：
                    <input name="Author_inf" type="text" class="easyui-validatebox frmText" value="<%= id > 0 ? item.Author_inf : string.Empty %>" />
                    关键字：
                    <input name="Keywords_inf" type="text" class="easyui-validatebox frmText" style="width: 250px;"
                        value="<%= id > 0 ? item.Keywords_inf : string.Empty %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    内容：
                </td>
                <td>
                    <textarea id="Content_inf" name="Content_inf" style="width: 850px; height: 400px;
                        visibility: hidden;"><%= id > 0 ? item.Content_inf : string.Empty%></textarea>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
        <a id="btn_Info_Save" class="easyui-linkbutton" href="javascript:void(0)">确定</a>
        <a id="btn_Info_Cancel" class="easyui-linkbutton" href="javascript:void(0)">取消</a>
    </div>
</body>
</html>
