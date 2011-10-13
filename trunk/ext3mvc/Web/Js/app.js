Ext.namespace('eddy.office');

eddy.office.app = function() {
    this.cachedModuls = [];
    this.userObj = new Object();
    this.JsToLoad = undefined;
    this.JsLoadCallBack = undefined;
    this.init();
}

Ext.extend(eddy.office.app, Ext.util.Observable, {
    init: function() {
        this.top = new Ext.Panel({
            region: 'north',
            height: 28,
            baseCls: 'x-panel-header',
            cls: 'topCls',
            layout: 'hbox',
            layoutConfig: { padding: '0', align: 'top' },
            margins: '0 0 3 0',
            items: [
				{ xtype: 'label', html: '<img class="icon-logo" src="../images/s.gif">管理系统', flex: 1 },
				{ id: 'userNameLabel', xtype: "label", html: '欢迎您,admin' }, { xtype: 'spacer', width: 5 },
				{ id: 'deptNameLabel', xtype: "label", html: '管理部' }
			]
        })

        this.MenuTreePanel = new eddy.office.MenuTreePanel({
            title: '导航',
            region: 'west',
            split: true,
            border: true,
            //collapseMode:'mini',//在分割线处出现按钮
            collapsible: true,
            collapsed: false,
            width: 180,
            minSize: 10,
            maxSize: 300,
            layout: { type: 'accordion', animate: true }
        });
        this.MenuTreePanel.on('menuClick', this.clickTree, this);
        this.MenuTreePanel.loadTree(this.MenuTreePanel, 0);

        this.mainTab = new Ext.TabPanel({
            region: 'center',
            activeTab: 0,
            plugins: new Ext.ux.TabCloseMenu(),
            items: [{ id: 'welcome-panel', title: 'API Home', html: 'home', iconCls: 'icon-docs', autoScroll: true}],
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
        if (!AdStatETheme || AdStatETheme == '') {
            AdStatETheme = '/ExtJS/resources/css/xtheme-blue.css';
        }

        Ext.Themes = [
			['默认主题', '/ExtJS/resources/css/xtheme-blue.css'],
			['灰色主题', '/ExtJS/resources/css/xtheme-gray.css'],
			['灰绿主题', '/ExtJS/resources/css/xtheme-tp.css'],
			['深紫主题', '/ExtJS/resources/css/xtheme-indigo.css'],
			['粉红主题', '/ExtJS/resources/css/xtheme-pink.css']
		];

        this.themesStore = new Ext.data.SimpleStore({
            fields: ['name', 'css'],
            data: Ext.Themes
        });

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
            window.location.reload();
        });

        this.btoolBar = new Ext.Toolbar({
            region: 'south',
            height: 27,
            layout: 'hbox',
            layoutConfig: {
                padding: '0',
                align: 'center'
            },
            defaults: { margins: '0 0 0 0' },
            items: [
				{
				    text: '退出系统',
				    iconCls: 'logout',
				    handler: function() {
				        Ext.MessageBox.confirm('提示', "确定退出登录？？", function(btn) {
				            if (btn != 'yes') {
				                return;
				            }

				            Ext.Ajax.request({
				                method: 'POST',
				                url: '/home/logoutAction',
				                success: function(resp) {
				                    var obj = Ext.util.JSON.decode(resp.responseText);
				                    if (obj.result == 'success') {
				                        window.location.href = '/index.html';
				                    }
				                    else {
				                        Ext.MessageBox.alert('报错了！！！', '错误！！！');
				                    }
				                }
				            })
				        });
				    }
				},
			    '-',
			    {
			        xtype: 'spacer',
			        flex: .5
			    },
	            '-',
	            {
	                xtype: 'label',
	                html: '<table border="0"><tr><td valign="middle">管理系统1.0</td></tr></table>'
	            },
	            '-',
	            {
	                xtype: 'spacer',
	                flex: .5
	            },
	            {
	                text: '收展',
	                iconCls: 'expand',
	                scope: this,
	                handler: function() {
	                    if (this.top.collapsed)
	                        myApp.top.expand();
	                    else
	                        this.top.collapse();
	                }
	            },
	            '-',
	            '-',
	            this.themesCombo
			]
        });

        var viewport = new Ext.Viewport({
            layout: 'border',
            items: [this.MenuTreePanel, this.mainTab, this.top, this.btoolBar]
        });
        this.loadMask = new Ext.LoadMask(this.mainTab.body)
    },

    toolAction: function(btn) {
    },

    changeTab: function(p, t) {
        if (!t)
            return;
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
        for (var i = 0; i < this.cachedModuls.length; i++) {
            if (this.cachedModuls[i].id == modulId)
                return this.cachedModuls[i].module;
        }
    },

    addModul: function(moduleStr, nodeAttr) {
        var moduleInstance = eval(moduleStr);
        this.mainTab.add({
            id: 'tab-' + nodeAttr.id,
            title: nodeAttr.text,
            iconCls: nodeAttr.cls,
            closable: true,
            layout: 'fit',
            items: [moduleInstance]
        }).show();
        moduleInstance.initMethod();
    },

    loadModel: function(nodeAttr, tab) {
        var n = this.mainTab.get('tab-' + nodeAttr.id);
        if (n) {
            this.mainTab.setActiveTab(n);
        }
        else {
            if (nodeAttr.type == 'iframe') {
                var itemPanel = new Ext.Panel({
                    id: 'tab-' + nodeAttr.id,
                    title: nodeAttr.text,
                    iconCls: nodeAttr.cls,
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
                    this.loadJs(jsFiles, function() {
                        var moduleStr = "new " + nodeAttr.mainClass + "();";
                        myApp.cachedModuls.push({ id: nodeAttr.id, module: moduleStr });
                        myApp.addModul(moduleStr, nodeAttr);
                        myApp.loadMask.hide();
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
                    this.loadJs(jsFiles, function() {
                        var moduleStr = "new " + nodeAttr.mainClass + "();";
                        myApp.cachedModuls.push({ id: nodeAttr.id, module: moduleStr });
                        myApp.addModul(moduleStr, nodeAttr);
                        myApp.loadMask.hide();
                    });
                }
            } else if (nodeAttr.type == 'load') {
                this.loadMask.show();
                var autoLoad = { url: nodeAttr.url };
                var itemPanel = this.mainTab.add(new Ext.Panel({
                    id: 'tab-' + nodeAttr.id,
                    title: nodeAttr.text,
                    iconCls: nodeAttr.cls,
                    autoLoad: autoLoad,
                    closable: true,
                    layout: 'fit'
                }));
                this.mainTab.setActiveTab(itemPanel);
                myApp.loadMask.hide();
            }
        }
    },

    loadJs: function(js, callback) {
        myApp.JsToLoad = js;
        myApp.JsLoadCallBack = callback;
        myApp._loadJs();
    },

    _loadJs: function() {
        var js = myApp.JsToLoad;
        var callback = myApp.JsLoadCallBack;
        if (Ext.type(myApp.JsToLoad) != 'string') {
            if (myApp.JsToLoad.length == 1) {
                js = myApp.JsToLoad[0];
                callback = myApp.JsLoadCallBack;
            }
            else {
                js = myApp.JsToLoad.shift();
                callback = myApp._loadJs;
            }
        }

        Ext.Ajax.request({
            url: js,
            success: myApp._onLoadJs,
            method: 'GET',
            scope: callback
        });
    },

    _onLoadJs: function(response) {
        eval(response.responseText);
        this();
    },

    loadUserInfo: function() {
        Ext.Ajax.request({
            method: 'post',
            url: '/home/LoginAction',
            params: '',
            scope: this,
            success: function(resp) {
                var obj = Ext.util.JSON.decode(resp.responseText);
                Ext.apply(this.userObj, obj);
                Ext.getCmp('userNameLabel').getEl().update('欢迎您,' + this.userObj.realName);
                Ext.getCmp('deptNameLabel').getEl().update(this.userObj.userDeptName);
            }
        });
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

Ext.onReady(function() {
    Ext.QuickTips.init();
    myApp = new eddy.office.app();
    myApp.loadUserInfo();

    window.addTab = function(item) {
        myApp.addTab(item);
    }
});
