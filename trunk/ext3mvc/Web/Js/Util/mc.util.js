Ext.namespace('mc.frame');
mc.frame.defaultUrl = function() {
    return 'home/load';
}
mc.frame.mask = new Ext.LoadMask(Ext.getBody(), { msg: "数据下载中,请稍等..." });
//同步ajax
mc.frame.createRequest = function() {
    var objXMLHttp = null;
    if (window.XMLHttpRequest) {
        objXMLHttp = new XMLHttpRequest();

        if (objXMLHttp.overrideMimeType) {
            objXMLHttp.overrideMimeType('text/xml');
        }
    }
    else {
        var MSXML = ['Msxml2.XMLHTTP', 'Microsoft.XMLHTTP', 'MSXML2.XMLHTTP.5.0', 'MSXML2.XMLHTTP.4.0', 'MSXML2.XMLHTTP.3.0'];

        for (var n = 0; n < MSXML.length; n++) {
            try {
                objXMLHttp = new ActiveXObject(MSXML[n]);
                break;
            }
            catch (e) { }
        }
    }
    return objXMLHttp;
};
mc.frame.ajaxSync = function(cfg) {
    cfg = typeof cfg == 'object' ? cfg : {};
    var url = cfg.url;
    var params = cfg.params || null;
    var method = cfg.method || 'post';
    var isEecode = cfg.isEecode || true;
    if (cfg.masker) cfg.masker.el.mask('数据提交中...', 'openLinkLoading');
    var http_request = mc.frame.createRequest();
    http_request.open(method, url, false);
    http_request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    http_request.setRequestHeader("encoding", "utf-8");
    if (cfg.params) {
        params = Ext.Object.toQueryString(params);
        http_request.setRequestHeader("Content-length", params.length);
    }
    http_request.send(params);
    if (cfg.masker) { cfg.masker.el.unmask(); }
    if (cfg.isEecode && http_request.responseText) return Ext.decode(http_request.responseText);
    return http_request.responseText;
}
//异步ajax
mc.frame.ajax = function(cfg) {
    cfg = typeof cfg == 'object' ? cfg : {};
    if (cfg.masker)
        cfg.masker.el.mask('数据提交中...', 'openLinkLoading');
    Ext.Ajax.request({
        url: cfg.url || mc.frame.defaultUrl(), params: cfg.params || { e: '' }, method: cfg.method || 'post',
        success: function(response, opts) {
            if (cfg.masker) { cfg.masker.el.unmask(); }
            var rs = response.responseText ? Ext.decode(response.responseText) : {};
            if (rs.success) {
                if (Ext.isFunction(cfg.onSuccess))
                    cfg.onSuccess.createDelegate(cfg.scope || window, [rs, opts])();
            } else {
                if (cfg.noErrMsg) {
                    if (Ext.isFunction(cfg.onFailure))
                        cfg.onFailure.createDelegate(cfg.scope || window, [rs, opts])();
                } else {
                    Ext.Msg.show({
                        title: '系统提示', msg: rs.msg || 'Error!!', width: 280, buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING, scope: cfg.scope, closable: false,
                        fn: function(btn) {
                            if (Ext.isFunction(cfg.onFailure))
                                cfg.onFailure.createDelegate(cfg.scope || window, [rs, opts, btn])();
                        }
                    });
                }
            }
        },
        failure: function(response, opts) {
            if (cfg.masker) { cfg.masker.el.unmask(); }
            var rs = response.responseText ? Ext.decode(response.responseText) : {};
            var msg = '';
            switch (opts.status) {
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
                    msg = '你请求的页面遇到问题，操作失败!错误代码:' + opts.status;
                    break;
            }
            Ext.Msg.show({
                title: '系统提示', msg: rs.msg || msg, width: 280, buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING, closable: false, scope: cfg.scope,
                fn: function(btn) {
                    if (Ext.isFunction(cfg.onFailure))
                        cfg.onFailure.createDelegate(cfg.scope || window, [rs, opts, btn])();
                }
            });
        }
    });
}
//表单ajax
mc.frame.submit = function(cfg) {
    cfg = typeof cfg == 'object' ? cfg : {};
    if (cfg.masker)
        cfg.masker.el.mask('数据提交中...', 'openLinkLoading');
    cfg.form.submit({
        url: cfg.url || mc.frame.defaultUrl(), params: cfg.params || { e: '' }, method: cfg.method || 'post', waitTitle: cfg.waitTitle || '请等待', waitMsg: cfg.waitMsg || '数据提交中...',
        success: function(form, action) {
            if (cfg.masker) { cfg.masker.el.unmask(); }
            var rs = action.response.responseText ? Ext.decode(action.response.responseText) : {};
            if (action.response.responseText) {
                if (Ext.isFunction(cfg.onSuccess))
                    cfg.onSuccess.createDelegate(cfg.scope || window, [rs, form])();
            } else {
                Ext.Msg.show({
                    title: '系统提示', msg: rs.msg || 'Error!!', width: 280, buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING, scope: cfg.scope, closable: false,
                    fn: function(btn) {
                        if (Ext.isFunction(cfg.onFailure))
                            cfg.onFailure.createDelegate(cfg.scope || window, [rs, form, btn])();
                    }
                });
            }
        },
        failure: function(form, action) {
            if (cfg.masker) { cfg.masker.el.unmask(); }
            var rs = action.response.responseText ? Ext.decode(action.response.responseText) : {};
            var msg = '';
            switch (action.response.status) {
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
                    msg = '你请求的页面遇到问题，操作失败!错误代码:' + action.response.status;
                    break;
            }
            Ext.Msg.show({
                title: '系统提示', msg: rs.msg || msg, width: 280, buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING, closable: false, scope: cfg.scope,
                fn: function(btn) {
                    if (Ext.isFunction(cfg.onFailure))
                        cfg.onFailure.createDelegate(cfg.scope || window, [rs, form, btn])();
                }
            });
        }
    });
}
function ForDight(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}
Ext.apply(Ext.form.VTypes, {
    //首先定义一个vtype名称，和他的验证函数，val参数是文本框的值，field是文本框。一般我就使用val和正则表达式比较就OK了。
    //然后定义一个vtype的报错信息，与vtype名称加Text后缀。OK了。
    passwd: function(val, field) {
        if (field.initialPassField) {
            var pwd = Ext.getCmp(field.initialPassField);
            return (val == pwd.getValue());
        }
        return true;
    },
    passwdText: '两次输入的密码不一致！',

    chinese: function(val, field) {
        var reg = /^[\u4e00-\u9fa5]+$/i;
        if (!reg.test(val)) {
            return false;
        }
        return true;
    },
    chineseText: '请输入中文',

    age: function(val, field) {
        try {
            if (parseInt(val) >= 18 && parseInt(val) <= 100)
                return true;
            return false;
        }
        catch (err) {
            return false;
        }
    },
    ageText: '年龄输入有误',

    alphanum: function(val, field) {
        try {
            if (!/\W/.test(val))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }
    },
    alphanumText: '请输入英文字母或是数字,其它字符是不允许的.',

    url: function(val, field) {
        try {
            if (/^(http|https|ftp):\/\/(([A-Z0-9][A-Z0-9_-]*)(\.[A-Z0-9][A-Z0-9_-]*)+)(:(\d+))?\/?/i.test(val))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }
    },
    urlText: '请输入有效的URL地址.',

    datecn: function(val, field) {
        try {
            var regex = /^(\d{4})-(\d{2})-(\d{2})$/;
            if (!regex.test(val)) return false;
            var d = new Date(val.replace(regex, '$1/$2/$3'));
            return (parseInt(RegExp.$2, 10) == (1 + d.getMonth())) && (parseInt(RegExp.$3, 10) == d.getDate()) && (parseInt(RegExp.$1, 10) == d.getFullYear());
        }
        catch (e) {
            return false;
        }
    },
    datecnText: '请使用这样的日期格式: yyyy-mm-dd. 例如:2008-06-20.',

    integer: function(val, field) {
        try {
            if (/^[-+]?[\d]+$/.test(val))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }
    },
    integerText: '请输入正确的整数',

    ip: function(val, field) {
        try {
            if ((/^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(val)))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }
    },
    ipText: '请输入正确的IP地址',

    phone: function(val, field) {
        try {
            if (/^((0[1-9]{3})?(0[12][0-9])?[-])?\d{6,8}$/.test(val))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }
    },
    phoneText: '请输入正确的电话号码,如:0920-29392929',

    mobilephone: function(val, field) {
        try {
            if (/(^0?[1][35][0-9]{9}$)/.test(val))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }
    },
    mobilephoneText: '请输入正确的手机号码',

    alpha: function(val, field) {
        try {
            if (/^[a-zA-Z]+$/.test(val))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }
    },
    alphaText: '请输入英文字母',

    money: function(val, field) {
        try {
            if (/^\d+\.\d{2}$/.test(val))
                return true;
            return false;
        }
        catch (e) {
            return false;
        }

    },
    moneyText: '请输入正确的金额',

    image: function(val, field) {
        try {
            var isVali = true;
            var i = val.lastIndexOf('.');
            if (i <= 0) isVali = false;
            var exname = Ext.util.Format.lowercase(val.substr(i, val.length - i));
            if (exname != '.jpg' && exname != '.jpeg' && exname != '.gif' && exname != '.bmp' && exname != '.png')
                isVali = false;
            return isVali;
        }
        catch (e) {
            return false;
        }

    },
    imageText: '请选择正确的图片'
});