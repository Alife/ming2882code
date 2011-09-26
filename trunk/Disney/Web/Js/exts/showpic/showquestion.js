showquestion = function(node) {
    var levels = {};
    levels.unsolve = GetIsLevel(node.attributes.Code, 'unsolve');
    levels.deal = GetIsLevel(node.attributes.Code, 'deal');
    levels.solve = GetIsLevel(node.attributes.Code, 'solve');
    levels.del = GetIsLevel(node.attributes.Code, 'delete');
    levels.otheraq = GetIsLevel(node.attributes.Code, 'otheraq');
    levels.patch = GetIsLevel(node.attributes.Code, 'patch');
    var kitworkid = node.attributes.id;
    var kitid = node.attributes.KitID;
    var proofState = node.attributes.ProofState;
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'UserID', type: 'int' },
        { name: 'TrueName', type: 'string' },
        { name: 'KitID', type: 'int' },
        { name: 'KitClassID', type: 'int' },
        { name: 'ClassCode', type: 'string' },
        { name: 'ClassName', type: 'string' },
        { name: 'KitChildID', type: 'int' },
        { name: 'ChildCode', type: 'string' },
        { name: 'ChildName', type: 'string' },
        { name: 'FileName', type: 'string' },
        { name: 'Intro', type: 'string' },
        { name: 'State', type: 'int' },
        { name: 'QuestionTypeText', type: 'string' },
        { name: 'StateText', type: 'string' },
        { name: 'CreateTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'IntroTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'Remark', type: 'string' },
        { name: 'Tw', type: 'string' },
        { name: 'IsPatch'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        url: "/showpic/showquestion/" + kitworkid,
        reader: new Ext.data.JsonReader({ fields: fields })
    });
    store.load();
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var grid = new Ext.grid.EditorGridPanel({
        store: store, sm: check_select, clicksToEdit: 1, expandable: false, autoScroll: true, animate: true, border: false, loadMask: true, //autoExpandColumn: 'Intro',
        columns: [
                new Ext.grid.RowNumberer(), check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', hidden: true },
                { header: '班级', tooltip: "班级", width: 70, dataIndex: 'ClassName', sortable: true },
                { header: '班级编号', tooltip: "班级编号", width: 65, dataIndex: 'ClassCode', sortable: true, hidden: true },
                { header: '小朋友', tooltip: "小朋友", width: 65, dataIndex: 'ChildName', sortable: true },
                { header: '小朋友编号', tooltip: "班级编号", width: 65, dataIndex: 'ChildCode', sortable: true, hidden: true },
                { header: '文件名', tooltip: "文件名", width: 65, dataIndex: 'FileName', sortable: true },
                { header: '内容', tooltip: "内容", width: 180, id: 'Intro', dataIndex: 'Intro', sortable: false, menuDisabled: true,
                    renderer: function(value, cell) {
                        cell.attr = 'style="white-space:normal"';
                        return value;
                    }
                },
                { header: '童话世界处理', tooltip: "童话世界处理", width: 180, dataIndex: 'Tw', sortable: false, menuDisabled: true,
                    renderer: function(value, cell) {
                        cell.attr = 'style="white-space:normal"';
                        return value;
                    },
                    editor: new Ext.form.TextArea({ allowBlank: false, allowNegative: false, height: 80, anchor: '100%' })
                    //renderer: function() { return '<div class="icon-tag_blue tag_blue" style="padding-left:18px;height:16px;cursor:pointer;">处理</div>' }
                },
                { header: '问题类型', tooltip: "问题类型", width: 70, dataIndex: 'QuestionTypeText', sortable: true, menuDisabled: true },
                { header: '状态', tooltip: "状态", width: 50, dataIndex: 'StateText', sortable: true },
                { header: '是否生成返单', tooltip: "是否生成返单", width: 50, dataIndex: 'IsPatch', sortable: true, hidden: typeInfo.Type != 1, renderer: function(v) { return v ? '已生成' : '未生成'; } },
                { header: '提交时间', tooltip: "提交时间", width: 130, dataIndex: 'CreateTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '上次解决时间', tooltip: "上次解决时间", width: 130, dataIndex: 'IntroTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '已处理原因', tooltip: "已处理原因", width: 150, dataIndex: 'Remark', sortable: true,
                    renderer: function(value, cell) {
                        cell.attr = 'style="white-space:normal"';
                        return value;
                    }
                }
            ],
        tbar: [
            { text: '生成返工单', tooltip: '生成有价返工工作单', iconCls: 'icon-nav', ref: '../returnBtn', disabled: true, hidden: levels.patch, handler: returnBtn },
            { xtype: 'tbseparator', hidden: levels.patch },
            { text: '验证处理', tooltip: '美工验证还有没有漏修改的问题', iconCls: 'icon-application_put', hidden: levels.deal, handler: checkdealBtn },
            { xtype: 'tbseparator', hidden: levels.deal },
            { text: '删除档图', tooltip: '美工全部已处理后可以点此功能删除校稿中图片', iconCls: 'icon-delete', hidden: levels.deal, handler: delkitphoto },
            { xtype: 'tbseparator', hidden: levels.deal },
            { text: '已处理', tooltip: '已处理', iconCls: 'icon-edit', ref: '../dealBtn', disabled: true, hidden: levels.deal, handler: function() { deal(this.text, '/showpic/kitquestiondeal'); } },
            { xtype: 'tbseparator', hidden: levels.deal },
            { text: '解释处理', tooltip: '美工认为不用修改的可以解释并修改状态为已处理', iconCls: 'icon-add', ref: '../dealremarkBtn', disabled: true, hidden: levels.deal, handler: dealremark },
            { xtype: 'tbseparator', hidden: levels.deal },
            { text: '未解决', tooltip: '新问题或还没有解决的问题', iconCls: 'icon-cancel', ref: '../unsolveBtn', disabled: true, hidden: levels.unsolve, handler: function() { deal(this.text, '/showpic/kitquestionunsolve'); } },
            { xtype: 'tbseparator', hidden: levels.unsolve },
            { text: '已解决', tooltip: '点击已解决后,问题将删除', iconCls: 'icon-accept', ref: '../solveBtn', disabled: true, hidden: levels.solve, handler: function() { deal(this.text, '/showpic/kitquestionsolve'); } },
            { xtype: 'tbseparator', hidden: levels.solve },
            { text: '删除问题', tooltip: '删除', iconCls: 'icon-delete', ref: '../deleteBtn', disabled: true, hidden: levels.del, handler: function() { deal(this.text, '/showpic/kitquestiondel'); } },
            { xtype: 'tbseparator', hidden: levels.del },
            { text: '刷新', iconCls: 'x-tbar-loading', handler: function() { store.reload(); grid.getSelectionModel().clearSelections(); } }
        ],
        listeners: {
            "afteredit": function(e) {
                var r = e.record; //得到当前行所有数据
                var f = e.field; //得到修改列
                var v = e.value; //得到修改列修改后值
                if (v == '') { grid.getStore().reload(); return; }
                Ext.Ajax.request({
                    url: "/showpic/saveotheraqquestion",
                    method: "POST",
                    params: { Tw: v, ID: r.data.ID },
                    success: function(r) { grid.getStore().reload(); },
                    failure: function() { Ext.Msg.alert("系统提示!", '失败'); }
                });
            },
            'render': function() {
                grid.getColumnModel().setEditable(9, !levels.otheraq && proofState != 6);
            }
        }
    });
    function deal(text, url) {
        var s = grid.getSelectionModel().getSelections();
        var ids = new Array();
        var storeitems = new Array();
        for (var i = 0, r; r = s[i]; i++) {
            if (r.data.ID)
                ids.push(r.data.ID);
            storeitems.push(r);
        }
        Ext.MessageBox.show({ title: '提示框', msg: '你确定' + text + '吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: url,
                        method: "POST",
                        params: { id: ids, kitworkid: kitworkid },
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
    function returnBtn() {
        var s = grid.getSelectionModel().getSelections();
        var ids = new Array();
        var storeitems = new Array();
        for (var i = 0, r; r = s[i]; i++) {
            if (r.data.ID)
                ids.push(r.data.ID);
            storeitems.push(r);
        }
        Ext.MessageBox.show({ title: '提示框', msg: '你确定生成有价返工工作单吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/returnpatch',
                        method: "POST",
                        params: { id: ids, kitworkid: kitworkid },
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
    function checkdealBtn() {
        Ext.MessageBox.show({ title: '提示框', msg: '你确定都处理好了吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/checkdeal',
                        method: "POST",
                        params: { id: kitworkid, kitid: kitid },
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
    function delkitphoto() {
        Ext.MessageBox.show({ title: '提示框', msg: '你确定要删除档图吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/showpic/delkitphoto',
                        method: "POST",
                        params: { id: kitid },
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
    var form = new Ext.form.FormPanel({
        frame: true, border: false, plain: true, layout: "form", defaultType: "textfield", labelAlign: 'right', labelWidth: 80,
        items: [{ xtype: 'hidden', name: 'ID', hidden: false }, { xtype: 'textarea', name: 'Remark', fieldLabel: '已处理原因', height: 50, anchor: '100%'}]
    });
    var win = new Ext.Window({
        title: '解释处理', closeAction: 'hide', width: 400, height: 130, layout: 'fit', plain: true, border: false,
        /*modal: 'true',*/buttonAlign: 'center', loadMask: true, animateTarget: document.body, items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: '/showpic/dealremark/',
                        params: { id: grid.getSelectionModel().getSelected().data.ID },
                        success: function(frm, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            form.getForm().reset(); win.hide();
                            store.reload();
                        },
                        failure: function(frm, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                        }
                    });
                }
            }
        }, { text: '取消', handler: function() { form.getForm().reset(); win.hide(); } }]
    });
    function dealremark() {
        win.show();
        var s = grid.getSelectionModel().getSelected();
        form.getForm().loadRecord(s);
    }
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.unsolveBtn.setDisabled(sm.getCount() < 1);
        grid.dealBtn.setDisabled(sm.getCount() < 1);
        grid.solveBtn.setDisabled(sm.getCount() < 1);
        grid.dealremarkBtn.setDisabled(sm.getCount() < 1);
        grid.returnBtn.setDisabled(sm.getCount() < 1);
        grid.deleteBtn.setDisabled(sm.getCount() < 1);
    });
    GridMain(node, grid);
}