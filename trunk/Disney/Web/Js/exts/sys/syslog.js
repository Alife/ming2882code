syslog = function(node) {
    var frmid = '_' + node.id;
    var query = { limit: 20, categoryid: 0, opid: 0, usercode: '', objcode: '', startDate: '', endDate: '' };
    //指定列参数
    var fields = ['ID', 'UserID', 'TrueName', 'IP', 'LogTime', 'OpID', 'ObjCode', 'Content'];
    var store = new Ext.data.Store({
        reader: new Ext.data.JsonReader({
            fields: fields,
            root: "data",
            id: "ID",
            totalProperty: "records"
        }),
        url: '/sys/syslog/',
        listeners: {
            beforeload: function(s, options) {
                Ext.apply(s.baseParams, { limit: query.limit, categoryid: query.categoryid, opid: query.opid,
                    usercode: query.usercode, objcode: query.objcode, startDate: query.startDate, endDate: query.endDate
                });
            }
        }
    });
    //加载时参数
    store.load({ params: { start: 0} });
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(),
		        { header: "ID", dataIndex: "ID", tooltip: "ID", hidden: true, sortable: true, width: 50 },
		        { header: "用户姓名", tooltip: "用户姓名", dataIndex: "TrueName", sortable: true, width: 100 },
		        { header: "IP", tooltip: "IP", dataIndex: "IP", sortable: true, width: 100 },
		        { header: "日志日期", tooltip: "日志日期", dataIndex: "LogTime", sortable: true, width: 125, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
		        { header: "日志内容", tooltip: "日志内容", dataIndex: "Content", id: "Content", sortable: true, width: 200 }
            ]);
    var grid = new Ext.grid.GridPanel({
        store: store, cm: cm, expandable: false, animate: true, buttonAlign: "left", loadMask: true, autoScroll: true, border: false, autoExpandColumn: "Content",
        bbar: new Ext.PagingToolbar({ store: store, pageSize: query.limit, plugins: new Ext.ux.ProgressBarPager() }),
        tbar: [{ text: '刷新', iconCls: 'x-tbar-loading', handler: function() { grid.store.reload({ params: { start: (grid.getBottomToolbar().getPageData().activePage - 1) * query.limit} }); grid.getSelectionModel().clearSelections(); } }]
    });
    var categoryStore = new Ext.data.JsonStore({
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/sys/getlogcategory',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    categoryStore.load();
    var categoryCmb = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        emptyText: '---请选择---', fieldLabel: '日志分类', name: 'Name', hiddenName: 'Name', displayField: 'Name', valueField: 'ID',
        id: 'categoryid' + frmid, width: 147, store: categoryStore,
        listeners: {
            'select': function(combo, record) {
                var id = record.get('ID');
                logopStore.removeAll();
                logopCmb.reset();
                if (id > 0) {
                    logopStore.proxy = new Ext.data.HttpProxy({
                        url: String.format('/sys/getlogop/{0}', id)
                    });
                    logopStore.load();
                }
            }
        }
    });
    var logopStore = new Ext.data.JsonStore({
        idProperty: 'ID',
        fields: ["ID", "OpName"],
        url: '/sys/getlogop',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "OpName": "---请选择---" }));
            }
        }
    });
    var logopCmb = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        emptyText: '---请选择---', fieldLabel: '日志操作', name: 'OpName', hiddenName: 'OpName', displayField: 'OpName', valueField: 'ID',
        id: 'opid' + frmid, width: 150, store: logopStore
    });
    var searchPanel = new Ext.Panel({
        layout: 'fit',
        border: false,
        title: '当前位置:' + node.text,
        tbar: [
		            '&nbsp;日志分类:&nbsp; ', categoryCmb,
		            '&nbsp;日志操作:&nbsp; ', logopCmb,
		            '&nbsp;日志操作人:&nbsp;', { xtype: "textfield", id: "usercode" + frmid, width: 120}],
        items: [grid],
        listeners: { 'render': function() { searchtbar.render(searchPanel.tbar); } }
    });
    var searchtbar = new Ext.Toolbar({
        items: [
            '&nbsp;用户/订单编号:&nbsp; ', { xtype: "textfield", id: "objcode" + frmid, width: 120 },
            '&nbsp;日志日期:&nbsp; ', { xtype: 'datefield', id: 'startDate' + frmid, format: 'Y-m-d' }, '--', { xtype: 'datefield', id: 'endDate' + frmid, format: 'Y-m-d' }, '-',
            { text: '查 找　', pressed: true, iconCls: 'icon-search', handler: searchInfo }
          ]
    });
    function searchInfo() {
        query.categoryid = Ext.getCmp('categoryid' + frmid).getValue()
        query.opid = Ext.getCmp('opid' + frmid).getValue();
        query.usercode = Ext.getCmp('usercode' + frmid).getValue();
        query.objcode = Ext.getCmp('objcode' + frmid).getValue();
        query.startDate = Ext.getCmp('startDate' + frmid).getValue(); //开始时间
        query.endDate = Ext.getCmp('endDate' + frmid).getValue(); //结束时间
        grid.store.reload({ params: { start: 0} });
    }
    GridMain(node, searchPanel);
}
        