<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>迪士尼童话相册管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="/ext/resources/css/ext-all.css" />
    <link rel="Stylesheet" type="text/css" href="/ext/resources/css/xtheme-blue.css" />
    <link rel="stylesheet" type="text/css" href="/css/default.css" />
    <link rel="stylesheet" type="text/css" href="/ext/ux/css/TreeGrid.css" />
    <link rel="stylesheet" type="text/css" href="/ext/ux/css/fileuploadfield.css" />

    <script type="text/javascript" src="/ext/adapter/ext/ext-base.js"></script>

    <script type="text/javascript" src="/ext/ext-all.js"></script>

    <script type="text/javascript">
        var c_name = "disneycss";
        if (document.cookie.length > 0) {
            var c_start = document.cookie.indexOf(c_name + "=")
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1
                var c_end = document.cookie.indexOf(";", c_start)
                if (c_end == -1) c_end = document.cookie.length
                var css = unescape(document.cookie.substring(c_start, c_end));
                document.getElementsByTagName("link")[1].href = "/ext/resources/css/xtheme-" + css + ".css";
            }
        }
        Ext.onReady(function() {
            Ext.form.Field.prototype.msgTarget = 'qtip';
            Ext.QuickTips.init();
            var loginForm = new Ext.FormPanel({
                el: 'hello-tabs',
                name: 'loginForm',
                autoTabs: true,
                activeTab: 0,
                deferredRender: false,
                border: false,
                labelWidth: 80,
                items: {
                    xtype: 'tabpanel',
                    activeTab: 0,
                    border: false,
                    defaults: { autoHeight: true, labelAlign: 'right', bodyStyle: 'padding:10px' },
                    items: [{
                        title: '身份认证',
                        layout: 'form',
                        defaults: { width: 230 },
                        defaultType: 'textfield',
                        items: [
                        { fieldLabel: '用户编号', name: 'loginId', allowBlank: false, emptyText: '用户编号/电话号码/用户名', blankText: '必须填写!' },
                        { fieldLabel: '密码', name: 'password', inputType: 'password', allowBlank: false, blankText: '必须填写!' },
                        { fieldLabel: '验证码', name: 'validateCode', xtype: 'textfield', width: 100, allowBlank: false, maxLength: 4, blankText: '必须填写!', maxLengthText: '最多不能超过4位' },
                        { xtype: 'compositefield', fieldLabel: '', combineErrors: false, items: [{ id: 'validateCodeImg', height: 42, xtype: 'displayfield'}] },
                        { xtype: 'compositefield', fieldLabel: '', combineErrors: false, items: [{ xtype: 'checkbox', name: 'rememberMe', boxLabel: '记住登录', inputValue: true}]}]
                    }, {
                        title: '信息公告栏',
                        layout: '',
                        html: '',
                        defaults: { width: 230 }
                    }, {
                        title: '关于',
                        layout: '',
                        html: '迪士尼童话相册管理系统 V1.0 <br><br> 版权所有 &copy 2010 欣创互联 <br><br><b>技术支持</b><br><br>电话: &nbsp;&nbsp;QQ:455261011&nbsp;&nbsp; Email:455261011@qq.com<br><br>访问官方网站:<a href="http://www.daodao123.com" target="_blank" title="点此访问欣创互联主页">www.daodao123.com</a>',
                        defaults: { width: 230 }
                    }
                    ]
                }
            });
            var win = new Ext.Window({
                //renderTo: Ext.getBody(),
                el: 'hello-win',
                layout: 'fit',
                width: 460,
                height: 280,
                closeAction: 'hide',
                plain: true,
                modal: true,
                collapsible: true,
                maximizable: false,
                draggable: true,
                closable: false,
                resizable: false,
                buttonAlign: 'center',
                animateTarget: document.body,
                items: loginForm,
                keys: {
                    key: Ext.EventObject.ENTER,
                    fn: function(btn, e) {
                        win.buttons[0].fireEvent("click", this);
                    }
                },
                buttons: [{
                    text: '登录',
                    listeners: {
                        "click": function() {
                            if (loginForm.getForm().isValid()) {
                                loginForm.getForm().submit({
                                    url: '/user/login',
                                    waitTitle: '提示',
                                    method: 'POST',
                                    waitMsg: '正在验证您的身份,请稍候.....',
                                    success: function(form, response) {
                                        var temp = Ext.util.JSON.decode(response.response.responseText);
                                        if (temp.success)
                                            window.location.href = '/work';
                                        else
                                            Ext.Msg.alert("系统提示!", temp.msg);
                                    },
                                    failure: function(form, response) {
                                        var temp = Ext.util.JSON.decode(response.response.responseText);
                                        Ext.Msg.alert("系统提示!", temp.msg);
                                        document.getElementById("imageCode").src = '/validateCode?' + Math.random();
                                    }
                                });
                            }
                        }
                    }
                }, { text: '取消', handler: function() { window.opener = null; window.open('', '_self'); window.close(); } }
                ]
            });
            win.show();
            var validateCodeImg = Ext.getDom('validateCodeImg');
            Ext.get(validateCodeImg.parentNode).createChild({ tag: 'img', src: '/validateCode',
                align: 'absbottom', id: "imageCode", style: 'cursor:pointer;'
            })
            Ext.DomHelper.insertAfter('imageCode', { tag: 'a', href: "#", style: "color:red", onclick: "javascript:document.getElementById('imageCode').src='/validateCode?' + Math.random();", html: "换一张" });
            Ext.DomHelper.insertAfter('imageCode', { tag: 'span', html: '看不清？' })
        });
    </script>

</head>
<body>
    <div id="hello-win" class="x-hidden">
        <div class="x-window-header">
            迪士尼童话相册管理系统
        </div>
        <div id="hello-tabs">
        </div>
        <div id="loginInfo" style='color: red; padding-left: 120px;'>
        </div>
    </div>
</body>
</html>
