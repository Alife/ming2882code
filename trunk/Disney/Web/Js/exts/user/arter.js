arter = function(node) {
    var levels = {};
    levels.add = GetIsLevel(node.attributes.Code, 'add');
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    levels.del = GetIsLevel(node.attributes.Code, 'del');
    levels.logout = GetIsLevel(node.attributes.Code, 'logout');
    var frmid = '_' + node.id;
    var url = '';
    var query = { limit: 20, keyword: '', provinceID: 0, countryID: 0, deptID: '', type: 2 };
    var editurl = '/user/useredit';
    var addurl = '/user/useradd';
    //省份
    var shengStore = new Ext.data.JsonStore({
        autoDestroy: true,
        autoLoad: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/sys/areachildlist',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    var shengCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        emptyText: '---请选择---', fieldLabel: '所在省份', name: 'ddlProvinceID',
        id: 'ddlProvinceID' + frmid, hiddenName: 'ddlProvinceID', displayField: 'Name', valueField: 'ID', width: 120, store: shengStore,
        listeners: {
            'select': function(combo, record) {
                var id = record.get('ID');
                shiStore.removeAll();
                shiCombox.reset();
                if (id > 0) {
                    shiStore.proxy = new Ext.data.HttpProxy({
                        url: String.format('/sys/areachildlist/{0}', id)
                    });
                    shiStore.load();
                }
            }
        }
    });
    var frmshengCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, blankText: '必须选择!', emptyText: '---请选择---', fieldLabel: '所在省份', name: 'ProvinceID',
        hiddenName: 'ProvinceID', displayField: 'Name', valueField: 'ID', width: 120, store: shengStore,
        listeners: {
            'select': function(combo, record) {
                var id = record.get('ID');
                frmshiStore.removeAll();
                frmshiCombox.reset();
                if (id > 0) {
                    frmshiStore.proxy = new Ext.data.HttpProxy({
                        url: String.format('/sys/areachildlist/{0}', id)
                    });
                    frmshiStore.load();
                }
            }
        }
    });
    var jgshengCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, blankText: '必须选择!', emptyText: '---请选择---', fieldLabel: '所在省份', name: 'ProvinceID',
        hiddenName: 'ProvinceID', displayField: 'Name', valueField: 'ID', width: 120, store: shengStore,
        listeners: {
            'select': function(combo, record) {
                var id = record.get('ID');
                jgshiStore.removeAll();
                jgshiCombox.reset();
                if (id > 0) {
                    jgshiStore.proxy = new Ext.data.HttpProxy({
                        url: String.format('/sys/areachildlist/{0}', id)
                    });
                    jgshiStore.load();
                }
            }
        }
    });
    //市
    var shiStore = new Ext.data.JsonStore({
        autoDestroy: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/sys/areachildlist',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    var shiCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        emptyText: '---请选择---', fieldLabel: '所在城市', name: 'ddlCountryID',
        id: 'ddlCountryID' + frmid, hiddenName: 'ddlCountryID', displayField: 'Name', valueField: 'ID', width: 120, store: shiStore
    });
    var frmshiStore = new Ext.data.JsonStore({
        autoDestroy: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/sys/areachildlist',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    var frmshiCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, blankText: '必须选择!', emptyText: '---请选择---', fieldLabel: '所在城市', name: 'CountryID',
        hiddenName: 'CountryID', displayField: 'Name', valueField: 'ID', width: 120, store: frmshiStore
    });
    var jgshiStore = new Ext.data.JsonStore({
        autoDestroy: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/sys/areachildlist',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '0', "Name": "---请选择---" }));
            }
        }
    });
    var jgshiCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, blankText: '必须选择!', emptyText: '---请选择---', fieldLabel: '所在城市', name: 'BirthPlace',
        hiddenName: 'BirthPlace', displayField: 'Name', valueField: 'ID', width: 120, store: jgshiStore
    });

    //所属部门下拉框
    var deptStore = new Ext.data.JsonStore({
        autoLoad: true,
        autoDestroy: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/baseset/dept',
        listeners: {
            load: function() {
                this.insert(0, new Ext.data.Record({ "ID": '', "Name": "---请选择---" }));
            }
        }
    });
    deptStore.load();
    var deptStoreCombo = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        emptyText: '---请选择---', fieldLabel: '所属部门', name: 'ddlDepartmentID',
        id: 'ddlDepartmentID' + frmid, hiddenName: 'ddlDepartmentID', displayField: 'Name', valueField: 'ID', width: 120,
        store: deptStore
    });
    var frmdeptStore = new Ext.data.JsonStore({
        autoLoad: true,
        autoDestroy: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/baseset/dept'
    });
    frmdeptStore.load();
    var frmdeptStoreCombo = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, emptyText: '---请选择---', fieldLabel: '所属部门', name: 'DepartmentID', hiddenName: 'DepartmentID', displayField: 'Name', valueField: 'ID', width: 120,
        store: frmdeptStore
    });
    //所属部门下拉框结束
    var typeStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['ID', 'Name']), root: "data" }),
        url: '/baseset/usertype/' + query.type
    });
    typeStore.load();
    var typeCmb = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, emptyText: '---请选择---', fieldLabel: '美工类型', name: 'TypeID', hiddenName: 'TypeID', displayField: 'Name', valueField: 'ID', width: 120,
        store: typeStore
    });
    var efield = Ext.data.Record.create([{ name: 'displayValue' }, { name: 'displayText'}]);
    var educationStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efield, root: "data" }),
        url: '/baseset/education/'
    });
    var nationStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efield, root: "data" }),
        url: '/baseset/nation/'
    });
    var politicsStatusStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efield, root: "data" }),
        url: '/baseset/politicsStatus/'
    });
    var form = new Ext.form.FormPanel({
        autoTabs: true,
        layout: 'form',
        border: false,
        labelAlign: 'right',
        method: 'POST',
        waitMsgTarget: true,
        reader: new Ext.data.JsonReader({ id: 'ID' },
                    new Ext.data.Record.create([
                        { name: 'ID', type: 'int' },
                        { name: 'UserName', type: 'string' },
                        { name: 'UserCode', type: 'string' },
                        { name: 'TrueName', type: 'string' },
                        { name: 'UserCard', type: 'string' },
                        { name: 'Email', type: 'string' },
                        { name: 'Mobile', type: 'string' },
                        { name: 'Tel', type: 'string' },
                        { name: 'CountryID', type: 'int' },
                        { name: 'Sex', type: 'int' },
                        { name: 'Birthday', type: 'string' },
                        { name: 'TypeID', type: 'int' },
                        { name: 'DepartmentID' },
                        { name: 'DayNum', type: 'int' },
                        { name: 'Zip', mapping: 'userinfo.Zip', type: 'string' },
                        { name: 'Address', mapping: 'userinfo.Address', type: 'string' },
                        { name: 'IsMarry', mapping: 'userinfo.IsMarry' },
                        { name: 'BirthPlace', mapping: 'userinfo.BirthPlace' },
                        { name: 'PoliticsStatus', mapping: 'userinfo.PoliticsStatus' },
                        { name: 'College', mapping: 'userinfo.College', type: 'string' },
                        { name: 'Speciality', mapping: 'userinfo.Speciality', type: 'string' },
                        { name: 'Education', mapping: 'userinfo.Education' },
                        { name: 'JobTime', mapping: 'userinfo.JobTime', type: 'string' },
                        { name: 'OnDutyTime', mapping: 'userinfo.OnDutyTime', type: 'string' },
                        { name: 'DimissionTime', mapping: 'userinfo.DimissionTime', type: 'string' },
                        { name: 'Duty', mapping: 'userinfo.Duty', type: 'string' },
                        { name: 'Nation', mapping: 'userinfo.Nation' },
                        { name: 'IDCard', mapping: 'userinfo.IDCard', type: 'string' },
                        { name: 'Resume', mapping: 'userinfo.Resume', type: 'string' }
                ])
            ),
        items: {
            xtype: 'tabpanel', border: false, deferredRender: false, activeTab: 0, defaults: { autoHeight: true, bodyStyle: 'padding:10px' },
            items: [{
                title: '基本信息', layout: 'form', defaultType: 'textfield', labelWidth: 100,
                items: [
                    { xtype: 'hidden', name: 'ID', hidden: false },
                    { xtype: 'textfield', name: 'Mobile', fieldLabel: '联系电话', width: '113', allowBlank: false, vtype: 'mobilephone', invalidText: "联系电话已经存在",
                        validator: function() {
                            var code = form.getForm().getValues().Mobile;
                            var oldcode = '';
                            if (url == editurl)
                                oldcode = grid.getSelectionModel().getSelected().data.Mobile;
                            var conn = Ext.lib.Ajax.getConnectionObject().conn;
                            conn.open("POST", String.format('/user/existsmobile?code={0}&oldcode={1}', code, oldcode), false);
                            conn.send(null);
                            return Ext.util.JSON.decode(conn.responseText);
                        }
                    },
                    { xtype: 'textfield', name: 'TrueName', fieldLabel: '姓名', width: '113', allowBlank: false },
                    { xtype: 'combo', name: 'Sex', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '选择',
                        fieldLabel: '性别', displayField: 'displayText', valueField: 'displayValue', hiddenName: 'Sex', anchor: '40%', allowBlank: false, blankText: '必须选择!',
                        store: new Ext.data.ArrayStore({
                            id: 0,
                            fields: ['displayValue', 'displayText'],
                            data: [[1, '男'], [2, '女']]
                        })
                    },
                    frmdeptStoreCombo, typeCmb,
                    { xtype: 'textfield', name: 'Duty', fieldLabel: '职位', width: '113', allowBlank: false },
                    { xtype: 'textfield', name: 'DayNum', fieldLabel: '日完成量', width: '50', allowBlank: false },
                    { xtype: 'compositefield', fieldLabel: '所在地区', combineErrors: false, items: [frmshengCombox, frmshiCombox] },
                    { xtype: 'compositefield', fieldLabel: '籍贯', combineErrors: false, items: [jgshengCombox, jgshiCombox] },
                    { xtype: 'textfield', name: 'Password', fieldLabel: '密码', inputType: 'password', width: '113', ref: '../../pwdref'}]
            }, {
                title: '其他信息', layout: 'anchor',
                items: [{ anchor: "100%,75%", layout: 'column', border: false,
                    items: [{ layout: 'form', columnWidth: .55, border: false, labelWidth: 100, defaults: { width: 120 },
                        items: [
                                { xtype: "textfield", fieldLabel: "毕业学校", maxLength: 50, maxLengthText: "输入过长", name: 'College' },
                                { xtype: 'combo', fieldLabel: "学历", name: 'Education', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue',
                                    hiddenName: 'Education', triggerAction: 'all', emptyText: '----请选择----', store: educationStore
                                },
                                { xtype: "checkbox", fieldLabel: "是否结婚", name: "IsMarry", checked: true, inputValue: true },
                                { xtype: 'datefield', name: 'OnDutyTime', fieldLabel: '到职时间', format: 'Y-m-d' },
                                { xtype: 'combo', fieldLabel: "民族", name: 'Nation', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue',
                                    hiddenName: 'Nation', triggerAction: 'all', emptyText: '----请选择----', store: nationStore
                                }
                        ]
                    }, { layout: 'form', columnWidth: .45, border: false, labelWidth: 100, defaults: { width: 120 },
                        items: [
                                { xtype: "textfield", fieldLabel: "专业", maxLength: 20, maxLengthText: "输入过长", name: 'Speciality' },
                                { xtype: 'combo', fieldLabel: "政治面貌", name: 'PoliticsStatus', mode: 'local', editable: false, displayField: 'displayText', valueField: 'displayValue',
                                    hiddenName: 'PoliticsStatus', triggerAction: 'all', emptyText: '----请选择----', store: politicsStatusStore
                                },
                                { xtype: 'datefield', name: 'JobTime', fieldLabel: '参加工作时间', format: 'Y-m-d' },
                                { xtype: 'datefield', name: 'DimissionTime', fieldLabel: '离职时间', format: 'Y-m-d' },
                                { xtype: 'datefield', name: 'Birthday', fieldLabel: '生日', format: 'Y-m-d' }
                        ]
                    }
                    ]
                }, { anchor: "100%,25%", layout: 'form', labelWidth: 100, border: false,
                    items: [
                        { xtype: 'textfield', name: 'IDCard', fieldLabel: '身份证', anchor: '70%' },
                        { xtype: 'textfield', name: 'Tel', fieldLabel: '固定电话', anchor: '80%', maxLengthText: "输入过长" },
                        { xtype: 'textfield', name: 'Email', fieldLabel: 'Email', width: '200', maxLength: 45, maxLengthText: "输入过长", invalidText: "Email已经存在",
                            validator: function() {
                                var code = form.getForm().getValues().Email;
                                var oldcode = '';
                                if (url == editurl)
                                    oldcode = grid.getSelectionModel().getSelected().data.Email;
                                var conn = Ext.lib.Ajax.getConnectionObject().conn;
                                conn.open("POST", String.format('/user/existsemail?code={0}&oldcode={1}', code, oldcode), false);
                                conn.send(null);
                                return Ext.util.JSON.decode(conn.responseText);
                            }
                        },
                        { xtype: 'compositefield', fieldLabel: '联系地址', combineErrors: false, items: [
                            { xtype: 'textfield', name: 'Address', fieldLabel: '联系地址', width: '300', maxLength: 100, maxLengthText: "输入过长" },
                            { xtype: 'textfield', name: 'Zip', fieldLabel: '邮编', maxLength: 6, maxLengthText: "输入过长", width: '60'}]
                        }
                        ]
                }
                ]
            }
            ]
        }
    });
    var win = new Ext.Window({
        closeAction: 'hide',
        width: 550,
        height: 360,
        layout: 'fit',
        plain: true,
        border: false,
        /*modal: 'true',*/
        buttonAlign: 'center',
        loadMask: true,
        animateTarget: document.body,
        items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: url,
                        success: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            form.reset(); win.hide();
                            store.reload({ params: { start: (grid.getBottomToolbar().getPageData().activePage - 1) * query.limit} });
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
    function loadedit() {
        var s = grid.getSelectionModel().getSelected();
        win.setTitle('编辑' + node.text);
        win.show();
        var pwdtxt = form.pwdref;
        pwdtxt.el.dom.parentNode.parentNode.style.display = '';
        url = editurl;
        form.getForm().load({
            url: '/user/getuser/' + s.data.ID,
            waitMsg: '正在载入数据...',
            success: function(frm, action) {
                var countryID = action.result.data.CountryID;
                var conn = Ext.lib.Ajax.getConnectionObject().conn;
                conn.open("POST", String.format('/sys/areaparentlist?id={0}&type={1}', countryID, 3), false);
                conn.send(null);
                var respText = Ext.util.JSON.decode(conn.responseText);
                var provinceID = respText[0].ID;
                frmshiStore.removeAll();
                frmshengCombox.setValue(provinceID);
                frmshiStore.proxy = new Ext.data.HttpProxy({ url: String.format('/sys/areachildlist/{0}', provinceID) });
                frmshiStore.load({
                    callback: function(r, options, success) {
                        if (success) frmshiCombox.setValue(countryID);
                    }
                });
                var jgcountryID = action.result.data.BirthPlace;
                var jgconn = Ext.lib.Ajax.getConnectionObject().conn;
                jgconn.open("POST", String.format('/sys/areaparentlist?id={0}&type={1}', jgcountryID, 3), false);
                jgconn.send(null);
                var jgrespText = Ext.util.JSON.decode(jgconn.responseText);
                var jgprovinceID = jgrespText[0].ID;
                jgshiStore.removeAll();
                jgshengCombox.setValue(jgprovinceID);
                jgshiStore.proxy = new Ext.data.HttpProxy({ url: String.format('/sys/areachildlist/{0}', jgprovinceID) });
                jgshiStore.load({
                    callback: function(r, options, success) {
                        if (success) jgshiCombox.setValue(jgcountryID);
                    }
                });
            },
            failure: function(frm, action) {
                win.hide();
                Ext.Msg.alert('提示', '原因如下：' + action.result.errors.info);
            }
        });
    };
    var fields = Ext.data.Record.create([
        { name: 'ID', type: 'int' },
        { name: 'UserName', type: 'string' },
        { name: 'UserCode', type: 'string' },
        { name: 'TrueName', type: 'string' },
        { name: 'UserCard', type: 'string' },
        { name: 'Email', type: 'string' },
        { name: 'Mobile', type: 'string' },
        { name: 'Tel', type: 'string' },
        { name: 'CountryID', type: 'int' },
        { name: 'Sex', type: 'int' },
        { name: 'Birthday', type: 'string' },
        { name: 'IsClose' },
        { name: 'Type', type: 'int' },
        { name: 'DepartmentID', type: 'int' },
        { name: 'Credit', type: 'decimal' },
        { name: 'Freeze', type: 'decimal' },
        { name: 'Avatar', type: 'string' },
        { name: 'RegTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'LoginTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'LoginIP' },
        { name: 'LoginNum' },
        { name: 'DayNum' },
        { name: 'TypeName' },
        { name: 'DeptName'}]
    );
    //读取数据
    var store = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({
            url: "/user/arterlist",
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
                Ext.apply(s.baseParams, { limit: query.limit, keyword: query.keyword,
                    provinceID: query.provinceID, countryID: query.countryID, deptID: query.deptID, type: query.type
                });
            }
        }
    });
    store.load({ params: { start: 0} });
    var check_select = new Ext.grid.CheckboxSelectionModel();
    var grid = new Ext.grid.GridPanel({
        expandable: false,
        autoScroll: true,
        animate: true,
        border: false,
        store: store,
        loadMask: true,
        sm: check_select,
        autoExpandColumn: 'TrueName',
        tbar: [
                    { text: '增加', iconCls: 'icon-add', hidden: levels.add,
                        handler: function() {
                            url = addurl;
                            form.getForm().reset();
                            win.setTitle('增加' + node.text);
                            win.show();
                            var pwdtxt = form.pwdref;
                            pwdtxt.el.dom.parentNode.parentNode.style.display = 'none';
                        }
                    }, { xtype: 'tbseparator', hidden: levels.add },
                    { text: "编辑", iconCls: 'icon-edit', ref: '../appeditBtn', disabled: true, hidden: levels.edit, handler: loadedit }, { xtype: 'tbseparator', hidden: levels.edit },
                    { text: "注销", iconCls: 'icon-closed', ref: '../logoutBtn', disabled: true, hidden: levels.logout, handler: logout }, { xtype: 'tbseparator', hidden: levels.logout },
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
                                            url: '/user/deleteUser',
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
        listeners: { 'celldblclick': { fn: loadedit, scope: this} },
        columns: [
                new Ext.grid.RowNumberer(),
                check_select,
                { header: 'ID', width: 50, dataIndex: 'ID', sortable: true, hidden: true },
                { header: '工员编号', tooltip: '工员编号', width: 100, dataIndex: 'UserCode', sortable: true },
                { header: '姓名', tooltip: '姓名', width: 150, id: 'TrueName', dataIndex: 'TrueName', sortable: true },
                { header: '类型', tooltip: '类型', width: 100, dataIndex: 'TypeName', sortable: true },
                { header: '所属部门', tooltip: '所属部门', width: 230, dataIndex: 'DeptName', sortable: true },
                { header: '日完成量', tooltip: '日完成量', width: 80, dataIndex: 'DayNum', sortable: true },
                { header: '联系电话', tooltip: '联系电话', width: 120, dataIndex: 'Mobile', sortable: true },
                { header: '注册时间', tooltip: '注册时间', width: 130, dataIndex: 'RegTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '登录时间', tooltip: '登录时间', width: 130, dataIndex: 'LoginTime', sortable: true, renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s') },
                { header: '会员状态', tooltip: '会员状态', width: 80, dataIndex: 'IsClose', sortable: true, renderer: function(v) { if (v) return '注销'; else return '正常' } }],
        bbar: new Ext.PagingToolbar({
            store: store,
            pageSize: query.limit,
            plugins: new Ext.ux.ProgressBarPager()
        })
    });
    grid.getSelectionModel().on('selectionchange', function(sm) {
        grid.removeBtn.setDisabled(sm.getCount() < 1);
        grid.appeditBtn.setDisabled(sm.getCount() < 1);
        grid.logoutBtn.setDisabled(sm.getCount() < 1);
    });
    function logout() {
        var s = grid.getSelectionModel().getSelected();
        Ext.MessageBox.show({ title: '提示框', msg: '你确定要注销吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
            fn: function(btn) {
                if (btn == 'ok') {
                    Ext.Ajax.request({
                        url: '/user/userlogout/' + s.data.ID,
                        method: "POST",
                        success: function(response, options) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            store.reload();
                        },
                        failure: function(cardform, response) {
                            var temp = Ext.util.JSON.decode(response.responseText);
                            Ext.Msg.alert("系统提示!", '注销失败');
                        }
                    });
                }
            }
        });
    }
    var searchPanel = new Ext.Panel({
        layout: 'fit',
        border: false,
        title: '当前位置:' + node.text,
        tbar: [
                '&nbsp;美工姓名/编号：', { xtype: "textfield", name: 'keyword', id: 'keyword' + frmid, width: 120 },
                '&nbsp;所在省：', shengCombox,
                '&nbsp;所在市：', shiCombox,
                '&nbsp;所属部门：', deptStoreCombo, '-',
                {
                    xtype: 'button', name: 'find', text: " &nbsp;查&nbsp;&nbsp;找&nbsp;&nbsp; ", iconCls: 'icon-search', pressed: true,
                    listeners: {
                        "click": function() {
                            query.keyword = Ext.getCmp('keyword' + frmid).getValue();
                            query.provinceID = Ext.getCmp('ddlProvinceID' + frmid).getValue();
                            query.countryID = Ext.getCmp('ddlCountryID' + frmid).getValue();
                            query.deptID = Ext.getCmp('ddlDepartmentID' + frmid).getValue();
                            store.load({ params: { start: 0} });
                        }
                    }
                }
            ],
        items: [grid]
    });

    GridMain(node, searchPanel);
}