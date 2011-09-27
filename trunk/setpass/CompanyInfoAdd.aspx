<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/AdminSite.Master" CodeBehind="CompanyInfoAdd.aspx.cs"
    Inherits="WebSite.Admin.CompanyInfoAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="sitemap">
        <img src="/admin/images/main_14.gif" />
        当前位置：<span><a href="/main.aspx">管理中心</a></span> &gt; <span><a href="CompanyInfo.aspx">
            站点信息管理</a></span> &gt; <label>增加站点信息管理</label>
    </div>
    <div style="padding-top: 10px;">
    </div>
    <div id="main">
        <form id="frm" action="" method="post">
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablefont tableform">
            <tr>
                <th>
                    名称
                </th>
                <td>
                    <input id="InfoTitle" name="InfoTitle" type="text" value="" />
                </td>
            </tr>
            <tr>
                <th>
                    內容
                </th>
                <td>
                    <textarea id="InfoContent" name="InfoContent" style="width:100%;height:400px;visibility:hidden;"></textarea>
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
                id: 'InfoContent',
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
            $("#frm").validate({
                rules: {
                    InfoTitle: {
                        required: true,
                        maxlength: 50
                    }
                },
                messages: {
                    InfoTitle: {
                        required: "请填写名称",
                        minlength: "名称在50个字符內"
                    }
                }
            });
        });
    </script>

</asp:Content>
