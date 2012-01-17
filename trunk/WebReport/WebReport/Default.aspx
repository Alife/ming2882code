<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="report_left">
                <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" 
                    Width="100%" ShowLines="True" ExpandDepth="2">
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="Reports" Text="报表列表" Value="报表列表" />
                        <asp:TreeNodeBinding DataMember="Folder" TextField="Name" ToolTipField="Name"
                            ValueField="Name" />
                        <asp:TreeNodeBinding DataMember="Report" NavigateUrlField="Url" 
                            TextField="Name" ValueField="File" />
                    </DataBindings>
                </asp:TreeView>
                <asp:XmlDataSource ID="XmlDataSource1" runat="server" 
                    DataFile="~/Report/reports.xml"></asp:XmlDataSource>
            </div>
            <div class="report_right">
                <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                </asp:SiteMapPath>
                <rpt:WebReport ID="WebReport1" runat="server" Width="900px" OnStartReport="WebReport1_StartReport" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
