<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MeteContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="sitemap">
        當前位置：<span><a href="/admin">管理中心</a></span>
    </div>
    <div style="padding: 15px;">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="grid" style="display:none">
            <tr>
                <th colspan="2">
                    一、统计
                </th>
            </tr>
            <tr>
                <td>
                    商品统计：
                </td>
                <td>
                    订单统计：
                </td>
            </tr>
            <tr>
                <td>
                    文章统计：
                </td>
                <td>
                    评论统计：
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
