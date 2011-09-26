$(function() {
    $("#birthday").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
    $("#frm").validate({
        submitHandler: function(form) {
            $.ajax({
                type: "post",
                url: '/member/profile.aspx',
                dataType: 'json',
                data: $(form).serialize(),
                success: function(json) { jAlert(json.msg, '溫馨提示', function() { if (json.success) location.reload(); }); }
            });
        },
        rules: {
            truename: "required",
            email: {
                required: true,
                email: true,
                remote: {
                    url: "/member/existemail.ashx",
                    type: "post",
                    data: {
                        oldemail: $("#oldemail").val(),
                        name: function() { return $("#email").val(); }
                    }
                }
            },
            sex: "required",
            countryid: "required",
            mobile: {
                required: true,
                isMobile: true
            },
            tel: { required: true }
        },
        messages: {
            truename: { required: "請填寫姓名" },
            email: {
                required: "請填寫郵箱",
                email: "郵箱格式不正確",
                remote: jQuery.format("{0}郵箱已經存在")
            },
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