$(function() {
    $(".ib_content img").LoadImage(true, 600, 600);
    $('.tops').each(function() {
        $(this).click(function() {
            var cid = $(this).parents('.item').attr('id').replace('commond', '');
            var nid = scope._nid;
            var obj = $(this);
            postdos(obj, nid, cid, 1);
        });
    })
    $('.dot').each(function() {
        $(this).click(function() {
            var cid = $(this).parents('.item').attr('id').replace('commond', '');
            var nid = scope._nid;
            var obj = $(this);
            postdos(obj, nid, cid, 2);
        });
    })
    $('.item').each(function() {
        var cid = $(this).attr('id').replace('commond', '');
        var ma_body = $(this).find('.ma_body');
        var commentlist = $(this).find('.commentlist');
        var nid = scope._nid;
        $(this).find('#reply_' + cid).click(function() {
            commentlist.show();
            if (ma_body.children().is('.frm')) { ma_body.children('.frm').show(); }
            else {
                ma_body.append('<div class="frm">\
                                    <form id="recommentfrm_' + cid + '" action="' + scope._weburl + 'article/recomment" method="post"> \
                                    <fieldset>\
                                        <dl>\
                                            <dt>回复:</dt>\
                                            <dd><textarea id="content_' + cid + '" name="content" style="visibility: hidden; display: none; "></textarea></dd>\
                                            <dt>验证码:</dt>\
                                            <dd><input id="vcode_' + cid + '" name="vcode" type="text" value="" class="w40" /><p></p></dd> \
                                            <dt>&nbsp;</dt> \
                                            <dd><input name="nid" type="hidden" value="' + nid + '" /><input name="cid" type="hidden" value="' + cid + '" />\
                                                <input id="btnc_' + cid + '" name="btnc" type="submit" value="提交" class="button" />\
                                                <input id="btncancel_' + cid + '" name="btncancel" type="button" value="取消" class="button" /></dd> \
                                        </dl> \
                                    </fieldset>\
                                    </form>\
                                </div>')
                CKEDITOR.replace('content_' + cid, { uiColor: '#efefef', toolbar: 'Small', height: 100 });
                $('#btncancel_' + cid).click(function() {
                    $(this).parents('.frm').hide();
                    if ($(this).parents('.frm').prev().html() == null)
                        commentlist.hide();
                });
                $('#recommentfrm_' + cid).validate({
                    submitHandler: function(form) {
                        $.ajax({
                            type: "post",
                            url: $(form).attr('action'),
                            dataType: 'json',
                            data: $(form).serialize(),
                            success: function(json) { jAlert(json.msg, '操作提示', function() { if (json.err == 1) location.reload(); }); }
                        });
                    },
                    rules: {
                        content: "required",
                        vcode: "required"
                    },
                    messages: {
                        content: "请填写的回复内容",
                        vcode: "请填写验证码"
                    }
                });
                $('#vcode_' + cid).focus(function() {
                    if ($(this).next().html() == ''){
                        $(this).next().append('<img src="' + scope._webimgurl + 'loading.gif" alt="看不清楚,点击换张图片" onclick=this.src=this.src+"?" class="pointer" />看不清楚,点击图片换张');
                        $(this).next().children().attr('src', '' + scope._weburl + 'validateCode?' + Math.random());
                    }
                    return false;
                });
                return false;
            }
        });
    });
    $("#frmc").validate({
        submitHandler: function(form) {
            $.ajax({
                type: "post",
                url: $(form).attr('action'),
                dataType: 'json',
                data: $(form).serialize(),
                success: function(json) { jAlert(json.msg, '操作提示', function() { if (json.err == 1) location.reload(); }); }
            });
        },
        rules: {
            content: "required",
            vcode: "required"
        },
        messages: {
            content: "请填写的评论内容",
            vcode: "请填写验证码"
        }
    });
    $('#vcode').focus(function() {
        if ($(this).next().html() == ''){
            $(this).next().append('<img src="' + scope._webimgurl + 'loading.gif" alt="看不清楚,点击换张图片" onclick=this.src=this.src+"?" class="pointer" />看不清楚,点击图片换张');
            $(this).next().children().attr('src', '' + scope._weburl + 'validateCode?' + Math.random());
        }
        return false;
    });
    CKEDITOR.replace('content', { uiColor: '#efefef', toolbar: 'Small' });
});
function postdos(obj, nid, cid, dots) {
    $.ajax({
        type: "POST",
        url: '/article/dots',
        dataType: "json",
        data: { nid: nid, cid: cid, dots: dots },
        success: function(json) {
            jAlert(json.msg, '温馨提示', function() {
                if (json.err == 1)
                    obj.next().html(Number(obj.next().html()) + 1);
            });
        }
    });
}
