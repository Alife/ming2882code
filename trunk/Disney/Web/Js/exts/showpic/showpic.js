showpic = function(node) {
    var levels = {};
    levels.solve = GetIsLevel(node.attributes.Code, 'solve');
    var kitworkid = node.attributes.id;
    var kitid = node.attributes.KitID;
    var kitClassID = '';
    var kitChildID = '';
    var fileName = '';
    var saveurl = '';
    var store = new Ext.data.JsonStore({
        url: String.format('/showpic/kitimg?kitworkid={0}&kitid={1}&classid={2}', kitworkid, kitid, kitClassID),
        fields: ['name', 'url', 'size', 'width', 'height', 'isaq']
    });
    var tpl = new Ext.XTemplate('<tpl for=".">',
                '<div class="thumb-wrap{[this.isaq(values)]}" id="{name}">',
		        '   <div class="thumb"><img src="{url}" title="{name}"></div>',
		        '   <span class="x-editable">{name}</span>{[this.isaqsqan(values)]}',
		        '</div>',
                '</tpl>',
                '<div class="x-clear"></div>', {
                    isaq: function(values) {
                        if (values.isaq) return ' red';
                        else return ' white';
                    },
                    isaqsqan: function(values) {
                        if (values.isaq) return ' <span class="question-red"></span>';
                        else return '';
                    }
                }
	        );
    var classFields = Ext.data.Record.create(['ID', 'Name', 'Code', 'KitID', 'BoyNum', 'GirlNum', 'Imprint']);
    var classStore = new Ext.data.Store({
        reader: new Ext.data.JsonReader({ fields: classFields, id: "ID", root: "data" }),
        url: '/showpic/getclass/' + kitid
    });
    classStore.load({ callback: function(r, options, success) { kitClassID = r[0].get('ID'); } });
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
                        url: String.format('/showpic/kitimg?kitworkid={0}&kitid={1}&classid={2}', kitworkid, kitid, kitClassID)
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
                        url: String.format('/showpic/kitimg?kitworkid={0}&kitid={1}&classid={2}&folder={3}', kitworkid, kitid, classCmb.getValue(), folder)
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
        url: '/showpic/questiontype',
        listeners: { load: function() { this.insert(0, new Ext.data.Record({ "displayValue": '', "displayText": "---请选择---" })); } }
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
                        { name: 'QuestionType' },
                        { name: 'CreateTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
                        { name: 'IntroTime', type: 'date', dateFormat: 'Y-m-d H:i:s' },
                        { name: 'Remark', type: 'string' }
                ])
        ),
        items: [{ xtype: 'hidden', name: 'ID', hidden: false }, { xtype: 'hidden', name: 'UserID', hidden: false },
            { xtype: 'textarea', fieldLabel: '问题描述', name: 'Intro', height: 80, anchor: '100%', allowBlank: userInfo.ID == 1 || typeInfo.Type == 2, readOnly: !(userInfo.ID == 1 || userInfo.ID == 2 || typeInfo.Type == 6) },
            {
                xtype: 'combo', mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
                fieldLabel: '问题类型', name: 'QuestionType', hiddenName: 'QuestionType', displayField: 'displayText', valueField: 'displayValue', width: 120,
                store: questiontypeStore, readOnly: !(userInfo.ID == 1 || userInfo.ID == 2 || typeInfo.Type == 6)
            },
            { xtype: 'datefield', name: 'CreateTime', fieldLabel: '上次提交时间', width: 180, readOnly: true, format: 'Y-m-d H:i:s' },
            { xtype: 'datefield', name: 'IntroTime', fieldLabel: '上次解决时间', width: 180, readOnly: true, format: 'Y-m-d H:i:s' },
            { xtype: 'textarea', fieldLabel: '已处理原因', name: 'Remark', height: 50, anchor: '100%', hidden: !(userInfo.ID == 1 || typeInfo.Type == 2), hideLabel: !(userInfo.ID == 1 || typeInfo.Type == 2)}]
    });
    var win = new Ext.Window({
        title: '提交/查看问题', closeAction: 'hide', width: 500, height: (userInfo.ID == 1 || typeInfo.Type == 2) ? 290 : 240, layout: 'fit', plain: true, border: false,
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
                            //Ext.Msg.alert("系统提示!", temp.msg);
                            form.reset(); win.hide();
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
    var showleft = new Ext.Panel({
        cls: 'images-view',
        border: false,
        autoScroll: true,
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
            listeners: {
                click: {
                    fn: function(dv, index, node) {
                        if (node) {
                            var data = dv.getStore().getAt(index).data;
                            fileName = data.name;
                            var image = Ext.get('image-' + kitworkid);
                            image.dom.src = data.url;
                            image.show();
                            image.setWidth(data.width);
                            image.setHeight(data.height);
                            pageInit();
                        }
                    }
                }
            }
        })]
    });
    var showcenter = new Ext.Panel({
        border: false, bodyStyle: 'background:#fff;', resizable: false, defaults: { autoScroll: true },
        listeners: { 'render': function() { this.setWidth(panel.getWidth() - 160); } },
        html: '<img id="image-' + kitworkid + '" src="" style="cursor: url(images/openhand_8_8.cur),default;display:none" title="可上下左右移动" />'
    });
    function pageInit() {
        var image = Ext.get('image-' + kitworkid);
        image.on({
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
        var dd = new Ext.dd.DD(image, 'pic');
        //dd.setXConstraint(0, 0);
        image.center(); //图片居中
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
            el.stopFx();
        }
        el.fadeOut({
            duration: 1.0,
            callback: function() {
                el.setSize(size.width, size.height, {
                    callback: function() {
                        center(el, function(ee) { ee.fadeIn(); });
                    }
                });
            }
        });
    }
    var panel = new Ext.Panel({
        title: '当前位置:' + node.text,
        expandable: false, autoScroll: false, resizable: false, animate: true, border: false, loadMask: true, layout: 'column',
        tbar: [
                    { text: '向左', id: 'icon-left', iconCls: 'icon-left', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '向右', id: 'icon-right', iconCls: 'icon-right', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '向上', id: 'icon-up', iconCls: 'icon-up', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '向下', id: 'icon-down', iconCls: 'icon-down', hidden: true }, { xtype: 'tbseparator', hidden: true },
                    { text: '放大', id: 'icon-in', iconCls: 'icon-in' }, '-',
                    { text: '缩小', id: 'icon-out', iconCls: 'icon-out' }, '-',
                    { text: '还原', id: 'icon-zoom', iconCls: 'icon-zoom' }, '-',
                    { text: '本图通过校验', iconCls: 'icon-accept', tooltip: "本图通过校验", hidden: levels.solve,
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
                                                    //Ext.Msg.alert("系统提示!", temp.msg);
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
                    }, { xtype: 'tbseparator', hidden: levels.solve },
                    { text: '提交/查看问题', iconCls: 'icon-comment',
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
                    { text: '其它问题', iconCls: 'icon-tag_blue', hidden: levels.solve,
                        handler: function() {
                            win.show();
                            form.getForm().reset();
                            saveurl = '/showpic/saveotherquestion';
                        }
                    }, { xtype: 'tbfill' }, '班&nbsp;&nbsp;&nbsp;&nbsp;级:', classCmb, '文件夹:', numCmb
                    , { text: '重新加载图片', tooltip: '重新加载图片可以清除图片在本电脑的缓存',
                        handler: function() {
                            Ext.Ajax.request({
                                url: '/showpic/updateprooftime/',
                                method: "POST",
                                params: { id: kitworkid },
                                waitMsg: '提交数据中...',
                                success: function(response, options) {
                                    store.reload();
                                },
                                failure: function(response, options) {
                                    var temp = Ext.util.JSON.decode(response.responseText);
                                    Ext.Msg.alert("系统提示!", '失败');
                                }
                            });
                        }
                    }
                ],
        items: [showcenter, showleft]
    });
    GridMain(node, panel);
    showleft.setSize(160, panel.getHeight() - 52);
    showcenter.setHeight(panel.getHeight() - 52);
};