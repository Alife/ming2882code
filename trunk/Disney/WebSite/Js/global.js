function onAreaChange(cid, province, city) {
    var ddl = $(city)
    ddl[0].length = 0;
    ddl[0].options.add(new Option("--", ""));
    var pid = $(province).val();
    if (pid == 0) return false;
    $.ajax({
        type: "post",
        url: '/member/getarea',
        dataType: "json",
        data: { pid: pid },
        success: function(json) {
            $.each(json, function(i, item) {
                ddl[0].options.add(new Option(item.Name, item.ID));
            });
            for (var i = 0; i < ddl[0].options.length; i++) {
                if (ddl[0].options[i].value == cid) {
                    ddl[0].options[i].selected = true;
                    break;
                }
            }
        }
    });
}
function inputblur(obj) {
    $(obj).focus(function() {
        if ($(this).val() == this.defaultValue)
            $(this).val("");
    }).blur(function() {
        if ($(this).val() == "")
            $(this).val(this.defaultValue);
    });
}
jQuery.fn.LoadImage = function(scaling, width, height, loadpic) {
    if (loadpic == null) loadpic = scope._webimgurl + "loading.gif";
    return this.each(function() {
        var t = $(this);
        var src = $(this).attr("src")
        var img = new Image();
        //alert("Loading...")
        img.src = src;
        //自动缩放图片
        var autoScaling = function() {
            if (scaling) {
                if (img.width > 0 && img.height > 0) {
                    if (img.width / img.height >= width / height) {
                        if (img.width > width) {
                            t.width(width);
                            t.height((img.height * width) / img.width);
                        } else {
                            t.width(img.width);
                            t.height(img.height);
                        }
                    }
                    else {
                        if (img.height > height) {
                            t.height(height);
                            t.width((img.width * height) / img.height);
                        } else {
                            t.width(img.width);
                            t.height(img.height);
                        }
                    }
                }
            }
        }
        //处理ff下会自动读取缓存图片
        if (img.complete) {
            //alert("getToCache!");
            autoScaling();
            return;
        }
        //$(this).attr("src", "");
        var loading = $("<img alt=\"加载中...\" title=\"图片加载中...\" src=\"" + loadpic + "\" />");

        t.hide();
        t.after(loading);
        $(img).load(function() {
            autoScaling();
            loading.remove();
            t.attr("src", this.src);
            t.show();
        });

    });
}
var scope = scope ? scope : {};
$(function() {
    scope._version = "1.0";
    scope._ua = navigator.userAgent.toLowerCase();
    scope._IE = /msie/.test(scope._ua);
    scope._OPERA = /opera/.test(scope._ua);
    scope._MOZ = /gecko/.test(scope._ua);
    scope._IE5 = /msie 5 /.test(scope._ua);
    scope._IE55 = /msie 5.5/.test(scope._ua);
    scope._IE6 = /msie 6/.test(scope._ua);
    scope._IE7 = /msie 7/.test(scope._ua);
    scope._SAFARI = /safari/.test(scope._ua);
    scope._winXP = /windows nt 5.1/.test(scope._ua);
    scope._winVista = /windows nt 6.0/.test(scope._ua);
    scope._BASEJS = '/js/';
    scope._weburl = '/';
    scope._webimgurl = '/images/';
    var _IE = scope._IE, _MOZ = scope._MOZ, _IE6 = scope._IE6;
    if (_IE6) {
        $('.avatar').each(function() {
            var p = $(this).parents('.pm');
            var w = p.width();
            var h = p.height();
            $(this).LoadImage(true, w, h);
        });
        $('.pm').each(function() {
            $(this).mouseover(function() {
                $(this).addClass('pmhover');
            }).mouseout(function() {
                $(this).removeClass('pmhover');
            });
        });
        $("input").each(function() {
            $(this).mouseover(function() {
                switch ($(this).attr("type")) {
                    case 'button':
                        if ($(this).hasClass('buttonhover')) {
                            $(this).removeClass("button");
                            $(this).addClass("buttonhover");
                        }
                        break;
                    case 'submit':
                        if ($(this).hasClass('button')) {
                            $(this).removeClass("button");
                            $(this).addClass("buttonhover");
                        }
                        break;
                    case 'text':
                        if ($(this).attr('class') != 'readonly')
                            $(this).addClass("inputhover");
                        break;
                    case 'file':
                        $(this).addClass("inputhover");
                        break;
                    case 'password':
                        $(this).addClass("inputhover");
                        break;
                }
            }).mouseout(function() {
                switch ($(this).attr("type")) {
                    case 'button':
                        if ($(this).hasClass('buttonhover')) {
                            $(this).addClass("button");
                            $(this).removeClass("buttonhover");
                        }
                        break;
                    case 'submit':
                        if ($(this).hasClass('button')) {
                            $(this).addClass("button");
                            $(this).removeClass("buttonhover");
                        }
                        break;
                    case 'text':
                        if ($(this).attr('class') != 'readonly')
                            $(this).removeClass("inputhover");
                        break;
                    case 'file':
                        $(this).removeClass("inputhover");
                        break;
                    case 'password':
                        $(this).removeClass("inputhover");
                        break;
                }
            });
        });
        $("textarea").each(function() {
            $(this).mouseover(function() {
                $(this).addClass("inputhover");
            }).mouseout(function() {
                $(this).removeClass("inputhover");
            });
        });
    }
    if (scope._pageid) {
        Global.loadResource();
    }
});
var Global = {
    dw: function(s) {
        $.ajax({ type: "GET", url: s, dataType: "script" });
    },
    dwScript: function(o) {
        if (o.url) {
            if (!$.isArray(o.id))
                this.dw(o.url);
            else {
                for (var i = 0; i < o.id.length; i++) {
                    this.dw(o.url[i])
                }
            }
        }
        else {
            throw new Error("no script content or url specified")
        }
    },
    getJsVersion: function() {
        var ver = false;
        if (typeof (scope) != "undefined") {
            ver = (typeof (scope._version) != "undefined") ? scope._version : ""
        } else {
            if (window.parent) {
                if (typeof (window.parent.conf) != "undefined") {
                    ver = (typeof (window.parent.scope._version) != "undefined") ? window.parent.scope._version : ""
                }
            }
        }
        if (ver) {
            return "?version=" + ver
        } else {
            return ""
        }
    },
    getPageId: function() {
        return scope._pageid
    },
    loadResource: function() {
        var page = this.getPageId();
        var url;
        var _mode = scope._devMode ? scope._devMode : 1;
        switch (scope._devMode) {
            case 1:
                url = scope._BASEJS + scope._pagename + "/" + page + ".js" + this.getJsVersion();
                break;
            case 2:
                if (scope._uid == scope._fuid)
                    url = scope._BASEJS + scope._pagename + ".js" + this.getJsVersion();
                break;
            case 3:
                page = page.split(',');
                url = new Array();
                for (var i = 0; i < page.length; i++)
                    url[i] = scope._BASEJS + scope._pagename + "/" + page[i] + ".js" + this.getJsVersion();
                break;
            case 4:
                page = page.split(',');
                url = new Array();
                var pname = scope._pagename.split(',');
                for (var i = 0; i < page.length; i++)
                    url[i] = scope._BASEJS + pname[i] + "/" + page[i] + ".js" + this.getJsVersion();
                break;
            default:
                url = scope._BASEJS + scope._pagename + "/" + page + ".js";
                break
        }
        this.dwScript({
            id: page,
            url: url
        })
    }
};
