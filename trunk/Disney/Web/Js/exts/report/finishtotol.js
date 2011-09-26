finishtotol = function(node) {
    var frmid = '_' + node.id;
    var query = { arter: '', beginTime: '', endTime: '' };
    var fields = Ext.data.Record.create([
        { name: 'Custom', type: 'string' },
        { name: 'Strength' },
        { name: 'Robe' },
        { name: 'GroupPhoto' },
        { name: 'Life' },
        { name: 'Classmates' },
        { name: 'ClassmatesTeacher' },
        { name: 'ClassmatesPeopleNum' },
        { name: 'Cover' },
        { name: 'Head' },
        { name: 'Amt' },
        { name: 'Amount' },
        { name: 'PhotoNum'}]
    );
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/report/finishtotol/",
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
    var totolTpl = new Ext.Template([
        '<dl class="artertotol">',
        '<dt>禮服/學士/團照/生活照/全家福/便服共:</dt><dd>{p1000}×1={p1000}</dd>',
        '<dt>余禮服/學士/團照/生活照/全家福/便服:</dt><dd>{ps1000}×0.7={pst1000}</dd>',
        '<dt>同學錄=老師+學生數:</dt><dd>{class_p}×0.5={class_totol}</dd>',
        '<dt>封面/年历:</dt><dd>{cover_p}×0.5={cover_totol}</dd>',
        '<dt>大頭貼:</dt><dd>{head_p}×0.5={head_totol}</dd>',
        '<dt>共计:</dt><dd>{totol}</dd>',
        '</dl>'
    ]);
    function GridSum(grid) {
        var totols = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        var n = grid.getStore().getCount(); // 获得总行数
        if (n > 0) {
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
                totols[11] += record.data.Amt;
            });
            var p = new Ext.data.Record({
                Custom: '<div style="text-align:right;color:red">统计：</div>',
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
                PhotoNum: '<span style="color:red">' + totols[10] + "</span>",
                Amt: '<span style="color:red">' + ForDight(totols[11], 2) + "</span>"
            });
            grid.store.insert(n, p);
            var p1000_all = totols[1] + totols[2] + totols[3];
            var p1000 = p1000_all >= 1000 ? 1000 : p1000_all;
            var ps1000 = p1000_all > 1000 ? p1000_all - 1000 : 0;
            var pst1000 = ForDight(ps1000 * 0.7, 2);
            var class_p = totols[5] + totols[6]; var class_totol = ForDight(class_p * 0.5, 2);
            var cover_p = totols[7]; var cover_totol = ForDight(cover_p * 0.5, 2);
            var head_p = totols[8]; var head_totol = ForDight(head_p * 0.5, 2);
            var totol = ForDight(p1000 + pst1000 + class_totol + cover_totol + head_totol, 2);
            totolTpl.overwrite(totolPanel.body, { p1000: p1000, ps1000: ps1000, pst1000: pst1000, class_p: class_p, class_totol: class_totol,
                cover_p: cover_p, cover_totol: cover_totol, head_p: head_p, head_totol: head_totol, totol: totol
            });
        } else {
            totolTpl.overwrite(totolPanel.body, { p1000: 0, ps1000: 0, pst1000: 0, class_p: 0, class_totol: 0,
                cover_p: 0, cover_totol: 0, head_p: 0, head_totol: 0, totol: 0
            });
        }
    }
    var cm = new Ext.grid.ColumnModel([
                new Ext.grid.RowNumberer(),
                { header: '园所名称', tooltip: "园所名称", width: 150, id: 'Custom', dataIndex: 'Custom' },
                { header: '人数', tooltip: '人数', width: 50, dataIndex: 'Strength', align: 'right' },
                { header: '礼服/學士', tooltip: "礼服/學士", width: 80, dataIndex: 'Robe', align: 'right' },
                { header: '团照', tooltip: "团照", width: 50, dataIndex: 'GroupPhoto', align: 'right' },
                { header: '生活照/全家福/便服', tooltip: "生活照/全家福/便服", width: 130, dataIndex: 'Life', align: 'right' },
                { header: '同学录', tooltip: "同学录", width: 60, dataIndex: 'Classmates', align: 'right' },
                { header: '老師', tooltip: "老師", width: 50, dataIndex: 'ClassmatesTeacher', align: 'right' },
                { header: '小朋友人數', tooltip: "小朋友人數", width: 85, dataIndex: 'ClassmatesPeopleNum', align: 'right' },
                { header: '封面/年歷', tooltip: "封面/年歷", width: 80, dataIndex: 'Cover', align: 'right' },
                { header: '大头贴', tooltip: "大头贴", width: 60, dataIndex: 'Head', align: 'right' },
                { header: '圖片數', tooltip: "圖片數", width: 60, dataIndex: 'PhotoNum', align: 'right' },
                { header: '总金额', tooltip: "总金额", width: 60, dataIndex: 'Amt', align: 'right', hidden: userInfo.ID != 2 },
                { header: '美工金额', tooltip: "美工金额", width: 75, dataIndex: 'Amount', align: 'right', hidden: userInfo.ID == 2 }
                ]);
    var userCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'UserCode', 'TrueName']), id: "ID", root: "data", totalProperty: "records" }),
        url: String.format('/user/userlist?type={0}', 2)
    });
    var hidArterID = new Ext.form.Hidden({ id: 'arter' + frmid, name: 'ArterID' });
    var grid = new Ext.grid.GridPanel({
        store: store, cm: cm, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, autoExpandColumn: 'Custom', anchor: '100% 65%',
        tbar: [
                { xtype: 'displayfield', value: '&nbsp;美工:', hidden: userInfo.ID == 2 },
                 new Ext.form.ComboBox({ hidden: userInfo.ID == 2,
                     store: userCmbStore,
                     tpl: new Ext.XTemplate(
                        '<tpl for="."><div class="search-item">',
                            '{TrueName}({UserCode})',
                        '</div></tpl>'
                    ), name: 'Arter', hiddenName: 'Arter', valueField: 'ID', displayField: 'Arter', fieldLabel: '美工',
                     loadingText: '查询中...', minChars: 1, queryDelay: 1000, queryParam: 'keyword',
                     typeAhead: false, hideTrigger: true, width: 250, height: 200, pageSize: 10, itemSelector: 'div.search-item',
                     listeners: {
                         collapse: function() {
                             if (this.getStore().getCount() == 0)
                                 this.setValue('');
                         }
                     },
                     onSelect: function(record) {
                         if (record.data) {
                             hidArterID.setValue(record.data.UserCode);
                             this.setValue(record.data.TrueName + '(' + record.data.UserCode + ')');
                             this.collapse();
                         }
                     }
                 }), hidArterID,
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
                }, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;导&nbsp;&nbsp;出&nbsp;&nbsp; ", iconCls: 'icon-excel', pressed: true,
                    listeners: {
                        "click": function() {
                            document.location.href = String.format('/report/exporttotolorderdetail/{0}?arter={1}&beginTime={2}&endTime={3}'
                            , totolid, query.arter, query.beginTime, query.endTime);
                        }
                    }
                }
            ]
    });
    var totolPanel = new Ext.Panel({ title: '统计信息', anchor: '100% 35%', border: false, loadMask: true });
    var panel = new Ext.Panel({
        title: '当前位置:' + node.text, expandable: false, autoScroll: false, resizable: false, animate: true, border: false, loadMask: true, layout: 'anchor',
        items: [grid, totolPanel]
    });
    GridMain(node, panel);
    if (userInfo.ID == 2)
        grid.removeColumn('Amount');
}