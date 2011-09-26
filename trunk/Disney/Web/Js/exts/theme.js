//生成皮肤
function makeCookie() {
    var themes =
        [
            ['blue', '默认'],
            ['gray', '灰色']//,
    //            ['green', '绿色'],
    //            ['pink', '粉色'],
    //            ['purple', '紫色'],
    //            ['olive', '橄榄绿'],
    //            ['slate', '暗蓝色']
        ];
    this.cbThemes = new Ext.form.ComboBox({
        id: 'cbThemes',
        store: themes,
        width: 80,
        typeAhead: true,
        triggerAction: 'all',
        emptyText: '界面主题',
        forceSelection: true, editable: false,
        selectOnFocus: true
    });
    this.cbThemes.on({
        "select": function(field) {
            var css = field.getValue();
            var date = new Date();
            date.setTime(date.getTime() + 30 * 24 * 3066 * 1000);
            document.getElementsByTagName("link")[1].href = "/ext/resources/css/xtheme-" + css + ".css";
            document.cookie = "disneycss=" + css + ";expires=" + date.toGMTString();
        }
    });
    var c_name = "disneycss";
    if (document.cookie.length > 0) {
        var c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            var c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            var css = unescape(document.cookie.substring(c_start, c_end));
            document.getElementsByTagName("link")[1].href = "/ext/resources/css/xtheme-" + css + ".css";
        }
    }
}    