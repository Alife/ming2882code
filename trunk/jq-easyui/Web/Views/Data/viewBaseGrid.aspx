<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>基础DataGrid</title>

    <script type="text/javascript">
        //$(function() {
        $.Class("data.BaseGrid", {
            Extends: [],
            Implements: [],
            grid: $('#viewBaseGrid').datagrid({
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
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function() {
                        $('#btnsave').linkbutton('enable');
                        test.add();
                    }
                }, '-', {
                    id: 'btncut',
                    text: '编辑',
                    iconCls: 'icon-edit',
                    handler: function() {
                        $('#btnsave').linkbutton('enable');
                        alert('cut');
                    }
                }, '-', {
                    id: 'btnsave',
                    text: '保存',
                    iconCls: 'icon-save',
                    handler: function() {
                        //$('#btnsave').linkbutton('disable');
                        //alert('save');
                        //$(test.grid.datagrid('options').toolbar[0]).linkbutton('disable');
                        alert($('.datagrid-toolbar', test.grid).html());
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
                    text: '搜索',
                    iconCls: 'icon-search',
                    handler: function() {
                        $('#btnsave').linkbutton('disable');
                        alert('save');
                    }
                }
                    ]
            }),
            add: function() {
                alert("1");
            }
        });
        var test = new data.BaseGrid();
        //});
    </script>

</head>
<body>
    <div id="viewBaseGrid">
    </div>
</body>
</html>
