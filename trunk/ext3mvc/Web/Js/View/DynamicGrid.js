Ext.grid.DynamicGrid = Ext.extend(Ext.grid.GridPanel, {
    initComponent: function() {
        //创建store
        var ds = new Ext.data.Store({
            url: this.storeUrl,
            reader: new Ext.data.JsonReader()
        });

        //设置默认配置
        var config = {
            viewConfig: { forceFit: true },
            enableColLock: false, loadMask: true, stripeRows: true, border: false,
            ds: ds,
            columns: []
        };

        //给分页PagingToolbar绑定store
        this.bbar.bindStore(ds, true);

        Ext.apply(this, config);
        Ext.apply(this.initialConfig, config);
        Ext.grid.DynamicGrid.superclass.initComponent.apply(this, arguments);
    },
    addForm: function() {
        alert('parent');
    },

    onRender: function(ct, position) {
        this.colModel.defaultSortable = true;
        Ext.grid.DynamicGrid.superclass.onRender.call(this, ct, position);

        this.el.mask('Loading...');
        this.store.on('load', function() {
            var me = this;
            if (typeof (this.store.reader.jsonData.columns) === 'object') {
                var columns = [];

                if (this.rowNumberer) {
                    columns.push(new Ext.grid.RowNumberer());
                }

                if (this.checkboxSelModel) {
                    columns.push(new Ext.grid.CheckboxSelectionModel());
                    me.getSelectionModel().on('selectionchange', function(sm) {
                        me.editBtn.setDisabled(sm.getCount() < 1);
                    });
                }

                Ext.each(this.store.reader.jsonData.columns,
					function(column) {
					    columns.push(column);
					}
				);

                this.getColumnModel().setConfig(columns);
            }

            this.el.unmask();
        }, this);

        this.store.load();
    }
});