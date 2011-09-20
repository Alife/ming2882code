﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>童話世界</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.ad-gallery.pack.js" type="text/javascript"></script>
    <style type="text/css">body{background:#255583 url(images/d/hhbg_3.jpg) no-repeat center 0;}</style>
</head>
<body>
    <div id="container">
        <div id="header">
            <div class="logo"></div>
            <div class="menu">
                <ul class="clearfix">
                    <li><a href="index.aspx">關于我們</a></li>
                    <li><a href="portfolio.aspx" class="current">作品集</a></li>
                    <li><a href="album.aspx">相冊介紹</a></li>
                    <li><a href="clothing.aspx">服裝</a></li>
                    <li><a href="version.aspx">版型</a></li>
                    <li><a href="contact.aspx">聯繫我們</a></li>
                </ul>
                <div class="menu_bottom"></div>
            </div>
            <div class="clear"></div>
        </div>
        <div id="main">
            <div class="content">
                <div class="content_left content_left_list">
                    <h2><label>作品集</label></h2>
                    <ul class="list">
                        <li class="current"><a href="portfolio.aspx?id=1">精緻組</a></li>
                        <li><a href="portfolio.aspx?id=2">豪華組</a></li>
                        <li><a href="portfolio.aspx?id=3">尊貴組</a></li>
                        <li><a href="portfolio.aspx?id=4">旗艦組</a></li>
                    </ul>
                </div>
                <div class="content_right">
                    <ul class="portfolio_list">
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                        <li><p class="pm"><a href=""><img src="images/d/p1.jpg" alt="" /></a></p><div>文件名</div></li>
                    </ul>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function() {
        });
    </script>
</body>
</html>
