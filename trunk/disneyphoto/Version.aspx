<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>童話世界</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.ad-gallery.pack.js" type="text/javascript"></script>
</head>
<body>
    <div id="container">
        <div id="header">
            <div class="logo"></div>
            <div class="menu">
                <ul class="clearfix">
                    <li><a href="index.aspx">關于我們</a></li>
                    <li><a href="portfolio.aspx">作品集</a></li>
                    <li><a href="album.aspx">相冊介紹</a></li>
                    <li><a href="clothing.aspx" class="current">服裝</a></li>
                    <li><a href="version.aspx" class="current">版型</a></li>
                    <li><a href="contact.aspx">聯繫我們</a></li>
                </ul>
                <div class="menu_bottom"></div>
            </div>
            <div class="clear"></div>
        </div>
        <div id="main">
            <div class="content">
                <div class="content_left">
                    <h2><label>迪士尼系列相冊</label></h2>
                    <ul>
                        <li><a href="clothing.aspx?id=3">團體照版型</a></li>
                        <li class="current"><a href="index.aspx?id=4">同學錄版型</a></li>
                        <li><a href="index.aspx?id=5">生活小組照版型</a></li>
                    </ul>
                </div>
                <div class="content_right">
                    <div id="gallery" class="ad-gallery"> 
                        <div class="ad-image-wrapper"> 
                        </div> 
                        <div class="ad-controls"> 
                            <div id="descriptions"></div>
                        </div> 
                        <div class="ad-nav"> 
                            <div class="ad-thumbs">
                                <ul class="ad-thumb-list">
                                    <% int id = 0; int.TryParse(Request["id"], out id); id = id == 0 ? 3 : id;
                                       string currentfile = "images/graduation/";
                                       if (id == 1)
                                       {
                                           string currentfolder = Server.MapPath("images/graduation/boy/");
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
                                       string currentfolder = Server.MapPath("images/graduation/girl/");
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
                                       string currentfolder = Server.MapPath("images/graduation/Group Photo/");
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
                                       string currentfolder = Server.MapPath("images/graduation/classmates/");
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
                                       string currentfolder = Server.MapPath("images/graduation/life/");
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
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function() {
            var galleries = $('.ad-gallery').adGallery({ loader_image: 'images/graduation/loader.gif' });
            galleries[0].settings.description_wrapper = $('#descriptions');
            $('#switch-effect').change(function() {
                galleries[0].settings.effect = $(this).val();
                return false;
            });
            $('#toggle-slideshow').click(function() {
                galleries[0].slideshow.toggle();
                return false;
            });
        });
    </script>
</body>
</html>
