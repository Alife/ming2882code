getkitclass = function(node) {
    var kitid = node.attributes.id;
    var kitClassID = 0;
    var kitChildID = 0;
    var kitClassStoreselect = new Ext.grid.CheckboxSelectionModel();
    var kitClassFields = Ext.data.Record.create(['ID', 'Name', 'Code', 'KitID', 'BoyNum', 'GirlNum', 'Imprint']);
    var kitClassStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: kitClassFields, id: "ID" }),
        url: '/order/getclass/' + kitid
    });
    kitClassStore.load();
    var kitClassPanelcbs = new Ext.grid.CheckboxSelectionModel();
    var kitClassGrid = new Ext.grid.GridPanel({
        store: kitClassStore, autoScroll: true, expandable: false, border: false, loadMask: true, sm: kitClassPanelcbs, autoExpandColumn: "Name",
        columns: [
                    new Ext.grid.RowNumberer(),
                    kitClassStoreselect,
                    { header: "ID", width: 20, dataIndex: 'ID', hidden: true },
                    { header: "名称", width: 160, dataIndex: 'Name', id: 'Name' },
                    { header: "编号", width: 80, dataIndex: 'Code' }
                 ],
        tbar: [{ text: '刷新', iconCls: 'x-tbar-loading', handler: function() { kitClassStore.reload(); kitClassGrid.getSelectionModel().clearSelections(); } }],
        listeners: {
            "rowclick": function() {
                var s = this.getSelectionModel().getSelected();
                var id = s.data.ID;
                if (id > 0) {
                    kitClassID = id;
                    kitChildStore.proxy = new Ext.data.HttpProxy({
                        url: '/order/getchild/' + id
                    });
                    kitChildStore.load({
                        callback: function(r, options, success) {
                            var n = kitChildGrid.getStore().getCount();
                            if (n == 0) {
                                for (var i = 1; i <= s.data.BoyNum + s.data.GirlNum; i++) {
                                    var code = '';
                                    if (i.toString().length == 1) code = '0' + i; else code = i.toString();
                                    kitChildStore.add(new kitChildFields({ ID: 0, Code: code, KitClassID: kitClassID, TrueName: code, Sex: '' }));
                                }
                            }
                        }
                    });
                    kitCostumeStore.removeAll();
                }
            }
        }
    });
    var kitClassPanel = new Ext.Panel({
        border: false,
        loadMask: true,
        columnWidth: 0.33,
        items: [kitClassGrid],
        listeners: { "render": function() { kitClassGrid.setHeight(panel.getHeight() - 26); } }
    });
    //小朋友开始
    var kitChildStoreselect = new Ext.grid.CheckboxSelectionModel();
    var kitChildFields = Ext.data.Record.create(['ID', 'TrueName', 'Code', 'KitClassID', 'Sex']);
    var kitChildStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: kitChildFields, id: "ID" }),
        url: '/order/getchild/'
    });
    var sexCmb = new Ext.form.ComboBox({ name: 'Sex', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '选择',
        fieldLabel: '性别', displayField: 'displayText', valueField: 'displayValue', hiddenName: 'Sex', width: 50, allowBlank: false,
        store: new Ext.data.ArrayStore({
            fields: ['displayValue', 'displayText'],
            data: [[1, '男'], [2, '女']]
        })
    });
    var kitChildModel = new Ext.grid.ColumnModel({
        columns: [
                new Ext.grid.RowNumberer(),
                kitChildStoreselect,
                { header: "ID", width: 20, dataIndex: 'ID', hidden: true },
                { header: "姓名", width: 160, dataIndex: 'TrueName', id: 'TrueName', editor: new Ext.form.TextField({ allowBlank: false, allowNegative: false }) },
                { header: "编号", width: 80, dataIndex: 'Code', editor: new Ext.form.TextField({ allowBlank: false, allowNegative: false }) },
                { header: "性别", width: 80, dataIndex: 'Sex', editor: sexCmb, renderer: Ext.util.Format.comboRenderer(sexCmb) }
            ]
    });
    var kitChildGrid = new Ext.grid.EditorGridPanel({
        store: kitChildStore, loadMask: true, sm: kitChildStoreselect, border: false, width: '100%', clicksToEdit: 1, colModel: kitChildModel, autoExpandColumn: "TrueName",
        tbar: [{
            text: '增加', iconCls: 'icon-add', ref: '../addBtn', disabled: true,
            handler: function() {
                var n = kitChildGrid.getStore().getCount();
                var p = new kitChildFields({ ID: 0, Code: '', KitClassID: kitClassID, TrueName: '', Sex: '' });
                kitChildGrid.stopEditing();
                kitChildStore.insert(n, p);
                kitChildGrid.startEditing(n, 1);
            }
        }, {
            text: '保存', iconCls: 'icon-add', ref: '../saveBtn', disabled: true,
            handler: function() {
                var m = kitChildStore.getRange();
                var kitChildsends = new Array();
                if (m.length > 0) {
                    Ext.each(m, function(r) {
                        var item = new Object();
                        item.ID = r.get('ID');
                        item.Code = r.get('Code');
                        item.KitClassID = r.get('KitClassID');
                        item.TrueName = r.get('TrueName');
                        item.Sex = r.get('Sex') ? r.get('Sex') : '';
                        kitChildsends.push(item);
                    })
                }
                if (kitChildsends.length == 0)
                    Ext.Msg.alert("系统提示!", '没有填写小朋友信息');
                else {
                    Ext.Ajax.request({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: '/order/childadd',
                        params: { kitChildsends: Ext.util.JSON.encode(kitChildsends) },
                        success: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            kitChildStore.reload();
                        },
                        failure: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                        }
                    });
                }
            }
        }, {
            text: '删除', iconCls: 'icon-delete', ref: '../removeBtn', disabled: true,
            handler: function() {
                var s = kitChildGrid.getSelectionModel().getSelections();
                var ids = new Array();
                var storeitems = new Array();
                for (var i = 0, r; r = s[i]; i++) {
                    if (r.data.ID > 0) {
                        ids.push(r.data.ID);
                        storeitems.push(r);
                    }
                    else
                        kitChildStore.remove(r);
                }
                if (ids.length > 0) {
                    var dataCount = kitChildGrid.getStore().getTotalCount();
                    if (dataCount == ids.length)
                        Ext.Msg.alert("系统提示!", '不可以没有小朋友哦');
                    else {
                        Ext.MessageBox.show({ title: '提示框', msg: '你确定要删除吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                            fn: function(btn) {
                                if (btn == 'ok') {
                                    Ext.Ajax.request({
                                        url: '/order/childdelete/',
                                        params: { id: ids },
                                        success: function(response, options) {
                                            var temp = Ext.util.JSON.decode(response.responseText);
                                            //Ext.Msg.alert("系统提示!", temp.msg);
                                            if (temp.success) {
                                                for (var i = 0, r; r = storeitems[i]; i++) kitChildStore.remove(r);
                                            }
                                        }
                                    });
                                }
                            }
                        });
                    }
                }
            }
        }
        ],
        listeners: {
            "rowclick": function() {
                var s = this.getSelectionModel().getSelected();
                var id = s.data.ID;
                if (id > 0) {
                    kitChildID = id;
                    kitCostumeStore.proxy = new Ext.data.HttpProxy({
                        url: '/order/getcostume/' + id
                    });
                    kitCostumeStore.load();
                }
            }
        }
    });
    kitClassGrid.getSelectionModel().on('selectionchange', function(sm) {
        kitChildGrid.removeBtn.setDisabled(sm.getCount() < 1);
        kitChildGrid.addBtn.setDisabled(sm.getCount() < 1);
        kitChildGrid.saveBtn.setDisabled(sm.getCount() < 1);
    });
    var kitChildPanel = new Ext.Panel({
        border: false,
        loadMask: true,
        columnWidth: 0.33,
        items: [kitChildGrid],
        listeners: { "render": function() { kitChildGrid.setHeight(panel.getHeight() - 26); } }
    });
    //小朋友结束
    //小朋友服装开始
    var kitCostumeStoreselect = new Ext.grid.CheckboxSelectionModel();
    var kitCostumeFields = Ext.data.Record.create(['ID', 'CostumeID', 'KitChildID']);
    var kitCostumeStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: kitCostumeFields, id: "ID" }),
        url: '/order/getcostume/'
    });
    var costumestore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create([
                    { name: 'ID', type: 'int' },
                    { name: 'Name', type: 'string' },
                    { name: 'Code', type: 'string' },
                    { name: 'OrderID', type: 'int'}]
                )
        }),
        url: '/baseset/costume'
    });
    costumestore.load();
    var costumeCmb = new Ext.form.ComboBox({ name: 'CostumeID', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '选择',
        fieldLabel: '服装', displayField: 'Name', valueField: 'ID', hiddenName: 'CostumeID', width: 50, allowBlank: false,
        store: costumestore
    });
    var kitCostumeModel = new Ext.grid.ColumnModel({
        columns: [
                new Ext.grid.RowNumberer(),
                kitCostumeStoreselect,
                { header: "ID", width: 20, dataIndex: 'ID', hidden: true },
                { header: "服装", width: 120, dataIndex: 'CostumeID', id: 'CostumeID', editor: costumeCmb, renderer: Ext.util.Format.comboRenderer(costumeCmb) }
            ]
    });
    var kitCostumeGrid = new Ext.grid.EditorGridPanel({
        store: kitCostumeStore, loadMask: true, sm: kitCostumeStoreselect, border: false, width: '100%', clicksToEdit: 1, colModel: kitCostumeModel, autoExpandColumn: "CostumeID",
        tbar: [{
            text: '增加', iconCls: 'icon-add', ref: '../addBtn', disabled: true,
            handler: function() {
                var n = kitCostumeGrid.getStore().getCount();
                var p = new kitCostumeFields({ ID: 0, CostumeID: '', KitChildID: kitChildID });
                kitCostumeGrid.stopEditing();
                kitCostumeStore.insert(n, p);
                kitCostumeGrid.startEditing(n, 1);
            }
        }, {
            text: '保存', iconCls: 'icon-add', ref: '../saveBtn', disabled: true,
            handler: function() {
                var m = kitCostumeStore.getRange();
                var kitCostumesends = new Array();
                if (m.length > 0) {
                    Ext.each(m, function(r) {
                        var item = new Object();
                        item.ID = r.get('ID');
                        item.CostumeID = r.get('CostumeID');
                        item.KitChildID = r.get('KitChildID');
                        kitCostumesends.push(item);
                    })
                }
                if (kitCostumesends.length == 0)
                    Ext.Msg.alert("系统提示!", '没有填写班级信息');
                else {
                    Ext.Ajax.request({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: '/order/costumeadd',
                        params: { kitCostumesends: Ext.util.JSON.encode(kitCostumesends) },
                        success: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            kitCostumeStore.reload();
                        },
                        failure: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                        }
                    });
                }
            }
        }, {
            text: '删除', iconCls: 'icon-delete', ref: '../removeBtn', disabled: true,
            handler: function() {
                var s = kitCostumeGrid.getSelectionModel().getSelections();
                var ids = new Array();
                var storeitems = new Array();
                for (var i = 0, r; r = s[i]; i++) {
                    if (r.data.ID > 0) {
                        ids.push(r.data.ID);
                        storeitems.push(r);
                    }
                    else
                        kitCostumeStore.remove(r);
                }
                if (ids.length > 0) {
                    var dataCount = kitCostumeGrid.getStore().getTotalCount();
                    if (dataCount == ids.length)
                        Ext.Msg.alert("系统提示!", '小朋友的服装必须要有数据');
                    else {
                        Ext.MessageBox.show({ title: '提示框', msg: '你确定要删除吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                            fn: function(btn) {
                                if (btn == 'ok') {
                                    Ext.Ajax.request({
                                        url: '/order/costumedelete/',
                                        params: { id: ids },
                                        success: function(response, options) {
                                            var temp = Ext.util.JSON.decode(response.responseText);
                                            //Ext.Msg.alert("系统提示!", temp.msg);
                                            if (temp.success) {
                                                for (var i = 0, r; r = storeitems[i]; i++) kitCostumeStore.remove(r);
                                            }
                                        },
                                        failure: function(response, options) {
                                            var temp = Ext.util.JSON.decode(response.responseText);
                                            Ext.Msg.alert("系统提示!", temp.msg);
                                        }
                                    });
                                }
                            }
                        });
                    }
                }
            }
        }
        ]
    });
    kitChildGrid.getSelectionModel().on('selectionchange', function(sm) {
        kitCostumeGrid.removeBtn.setDisabled(sm.getCount() < 1);
        kitCostumeGrid.addBtn.setDisabled(sm.getCount() < 1);
        kitCostumeGrid.saveBtn.setDisabled(sm.getCount() < 1);
    });
    var kitCostumePanel = new Ext.Panel({
        border: false,
        loadMask: true,
        columnWidth: 0.33,
        items: [kitCostumeGrid],
        listeners: { "render": function() { kitCostumeGrid.setHeight(panel.getHeight() - 26); } }
    });
    //小朋友服装结束

    var panel = new Ext.Panel({
        title: '当前位置:' + node.text,
        expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, layout: 'column',
        items: [kitClassPanel, kitChildPanel, kitCostumePanel]
    });
    GridMain(node, panel);
}