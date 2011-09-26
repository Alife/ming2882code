useredit = function(node) {
    //省份
    var shengStore = new Ext.data.JsonStore({
        autoDestroy: true,
        autoLoad: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/sys/areachildlist'
    });
    var shengCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, blankText: '必须选择!', emptyText: '---请选择---', fieldLabel: '所在省份', name: 'ProvinceID',
        hiddenName: 'ProvinceID', displayField: 'Name', valueField: 'ID', width: 120, store: shengStore,
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
    //市
    var shiStore = new Ext.data.JsonStore({
        autoDestroy: true,
        idProperty: 'ID',
        fields: ["ID", "Name"],
        url: '/sys/areachildlist'
    });
    var shiCombox = new Ext.form.ComboBox({ mode: 'local', triggerAction: 'all', forceSelection: true, editable: false,
        allowBlank: false, blankText: '必须选择!', emptyText: '---请选择---', fieldLabel: '所在城市', name: 'CountryID',
        hiddenName: 'CountryID', displayField: 'Name', valueField: 'ID', width: 120, store: shiStore
    });
    var form = new Ext.form.FormPanel({
        title: '当前位置:' + node.text,
        layout: 'fit',
        autoTabs: true,
        border: false,
        labelAlign: 'right',
        method: 'POST',
        waitMsgTarget: true,
        buttonAlign: 'center',
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
                        { name: 'Sex' },
                        { name: 'Birthday', type: 'string' },
                        { name: 'Type', type: 'int' },
                        { name: 'DepartmentID', type: 'int' },
                        { name: 'Zip', mapping: 'userinfo.Zip', type: 'string' },
                        { name: 'Address', mapping: 'userinfo.Address', type: 'string' }
                ])
            ),
        items: {
            xtype: 'tabpanel', border: false, deferredRender: false, activeTab: 0, defaults: { autoHeight: true, bodyStyle: 'padding:10px' },
            items: [{
                title: '基本信息',
                layout: 'form',
                defaultType: 'textfield',
                items: [
                    { xtype: 'hidden', name: 'ID', hidden: false },
                    { xtype: 'textfield', name: 'UserName', fieldLabel: '用户名', width: '113', allowBlank: false, invalidText: "用户名已经存在",
                        validator: function() {
                            var code = form.getForm().getValues().UserName;
                            var oldcode = userInfo.UserName;
                            var conn = Ext.lib.Ajax.getConnectionObject().conn;
                            conn.open("POST", String.format('/user/existsusername?code={0}&oldcode={1}', code, oldcode), false);
                            conn.send(null);
                            return Ext.util.JSON.decode(conn.responseText);
                        }
                    },
                    { xtype: 'textfield', name: 'Mobile', fieldLabel: '联系电话', width: '113',
                        validator: function() {
                            var code = form.getForm().getValues().Mobile;
                            var oldcode = userInfo.Mobile;
                            var conn = Ext.lib.Ajax.getConnectionObject().conn;
                            conn.open("POST", String.format('/user/existsmobile?code={0}&oldcode={1}', code, oldcode), false);
                            conn.send(null);
                            return Ext.util.JSON.decode(conn.responseText);
                        }
                    },
                    { xtype: 'textfield', name: 'TrueName', fieldLabel: '名字/名称', width: '113', allowBlank: false },
                    { xtype: 'combo', name: 'Sex', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, 
                        fieldLabel: '性别', displayField: 'displayText', valueField: 'displayValue', hiddenName: 'Sex', width: 80,
                        store: new Ext.data.ArrayStore({
                            id: 0,
                            fields: ['displayValue', 'displayText'],
                            data: [[1, '男'], [2, '女']]
                        })
                    },
                    { xtype: 'compositefield', fieldLabel: '所在地区', combineErrors: false, items: [shengCombox, shiCombox] },
                    { xtype: 'textfield', name: 'Zip', fieldLabel: '邮编', maxLength: 6, maxLengthText: "输入过长", width: '60' },
                    { xtype: 'textfield', name: 'Tel', fieldLabel: '固定电话', width: 180, maxLengthText: "输入过长" },
                    { xtype: 'textfield', name: 'Email', fieldLabel: 'Email', width: 200, maxLength: 45, maxLengthText: "输入过长", invalidText: "Email已经存在",
                        validator: function() {
                            var code = form.getForm().getValues().Email;
                            var oldcode = userInfo.Email;
                            var conn = Ext.lib.Ajax.getConnectionObject().conn;
                            conn.open("POST", String.format('/user/existsemail?code={0}&oldcode={1}', code, oldcode), false);
                            conn.send(null);
                            return Ext.util.JSON.decode(conn.responseText);
                        }
                    },
                    { xtype: 'textfield', name: 'Address', fieldLabel: '联系地址', width: 300, maxLength: 100, maxLengthText: "输入过长"}]
            }
            ]
        },
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: '/user/useredit',
                        success: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                        },
                        failure: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                        }
                    });
                }
            }
        }
        ]
    });
    GridMain(node, form);
    form.load({
        url: '/user/getuser/' + userInfo.ID,
        waitMsg: '正在载入数据...',
        success: function(frm, action) {
            var countryID = userInfo.CountryID;
            var conn = Ext.lib.Ajax.getConnectionObject().conn;
            conn.open("POST", String.format('/sys/areaparentlist?id={0}&type={1}', countryID, 3), false);
            conn.send(null);
            var respText = Ext.util.JSON.decode(conn.responseText);
            var provinceID = respText[0].ID;
            shengCombox.setValue(provinceID);
            shiStore.removeAll();
            shiStore.proxy = new Ext.data.HttpProxy({ url: String.format('/sys/areachildlist/{0}', provinceID) });
            shiStore.load({
                callback: function(r, options, success) {
                    if (success) shiCombox.setValue(countryID);
                }
            });
        },
        failure: function(frm, action) {
            Ext.Msg.alert('提示', '没有权限');
        }
    });
}