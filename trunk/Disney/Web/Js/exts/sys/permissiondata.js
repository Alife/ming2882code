permissiondata = function(node) {
    var roleid = node.attributes.id;
    var efields = Ext.data.Record.create([{ name: 'displayValue' }, { name: 'displayText'}]);
    var confineStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/sys/confine'
    });
    confineStore.load();
    var confineCmb = new Ext.form.ComboBox({
        hiddenName: 'Confine', Name: 'Confine', fieldLabel: '级别', emptyText: '---请选择---', displayField: 'displayText', valueField: 'displayValue',
        mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, lazyRender: true, allowBlank: false, store: confineStore,
        listeners: {
            'select': function(combo, record) {
                var confine = record.get('displayValue');
                if (confine == 1 || confine == 5) {
                    detpCmb.disable();
                    userCmb.disable();
                    hidResource.disable();
                    detpCmb.setValue('');
                    userCmb.setValue('');
                    hidResource.setValue('');
                } else if (confine == 4) {
                    detpCmb.disable();
                    userCmb.enable();
                    hidResource.enable();
                    detpCmb.setValue('');
                } else {
                    detpCmb.enable();
                    userCmb.disable();
                    hidResource.disable();
                    userCmb.setValue('');
                    hidResource.setValue('');
                }
            }
        }
    });
    var resourceTypeStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/sys/resourceType'
    });
    resourceTypeStore.load();
    var resourceTypeCmb = new Ext.form.ComboBox({
        hiddenName: 'ResourceType', Name: 'ResourceType', fieldLabel: '数据类型', emptyText: '---请选择---', displayField: 'displayText', valueField: 'displayValue',
        mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, lazyRender: true, allowBlank: false, store: resourceTypeStore
    });
    var dataType = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'Code', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'OrderID', type: 'int' },
        { name: 'ParentID', type: 'int' },
        { name: 'Path', type: 'int'}]
    );
    var detpCmbStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/baseset/dept'
    });
    var detpCmb = new Ext.form.ComboBox({
        mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, disabled: true, emptyText: '---请选择---',
        fieldLabel: '机构', name: 'ResourceID', hiddenName: 'ResourceID', displayField: 'Name', valueField: 'ID',
        store: detpCmbStore
    });
    var userCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'UserCode', 'TrueName']), id: "ID" }),
        url: '/user/usersearch'
    });
    var userTpl = new Ext.XTemplate(
        '<tpl for="."><div class="search-item">',
            '{TrueName}({UserCode})',
        '</div></tpl>'
    );
    var userCmb = new Ext.form.ComboBox({
        store: userCmbStore, tpl: userTpl, name: 'ResourceText', hiddenName: 'ResourceText',
        valueField: 'ID', displayField: 'TrueName', fieldLabel: '用户', loadingText: '查询中...', minChars: 1, queryDelay: 1000,
        typeAhead: false, hideTrigger: true, allowBlank: false, disabled: true, width: 250, height: 200, pageSize: 20, itemSelector: 'div.search-item',
        listeners: {
            collapse: function() {
                if (this.getStore().getCount() == 0)
                    this.setValue('');
            }
        },
        onSelect: function(record) {
            if (record.data) {
                hidResource.setValue(record.data.ID);
                this.setValue(record.data.TrueName + '(' + record.data.UserCode + ')');
                this.collapse();
            }
        }
    });
    var hidResource = new Ext.form.Hidden({ name: 'ResourceID', disabled: true });
    var form = new Ext.form.FormPanel({
        frame: true,
        border: false,
        plain: true,
        layout: "form",
        defaultType: "textfield",
        labelAlign: 'right', labelWidth: 80,
        items: [{ xtype: 'hidden', name: 'ID', hidden: false }, resourceTypeCmb, confineCmb, detpCmb, userCmb, hidResource]
    });
    var win = new Ext.Window({
        title: '增加' + node.text,
        closeAction: 'hide',
        width: 400,
        height: 200,
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
                        params: { roleid: roleid },
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
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var fields = Ext.data.Record.create(['ID', 'RoleID', 'Confine', 'ConfineText', 'ResourceID', 'ResourceText', 'ResourceType', 'ResourceTypeText']);
    var store = new Ext.data.Store({ reader: new Ext.data.JsonReader({ fields: fields }), url: '/sys/permissiondata/' + roleid });
    store.load();
    var grid = new Ext.grid.GridPanel({
        title: '当前位置:' + node.text,
        expandable: false, autoScroll: true, animate: true, border: false, store: store, loadMask: true, sm: check_select, autoExpandColumn: 'ResourceTypeText',
        columns: [
                new Ext.grid.RowNumberer(),
                check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '数据类型', width: 170, dataIndex: 'ResourceTypeText', id: 'ResourceTypeText', sortable: true },
                { header: '级别', width: 150, dataIndex: 'ConfineText', sortable: true },
                { header: '资源', width: 200, dataIndex: 'ResourceText', sortable: true}],
        tbar: [
            { text: '增加', iconCls: 'icon-add',
                handler: function() {
                    url = '/sys/permissiondataadd';
                    form.getForm().reset();
                    win.setTitle('增加' + node.text);
                    win.show();
                    detpCmb.disable();
                    userCmb.disable();
                    hidResource.disable();
                    detpCmb.setValue('');
                    userCmb.setValue('');
                    hidResource.setValue('');
                }
            },
            { xtype: 'tbseparator' },
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, handler: function() { edit(); } },
            { xtype: 'tbseparator' },
            {
                text: '删除', iconCls: 'icon-delete', ref: '../removeBtn', disabled: true,
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
                                    url: '/sys/permissiondatadelete',
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
            }, { xtype: 'tbseparator' },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
        ],
        listeners: {
            'celldblclick': {
                fn: function() { edit(); },
                scope: this
            }
        }
    });
    function edit() {
        var s = grid.getSelectionModel().getSelected();
        win.setTitle('编辑' + node.text);
        win.show();
        url = '/sys/permissiondataedit';
        var s = grid.getSelectionModel().getSelected();
        form.getForm().loadRecord(s);
        var confine = s.data.Confine;
        if (confine == 1 || confine == 5) {
            detpCmb.disable();
            userCmb.disable();
            hidResource.disable();
            detpCmb.setValue('');
            userCmb.setValue('');
            hidResource.setValue('');
        } else if (confine == 4) {
            detpCmb.disable();
            userCmb.enable();
            hidResource.enable();
            detpCmb.setValue('');
            hidResource.setValue(s.data.ResourceID);
        } else {
            detpCmb.enable();
            userCmb.disable();
            hidResource.disable();
            userCmb.setValue('');
            hidResource.setValue('');
        }
    }
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
};