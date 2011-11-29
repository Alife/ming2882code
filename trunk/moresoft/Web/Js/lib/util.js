/*
    // class
	$.Class("ming.net.Test", {
				Extends : [],
				Implements : [],
				instance : function() {
					alert("wwww");
				},
				kkk : function() {
					alert("this is a supper method!");
				}
			});
	var test = new ming.net.Test();
	alert(test.isExt(ming.net.Test));
	alert(test.isImp(ming.Interface));
	// interface
	$.Interface("ming.Interface", {
				Extends : [],
				Implements : [],
				Methods : ['method1', 'method2']
			});
	// extends
	$.Class("ming.Test2", {
				Extends : [ming.net.Test],
				Implements : [ming.Interface],
				instance : function() {
					alert("sub class");
				}
			})
	var test2 = new ming.Test2();
	alert(test2.isExt(ming.net.Test));
	test2.kkk();
	test2.mehod1();
	alert(test2.isImp(ming.Interface));
*/
$.showMessage = function(message) {
    alert(message);
}
/**
* 创建命名空间和类
*/
$.Class = function(name, prototype) {
    // 命名空间
    var parentPakege = window;
    var nameSpace;
    var className = "";
    var createNameSpace = function(str) {
        parentPakege[str] = parentPakege[str] || {};
        nameSpace = parentPakege[str];
        parentPakege = nameSpace;
    };
    var namespaces = name.split(".");
    $.each(namespaces, function(i, str) {
        if (i == (namespaces.length - 1)) {
            className = str;
        } else {
            createNameSpace(str);
        }
    });
    nameSpace[className] = function(options) {
        this.options = $.extend({}, $.Class.defaults,
				nameSpace[className].defaults, options);
        this.instance();
    };
    // 继承Class类基本属性
    $.extend(nameSpace[className].prototype, $.Class.prototype);
    // 实现接口
    if (prototype.Implements) {
        $.each(prototype.Implements, function(i, inter) {
            $.Implements(nameSpace[className], inter);
        });
    }
    // 继承基类
    if (prototype.Extends) {
        $.each(prototype.Extends, function(i, sub) {
            $.Extends(nameSpace[className], sub);
        });
    }
    // 本身属性及实现
    $.extend(nameSpace[className].prototype, prototype);
};

