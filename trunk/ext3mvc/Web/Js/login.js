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
                        { xtype: 'checkbox', name: 'rememberMe', boxLabel: '记住登录', inputValue: true, uncheckedValue: false },
                        { xtype: 'displayfield', value: '忘记密码', style: 'padding-top:4px;cursor:pointer',
                            listeners: {
                                'click': {
                                    fn: function(field) {
                                        this.forgetWin.show();
                                    },
                                    scope: this
                                }
                            }
                        }
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
    //    var rs = ajaxSyncCall('/home/loadUserInfo', '');
    //    if (rs.success) {
    //        mc.frame.myApp = new mc.frame.app();
    //        mc.frame.myApp.loadUserInfo(rs.data);
    //    } else {
    //        mc.frame.myLogin = new mc.frame.login();
    //        mc.frame.myLogin.onShow();
    //    }
    mc.frame.ajax({ url: '/home/loadUserInfo', scope: this, noErrMsg: true, sync: true,
        onSuccess: function(rs, opts) {
            mc.frame.myApp = new mc.frame.app();
            mc.frame.myApp.loadUserInfo(rs.data);
        },
        onFailure: function(rs, opts) {
            mc.frame.myLogin = new mc.frame.login();
            mc.frame.myLogin.onShow();
        }
    });
});
function ajaxSyncCall(cfg) {
    cfg = typeof cfg == 'object' ? cfg : {};
    var scope = cfg.scope || this;
    var httpRequest;
    if (window.ActiveXObject) {
        httpRequest = new ActiveXObject('Microsoft.XMLHTTP');
    } else if (window.XMLHttpRequest) {
        httpRequest = new XMLHttpRequest();
    }
    httpRequest.open(cfg.method || 'post', cfg.url, false);
    httpRequest.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    httpRequest.send(typeof (cfg.params) == 'string' ? cfg.params : Ext.Object.toQueryString(cfg.params || { e: Math.random() }));
    setTimeout(function() { isTimeout = true; }, 5000);
    httpRequest.onreadystatechange = function() {
        if (httpRequest.readyState == 4 && !isTiemout) {
            if (httpRequest.status == 200) {
                var rs = httpRequest.responseText ? Ext.decode(httpRequest.responseText) : {};
                try {
                    if (cfg.noErrMsg && Ext.isFunction(cfg.onSuccess)) {
                        cfg.onSuccess.createDelegate(cfg.scope || window, [rs, cfg])();
                    }
                }
                catch (e) {
                    Ext.Msg.alert({ title: '系统提示', msg: "实际错误消息为：" + e.message + "\n错误类型字符串为：" + e.name });
                }
            }
            else {
                var rs = {};
                if (httpRequest.responseText && httpRequest.status == 200) rs = Ext.decode(httpRequest.responseText);
                var msg = '';
                switch (httpRequest.status) {
                    case 403:
                        msg = '你请求的页面禁止访问!';
                        break;
                    case 404:
                        msg = '你请求的页面不存在!';
                        break;
                    case 500:
                        msg = '你请求的页面服务器内部错误!';
                        break;
                    case 502:
                        msg = 'Web服务器收到无效的响应!';
                        break;
                    case 503:
                        msg = '服务器繁忙，请稍后再试!';
                        break;
                    default:
                        msg = '你请求的页面遇到问题，操作失败!错误代码:' + httpRequest.status;
                        break;
                }
                Ext.Msg.show({
                    title: '系统提示', msg: rs.msg || msg, width: 280, buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING, closable: false, scope: cfg.scope,
                    fn: function(btn) {
                        if (Ext.isFunction(cfg.onFailure))
                            cfg.onFailure.createDelegate(cfg.scope || window, [rs, cfg, btn])();
                    }
                });
            }
        }

    };
    return false;
}
