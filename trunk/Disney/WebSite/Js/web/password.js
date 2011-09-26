$(function() {
    $("#frm").validate({
        submitHandler: function(form) {
            $.ajax({
                type: "post",
                url: '/member/password.aspx',
                dataType: 'json',
                data: $(form).serialize(),
                success: function(json) { jAlert(json.msg, '溫馨提示', function() { if (json.success) location.reload(); }); }
            });
        },
        rules: {
            currentPassword: "required",
            newPassword: {
                required: true,
                rangelength: [5, 15]
            },
            confirmPassword: {
                required: true,
                rangelength: [5, 15],
                equalTo: "#newPassword"
            }
        },
        messages: {
            currentPassword: "請填寫原密碼",
            newPassword: {
                required: "請填寫新密碼",
                rangelength: "新密碼在{0}-{1}個字符"
            },
            confirmPassword: {
                required: "請填寫確認密碼",
                rangelength: "確認密碼在{0}-{1}個字符",
                equalTo: "密碼不一致"
            }
        }
    });
});