<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>基础DataGrid</title>

    <script type="text/javascript">
        $(function() {
            $('#viewBaseGrid').datagrid({
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
        });
    </script>

</head>
<body>
    <table id="viewBaseGrid">
    </table>
</body>
</html>
