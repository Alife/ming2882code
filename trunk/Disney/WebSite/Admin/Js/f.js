var _js = 'http://js.hi-p.cn/';
var _weburl = 'http://www.hi-p.cn/';
var _webimgurl = 'http://img.hi-p.cn/';

(function() {
    $.fn.selectAll = function(options) {
        var settings = {
            range: '',
            func: null
        };
        return this.each(function() {
            if (options) settings = $.extend(settings, options);
            var self = $(this);
            var checks = $('input[type="checkbox"]', settings.range);
            var sizes = checks.size();
            $(this).click(function() {
                var isCheck = $(this).attr("checked");
                checks.attr('checked', isCheck);
                var checkedSize = $('input[type="checkbox"]:checked', settings.range).size();
                if ($.isFunction(settings.func)) {
                    settings.func(checkedSize);
                }
            })
            checks.click(function() {
                var checkedSize = $('input[type="checkbox"]:checked', settings.range).size();
                if (checkedSize < sizes) {
                    self.attr('checked', '');
                } else {
                    self.attr('checked', 'checked');
                }
                if ($.isFunction(settings.func)) {
                    settings.func(checkedSize);
                }
            })
        })
    }
})(jQuery)
function opfun(op, url) {
    var id = '';
    $('input:checkbox[name=cbitem]').each(function() {
        if ($(this).attr('checked'))
            id += $(this).val() + ',';
    });
    if (id == '') {
        jAlert('没有选项');
        return false;
    } else {
        if (op == 'del') {
            jConfirm('确认要删除吗?', '温馨提示', function(r) {
                if (r) { ops(id, op, url); }
            });
        } else { ops(id, op, url); }
    }
}
function ops(id, op, url) {
    $.ajax({
        type: "post",
        url: url,
        dataType: "json",
        data: { id: id, op: op },
        success: function(json) {
            jAlert(json.msg, '温馨提示', function() { location.reload(); });
        }
    });
    return false;
}
function oporder(url) {
    var id = '';
    var orderid = '';
    $('input:checkbox[name=cbitem]').each(function() {
        id += $(this).val() + ',';
    });
    $('input:text[name=orderid]').each(function() {
        orderid += $(this).val() + ',';
    });
    $.ajax({
        type: "post",
        url: url,
        dataType: "json",
        data: { id: id, orderid: orderid },
        success: function(json) {
            jAlert(json.msg, '温馨提示', function() { location.reload(); });
        }
    });
}

//省市分类联动
function onAreaChange(cid, province, city) {
    var ddl = $(city)
    ddl[0].length = 0;
    ddl[0].options.add(new Option("--", "0"));
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
} //省市分类联动结束
function loadmsgfrom(vid, tid) {
    $(document.body).append('<div id="' + vid + '" title="发送留言" style="display: none;">\
        <fieldset style="padding:0px;margin:0px;">\
            <form id="frmmsg" name="frmmsg">\
            <dl>\
                <dt for="title">标题：</dt>\
                <dd><input id="title" name="title" value="" type="text" /></dd>\
                <dt for="content">内容：</dt>\
                <dd><textarea id="content" name="content" rows="8" cols="50"></textarea></dd>\
                <dt>&nbsp;</dt>\
                <dd>\
                <input name="id" id="id" type="hidden" value="" />\
                <input name="op" id="op" type="hidden" value="" />\
                <input id="btnmsg" type="submit" value="发送" class="button" /></dd>\
            </dl>\
            </form>\
        </fieldset>\
    </div>');
}

(function(JQ) {
    JQ.fn.textEdit = function(obj, pobj, box, url) {
        var id = $(pobj).attr("link");
        $(obj).click(function() {
            var o = $.trim($(obj).html());
            var textarea = $(box).blur(
                            function() {
                                var spanValue = $.trim($(this).val());
                                if (spanValue === '') {
                                    jAlert('没有填写内容');
                                    return;
                                }
                                if (spanValue != o) {
                                    $.ajax({
                                        type: "POST",
                                        url: url,
                                        dataType: "json",
                                        data: { pid: id, title: spanValue },
                                        success: function(json) {
                                            jAlert(json.msg);
                                        }
                                    });
                                }
                                var spanText = $('<em class="inline-edit" title="点击可以编辑">' + spanValue + '</em>');
                                spanText.textEdit(spanText, $(pobj), box, url);
                                $(this).after(spanText).remove();
                                spanText.siblings().val(spanValue);
                            }).focus(function() { this.select(); });
            $(obj).after(textarea).remove();
            textarea[0].focus();
        });
    }
} (jQuery));
jQuery(function($) {
    $.datepicker.regional['zh-CN'] = {
        closeText: '关闭',
        prevText: '&#x3c;上月',
        nextText: '下月&#x3e;',
        currentText: '今天',
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
		'七月', '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['一', '二', '三', '四', '五', '六',
		'七', '八', '九', '十', '十一', '十二'],
        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
        dateFormat: 'yy-mm-dd', firstDay: 1,
        isRTL: false
    };
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
});
jQuery.validator.addMethod("byteRangeLength", function(value, element, param) {
    var length = value.length;
    for (var i = 0; i < value.length; i++) {
        if (value.charCodeAt(i) > 127) {
            length++;
        }
    }
    return this.optional(element) || (length >= param[0] && length <= param[1]);
}, "输入的值在3-15个字节之间。");
jQuery.validator.addMethod("stringCheck", function(value, element) {
    return this.optional(element) || /^[\u0391-\uFFE5\w]+$/.test(value);
}, "只能包括中文字、英文字母、数字和下划线");
jQuery.validator.addMethod("isPhone", function(value, element) {
    var length = value.length;
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    var tel = /^\d{3,4}-?\d{7,9}$/;
    return this.optional(element) || (tel.test(value) || mobile.test(value));
}, "请正确填写您的联系电话");
jQuery.validator.addMethod("isMobile", function(value, element) {
    var length = value.length;
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    return this.optional(element) || (length == 11 && mobile.test(value));
}, "请正确填写您的手机号码");
$(function() {
    $(".tablefont tr").mouseover(function() {
        //如果鼠标移到class为list-div的表格的tr上时，执行函数
        $(this).addClass("over");
    }).mouseout(function() {
        //给这行添加class值为over，并且当鼠标一出该行时执行函数
        $(this).removeClass("over");
    }) //移除该行的class
    $(".tablefont tr:even").addClass("alt");
    //给class为list-div的表格的偶数行添加class值为alt
    $("#cbChoose").selectAll({ range: '.tbodyfont' });
    $("#loading").bind("ajaxSend", function() {
        $(this).show();
    }).bind("ajaxComplete", function() {
        $(this).hide();
    });
});