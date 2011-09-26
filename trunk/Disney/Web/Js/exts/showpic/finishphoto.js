finishphoto = function(node) {
    var levels = {};
    levels.recheck = GetIsLevel(node.attributes.Code, 'recheck');
    var frmid = '_' + node.id;
    var url = '';
    var query = { limit: 20, kitName: '' };
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'UserID', type: 'int' },
        { name: 'KitWorkID', type: 'int' },
        { name: 'ConfirmMan', type: 'string' },
        { name: 'Tel', type: 'string' },
        { name: 'ConfirmTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'KitID', type: 'int' },
        { name: 'Name', type: 'string' },
        { name: 'Code', type: 'string' },
        { name: 'WorkName', type: 'string' },
        { name: 'Custom', type: 'string' },
        { name: 'KitTypeID', type: 'int' },
        { name: 'KitType', type: 'string' },
        { name: 'ClassTypeID', type: 'int' },
        { name: 'ClassType', type: 'string' },
        { name: 'InsideMaterialID', type: 'int' },
        { name: 'InsideMaterial', type: 'string' },
        { name: 'Remark'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/showpic/finishphoto",
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
                Ext.apply(s.baseParams, { limit: query.limit, kitName: query.kitName });
            }
        }
    });
    store.load({ params: { start: 0} });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(),
                check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '编号', tooltip: "编号", width: 100, dataIndex: 'Code', sortable: true, hidden: true },
                { header: '园所名称', tooltip: "园所名称", width: 150, dataIndex: 'Custom', sortable: true },
                { header: '名称', tooltip: "名称", width: 150, id: 'WorkName', dataIndex: 'WorkName', sortable: true },
                { header: '确认人', tooltip: "确认人", width: 80, dataIndex: 'ConfirmMan', sortable: true },
                { header: '联系电话', tooltip: "联系电话", width: 80, dataIndex: 'Tel', sortable: true },
                { header: '套系类型', tooltip: "套系类型", width: 100, dataIndex: 'KitType', sortable: true },
                { header: '同学录套图', tooltip: "同学录套图", width: 80, dataIndex: 'ClassType', sortable: true },
                { header: '封面材料', tooltip: "封面材料", width: 90, dataIndex: 'InsideMaterial', sortable: true },
                { header: '确认时间', tooltip: "确认时间", width: 120, dataIndex: 'ConfirmTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '备注', tooltip: "备注", width: 100, dataIndex: 'Remark', sortable: true}]);
    var grid = new Ext.grid.GridPanel({
        title: '当前位置:' + node.text,
        expandable: false,
        autoScroll: true,
        animate: true,
        border: false,
        store: store,
        loadMask: true,
        sm: check_select,
        autoExpandColumn: 'WorkName',
        tbar: [
            { text: '校图', tooltip: '重新校图', iconCls: 'icon-application_view_gallery', ref: '../recheckBtn', disabled: true, hidden: levels.recheck, handler: recheck },
            { xtype: 'tbseparator', hidden: levels.recheck },
             '档名称/编号:', { xtype: "textfield", id: 'kitName' + frmid, width: 120 }, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: {
                        "click": function() {
                            query.kitName = Ext.getCmp('kitName' + frmid).getValue();
                            query.className = Ext.getCmp('className' + frmid).getValue();
                            store.load({ params: { start: 0} });
                        }
                    }
                }
                ],
        cm: cm,
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        })
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.recheckBtn.setDisabled(sm.getCount() < 1);
    });
    function recheck() {
        var s = grid.getSelectionModel().getSelected();
        Ext.MessageBox.show({ title: '提示框', msg: '你确定重新校图吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/recheck/',
                        method: "POST",
                        params: { id: s.data.KitWorkID },
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