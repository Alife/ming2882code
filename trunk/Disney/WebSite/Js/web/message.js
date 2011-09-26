$(function() {
    $("#frmmsg").validate({
        rules: {
            truename: {
                required: true,
                rangelength: [2, 8],
                stringCheck: true
            },
            tel: {
                required: true,
                isPhone: true
            },
            mobile: {
                required: false,
                isMobile: true
            },
            qq: {
                required: true,
                digits: true
            },
            content: "required"
        },
        messages: {
            truename: {
                required: "请填写昵称",
                rangelength: "昵称介于{0}-{1}个字符"
            },
            tel: {
                required: "请填写电话号(可以是固定电话或手机)",
                isPhone: "请正确填写您的联系电话(可以是固定电话或手机)"
            },
            mobile: {
                isMobile: "请正确填写您的手机"
            },
            qq: {
                required: "请填写QQ号",
                digits: "QQ号必须是数字"
            },
            content: "留言没有填写"
        }
    });
});