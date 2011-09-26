$(function() {
    $("#birthday").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
    $("#frm").validate({
        //success: "Valid",
        submitHandler: function(form) {
            $.ajax({
                type: "post",
                url: '/member/register.aspx',
                dataType: 'json',
                data: $(form).serialize(),
                success: function(json) { jAlert(json.msg, '溫馨提示', function() { if (json.success) location.href = json.data; }); }
            });
        },
        rules: {
            username: {
                required: true,
                rangelength: [2, 15],
                stringCheck: true,
                remote: {
                    url: "/member/existuser.ashx",
                    type: "post",
                    data: {
                        name: function() { return $("#username").val(); }
                    }
                }
            },
            password: {
                required: true,
                rangelength: [5, 15]
            },
            confirmPassword: {
                required: true,
                rangelength: [5, 15],
                equalTo: "#password"
            },
            email: {
                required: true,
                email: true,
                remote: {
                    url: "/member/existemail.ashx",
                    type: "post",
                    data: {
                        name: function() { return $("#email").val(); }
                    }
                }
            },
            truename: "required",
            sex: "required",
            countryid: "required",
            mobile: {
                required: true,
                isMobile: true
            },
            tel: { required: true }
        },
        messages: {
            username: {
                required: "請填寫會員帳號",
                rangelength: "會員帳號在{0}-{1}個字符",
                remote: jQuery.format("{0}會員帳號已經存在")
            },
            password: {
                required: "請填寫密碼",
                rangelength: "密碼在{0}-{1}個字符"
            },
            confirmPassword: {
                required: "請填寫確認密碼",
                rangelength: "確認密碼在{0}-{1}個字符",
                equalTo: "密碼不一致"
            },
            email: {
                required: "請填寫郵箱",
                email: "郵箱格式不正確",
                remote: jQuery.format("{0}郵箱已經存在")
            },
            truename: { required: "請填寫姓名" },
            sex: "請選擇性別",
            countryid: "請選擇縣市",
            mobile: {
                required: "請填寫移動手機",
                isMobile: "請正確填寫你的移動手機"
            },
            tel: { required: "請填寫聯繫電話(可以是固定電話或手機)" }
        }
    });
});