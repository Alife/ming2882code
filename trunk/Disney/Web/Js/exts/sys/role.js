role = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    levels.setper = GetIsLevel(node.attributes.Code, 'setper');
    levels.setdata = GetIsLevel(node.attributes.Code, 'setdata');
    var dataType = Ext.data.Record.create([{ name: 'ID', type: 'int' }, { name: 'RoleName', type: 'string' }, { name: 'Description', type: 'string'}]);
    var store = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/sys/role'
    });
    var url = '';
    var form = new Ext.form.FormPanel({
        frame: true,
        border: false,
        plain: true,
        layout: "form",
        defaultType: "textfield",
        labelAlign: 'right', labelWidth: 80,
        items: [
                { xtype: 'hidden', name: 'ID', hidden: false },
                { xtype: 'textfield', name: 'RoleName', fieldLabel: '名称', anchor: '60%', allowBlank: false },
                { xtype: 'textarea', name: 'Description', fieldLabel: '说明', anchor: '90%', height: 40, allowBlank: false }
            ]
    });
    var win = new Ext.Window({
        title: '',
        closeAction: 'hide',
        width: 400,
        height: 150,
        layout: 'fit',
        plain: true,
        border: false,
        /*modal: 'true',*/
        buttonAlign: 'center',
        loadMask: true,
        animateTarget: document.body,
        items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: url,
                        success: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            form.reset(); win.hide();
                            grid.store.reload();
                        },
                        failure: function(form, response) {
                            Ext.MessageBox.alert("提示!", "保存信息失败!");
                        }
                    });
                }
            }
        }, { text: '取消', handler: function() { form.getForm().reset(); win.hide(); } }]
    });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var grid = new Ext.grid.GridPanel({
        title: '当前位置:角色管理',
        store: store,
        border: false,
        loadMask: true,
        autoExpandColumn: 'Description',
        sm: check_select,
        tbar: [{ text: '增加', iconCls: 'icon-add', hidden: levels.add, handler: function() { url = '/sys/roleadd'; form.getForm().reset(); win.setTitle('增加角色'); win.show(); } },
            { xtype: 'tbseparator', hidden: levels.add },
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit,
                handler: function() {
                    win.setTitle('编辑角色');
                    win.show();
                    url = '/sys/roleedit';
                    form.getForm().loadRecord(grid.getSelectionModel().getSelected());
                }
            }, { xtype: 'tbseparator', hidden: levels.edit }, {
                ref: '../removeBtn',
                iconCls: 'icon-delete',
                text: '删除',
                disabled: true, hidden: levels.del,
                handler: function() {
                    var s = grid.getSelectionModel().getSelections();
                    var ids = new Array();
                    var storeitems = new Array();
                    for (var i = 0, r; r = s[i]; i++) {
                        if (r.data.ID)
                            ids.push(r.data.ID);
                        storeitems.push(r);
                    }
                    Ext.MessageBox.show({ title: '提示框', msg: '你确定要删除吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                        fn: function(btn) {
                            if (btn == 'ok') {
                                Ext.Ajax.request({
                                    url: '/sys/roledelete',
                                    params: { id: ids },
                                    success: function(response, options) {
                                        var temp = Ext.util.JSON.decode(response.responseText);
                                        //Ext.Msg.alert("系统提示!", temp.msg);
                                        if (temp.success) {
                                            for (var i = 0, r; r = storeitems[i]; i++) store.remove(r);
                                        }
                                    }
                                });
                            }
                        }
                    });
                }
            }, { xtype: 'tbseparator', hidden: levels.del },
            { text: '权限设置', iconCls: 'icon-op', ref: '../peropBtn', disabled: true, hidden: levels.setper,
                handler: function() {
                    var selectedItem = grid.getSelectionModel().getSelected();
                    var data = selectedItem.data;
                    var tab = center.getComponent('permission_' + data.ID);
                    if (!tab) {
                        tab = center.add({
                            'id': "permission_" + data.ID,
                            'title': data.RoleName + '(权限设置)',
                            closable: true,
                            iconCls: 'icontab ' + node.attributes.Icon,
                            html: '<iframe scrolling="auto" frameborder="0" width="100%" height="100%" src="/sys/loadpermission/' + data.ID + '" mce_src="/sys/loadpermission/' + data.ID + '"></iframe>'
                        });
                    }
                    center.setActiveTab(tab);
                }
            }, { xtype: 'tbseparator', hidden: levels.setper },
            { text: '数据权限', iconCls: 'icon-nav', ref: '../setdataBtn', disabled: true, hidden: levels.setdata,
                handler: function() {
                    var selectedItem = grid.getSelectionModel().getSelected();
                    var data = selectedItem.data;
                    /*var tab = center.getComponent('permissiondata_' + data.ID);
                    if (!tab) {
                    tab = center.add({
                    'id': "permissiondata_" + data.ID,
                    'title': data.RoleName + '(数据权限)',
                    closable: true,
                    iconCls: 'icontab ' + node.attributes.Icon,
                    html: '<iframe scrolling="auto" frameborder="0" width="100%" height="100%" src="/sys/loadpermissiondata/' + data.ID + '"></iframe>'
                    });
                    }
                    center.setActiveTab(tab);*/
                    jsload('/js/exts/sys/permissiondata.js', 'permissiondata',
                        { 'id': 'permissiondata_' + data.ID, 'text': data.RoleName + '(数据权限)', 'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'id': data.ID} });
                }
            }, { xtype: 'tbseparator', hidden: levels.setdata },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.load(); } }
        ],
        listeners: {
            'celldblclick': {
                fn: function() {
                    if (!levels.edit) {
                        win.setTitle('编辑角色');
                        win.show();
                        url = '/sys/roleedit';
                        form.getForm().loadRecord(grid.getSelectionModel().getSelected());
                    }
                },
                scope: this
            }
        },
        columns: [
            new Ext.grid.RowNumberer(),
            check_select,
            { header: 'ID', tooltip: "ID", dataIndex: 'ID', width: 50, sortable: true, hidden: true },
            { header: '名称', tooltip: "名称", dataIndex: 'RoleName', width: 220, sortable: true },
            { id: 'Description', tooltip: "说明", header: '说明', dataIndex: 'Description', width: 100, sortable: true }
        ]
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.peropBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
        grid.setdataBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
}
/*
function permission(node) {
var check_select = new Ext.grid.CheckboxSelectionModel();
var dataType = Ext.data.Record.create([{ name: 'ID', type: 'int' }, { name: 'FieldName', type: 'string' }, { name: 'Field', type: 'string'}]);
var store = new Ext.data.Store({ reader: new Ext.data.JsonReader({ fields: dataType }) });
var grid = new Ext.grid.GridPanel({
store: store,
columns: [
new Ext.grid.RowNumberer(),
check_select,
{ header: 'ID', width: 60, sortable: true, dataIndex: 'ID' },
{ header: '字段名称', width: 150, sortable: true, dataIndex: 'FieldName' },
{ id: 'Field', header: '字段', width: 200, sortable: true, dataIndex: 'Field' }
],
autoExpandColumn: 'Field',
sm: check_select,
frame: true,
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
var opid = '';
var win = new Ext.Window({
title: '设置字段',
closeAction: 'hide',
width: 600,
height: 300,
layout: 'fit',
plain: true,
border: false,
loadMask: true,
buttonAlign: 'center',
loadMask: true,
animateTarget: document.body,
items: grid,
buttons: [{ text: '保存',
handler: function() {
var s = grid.getSelectionModel().getSelections();
var ids = new Array();
var storeitems = new Array();
for (var i = 0, r; r = s[i]; i++) {
if (r.data.ID)
ids[ids.length] = r.data.ID;
}
Ext.Ajax.request({
url: '/sys/permissionfield',
method: 'POST',
params: { opid: opid, rid: node.id, fields: ids },
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
var checkall = new Ext.form.Checkbox({ name: 'checkall', fieldLabel: '功能名称', boxLabel: '全选', inputValue: true,
handler: function() {
var cbs = document.getElementsByName('cbopitem');
if (this.checked) {
Ext.each(cbs, function() { this.checked = true; });
} else {
Ext.each(cbs, function() { this.checked = false; });
}
}
});
var form = new Ext.form.FormPanel({
frame: true,
border: false,
plain: true,
autoScroll: true,
layout: "form",
items: [checkall],
labelWidth: 150
});

//加载角度权限
var conn = Ext.lib.Ajax.getConnectionObject().conn;
conn.open("POST", '/sys/permission/' + node.id, false);
conn.send(null);
var respPers = Ext.util.JSON.decode(conn.responseText);
//加载功能
var checklist = [];
conn.open("POST", '/sys/loadapps', false);
conn.send(null);
var respApps = Ext.util.JSON.decode(conn.responseText);
Ext.each(respApps, function(appitem) {
var id = appitem.ID;
var path = appitem.Path;
var name = appitem.Name;
var description = appitem.Description == '' ? '' : '(' + appitem.Description + ')';
name = GetCategoryPath(path, '|') + name + description;
var checkGroup = [];
//加载功能操作
conn.open("POST", '/sys/operation/' + id, false);
conn.send(null);
var respOps = Ext.util.JSON.decode(conn.responseText);
Ext.each(respOps, function(operitem, index) {
//加载功能操作是否设置字段
conn.open("POST", '/sys/field/' + operitem.ID, false);
conn.send(null);
var respFields = Ext.util.JSON.decode(conn.responseText);
//加载数据到字段设置grid中,并判断过滤字段
var chk;
if (respFields.length > 0) {
//加载过滤的字段
var respfilterFields;
if (respPers.length > 0) {
conn.open("POST", '/sys/loadpermissionfield/' + respPers[index].ID, false);
conn.send(null);
respfilterFields = Ext.util.JSON.decode(conn.responseText);
}
var ischeck = getopIscheck(respPers, operitem.ID);
var button = new Ext.Button({ text: '设置字段', cls: 'col60', disabled: !ischeck });
button.on("click", function() {
opid = operitem.ID;
win.show();
store.loadData(respFields);
grid.getSelectionModel().selectAll();
if (respfilterFields) {
Ext.each(respFields, function(gridrecord, i) {
Ext.each(respfilterFields, function(respfilterFieldsitem) {
if (respfilterFieldsitem.FieldID == gridrecord.ID)
grid.getSelectionModel().deselectRow(i);
});
});
}
})
checkGroup.push([{ boxLabel: operitem.Operation, name: 'cbopitem', inputValue: operitem.ID, checked: ischeck }, button]);
}
else {
chk = { boxLabel: operitem.Operation, name: 'cbopitem', inputValue: operitem.ID, checked: getopIscheck(respPers, operitem.ID) };
checkGroup.push(chk);
}
});
if (checkGroup.length > 0)
checklist.push(new Ext.form.CheckboxGroup({ fieldLabel: name, columns: 6, items: checkGroup }));
});
form.add(checklist);

var formwin = new Ext.Window({
loadMask: true,
title: node.text,
width: 700,
height: 400,
layout: 'fit',
closeAction: 'hide',
plain: true,
border: false,
buttonAlign: 'center',
loadMask: true,
animateTarget: document.body,
items: form,
buttons: [{
text: '保存',
handler: function() {
if (form.getForm().isValid()) {
Ext.MessageBox.show({ title: '提示框', msg: '你确定要修改吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
fn: function(btn) {
if (btn == 'ok') {
form.getForm().submit({
waitMsg: "数据保存中...",
waitTitle: "请稍侯",
url: '/sys/permissionsave',
params: { id: node.id },
success: function(form, response) {
var temp = Ext.util.JSON.decode(response.response.responseText);
Ext.Msg.alert("系统提示!", temp.msg);
formwin.hide();
},
failure: function(form, response) {
var temp = Ext.util.JSON.decode(response.response.responseText);
Ext.Msg.alert("系统提示!", temp.msg);
}
});
}
}
});
}
}
}, {
text: '取消',
handler: function() {
formwin.hide();
form.getForm().reset();
}
}
]
});
formwin.show();
};
function getopIscheck(respPers, opid) {
var check = false; length
for (var i = 0; i < respPers.length; i++) {
if (respPers[i].OperationID == opid) {
check = true;
break;
}
}
return check;
}*/