operation = function(node) {
    var dataType = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'Operation', type: 'string' },
        { name: 'Code', type: 'string' },
        { name: 'OrderID', type: 'int' },
        { name: 'Icon', type: 'string'}]);
    var store = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/sys/operation/' + node.attributes.id
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
                { xtype: 'textfield', name: 'Operation', fieldLabel: '名称', anchor: '70%', allowBlank: false },
                { xtype: 'textfield', name: 'Code', fieldLabel: '标识', anchor: '70%', allowBlank: false },
                { xtype: 'numberfield', name: 'OrderID', fieldLabel: '排序', anchor: '30%', allowBlank: false, vtypeText: '必须填写数字!' },
                { xtype: 'textfield', name: 'Icon', fieldLabel: '图标', anchor: '50%', allowBlank: false }
            ]
    });
    var win = new Ext.Window({
        title: '',
        closeAction: 'hide',
        width: 500,
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
                        success: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            form.reset(); win.hide();
                            grid.store.reload();
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
    var grid = new Ext.grid.GridPanel({
        title: '当前位置:功能模块 > ' + node.text,
        store: store,
        border: false,
        loadMask: true,
        autoExpandColumn: 'Operation',
        sm: check_select,
        tbar: [{
            iconCls: 'icon-add',
            text: '增加',
            handler: function() { url = '/sys/operationadd?appid=' + node.attributes.id; form.getForm().reset(); win.setTitle('增加功能操作'); win.show(); }
        }, '-', { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true,
            handler: function() {
                win.setTitle('编辑功能操作');
                win.show();
                url = '/sys/operationedit';
                form.getForm().loadRecord(grid.getSelectionModel().getSelected());
            }
        }, '-', {
            ref: '../removeBtn',
            iconCls: 'icon-delete',
            text: '删除',
            disabled: true,
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
                                url: '/sys/operationdelete',
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
        }, '-', { text: '设置字段', iconCls: 'icon-op', ref: '../fielesetBtn', disabled: true,
            handler: function() {
                var selectedItem = grid.getSelectionModel().getSelected();
                var data = selectedItem.data;
                var tab = center.getComponent('opfield_' + data.ID);
                if (tab)
                    center.setActiveTab(tab);
                else
                    jsload('/js/exts/sys/field.js', 'field',
                        { 'id': 'opfield_' + data.ID, 'text': data.Operation + '(字段设置)',
                            'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'id': data.ID, ptitle: node.text }
                        });
            }
        }, '-', { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.load(); } }
        ],
        listeners: {
            'celldblclick': {
                fn: function() {
                    win.setTitle('编辑功能操作');
                    win.show();
                    url = '/sys/operationedit';
                    form.getForm().loadRecord(grid.getSelectionModel().getSelected());
                },
                scope: this
            }
        },
        columns: [
            new Ext.grid.RowNumberer(),
            check_select,
            { header: 'ID', dataIndex: 'ID', width: 50, sortable: true, hidden: true },
            { id: 'Operation', header: '名称', dataIndex: 'Operation', width: 220, sortable: true },
            { header: '标识', dataIndex: 'Code', width: 150, sortable: true },
            { header: '排序', dataIndex: 'OrderID', width: 50, sortable: true },
            { header: '图标', dataIndex: 'Icon', width: 150, sortable: true }
        ]
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
        grid.fielesetBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
}