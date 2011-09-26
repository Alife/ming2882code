artermonthtotol = function(node) {
    var frmid = '_' + node.id;
    var totolid = node.totolMonth.ID;
    var query = { arter: '', beginTime: '', endTime: '' };
    var fields = Ext.data.Record.create([
        { name: 'TrueName', type: 'string' },
        { name: 'Robe', type: 'int' },
        { name: 'GroupPhoto', type: 'int' },
        { name: 'Life', type: 'int' },
        { name: 'Classmates', type: 'int' },
        { name: 'ClassmatesTeacher' },
        { name: 'ClassmatesPeopleNum' },
        { name: 'Cover', type: 'int' },
        { name: 'Head', type: 'int' },
        { name: 'Amount'}]
    );
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/report/artermonthtotol/" + totolid,
            method: "POST"
        }),
        reader: new Ext.data.JsonReader({
            fields: fields,
            id: "ID"
        }),
        listeners: {
            beforeload: function(s, options) {
                Ext.apply(s.baseParams, { arter: query.arter, beginTime: query.beginTime, endTime: query.endTime });
            },
            load: function(s, records, options) { GridSum(grid); }
        }
    });
    store.load();
    function GridSum(grid) {
        var n = grid.getStore().getCount(); // 获得总行数
        if (n > 0) {
            var totols = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
            grid.store.each(function(record) {
                totols[0] += Number(record.data.Robe);
                totols[1] += Number(record.data.GroupPhoto);
                totols[2] += Number(record.data.Life);
                totols[3] += Number(record.data.Classmates);
                totols[4] += Number(record.data.ClassmatesTeacher);
                totols[5] += Number(record.data.ClassmatesPeopleNum);
                totols[6] += Number(record.data.Cover);
                totols[7] += Number(record.data.Head);
                totols[8] += record.data.Amount;
            });
            var p = new Ext.data.Record({
                TrueName: '<div style="text-align:right;color:red">统计：</div>',
                Robe: '<span style="color:red">' + totols[0] + "</span>",
                GroupPhoto: '<span style="color:red">' + totols[1] + "</span>",
                Life: '<span style="color:red">' + totols[2] + "</span>",
                Classmates: '<span style="color:red">' + totols[3] + "</span>",
                ClassmatesTeacher: '<span style="color:red">' + totols[4] + "</span>",
                ClassmatesPeopleNum: '<span style="color:red">' + totols[5] + "</span>",
                Cover: '<span style="color:red">' + totols[6] + "</span>",
                Head: '<span style="color:red">' + totols[7] + "</span>",
                Amount: '<span style="color:red">' + ForDight(totols[8], 2) + "</span>"
            });
            grid.store.insert(n, p);
        }
    }
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(),
                { header: '美工姓名', tooltip: "美工姓名", width: 150, id: 'TrueName', dataIndex: 'TrueName' },
                { header: '礼服', tooltip: "礼服", width: 100, dataIndex: 'Robe', align: 'right' },
                { header: '团照', tooltip: "团照", width: 100, dataIndex: 'GroupPhoto', align: 'right' },
                { header: '生活照', tooltip: "生活照", width: 100, dataIndex: 'Life', align: 'right' },
                { header: '同学录', tooltip: "同学录", width: 100, dataIndex: 'Classmates', align: 'right' },
                { header: '老師', tooltip: "老師", width: 90, dataIndex: 'ClassmatesTeacher', align: 'right' },
                { header: '同學錄人數', tooltip: "同學錄人數", width: 90, dataIndex: 'ClassmatesPeopleNum', align: 'right' },
                { header: '封面', tooltip: "封面", width: 100, dataIndex: 'Cover', align: 'right' },
                { header: '大头贴', tooltip: "大头贴", width: 100, dataIndex: 'Head', align: 'right' },
                { header: '金额', tooltip: "金额", width: 100, dataIndex: 'Amount', align: 'right' }
                ]);
    var grid = new Ext.grid.GridPanel({
        title: '当前位置:' + node.text, store: store, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, autoExpandColumn: 'TrueName',
        tbar: [
                '&nbsp;美工:', { xtype: "textfield", id: 'arter' + frmid, width: 120 },
                '&nbsp;月结时间: ',
                { xtype: 'datefield', id: 'beginTime' + frmid, format: 'Y-m-d', width: 120 }, '--',
                { xtype: 'datefield', id: 'endTime' + frmid, format: 'Y-m-d', width: 120 }, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: {
                        "click": function() {
                            query.arter = Ext.getCmp('arter' + frmid).getValue();
                            query.beginTime = Ext.getCmp('beginTime' + frmid).getValue();
                            query.endTime = Ext.getCmp('endTime' + frmid).getValue();
                            store.load();
                        }
                    }
                }
            ],
        cm: cm
    });
    GridMain(node, grid);
}