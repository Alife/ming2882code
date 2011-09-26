Ext.onReady(function() {
    var fileName = '';
    var saveurl = '';
    Ext.QuickTips.init();
    Ext.BLANK_IMAGE_URL = "/ext/resources/images/default/s.gif";
    var store = new Ext.data.JsonStore({
        url: String.format('/showpic/kitimg?kitid={0}&classid={1}', kitid, kitClassID),
        fields: ['name', 'url', 'size', 'width', 'height']
    });
    store.load();
    var tpl = new Ext.XTemplate('<tpl for=".">',
                '<div class="thumb-wrap" id="{name}">',
		        '<div class="thumb"><img src="{url}" title="{name}"></div>',
		        '<span class="x-editable">{name}</span></div>',
                '</tpl>',
                '<div class="x-clear"></div>'
	        );
    var lookup = {};
    var classFields = Ext.data.Record.create(['ID', 'Name', 'Code', 'KitID', 'BoyNum', 'GirlNum', 'Imprint']);
    var classStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: classFields, id: "ID" }),
        url: '/order/getclass/' + kitid,
        listeners: { 'load': function() { classCmb.setValue(kitClassID); } }
    });
    classStore.load();
    var classCmb = new Ext.form.ComboBox({
        mode: 'local', triggerAction: 'all', forceSelection: true, emptyText: '---请选择---', editable: false,
        fieldLabel: '班级', name: 'ClassID', hiddenName: 'ClassID', displayField: 'Name', valueField: 'ID', width: 110,
        store: classStore,
        listeners: {
            'select': function(combo, record) {
                var id = record.get('ID');
                kitClassID = id;
                kitChildID = '';
                numStore.removeAll();
                numCmb.reset();
                if (id > 0) {
                    numStore.proxy = new Ext.data.HttpProxy({
                        url: String.format('/showpic/getclassnum?kitid={0}&classid={1}', kitid, kitClassID)
                    });
                    numStore.load();
                    store.proxy = new Ext.data.HttpProxy({
                        url: String.format('/showpic/kitimg?kitid={0}&classid={1}', kitid, kitClassID)
                    });
                    store.load();
                }
            }
        }
    });
    var numStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: Ext.data.Record.create(['KitChildID', 'ChildName', 'displayText', 'displayValue']), root: "data" }),
        url: '/showpic/getclassnum/'
    });
    var numCmb = new Ext.form.ComboBox({
        mode: 'local', triggerAction: 'all', forceSelection: true, emptyText: '---请选择---', editable: false,
        fieldLabel: '文件夹', name: 'NumID', hiddenName: 'NumID', displayField: 'ChildName', valueField: 'displayValue', width: 110,
        store: numStore,
        listeners: {
            'select': function(combo, record) {
                var folder = record.get('displayValue');
                kitChildID = record.get('KitChildID');
                store.removeAll();
                if (folder > 0) {
                    store.proxy = new Ext.data.HttpProxy({
                        url: String.format('/showpic/kitimg?kitid={0}&classid={1}&folder={2}', kitid, classCmb.getValue(), folder)
                    });
                    store.load();
                }
            }
        }
    });
    var efields = Ext.data.Record.create([{ name: 'displayValue' }, { name: 'displayText'}]);
    var questiontypeStore = new Ext.data.Store({
        autoLoad: true,
        reader: new Ext.data.JsonReader({ fields: efields, root: "data" }),
        url: '/showpic/questiontype'
    });
    var form = new Ext.form.FormPanel({
        frame: true, border: false, plain: true, layout: "form", defaultType: "textfield", labelAlign: 'right', labelWidth: 80,
        method: 'POST', waitMsgTarget: true,
        reader: new Ext.data.JsonReader({ id: 'ID' },
                    new Ext.data.Record.create([
                        { name: 'ID', type: 'int' },
                        { name: 'KitID', type: 'int' },
                        { name: 'KitClassID', type: 'int' },
                        { name: 'KitChildID' },
                        { name: 'UserID', type: 'int' },
                        { name: 'Intro', type: 'string' },
                        { name: 'QuestionType', type: 'int' },
                        { name: 'CreateTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
                        { name: 'IntroTime', type: 'date', dateFormat: 'Y-m-d H:i:s' }
                ])
        ),
        items: [{ xtype: 'hidden', name: 'ID', hidden: false }, { xtype: 'hidden', name: 'UserID', hidden: false },
            { xtype: 'textarea', name: 'Intro', height: 100, anchor: '100%', allowBlank: false, hideLabel: true },
            {
                xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                fieldLabel: '问题类型', name: 'QuestionType', hiddenName: 'QuestionType', displayField: 'displayText', valueField: 'displayValue', width: 120,
                store: questiontypeStore
            },
            { xtype: 'datefield', name: 'CreateTime', fieldLabel: '上次提交时间', width: 180, readOnly: true, format: 'Y-m-d H:i:s' },
            { xtype: 'datefield', name: 'IntroTime', fieldLabel: '上次解决时间', width: 180, readOnly: true, format: 'Y-m-d H:i:s'}]
    });
    var win = new Ext.Window({
        title: '提交问题', closeAction: 'hide', width: 500, height: 270, layout: 'fit', plain: true, border: false,
        /*modal: 'true',*/buttonAlign: 'center', loadMask: true, animateTarget: document.body, items: form,
        buttons: [{ text: '保存',
            handler: function() {
                if (form.getForm().isValid()) {
                    form.getForm().submit({
                        waitMsg: "数据保存中...",
                        waitTitle: "请稍侯",
                        url: saveurl,
                        params: { kitworkid: kitworkid, kitClassID: kitClassID, kitChildID: kitChildID, fileName: fileName },
                        success: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                            form.reset(); win.hide();
                            store.reload();
                        },
                        failure: function(form, response) {
                            var temp = Ext.util.JSON.decode(response.response.responseText);
                            Ext.Msg.alert("系统提示!", temp.msg);
                        }
                    });
                }
            }
        }, { text: '取消', handler: function() { form.getForm().reset(); win.hide(); } }]
    });
    var panel_west = new Ext.Panel({
        id: 'images-view',
        region: 'east',
        title: '略缩图',
        split: true,
        border: false,
        width: 160,
        minSize: 160,
        maxSize: 400,
        autoScroll: true,
        margins: '0 0 0 0',
        items: [new Ext.DataView({
            store: store,
            tpl: tpl,
            border: false,
            loadMask: true,
            animate: true,
            autoHeight: true,
            multiSelect: true,
            overClass: 'x-view-over',
            itemSelector: 'div.thumb-wrap',
            loadingText: '图片加载中',
            emptyText: '没有图片',
            prepareData: function(data) {
                data.name = data.name;
                data.url = data.url;
                data.size = Ext.util.Format.fileSize(data.size);
                lookup[data.name] = data;
                return data;
            },
            listeners: {
                selectionchange: {
                    fn: function(dv, nodes) {
                        if (nodes && nodes.length > 0) {
                            nodes = nodes[0];
                            var data = lookup[nodes.id];
                            fileName = data.name;
                            var image = Ext.get('image');
                            image.dom.src = data.url;
                            image.show();
                            image.setWidth(data.width);
                            image.setHeight(data.height);
                            pageInit();
                        }
                    }
                }
            }
        })],
        tbar: ['班&nbsp;&nbsp;&nbsp;&nbsp;级:', classCmb],
        listeners: { 'render': function() { new Ext.Toolbar(['文件夹:', numCmb]).render(this.tbar); } }
    });
    var center = new Ext.Panel({
        id: "west_center",
        region: 'center',
        title: '当前位置:浏览图片',
        border: false, bodyStyle: 'background:#fff;', resizable: false,
        defaults: { autoScroll: true },
        tbar: [
                    { text: '向左', id: 'icon-left', iconCls: 'icon-left', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '向右', id: 'icon-right', iconCls: 'icon-right', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '向上', id: 'icon-up', iconCls: 'icon-up', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '向下', id: 'icon-down', iconCls: 'icon-down', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '放大', id: 'icon-in', iconCls: 'icon-in' }, '-',
                    { text: '缩小', id: 'icon-out', iconCls: 'icon-out' }, '-',
                    { text: '还原', id: 'icon-zoom', iconCls: 'icon-zoom' }, '-',
                    { text: '结束校图', iconCls: 'icon-application_put',
                        handler: function() {
                            Ext.MessageBox.show({ title: '提示框', msg: '你确定结束校图吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                                fn: function(btn) {
                                    if (btn == 'ok') {
                                        Ext.Ajax.request({
                                            url: '/showpic/kitfinish/',
                                            method: "POST",
                                            params: { id: kitworkid },
                                            waitMsg: '提交数据中...',
                                            success: function(response, options) {
                                                var temp = Ext.util.JSON.decode(response.responseText);
                                                Ext.Msg.alert("系统提示!", temp.msg);
                                                var node = { 'id': 'node_' + data.ID, 'text': '工作单' + data.Name + '明显', 'kitorder': data,
                                                    'attributes': { 'Icon': 'icontab ' + node.attributes.Icon, 'Code': node.attributes.Code }
                                                };
                                                ALLEvents(node); //刷新
                                                var tab = center.getComponent(node.id); //关闭
                                                tab.destroy();
                                            },
                                            failure: function(response, options) {
                                                var temp = Ext.util.JSON.decode(response.responseText);
                                                Ext.Msg.alert("系统提示!", '失败');
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }, '-',
                    { text: '本图通过校验', iconCls: 'icon-accept', tooltip: "本图通过校验",
                        handler: function() {
                            Ext.MessageBox.show({ title: '提示框', msg: '你确定通过此图校验吗?', buttons: Ext.MessageBox.OKCANCEL, icon: Ext.MessageBox.WARNING,
                                fn: function(btn) {
                                    if (btn == 'ok') {
                                        if (fileName != '') {
                                            Ext.Ajax.request({
                                                url: '/showpic/setkitProofState/',
                                                method: "POST",
                                                params: { kitworkid: kitworkid, kitClassID: kitClassID, kitChildID: kitChildID, fileName: fileName },
                                                waitMsg: '提交数据中...',
                                                success: function(response, options) {
                                                    var temp = Ext.util.JSON.decode(response.responseText);
                                                    Ext.Msg.alert("系统提示!", temp.msg);
                                                    store.reload();
                                                },
                                                failure: function(response, options) {
                                                    var temp = Ext.util.JSON.decode(response.responseText);
                                                    Ext.Msg.alert("系统提示!", '失败');
                                                }
                                            });
                                        } else
                                            Ext.Msg.alert('提示', '请先选择图片');
                                    }
                                }
                            });
                        }
                    }, '-',
                    { text: '提交问题', iconCls: 'icon-comment',
                        handler: function() {
                            if (fileName != '') {
                                win.show();
                                saveurl = '/showpic/savequestion';
                                form.getForm().load({
                                    url: '/showpic/getquestion/',
                                    params: { kitworkid: kitworkid, kitClassID: kitClassID, kitChildID: kitChildID, fileName: fileName },
                                    waitMsg: '正在载入数据...',
                                    success: function(frm, action) { },
                                    failure: function(frm, action) {
                                        win.hide();
                                        Ext.Msg.alert('提示', '出错了');
                                    }
                                });
                            } else
                                Ext.Msg.alert('提示', '请先选择图片');
                        }
                    }, '-',
                    { text: '其它问题', iconCls: 'icon-tag_blue',
                        handler: function() {
                            win.show();
                            form.getForm().reset();
                            saveurl = '/showpic/saveotherquestion';
                        }
                    }
                ],
        html: '<img id="image" src="" style="cursor: url(images/openhand_8_8.cur),default;display:none" title="可上下左右移动" />'
    });
    var viewport = new Ext.Viewport({
        layout: 'border',
        items: [panel_west, center]
    });
    function pageInit() {
        var image = Ext.get('image');
        Ext.get('image').on({
            'mousedown': {
                fn: function() {
                    this.setStyle('cursor', 'url(/images/closedhand_8_8.cur),default;')
                }, scope: image
            },
            'mouseup': {
                fn: function() { this.setStyle('cursor', 'url(/images/openhand_8_8.cur),move;') }, scope: image
            },
            'dblclick': {
                fn: function() { zoom(image, true, 1.2); }
            }
        });
        new Ext.dd.DD(image, 'pic');
        //image.center(); //图片居中
        //获得原始尺寸
        image.osize = {
            width: image.getWidth(),
            height: image.getHeight()
        };
        Ext.get('icon-up').on('click', function() { imageMove('up', image) }); //向上移动
        Ext.get('icon-down').on('click', function() { imageMove('down', image) }); //向下移动
        Ext.get('icon-left').on('click', function() { imageMove('left', image) }); //左移
        Ext.get('icon-right').on('click', function() { imageMove('right', image) }); //右移动
        Ext.get('icon-in').on('click', function() { zoom(image, true, 1.5) }); //放大
        Ext.get('icon-out').on('click', function() { zoom(image, false, 1.5) }); //缩小
        Ext.get('icon-zoom').on('click', function() { restore(image) }); //还原
    };
    //图片移动　
    function imageMove(direction, el) {
        el.move(direction, 50, true);
    }
    //el　图片对象　
    //type　true放大,false缩小　
    //offset　量　
    function zoom(el, type, offset) {
        var width = el.getWidth();
        var height = el.getHeight();
        var nwidth = type ? (width * offset) : (width / offset);
        var nheight = type ? (height * offset) : (height / offset);
        var left = type ? -((nwidth - width) / 2) : ((width - nwidth) / 2);
        var top = type ? -((nheight - height) / 2) : ((height - nheight) / 2);
        el.animate({
            height: { to: nheight, from: height },
            width: { to: nwidth, from: width },
            left: { by: left },
            top: { by: top }
        }, null, null, 'backBoth', 'motion');
    }
    //图片还原
    function restore(el) {
        var size = el.osize;
        function center(el, callback) {
            el.center();
            callback(el);
        }
        el.fadeOut({
            callback: function() {
                el.setSize(size.width, size.height, {
                    callback: function() {
                        center(el, function(ee) { ee.fadeIn(); });
                    }
                });
            }
        });
    }
});