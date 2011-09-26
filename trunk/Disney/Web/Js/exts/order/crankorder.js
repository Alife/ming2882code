crankorder = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    var frmid = '_' + node.id;
    var url = '';
    var isedit = false;
    var query = { limit: 20, keyword: '', custom: '', state: '', beginTime: '', endTime: '' };
    var editurl = '/order/kitedit';
    var addurl = '/order/kitadd';
    var kittypeStore = new Ext.data.JsonStore({
        autoLoad: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/baseset/getkittype',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    var kittemplateStore = new Ext.data.JsonStore({
        autoLoad: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/baseset/kittemplate',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    var classTypeStore = new Ext.data.JsonStore({
        autoLoad: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/baseset/classtype',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    var insideMaterialStore = new Ext.data.JsonStore({
        autoLoad: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/baseset/insidematerial',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    //班级信息
    var classStoreselect = new Ext.grid.CheckboxSelectionModel();
    var classFields = Ext.data.Record.create(['ID', 'Name', 'Code', 'KitID', 'BoyNum', 'GirlNum', 'Imprint']);
    var classStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: classFields, id: "ID" }),
        url: '/order/getclass/'
    });
    //classStore.load();
    var classModel = new Ext.grid.ColumnModel({
        columns: [
                    new Ext.grid.RowNumberer(),
                    classStoreselect,
                    { header: "ID", width: 20, dataIndex: 'ID', hidden: true },
                    { header: "名称", width: 180, dataIndex: 'Name', id: 'Name', editor: new Ext.form.TextField({ allowBlank: false, allowNegative: false }) },
                    { header: "编号", width: 80, dataIndex: 'Code', editor: new Ext.form.TextField({ allowBlank: false, allowNegative: false }) },
                    { header: "男生人数", width: 70, dataIndex: 'BoyNum', editor: new Ext.form.NumberField({ allowBlank: false, allowNegative: false }) },
                    { header: "女生人数", width: 70, dataIndex: 'GirlNum', editor: new Ext.form.NumberField({ allowBlank: false, allowNegative: false }) },
                    { header: "图照刻印内容", width: 110, dataIndex: 'Imprint', editor: new Ext.form.TextField({ allowBlank: false, allowNegative: false }) }
                 ]
    });
    var classGrid = new Ext.grid.EditorGridPanel({
        store: classStore, loadMask: true, sm: classStoreselect, border: false, clicksToEdit: 1, colModel: classModel, autoExpandColumn: "Name",
        tbar: [
            {
                text: '增加', iconCls: 'icon-add',
                handler: function() {
                    var n = classGrid.getStore().getCount();
                    var p = new classFields({ ID: 0, Code: '', Name: '', BoyNum: '', GirlNum: '', Imprint: '' });
                    classGrid.stopEditing();
                    classStore.insert(n, p);
                    classGrid.startEditing(n, 1);
                }
            },
            {
                text: '删除', iconCls: 'icon-delete', ref: '../removeBtn', disabled: true,
                handler: function() {
                    var s = classGrid.getSelectionModel().getSelections();
                    var ids = new Array();
                    var storeitems = new Array();
                    for (var i = 0, r; r = s[i]; i++) {
                        if (r.data.ID > 0) {
                            ids.push(r.data.ID);
                            storeitems.push(r);
                        }
                        else
                            classStore.remove(r);
                    }
                    if (ids.length > 0) {
                        var dataCount = classGrid.getStore().getTotalCount();
                        if (dataCount == ids.length)
                            Ext.Msg.alert("系统提示!", '班级必须要有数据');
                        else {
                            Ext.MessageBox.show({ title: '提示框', msg: '你确定要删除吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                                fn: function(btn) {
                                    if (btn == 'ok') {
                                        Ext.Ajax.request({
                                            url: '/order/classdelete/',
                                            params: { id: ids },
                                            success: function(response, options) {
                                                var temp = Ext.util.JSON.decode(response.responseText);
                                                //Ext.Msg.alert("系统提示!", temp.msg);
                                                if (temp.success) {
                                                    for (var i = 0, r; r = storeitems[i]; i++) classStore.remove(r);
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
        ]
    });
    classGrid.getSelectionModel().on('selectionchange', function(sm) { if (isedit) classGrid.removeBtn.setDisabled(sm.getCount() < 1); else classGrid.removeBtn.setDisabled(true) });
    var customCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'UserCode', 'TrueName']), id: "ID", root: "data", totalProperty: "records" }),
        url: '/user/customlist?type=6'
    });
    var userTpl = new Ext.XTemplate(
        '<tpl for="."><div class="search-item">',
            '{TrueName}({UserCode})',
        '</div></tpl>'
    );
    var customCmb = new Ext.form.ComboBox({
        store: customCmbStore, tpl: userTpl, name: 'Custom', hiddenName: 'Custom',
        valueField: 'ID', displayField: 'Custom', fieldLabel: '园所', loadingText: '查询中...', minChars: 1, queryDelay: 1000, queryParam: 'keyword',
        typeAhead: false, hideTrigger: true, allowBlank: false, width: 250, height: 200, pageSize: 20, itemSelector: 'div.search-item',
        listeners: {
            collapse: function() {
                if (this.getStore().getCount() == 0)
                    this.setValue('');
            }
        },
        onSelect: function(record) {
            if (record.data) {
                form.getForm().findField('CustomID').setValue(record.data.ID);
                this.setValue(record.data.TrueName + '(' + record.data.UserCode + ')');
                this.collapse();
            }
        }
    });
    var userCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'UserCode', 'TrueName']), id: "ID", root: "data", totalProperty: "records" }),
        url: '/user/stafflist?type=7'
    });
    var userCmb = new Ext.form.ComboBox({
        store: userCmbStore, tpl: userTpl, name: 'Writer', hiddenName: 'Writer',
        valueField: 'ID', displayField: 'Writer', fieldLabel: '负责人', loadingText: '查询中...', minChars: 1, queryDelay: 1000, queryParam: 'keyword',
        typeAhead: false, hideTrigger: true, width: 250, height: 200, pageSize: 20, itemSelector: 'div.search-item',
        listeners: {
            collapse: function() {
                if (this.getStore().getCount() == 0)
                    this.setValue('');
            },
            render: function() { form.getForm().findField('UserID').setValue(263); }
        },
        onSelect: function(record) {
            if (record.data) {
                form.getForm().findField('UserID').setValue(record.data.ID);
                this.setValue(record.data.TrueName + '(' + record.data.UserCode + ')');
                this.collapse();
            }
        }
    });
    var form = new Ext.form.FormPanel({
        border: false, waitMsgTarget: true, autoTabs: true, plain: true, labelAlign: 'right',
        items: {
            xtype: 'tabpanel', border: false, activeTab: 0, deferredRender: false, defaults: { autoHeight: true },
            items: [{
                title: '基本信息', layout: 'form', defaultType: 'textfield', labelWidth: 100, style: 'padding-top:5px',
                items: [
                        { xtype: 'hidden', name: 'ID', hidden: false },
                        { xtype: 'textfield', name: 'Code', fieldLabel: '单据编号', width: 180, allowBlank: false },
                        { xtype: 'textfield', name: 'Name', fieldLabel: '单据名称', width: 180, hidden: true, hideLabel: true },
                        customCmb, { xtype: 'hidden', name: 'CustomID', hidden: false },
                        userCmb, { xtype: 'hidden', name: 'UserID', hidden: false },
                        {
                            xtype: 'compositefield', fieldLabel: '套系类型', combineErrors: false,
                            items: [
                                    {
                                        xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                                        fieldLabel: '套系类型', name: 'KitTypeID', hiddenName: 'KitTypeID', displayField: 'Name', valueField: 'ID', width: 120, allowBlank: false,
                                        store: kittypeStore
                                    },
                                    { xtype: 'displayfield', value: '套图模板:' },
                                    {
                                        xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                                        fieldLabel: "工作单类型", name: 'TemplateID', hiddenName: 'TemplateID', displayField: 'Name', valueField: 'ID', width: 120, allowBlank: false,
                                        store: kittemplateStore
                                    }
                            ]
                        },
                        {
                            xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                            fieldLabel: '同学录套图', name: 'ClassTypeID', hiddenName: 'ClassTypeID', displayField: 'Name', valueField: 'ID', width: 120, allowBlank: false,
                            store: classTypeStore, value: '4'
                        },
                        { xtype: 'compositefield', fieldLabel: '封面材料', combineErrors: false,
                            items: [{
                                xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                                fieldLabel: '封面材料', name: 'InsideMaterialID', hiddenName: 'InsideMaterialID', displayField: 'Name', valueField: 'ID', width: 120, allowBlank: false,
                                store: insideMaterialStore, value: '1'
                            }, { xtype: 'displayfield', value: '解析度:' }, { xtype: 'numberfield', name: 'Resolution', fieldLabel: "解析度", width: 50, allowBlank: false, value: '254' }
                            ]
                        },
                        { xtype: 'datefield', name: 'EndTime', fieldLabel: '交货时间 ', width: 100, allowBlank: false, format: 'Y-m-d' },
                        { xtype: 'textarea', name: 'Remark', fieldLabel: '介绍', height: 60, anchor: '98%'}]
            }, { title: '班级信息', items: [classGrid]}]
        }, listeners: { "render": function() { classGrid.setHeight(win.getHeight() - 97); } }
    });
    var win = new Ext.Window({
        closeAction: 'hide', width: 600, height: 380, layout: 'fit', plain: true, border: false, /*modal: 'true',*/buttonAlign: 'center', loadMask: true,
        animateTarget: document.body, items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    var m = classStore.getRange();
                    var classsends = new Array();
                    if (m.length > 0) {
                        Ext.each(m, function(r) {
                            var item = new Object();
                            item.ID = r.get('ID');
                            item.Code = r.get('Code');
                            item.Name = r.get('Name');
                            item.BoyNum = r.get('BoyNum');
                            item.GirlNum = r.get('GirlNum');
                            item.Imprint = r.get('Imprint');
                            classsends.push(item);
                        })
                    }
                    if (classsends.length == 0)
                        Ext.Msg.alert("系统提示!", '没有填写班级信息');
                    else {
                        form.getForm().submit({
                            waitMsg: "数据保存中...",
                            waitTitle: "请稍侯",
                            url: url,
                            params: { classsends: Ext.util.JSON.encode(classsends) },
                            success: function(frm, response) {
                                var temp = Ext.util.JSON.decode(response.response.responseText);
                                //Ext.Msg.alert("系统提示!", temp.msg);
                                form.getForm().reset(); win.hide();
                                store.reload({ params: { start: (grid.getBottomToolbar().getPageData().activePage - 1) * query.limit} });
                            },
                            failure: function(frm, response) {
                                var temp = Ext.util.JSON.decode(response.response.responseText);
                                Ext.Msg.alert("系统提示!", temp.msg);
                            }
                        });
                    }
                }
            }
        }, { text: '取消', handler: function() { form.getForm().reset(); win.hide(); } }]
    });
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'Name', type: 'string' },
        { name: 'Code', type: 'string' },
        { name: 'State', type: 'int' },
        { name: 'Strength', type: 'int' },
        { name: 'StateText', type: 'string' },
        { name: 'CustomID' },
        { name: 'CustomCode', type: 'string' },
        { name: 'Custom', type: 'string' },
        { name: 'UserID' },
        { name: 'UserCode', type: 'string' },
        { name: 'Writer', type: 'string' },
        { name: 'EndTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'CameraManID' },
        { name: 'CameraTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'KitTypeID', type: 'int' },
        { name: 'KitType', type: 'string' },
        { name: 'DeptName', type: 'string' },
        { name: 'ClassTypeID', type: 'int' },
        { name: 'ClassType', type: 'string' },
        { name: 'InsideMaterialID', type: 'int' },
        { name: 'InsideMaterial', type: 'string' },
        { name: 'InsideName', type: 'string' },
        { name: 'CoverName', type: 'string' },
        { name: 'TemplateID', type: 'int' },
        { name: 'Template', type: 'string' },
        { name: 'Resolution' },
        { name: 'IsValid' },
        { name: 'Remark'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/order/kit",
            method: "POST"
        }),
        reader: new Ext.data.JsonReader({
            fields: fields,
            root: "data",
            id: "ID",
            totalProperty: "records"
        }),
        listeners: {
            beforeload: function(s, options) {
                Ext.apply(s.baseParams, { limit: query.limit, keyword: query.keyword, custom: query.custom, state: query.state,
                    beginTime: query.beginTime, endTime: query.endTime
                });
            }
        }
    });
    store.load({ params: { start: 0} });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var grid = new Ext.grid.GridPanel({
        store: store, sm: check_select, autoExpandColumn: 'Name', expandable: false, autoScroll: true, animate: true, border: false, loadMask: true,
        tbar: [
                    { text: '增加', iconCls: 'icon-add', hidden: levels.add,
                        handler: function() {
                            url = addurl;
                            win.setTitle('增加' + node.text);
                            win.show();
                            classStore.removeAll();
                            //setformitem(form, win, 0);
                            isedit = true;
                        }
                    }, { xtype: 'tbseparator', hidden: levels.add },
                    { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit, handler: loadedit }, { xtype: 'tbseparator', hidden: levels.edit },
                    { text: "班级明细", iconCls: 'icon-user_member', ref: '../classBtn', disabled: true, hidden: levels.edit,
                        handler: function() {
                            var data = grid.getSelectionModel().getSelected().data;
                            jsload('/js/exts/order/getkitclass.js', 'getkitclass',
                            { 'id': 'getkitclass_' + data.ID, 'text': data.Name + '(' + data.Code + ')', 'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'id': data.ID} });
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
                                            url: '/order/deletekit',
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
        listeners: { 'celldblclick': { fn: loadedit} },
        columns: [
                new Ext.grid.RowNumberer(),
                check_select,
                { header: 'ID', tooltip: "ID", width: 50, dataIndex: 'ID', hidden: true },
                { header: '编号', tooltip: "编号", width: 100, dataIndex: 'Code', sortable: true },
                { header: '名称', tooltip: "名称", width: 150, id: 'Name', dataIndex: 'Name', sortable: true },
                { header: '客户', tooltip: "客户", width: 140, dataIndex: 'Custom', sortable: true, hidden: true },
                { header: '负责人', tooltip: "负责人", width: 50, dataIndex: 'Writer', sortable: true },
                { header: '人数', tooltip: "人数", width: 50, dataIndex: 'Strength', sortable: true },
                { header: '状态', tooltip: "状态", width: 50, dataIndex: 'StateText', sortable: true },
                { header: '套系类型', tooltip: "套系类型", width: 100, dataIndex: 'KitType', sortable: true },
                { header: '套图模板', tooltip: "套图模板", width: 100, dataIndex: 'Template', sortable: true },
                { header: '同学录', tooltip: "同学录", width: 75, dataIndex: 'ClassType', sortable: true },
                { header: '礼服尺寸', tooltip: "礼服尺寸", width: 70, dataIndex: 'InsideName', sortable: true },
                { header: '封面尺寸', tooltip: "封面尺寸", width: 70, dataIndex: 'CoverName', sortable: true },
                { header: '封面材料', tooltip: "封面材料", width: 100, dataIndex: 'InsideMaterial', sortable: true, hidden: true },
                { header: '交货时间', tooltip: "交货时间", width: 80, dataIndex: 'EndTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d') },
                { header: '备注', tooltip: "备注", width: 120, dataIndex: 'Remark', sortable: true}],
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        })
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
        grid.classBtn.setDisabled(sm.getCount() < 1);
    });
    function loadedit() {
        var s = grid.getSelectionModel().getSelected();
        win.setTitle('编辑' + node.text);
        win.show();
        url = editurl;
        classStore.removeAll();
        classStore.proxy = new Ext.data.HttpProxy({ url: String.format('/order/getclass/{0}', s.data.ID) });
        classStore.load();
        form.getForm().loadRecord(s);
        //if (s.data.State > 1) {
        //    setformitem(form, win, 1);
        //    isedit = false;
        //} else
        isedit = true;
        if (userInfo.ID == 1) win.buttons[0].setDisabled(false);
    };
    var efields = Ext.data.Record.create([{ name: 'displayValue' }, { name: 'displayText'}]);
    var kitstateStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/order/kitstate',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "displayValue": '', "displayText": "---请选择---" }));
            }
        }
    });
    var searchPanel = new Ext.Panel({
        layout: 'fit',
        border: false,
        title: '当前位置:' + node.text,
        tbar: [
                '&nbsp;名称/编号:', { xtype: "textfield", id: 'keyword' + frmid, width: 120 },
                '&nbsp;园所名称/编号:', { xtype: "textfield", id: 'custom' + frmid, width: 120 },
                '&nbsp;交货时间:&nbsp; ',
                { xtype: 'datefield', id: 'beginTime' + frmid, format: 'Y-m-d' }, '--',
                { xtype: 'datefield', id: 'endTime' + frmid, format: 'Y-m-d' },
                '&nbsp;状态:', { xtype: 'combo', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue', width: 120,
                    id: 'state' + frmid, name: 'State', hiddenName: 'State', triggerAction: 'all', emptyText: '----请选择----', store: kitstateStore
                },
                '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: {
                        "click": function() {
                            query.keyword = Ext.getCmp('keyword' + frmid).getValue();
                            query.state = Ext.getCmp('state' + frmid).getValue();
                            query.custom = Ext.getCmp('custom' + frmid).getValue();
                            query.beginTime = Ext.getCmp('beginTime' + frmid).getValue();
                            query.endTime = Ext.getCmp('endTime' + frmid).getValue();
                            store.load({ params: { start: 0} });
                        }
                    }
                }
            ],
        items: [grid]
    });
    GridMain(node, searchPanel);
}
