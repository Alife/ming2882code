mc.frame.app = function() { this.userObj = new Object(); this.init(); }
Ext.extend(mc.frame.app, Ext.util.Observable, {
    init: function() {
        this.top = new Ext.Panel({
            region: 'north', collapseMode: 'mini', height: 28, baseCls: 'x-panel-header', cls: 'topCls', layoutConfig: { padding: '0', align: 'top' }, margins: '0 0 3 0',
            items: [
				{ xtype: 'label', html: '<img class="logo" src="../images/s.gif">管理系统', style: 'float:left;display:block;' },
				{ id: 'welcomeLabel', xtype: "label", html: '欢迎您,admin 管理部', style: 'float:right;display:block;' }
			]
        })
        this.MenuTreePanel = new mc.frame.MenuTreePanel({
            title: '导航', region: 'west', split: true, border: true, collapseMode: 'mini', /*在分割线处出现按钮*/collapsible: true, collapsed: false, width: 180, minSize: 10, maxSize: 300,
            layout: { type: 'accordion', animate: true }
        });
        this.MenuTreePanel.on('menuClick', this.clickTree, this);
        this.MenuTreePanel.loadTree(this.MenuTreePanel, 0);

        this.mainTab = new Ext.TabPanel({
            region: 'center', activeTab: 0, plugins: new Ext.ux.TabCloseMenu(), items: [{ id: 'welcome-panel', title: 'API Home', html: 'home', iconCls: 'homemanage', autoScroll: true}],
            initEvents: function() {
                Ext.TabPanel.superclass.initEvents.call(this);
                //this.on('add', this.onAdd, this, { target: this });
                this.on('remove', this.onRemove, this, { target: this });
                this.mon(this.strip, 'mousedown', this.onStripMouseDown, this);
                this.mon(this.strip, 'contextmenu', this.onStripContextMenu, this);
                if (this.enableTabScroll) {
                    this.mon(this.strip, 'mousewheel', this.onWheel, this);
                }
                this.mon(this.strip, 'dblclick', this.onTitleDbClick, this);
            },
            onTitleDbClick: function(e, target, o) {
                var t = this.findTargets(e);
                if (t.item.fireEvent('beforeclose', t.item) !== false && t.item.closable) {
                    t.item.fireEvent('close', t.item);
                    this.remove(t.item);
                }
            }
        });
        this.mainTab.on('tabchange', this.changeTab, this);
        var cp = new Ext.state.CookieProvider();
        var AdStatETheme = cp.get("AdStatEThemeCSS");
        if (!AdStatETheme || AdStatETheme == '')
            AdStatETheme = '/compress/cachecontent/blue/css?version=1.0';
        Ext.Themes = [
			['默认主题', '/compress/cachecontent/blue/css?version=1.0'],
			['灰色主题', '/compress/cachecontent/gray/css?version=1.0'],
			['灰绿主题', '/compress/cachecontent/tp/css?version=1.0'],
			['深紫主题', '/compress/cachecontent/indigo/css?version=1.0'],
			['粉红主题', '/compress/cachecontent/pink/css?version=1.0']
		];
        this.themesStore = new Ext.data.SimpleStore({ fields: ['name', 'css'], data: Ext.Themes });
        this.themesCombo = new Ext.form.ComboBox({
            id: 'ThemeCombId',
            store: this.themesStore,
            width: 90,
            valueField: 'css',
            displayField: 'name',
            value: AdStatETheme,
            mode: 'local',
            fieldLabel: '主题',
            typeAhead: true,
            editable: false,
            triggerAction: 'all',
            selectOnFocus: true
        });
        this.themesCombo.on('select', function(c, re, index) {
            Ext.util.CSS.swapStyleSheet('theme', c.getValue());
            cp.set("AdStatEThemeCSS", c.getValue());
        });
        this.btoolBar = new Ext.Toolbar({
            region: 'south', height: 27, layout: 'hbox', layoutConfig: { padding: '0', align: 'center' }, defaults: { margins: '0 0 0 0' },
            items: [
				{
				    text: '退出系统', iconCls: 'logout',
				    handler: function() {
				        Ext.MessageBox.confirm('系统提示', "确定退出登录？？", function(btn) {
				            if (btn != 'yes')
				                return;
				            mc.frame.ajax({ url: '/home/logout', scope: this, method: 'get', onSuccess: function(rs, opts) { window.location.reload(); } });
				        });
				    }
				}, '-', { xtype: 'spacer', flex: .5 }, '-',
			    { xtype: 'label', html: '<table border="0"><tr><td valign="middle">管理系统1.0</td></tr></table>' }, '-', { xtype: 'spacer', flex: .5 },
	            {
	                text: '收缩',
	                iconCls: 'application_side_contract',
	                scope: this,
	                handler: function(b) {
	                    if (this.top.collapsed) { this.top.expand(); this.MenuTreePanel.expand(); b.setText('收缩'); b.setIconClass('application_side_contract'); }
	                    else { this.top.collapse(); this.MenuTreePanel.collapse(); b.setText('展开'); b.setIconClass('application_side_expand'); }
	                }
	            }, '-', '-', this.themesCombo
			]
        });
        var viewport = new Ext.Viewport({ layout: 'border', items: [this.MenuTreePanel, this.mainTab, this.top, this.btoolBar] });
        this.loadMask = new Ext.LoadMask(this.mainTab.body)
    },
    changeTab: function(p, t) {
        if (!t) return;
        this.MenuTreePanel.selectTreeNode(p, t);
    },
    clickTree: function(nodeAttr) {
        if (!nodeAttr.leaf) return false;
        var id = 'tab-' + nodeAttr.id;
        var tab = Ext.getCmp(id);
        if (!tab) {
            this.mainTab.setActiveTab(tab);
            this.loadModel(nodeAttr, tab);
        } else {
            this.mainTab.setActiveTab(tab);
        }
    },
    findCachedModul: function(modulId) {
        for (var i = 0; i < mc.frame.cachedModuls.length; i++) {
            if (mc.frame.cachedModuls[i].id == modulId)
                return mc.frame.cachedModuls[i].module;
        }
    },
    addModul: function(moduleStr, nodeAttr) {
        var moduleInstance = eval(moduleStr);
        this.mainTab.add({ id: 'tab-' + nodeAttr.id, title: nodeAttr.text, iconCls: nodeAttr.iconCls, items: [moduleInstance], closable: true, layout: 'fit' }).show();
        moduleInstance.initMethod();
    },
    loadModel: function(nodeAttr, tab) {
        var n = this.mainTab.get('tab-' + nodeAttr.id);
        if (n) this.mainTab.setActiveTab(n);
        else {
            if (nodeAttr.type == 'iframe') {
                var itemPanel = new Ext.Panel({
                    id: 'tab-' + nodeAttr.id,
                    title: nodeAttr.text,
                    iconCls: nodeAttr.iconCls,
                    closable: true,
                    layout: 'fit',
                    html: '<iframe scrolling="auto" frameborder="0" width="100%" height="100%" src="' + nodeAttr.url + '"></iframe>'
                });
                this.mainTab.add(itemPanel).show();
            }
            else if (nodeAttr.type == 'loadjs') {
                var moduleStr = this.findCachedModul(nodeAttr.id);
                if (moduleStr) {
                    this.addModul(moduleStr, nodeAttr);
                }
                else {
                    this.loadMask.show();
                    Ext.namespace(nodeAttr.namespace1);
                    var jsFiles = nodeAttr.url.split(';');
                    var me = this;
                    mc.frame.loadJs(jsFiles, function() {
                        var moduleStr = "new " + nodeAttr.mainClass + "();";
                        mc.frame.cachedModuls.push({ id: nodeAttr.id, module: moduleStr });
                        me.addModul(moduleStr, nodeAttr);
                        me.loadMask.hide();
                    });
                }
            }
            else if (nodeAttr.type == 'jsclass') {
                var moduleStr = this.findCachedModul(nodeAttr.id);
                if (moduleStr) {
                    this.addModul(moduleStr, nodeAttr);
                }
                else {
                    this.loadMask.show();
                    Ext.namespace(nodeAttr.namespace1);
                    var jsFiles = nodeAttr.jsUrl.split(';');
                    var me = this;
                    mc.frame.loadJs(jsFiles, function() {
                        var moduleStr = "new " + nodeAttr.mainClass + "();";
                        mc.frame.cachedModuls.push({ id: nodeAttr.id, module: moduleStr });
                        me.addModul(moduleStr, nodeAttr);
                        me.loadMask.hide();
                    });
                }
            } else if (nodeAttr.type == 'load') {
                this.loadMask.show();
                var autoLoad = { url: nodeAttr.url };
                var itemPanel = this.mainTab.add(new Ext.Panel({
                    id: 'tab-' + nodeAttr.id,
                    title: nodeAttr.text,
                    iconCls: nodeAttr.iconCls,
                    autoLoad: autoLoad,
                    closable: true,
                    layout: 'fit'
                }));
                this.mainTab.setActiveTab(itemPanel);
                this.loadMask.hide();
            }
        }
    },
    loadUserInfo: function(obj) {
        Ext.apply(this.userObj, obj);
        Ext.getCmp('welcomeLabel').getEl().update('欢迎您,' + this.userObj.UserName + ' ' + this.userObj.DeptID);
    },
    addTab: function(item) {
        var n = this.mainTab.get(item.id);
        if (n) this.mainTab.setActiveTab(n);
        else this.mainTab.add(item).show();
    },
    findTab: function(id) {
        return this.mainTab.get(id);
    }
});