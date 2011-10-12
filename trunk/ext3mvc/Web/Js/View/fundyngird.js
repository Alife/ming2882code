com.ms.basic.DynamicGrid = Ext.extend(Ext.Panel, {
    initComponent: function() {
        this.grid = new Ext.grid.DynamicGrid({
            //title: '测试动态列',
            storeUrl: '/home/dynamicGrid',
            width: 600,
            height: 200,
            rowNumberer: true,
            checkboxSelModel: true,
            sm: new Ext.grid.CheckboxSelectionModel(),
            tbar: [{ text: '增加', iconCls: 'icon-add', scope: this, handler: this.addForm }, { text: '修改', iconCls: 'icon-edit', ref: '../editBtn', disabled: true/*, scope: this, handler: this.addForm*/}],
            bbar: new Ext.PagingToolbar({
                pageSize: 5,
                displayInfo: true,
                displayMsg: '显示第{0}到{1}条数据,共{2}条',
                emptyMsg: "没有数据",
                beforePageText: "第",
                afterPageText: '页 共{0}页'
            })
        });
        Ext.apply(this, { iconCls: 'tabs', autoScroll: false, closable: true, border: false, layout: 'fit', items: [this.grid] });
        //调用父类构造函数（必须）
        com.ms.basic.DynamicGrid.superclass.initComponent.apply(this, arguments);
    },
    addForm: function() {
        alert(this.grid.getId());
    },
    initMethod: function() {
    }
});