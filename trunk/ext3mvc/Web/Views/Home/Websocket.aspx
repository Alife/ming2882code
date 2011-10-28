<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>1</title>
<script type="text/javascript"><!--
if (!window.WebSocket) { 
	alert("WebSocket not supported by this browser!"); 
} 
var ws;
function display() { 
	if(ws == null){
	    ws = new WebSocket("ws://127.16.2.119:2000"); 
		ws.onmessage = function(evt) { 
			alert("接收到信息:"+evt.data);
		}; 
		
		ws.onclose = function() { 
			alert("连接已关闭。");
			ws = null;
		};
		
		ws.onerror = function(evt){
			alert("连接出错:"+evt.data);
			ws = null;
		}
		
		ws.onopen = function(evt) { 
			alert("连接已打开。");
		}; 
	}
} 
function wssend(){
	if(ws!=null){
		var sendstr = document.getElementById("txtSend").value;
		if(sendstr == ""){
			sendstr = " ";
		}
		ws.send(sendstr);
	}else{
		alert("连接失效。");
	}
}
// -->
</script>
</head>
<body style="text-align:center;">
<input type="button" value="点我" onclick="display()"/>
<br />
<input type="text" id="txtSend" /><input type="button" id="btnSend" value="发送" onclick="wssend()"/>
</body>
</html>

