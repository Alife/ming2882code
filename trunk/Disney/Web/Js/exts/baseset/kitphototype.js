kitphototype = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    var url = '';
    var categoryCmbStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create([{ name: 'displayValue' }, { name: 'displayText'}]), root: "data" }),
        url: '/baseset/getKitPhotoType/'
    });
    var categoryCmb = new Ext.form.ComboBox({
        mode: 'local', triggerAction: 'all', forceSelection: true, emptyText: '---请选择---',
        fieldLabel: '分类', name: 'Category', hiddenName: 'Category', valueField: 'displayValue', displayField: 'displayText', anchor: '60%', allowBlank: false,
        store: categoryCmbStore
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
                { xtype: 'textfield', name: 'Name', fieldLabel: '名称', width: 180, allowBlank: false },
                categoryCmb,
                { xtype: 'numberfield', name: 'Price', fieldLabel: "价格", width: 80, allowBlank: false },
                { xtype: 'numberfield', name: 'ArtPrice', fieldLabel: "美工价格", width: 80, allowBlank: false },
                { xtype: 'textfield', name: 'Formula', fieldLabel: "公式", anchor: '80%', allowBlank: false },
                { xtype: 'numberfield', name: 'OrderID', fieldLabel: '排序 ', width: 50, allowBlank: false }
            ]
    });
    var win = new Ext.Window({
        title: '增加' + node.text,
        closeAction: 'hide',
        width: 400,
        height: 230,
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
        { name: 'Name', type: 'string' },
        { name: 'Category', type: 'int' },
        { name: 'KitPhotoTypeName', type: 'string' },
        { name: 'OrderID', type: 'int' },
        { name: 'Price', type: 'decimal' },
        { name: 'ArtPrice', type: 'decimal' },
        { name: 'Formula', type: 'string'}]
    );
    var store = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/baseset/kitphototype'
    });
    store.load({ callback: function(r, options, success) { if (!success) Ext.Msg.alert('系统提示', '没有权限'); } });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        check_select,
        { header: 'ID', tooltip: "ID", dataIndex: 'ID', width: 50, hidden: true },
        { header: '名称', tooltip: "名称", dataIndex: 'Name', id: 'Name', width: 150, sortable: true },
        { header: '分类', tooltip: "分类", dataIndex: 'KitPhotoTypeName', width: 80, sortable: true },
        { header: '价格/元', tooltip: "价格/元", dataIndex: 'Price', width: 70, sortable: true },
        { header: '美工价格/元', tooltip: "美工价格/元", dataIndex: 'ArtPrice', width: 90, sortable: true },
        { header: '公式', tooltip: "公式", dataIndex: 'Formula', width: 450, sortable: true },
        { header: '排序', tooltip: "排序", dataIndex: 'OrderID', width: 80, sortable: true }
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
        autoExpandColumn: 'Name',
        title: '当前位置:' + node.text,
        tbar: [
            { text: '增加', iconCls: 'icon-add', hidden: levels.add, handler: function() { url = '/baseset/kitphototypeadd'; form.getForm().reset(); win.setTitle('增加' + node.text); win.show(); } },
            { xtype: 'tbseparator', hidden: levels.add },
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit,
                handler: function() {
                    win.setTitle('编辑' + node.text);
                    win.show();
                    url = '/baseset/kitphototypeedit';
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
                                    url: '/baseset/kitphototypedelete',
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
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections();  } }
        ],
        listeners: {
            'celldblclick': {
                fn: function() {
                    if (!levels.edit) {
                        win.setTitle('编辑' + node.text);
                        win.show();
                        url = '/baseset/kitphototypeedit';
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