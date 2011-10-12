Ext.onReady(function() {
	Ext.Ajax.defaultHeaders = {'Request-By': 'Ext'};
	Ext.Ajax.on('requestcomplete',checkUserSessionStatus, this);
	function checkUserSessionStatus(conn,response,options) {
		try {
			var obj=Ext.util.JSON.decode(response.responseText);
	        if(obj.result == 'timeout'){
	        	Ext.MessageBox.alert('提示', obj.info, function() {
	        		window.top.window.location.href = obj.redirectURL;
	        	});
	        }
	        else if(obj.result == 'error') {
	        	var title = obj.title || '提示';
	        	Ext.MessageBox.alert(title, obj.info, function() {
	        	});
	        }
		}
		catch(e) {}
	}
	
//	new Ext.KeyMap(document, {
//        key: Ext.EventObject.F5,
//        fn: function(keycode, e) {
//        	alert(keycode);
//            if (Ext.isIE) {
//                e.browserEvent.keyCode = 8;
//            }
//            e.stopEvent();
//        }
//    });
});
	