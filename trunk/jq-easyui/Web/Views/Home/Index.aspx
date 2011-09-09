<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Index</title>
    <%string path = ""; %>
    <link rel="stylesheet" type="text/css" href="<%= path%>/js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="<%= path%>/js/themes/default/css/default.css" />

    <script src="<%= path%>/js/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="<%= path%>/js/jquery.easyui-1.2.3.min.js" type="text/javascript"></script>

    <script src="<%= path%>/js/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <script src="<%= path%>/js/main.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _menus = { "menus": [
						{ "menuid": "1", "icon": "icon-sys", "menuname": "控件使用",
						    "menus": [
									{ "menuid": "13", "menuname": "用户管理", "icon": "icon-users", "url": "/demo/窗体.html" },
									{ "menuid": "14", "menuname": "角色管理", "icon": "icon-role", "url": "/demo/表单.html" },
									{ "menuid": "15", "menuname": "权限设置", "icon": "icon-set", "url": "/demo/数据网格.html" },
									{ "menuid": "16", "menuname": "系统日志", "icon": "icon-log", "url": "/data/combobox_data" }
								]
						}, { "menuid": "8", "icon": "icon-sys", "menuname": "员工管理",
						    "menus": [{ "menuid": "21", "menuname": "员工列表", "icon": "icon-nav", "url": "demo.html" },
									{ "menuid": "22", "menuname": "视频监控", "icon": "icon-nav", "url": "demo1.html" }
								]
						}, { "menuid": "56", "icon": "icon-sys", "menuname": "部门管理",
						    "menus": [{ "menuid": "31", "menuname": "添加部门", "icon": "icon-nav", "url": "demo1.html" },
									{ "menuid": "32", "menuname": "部门列表", "icon": "icon-nav", "url": "demo2.html" }
								]
						}, { "menuid": "28", "icon": "icon-sys", "menuname": "财务管理",
						    "menus": [{ "menuid": "41", "menuname": "收支分类", "icon": "icon-nav", "url": "demo.html" },
									{ "menuid": "42", "menuname": "报表统计", "icon": "icon-nav", "url": "demo1.html" },
									{ "menuid": "43", "menuname": "添加支出", "icon": "icon-nav", "url": "demo2.html" }
								]
						}, { "menuid": "39", "icon": "icon-sys", "menuname": "商城管理",
						    "menus": [{ "menuid": "51", "menuname": "商品分类", "icon": "icon-nav", "url": "demo.html" },
									{ "menuid": "52", "menuname": "商品列表", "icon": "icon-nav", "url": "demo1.html" },
									{ "menuid": "53", "menuname": "商品订单", "icon": "icon-nav", "url": "demo2.html" }
								]
						}
				]
        };
        //设置登录窗口
        function openPwd() {
            $('#w').window({
                title: '修改密码',
                width: 300,
                modal: true,
                shadow: true,
                closed: true,
                height: 160,
                resizable: false
            });
        }
        //关闭登录窗口
        function closePwd() {
            $('#w').window('close');
        }
        //修改密码
        function serverLogin() {
            var $newpass = $('#txtNewPass');
            var $rePass = $('#txtRePass');
            if ($newpass.val() == '') {
                msgShow('系统提示', '请输入密码！', 'warning');
                return false;
            }
            if ($rePass.val() == '') {
                msgShow('系统提示', '请在一次输入密码！', 'warning');
                return false;
            }
            if ($newpass.val() != $rePass.val()) {
                msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
                return false;
            }
            $.post('/ajax/editpassword.ashx?newpass=' + $newpass.val(), function(msg) {
                msgShow('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + msg, 'info');
                $newpass.val('');
                $rePass.val('');
                close();
            })
        }

        $(function() {
            openPwd();
            $('#editpass').click(function() { $('#w').window('open'); });
            $('#btnEp').click(function() { serverLogin(); });
            $('#btnCancel').click(function() { closePwd(); })
            $('#loginOut').click(function() {
                $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function(r) {
                    if (r) { location.href = '/sys/logout'; }
                });
            })
            $('#kk6').datagrid({
                //title:'列表区域',
                url: '/data/datagrid_data',
                headerCls: "header_cls",
                nowrap: false,
                striped: true,
                fit: true,
                border: false,
                sortName: 'code',
                sortOrder: 'desc',
                idField: 'code',
                fitColumns: true,
                rownumbers: true,
                pagination: true,
                pageSize: 10,
                pageList: [10, 20, 50, 100],
                frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: '编码', field: 'code', align: 'center', width: $(this).width() * 0.1, sortable: true }
				]],
                columns: [[
			        { title: '基本信息', colspan: 3 },
					{ field: 'opt', title: '操作', width: $(this).width() * 0.1, align: 'center', rowspan: 2,
					    formatter: function(value, rec) {
					        return '<span style="color:red">Edit Delete</span>';
					    }
					}
				], [
					{ field: 'name', title: '姓名', align: 'center', width: $(this).width() * 0.16 },
					{ field: 'addr', title: '地址', align: 'center', width: $(this).width() * 0.16, rowspan: 2, sortable: true },
					{ field: 'col4', title: '代号', align: 'center', width: $(this).width() * 0.16, rowspan: 2 }
				]],
                toolbar: [{
                    id: 'btnadd',
                    text: '新增组名',
                    iconCls: 'icon-group-add',
                    handler: function() {
                        $('#btnsave').linkbutton('enable');
                        alert('add');
                    }
                }, '-', {
                    id: 'btncut',
                    text: '分组编辑',
                    iconCls: 'icon-group-edit',
                    handler: function() {
                        $('#btnsave').linkbutton('enable');
                        alert('cut');
                    }
                }, '-', {
                    id: 'btnsave',
                    text: '删除组',
                    iconCls: 'icon-group-delete',
                    handler: function() {
                        $('#btnsave').linkbutton('disable');
                        alert('save');
                    }
                }, '->', {
                    id: 'btn_import',
                    text: '导入',
                    iconCls: 'icon-import',
                    handler: function() {
                        $('#btnsave').linkbutton('disable');
                        alert('save');
                    }
                }, '-', {
                    id: 'btn_add',
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function() {
                        $('#btnsave').linkbutton('disable');
                        alert('save');
                    }
                }
                ]
            });
            //            $('#kk6').datagrid('loadData', {
            //                "total": 239,
            //                "rows": [
            //		            { "code": "001", "name": "Name 1", "addr": "Address 11", "col4": "col4 data" },
            //		            { "code": "002", "name": "Name 2", "addr": "Address 13", "col4": "col4 data" },
            //		            { "code": "003", "name": "Name 3", "addr": "Address 87", "col4": "col4 data" },
            //		            { "code": "004", "name": "Name 4", "addr": "Address 63", "col4": "col4 data" },
            //		            { "code": "005", "name": "Name 5", "addr": "Address 45", "col4": "col4 data" },
            //		            { "code": "006", "name": "Name 6", "addr": "Address 16", "col4": "col4 data" },
            //		            { "code": "007", "name": "Name 7", "addr": "Address 27", "col4": "col4 data" },
            //		            { "code": "008", "name": "Name 8", "addr": "Address 81", "col4": "col4 data" },
            //		            { "code": "009", "name": "Name 9", "addr": "Address 69", "col4": "col4 data" },
            //		            { "code": "010", "name": "Name 10", "addr": "Address 78", "col4": "col4 data"}]
            //            });
        });    </script>

