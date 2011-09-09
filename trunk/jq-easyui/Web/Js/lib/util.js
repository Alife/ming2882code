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
