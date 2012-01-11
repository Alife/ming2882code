<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        fastreport web
    </h2>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:SqlDataSource ID="Products" runat="server" ConnectionString="<%$ ConnectionStrings:NorthWind %>"
        SelectCommand="SELECT * FROM [products]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Categories" runat="server" ConnectionString="<%$ ConnectionStrings:NorthWind %>"
        SelectCommand="SELECT * FROM [Categories]"></asp:SqlDataSource>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rpt:WebReport ID="WebReport1" runat="server" Width="100%" OnStartReport="WebReport1_StartReport"
                ReportDataSources="Products;Categories" ReportFile="~/Report/Report With Cover Page.frx" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
