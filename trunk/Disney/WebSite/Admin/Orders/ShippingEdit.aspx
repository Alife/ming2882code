<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" CodeFile="ShippingEdit.aspx.cs" Inherits="Admin_Orders_ShippingEdit" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="sitemap">
        <img src="/admin/images/main_14.gif" />
        當前位置：<span><a href="/admin">管理中心</a></span> &gt; <span><a href="shipping.aspx">配送方式</a></span> &gt;<label>修改配送方式</label>
    </div>
    <div style="padding-top: 10px;">
    </div>
    <div id="main">
        <%Shipping item = ShippingBLL.GetItem(int.Parse(Request["id"])); %>
        <form id="frm" action="" method="post">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablefont tableform">
            <tr>
                <th>
                    名称
                </th>
                <td>
                    <input id="name" name="name" type="text" value="<%= item.Name%>" />
                </td>
            </tr>
            <tr>
                <th>
                    价格
                </th>
                <td>
                    <input id="price" name="price" type="text" value="<%= item.Price%>" />
                </td>
            </tr>
            <tr>
                <th>
                    排序
                </th>
                <td>
                    <input id="orderid" name="orderid" type="text" value="<%= item.OrderID%>" style="width: 50px;" />
                </td>
            </tr>
            <tr>
                <th>
                </th>
                <td>
                    <input id="id" name="id" type="hidden" value="<%= item.ID%>" />
                    <input id="btnsave" type="submit" value="保存" class="button" />
                </td>
            </tr>
        </table>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MeteContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#frm").validate({
                rules: {
                    name: {
                        required: true,
                        maxlength: 50
                    },
                    price: {
                        required: true,
                        number: true
                    },
                    orderid: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    name: {
                        required: "请填写配送名称",
                        minlength: "配送名称长度在50个字符内"
                    },
                    price: {
                        required: "请填写价格",
                        number: "排序必须是数字"
                    },
                    orderid: {
                        required: "请填写排序",
                        number: "排序必须是数字"
                    }
                }
            });
        });
    </script>

</asp:Content>
