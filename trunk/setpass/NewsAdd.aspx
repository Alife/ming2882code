<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" CodeBehind="NewsAdd.aspx.cs"
    Inherits="WebSite.Admin.NewsAdd" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="sitemap">
        <img src="/admin/images/main_14.gif" />
        当前位置：<span><a href="/main.aspx">管理中心</a></span> &gt; <span><a href="News.aspx">资讯管理</a></span>
        &gt;
        <label>
            增加资讯</label>
    </div>
    <div style="padding-top: 10px;">
    </div>
    <div id="main">
        <%int typeid = 0; int.TryParse(Request["typeid"], out typeid);%>
        <form id="frm" action="" method="post" enctype="multipart/form-data">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablefont tableform">
            <tr>
                <th>
                    所属类别：
                </th>
                <td>
                    <select id="NewsTypeId" name="NewsTypeId">
                        <%
                            List<NewsType> list = NewsTypeBLL.GetList(typeid, 0);
                            foreach (var titem in list)
                            {%>
                        <option value="<%= titem.ID%>">
                            <%= titem.Name%></option>
                        <%} %>
                    </select>
                </td>
            </tr>
            <tr>
                <th>
                    标题：
                </th>
                <td>
                    <input id="Newstheme" name="Newstheme" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    发布人：
                </th>
                <td>
                    <input id="NewsMen" name="NewsMen" type="text" />
                    发布时间：<input id="NewsTimen" name="NewsTimen" type="text" value="<%= DateTime.Now%>"
                        readonly />
                    资讯排序：<input id="NewsOrderby" name="NewsOrderby" type="text" style="width: 50px;" />
                </td>
            </tr>
            <tr>
                <th>
                    资讯类型：
                </th>
                <td>
                    <input id="NewsBool" name="NewsBool" type="radio" value="0" />
                    文字
                    <input id="Radio1" name="NewsBool" type="radio" value="1" />
                    图片
                </td>
            </tr>
            <tr>
                <th>
                    图片：
                </th>
                <td>
                    <input id="NewsPic" name="NewsPic" type="file" />
                </td>
            </tr>
            <tr>
                <th>
                    简介：
                </th>
                <td>
                    <textarea id="NewsContent" cols="20" name="NewsContent" rows="2" style="width:100%;height:400px;visibility:hidden;"></textarea>
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

    <script type="text/javascript" src="/admin/js/selectdate.js"></script>

    <script type="text/javascript" charset="utf-8" src="/kindeditor/kindeditor.js"></script>

    <script type="text/javascript">
        $(function() {
            KE.show({
                id: 'NewsContent',
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
            $("#NewsTimen").click(function() { getDatePicker('NewsTimen', event, 21) });
            $("#frm").validate({
                rules: {
                    Newstheme: {
                        required: true,
                        maxlength: 100
                    }
                },
                messages: {
                    Newstheme: {
                        required: "请填写名称",
                        minlength: "名称在100个字符內"
                    }
                }
            });
        });
    </script>

</asp:Content>
