<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" CodeFile="Shipping.aspx.cs" Inherits="Admin_Orders_Shipping"%>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="sitemap">
        <img src="/admin/images/main_14.gif" />當前位置：<span><a href="/admin">管理中心</a></span> &gt;<label>配送方式</label>
    </div>
    <div id="main">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="30">
                    <ul class="navapp clearfix">
                        <li class="cbChoose">
                            <input type="checkbox" value="0" id="cbChoose" />全选</li>
                        <li class="mbg add"><a href="shippingadd.aspx" title="新增">新增</a></li>
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
                                價格
                            </th>
                            <th>
                                排序
                            </th>
                        </tr>
                        <%
                            List<Shipping> list = ShippingBLL.GetList();
                            foreach (Shipping item in list)
                            {%>
                        <tr class="tbodyfont">
                            <td>
                                <input name="cbitem" type="checkbox" value="<%= item.ID%>" />
                            </td>
                            <td class="textleft">
                                <%= item.Name%>
                            </td>
                            <td>
                                <%= item.Price%>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MeteContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#edit").click(function() {
                var id = $('input:checkbox[name=cbitem][checked=true]').val();
                if (!id) {
                    jAlert('沒有選項');
                    return false;
                } else {
                    location.href = 'shippingedit.aspx?id=' + id;
                }
            });
            $('#del').click(function() {
                opfun('del', 'shipping.aspx?op=del');
            });
        });
    </script>

</asp:Content>
