artistprice = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    var url = '';
    var kitPhotoTypeCmbStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create([{ name: 'ID' }, { name: 'Name' }, { name: 'ArtPrice'}]), root: "data" }),
        url: '/baseset/kitphototypes/'
    });
    var kitPhotoTypeCmb = new Ext.form.ComboBox({
        mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
        fieldLabel: '档图类型', name: 'KitPhotoTypeID', hiddenName: 'KitPhotoTypeID', valueField: 'ID', displayField: 'Name', width: 140, allowBlank: false,
        store: kitPhotoTypeCmbStore,
        listeners: {
            'select': function(combo, record) {
                if (record.data) {
                    priceTxt.setValue(record.data.ArtPrice);
                }
            }
        }
    });
    var priceTxt = new Ext.form.NumberField({ name: 'Price', fieldLabel: "价格", width: 80, allowBlank: false });
    var userCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'UserCode', 'TrueName']), id: "ID", root: "data", totalProperty: "records" }),
        url: String.format('/user/userlist?type={0}', 2)
    });
    var userTpl = new Ext.XTemplate(
        '<tpl for="."><div class="search-item">',
            '{TrueName}({UserCode})',
        '</div></tpl>'
    );
    var hidArterID = new Ext.form.Hidden({ name: 'UserID' });
    var form = new Ext.form.FormPanel({
        frame: true,
        border: false,
        plain: true,
        layout: "form",
        defaultType: "textfield",
        labelAlign: 'right', labelWidth: 80,
        items: [
                { xtype: 'hidden', name: 'ID', hidden: false },
                new Ext.form.ComboBox({
                    store: userCmbStore, tpl: userTpl, name: 'Arter', hiddenName: 'Arter', valueField: 'ID', displayField: 'Arter',
                    fieldLabel: '美工', loadingText: '查询中...', minChars: 1, queryDelay: 1000, queryParam: 'keyword',
                    typeAhead: false, hideTrigger: true, allowBlank: false, width: 250, height: 200, pageSize: 10, itemSelector: 'div.search-item',
                    listeners: {
                        collapse: function() {
                            if (this.getStore().getCount() == 0)
                                this.setValue('');
                        }
                    },
                    onSelect: function(record) {
                        if (record.data) {
                            hidArterID.setValue(record.data.ID);
                            this.setValue(record.data.TrueName + '(' + record.data.UserCode + ')');
                            this.collapse();
                        }
                    }
                }), hidArterID, kitPhotoTypeCmb, priceTxt
            ]
    });
    var win = new Ext.Window({
        title: '增加' + node.text,
        closeAction: 'hide',
        width: 400,
        height: 180,
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
    var dataType = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'UserID', type: 'int' },
        { name: 'Arter', type: 'string' },
        { name: 'Price', type: 'decimal' },
        { name: 'KitPhotoTypeID', type: 'int' },
        { name: 'KitPhotoType', type: 'string'}]
    );
    var store = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/baseset/artistprice'
    });
    store.load({ callback: function(r, options, success) { if (!success) Ext.Msg.alert('系统提示', '没有权限'); } });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        check_select,
        { header: 'ID', tooltip: "ID", dataIndex: 'ID', width: 50, hidden: true },
        { header: '美工', tooltip: "美工", dataIndex: 'Arter', id: 'Arter', width: 150, sortable: true },
        { header: '档图类型', tooltip: "档图类型", dataIndex: 'KitPhotoType', width: 200, sortable: true },
        { header: '价格/元', tooltip: "价格/元", dataIndex: 'Price', width: 70, sortable: true }
    ]);
    var grid = new Ext.grid.GridPanel({
        expandable: false,
        autoScroll: true,
        animate: true,
        border: false,
        store: store,
        loadMask: true,
        sm: check_select,
        cm: cm,
        autoExpandColumn: 'Arter',
        title: '当前位置:' + node.text,
        tbar: [
            { text: '增加', iconCls: 'icon-add', hidden: levels.add, handler: function() { url = '/baseset/artistpriceadd'; form.getForm().reset(); win.setTitle('增加' + node.text); win.show(); } },
            { xtype: 'tbseparator', hidden: levels.add },
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit,
                handler: function() {
                    win.setTitle('编辑' + node.text);
                    win.show();
                    url = '/baseset/artistpriceedit';
                    var s = grid.getSelectionModel().getSelected();
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
                                    url: '/baseset/artistpricedelete',
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
                        url = '/baseset/artistpriceedit';
                        var s = grid.getSelectionModel().getSelected();
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
    GridMain(node, grid);
}; 