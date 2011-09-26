totolorder = function(node) {
    var levels = {};
    levels.month = GetIsLevel(node.attributes.Code, 'month');
    levels.artermonth = GetIsLevel(node.attributes.Code, 'artermonth');
    var frmid = '_' + node.id;
    var query = { limit: 20, state: '', beginTime: '', endTime: '' };
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'OrderName', type: 'string' },
        { name: 'BeginTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'EndTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'State', type: 'int' },
        { name: 'StateText', type: 'string' },
        { name: 'BalanceTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'ArterBalanceTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'PhotoNum' },
        { name: 'Amt' },
        { name: 'BalanceAccount' },
        { name: 'Amount'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/report/totolorder",
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
                Ext.apply(s.baseParams, { limit: query.limit, state: query.state, beginTime: query.beginTime, endTime: query.endTime });
            },
            load: function(s, records, options) { GridSum(grid); }
        }
    });
    store.load({ params: { start: 0} });
    function GridSum(grid) {
        var n = grid.getStore().getCount(); // 获得总行数
        if (n > 0) {
            var totols = new Array(0, 0, 0, 0);
            grid.store.each(function(record) {
                totols[0] += Number(record.data.PhotoNum);
                totols[1] += record.data.Amount;
                totols[2] += record.data.Amt;
                totols[3] += record.data.BalanceAccount;
            });
            var p = new Ext.data.Record({
                StateText: '<div style="text-align:right;color:red">统计：</div>',
                PhotoNum: '<span style="color:red">' + totols[0] + "</span>",
                Amount: '<span style="color:red">' + ForDight(totols[1], 2) + "</span>",
                Amt: '<span style="color:red">' + ForDight(totols[2], 2) + "</span>",
                BalanceAccount: '<span style="color:red">' + ForDight(totols[3], 2) + "</span>"
            });
            grid.store.insert(n, p);
        }
    }
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(), check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '名称', tooltip: "名称", width: 70, dataIndex: 'OrderName', sortable: true },
                { header: '结算开始时间', tooltip: "结算开始时间", width: 130, dataIndex: 'BeginTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '结算结束时间', tooltip: "结算结束时间", width: 130, dataIndex: 'EndTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '结算状态', tooltip: "结算状态", width: 70, dataIndex: 'StateText', sortable: true },
                { header: '图片总数', tooltip: "图片总数", width: 80, dataIndex: 'PhotoNum', sortable: true, align: 'right' },
                { header: '总金额', tooltip: "总金额", width: 80, dataIndex: 'Amt', sortable: true, hidden: userInfo.ID != 2, align: 'right' },
                { header: '月结金额', tooltip: "月结金额", width: 80, dataIndex: 'BalanceAccount', sortable: true, hidden: userInfo.ID != 2, align: 'right' },
                { header: '美工金额', tooltip: "美工金额", width: 90, dataIndex: 'Amount', sortable: true, hidden: userInfo.ID == 2, align: 'right' },
                { header: '月结日期', tooltip: "月结日期", width: 130, dataIndex: 'BalanceTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '美工月结日期', tooltip: "美工月结日期", width: 130, dataIndex: 'ArterBalanceTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') }
                ]);
    var grid = new Ext.grid.GridPanel({
        store: store, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, sm: check_select, cm: cm, //autoExpandColumn: 'WorkName',
        tbar: [
            { text: '月结', iconCls: 'icon-accept', ref: '../monthBtn', disabled: true, handler: function() { monthwin.show(); }, hidden: levels.month },
            { xtype: 'tbseparator', hidden: levels.month },
            { text: '美工月结', iconCls: 'icon-accept', ref: '../artermonthBtn', disabled: true, handler: artermonth, hidden: levels.artermonth },
            { xtype: 'tbseparator', hidden: levels.artermonth },
            { text: '查看明显', iconCls: 'icon-nav', ref: '../detailBtn', disabled: true,
                handler: function() {
                    var s = grid.getSelectionModel().getSelected();
                    var data = s.data;
                    if (data.ID) {
                        jsload('/js/exts/report/totolorderdetail.js', 'totolorderdetail',
                        { 'id': 'totolorderdetail_' + data.ID, 'text': Ext.util.Format.date(data.BeginTime, 'Y-m-d H:i:s') + '-' + Ext.util.Format.date(data.EndTime, 'Y-m-d H:i:s') + '月结明显'
                            , 'totolMonth': data, 'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'Code': node.attributes.Code }
                        });
                    }
                }
            },
            { xtype: 'tbseparator' },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
            ],
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        })
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.detailBtn.setDisabled(sm.getCount() < 1);
        var s = sm.getSelected();
        if (s && s.data.State == 1)
            grid.monthBtn.setDisabled(sm.getCount() < 1);
        else
            grid.monthBtn.setDisabled(true);
        if (s && s.data.State == 2)
            grid.artermonthBtn.setDisabled(sm.getCount() < 1);
        else
            grid.artermonthBtn.setDisabled(true);
    });
    var monthform = new Ext.form.FormPanel({
        frame: true, border: false, plain: true, layout: "form", defaultType: "textfield",
        labelAlign: 'right', labelWidth: 80,
        items: [
                { xtype: 'datetimefield', name: 'BalanceTime', fieldLabel: '结算时间', width: 150, allowBlank: false },
                { xtype: 'numberfield', name: 'BalanceAccount', fieldLabel: '结算金额', width: 150, allowBlank: false }
            ]
    });
    var monthwin = new Ext.Window({
        title: '确定月结', closeAction: 'hide', width: 400, height: 140, layout: 'fit', plain: true,
        border: false, /*modal: 'true',*/buttonAlign: 'center', loadMask: true, animateTarget: document.body, items: monthform,
        buttons: [{ text: '保存',
            handler: function() {
                Ext.MessageBox.show({ title: '提示框', msg: '你确定月结吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                    fn: function(btn) {
                        if (btn == 'ok') {
                            if (monthform.getForm().isValid()) {
                                var s = grid.getSelectionModel().getSelected();
                                monthform.getForm().submit({
                                    waitMsg: "数据保存中...",
                                    waitTitle: "请稍侯",
                                    url: '/report/op_month/',
                                    params: { id: s.data.ID },
                                    success: function(frm, response) {
                                        var temp = Ext.util.JSON.decode(response.response.responseText);
                                        Ext.Msg.alert("系统提示!", temp.msg);
                                        monthform.getForm().reset(); monthwin.hide();
                                        store.reload();
                                    },
                                    failure: function(frm, response) {
                                        var temp = Ext.util.JSON.decode(response.response.responseText);
                                        Ext.Msg.alert("系统提示!", temp.msg);
                                    }
                                });
                            }
                        }
                    }
                });
            }
        }, { text: '取消', handler: function() { monthform.getForm().reset(); monthwin.hide(); } }]
    });
    function artermonth() {
        var s = grid.getSelectionModel().getSelected();
        Ext.MessageBox.show({ title: '提示框', msg: '你确定月结吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/report/op_artermonth/',
                        params: { id: s.data.ID },
                        method: "POST",
                        success: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
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
    var stateStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/order/balance',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "displayValue": '', "displayText": "---请选择---" }));
            }
        }
    });
    var searchPanel = new Ext.Panel({
        title: '当前位置:' + node.text, layout: 'fit', border: false,
        tbar: [
                '&nbsp;结算状态:', { xtype: 'combo', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue', width: 120,
                    id: 'state' + frmid, name: 'state', hiddenName: 'state', triggerAction: 'all', emptyText: '----请选择----', store: stateStore
                },
                '&nbsp;开始时间: ',
                { xtype: 'datefield', id: 'beginTime' + frmid, format: 'Y-m-d', width: 120 }, '&nbsp;结束时间:',
                { xtype: 'datefield', id: 'endTime' + frmid, format: 'Y-m-d', width: 120 }, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: {
                        "click": function() {
                            query.state = Ext.getCmp('state' + frmid).getValue();
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
    if (userInfo.ID == 2)
        grid.removeColumn('Amount');
}