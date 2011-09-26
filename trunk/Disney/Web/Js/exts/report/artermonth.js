artermonth = function(node) {
    var frmid = '_' + node.id;
    var totolid = node.totolMonth.ID;
    var query = { limit: 20, arter: '', beginTime: '', endTime: '' };
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'UserID', type: 'int' },
        { name: 'KitPhotoID', type: 'int' },
        { name: 'PeopleNum', type: 'int' },
        { name: 'PhotoNum', type: 'int' },
        { name: 'TeacherNum', type: 'int' },
        { name: 'KitPhotoType', type: 'string' },
        { name: 'Amount', type: 'decimal' },
        { name: 'WorkName', type: 'string' },
        { name: 'TrueName', type: 'string' },
        { name: 'FinishTime', type: 'date', dateFormat: 'Y-m-d' },
        { name: 'Remark', type: 'string'}]
    );
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/report/artermonth/" + totolid,
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
                Ext.apply(s.baseParams, { limit: query.limit, arter: query.arter, beginTime: query.beginTime, endTime: query.endTime });
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
                totols[0] += Number(record.data.PeopleNum);
                totols[1] += Number(record.data.PhotoNum);
                totols[2] += Number(record.data.TeacherNum);
                totols[3] += ForDight(record.data.Amount, 2);
            });
            var p = new Ext.data.Record({
                TrueName: '<div style="text-align:right;color:red">统计：</div>',
                PeopleNum: '<span style="color:red">' + totols[0] + "</span>",
                PhotoNum: '<span style="color:red">' + totols[1] + "</span>",
                TeacherNum: '<span style="color:red">' + totols[2] + "</span>",
                Amount: '<span style="color:red">' + ForDight(totols[3], 2) + "</span>"
            });
            grid.store.insert(n, p);
        }
    }
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(),
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '工作单名称', tooltip: "工作单名称", width: 100, dataIndex: 'WorkName', id: 'WorkName' },
                { header: '档图类型', tooltip: "档图类型", dataIndex: 'KitPhotoType', width: 100 },
                { header: '美工', tooltip: "美工", width: 100, dataIndex: 'TrueName' },
                { header: '总人数量', tooltip: "总人数量", dataIndex: 'PeopleNum', width: 100, align: 'right' },
                { header: '图片数量', tooltip: "图片数量", dataIndex: 'PhotoNum', width: 100, align: 'right' },
                { header: '老师数量', tooltip: "老师数量", dataIndex: 'TeacherNum', width: 100, align: 'right' },
                { header: '金额', tooltip: "金额", dataIndex: 'Amount', width: 90, align: 'right' },
                { header: '完成日期', tooltip: "完成日期", width: 130, dataIndex: 'FinishTime', renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '备注', tooltip: "备注", width: 150, dataIndex: 'Remark' }
                ]);
    var grid = new Ext.grid.GridPanel({
        title: '当前位置:' + node.text,
        store: store, sm: check_select, cm: cm, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, autoExpandColumn: 'WorkName',
        tbar: [
                '&nbsp;美工:', { xtype: "textfield", id: 'arter' + frmid, width: 120 },
                '&nbsp;完成时间: ',
                { xtype: 'datefield', id: 'beginTime' + frmid, format: 'Y-m-d', width: 120 }, '--',
                { xtype: 'datefield', id: 'endTime' + frmid, format: 'Y-m-d', width: 120 }, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: {
                        "click": function() {
                            query.arter = Ext.getCmp('arter' + frmid).getValue();
                            query.beginTime = Ext.getCmp('beginTime' + frmid).getValue();
                            query.endTime = Ext.getCmp('endTime' + frmid).getValue();
                            store.load({ params: { start: 0} });
                        }
                    }
                }
            ],
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        })
    });
    GridMain(node, grid);
}