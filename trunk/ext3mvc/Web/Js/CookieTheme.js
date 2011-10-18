Ext.onReady(function() {
	var cp = new Ext.state.CookieProvider();
	var AdStatETheme = cp.get("AdStatEThemeCSS");
	if(!AdStatETheme)
	    AdStatETheme = '/compress/cachecontent/blue/css?version=1.0';
		
	var cssTag = document.createElement('link');
	cssTag.setAttribute('rel','stylesheet');
	cssTag.setAttribute('type','text/css');
	cssTag.setAttribute('href',AdStatETheme);
	document.getElementsByTagName('head')[0].appendChild(cssTag);
	
//	cssTag = document.createElement('link');
//	cssTag.setAttribute('rel','stylesheet');
//	cssTag.setAttribute('type','text/css');
//	if(AdStatETheme.indexOf('blue.css')!=-1) {
//		cssTag.setAttribute('href','/css/mytheme-blue.css');
//	}
//	else if(AdStatETheme.indexOf('xtheme-tp.css')!=-1) {
//		cssTag.setAttribute('href','/css/mytheme-gray.css');
//	}
//	else if(AdStatETheme.indexOf('gray.css')!=-1) {
//		cssTag.setAttribute('href','/css/mytheme-gray.css');
//	}
//	else if(AdStatETheme.indexOf('silverCherry.css')!=-1) {
//		cssTag.setAttribute('href','/css/mytheme-silverCherry.css');
//	}
//	else if(AdStatETheme.indexOf('pink.css')!=-1) {
//		cssTag.setAttribute('href','/css/mytheme-pink.css');
//	}
//	else if(AdStatETheme.indexOf('indigo.css')!=-1) {
//		cssTag.setAttribute('href','/css/mytheme-indigo.css');
//	}
//	
//	
//	document.getElementsByTagName('head')[0].appendChild(cssTag);
});