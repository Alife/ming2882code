finishorder = function(node) {
    var levels = {};
    levels.uploaded = GetIsLevel(node.attributes.Code, 'uploaded');
    levels.month = GetIsLevel(node.attributes.Code, 'month');
    var frmid = '_' + node.id;
    var url = '';
    var query = { limit: 20, keyword: '', custom: '', arter: '', state: '2,3,4', proofState: '', beginTime: '', endTime: '' };
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'KitID', type: 'int' },
        { name: 'WorkName', type: 'string' },
        { name: 'OrderName', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'Code', type: 'string' },
        { name: 'Strength', type: 'int' },
        { name: 'PeopleNum', type: 'int' },
        { name: 'State', type: 'int' },
        { name: 'StateText', type: 'string' },
        { name: 'ProofState', type: 'int' },
        { name: 'ProofStateText', type: 'string' },
        { name: 'CustomID' },
        { name: 'CustomCode', type: 'string' },
        { name: 'Custom', type: 'string' },
        { name: 'IsCooperate', type: 'bool' },
        { name: 'UploadFile', type: 'string' },
        { name: 'Type', type: 'int' },
        { name: 'TypeText', type: 'string' },
        { name: 'DeptName', type: 'string' },
        { name: 'EndTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'CameraManID' },
        { name: 'CameraTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'SendTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'PlanTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'BeginTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'FinishTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'ArterID' },
        { name: 'ArterCode', type: 'string' },
        { name: 'Arter', type: 'string' },
        { name: 'KitTypeID', type: 'int' },
        { name: 'KitType', type: 'string' },
        { name: 'ClassTypeID', type: 'int' },
        { name: 'ClassType', type: 'string' },
        { name: 'InsideMaterialID', type: 'int' },
        { name: 'InsideMaterial', type: 'string' },
        { name: 'InsideName', type: 'string' },
        { name: 'CoverName', type: 'string' },
        { name: 'Template', type: 'string' },
        { name: 'TotolMonthID' },
        { name: 'Resolution' },
        { name: 'Remark'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/order/finishorder",
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
                Ext.apply(s.baseParams, { limit: query.limit, keyword: query.keyword, custom: query.custom, arter: query.arter,
                    state: query.state, proofState: query.proofState, beginTime: query.beginTime, endTime: query.endTime
                });
            }
        }
    });
    store.load({ params: { start: 0} });
    var sm = new Ext.grid.CheckboxSelectionModel();
    //    var sm = new Ext.grid.CheckboxSelectionModel({
    //        handleMouseDown: function(g, rowIndex, e) {
    //            if (e.button !== 0 || this.isLocked())
    //                return;
    //            var view = this.grid.getView();
    //            if (e.shiftKey && !this.singleSelect && this.last !== false) {
    //                var last = this.last;
    //                this.selectRange(last, rowIndex, e.ctrlKey);
    //                this.last = last;
    //                view.focusRow(rowIndex);
    //            } else {
    //                var isSelected = this.isSelected(rowIndex);
    //                if (isSelected) {
    //                    this.deselectRow(rowIndex);
    //                } else if (!isSelected || this.getCount() > 1) {
    //                    this.selectRow(rowIndex, true);
    //                    view.focusRow(rowIndex);
    //                }
    //            }
    //        },
    //        isLocked: Ext.emptyFn,
    //        initEvents: function() {
    //            Ext.grid.CheckboxSelectionModel.superclass.initEvents.call(this);
    //            this.grid.on('render', function() {
    //                var view = this.grid.getView();
    //                view.mainBody.on('mousedown', this.onMouseDown, this);
    //                Ext.fly(view.lockedInnerHd).on('mousedown', this.onHdMouseDown, this);
    //            }, this);
    //        }
    //    });
    //    sm.lock();
    //var cm = new Ext.ux.grid.LockingColumnModel([
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer({ locked: true }), sm,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true, locked: true },
                { header: '编号', tooltip: "编号", width: 100, dataIndex: 'Code', sortable: true, hidden: true, locked: true },
                { header: '名称', tooltip: "名称", width: 250, id: 'WorkName', dataIndex: 'WorkName', sortable: true, locked: true, menuDisabled: true,
                    renderer: function(value, cell) {
                        cell.attr = 'style="white-space:normal"';
                        return value;
                    }
                },
                { header: '美工', tooltip: "美工", width: 50, dataIndex: 'Arter', sortable: true, locked: true },
                { header: '负责人', tooltip: "负责人", width: 50, dataIndex: 'DeptName', sortable: true },
                { header: '状态', tooltip: "状态", width: 50, dataIndex: 'StateText', sortable: true },
                { header: '校图状态', tooltip: "校图状态", width: 70, dataIndex: 'ProofStateText', sortable: true, hidden: true },
                { header: '件类型', tooltip: "件类型", width: 70, dataIndex: 'TypeText', sortable: true },
                { header: '套图模板', tooltip: "套图模板", width: 90, dataIndex: 'Template', sortable: true },
                { header: '人数', tooltip: '人数', width: 50, dataIndex: 'PeopleNum', sortable: true },
                { header: '同学录', tooltip: "同学录", width: 75, dataIndex: 'ClassType', sortable: true },
                { header: '礼服尺寸', tooltip: "礼服尺寸", width: 70, dataIndex: 'InsideName', sortable: true },
                { header: '封面尺寸', tooltip: "封面尺寸", width: 70, dataIndex: 'CoverName', sortable: true },
                { header: '套系类型', tooltip: "套系类型", width: 70, dataIndex: 'KitType', sortable: true, hidden: true },
                { header: '封面材料', tooltip: "封面材料", width: 80, dataIndex: 'InsideMaterial', sortable: true, hidden: true },
                { header: '交货时间', tooltip: "交货时间", width: 80, dataIndex: 'EndTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d'), hidden: true },
                { header: '实际完成日期', tooltip: "实际完成日期", width: 130, dataIndex: 'FinishTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '预计完成日期', tooltip: "预计完成日期", width: 130, dataIndex: 'PlanTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s'), hidden: true },
                { header: '上傳', tooltip: '上傳', width: 50, dataIndex: 'UploadFile', sortable: true },
                { header: '账单', tooltip: '账单', width: 50, dataIndex: 'OrderName', sortable: true },
                { header: '备注', tooltip: "备注", width: 150, dataIndex: 'Remark', sortable: true,
                    renderer: function(value, cell) {
                        cell.attr = 'style="white-space:normal"';
                        return value;
                    }
                }
                ]);
    var grid = new Ext.grid.GridPanel({
        store: store, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, sm: sm, cm: cm,
        //view: new Ext.ux.grid.LockingGridView({ syncHeights: true, lockText: '锁定', unlockText: '解锁' }), //autoExpandColumn: 'WorkName',
        tbar: [
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.uploaded, handler: loadedit }, { xtype: 'tbseparator', hidden: levels.uploaded },
            { text: '已传', iconCls: 'icon-application_put', ref: '../uploadedBtn', disabled: true, hidden: levels.uploaded, handler: uploaded },
            { xtype: 'tbseparator', hidden: levels.uploaded },
            { text: '结算', iconCls: 'icon-accept', ref: '../finishBtn', disabled: true, hidden: levels.month, handler: month },
            { xtype: 'tbseparator', hidden: levels.month },
            { text: '调整结算', iconCls: 'icon-accept', ref: '../refinishBtn', disabled: true, hidden: levels.month,
                handler: function() { editmonth.show(); editmonth.get(0).get(0).setValue(grid.getSelectionModel().getSelected().data.TotolMonthID) }
            },
            { xtype: 'tbseparator', hidden: levels.month },
            { text: '合并', iconCls: 'icon-nav', ref: '../navBtn', disabled: true, hidden: levels.month, handler: navBtn },
            { xtype: 'tbseparator', hidden: levels.month },
            { text: '查看明显', iconCls: 'icon-nav', ref: '../detailBtn', disabled: true,
                handler: function() {
                    var s = grid.getSelectionModel().getSelected();
                    var data = s.data;
                    jsload('/js/exts/order/workorderdetail.js', 'workorderdetail',
                        { 'id': 'workorderdetail_' + data.ID, 'text': '工作单' + data.Name + '明显(' + data.Code + ')', 'kitorder': data,
                            'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'Code': node.attributes.Code }
                        });
                }
            },
            { xtype: 'tbseparator' },
            { text: '重算', iconCls: 'icon-nav', ref: '../retotolBtn', disabled: true, hidden: levels.month, handler: retotol },
            { xtype: 'tbseparator', hidden: levels.month },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
            ],
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        }),
        listeners: { 'celldblclick': { fn: loadedit }, 'render': function() { /*if (levels.month) cm.setHidden(1, true)*/ } }
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.detailBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
        grid.refinishBtn.setDisabled(sm.getCount() < 1);
        grid.navBtn.setDisabled(sm.getCount() < 1);
        var s = sm.getSelected();
        if (s && s.data.State == 3 && Ext.getCmp('state' + frmid).getValue() == 3)
            grid.finishBtn.setDisabled(sm.getCount() < 1);
        else
            grid.finishBtn.setDisabled(true);
        if (s && s.data.State == 2 && Ext.getCmp('state' + frmid).getValue() == 2)
            grid.uploadedBtn.setDisabled(sm.getCount() < 1);
        else
            grid.uploadedBtn.setDisabled(true);
        grid.retotolBtn.setDisabled(sm.getCount() < 1);
    });
    var efields = Ext.data.Record.create([{ name: 'displayValue' }, { name: 'displayText'}]);
    var kittypestateStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/order/kittypestate'
    });
    var kitphotostateStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/order/kitphotostate'
    });
    var userCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'UserCode', 'TrueName']), id: "ID", root: "data", totalProperty: "records" }),
        url: String.format('/user/userlist?type={0}', 2)
    });
    var codeCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'Code', 'Name', 'Custom', 'EndTime', 'Strength']), id: "ID", root: "data", totalProperty: "records" }),
        url: '/order/kit'
    });
    var hidArterID = new Ext.form.Hidden({ name: 'ArterID' });
    var hidKitID = new Ext.form.Hidden({ name: 'KitID' });
    var form = new Ext.form.FormPanel({
        frame: true, border: false, plain: true, labelAlign: 'right', labelWidth: 80,
        items: [{ xtype: 'hidden', name: 'ID', hidden: false },
                { xtype: 'textfield', name: 'WorkName', fieldLabel: '工作单名称 ', anchor: '98%', allowBlank: false },
                new Ext.form.ComboBox({
                    store: codeCmbStore,
                    tpl: new Ext.XTemplate(
                        '<tpl for="."><div class="search-item">',
                            '{Name}({Code})',
                        '</div></tpl>'
                    ), name: 'Name', hiddenName: 'Name', valueField: 'ID', displayField: 'Name', fieldLabel: '制程单',
                    loadingText: '查询中...', minChars: 1, queryDelay: 1000, queryParam: 'keyword',
                    typeAhead: false, hideTrigger: true, allowBlank: false, anchor: '98%', height: 200, pageSize: 10, itemSelector: 'div.search-item',
                    listeners: {
                        collapse: function() {
                            if (this.getStore().getCount() == 0)
                                this.setValue('');
                        }
                    },
                    onSelect: function(record) {
                        if (record.data) {
                            hidKitID.setValue(record.data.ID);
                            this.setValue(record.data.Name + '(' + record.data.Code + ')');
                            form.getForm().findField('SendTime').setValue(record.data.EndTime);
                            form.getForm().findField('PeopleNum').setValue(record.data.Strength);
                            this.collapse();
                        }
                    }
                }), hidKitID,
                {
                    xtype: 'compositefield', fieldLabel: '送件日期', combineErrors: false,
                    items: [{ xtype: 'datefield', name: 'SendTime', width: 100, allowBlank: false, format: 'Y-m-d' },
                            { xtype: 'displayfield', value: '人數:' },
                            { xtype: 'numberfield', name: 'PeopleNum', width: 50, allowBlank: false, vtypeText: '必须填写数字!' },
                            { xtype: 'displayfield', value: '工作单状态:' },
                            { xtype: 'combo', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue', width: 120, allowBlank: false,
                                name: 'State', hiddenName: 'State', triggerAction: 'all', fieldLabel: "工作单状态", emptyText: '----请选择----', store: kitphotostateStore
                            }
                    ]
                },
                {
                    xtype: 'compositefield', fieldLabel: '预计完成日期', combineErrors: false,
                    items: [
                            { xtype: 'datetimefield', name: 'PlanTime', fieldLabel: '预计完成日期 ', width: 150, /*allowBlank: false,*/format: 'Y-m-d H:i:s' },
                            { xtype: 'displayfield', value: '件类型:' },
                            { xtype: 'combo', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue', width: 120, allowBlank: false,
                                name: 'Type', hiddenName: 'Type', triggerAction: 'all', fieldLabel: "件类型", emptyText: '----请选择----', store: kittypestateStore
                            }
                    ]
                },
                {
                    xtype: 'compositefield', fieldLabel: '美工', combineErrors: false,
                    items: [new Ext.form.ComboBox({
                        store: userCmbStore,
                        tpl: new Ext.XTemplate(
                        '<tpl for="."><div class="search-item">',
                            '{TrueName}({UserCode})',
                        '</div></tpl>'
                    ), name: 'Arter', hiddenName: 'Arter', valueField: 'ID', displayField: 'Arter', fieldLabel: '美工',
                        loadingText: '查询中...', minChars: 1, queryDelay: 1000, queryParam: 'keyword',
                        typeAhead: false, hideTrigger: true, allowBlank: false, width: '380', height: 200, pageSize: 10, itemSelector: 'div.search-item',
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
                    }), { name: 'IsCooperate', xtype: 'checkbox', boxLabel: '是否合作完成', inputValue: true}]
                }, hidArterID, { xtype: 'textfield', name: 'UploadFile', fieldLabel: '上傳文件夾 ', anchor: '98%' },
                { xtype: 'textarea', name: 'Remark', fieldLabel: '备注', height: 80, anchor: '98%'}]
    });
    var win = new Ext.Window({
        closeAction: 'hide', width: 500, height: 320, layout: 'fit', plain: true, border: false, /*modal: 'true',*/buttonAlign: 'center', loadMask: true, animateTarget: document.body,
        items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: url,
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
        }, { text: '取消', handler: function() { form.getForm().reset(); win.hide(); } }]
    });
    function loadedit() {
        if (!levels.uploaded) {
            win.setTitle('生成工作单');
            win.show();
            url = '/order/kitworkedit';
            var s = grid.getSelectionModel().getSelected();
            form.getForm().loadRecord(s);
        }
    }
    function navBtn() {
        var workname = Ext.util.Format.uppercase(grid.getSelectionModel().getSelected().data.Code);
        var s = grid.getSelectionModel().getSelections();
        var ids = new Array(); var kitids = new Array(); var names = new Array();
        for (var i = 0, r; r = s[i]; i++) {
            if (r.data.ID && (r.data.ProofState == 1 || r.data.ProofState == 3)) {
                ids.push(r.data.ID);
                kitids.push(r.data.KitID);
            }
        }
        if (ids.length > 1) {
            for (var i = 0, r; r = kitids[i]; i++) {
                if (r != kitids[0]) {
                    Ext.Msg.alert("系统提示!", '请选择多个同档并正在校图的记录');
                    return;
                } else {
                    names.push(Ext.util.Format.uppercase(s[i].data.WorkName).replace(workname, ''));
                }
            }
            workname += names.join('、');
            Ext.MessageBox.show({ title: '提示框', msg: '你确定合并吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                fn: function(btn) {
                    if (btn == 'ok') {
                        Ext.Ajax.request({
                            url: '/order/coalition/',
                            params: { id: ids, workname: workname },
                            method: "POST",
                            success: function(response, options) {
                                var temp = Ext.util.JSON.decode(response.responseText);
                                //Ext.Msg.alert("系统提示!", temp.msg);
                                store.reload();
                            },
                            failure: function(response, options) {
                                var temp = Ext.util.JSON.decode(response.responseText);
                                Ext.Msg.alert("系统提示!", '失败');
                            }
                        });
                    }
                }
            });
        } else
            Ext.Msg.alert("系统提示!", '请选择多个同档的记录');
    }
    function month() {
        var s = grid.getSelectionModel().getSelections();
        var ids = new Array();
        for (var i = 0, r; r = s[i]; i++) {
            if (r.data.ID)//&& r.data.State == 4)
                ids.push(r.data.ID);
        }
        Ext.MessageBox.show({ title: '提示框', msg: '你确定结算吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/order/totolmonth/',
                        params: { id: ids },
                        method: "POST",
                        success: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            store.reload();
                        },
                        failure: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            Ext.Msg.alert("系统提示!", '失败');
                        }
                    });
                }
            }
        });
    }
    var monthStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'OrderName']), id: "ID" }),
        url: '/order/gettotolmonth'
    });
    if (!levels.month)
        monthStore.load();
    var editmonth = new Ext.Window({
        closeAction: 'hide', width: 300, height: 100, layout: 'fit', plain: true, border: false, /*modal: 'true',*/buttonAlign: 'center', loadMask: true, animateTarget: document.body,
        items: new Ext.form.FormPanel({
            frame: true, border: false, plain: true, labelAlign: 'right', labelWidth: 80,
            items: new Ext.form.ComboBox({ xtype: 'combo', mode: 'local', editable: false, displayField: 'OrderName', valueField: 'ID', width: 100, allowBlank: false,
                name: 'TotolMonthID', hiddenName: 'TotolMonthID', triggerAction: 'all', emptyText: '----请选择----', store: monthStore
            })
        }),
        buttons: [{ text: '保存',
            handler: function() {
                var efrm = editmonth.get(0);
                if (efrm.getForm().isValid()) {
                    efrm.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: '/order/editmonth',
                        params: { id: grid.getSelectionModel().getSelected().data.ID },
                        success: function(frm, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            efrm.getForm().reset(); editmonth.hide();
                            store.reload({ params: { start: (grid.getBottomToolbar().getPageData().activePage - 1) * query.limit} });
                        },
                        failure: function(frm, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                        }
                    });
                }
            }
        }, { text: '取消', handler: function() { editmonth.get(0).getForm().reset(); editmonth.hide(); } }]
    });
    function uploaded() {
        var s = grid.getSelectionModel().getSelections();
        var ids = new Array();
        for (var i = 0, r; r = s[i]; i++) {
            if (r.data.ID)//&& r.data.State == 4)
                ids.push(r.data.ID);
        }
        Ext.MessageBox.prompt('Name', '上傳文件夾:', function(btn, text) {
            if (btn == 'ok') {
                Ext.Ajax.request({
                    url: '/order/uploaded/',
                    params: { id: ids, uploadFile: text },
                    method: "POST",
                    success: function(response, options) {
                        var temp = Ext.util.JSON.decode(response.responseText);
                        //Ext.Msg.alert("系统提示!", temp.msg);
                        store.reload();
                    },
                    failure: function(response, options) {
                        var temp = Ext.util.JSON.decode(response.responseText);
                        Ext.Msg.alert("系统提示!", '失败');
                    }
                });
            }
        });
    }
    function retotol() {
        var s = grid.getSelectionModel().getSelections();
        var ids = new Array();
        for (var i = 0, r; r = s[i]; i++) {
            if (r.data.ID)//&& r.data.State == 4)
                ids.push(r.data.ID);
        }
        Ext.MessageBox.show({ title: '提示框', msg: '你确定重算吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/order/retotol/',
                        params: { id: ids },
                        method: "POST",
                        success: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            store.reload();
                        },
                        failure: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            Ext.Msg.alert("系统提示!", '失败');
                        }
                    });
                }
            }
        });
    }
    var efields = Ext.data.Record.create([{ name: 'displayValue' }, { name: 'displayText'}]);
    var kitproofstateStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/order/kitproofstate',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "displayValue": '', "displayText": "---请选择---" }));
            }
        }
    });
    var searchPanel = new Ext.Panel({
        title: '当前位置:' + node.text, layout: 'fit', border: false,
        tbar: [
                '&nbsp;名称/编号:', { xtype: "textfield", id: 'keyword' + frmid, width: 120, listeners: { 'blur': search} },
                '&nbsp;状态:', { xtype: 'combo', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue', width: 100,
                    id: 'state' + frmid, name: 'State', hiddenName: 'State', triggerAction: 'all', value: query.state,
                    store: new Ext.data.ArrayStore({ fields: ['displayValue', 'displayText'], data: [['2,3,4', '----请选择----'], ['2', '完成'], ['3', '已传'], ['4', '结算']] }),
                    listeners: { "select": search }
                },
                '&nbsp;校图状态:', { xtype: 'combo', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue', width: 100,
                    id: 'proofstate' + frmid, name: 'ProofState', hiddenName: 'ProofState', triggerAction: 'all', emptyText: '----请选择----', store: kitproofstateStore,
                    listeners: { "select": search }
                },
                { xtype: 'displayfield', value: '&nbsp;美工:', hidden: typeInfo.Type == 2 }, { xtype: "textfield", id: 'arter' + frmid, width: 100, hidden: typeInfo.Type == 2 },
                '&nbsp;完成时间: ',
                { xtype: 'datefield', id: 'beginTime' + frmid, format: 'Y-m-d', width: 100 }, '--',
                { xtype: 'datefield', id: 'endTime' + frmid, format: 'Y-m-d', width: 100 }, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: { "click": search }
                }
            ],
        items: [grid]
    });
    function search() {
        query.keyword = Ext.getCmp('keyword' + frmid).getValue();
        query.state = Ext.getCmp('state' + frmid).getValue();
        query.proofState = Ext.getCmp('proofstate' + frmid).getValue();
        query.arter = Ext.getCmp('arter' + frmid).getValue();
        query.beginTime = Ext.getCmp('beginTime' + frmid).getValue();
        query.endTime = Ext.getCmp('endTime' + frmid).getValue();
        store.load({ params: { start: 0} });
    }
    GridMain(node, searchPanel);
}