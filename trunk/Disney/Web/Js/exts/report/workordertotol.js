workordertotol = function(node) {
    var fields = Ext.data.Record.create([
        { name: 'Code', type: 'string' },
        { name: 'Strength' },
        { name: 'Robe' },
        { name: 'GroupPhoto' },
        { name: 'Life' },
        { name: 'Classmates' },
        { name: 'ClassmatesTeacher' },
        { name: 'ClassmatesPeopleNum' },
        { name: 'Cover' },
        { name: 'Head' },
        { name: 'PhotoNum' },
        { name: 'Amount'}]
    );
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/report/workordertotol/",
            method: "POST"
        }),
        reader: new Ext.data.JsonReader({
            fields: fields,
            id: "ID"
        }),
        listeners: { load: function(s, records, options) { GridSum(grid); } }
    });
    function GridSum(grid) {
        var n = grid.getStore().getCount(); // 获得总行数
        if (n > 0) {
            var totols = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            grid.store.each(function(record) {
                totols[0] += Number(record.data.Strength);
                totols[1] += Number(record.data.Robe);
                totols[2] += Number(record.data.GroupPhoto);
                totols[3] += Number(record.data.Life);
                totols[4] += Number(record.data.Classmates);
                totols[5] += Number(record.data.ClassmatesTeacher);
                totols[6] += Number(record.data.ClassmatesPeopleNum);
                totols[7] += Number(record.data.Cover);
                totols[8] += Number(record.data.Head);
                totols[9] += record.data.Amount;
                totols[10] += record.data.PhotoNum;
            });
            var p = new Ext.data.Record({
                Code: '<div style="text-align:right;color:red">统计：</div>',
                Strength: '<span style="color:red">' + totols[0] + "</span>",
                Robe: '<span style="color:red">' + totols[1] + "</span>",
                GroupPhoto: '<span style="color:red">' + totols[2] + "</span>",
                Life: '<span style="color:red">' + totols[3] + "</span>",
                Classmates: '<span style="color:red">' + totols[4] + "</span>",
                ClassmatesTeacher: '<span style="color:red">' + totols[5] + "</span>",
                ClassmatesPeopleNum: '<span style="color:red">' + totols[6] + "</span>",
                Cover: '<span style="color:red">' + totols[7] + "</span>",
                Head: '<span style="color:red">' + totols[8] + "</span>",
                Amount: '<span style="color:red">' + ForDight(totols[9], 2) + "</span>",
                PhotoNum: '<span style="color:red">' + ForDight(totols[10], 2) + "</span>"
            });
            grid.store.insert(n, p);
        }
    }
    store.load();
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(),
                { header: '工作单', tooltip: "工作单", width: 150, id: 'Code', dataIndex: 'Code' },
                { header: '礼服/學士', tooltip: "礼服", width: 100, dataIndex: 'Robe', align: 'right' },
                { header: '团照', tooltip: "团照", width: 100, dataIndex: 'GroupPhoto', align: 'right' },
                { header: '生活照/全家福/便服', tooltip: "生活照/全家福/便服", width: 130, dataIndex: 'Life', align: 'right' },
                { header: '同学录', tooltip: "同学录", width: 100, dataIndex: 'Classmates', align: 'right' },
                { header: '老師', tooltip: "老師", width: 90, dataIndex: 'ClassmatesTeacher', align: 'right' },
                { header: '小朋友人數', tooltip: "小朋友人數", width: 90, dataIndex: 'ClassmatesPeopleNum', align: 'right' },
                { header: '封面/年歷', tooltip: "封面/年歷", width: 100, dataIndex: 'Cover', align: 'right' },
                { header: '大头贴', tooltip: "大头贴", width: 100, dataIndex: 'Head', align: 'right' },
                { header: 'P數', tooltip: "P數", width: 100, dataIndex: 'PhotoNum', align: 'right' }
                ]);
    var grid = new Ext.grid.GridPanel({ title: '当前位置:' + node.text, store: store, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, autoExpandColumn: 'Code', cm: cm });
    GridMain(node, grid);
}