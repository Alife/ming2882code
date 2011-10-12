function rmbMoneyRender(v, cellmeta, record, rowIndex, columnIndex, stor){
	if(!v && v != 0)
		return '';
    v = (Math.round((v-0)*100))/100;
    v = (v == Math.floor(v)) ? v : ((v*10 == Math.floor(v*10)) ? v + "0" : v);
    v = String(v);
    var ps = v.split('.');
    var whole = ps[0];
    var r = /(\d+)(\d{3})/;
    while (r.test(whole)) {
        whole = whole.replace(r, '$1' + ',' + '$2');
    }
    v = whole;
    if(v.charAt(0) == '-'){
    	return '-' + v.substr(1);
    }
    return v;
};

function renderRatio(value, cellmeta, record, rowIndex, columnIndex, stor) {
	if(!value && value != 0) return '';
	return value + '%';
}


function rmbMoneyRenderOld(v, cellmeta, record, rowIndex, columnIndex, stor){
    v = (Math.round((v-0)*100))/100;
    v = (v == Math.floor(v)) ? v + ".00" : ((v*10 == Math.floor(v*10)) ? v + "0" : v);
    v = String(v);
    var ps = v.split('.');
    var whole = ps[0];
    var sub = ps[1] ? '.'+ ps[1] : '.00';
    var r = /(\d+)(\d{3})/;
    while (r.test(whole)) {
        whole = whole.replace(r, '$1' + ',' + '$2');
    }
    v = whole + sub;
    if(v.charAt(0) == '-'){
        return '-￥' + v.substr(1);
    }
    return "￥" +  v;
};