$.Class.prototype = {
    Extends: [],
    Implements: [],
    instance: function() {

    },
    destroy: function() {

    },
    // 是否实现某些接口
    isImp: function() {
        var checkResult = true;
        var object = this;
        if (!object && typeof (object) != 'function') {
            return false;
        }
        $.each(arguments, function(i, interFace) {
            var result = $.grep(object.Implements, function(inter, i) {
                return inter = interFace;
            });
            if (result.length <= 0) {
                checkResult = false;
                return;
            }
        });
        return checkResult;
    },
    // 是否扩展自某些基类
    isExt: function() {
        var checkResult = true;
        var object = this;
        if (!object && typeof (object) != 'function') {
            return false;
        }
        $.each(arguments, function(i, supperClass) {
            var result = $.grep(object.Extends, function(sup, i) {
                return sup = supperClass;
            });
            if (result.length <= 0) {
                checkResult = false;
                return;
            }
        });
        return checkResult;
    }
};
$.Class.defaults = {
    enabled: true
};
/**
* 接口
*/
$.Interface = function(name, prototype) {
    // 命名空间
    var parentPakege = window;
    var nameSpace;
    var interfaceName = "";
    var createNameSpace = function(str) {
        parentPakege[str] = parentPakege[str] || {};
        nameSpace = parentPakege[str];
        parentPakege = nameSpace;
    };
    var namespaces = name.split(".");
    $.each(namespaces, function(i, str) {
        if (i == (namespaces.length - 1)) {
            interfaceName = str;
        } else {
            createNameSpace(str);
        }
    });
    nameSpace[interfaceName] = function(options) {
        this.options = $.extend({}, $.Interface.defaults,
				nameSpace[interfaceName].defaults, options);
        this.instance();
        return null;
    };
    // 继承Interface类基本属性
    nameSpace[interfaceName].prototype = $.extend({}, $.Interface.prototype);
    // 继承其他接口
    if (prototype.Extends) {
        $.each(prototype.Extends, function(i, subInterface) {
            $.Extends(nameSpace[interfaceName], subInterface);
        });
    }
    // 本身属性及实现
    $.extend(nameSpace[interfaceName].prototype, prototype);
    // 接口方法
    if (prototype.Methods) {
        $.each(prototype.Methods, function(i, method) {
            nameSpace[interfaceName].prototype[method] = function() {
                $.showMessage("This is a interface method!");
            }
        });
    }

}
$.Interface.prototype = {
    Extends: [],
    Methords: [],
    instance: function() {
        $.showMessage("This is a interface!Can not instance!");
    },
    destroy: function() {

    }
}
$.Interface.defaults = {
    enabled: true
};
/**
* 继承基类属性
*/
$.Extends = function(Class) {
    if (!Class && typeof (Class) != 'function') {
        return;
    }
    $.each(arguments, function(i) {
        if (i > 0) {
            $.extend(Class.prototype, this.prototype);
            Class.prototype.Extends.push(this);
        }
    });
}
/**
* 实现接口属性
*/
$.Implements = function(Class) {
    if (!Class && typeof (Class) != 'function') {
        return;
    }
    $.each(arguments, function(i) {
        if (i > 0) {
            Class.prototype.Implements.push(this);
            $.extend(Class.prototype, this.prototype);
        }
    });
}
/*
JS操作cookie
if (!getcookie("mytheme")) {
    setcookie("mytheme", "gray");
}
alert(getcookie("mytheme"));
delcookie("mytheme");
*/
function setcookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

function getcookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) {
        return unescape(arr[2]);
    } else {
        return "";
    }
}

function delcookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}



