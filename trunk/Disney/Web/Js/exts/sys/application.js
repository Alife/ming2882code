application = function(node) {
    var levels = {}
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    levels.setop = GetIsLevel(node.attributes.Code, 'setop');
    var url = '';
    var addurl = '/sys/applicationadd';
    var editurl = '/sys/applicationedit';
    var record = Ext.data.Record.create([
     	{ name: 'ID' },
   		{ name: 'Name' },
   		{ name: 'Code' },
   		{ name: 'Url' },
   		{ name: 'Description' },
   		{ name: 'IsHidden', type: 'bool' },
   		{ name: 'Icon' },
     	{ name: 'ParentID', type: 'int' },
     	{ name: 'Path', type: 'int' },
     	{ name: 'Lft', type: 'int' },
     	{ name: 'Rgt', type: 'int' },
     	{ name: 'IsLeaf', type: 'bool' }
   	]);
    var comboStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ id: 'ID' }, record),
        proxy: new Ext.data.HttpProxy({
            method: 'POST',
            url: '/sys/loadapps'
        }),
        listeners: {
            load: function() {
                this.each(function(record) {
                    var path = record.get('Path');
                    var name = record.get('Name');
                    record.set('Name', GetCategoryPath(path) + name);
                });
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---", "Path": '1' }));
            }
        }
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
                { xtype: 'textfield', name: 'Name', fieldLabel: '名称', anchor: '40%', allowBlank: false },
                { xtype: 'textfield', name: 'Code', fieldLabel: '编号', anchor: '40%', allowBlank: false, invalidText: "编号已经存在",
                    validator: function() {
                        var code = form.getForm().getValues().Code;
                        var oldcode = '';
                        if (url == editurl)
                            oldcode = grid.getSelectionModel().getSelected().data.Code;
                        var conn = Ext.lib.Ajax.getConnectionObject().conn;
                        conn.open("POST", String.format('/sys/existsapplication?code={0}&oldcode={1}', code, oldcode), false);
                        conn.send(null);
                        return Ext.util.JSON.decode(conn.responseText);
                    }
                },
                { xtype: 'textfield', name: 'Url', fieldLabel: '地址', anchor: '40%' },
                {
                    xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                    fieldLabel: '父类', name: 'ParentID', hiddenName: 'ParentID', displayField: 'Name', valueField: 'ID', anchor: '40%',
                    store: comboStore
                },
                {
                    xtype: 'compositefield', fieldLabel: '图标', combineErrors: false,
                    items: [{ name: 'Icon', xtype: 'textfield', width: 100, allowBlank: false },
                    { name: 'IsHidden', xtype: 'checkbox', boxLabel: '是否隐藏', inputValue: true}]
                },
                { xtype: 'textarea', name: 'Description', fieldLabel: '介绍', height: 100, anchor: '98%' }
            ]
    });
    var win = new Ext.Window({
        title: '增加功能',
        closeAction: 'hide',
        width: 600,
        height: 300,
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
                            comboStore.reload();
                            grid.store.reload();
                            form.reset(); win.hide();
                            grid.getSelectionModel().clearSelections();
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
    var store = new Ext.ux.maximgb.tg.NestedSetStore({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ id: 'ID' }, record),
        url: '/sys/loadapps'
        //        ,listeners: {
        //            load: function() { store.expandAll(); }
        //        }
    });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var grid = new Ext.ux.maximgb.tg.EditorGridPanel({
        title: '当前位置:' + node.text,
        store: store,
        sm: check_select,
        border: false,
        loadMask: true,
        master_column_id: 'Name',
        autoExpandColumn: 'Description',
        tbar: [
            { text: '增加', iconCls: 'icon-add', hidden: levels.add, handler: function() { url = addurl; form.getForm().reset(); win.setTitle('增加功能'); win.show(); } },
            { xtype: 'tbseparator', hidden: levels.add },
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit,
                handler: function() {
                    var selectedItem = grid.getSelectionModel().getSelected();
                    win.setTitle('编辑功能');
                    win.show();
                    url = editurl;
                    form.getForm().loadRecord(selectedItem);
                }
            }, { xtype: 'tbseparator', hidden: levels.edit },
            { text: '删除', iconCls: 'icon-delete', ref: '../removeBtn', disabled: true, hidden: levels.del,
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
                                    url: '/sys/applicationdelete',
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
            { text: '设置操作', iconCls: 'icon-op', ref: '../appopBtn', disabled: true, hidden: levels.setop,
                handler: function() {
                    var selectedItem = grid.getSelectionModel().getSelected().data;
                    var id = selectedItem.ID;
                    var tab = center.getComponent('appop_' + id);
                    if (tab)
                        center.setActiveTab(tab);
                    else
                        jsload('/js/exts/sys/operation.js', 'operation',
                        { 'id': 'appop_' + id, 'text': selectedItem.Name + '(设置)', 'attributes': { 'Icon': 'icontab ' + selectedItem.Icon, 'id': id} });
                }
            }, { xtype: 'tbseparator', hidden: levels.setop },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { grid.store.reload(); comboStore.reload(); grid.getSelectionModel().clearSelections(); } }
        ],
        columns: [
                new Ext.grid.RowNumberer(),
                check_select,
                { header: "ID", tooltip: "ID", width: 50, sortable: true, dataIndex: 'ID', hidden: true },
                { id: 'Name', header: '名称', tooltip: "名称", width: 220, dataIndex: 'Name' },
                { header: '编号', tooltip: "编号", width: 180, dataIndex: 'Code' },
                { header: '地址', tooltip: "地址", width: 250, dataIndex: 'Url' },
                { header: '图标', tooltip: "图标", width: 180, dataIndex: 'Icon' },
                { header: '是否隐藏', tooltip: "是否隐藏", width: 70, dataIndex: 'IsHidden', renderer: function(value) { if (value) return "是"; else return "否"; } },
                { id: 'Description', header: '介绍', tooltip: "介绍", width: 300, dataIndex: 'Description'}],
        listeners: {
            'celldblclick': {
                fn: function() {
                    if (!levels.edit) {
                        var selectedItem = grid.getSelectionModel().getSelected();
                        win.setTitle('编辑功能');
                        win.show();
                        url = editurl;
                        form.getForm().loadRecord(selectedItem);
                    }
                },
                scope: this
            }
        }
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
        grid.appopBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
};