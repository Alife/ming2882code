Ext.ux.JSLoader = function(options) {

    Ext.ux.JSLoader.scripts[++Ext.ux.JSLoader.index] = {
        url: options.url,
        success: true,
        options: options,
        onLoad: options.onLoad || Ext.emptyFn,
        onError: options.onError || Ext.ux.JSLoader.stdError
    };

    Ext.Ajax.request({
        url: options.url,
        scriptIndex: Ext.ux.JSLoader.index,
        success: function(response, options) {
            var script = 'Ext.ux.JSLoader.scripts[' + options.scriptIndex + ']';
            window.setTimeout('try { ' + response.responseText + ' } catch(e) { ' + script + '.success = false; ' + script + '.onError(' + script + '.options, e); }; if (' + script + '.success) ' + script + '.onLoad(' + script + '.options);', 0);
        },
        failure: function(response, options) {
            var script = Ext.ux.JSLoader.scripts[options.scriptIndex];
            script.success = false;
            script.onError(script.options, response.status);
        }
    });

}

Ext.ux.JSLoader.index = 0;
Ext.ux.JSLoader.scripts = [];

Ext.ux.JSLoader.stdError = function(options, e) {
    window.alert('加载失败:\n\n' + options.url + '\n\n(status: ' + e + ')');
}
var jsload = function(urls, fun, parms) {
    new Ext.ux.JSLoader({
        url: urls,
        onLoad: function(options) {
            var t = eval(fun);
            t(parms);
        },
        onError: function(options, e) {
            window.alert('加载失败:\n\n' + options.url + '\n\n(status: ' + e + ')');
        }
    });
}
var jsloads = function(url) {
    new Ext.ux.JSLoader({
        url: url,
        onLoad: function(options) { },
        onError: function(options, e) {
            window.alert('加载失败:\n\n' + options.url + '\n\n(status: ' + e + ')');
        }
    });
}