function DateUtil() { }
/**
*功能:格式化时间
*示例:DateUtil.Format("yyyy/MM/dd","Thu Nov 9 20:30:37 UTC+0800 2006 ");
*返回:2006/11/09
*/
DateUtil.Format = function(fmtCode, date) {
    var result, d, arr_d;

    var patrn_now_1 = /^y{4}-M{2}-d{2}\sh{2}:m{2}:s{2}$/;
    var patrn_now_11 = /^y{4}-M{1,2}-d{1,2}\sh{1,2}:m{1,2}:s{1,2}$/;

    var patrn_now_2 = /^y{4}\/M{2}\/d{2}\sh{2}:m{2}:s{2}$/;
    var patrn_now_22 = /^y{4}\/M{1,2}\/d{1,2}\sh{1,2}:m{1,2}:s{1,2}$/;

    var patrn_now_3 = /^y{4}年M{2}月d{2}日\sh{2}时m{2}分s{2}秒$/;
    var patrn_now_33 = /^y{4}年M{1,2}月d{1,2}日\sh{1,2}时m{1,2}分s{1,2}秒$/;

    var patrn_date_1 = /^y{4}-M{2}-d{2}$/;
    var patrn_date_11 = /^y{4}-M{1,2}-d{1,2}$/;

    var patrn_date_2 = /^y{4}\/M{2}\/d{2}$/;
    var patrn_date_22 = /^y{4}\/M{1,2}\/d{1,2}$/;

    var patrn_date_3 = /^y{4}年M{2}月d{2}日$/;
    var patrn_date_33 = /^y{4}年M{1,2}月d{1,2}日$/;

    var patrn_time_1 = /^h{2}:m{2}:s{2}$/;
    var patrn_time_11 = /^h{1,2}:m{1,2}:s{1,2}$/;
    var patrn_time_2 = /^h{2}时m{2}分s{2}秒$/;
    var patrn_time_22 = /^h{1,2}时m{1,2}分s{1,2}秒$/;

    if (!fmtCode) { fmtCode = "yyyy/MM/dd hh:mm:ss"; }
    if (date) {
        d = new Date(date);
        if (isNaN(d)) {
            msgBox("时间参数非法\n正确的时间示例:\nThu Nov 9 20:30:37 UTC+0800 2006\n或\n2006/       10/17");
            return;
        }
    } else {
        d = new Date();
    }

    if (patrn_now_1.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.yyyy + "-" + arr_d.MM + "-" + arr_d.dd + " " + arr_d.hh + ":" + arr_d.mm + ":" + arr_d.ss;
    }
    else if (patrn_now_11.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.yyyy + "-" + arr_d.MM + "-" + arr_d.dd + " " + arr_d.hh + ":" + arr_d.mm + ":" + arr_d.ss;
    }
    else if (patrn_now_2.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.yyyy + "/" + arr_d.MM + "/" + arr_d.dd + " " + arr_d.hh + ":" + arr_d.mm + ":" + arr_d.ss;
    }
    else if (patrn_now_22.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.yyyy + "/" + arr_d.MM + "/" + arr_d.dd + " " + arr_d.hh + ":" + arr_d.mm + ":" + arr_d.ss;
    }
    else if (patrn_now_3.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.yyyy + "年" + arr_d.MM + "月" + arr_d.dd + "日" + " " + arr_d.hh + "时" + arr_d.mm + "分" + arr_d.ss + "秒";
    }
    else if (patrn_now_33.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.yyyy + "年" + arr_d.MM + "月" + arr_d.dd + "日" + " " + arr_d.hh + "时" + arr_d.mm + "分" + arr_d.ss + "秒";
    }

    else if (patrn_date_1.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.yyyy + "-" + arr_d.MM + "-" + arr_d.dd;
    }
    else if (patrn_date_11.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.yyyy + "-" + arr_d.MM + "-" + arr_d.dd;
    }
    else if (patrn_date_2.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.yyyy + "/" + arr_d.MM + "/" + arr_d.dd;
    }
    else if (patrn_date_22.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.yyyy + "/" + arr_d.MM + "/" + arr_d.dd;
    }
    else if (patrn_date_3.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.yyyy + "年" + arr_d.MM + "月" + arr_d.dd + "日";
    }
    else if (patrn_date_33.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.yyyy + "年" + arr_d.MM + "月" + arr_d.dd + "日";
    }
    else if (patrn_time_1.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.hh + ":" + arr_d.mm + ":" + arr_d.ss;
    }
    else if (patrn_time_11.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.hh + ":" + arr_d.mm + ":" + arr_d.ss;
    }
    else if (patrn_time_2.test(fmtCode)) {
        arr_d = splitDate(d, true);
        result = arr_d.hh + "时" + arr_d.mm + "分" + arr_d.ss + "秒";
    }
    else if (patrn_time_22.test(fmtCode)) {
        arr_d = splitDate(d);
        result = arr_d.hh + "时" + arr_d.mm + "分" + arr_d.ss + "秒";
    }
    else {
        msgBox("没有匹配的时间格式!");
        return;
    }

    return result;
};
function splitDate(d, isZero) {
    var yyyy, MM, dd, hh, mm, ss;
    if (isZero) {
        yyyy = d.getYear();
        MM = (d.getMonth() + 1) < 10 ? "0" + (d.getMonth() + 1) : d.getMonth() + 1;
        dd = d.getDate() < 10 ? "0" + d.getDate() : d.getDate();
        hh = d.getHours() < 10 ? "0" + d.getHours() : d.getHours();
        mm = d.getMinutes() < 10 ? "0" + d.getMinutes() : d.getMinutes();
        ss = d.getSeconds() < 10 ? "0" + d.getSeconds() : d.getSeconds();
    } else {
        yyyy = d.getYear();
        MM = d.getMonth() + 1;
        dd = d.getDate();
        hh = d.getHours();
        mm = d.getMinutes();
        ss = d.getSeconds();
    }
    return { "yyyy": yyyy, "MM": MM, "dd": dd, "hh": hh, "mm": mm, "ss": ss };
}
function msgBox(msg) {
    window.alert(msg);
}