</head>
<body class="easyui-layout" style="overflow-y: hidden" scroll="no">
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px;
            width: 100%; background: white; text-align: center;">
            <img src="images/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div region="north" split="true" border="false" style="overflow: hidden; height: 30px;
        background: url(images/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%;
        line-height: 20px; color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <span style="float: right; padding-right: 20px;" class="head">欢迎 疯狂秀才 <a href="#"
            id="editpass">修改密码</a> <a href="#" id="loginOut">安全退出</a></span> <span style="padding-left: 10px;
                font-size: 16px;">
                <img src="images/blocks.gif" width="20" height="20" align="absmiddle" />
                jQuery.EasyUI- 1.2.2 应用实例</span>
    </div>
    <div region="south" split="true" style="height: 30px; text-align: center">
        <div class="footer">
            copy@2011</div>
    </div>
    <div region="west" hide="true" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div id="nav" class="easyui-accordion" fit="true" border="false">
            <!--  导航内容 -->
        </div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <div title="欢迎使用">
                <table id="kk6">
                </table>
            </div>
        </div>
    </div>
    <!--修改密码窗口-->
    <div id="w" class="easyui-window" title="修改密码" collapsible="false" minimizable="false"
        maximizable="false" icon="icon-save" style="width: 300px; height: 150px; padding: 5px;
        background: #fafafa;">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <table cellpadding="3">
                    <tr>
                        <td>
                            新密码：
                        </td>
                        <td>
                            <input id="txtNewPass" type="Password" class="txt01" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            确认密码：
                        </td>
                        <td>
                            <input id="txtRePass" type="Password" class="txt01" />
                        </td>
                    </tr>
                </table>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a id="btnEp" class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)">确定</a>
                <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">
                    取消</a>
            </div>
        </div>
    </div>
    <div id="mm" class="easyui-menu" style="width: 150px;">
        <div id="mm-tabupdate">
            刷新</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabclose">
            关闭</div>
        <div id="mm-tabcloseall">
            全部关闭</div>
        <div id="mm-tabcloseother">
            除此之外全部关闭</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabcloseright">
            当前页右侧全部关闭</div>
        <div id="mm-tabcloseleft">
            当前页左侧全部关闭</div>
        <div class="menu-sep">
        </div>
        <div id="mm-exit">
            退出</div>
    </div>
</body>
</html>
