Ext.BLANK_IMAGE_URL = "/ext/resources/images/default/s.gif";
Ext.QuickTips.init();
Ext.form.Field.prototype.msgTarget = 'side';
var Tree = Ext.tree;
var clock = new Ext.Toolbar.TextItem('');
var footToolBars = new Ext.Toolbar({
    border: false, x: 0, y: 0, id: "footToolBars",
    listeners: {
        'render': {
            fn: function() {
                Ext.TaskMgr.start({
                    run: function() { Ext.fly(clock.getEl()).update(new Date().format('G:i:s A')); }, interval: 1000
                });
            }
        }
    }
});
if (!Ext.grid.GridView.prototype.templates) {
    Ext.grid.GridView.prototype.templates = {};
}
Ext.grid.GridView.prototype.templates.cell = new Ext.Template(
   '<td class="x-grid3-col x-grid3-cell x-grid3-td-{id} x-selectable {css}" style="{style}" tabIndex="0" {cellAttr}>',
   '<div class="x-grid3-cell-inner x-grid3-col-{id}" {attr}>{value}</div>',
   '</td>'
);
var panel_footToolbar = new Ext.Panel({ border: false, x: 0, y: 0, items: [footToolBars] });
var panel_south = new Ext.Panel({
    id: "panel_south", region: "south", height: 24, frame: false, border: false, margins: '0 0 0 0',
    items: [panel_footToolbar]
});
var headToolBars = new Ext.Toolbar({ border: false, x: 0, y: 0, id: "headToolBars" });
var panel_headToolbar = new Ext.Panel({ border: false, x: 0, y: 0, items: [headToolBars] });
var panel_north = new Ext.Panel({
    id: "panel_north", region: "north", height: 25, frame: false, border: false, margins: '0 0 0 0',
    items: [panel_headToolbar]
});
var panel_west = new Ext.Panel({ id: 'west_panel', region: 'west', title: '导航菜单', split: true, width: 130, minSize: 130, maxSize: 400, collapsible: true, layout: { type: 'accordion', animate: true} });
var center = null;
var userInfo = {};
var insInfo = {};
var typeInfo = {};
Ext.onReady(function() {
    setTimeout(function() {
        Ext.get('loading').remove();
        Ext.get('loading-mask').fadeOut({ remove: true });
    }, 500);
    var conn = Ext.lib.Ajax.getConnectionObject().conn;
    conn.open("POST", '/user/getuser', false);
    conn.send(null);
    userInfo = Ext.util.JSON.decode(conn.responseText);
    if (!userInfo) {
        Ext.MessageBox.alert("系统提示!", "你还没有登录!");
        return false;
    }
    if (userInfo.DepartmentID) {
        conn.open("POST", '/baseset/getdept/' + userInfo.DepartmentID, false);
        conn.send(null);
        insInfo = Ext.util.JSON.decode(conn.responseText);
    }
    if (userInfo.TypeID) {
        conn.open("POST", '/baseset/getusertype/' + userInfo.TypeID, false);
        conn.send(null);
        typeInfo = Ext.util.JSON.decode(conn.responseText);
    }
    conn.open("POST", '/report/getmaintotol/', false);
    conn.send(null);
    var maintotol = Ext.util.JSON.decode(conn.responseText);
    Ext.state.Manager.setProvider(new Ext.state.CookieProvider());
    center = new Ext.TabPanel({
        region: 'center', id: "TabPanelID", layoutOnTabChange: true, enableTabScroll: true, defaults: { autoScroll: true }, activeTab: 0,
        plugins: new Ext.ux.TabCloseMenu(),
        items: [{
            id: 'home', xtype: "panel", title: "欢迎使用", border: false, iconCls: 'icontab icon-home',
            //html: "<iframe scrolling='no' width='100%' height='100%'  frameborder='0' src=''></iframe>"
            items: [new Ext.Panel({ title: "信息提示", border: false, plain: true, layout: 'column',
                tbar: ["&nbsp;欢迎您:", { xtype: "displayfield", value: userInfo.TrueName + "(" + userInfo.UserCode + ")！" }, "最后登录时间:" + new Date(userInfo.LoginTime).format('Y-m-d H:m:s')],
                items: [
                    { columnWidth: .25, baseCls: 'x-plain', bodyStyle: 'padding:5px 0 5px 5px', hidden: !(userInfo.ID == 1 || typeInfo.Type == 6),
                        items: [{
                            title: '校图统计',
                            html: '<ul class="maintotol">\
                                        <li>校图信息:<span title="点击校图">' + maintotol.proof + '条记录</span></li>\
                                   </ul>'
                        }
                        ]
                    },
                    { columnWidth: .25, baseCls: 'x-plain', bodyStyle: 'padding:5px 0 5px 5px', hidden: !(typeInfo.Type == 1 || typeInfo.Type == 2),
                        items: [{
                            title: '美工信息统计',
                            html: '<ul class="maintotol">\
                                        <li>工作单信息:<span title="点击查看工作单">' + maintotol.worker + '条记录</span></li>\
                                        <li>修图信息:<span title="点击查看问题">' + maintotol.editphoto + '条记录</span></li>\
                                   </ul>'
                        }
                        ]
                    },
                    { columnWidth: .25, baseCls: 'x-plain', bodyStyle: 'padding:5px 0 5px 5px', hidden: !(typeInfo.Type == 1),
                        items: [{
                            title: '制程信息统计',
                            html: '<ul class="maintotol">\
                                        <li>制程信息:<span title="点击查看制程">' + maintotol.kit + '条记录</span></li>\
                                        <li>完程制程信息:<span title="点击查看完成制程">' + maintotol.kitend + '条记录</span></li>\
                                   </ul>'
                        }
                        ]
                    },
                    { columnWidth: .25, baseCls: 'x-plain', bodyStyle: 'padding:5px 0 5px 5px',
                        items: [{
                            title: '文档下载',
                            html: '<ul class="maintotol">\
                                        <li><a href="/images/proof.doc" target="blank">校图流程文件</a></li>\
                                        <li><a href="/images/proofcon.doc" target="blank">線上校稿套圖完成確認單</a></li>\
                                   </ul>'
                        }
                        ]
                    }
                ]
            })]
        }
    ]
    });
    var viewport = new Ext.Viewport({
        layout: 'border',
        items: [panel_north, panel_south, panel_west, center]
    });
    footToolBars.addText({
        xtype: "label", text: "迪士尼童话相册管理系统"
    })
    footToolBars.addFill();
    footToolBars.addSeparator();
    footToolBars.addText("推荐分辨率:1024*768以上");
    footToolBars.addSeparator();
    footToolBars.addItem(clock)
    panel_south.doLayout();
    headToolBars.addText({
        xtype: "label", text: "迪士尼童话相册管理系统", id: "logo", cls: 'icon-logo'
    })
    headToolBars.addFill();
    if (userInfo.DepartmentID) {
        if (typeInfo.Type == 6)
            headToolBars.addText(String.format('欢迎您:<u>{0}({1})</u>！</u>', userInfo.TrueName, userInfo.UserCode));
        else
            headToolBars.addText(String.format('欢迎您:<u>{0}({1})</u>！您是<u>{2}</u>的<u>{3}</u>', userInfo.TrueName, userInfo.UserCode, insInfo.Name, userInfo.userinfo.Duty));
    }
    else
        headToolBars.addText(String.format('欢迎您:<u>{0}({1})</u>！您是<u>{2}</u>', userInfo.TrueName, userInfo.UserCode, typeInfo.Name));
    headToolBars.addSeparator();
    headToolBars.addButton({ text: '退出系统',
        handler: function() {
            Ext.Ajax.request({
                url: '/user/logout',
                success: function(response, options) {
                    var temp = Ext.util.JSON.decode(response.responseText);
                    if (temp.success)
                        window.location.href = '/';
                    else
                        Ext.Msg.alert("系统提示!", temp.msg);
                }
            });
        }
    });
    headToolBars.addSeparator();
    headToolBars.addItem(new makeCookie().cbThemes);
    panel_north.doLayout();

    conn.open("POST", '/sys/userapp?id=0', false);
    conn.send(null);
    var respText = Ext.util.JSON.decode(conn.responseText);
    if (respText.length == 0) {
        location.href = '/member';
    }
    Ext.each(respText, function(item) {
        conn.open("POST", '/sys/userapp?id=' + item.ID, false);
        conn.send(null);
        var children = Ext.util.JSON.decode(conn.responseText);
        var root = new Tree.TreeNode({ id: 'root_' + item.ID, disable: true });
        Ext.each(children, function(citem) {
            root.appendChild(new Ext.tree.TreeNode({
                id: "node_" + citem.ID, text: citem.Name, iconCls: citem.Icon, href: 'javascript:;',
                Url: '/js/exts/' + citem.Url + '.js', Icon: 'icontab ' + citem.Icon, Code: citem.Code, leaf: true,
                listeners: { "click": function(node, e) { if (node.isLeaf()) { e.stopEvent(); ALLEvents(node); } } }
            }));
        });
        var tree = new Ext.tree.TreePanel({ rootVisible: false, border: false, root: root });
        var tmp = new Ext.Panel({ id: "pannel_" + item.ID, title: item.Name, iconCls: 'iconmenu ' + item.Icon, border: false, autoWidth: true, items: [tree] });
        panel_west.add(tmp);
        panel_west.doLayout();
    });
});
function GridMain(node, grid) {
    var tab = center.getComponent(node.id);
    if (!tab) {
        var tab = center.add({ id: node.id, title: node.text, iconCls: node.attributes.Icon, xtype: "panel", layout: "fit", closable: true, items: [grid] });
    } else {
        tab.removeAll();
        tab.add(grid);
        tab.doLayout();
    }
    center.setActiveTab(tab);
}
function ALLEvents(node) {
    //var tab = center.getComponent(node.id);
    //if (!tab)
    jsload(node.attributes.Url, node.attributes.Code, node);
    //else {
    //    center.setActiveTab(tab);
    //tab.get(0).doLayout();
    //}
}
function GetCategoryPath(path) {
    var str = '';
    path -= 1;
    if (path != 0) {
        for (var i = 0; i < path; i++)
            str += "　";
    }
    return str;
}
function ForDight(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}
var now = new Date();                    //当前日期      
var nowMonth = now.getMonth();           //当前月      
var nowYear = now.getYear();             //当前年
nowYear += (nowYear < 2000) ? 1900 : 0;  //
function formatDate(date) {
    var myyear = date.getFullYear();
    var mymonth = date.getMonth() + 1;
    var myweekday = date.getDate();
    if (mymonth < 10) {
        mymonth = "0" + mymonth;
    }
    if (myweekday < 10) {
        myweekday = "0" + myweekday;
    }
    return (myyear + "-" + mymonth + "-" + myweekday);
}
//获得某月的天数      
function getMonthDays(myMonth) {
    var monthStartDate = new Date(nowYear, myMonth, 1);
    var monthEndDate = new Date(nowYear, myMonth + 1, 1);
    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
    return days;
}
//获得本月的开始日期      
function getMonthStartDate() {
    var monthStartDate = new Date(nowYear, nowMonth, 1);
    return formatDate(monthStartDate);
}
//获得本月的结束日期      
function getMonthEndDate() {
    var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowMonth));
    return formatDate(monthEndDate);
}
var beginTime = getMonthStartDate();
var endTime = getMonthEndDate();
function GetIsLevel(appcode, opcode) {
    var conn = Ext.lib.Ajax.getConnectionObject().conn;
    conn.open("POST", String.format('/sys/useroperation?appcode={0}&opcode={1}', appcode, opcode), false);
    conn.send(null);
    var respText = Ext.util.JSON.decode(conn.responseText);
    if (respText)
        return !respText.success;
    return true;
}
Ext.util.Format.comboRenderer = function(combo) {
    return function(value) {
        var record = combo.findRecord(combo.valueField, value);
        return record
                ? record.get(combo.displayField)
                : combo.valueNotFoundText;
    }
}
function setformitem(form, win, type) {
    if (type == 1) {
        form.getForm().items.eachKey(function(key, item) {
            if (item.getXType() == 'textfield' || item.getXType() == 'datefield')
                item.el.dom.readOnly = true;
            else if (item.getXType() == 'combo')
                item.readOnly = true;
            else if (item.getXType() == 'checkbox')
                item.setDisabled(true);
        });
        win.buttons[0].setDisabled(true);
    } else {
        form.getForm().items.eachKey(function(key, item) {
            if (item.getXType() == 'textfield' || item.getXType() == 'datefield')
                item.el.dom.readOnly = false;
            else if (item.getXType() == 'combo')
                item.readOnly = false;
            else if (item.getXType() == 'checkbox')
                item.setDisabled(false);
        });
        win.buttons[0].setDisabled(false);
    }
}
Ext.override(Ext.data.Store, {
    addField: function(field) {
        field = new Ext.data.Field(field);
        this.recordType.prototype.fields.replace(field);
        if (typeof field.defaultValue != 'undefined') {
            this.each(function(r) {
                if (typeof r.data[field.name] == 'undefined') {
                    r.data[field.name] = field.defaultValue;
                }
            });
        }
    },
    removeField: function(name) {
        this.recordType.prototype.fields.removeKey(name);
        this.each(function(r) {
            delete r.data[name];
            if (r.modified) {
                delete r.modified[name];
            }
        });
    }
});
Ext.override(Ext.grid.ColumnModel, {
    addColumn: function(column, colIndex) {
        if (typeof column == 'string') {
            column = { header: column, dataIndex: column };
        }
        var config = this.config;
        this.config = [];
        if (typeof colIndex == 'number') {
            config.splice(colIndex, 0, column);
        } else {
            colIndex = config.push(column);
        }
        this.setConfig(config);
        return colIndex;
    },
    removeColumn: function(colIndex) {
        var config = this.config;
        this.config = [config[colIndex]];
        config.splice(colIndex, 1);
        this.setConfig(config);
    }
});
Ext.override(Ext.grid.GridPanel, {
    addColumn: function(field, column, colIndex) {
        if (!column) {
            if (field.dataIndex) {
                column = field;
                field = field.dataIndex;
            } else {
                column = field.name || field;
            }
        }
        this.store.addField(field);
        return this.colModel.addColumn(column, colIndex);
    },
    removeColumn: function(name, colIndex) {
        this.store.removeField(name);
        if (typeof colIndex != 'number') {
            colIndex = this.colModel.findColumnIndex(name);
        }
        if (colIndex >= 0) {
            this.colModel.removeColumn(colIndex);
        }
    }
});