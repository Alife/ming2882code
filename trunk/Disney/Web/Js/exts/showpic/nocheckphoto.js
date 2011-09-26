nocheckphoto = function(node) {
    var levels = {};
    levels.finish = GetIsLevel(node.attributes.Code, 'finish');
    var kitworkid = 0;
    var query = { limit: 20, keyword: '', custom: '', arter: '', state: '4,5', proofState: '5', beginTime: '', endTime: '' };
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
            { text: '通过校图可送洗', tooltip: "请在确认本件全部校图完成，并且没有问题的情况下完成此操作", iconCls: 'icon-accept', ref: '../finishBtn', disabled: true, hidden: levels.finish, handler: solve },
            { xtype: 'tbseparator', hidden: levels.finish },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
        ],
        columns: [
                new Ext.grid.RowNumberer(),
                check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '编号', tooltip: "编号", width: 80, dataIndex: 'Code', sortable: true, hidden: true },
                { header: '工作单名称', tooltip: "工作单名称", width: 200, dataIndex: 'WorkName', sortable: true },
                { header: '名称', tooltip: "名称", width: 125, id: 'Name', dataIndex: 'Name', sortable: true },
                { header: '校图状态', tooltip: "校图状态", width: 70, dataIndex: 'ProofStateText', sortable: true },
                { header: '套系类型', tooltip: "套系类型", width: 70, dataIndex: 'KitType', sortable: true },
                { header: '上传时间', tooltip: "上传时间", width: 80, dataIndex: 'SendTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d') },
                { header: '内页尺寸', tooltip: "内页尺寸", width: 70, dataIndex: 'InsideName', sortable: true },
                { header: '封面尺寸', tooltip: "封面尺寸", width: 70, dataIndex: 'CoverName', sortable: true },
                { header: '同学录套图', tooltip: "同学录套图", width: 80, dataIndex: 'ClassType', sortable: true },
                { header: '封面材料', tooltip: "封面材料", width: 80, dataIndex: 'InsideMaterial', sortable: true}],
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        })
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        var s = sm.getSelected();
        if (s && (s.data.ProofState == 5))
            grid.finishBtn.setDisabled(sm.getCount() < 1);
        else
            grid.finishBtn.setDisabled(true);
    });
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
    GridMain(node, grid);
}