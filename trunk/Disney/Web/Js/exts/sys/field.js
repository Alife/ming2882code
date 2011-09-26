field = function(node) {
    var dataType = Ext.data.Record.create([{ name: 'ID', type: 'int' }, { name: 'FieldName', type: 'string' }, { name: 'Field', type: 'string'}]);
    var store = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/sys/field/' + node.attributes.id
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
                { xtype: 'textfield', name: 'FieldName', fieldLabel: '字段名称', anchor: '60%', allowBlank: false },
                { xtype: 'textfield', name: 'Field', fieldLabel: '字段', anchor: '60%', allowBlank: false }
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
        title: '当前位置:功能模块 > ' + node.attributes.ptitle + ' > ' + node.text,
        store: store,
        border: false,
        loadMask: true,
        autoExpandColumn: 'Field',
        sm: check_select,
        tbar: [{
            iconCls: 'icon-add',
            text: '增加',
            handler: function() { url = '/sys/fieldadd?opid=' + node.attributes.id; form.getForm().reset(); win.setTitle('增加字段'); win.show(); }
        }, '-', { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true,
            handler: function() {
                win.setTitle('编辑字段');
                win.show();
                url = '/sys/fieldedit';
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
                                url: '/sys/fielddelete',
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
        }, '-', { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.load(); } }
        ],
        listeners: {
            'celldblclick': {
                fn: function() {
                    win.setTitle('编辑字段');
                    win.show();
                    url = '/sys/fieldedit';
                    form.getForm().loadRecord(grid.getSelectionModel().getSelected());
                },
                scope: this
            }
        },
        columns: [
            new Ext.grid.RowNumberer(),
            check_select,
            { header: 'ID', tooltip: "ID", dataIndex: 'ID', width: 50, sortable: true, hidden: true },
            { header: '字段名称', tooltip: "字段名称", dataIndex: 'FieldName', width: 220, sortable: true },
            { id: 'Field', header: '字段', tooltip: "字段", dataIndex: 'Field', width: 300, sortable: true }
        ]
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
}