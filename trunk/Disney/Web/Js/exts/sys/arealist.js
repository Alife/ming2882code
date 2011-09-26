arealist = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    var pageSize = 20;
    var record = Ext.data.Record.create([
     	{ name: 'ID' },
   		{ name: 'Name' },
   		{ name: 'Code' },
   		{ name: 'Pinyin' },
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
            url: '/sys/arealist'
        }),
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---", "Code": '', "Path": '1' }));
            }
        }
    });
    form = new Ext.form.FormPanel({
        frame: true,
        border: false,
        plain: true,
        layout: "form",
        defaultType: "textfield",
        labelAlign: 'right', labelWidth: 80,
        items: [
                { xtype: 'hidden', name: 'ID', hidden: false },
                { xtype: 'textfield', name: 'Name', fieldLabel: '名称', anchor: '60%', allowBlank: false },
                { xtype: 'textfield', name: 'Pinyin', fieldLabel: '全拼', anchor: '60%', allowBlank: false },
                { xtype: 'textfield', name: 'Code', fieldLabel: '编号', anchor: '60%', allowBlank: false },
                {
                    xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, emptyText: '---请选择---',
                    fieldLabel: '父类', name: 'ParentID', hiddenName: 'ParentID', displayField: 'Name', valueField: 'ID', anchor: '50%',
                    store: comboStore
                }
            ]
    });
    var win = new Ext.Window({
        title: '增加地区',
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
                            comboStore.reload();
                            grid.store.reload({
                                params: { start: (grid.getBottomToolbar().getPageData().activePage - 1) * pageSize, limit: pageSize }
                            });
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
        url: '/sys/arealists',
        reader: new Ext.data.JsonReader({
            fields: record,
            root: "data",
            totalProperty: "records",
            id: "ID"
        })
    });
    store.load({ params: { start: 0, limit: pageSize} });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var grid = new Ext.ux.maximgb.tg.EditorGridPanel({
        title: '当前位置:' + node.text,
        store: store,
        sm: check_select,
        border: false,
        loadMask: true,
        master_column_id: 'Name',
        autoExpandColumn: 'Name',
        tbar: [{ text: '增加', iconCls: 'icon-add', hidden: levels.add, handler: function() { url = '/sys/areaadd'; oldcode = '', form.getForm().reset(); win.setTitle('增加地区'); win.show(); }
        }, { xtype: 'tbseparator', hidden: levels.add }, {
            text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit,
            handler: function() {
                win.setTitle('编辑地区');
                win.show();
                url = '/sys/areaedit';
                form.getForm().loadRecord(grid.getSelectionModel().getSelected());
            }
        }, { xtype: 'tbseparator', hidden: levels.edit }, {
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
                                url: '/sys/areadelete',
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
        }, { xtype: 'tbseparator', hidden: levels.del }, { text: '刷新', iconCls: 'x-tbar-loading',
            handler: function() {
                grid.store.reload({
                    params: { start: (grid.getBottomToolbar().getPageData().activePage - 1) * pageSize, limit: pageSize }
                });
                comboStore.reload(); grid.getSelectionModel().clearSelections();
            }
        }
        ],
        bbar: new Ext.ux.maximgb.tg.PagingToolbar({
            store: store,
            pageSize: pageSize,
            plugins: new Ext.ux.ProgressBarPager()
        }),
        listeners: {
            'celldblclick': {
                fn: function() {
                    if (!levels.edit) {
                        win.setTitle('编辑地区');
                        win.show();
                        url = '/sys/areaedit';
                        form.getForm().loadRecord(grid.getSelectionModel().getSelected());
                    }
                },
                scope: this
            }
        },
        columns: [
            new Ext.grid.RowNumberer(),
            check_select, {
                header: "ID",
                tooltip: "ID",
                width: 50,
                sortable: true,
                dataIndex: 'ID'
            }, {
                id: 'Name',
                header: "名称",
                tooltip: "名称",
                width: 160,
                sortable: true,
                dataIndex: 'Name'
            }, {
                header: "全拼",
                tooltip: "全拼",
                width: 200,
                sortable: true,
                dataIndex: 'Pinyin'
            }, {
                header: "编号",
                tooltip: "编号",
                width: 80,
                sortable: true,
                dataIndex: 'Code'
            }, {
                header: "Lft",
                tooltip: "Lft",
                width: 80,
                sortable: true,
                dataIndex: 'Lft'
            }, {
            header: "Rgt",
                tooltip: "Rgt",
                width: 80,
                sortable: true,
                dataIndex: 'Rgt'
            }
      ]
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
}