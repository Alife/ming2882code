workorderdetail = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    var ishidden = typeInfo.Type != 1 || userInfo.ID == 4;
    var url = '';
    var kitorder = node.kitorder;
    var kitworkid = kitorder.ID;
    var kitworkarterID = kitorder.ArterID;
    var kitPhotoTypeCmbStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create([{ name: 'ID' }, { name: 'Name' }, { name: 'ArtPrice' }, { name: 'Category'}]), root: "data" }),
        url: '/baseset/getartistprice/' + kitorder.ArterID
    });
    var peopleNumTxt = new Ext.form.NumberField({ name: 'PeopleNum', fieldLabel: "总人数", width: 80, allowBlank: false, value: kitorder.PeopleNum });
    var artistPriceTxt = new Ext.form.NumberField({ name: 'ArtistPrice', fieldLabel: "美工价格", width: 80, allowBlank: false, hidden: ishidden, hideLabel: ishidden });
    var numTxt = new Ext.form.NumberField({ name: 'Num', fieldLabel: "图片基数", width: 80,
        listeners: { 'blur': function() { photoNumTxt.setValue(this.getValue() * peopleNumTxt.getValue()); } }
    });
    var photoNumTxt = new Ext.form.NumberField({ name: 'PhotoNum', fieldLabel: "图片数量", width: 80, allowBlank: false });
    var kitPhotoTypeCmb = new Ext.form.ComboBox({
        mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
        fieldLabel: '档图类型', name: 'KitPhotoTypeID', hiddenName: 'KitPhotoTypeID', valueField: 'ID', displayField: 'Name', width: 140, allowBlank: false,
        store: kitPhotoTypeCmbStore,
        listeners: {
            'select': function(combo, record) {
                if (record.data) {
                    artistPriceTxt.setValue(record.data.ArtPrice);
                    numTxt.setValue(1);
                    peopleNumTxt.setValue(kitorder.PeopleNum);
                    photoNumTxt.setValue(kitorder.PeopleNum);
                }
            }
        }
    });
    var userCmbStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'UserCode', 'TrueName']), id: "ID", root: "data", totalProperty: "records" }),
        url: String.format('/user/userlist?type={0}', 2)
    });
    var userTpl = new Ext.XTemplate(
        '<tpl for="."><div class="search-item">',
            '{TrueName}({UserCode})',
        '</div></tpl>'
    );
    var hidArterID = new Ext.form.Hidden({ name: 'ArterID' });
    var form = new Ext.form.FormPanel({
        frame: true,
        border: false,
        plain: true,
        layout: "form",
        defaultType: "textfield",
        labelAlign: 'right', labelWidth: 100,
        items: [
                { xtype: 'hidden', name: 'ID', hidden: false }, kitPhotoTypeCmb, peopleNumTxt,
                { xtype: 'compositefield', fieldLabel: '图片基数', combineErrors: false,
                    items: [numTxt, { xtype: 'displayfield', value: '图片数量:' }, photoNumTxt, { xtype: 'displayfield', value: '老师数量:' },
                    { xtype: 'numberfield', name: 'TeacherNum', width: 80}]
                }, artistPriceTxt,
                new Ext.form.ComboBox({
                    store: userCmbStore, tpl: userTpl, name: 'Arter', hiddenName: '', valueField: 'ID', displayField: 'Arter',
                    fieldLabel: '美工', loadingText: '查询中...', minChars: 1, queryDelay: 1000, queryParam: 'keyword',
                    typeAhead: false, hideTrigger: true, anchor: '98%', height: 200, pageSize: 10, itemSelector: 'div.search-item',
                    listeners: {
                        collapse: function() {
                            if (this.getStore().getCount() == 0)
                                this.setValue('');
                        }
                    },
                    onSelect: function(record) {
                        if (record.data) {
                            hidArterID.setValue(record.data.ID);
                            this.setValue(record.data.TrueName + '(' + record.data.UserCode + ')');
                            this.collapse();
                        }
                    }
                }), hidArterID,
                { xtype: 'textarea', name: 'Remark', fieldLabel: '备注', height: 70, anchor: '98%' }
            ]
    });
    var win = new Ext.Window({
        title: '增加' + node.text, closeAction: 'hide',
        width: 500, height: 280, layout: 'fit', plain: true, border: false, /*modal: 'true',*/buttonAlign: 'center', loadMask: true, animateTarget: document.body,
        items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: url,
                        params: { kitworkarterID: kitworkarterID },
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
    var dataType = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'ArterID' },
        { name: 'KitWorkID', type: 'int' },
        { name: 'KitPhotoTypeID', type: 'int' },
        { name: 'KitPhotoType', type: 'string' },
        { name: 'PeopleNum' },
        { name: 'PhotoNum' },
        { name: 'TeacherNum' },
        { name: 'ArtistPrice' },
        { name: 'Arter' },
        { name: 'Amount', type: 'decimal' },
        { name: 'Remark', type: 'string'}]
    );
    var store = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: dataType }),
        url: '/order/kitphoto/' + kitworkid
    });
    store.load({ callback: function(r, options, success) { if (!success) Ext.Msg.alert('系统提示', '没有权限'); } });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        check_select,
        { header: 'ID', tooltip: "ID", dataIndex: 'ID', width: 50, hidden: true, menuDisabled: true },
        { header: '档图类型', tooltip: "档图类型", dataIndex: 'KitPhotoType', id: 'KitPhotoType', width: 100, menuDisabled: true },
        { header: '美工', tooltip: "美工", dataIndex: 'Arter', width: 90, menuDisabled: true },
        { header: '人数', tooltip: "人数", dataIndex: 'PeopleNum', width: 80, menuDisabled: true },
        { header: '图片数量', tooltip: "图片数量", dataIndex: 'PhotoNum', width: 80, menuDisabled: true },
        { header: '老师数量', tooltip: "老师数量", dataIndex: 'TeacherNum', width: 80, menuDisabled: true },
        { header: '单价/元', tooltip: "单价/元", dataIndex: 'ArtistPrice', width: 90, align: 'right', hidden: ishidden, menuDisabled: true },
        { header: '总价格/元', tooltip: "总价格/元", dataIndex: 'Amount', width: 100, align: 'right', hidden: ishidden, menuDisabled: true },
        { header: '备注', tooltip: "备注", width: 200, dataIndex: 'Remark', menuDisabled: true,
            renderer: function(value, cell) {
                cell.attr = 'style="white-space:normal"';
                return value;
            } }]);
    var grid = new Ext.grid.GridPanel({
        store: store, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, sm: check_select, cm: cm, autoExpandColumn: 'KitPhotoType',
        title: '当前位置:' + node.text,
        tbar: [
            { text: '增加', iconCls: 'icon-add', hidden: levels.add,
                handler: function() {
                    url = '/order/kitphotoadd?kitworkid=' + kitworkid;
                    form.getForm().reset();
                    win.setTitle('增加' + node.text);
                    win.show(); //form.getForm().findField('Type').setValue('1');
                }
            },
            { xtype: 'tbseparator', hidden: levels.add },
            { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit, handler: edit }, { xtype: 'tbseparator', hidden: levels.edit },
            {
                text: '删除', iconCls: 'icon-delete', ref: '../removeBtn', disabled: true, hidden: levels.del,
                handler: function() {
                    var s = grid.getSelectionModel().getSelections();
                    var ids = new Array();
                    var storeitems = new Array();
                    for (var i = 0, r; r = s[i]; i++) {
                        if (r.data.ID)
                            ids.push(r.data.ID);
                        storeitems.push(r);
                    }
                    Ext.MessageBox.show({ title: '提示框', msg: '你确定要删除吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                        fn: function(btn) {
                            if (btn == 'ok') {
                                Ext.Ajax.request({
                                    url: '/order/kitphotodelete',
                                    params: { id: ids },
                                    success: function(response, options) {
                                        var temp = Ext.util.JSON.decode(response.responseText);
                                        //Ext.Msg.alert("系统提示!", temp.msg);
                                        if (temp.success) {
                                            for (var i = 0, r; r = storeitems[i]; i++) store.remove(r);
                                        }
                                    }
                                });
                            }
                        }
                    });
                }
            }, { xtype: 'tbseparator', hidden: levels.del },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
        ],
        listeners: { 'celldblclick': edit }
    });
    function edit() {
        var s = grid.getSelectionModel().getSelected();
        if (!levels.edit && s.data.ID) {
            win.setTitle('编辑' + node.text);
            win.show();
            url = '/order/kitphotoedit';
            form.getForm().loadRecord(s);
        }
    }
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
}; 