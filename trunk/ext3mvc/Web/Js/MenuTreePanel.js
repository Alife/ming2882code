//Ext.namespace("eddy.office.MenuTreePanel");
eddy.office.MenuTreePanel = function(config) {
    Ext.apply(this, {
        items: []
    });

    eddy.office.MenuTreePanel.superclass.constructor.apply(this, arguments);

    this.addEvents('menuClick');
    this.initMenuEvent();
};

Ext.extend(eddy.office.MenuTreePanel, Ext.Panel, {

    initMenuEvent: function() {
    },

    loadMenuTree: function() {
    },

    onRender: function() {
        eddy.office.MenuTreePanel.superclass.onRender.apply(this, arguments);
        this.loadMenuTree();
    },

    loadTree: function(instanceTree, sysMenuId) {
        instanceTree.removeAll();

        Ext.Ajax.request({
            method: 'post',
            url: '/home/getUserButtons',
            params: { sysMenuId: sysMenuId },
            success: function(resp) {
                var obj = Ext.util.JSON.decode(resp.responseText);
                createLeftOpPanel(obj);
                instanceTree.doLayout();
            },
            fail: function() {
            }
        });

        function createLeftOpPanel(obj) {
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].actionPath != '') {
                    instanceTree.add({
                        title: obj[i].menuName
                    });
                }
                else {
                    var menuTree = createMenuTree(obj[i].nodeId, obj[i].menuName);
                    instanceTree.add({
                        title: obj[i].menuName,
                        iconCls: 'default_menu',
                        layout: "fit", border: false,
                        items: [menuTree],
                        listeners: {
                            'activate': function(obj) {
                                pName = obj.title;
                            }
                        }
                    });
                }
            }
        };

        function createMenuTree(menuId, menuName) {
            var menuRoot = new Ext.tree.AsyncTreeNode({
                id: menuId,
                text: menuName
            });

            var menuTree = new Ext.tree.TreePanel({
                rootVisible: false,
                autoScroll: true,
                bodyStyle: "background-color:#FDFDFD; border-width: 0px 0px 0px 0px;",
                loader: new Ext.tree.TreeLoader({ dataUrl: '/home/getUserTree', baseParams: { parantNodeId: menuId, menuName: menuName} })
            });

            menuTree.setRootNode(menuRoot);

            menuTree.addListener("click", function(node, event) {
                instanceTree.fireEvent('menuClick', node.attributes);
            });

            return menuTree;
        };

        function createTreeNode(id, pName, nodeName, nodeAction, jsUrl, icon) {
            var node = new Ext.tree.TreeNode({
                id: id,
                text: nodeName,
                jsUrl: jsUrl,
                url: nodeAction,
                panelClass: 'eddy.office.UserInfoPanel',
                listeners: {
                    "click": function(node, event) {
                        instanceTree.fireEvent('menuClick', node.attributes);
                    }
                }
            });
            return node;
        };

        function addDevItem() {
            var devmenuTree = new Ext.tree.TreePanel({
                rootVisible: false
            });

            var devMenuRoot = new Ext.tree.TreeNode({
                id: 0,
                text: ''
            });
            devmenuTree.setRootNode(devMenuRoot);

            instanceTree.add({
                title: '开发功能',
                iconCls: 'default_menu',
                layout: "fit",
                items: [
					 devmenuTree
				]
            });

            var node2 = createTreeNode(987, '开发功能', '资源管理', '../basic/resources.html', '', '../images/menu.gif');
            devMenuRoot.appendChild(node2);
        }
    },

    selectTreeNode: function(mainpane, tab) {
        var id = tab.id;
        if (this.layout.activeItem) {
            var tree = this.layout.activeItem.items.items[0];
            var node = tree.getNodeById(id);
            if (node) {
                tree.getSelectionModel().select(node);
            }
            else {
                tree.getSelectionModel().clearSelections();
            }
        }
    }
});

