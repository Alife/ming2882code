<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" CodeFile="PhotoCategory.aspx.cs" Inherits="Admin_Info_PhotoCategory" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MeteContent" runat="Server">
    <script type="text/javascript">
        $(function() {
            $('.del').click(function() {
                opfun('del', 'photocategory.aspx?op=del');
            });
            $(".edit").click(function() {
                var id = $('input:checkbox[name=cbitem][checked=true]').val();
                if (!id) {
                    jAlert('沒有選項');
                    return false;
                } else {
                    location.href = 'photocategoryedit.aspx?id=' + id;
                }
            });
        });          
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="sitemap">
        當前位置：<span><a href="/admin">管理中心</a></span> &gt;<label>相冊分類</label>
    </div>
    <div class="main">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="30">
                    <ul class="navapp clearfix">
                        <li class="cbChoose">
                            <input type="checkbox" value="0" id="cbChoose" />全选</li>
                        <li class="mbg add"><a href="photocategoryadd.aspx" title="新增">新增</a></li>
                        <li class="mbg edit" title="修改">修改</li>
                        <li class="mbg del" title="删除">删除</li>
                        <li id="loading" style="display: none;">正在提交中...</li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablefont">
                        <tr>
                            <th class="choose">
                                選擇
                            </th>
                            <th class="textleft">
                                名稱
                            </th>
                            <th>
                                拍攝時間
                            </th>
                            <th>
                                排序
                            </th>
                        </tr>
                        <%
                            List<w_PhotoCategory> list = w_PhotoCategoryBLL.GetList();
                            foreach (w_PhotoCategory item in list)
                            {%>
                        <tr class="tbodyfont">
                            <td>
                                <input name="cbitem" type="checkbox" value="<%= item.ID%>" />
                            </td>
                            <td class="textleft">
                                <%= item.Name%>
                            </td>
                            <td>
                                <%= item.ShootingTime%>
                            </td>
                            <td>
                                <%= item.OrderID%>
                            </td>
                        </tr>
                        <%} %>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>