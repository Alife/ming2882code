<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" CodeFile="PhotoCategoryAdd.aspx.cs" Inherits="Admin_Info_PhotoCategoryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="sitemap">
        <img src="/admin/images/main_14.gif" />
        當前位置：<span><a href="/admin">管理中心</a></span> &gt; <span><a href="photocategory.aspx">相冊分類</a></span> &gt;<label>增加相冊分類</label>
    </div>
    <div style="padding-top: 10px;">
    </div>
    <div id="main">
        <form id="frm" action="" method="post">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablefont tableform">
            <tr>
                <th>
                    名稱
                </th>
                <td>
                    <input id="name" name="name" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    拍攝時間
                </th>
                <td>
                    <input id="shootingTime" name="shootingTime" type="text" value="" readonly />
                </td>
            </tr>
            <tr>
                <th>
                    內容
                </th>
                <td>
                    <textarea id="intro" name="intro"></textarea>
                </td>
            </tr>
            <tr>
                <th>
                    排序
                </th>
                <td>
                    <input id="orderid" name="orderid" type="text" value="" style="width: 50px;" />
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
    <script type="text/javascript">
        $(function() {
            $("#shootingTime").click(function() { getDatePicker('shootingTime', event, 21) });
            $("#frm").validate({
                rules: {
                    name: {
                        required: true,
                        maxlength: 50
                    },
                    shootingTime: "required",
                    orderid: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    name: {
                        required: "請填寫分類名稱",
                        minlength: "分類名稱在50個字符內"
                    },
                    shootingTime: "請填寫拍攝時間",
                    orderid: {
                        required: "請填寫排序",
                        number: "排序必須是數字"
                    }
                }
            });
        });
    </script>

</asp:Content>
