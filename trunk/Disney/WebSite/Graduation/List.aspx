<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" %>

<%@ Import Namespace="Common" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Models" %>
<%@ Import Namespace="System.IO" %>
<%@ Register Src="~/Shared/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Application["WebName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Header ID="Header1" runat="server" nowmodel="graduation" />
    <div id="main">
        <div class="graduation_c">
            <ul>
                <li>
                    <img src="<%= Application["WebImageUrl"] %>graduation/image-1_01.jpg" alt="實現夢想，迪士尼主角換你當" /></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=1" title="服裝介紹 男">
                    <img src="<%= Application["WebImageUrl"] %>graduation/image-1_02.jpg" alt="服裝介紹 男" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=2" title="服裝介紹 女">
                    <img src="<%= Application["WebImageUrl"] %>graduation/image-1_03.jpg" alt="服裝介紹 女" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=3" title="團體照版型">
                    <img src="<%= Application["WebImageUrl"] %>graduation/image-1_04.jpg" alt="團體照版型" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=4" title="同學錄版型">
                    <img src="<%= Application["WebImageUrl"] %>graduation/image-1_05.jpg" alt="同學錄版型" /></a></li>
                <li><a href="<%= Application["WebUrl"] %>graduation/list.aspx?id=5" title="生活小組照版型">
                    <img src="<%= Application["WebImageUrl"] %>graduation/image-1_06.jpg" alt="生活小組照版型" /></a></li>
            </ul>
            <div class="clear">
            </div>
            <div id="gallery" class="ad-gallery"> 
                <div class="ad-image-wrapper"> 
                </div> 
                <div class="ad-controls"> 
                    <div id="descriptions"></div>
                </div> 
                <div class="ad-nav"> 
                    <div class="ad-thumbs">
                        <ul class="ad-thumb-list">
                            <% int id = 0; int.TryParse(Request["id"], out id);
                               string currentfile = Application["WebImageUrl"] + "graduation/";
                               if (id == 1)
                               {
                                   string currentfolder = Server.MapPath("/images/graduation/boy/");
                                   string[] arrfiles = Directory.GetFiles(currentfolder, "*.jpg", SearchOption.AllDirectories);
                                   foreach (string fileItem in arrfiles)
                                   {
                                       FileInfo fileinfo = new FileInfo(fileItem);
                                       string fdriname = fileinfo.Directory.Name;
                                       string filename = fileinfo.Name;
                                       string title = filename.Replace(".jpg", "");
                                       string filepath = currentfile + fdriname + "/" + filename;
                            %>
                            <li><a href="<%= filepath%>"><img src="<%= filepath%>" alt="<%= fdriname%>" title="<%= title%>" /></a></li>
                            <%
                                }
                           }
                           else if (id == 2)
                           {
                               string currentfolder = Server.MapPath("/images/graduation/girl/");
                               string[] arrfiles = Directory.GetFiles(currentfolder, "*.jpg", SearchOption.AllDirectories);
                               foreach (string fileItem in arrfiles)
                               {
                                   FileInfo fileinfo = new FileInfo(fileItem);
                                   string fdriname = fileinfo.Directory.Name;
                                   string filename = fileinfo.Name;
                                   string title = filename.Replace(".jpg", "");
                                   string filepath = currentfile + fdriname + "/" + filename;
                            %>
                            <li><a href="<%= filepath%>"><img src="<%= filepath%>" alt="<%= fdriname%>" title="<%= title%>" /></a></li>
                            <%
                              }
                           } 
                           else if (id == 3)
                           {
                               string currentfolder = Server.MapPath("/images/graduation/group_photo/");
                               string[] arrfiles = Directory.GetFiles(currentfolder, "*.jpg", SearchOption.AllDirectories);
                               foreach (string fileItem in arrfiles)
                               {
                                   FileInfo fileinfo = new FileInfo(fileItem);
                                   string fdriname = fileinfo.Directory.Name;
                                   string filename = fileinfo.Name;
                                   string title = filename.Replace(".jpg", "");
                                   string filepath = currentfile + fdriname + "/" + filename;
                            %>
                            <li><a href="<%= filepath%>"><img src="<%= filepath%>" alt="<%= fdriname%>" title="<%= title%>" /></a></li>
                            <%
                              }
                           }
                           else if (id == 4)
                           {
                               string currentfolder = Server.MapPath("/images/graduation/classmates/");
                               string[] arrfiles = Directory.GetFiles(currentfolder, "*.jpg", SearchOption.AllDirectories);
                               foreach (string fileItem in arrfiles)
                               {
                                   FileInfo fileinfo = new FileInfo(fileItem);
                                   string fdriname = fileinfo.Directory.Name;
                                   string filename = fileinfo.Name;
                                   string title = filename.Replace(".jpg", "");
                                   string filepath = currentfile + fdriname + "/" + filename;
                            %>
                            <li><a href="<%= filepath%>"><img src="<%= filepath%>" alt="<%= fdriname%>" title="<%= title%>" /></a></li>
                            <%
                              }
                           } 
                           else if (id == 5)
                           {
                               string currentfolder = Server.MapPath("/images/graduation/life/");
                               string[] arrfiles = Directory.GetFiles(currentfolder, "*.jpg", SearchOption.AllDirectories);
                               foreach (string fileItem in arrfiles)
                               {
                                   FileInfo fileinfo = new FileInfo(fileItem);
                                   string fdriname = fileinfo.Directory.Name;
                                   string filename = fileinfo.Name;
                                   string title = filename.Replace(".jpg", "");
                                   string filepath = currentfile + fdriname + "/" + filename;
                            %>
                            <li><a href="<%= filepath%>"><img src="<%= filepath%>" alt="<%= fdriname%>" title="<%= title%>" /></a></li>
                            <%
                              }
                           }  %>
                        </ul>
                    </div>
                </div>
            </div>
            <p>流覽方式: <select id="switch-effect"> 
              <option value="slide-hori">橫向流覽</option> 
              <option value="slide-vert">縱向流覽</option> 
              <option value="resize">收缩/展開</option> 
              <option value="fade">淡入/淡出</option> 
              <option value="">沒有</option> 
            </select>
            <a href="#" id="toggle-slideshow">切換幻燈片</a>
            <a href="#" id="toggle-description" style="display:none"> | 描述切換到圖片外顯示</a> 
            </p> 
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            var galleries = $('.ad-gallery').adGallery({ loader_image: '/images/graduation/loader.gif' });
            galleries[0].settings.description_wrapper = $('#descriptions');
            $('#switch-effect').change(function() {
                galleries[0].settings.effect = $(this).val();
                return false;
            });
            $('#toggle-slideshow').click(function() {
                galleries[0].slideshow.toggle();
                return false;
            });
            /*$('#toggle-description').click(function() {
            if (!galleries[0].settings.description_wrapper) {
            galleries[0].settings.description_wrapper = $('#descriptions');
            } else {
            galleries[0].settings.description_wrapper = false;
            }
            return false;
            });*/
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../Css/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery.ad-gallery.pack.js" type="text/javascript"></script>
</asp:Content>
