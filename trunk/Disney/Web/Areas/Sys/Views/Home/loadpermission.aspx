<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/ext/resources/css/ext-all.css" />
    <link rel="Stylesheet" type="text/css" href="/ext/resources/css/xtheme-blue.css" />
    <link rel="stylesheet" type="text/css" href="/css/default.css" />

    <script type="text/javascript" src="/ext/adapter/ext/ext-base.js"></script>

    <script type="text/javascript" src="/ext/ext-all.js"></script>

    <script type="text/javascript" src="/ext/ux/ext-basex.js"></script>

    <script type="text/javascript" src="/ext/resources/ext-lang-zh_CN.js"></script>

    <script type="text/javascript" src="/ext/ux/TableGrid.js"></script>

    <script type="text/javascript">
        var c_name = "disneycss";
        if (document.cookie.length > 0) {
            var c_start = document.cookie.indexOf(c_name + "=")
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1
                var c_end = document.cookie.indexOf(";", c_start)
                if (c_end == -1) c_end = document.cookie.length
                var css = unescape(document.cookie.substring(c_start, c_end));
                document.getElementsByTagName("link")[1].href = "/ext/resources/css/xtheme-" + css + ".css";
            }
        }
        var opid = '';
        var respfilterFields = [];
        var roid = '<%= ViewData["id"]%>';
        Ext.onReady(function() {
            var conn = Ext.lib.Ajax.getConnectionObject().conn;
            var btn = Ext.get("create-grid");
            var flagGrid = new Ext.ux.grid.TableGrid("the-table", {
                title: '当前位置:权限设置',
                border: false,
                loadMask: true,
                stripeRows: true,
                tbar: [{ text: '全选', iconCls: 'icon-add',
                    handler: function() {
                        var cbs = document.getElementsByName('cbopitem');
                        Ext.each(cbs, function() { this.checked = true; });
                    }
                }, '-', { text: '保存', iconCls: 'icon-save',
                    handler: function() {
                        var cbs = document.getElementsByName('cbopitem');
                        var cbopitem = [];
                        for (var i = 0; i < cbs.length; i++) {
                            if (cbs[i].checked)
                                cbopitem.push(cbs[i].value);
                        }
                        if (cbopitem.length > 0) {
                            Ext.MessageBox.show({ title: '提示框', msg: '你确定要修改吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                                fn: function(btn) {
                                    if (btn == 'ok') {
                                        Ext.Ajax.request({
                                            url: '/sys/permissionsave',
                                            params: { id: roid, cbopitem: cbopitem },
                                            success: function(resp, opts) {
                                                var temp = Ext.util.JSON.decode(resp.responseText);
                                                Ext.Msg.alert("系统提示!", temp.msg);
                                            },
                                            failure: function(resp, opts) {
                                                var temp = Ext.util.JSON.decode(resp.responseText);
                                                Ext.Msg.alert("系统提示!", temp.msg);
                                            }
                                        });
                                    }
                                }
                            });
                        } else Ext.Msg.alert("系统提示!", '保存成功');
                    } }],
                    listeners: {
                        "click": function(e) {
                            var btn = e.getTarget('.showinf');
                            if (btn) {
                                var o = btn.id.split(',');
                                store.proxy = new Ext.data.HttpProxy({ url: String.format('/sys/field/{0}', o[0]) });
                                store.load();
                                opid = o[0];
                                conn.open("POST", '/sys/loadpermissionfield/' + o[1], false);
                                conn.send(null);
                                respfilterFields = Ext.util.JSON.decode(conn.responseText);
                                win.show();
                            }
                        }
                    }
                });
            });
            var check_select = new Ext.grid.CheckboxSelectionModel();
            var dataType = Ext.data.Record.create([{ name: 'ID', type: 'int' }, { name: 'FieldName', type: 'string' }, { name: 'Field', type: 'string'}]);
            var store = new Ext.data.Store({ reader: new Ext.data.JsonReader({ fields: dataType }),
                listeners: {
                    load: function() {
                        check_select.selectAll();
                        this.each(function(record, i) {
                            if (respfilterFields.length > 0) {
                                Ext.each(respfilterFields, function(respfilterFieldsitem) {
                                    if (respfilterFieldsitem.FieldID == record.get('ID'))
                                        check_select.deselectRow(i);
                                });
                            }
                        });
                    }
                }
            });
            var grid = new Ext.grid.GridPanel({
                store: store,
                columns: [new Ext.grid.RowNumberer(),
                check_select,
                    { header: 'ID', width: 60, sortable: true, dataIndex: 'ID' },
                    { header: '字段名称', width: 150, sortable: true, dataIndex: 'FieldName' },
                    { id: 'Field', header: '字段', width: 200, sortable: true, dataIndex: 'Field'}],
                autoExpandColumn: 'Field',
                sm: check_select,
                border: false,
                plain: true,
                deferRowRender: false
            });
            check_select.handleMouseDown = Ext.emptyFn;
            grid.on("cellclick", function(grid, rowIndex, columnIndex, event) {
                if (columnIndex != 0) {
                    if (check_select.isSelected(rowIndex)) check_select.deselectRow(rowIndex);
                    else check_select.selectRow(rowIndex, true);
                }
            })
            var win = new Ext.Window({
                title: '设置字段',
                closeAction: 'hide',
                width: 600,
                height: 300,
                layout: 'fit',
                plain: true,
                border: false,
                loadMask: true,
                /*modal: 'true',*/
                buttonAlign: 'center',
                loadMask: true,
                animateTarget: document.body,
                items: [grid],
                buttons: [{ text: '保存',
                    handler: function() {
                        var s = grid.getSelectionModel().getSelections();
                        var ids = new Array();
                        for (var i = 0, r; r = s[i]; i++)
                            ids.push(r.data.ID);
                        Ext.Ajax.request({
                            url: '/sys/permissionfield',
                            method: 'POST',
                            params: { opid: opid, rid: roid, fields: ids },
                            success: function(response) {
                                var temp = Ext.util.JSON.decode(response.responseText);
                                Ext.Msg.alert("系统提示!", temp.msg);
                                if (temp.success)
                                    win.hide();
                            }
                        });
                    }
                }, { text: '取消', handler: function() { win.hide(); } }]
            });
    </script>

