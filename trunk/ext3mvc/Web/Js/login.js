mc.frame.login = Ext.extend(Ext.Window, {
    title: '用户登录',
    renderTo: Ext.getBody(),
    layout: 'fit',
    width: 350,
    height: 150,
    border: true,
    closable: false,
    resizable: true,
    closeAction: 'hide',
    plain: true,
    layout: { type: 'vbox', align: 'stretch' },
    initComponent: function() {
        this.loginForm = new Ext.form.FormPanel({
            frame: true,
            bodyStyle: 'padding-top:8px',
            labelAlign: 'right',
            labelWidth: 60,
            labelPad: 0,
            border: false,
            items: [
                    { xtype: 'textfield', name: 'userName', value: 'admin', fieldLabel: '用户名', allowBlank: false, anchor: '95%' },
                    { xtype: 'textfield', name: 'password', inputType: 'password', value: 'admin', fieldLabel: '密&nbsp;&nbsp;&nbsp;码', allowBlank: false, anchor: '95%' }
            ],
            buttons: [
					{ text: '登录', width: 70, scope: this, handler: this.doLogin },
					{ text: '重置', scope: this, handler: function() { this.loginForm.form.getEl().dom.reset(); } }
            ],
            keys: [{ key: [13], scope: this, fn: this.doLogin}]
        });
        Ext.apply(this, { items: [this.loginForm] });
        mc.frame.login.superclass.initComponent.apply(this, arguments);
    },
    openLoingWin: function() {
        this.show();
    },
    doLogin: function() {
        if (!this.loginForm.getForm().isValid())
            return;
        mc.frame.submit({ form: this.loginForm.getForm(), url: '/home/Login', scope: this,
            onSuccess: function(rs, form) {
                var myApp = new mc.frame.app();
                myApp.loadUserInfo(rs.data);
                this.hide();
            }
        });
    }
});

Ext.onReady(function() {
    Ext.BLANK_IMAGE_URL = (Ext.isIE6 || Ext.isIE7) ? "/extjs/resources/images/default/s.gif" : 'data:image/gif;base64,R0lGODlhAQABAID/AMDAwAAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==';
    Ext.QuickTips.init();
    Ext.form.Field.prototype.msgTarget = 'side';
    mc.frame.ajax({ url: '/home/loadUserInfo', scope: this, noErrMsg: true,
        onSuccess: function(rs, opts) {
            var myApp = new mc.frame.app();
            myApp.loadUserInfo(rs.data);
        },
        onFailure: function(rs, opts) {
            var login = new mc.frame.login();
            login.openLoingWin();
        }
    });
});
