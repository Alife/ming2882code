$(function() {
    $("#frm").validate({
        submitHandler: function(form) {
            $.ajax({
                type: "post",
                url: '/member/login.aspx',
                dataType: 'json',
                data: $(form).serialize(),
                success: function(json) { jAlert(json.msg, '溫馨提示', function() { if (json.success) location.href = json.data; }); }
            });
        },
        rules: {
            username: {
                required: true,
                rangelength: [2, 15]
            },
            password: {
                required: true,
                rangelength: [5, 15]
            }
        },
        messages: {
            username: {
                required: "請填寫會員帳號",
                rangelength: "會員帳號在{0}-{1}個字符"
            },
            password: {
                required: "請填寫密碼",
                rangelength: "密碼在{0}-{1}個字符"
            }
        }
    });
});