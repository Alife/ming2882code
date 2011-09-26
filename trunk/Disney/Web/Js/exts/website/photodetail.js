photodetail = function(node) {
    var dataType = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'PhotoTypeID', type: 'int' },
        { name: 'Name', type: 'string' },
        { name: 'FilePath', type: 'string' },
        { name: 'FileType', type: 'string' },
        { name: 'FileSize', type: 'int' },
        { name: 'Remark', type: 'string' },
        { name: 'CreateTime', type: 'date', dateFormat: 'Y-m-d'}]);
    var store = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/website/photodetail/' + node.attributes.id
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
                { xtype: 'textfield', name: 'Name', fieldLabel: '文件名称', anchor: '70%', allowBlank: false },
                { xtype: 'textarea', name: 'Remark', fieldLabel: '文件说明', height: 80, anchor: '98%' }
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
        title: '当前位置:网站菜单管理 > ' + node.text,
        store: store,
        border: false,
        loadMask: true,
        autoExpandColumn: 'Name',
        sm: check_select,
        tbar: [{ text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true,
            handler: function() {
                win.setTitle('编辑 ' + node.text);
                win.show();
                url = '/website/photodetailedit';
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
                                url: '/website/photodetaildelete',
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
                    win.setTitle('编辑功能操作');
                    win.show();
                    url = '/website/photodetailedit';
                    form.getForm().loadRecord(grid.getSelectionModel().getSelected());
                },
                scope: this
            }
        },
        columns: [
            new Ext.grid.RowNumberer(),
            check_select,
            { header: 'ID', dataIndex: 'ID', width: 50, sortable: true, hidden: true },
            { id: 'Name', header: '文件名称', dataIndex: 'Name', width: 220, sortable: true },
            { header: '类型', dataIndex: 'FileType', width: 70, sortable: true },
            { header: '大小', dataIndex: 'FileSize', width: 100, sortable: true, renderer: function(v) { return Ext.util.Format.fileSize(v); } },
            { header: '说明', dataIndex: 'Remark', width: 250, sortable: true }
        ]
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
}