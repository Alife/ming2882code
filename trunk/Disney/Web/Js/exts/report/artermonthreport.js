artermonthreport = function(node) {
    var frmid = '_' + node.id;
    var url = '';
    var query = { limit: 20, state: '2,3', beginTime: '', endTime: '' };
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'BeginTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'EndTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'State', type: 'int' },
        { name: 'StateText', type: 'string' },
        { name: 'BalanceTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'ArterBalanceTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'Amount'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/report/artermonthreport",
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
            }
        }
    });
    store.load({ params: { start: 0} });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(), check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '结算开始时间', tooltip: "结算开始时间", width: 130, dataIndex: 'BeginTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '结算结束时间', tooltip: "结算结束时间", width: 130, dataIndex: 'EndTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '结算状态', tooltip: "结算状态", width: 70, dataIndex: 'StateText', sortable: true },
                { header: '美工金额', tooltip: "美工金额", width: 90, dataIndex: 'Amount', sortable: true, align: 'right' },
                { header: '月结日期', tooltip: "月结日期", width: 130, dataIndex: 'BalanceTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '美工月结日期', tooltip: "美工月结日期", width: 130, dataIndex: 'ArterBalanceTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') }
                ]);
    var grid = new Ext.grid.GridPanel({
        store: store, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, sm: check_select, cm: cm, //autoExpandColumn: 'WorkName',
        tbar: [
            { text: '查看统计明显', iconCls: 'icon-nav', ref: '../totolBtn', disabled: true,
                handler: function() {
                    var s = grid.getSelectionModel().getSelected();
                    var data = s.data;
                    jsload('/js/exts/report/artermonthreportdetail.js', 'artermonthreportdetail',
                        { 'id': 'artermonthreportdetail_' + data.ID, 'text': Ext.util.Format.date(data.BeginTime, 'Y-m-d H:i:s') + '-' + Ext.util.Format.date(data.EndTime, 'Y-m-d H:i:s') + '月结明显'
                            , 'totolMonth': data, userCode: userInfo.UserCode, 'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'Code': node.attributes.Code }
                        });
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
        grid.totolBtn.setDisabled(sm.getCount() < 1);
    });
    var searchPanel = new Ext.Panel({
        title: '当前位置:' + node.text, layout: 'fit', border: false,
        tbar: [
                '&nbsp;开始时间: ',
                { xtype: 'datefield', id: 'beginTime' + frmid, format: 'Y-m-d', width: 120 }, '&nbsp;结束时间:',
                { xtype: 'datefield', id: 'endTime' + frmid, format: 'Y-m-d', width: 120 }, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: {
                        "click": function() {
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