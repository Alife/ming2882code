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
    <!--[if lt IE 7]>
	<link rel="stylesheet" type="text/css" href="css/ie_style.css" />
	<script type="text/javascript" src="js/ie_png.js"></script>  
	<script type="text/javascript">ie_png.fix('#header .logo,#header .menu,#header .menu_bottom,#header .menu li a:hover,#header .menu li a.current,#main .content .content_left h2,#main .content .content_left h2 label,#main .content .content_left h2.current label,#main .content .content_left ul li,#main .content .content_left ul li a,#main .content .content_left ul li a:hover,#main .content .content_left ul li.current a');</script>	
	<![endif]-->
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
                    <li><a href="clothing.aspx">服裝</a></li>
                    <li><a href="version.aspx">版型</a></li>
                    <li><a href="file.aspx" class="current">資料下載</a></li>
                    <li><a href="contact.aspx">聯繫我們</a></li>
                </ul>
                <div class="menu_bottom"></div>
            </div>
            <div class="clear"></div>
        </div>
        <div id="main">
            <div class="content">
                <div class="content_left">
                    <h2><label>資料下載</label></h2>
                </div>
                <div class="content_right" style="width:600px">
                    <ul>
                        <li><a href="http://work.jiaoguo.com/images/proof.doc" target="blank">校圖流程文件</a></li>
                        <li><a href="http://work.jiaoguo.com/images/shoot-notice.doc" target="blank">幼稚園拍照注意事項</a></li>
                        <li><a href="http://work.jiaoguo.com/images/name-order.xls" target="blank">姓名單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/cover.xls" target="blank">封面選擇單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/group.xls" target="blank">團照+班導+小組照挑片單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/optional.xls" target="blank">園所自挑照片單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/editphoto.xls" target="blank">修片要求</a></li>
                        <li><a href="http://work.jiaoguo.com/images/proofcon.doc" target="blank">線上校稿套圖完成確認單</a></li>
                        <li><a href="http://work.jiaoguo.com/images/gpcon.doc" target="blank">美工光碟校稿確認單</a></li>
                    </ul>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
</body>
</html>
