<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoType.aspx.cs" Inherits="Web.SysAdmin.InfoType1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
      $(function() {
          var lastIndex;
          $('#infoTypeGrid').treegrid({
              title: '文章分类',
              fit: true, rownumbers: true, animate: true, border: false,
              url: 'InfoType.aspx?type=load',
              idField: 'ID_ift',
              treeField: 'Name_ift',
              frozenColumns: [[
                 { title: '分类', field: 'Name_ift', width: 300,
                     formatter: function(value) {
                         return '<span style="color:red">' + value + '</span>';
                     }
                 }
                ]],
              columns: [[
                 { title: '编号', field: 'Code_ift', width: 100, editor: 'text' },
                 { title: '是否禁用', field: 'IsHide_ift', width: 100, editor: { type: 'checkbox', options: { on: true, off: false} },
                     formatter: function(value, row) {
                         return value == true ? '是' : '否';
                     }
                 },
                 { title: '排序', field: 'Sort_ift', width: 100, editor: 'text' },
                 { title: 'Url', field: 'Url_ift', width: 100, editor: 'text' }
                ]], onClickRow: function(row) {//运用单击事件实现一行的编辑结束，在该事件触发前会先执行onAfterEdit事件
                    var rowIndex = row.id;
                    if (lastIndex != rowIndex) {
                        $('#infoTypeGrid').treegrid('endEdit', lastIndex);
                    }
                }, onDblClickRow: function(row) {//运用双击事件实现对一行的编辑
                    var rowIndex = row.id;
                    if (lastIndex != rowIndex) {
                        $('#infoTypeGrid').treegrid('endEdit', lastIndex);
                        $('#infoTypeGrid').treegrid('beginEdit', rowIndex);
                        lastIndex = rowIndex;
                    }
                }, onBeforeEdit: function(row) {
                    beforEditRow(row); //这里是功能实现的主要步骤和代码
                }, onAfterEdit: function(row, changes) {
                    var rowId = row.id;
                    $.ajax({
                        url: "saveProductConfig.action",
                        data: row,
                        success: function(text) {
                            $.messager.alert('提示信息', text, 'info');
                        }
                    });
                }
          });
      });
      function beforEditRow(row) {
          var ID_ift = $('#infoTypeGrid').treegrid('getColumnOption', 'ID_ift');
          var Name_ift = $('#infoTypeGrid').treegrid('getColumnOption', 'Name_ift');
          var Code_ift = $('#infoTypeGrid').treegrid('getColumnOption', 'Code_ift');
          var IsHide_ift = $('#infoTypeGrid').treegrid('getColumnOption', 'IsHide_ift');
          var Sort_ift = $('#infoTypeGrid').treegrid('getColumnOption', 'Sort_ift');
          var Url_ift = $('#infoTypeGrid').treegrid('getColumnOption', 'Url_ift');

          var checkboxOptionsObj = new Object();
          checkboxOptionsObj.on = true;
          checkboxOptionsObj.off = false;
          var checkboxEditorObj = new Object();
          checkboxEditorObj.type = 'checkbox';
          checkboxEditorObj.options = checkboxOptionsObj;
          ID_ift.editor = null;
          IsHide_ift.editor = checkboxEditorObj;
          annotateCoclum.editor = checkboxEditorObj;
          Name_ift.editor = 'text';
          Code_ift.editor = 'text';
          Sort_ift.editor = 'text';
          Url_ift.editor = 'text';
      }
    </script>

</head>
<body>
    <table id="infoTypeGrid">
    </table>
</body>
</html>
