<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" CodeBehind="JobAdd.aspx.cs"
    Inherits="WebSite.Admin.JobAdd" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="sitemap">
        <img src="/admin/images/main_14.gif" />
        当前位置：<span><a href="/main.aspx">管理中心</a></span> &gt; <span><a href="Job.aspx">人才招聘管理</a></span>
        &gt;
        <label>
            增加人才招聘</label>
    </div>
    <div style="padding-top: 10px;">
    </div>
    <div id="main">
        <form id="frm" action="" method="post">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablefont tableform">
            <tr>
                <td>
                    招聘信息
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <th>
                    招聘岗位名：
                </th>
                <td>
                    <input id="jobPersonName" name="jobPersonName" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    所属部门：
                </th>
                <td>
                    <input id="jobSection" name="jobSection" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    工作性质：
                </th>
                <td>
                    <input id="jobXingzhi" name="jobXingzhi" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    有效时间：
                </th>
                <td>
                    <input id="YouxiaoDatetime" name="YouxiaoDatetime" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    所需文化：
                </th>
                <td>
                    <input id="wenping" name="wenping" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    性别要求：
                </th>
                <td>
                    <input name="sex" type="radio" value="0" checked />
                    不限
                    <input name="sex" type="radio" value="1" />
                    男
                    <input name="sex" type="radio" value="2" />
                    女
                </td>
            </tr>
            <tr>
                <th>
                    是否显示：
                </th>
                <td>
                    <input name="boolShow" type="radio" value="1" checked />
                    是
                    <input name="boolShow" type="radio" value="0" />
                    否
                </td>
            </tr>
            <tr>
                <th>
                    需求人数：
                </th>
                <td>
                    <input id="jobNum" name="jobNum" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    年龄要求：
                </th>
                <td>
                    <input id="age" name="age" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    工作地点：
                </th>
                <td>
                    <input id="jobAdress" name="jobAdress" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    职位要求：
                </th>
                <td>
                    <textarea id="jobContent" cols="20" name="jobContent" rows="2"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    联系人信息
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <th>
                    联系人：
                </th>
                <td>
                    <input id="LianxiMen" name="LianxiMen" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    联系電話：
                </th>
                <td>
                    <input id="LianxiTel" name="LianxiTel" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    FAX号：
                </th>
                <td>
                    <input id="LianxiFax" name="LianxiFax" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    Email：
                </th>
                <td>
                    <input id="Email" name="Email" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                </th>
                <td>
                    <input id="btnsave" type="submit" value="保存" class="button" />
                </td>
            </tr>
        </table>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MeteContent" runat="server">

    <script type="text/javascript" charset="utf-8" src="/kindeditor/kindeditor.js"></script>

    <script type="text/javascript">
        $(function() {
            KE.show({
                id: 'jobContent',
                imageUploadJson: '/kindeditor/asp.net/upload_json.ashx',
                fileManagerJson: '/kindeditor/asp.net/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function(id) {
                    KE.event.ctrl(document, 13, function() {
                        KE.util.setData(id);
                        document.forms['frm'].submit();
                    });
                    KE.event.ctrl(KE.g[id].iframeDoc, 13, function() {
                        KE.util.setData(id);
                        document.forms['frm'].submit();
                    });
                }
            });
            $("#YouxiaoDatetime").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
            $("#frm").validate({
                rules: {
                    jobPersonName: {
                        required: true,
                        maxlength: 50
                    }
//                    ,
//                    jobNum: {
//                        required: true,
//                        digits: true
//                    },
//                    age: {
//                        required: true,
//                        digits: true
//                    }
                },
                messages: {
                    jobPersonName: {
                        required: "请填写岗位名称",
                        minlength: "名称在50个字符內"
                    }
//                    ,
//                    jobNum: {
//                        required: '请填写需求人数',
//                        digits: '需求人数必须是数字'
//                    },
//                    age: {
//                        required: '请填写年龄要求',
//                        digits: '年龄要求必须是数字'
//                    }
                }
            });
        });
    </script>

</asp:Content>