</head>
<body>
    <table id="the-table" width="100%" border="0" cellpadding="0" cellspacing="1">
        <thead>
            <tr>
                <th>
                    分类名称
                </th>
                <th>
                    操作
                </th>
            </tr>
        </thead>
        <tbody>
            <%  
                List<sys_Permission> per = sys_PermissionBLL.GetList(Convert.ToInt32(ViewData["id"]), 0);
                List<sys_Application> list = sys_ApplicationBLL.GetList(0, 0);
                foreach (sys_Application item in list)
                {%>
            <tr>
                <td>
                    <%= Funs.GetCategroyPath(item.Path, "　")%><%= item.Name%><%= item.Description == "" ? "" : "(" + item.Description + ")"%>
                </td>
                <td>
                    <ul class="clearfix">
                        <%List<sys_Operation> op = sys_OperationBLL.GetList(item.ID);
                          foreach (sys_Operation opitem in op)
                          {
                              var perItem = per.FirstOrDefault(p => p.OperationID == opitem.ID);
                        %>
                        <li style="float: left; padding-right: 5px;">
                            <input id="cbopitem" name="cbopitem" type="checkbox" value="<%= opitem.ID%>" <%= (perItem != null ? " checked" : "")%> />
                            <%= opitem.Operation%><%List<sys_Field> fields = sys_FieldBLL.GetList(opitem.ID); if (fields.Count > 0)
                                                    {%>(<a class="showinf" href="javascript:;" id="<%= opitem.ID%>,<%= perItem!=null?perItem.ID:0%>">设置字段</a>)<%} %>
                        </li>
                        <%} %>
                    </ul>
                </td>
            </tr>
            <%} %>
        </tbody>
    </table>
</body>
</html>
