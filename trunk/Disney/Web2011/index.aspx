<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Common" %><%@ Import Namespace="BLL" %><%@ Import Namespace="Models" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>童話世界</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.ad-gallery.pack.js" type="text/javascript"></script>
    <!--[if lt IE 7]>
	<link rel="stylesheet" type="text/css" href="css/ie_style.css" />
	<script type="text/javascript" src="js/ie_png.js"></script>  
	<script type="text/javascript">ie_png.fix('#header .logo,#header .menu,#header .menu_bottom,#header .menu li a:hover,#header .menu li a.current,#main .content .content_left h2,#main .content .content_left h2 label,#main .content .content_left h2.current label,#main .content .content_left ul li,#main .content .content_left ul li a,#main .content .content_left ul li a:hover,#main .content .content_left ul li.current a');</script>	
	<![endif]-->
</head>
<body style="height:740px;">
    <div id="container">
        <div id="header">
            <div class="logo"></div>
            <div class="menu">
                <ul class="clearfix">
                    <li><a href="index.aspx" class="current">關于我們</a></li>
                    <li><a href="portfolio.aspx">作品集</a></li>
                    <li><a href="album.aspx">相冊介紹</a></li>
                    <li><a href="clothing.aspx">服裝</a></li>
                    <li><a href="version.aspx">版型</a></li>
                    <li><a href="file.aspx">資料下載</a></li>
                    <li><a href="contact.aspx">聯繫我們</a></li>
                </ul>
                <div class="menu_bottom"></div>
            </div>
            <div class="clear"></div>
        </div>
        <div id="main">
            <div class="content">
                <div class="content_left index">
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
                                <ul class="ad-thumb-list" style="padding-left:50px;">
                                    <li><a href="/images/d/i/bigimage00001.jpg"><img src="/images/d/i/image00001.jpg" bigsrc="/images/d/i/bigimage00001.jpg" oldsrc="/images/d/i/巴斯光年.jpg" alt="" title="巴斯光年" style="cursor:pointer;border:1px solid #000;" /></a></li>
                                    <li><a href="/images/d/i/bigimage00006.jpg"><img src="/images/d/i/image00006.jpg" bigsrc="/images/d/i/bigimage00006.jpg" oldsrc="/images/d/i/威廉王子.jpg" alt="" title="威廉王子" style="cursor:pointer;border:1px solid #000;" /></a></li>
                                    <li><a href="/images/d/i/bigimage00003.jpg"><img src="/images/d/i/image00003.jpg" bigsrc="/images/d/i/bigimage00003.jpg" oldsrc="/images/d/i/海軍.jpg" alt="" title="海軍" style="cursor:pointer;border:1px solid #000;" /></a></li>
                                    <li><a href="/images/d/i/bigimage00002.jpg"><img src="/images/d/i/image00002.jpg" bigsrc="/images/d/i/bigimage00002.jpg" oldsrc="/images/d/i/貝兒.jpg" alt="" title="貝兒" style="cursor:pointer;border:1px solid #000;" /></a></li>
                                    <li><a href="/images/d/i/bigimage00004.jpg"><img src="/images/d/i/image00004.jpg" bigsrc="/images/d/i/bigimage00004.jpg" oldsrc="/images/d/i/美妮.jpg" alt="" title="美妮" style="cursor:pointer;border:1px solid #000;" /></a></li>
                                    <li><a href="/images/d/i/bigimage00005.jpg"><img src="/images/d/i/image00005.jpg" bigsrc="/images/d/i/bigimage00005.jpg" oldsrc="/images/d/i/美人魚.jpg" alt="" title="美人魚" style="cursor:pointer;border:1px solid #000;" /></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <p style="display:none">流覽方式: <select id="switch-effect"> 
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
//            $('.ad-thumb-list img').each(function() {
//                $(this).click(function() {
//                    var old = $(this).attr('oldsrc');
//                    var bigsrc = $(this).attr('bigsrc');
//                    var alt = $(this).attr('alt');
//                    $('#ad-image').attr('src', bigsrc);
//                    $('#ad-image').attr('alt', alt);
//                    $('#ad-image').attr('title', alt);
//                    $('#descriptions').html(alt);
//                });
//            })
        });
    </script>
</body>
</html>
