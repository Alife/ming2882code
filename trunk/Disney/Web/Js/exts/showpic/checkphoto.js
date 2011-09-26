checkphoto = function(node) {
    var levels = {};
    levels.checkphoto = GetIsLevel(node.attributes.Code, 'checkphoto');
    levels.solve = GetIsLevel(node.attributes.Code, 'solve');
    levels.finish = GetIsLevel(node.attributes.Code, 'finish');
    levels.recheck = GetIsLevel(node.attributes.Code, 'recheck');
    levels.startdeal = GetIsLevel(node.attributes.Code, 'startdeal');
    var kitworkid = 0;
    var query = { limit: 20, keyword: '', custom: '', arter: '', state: '2,3,4', proofState: '', beginTime: '', endTime: '' };
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'WorkName', type: 'string' },
        { name: 'KitID', type: 'int' },
        { name: 'Name', type: 'string' },
        { name: 'Code', type: 'string' },
        { name: 'State', type: 'int' },
        { name: 'StateText', type: 'string' },
        { name: 'ProofState', type: 'int' },
        { name: 'ProofStateText', type: 'string' },
        { name: 'ProofBeginTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'ProofEndTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'Strength', type: 'int' },
        { name: 'CustomID' },
        { name: 'CustomCode', type: 'string' },
        { name: 'Custom', type: 'string' },
        { name: 'EndTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'CameraManID' },
        { name: 'CameraTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'SendTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'PlanTime', type: 'date', dateFormat: 'Y-m-d' },
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
        { name: 'Resolution' },
        { name: 'Remark'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/order/checkphoto",
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
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var grid = new Ext.grid.GridPanel({
        title: '当前位置:' + node.text, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, store: store, sm: check_select,
        autoExpandColumn: 'Name',
        tbar: [
            { text: '校图', tooltip: '查看问题', iconCls: 'icon-application_view_gallery', ref: '../showBtn', disabled: true, hidden: levels.checkphoto, handler: showpic },
            { xtype: 'tbseparator', hidden: levels.checkphoto },
            { text: '查看问题', tooltip: '查看问题', iconCls: 'icon-layout', ref: '../showquBtn', disabled: true, handler: showquestion }, '-',
            { text: '园所校图完成', tooltip: "请在本件全部校图完成后完成此操作", iconCls: 'icon-edit', ref: '../finishBtn', disabled: true, hidden: levels.finish, handler: finish },
            { xtype: 'tbseparator', hidden: levels.finish },
            { text: '通知美工修图', tooltip: '童话世界确认园所校图完，美工可以开始修图', iconCls: 'icon-application_put', ref: '../startdealBtn', disabled: true, hidden: levels.startdeal, handler: startdeal },
            { xtype: 'tbseparator', hidden: levels.startdeal },
            { text: '重新校图', tooltip: '重新校图', iconCls: 'icon-nav', ref: '../recheckBtn', disabled: true, hidden: levels.recheck, handler: recheck },
            { xtype: 'tbseparator', hidden: levels.recheck },
            { text: '不校图', tooltip: '不校图', iconCls: 'icon-nav', ref: '../notcheckBtn', disabled: true, hidden: levels.recheck, handler: notcheck },
            { xtype: 'tbseparator', hidden: levels.recheck },
            { text: '通过校图可送洗', tooltip: "请在确认本件全部校图完成，并且没有问题的情况下完成此操作", iconCls: 'icon-accept', ref: '../solveBtn', disabled: true, hidden: levels.solve, handler: solve },
            { xtype: 'tbseparator', hidden: levels.solve },
            { xtype: 'displayfield', value: '&nbsp;校图状态:&nbsp;', hidden: typeInfo.Type != 1 },
            { xtype: 'combo', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue', width: 120,
                name: 'ProofState', hiddenName: 'ProofState', triggerAction: 'all', emptyText: '----请选择----', hidden: typeInfo.Type != 1,
                store: new Ext.data.ArrayStore({
                    fields: ['displayValue', 'displayText'],
                    data: [['', '---请选择---'], [1, '校图中'], [2, '园所校图完成'], [6, '美工修图'], [3, '已处理']]
                }),
                listeners: {
                    'select': function(combo, record) {
                        query.proofState = record.get('displayValue');
                        store.load({ params: { start: 0} });
                    }
                }
            }, { xtype: 'tbseparator', hidden: typeInfo.Type != 1 },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
        ],
        listeners: { 'celldblclick': { fn: showpic, scope: this} },
        columns: [
                new Ext.grid.RowNumberer(),
                check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '编号', tooltip: "编号", width: 80, dataIndex: 'Code', sortable: true, hidden: true },
                { header: '工作单名称', tooltip: "工作单名称", width: 200, dataIndex: 'WorkName', sortable: true },
                { header: '名称', tooltip: "名称", width: 125, id: 'Name', dataIndex: 'Name', sortable: true },
                { header: '校图状态', tooltip: "校图状态", width: 85, dataIndex: 'ProofStateText', sortable: true },
                { header: '套系类型', tooltip: "套系类型", width: 70, dataIndex: 'KitType', sortable: true },
                { header: '上传时间', tooltip: "上传时间", width: 80, dataIndex: 'SendTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d') },
                { header: '内页尺寸', tooltip: "内页尺寸", width: 70, dataIndex: 'InsideName', sortable: true },
                { header: '封面尺寸', tooltip: "封面尺寸", width: 70, dataIndex: 'CoverName', sortable: true },
                { header: '同学录套图', tooltip: "同学录套图", width: 80, dataIndex: 'ClassType', sortable: true },
                { header: '封面材料', tooltip: "封面材料", width: 80, dataIndex: 'InsideMaterial', sortable: true, hidden: true },
                { header: '开始校图时间', tooltip: "开始校图时间", width: 130, dataIndex: 'ProofBeginTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '结束校图时间', tooltip: "结束校图时间", width: 130, dataIndex: 'ProofEndTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s')}],
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        })
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.showquBtn.setDisabled(sm.getCount() < 1);
        grid.recheckBtn.setDisabled(sm.getCount() < 1);
        grid.notcheckBtn.setDisabled(sm.getCount() < 1);
        var s = sm.getSelected();
        grid.showBtn.setDisabled(sm.getCount() < 1);
        if (s && (s.data.ProofState == 1 || s.data.ProofState == 3)) {
            grid.finishBtn.setDisabled(sm.getCount() < 1);
            grid.solveBtn.setDisabled(sm.getCount() < 1);
            if (s.data.ProofState == 3)
                grid.startdealBtn.setDisabled(sm.getCount() < 1);
        }
        else if (s && s.data.ProofState == 2)
            grid.startdealBtn.setDisabled(sm.getCount() < 1);
        else {
            grid.finishBtn.setDisabled(true);
            grid.solveBtn.setDisabled(true);
            grid.startdealBtn.setDisabled(true);
        }
    });
    function showpic() {
        var s = grid.getSelectionModel().getSelected();
        var data = s.data;
        Ext.Ajax.request({
            url: '/showpic/firstcheck/',
            method: "POST",
            params: { id: s.data.ID },
            waitMsg: '提交数据中...',
            success: function(response, options) {
                jsload('/js/exts/showpic/showpic.js', 'showpic',
                        { 'id': 'showpic_' + data.ID, 'text': data.Name + '(' + data.Code + '校图)', pnode: node,
                            'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'id': data.ID, 'Code': node.attributes.Code, 'KitID': data.KitID }
                        });
            },
            failure: function(response, options) {
                Ext.Msg.alert("系统提示!", '失败');
            }
        });
    }
    function showquestion() {
        var s = grid.getSelectionModel().getSelected();
        var data = s.data;
        jsload('/js/exts/showpic/showquestion.js', 'showquestion',
                        { 'id': 'showquestion_' + data.ID, 'text': data.Name + '(' + data.Code + '问题列表)',
                            'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'id': data.ID, 'Code': node.attributes.Code, 'KitID': data.KitID, 'ProofState': data.ProofState }
                        });
    }
    function recheck() {
        var s = grid.getSelectionModel().getSelected();
        Ext.MessageBox.show({ title: '提示框', msg: '你确定重新校图吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/recheck/',
                        method: "POST",
                        params: { id: s.data.ID },
                        waitMsg: '提交数据中...',
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
    function notcheck() {
        var s = grid.getSelectionModel().getSelected();
        Ext.MessageBox.show({ title: '提示框', msg: '你确定不校图吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/notcheck/',
                        method: "POST",
                        params: { id: s.data.ID },
                        waitMsg: '提交数据中...',
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
    var form = new Ext.form.FormPanel({
        frame: true, border: false, plain: true, layout: "form", defaultType: "textfield", labelAlign: 'right', labelWidth: 80,
        method: 'POST', waitMsgTarget: true,
        reader: new Ext.data.JsonReader({ id: 'ID' },
                    new Ext.data.Record.create([
                        { name: 'ID', type: 'int' },
                        { name: 'KitWorkID', type: 'int' },
                        { name: 'KitClassID', type: 'int' },
                        { name: 'ConfirmTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
                        { name: 'ConfirmMan', type: 'string' },
                        { name: 'Tel', type: 'string' },
                        { name: 'Remark' }
                ])
        ),
        items: [{ xtype: 'hidden', name: 'ID', hidden: false },
            { xtype: 'textfield', name: 'ConfirmMan', fieldLabel: '确认人姓名', width: 100, allowBlank: false },
            { xtype: 'textfield', name: 'Tel', fieldLabel: '联系电话', width: 180, allowBlank: false },
            { xtype: 'datetimefield', name: 'ConfirmTime', fieldLabel: '确认时间', width: 160, format: 'Y-m-d H:i:s', allowBlank: false },
            { xtype: 'textarea', name: 'Remark', fieldLabel: '备注', height: 50, anchor: '100%'}]
    });
    var win = new Ext.Window({
        title: '通过校图', closeAction: 'hide', width: 500, height: 220, layout: 'fit', plain: true, border: false,
        /*modal: 'true',*/buttonAlign: 'center', loadMask: true, animateTarget: document.body, items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: '/showpic/kitsolve/',
                        params: { kitworkid: kitworkid },
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
    function solve() {
        var s = grid.getSelectionModel().getSelected();
        kitworkid = s.data.ID;
        win.show();
        setformitem(form, win, 0);
    }
    function finish() {
        var s = grid.getSelectionModel().getSelected();
        Ext.MessageBox.show({ title: '提示框', msg: '你确定结束校图吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/kitfinish/',
                        method: "POST",
                        params: { id: s.data.ID },
                        waitMsg: '提交数据中...',
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
    function startdeal() {
        var s = grid.getSelectionModel().getSelected();
        Ext.MessageBox.show({ title: '提示框', msg: '童话世界确认园所校图完，美工可以开始修图吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/startdeal/',
                        method: "POST",
                        params: { id: s.data.ID },
                        waitMsg: '提交数据中...',
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
    GridMain(node, grid);
}