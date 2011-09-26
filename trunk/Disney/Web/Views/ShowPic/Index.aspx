<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
    <link rel="stylesheet" type="text/css" href="/ext/resources/css/ext-all.css" />
    <link rel="Stylesheet" type="text/css" href="/ext/resources/css/xtheme-blue.css" />
    <link rel="stylesheet" type="text/css" href="/css/default.css" />

    <script type="text/javascript" src="/ext/adapter/ext/ext-base.js"></script>

    <script type="text/javascript" src="/ext/ext-all.js"></script>

    <script type="text/javascript" src="/ext/ux/ext-basex.js"></script>

    <script type="text/javascript" src="/ext/resources/ext-lang-zh_CN.js"></script>

    <script type="text/javascript" src="/js/exts/showpic/show.js"></script>

</head>
<body>
    <%string id = Request["id"]; %>

    <script type="text/javascript">
        var kitworkid = '<%= id%>';
        var kitid = '<%= ViewData["kitID"]%>';
        var kitClassID = '<%= ViewData["kitClassID"]%>';
        var kitChildID = '';
    </script>

</body>
</html>
