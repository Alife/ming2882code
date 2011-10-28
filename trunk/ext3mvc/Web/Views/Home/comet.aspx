<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>Multipart XMLHttpRequest</title>
<script type="text/javascript" src="/js/jquery-1.4.4.min.js"></script>
<script type="text/javascript">
    jQuery(function ($) {
        if (!('XMLHttpRequest' in window && 'multipart' in window.XMLHttpRequest.prototype)) {
            alert('Comet Http Streaming is not supported in your browser !');
            throw new Error('Comet Http Streaming is not supported in your browser !');
        }        
        var xhr = $.ajaxSettings.xhr();
        xhr.multipart = true;
        xhr.open('GET', '/home/multipart', true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {
                $('#logs').append(xhr.responseText+"<br/>");
            }
        };
        xhr.send(null);
    });
</script>
</head>
<body>
<div id="logs" style="font-family: monospace;">
</div>
</body>
</html>
