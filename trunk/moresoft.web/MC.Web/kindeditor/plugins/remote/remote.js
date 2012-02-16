KindEditor.plugin('remote', function (K) {
    var editor = this, name = 'remote';
    editor.clickToolbar(name, function () {
        var dialog = editor.createDialog({
            name: 'remote_tip',
            width: 300,
            title: editor.lang('remote'),
            body: '<div style="margin:20px;">收集并上传远程图片中...<br>请不要关闭，上传完成后会自动关闭.</div>'
        });
        var html = editor.html();
        var imgs = $('img', html);
        var upimgs = []//收集远程图片对象img的数组，方便更新src属性
        , url = []//收集远程图片地址的数组
        , rx = new RegExp('^https?:\\/\\/' + location.host.replace(/\./g, '\\.'), 'i')//本站主域名的正则
        , rxRemote = /^https?:\/\// //远程图片正则
        , xhr, src;
        for (var i = 0; i < imgs.length; i++) {
            src = imgs[i].src;
            if (!rx.test(src) && rxRemote.test(src)) { url[url.length] = src; upimgs[upimgs.length] = imgs[i]; } //收集远程图片地址和img对象
        }
        function getParantA(obj) { while (obj = obj.parentNode) if (obj.tagName == 'A') return obj; return false; }
        if (url.length > 0) {//存在远程图片
            K.ajax('/kindeditor/asp.net/remotefiles.ashx', function (r) {
                if (r.success) {
                    var pNode;
                    for (var i = 0; i < r.items.length; i++) {
                        if (r.items[i]) {
                            html = html.replace(upimgs[i].src, r.items[i]);
                            //upimgs[i].src = r.items[i];
                            //pNode = getParantA(upimgs[i]); //========看图片是否加了链接，加了同时更新链接的href
                            //if (pNode) pNode.href = r.items[i];
                        }
                    }
                    editor.html(html);
                } else {
                    var dialog2 = editor.createDialog({
                        name: 'remote_tip_2',
                        width: 300,
                        title: '操作提示',
                        body: '<div style="margin:20px;">' + r.msg + '</div>'
                    });
                }
                editor.hideDialog();
            }, 'POST', {
                files: encodeURIComponent(url.join('|'))
            });
        }
        else {
            editor.hideDialog(); 
            var dialog1 = editor.createDialog({
                name: 'remote_tip_1',
                width: 300,
                title: '操作提示',
                body: '<div style="margin:20px;">没有远程图片需要上传！</div>'
            });
        }
    });
});