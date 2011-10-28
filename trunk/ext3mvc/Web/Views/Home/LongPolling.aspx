<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>HTTP Polling</title>
    <script type="text/javascript" src="/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            function long_polling() {
                $('#logs').append("Waiting...<br/>");
                $.getJSON('/comet/LongPolling', function (data) {
                    if (data.d) {
                        $('#logs').append(data.d + "<br/>");
                    } 
                    long_polling();
                });
            }
            long_polling();
        });
    </script>
</head>
<body>
<div id="logs" style="font-family: monospace;"></div>
</body>
</html>
