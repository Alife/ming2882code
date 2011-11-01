Ext.namespace('mc.frame');
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
    doLogin: function(btn) {
        if (!this.loginForm.getForm().isValid())
            return;
        this.loginForm.form.doAction('submit', {
            url: '/home/Login',
            method: 'post',
            waitTitle: '请等待',
            waitMsg: '正在登录...',
            params: '',
            success: function(form, resp) {
                var obj = Ext.util.JSON.decode(resp.responseText);
                if (resp.success) {
                    var myApp = new mc.frame.app();
                    myApp.loadUserInfo(obj);
                }
                else {
                    Ext.MessageBox.alert('提示', '用户名或者密码错误');
                }
            }
        });
    }
});

Ext.onReady(function() {
    Ext.QuickTips.init();
    Ext.form.Field.prototype.msgTarget = 'side';
    Ext.Ajax.request({
        method: 'post',
        url: '/home/loadUserInfo',
        params: '',
        success: function(resp) {
            var obj = Ext.util.JSON.decode(resp.responseText);
            if (obj.success) {
                var myApp = new mc.frame.app();
                myApp.loadUserInfo(obj);
            }
            else {
                var login = new mc.frame.login();
                login.openLoingWin();
            }
        },
        failure: function(resp) {
            switch (resp.status) {
                case 403:
                    Ext.Msg.show({ title: '提示', msg: '你请求的页面禁止访问!', icon: Ext.Msg.WARNING, buttons: Ext.Msg.OK })
                    break;
                case 404:
                    Ext.Msg.show({ title: '提示', msg: '你请求的页面不存在!', icon: Ext.Msg.WARNING, buttons: Ext.Msg.OK })
                    break;
                case 500:
                    Ext.Msg.show({ title: '提示', msg: '你请求的页面服务器内部错误!', icon: Ext.Msg.WARNING, buttons: Ext.Msg.OK })
                    break;
                case 502:
                    Ext.Msg.show({ title: '提示', msg: 'Web服务器收到无效的响应!', icon: Ext.Msg.WARNING, buttons: Ext.Msg.OK })
                    break;
                case 503:
                    Ext.Msg.show({ title: '提示', msg: '服务器繁忙，请稍后再试!!', icon: Ext.Msg.WARNING, buttons: Ext.Msg.OK })
                    break;
                default:
                    Ext.Msg.show({ title: '提示', msg: '你请求的页面遇到问题，操作失败!错误代码:' + resp.status, icon: Ext.Msg.WARNING, buttons: Ext.Msg.OK })
                    break;
            }
        }
    });
});
