Ext.namespace("Ext.ux.form");

Ext.ux.form.TreeComboBox = Ext.extend(Ext.form.ComboBox, {

    initComponent: function(ct, position) {
        this.store = new Ext.data.SimpleStore({ fields: ['text', 'id'], data: [[]] });
        this.divId = 'tree-' + Ext.id();
        if (isNaN(this.maxHeight))
            this.maxHeight = 200;
        Ext.apply(this, {
            tpl: '<tpl>' + '<div style="height:' + this.maxHeight + 'px;">' + '<div id="' + this.divId + '"></div>' + '</div></tpl>'
        });

        var root = new Ext.tree.AsyncTreeNode({
            text: this.rootText,
            id: this.rootId,
            loader: new Ext.tree.TreeLoader({
                dataUrl: this.treeUrl,
                clearOnLoad: true
            })
        });

        this.tree = new Ext.tree.TreePanel({
            border: false,
            root: root,
            rootVisible: this.rootVisible,
            listeners: {
                scope: this,
                click: function(node) {
                    var selectNodeModel = this.selectNodeModel || 'exceptRoot';
                    var isRoot = (node == this.tree.getRootNode());
                    var selModel = this.selectNodeModel;
                    var isLeaf = node.isLeaf();
                    if (isRoot && selModel != 'all') {
                        return;
                    } else if (selModel == 'folder' && isLeaf) {
                        return;
                    } else if (selModel == 'leaf' && !isLeaf) {
                        return;
                    }
                    this.setValue(node);
                    this.collapse();
                }
            }
        });

        Ext.ux.form.TreeComboBox.superclass.initComponent.call(this);
    },

    onRender: function(ct, position) {
        Ext.ux.form.TreeComboBox.superclass.onRender.call(this, ct, position);
        this.on("expand", function() {
            if (!this.tree.rendered) {
                this.tree.render(this.divId);
                this.tree.expandAll();
            }
        }, this)
    },
    setValue: function(node) {
        if (typeof node == "object") {
            // 当node为object对象时 node和tree里面的对应
            this.lastSelectionText = node.text;
            // 设置显示文本为node的text
            this.setRawValue(node.text);
            if (this.hiddenField) {
                // 设置隐藏值为node的id
                this.hiddenField.value = node.id;
            }
            this.value = node.id;
            return this;
        } else {
            // 当node为文本时 这段代码是从combo的源码中拷贝过来的 具体作用不细说了
            var text = node;
            if (this.valueField) {
                var r = this.findRecord(this.valueField, node);
                if (r) {
                    text = r.data[this.displayField];
                } else if (Ext.isDefined(this.valueNotFoundText)) {
                    text = this.valueNotFoundText;
                }
            }
            //this.setValue({id:1,text:'北京',leaf:false});
            this.lastSelectionText = text;
            if (this.hiddenField) {
                this.hiddenField.value = node;
            }
            Ext.form.ComboBox.superclass.setValue.call(this, text);
            this.value = node;
            return this;
        }
    }
});
Ext.reg('uxtreecombobox', Ext.ux.form.TreeComboBox);

//    var treepanel = new Tree.TreePanel({
//        title: '功能菜单',
//        width: 200,
//        minSize: 180,
//        maxSize: 250,
//        split: true,
//        autoHeight: false,
//        frame: true, // 美化界面
//        autoScroll : true, // 自动滚动
//        enableDD: false, // 是否支持拖拽效果
//        containerScroll: false, // 是否支持滚动条
//        rootVisible: false, // 是否隐藏根节点,很多情况下，我们选择隐藏根节点增加美观性
//        border: true, // 边框
//        animate: true, // 动画效果
//        dataUrl: '/sys/arealists',
//        root: new Ext.tree.AsyncTreeNode({ nodeType: 'async', text: 'ID', draggable: false }),
//        listeners: {
//            render: function() {
//                this.expandAll();
//            }
//        }
//    });
//    var comboboxtree = new Ext.ux.form.TreeComboBox({
//        mode: 'local', triggerAction: 'all', forceSelection: true, editable: false, emptyText: '---请选择---',
//        fieldLabel: '父类', name: 'ParentID', hiddenName: 'ParentID', displayField: 'text', valueField: 'id', anchor: '50%',
//        maxHeight: 200, treeUrl: '/sys/arealists', rootText: '---请选择---', rootId: '0', rootVisible: true, selectNodeModel: 'all'
//    });