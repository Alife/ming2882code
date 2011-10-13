<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta http-equiv="pragma" content="no-cache" />
		<title>管理系统</title>
		<link rel="stylesheet" type="text/css" href="/ExtJS/resources/css/ext-all.css"/>
		<link rel="stylesheet" type="text/css" href="/ExtJS/resources/css/ext-all-notheme.css"/>
		
		<script type="text/javascript" src="/ExtJS/adapter/ext/ext-base.js"></script>
		<script type="text/javascript" src="/ExtJS/ext-all.js"></script>
		<script type="text/javascript" src="/ExtJS/src/locale/ext-lang-zh_CN.js"></script>
        <script type="text/javascript" src="/ExtJs/ux/RowExpander.js"></script>
		<script type="text/javascript" src="/js/TabCloseMenu.js"></script>
		
		<!-- 主JS -->
		<script type="text/javascript" src="/js/app.js"></script>
		<script type="text/javascript" src="/js/MenuTreePanel.js"></script>
		
		<!-- 验证及导出等JS -->
		<script type="text/javascript" src="/js/ValidateSession.js"></script>
		<script type="text/javascript" src="/js/CookieTheme.js"></script>
		<script type="text/javascript" src="/js/util/CommonRenderer.js"></script>
		<script type="text/javascript" src="/js/util/export.js"></script>
        <script type="text/javascript" src="/js/util/gridToExcel.js"></script>		
		<link rel="stylesheet" type="text/css" href="/css/buttons.css"/>
		<link rel="stylesheet" type="text/css" href="/css/menu.css"/>
		<link rel="stylesheet" type="text/css" href="/css/mytheme-blue.css"/>
		<style type="text/css">
			.loading-indicator {
			    font:bold 13px Helvetica, Arial, Tahoma, Verdana, sans-serif;
			    position:absolute;
			    top:35%;
			    left:43%;
			    color:#444;
			    background-image:url(/images/ani_ajaxload.gif);
			    background-repeat: no-repeat;
			    background-position:left 5px;
			    padding:10px 10px 10px 38px;
				text-align:left;
			}
		</style>
	</head>
	<body>
	</body>
</html>