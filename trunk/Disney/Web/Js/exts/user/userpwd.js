userpwd = function(node) {
    var levels = {};
    levels.edit = GetIsLevel(node.attributes.Code, 'edit');
    if (levels.edit) {
        Ext.Msg.alert("系统提示!", '没有权限');
        return false;
    }
    var userType = Ext.util.Format.lowercase(userInfo.Type);
    var form = new Ext.form.FormPanel({
        title: '当前位置:' + node.text,
        border: false,
        labelAlign: 'right',
        method: 'POST',
        waitMsgTarget: true,
        bodyStyle: 'padding:10px',
        items: [{ xtype: 'textfield', name: 'OldPassword', fieldLabel: '旧密码', inputType: 'password', width: '113', allowBlank: false, blankText: '必须填写!', minLength: 6, minLengthText: '密码最少6位' },
        { xtype: 'textfield', name: 'Password', fieldLabel: '新密码', inputType: 'password', width: '113', allowBlank: false, blankText: '必须填写!', minLength: 6, minLengthText: '密码最少6位' },
        { xtype: 'textfield', name: 'RePassword', fieldLabel: '确认密码', inputType: 'password', width: '113', allowBlank: false, blankText: '必须填写!', minLength: 6, minLengthText: '密码最少6位',
            invalidText: "两次密码输入不正确",
            validator: function(newValue) {
                if (newValue != form.getForm().findField('Password').getValue())
                    return false;
                return true;
            }
        },
        { xtype: 'button', text: "保存", iconCls: 'icon-save', fieldLabel: '保存', itemCls: 'hiddenlabel',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据提交中...",
                        waitTitle: "请稍侯",
                        url: '/user/userpwd',
                        success: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            if (temp.success) form.reset();
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
}