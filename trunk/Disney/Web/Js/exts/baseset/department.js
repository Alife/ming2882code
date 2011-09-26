﻿department = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    var url = '';
    var dataType = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'Code', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'ParentID', type: 'int' },
     	{ name: 'Path', type: 'int' },
     	{ name: 'Lft', type: 'int' },
     	{ name: 'Rgt', type: 'int' },
     	{ name: 'IsLeaf', type: 'bool'}]
    );
    var parentCmbStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/baseset/dept',
        listeners: { load: function() { this.insert(0, new Ext.data.Record({ "ID": 0, "Name": "---请选择---", "Code": '', "Path": '1' })); } }
    });
    //var store = new Ext.ux.maximgb.tg.NestedSetStore({
    var store = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ id: 'ID' }, dataType),
        url: '/baseset/dept'
    });
    store.load({ callback: function(r, options, success) { if (!success) Ext.Msg.alert('系统提示', '没有权限'); } });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        check_select,
        { header: 'ID', tooltip: "ID", width: 50, dataIndex: 'ID', hidden: true },
        { header: '代码', tooltip: "代码", width: 100, dataIndex: 'Code', sortable: true },
        { header: '名称', tooltip: "名称", width: 150, dataIndex: 'Name', id: 'Name', sortable: true }
    ]);
    //var grid = new Ext.ux.maximgb.tg.EditorGridPanel({
    var grid = new Ext.grid.GridPanel({
        expandable: false,
        autoScroll: true,
        animate: true,
        border: false,
        store: store,
        loadMask: true,
        sm: check_select,
        cm: cm,
        master_column_id: 'Name',
        autoExpandColumn: 'Name',
        title: '当前位置:' + node.text,
        tbar: [
            { text: '增加', iconCls: 'icon-add', hidden: levels.add, handler: function() { url = '/baseset/deptadd'; form.getForm().reset(); win.setTitle('增加' + node.text); win.show(); } },
            { xtype: 'tbseparator', hidden: levels.add },
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit,
                handler: function() {
                    win.setTitle('编辑' + node.text);
                    win.show();
                    url = '/baseset/deptedit';
                    var s = grid.getSelectionModel().getSelected();
                    var t = s.data.Name;
                    t = t.replace(/(^\s*)|(\s*$)/g, '');
                    s.data.Name = t.replace(/(^　*)|(　*$)/g, "")
                    form.getForm().loadRecord(s);
                }
            }, { xtype: 'tbseparator', hidden: levels.edit },
            {
                text: '删除', iconCls: 'icon-delete', ref: '../removeBtn', disabled: true, hidden: levels.del,
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
                                    url: '/baseset/deptdelete',
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
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
        ],
        listeners: {
            'celldblclick': {
                fn: function() {
                    if (!levels.edit) {
                        win.setTitle('编辑' + node.text);
                        win.show();
                        url = '/baseset/deptedit';
                        var s = grid.getSelectionModel().getSelected();
                        var t = s.data.Name;
                        t = t.replace(/(^\s*)|(\s*$)/g, '');
                        s.data.Name = t.replace(/(^　*)|(　*$)/g, "")
                        form.getForm().loadRecord(s);
                    }
                },
                scope: this
            }
        }
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
    });
    var form = new Ext.form.FormPanel({
        frame: true,
        border: false,
        plain: true,
        layout: "form",
        defaultType: "textfield",
        labelAlign: 'right', labelWidth: 80,
        items: [
                { xtype: 'hidden', name: 'ID', hidden: false },
                { xtype: 'textfield', name: 'Code', fieldLabel: '代码', width: 180, allowBlank: false },
                { xtype: 'textfield', name: 'Name', fieldLabel: '名称', width: 180, allowBlank: false },
                {
                    xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                    fieldLabel: '父类', name: 'ParentID', hiddenName: 'ParentID', displayField: 'Name', valueField: 'ID', anchor: '60%',
                    store: parentCmbStore
                }
            ]
    });
    var win = new Ext.Window({
        title: '增加' + node.text,
        closeAction: 'hide',
        width: 400,
        height: 220,
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
                            store.reload();
                        },
                        failure: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                        }
                    });
                }
            }
        }, { text: '取消', handler: function() { form.getForm().reset(); win.hide(); } }]
    });
    GridMain(node, grid);
};