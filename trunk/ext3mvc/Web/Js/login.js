mc.frame.login = Ext.extend(Ext.Window, {
    title: '用户登录', renderTo: Ext.getBody(), width: 350, height: 160, border: true, plain: true, closable: false, closeAction: 'hide',
    initComponent: function() {
        this.loginForm = new Ext.form.FormPanel({
            frame: true, border: false, labelAlign: 'right', labelWidth: 60, labelPad: 0,
            items: [
                    { xtype: 'textfield', name: 'userName', value: 'admin', fieldLabel: '用户名', allowBlank: false, anchor: '95%' },
                    { xtype: 'textfield', name: 'password', inputType: 'password', value: 'admin', fieldLabel: '密&nbsp;&nbsp;&nbsp;码', allowBlank: false, anchor: '95%' },
                    {
                        xtype: 'compositefield', combineErrors: false,
                        items: [
                        { xtype: 'checkbox', name: 'rememberMe', boxLabel: '两周内自动登录', inputValue: true },
                        { xtype: 'displayfield', value: '忘记密码', style: 'padding-top:4px;cursor:pointer', listeners: { 'click': { fn: function() { this.forgetWin.show() }, scope: this}} }
                        ]
                    }
            ],
            buttons: [
					{ text: '登录', scope: this, handler: this.onLogin },
					{ text: '重置', scope: this, handler: function() { this.loginForm.form.getEl().dom.reset(); } }
            ],
            keys: [{ key: [13], scope: this, fn: this.onLogin}]
        });
        this.forgetForm = new Ext.form.FormPanel({
            frame: true, border: false, labelAlign: 'right', labelWidth: 60, labelPad: 0,
            items: [
                    { xtype: 'textfield', name: 'userName', fieldLabel: '用户名', allowBlank: false, anchor: '95%' },
                    { xtype: 'textfield', name: 'email', fieldLabel: '邮&nbsp;&nbsp;&nbsp;箱', allowBlank: false, anchor: '95%' }
            ],
            buttons: [
					{ text: '提交', scope: this, handler: this.onForget },
					{ text: '取消', scope: this, handler: function() { this.forgetForm.form.getEl().dom.reset(); this.forgetWin.hide(); } }
            ]
        });
        this.forgetWin = new Ext.Window({ title: '找回密码', width: 350, height: 150, border: true, modal: true, plain: true, closeAction: 'hide', items: [this.forgetForm] });
        Ext.apply(this, { items: [this.loginForm] });
        mc.frame.login.superclass.initComponent.apply(this, arguments);
    },
    onLogin: function() {
        if (!this.loginForm.getForm().isValid())
            return;
        mc.frame.submit({ form: this.loginForm.getForm(), url: '/home/Login', scope: this,
            onSuccess: function(rs, form) {
                mc.frame.myApp = new mc.frame.app();
                mc.frame.myApp.onShow();
                mc.frame.myApp.loadUserInfo(rs.data);
                mc.frame.myLogin = this;
                this.hide();
            }
        });
    },
    onForget: function() {
        if (!this.forgetForm.getForm().isValid())
            return;
        mc.frame.submit({ form: this.forgetForm.getForm(), url: '/home/forget', scope: this,
            onSuccess: function(rs, form) {
                Ext.Msg.alter({ title: '系统提示', msg: rs.msg });
                this.forgetWin.hide();
            }
        });
    },
    onShow: function() {
        this.show();
    }
});
mc.frame.myApp = function() { }
mc.frame.myLogin = function() { }
Ext.onReady(function() {
    Ext.BLANK_IMAGE_URL = (Ext.isIE6 || Ext.isIE7) ? "/extjs/resources/images/default/s.gif" : 'data:image/gif;base64,R0lGODlhAQABAID/AMDAwAAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==';
    Ext.QuickTips.init();
    Ext.form.Field.prototype.msgTarget = 'side';
    var rs = mc.frame.ajaxSyncString({ url: '/home/loadUserInfo' });
    if (rs.success) {
        mc.frame.myApp = new mc.frame.app();
        mc.frame.myApp.loadUserInfo(rs.data);
    } else {
        mc.frame.myLogin = new mc.frame.login();
        mc.frame.myLogin.onShow();
    }
    //    mc.frame.ajax({ url: '/home/loadUserInfo', scope: this, noErrMsg: true, sync: true,
    //        onSuccess: function(rs, opts) {
    //            mc.frame.myApp = new mc.frame.app();
    //            mc.frame.myApp.loadUserInfo(rs.data);
    //        },
    //        onFailure: function(rs, opts) {
    //            mc.frame.myLogin = new mc.frame.login();
    //            mc.frame.myLogin.onShow();
    //        }
    //    });
});
