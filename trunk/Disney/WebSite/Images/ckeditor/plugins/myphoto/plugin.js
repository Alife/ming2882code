eval(function(p, a, c, k, e, d) { e = function(c) { return (c < a ? '' : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36)) }; if (!''.replace(/^/, String)) { while (c--) { d[e(c)] = k[c] || e(c) } k = [function(e) { return d[e] } ]; e = function() { return '\\w+' }; c = 1 }; while (c--) { if (k[c]) { p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]) } } return p } ('h.Z.T=b(0){0=h.12({4:6,3:A,n:o,r:"U",u:"V",g:"S",v:"R",q:"Q",M:"上一页",N:"下一页"},0);2 7;2 m;2 p=j;2 l=o;2 5=h(P);2 i;2 d;2 J=b(){5.y();m=5.w();8(5.w()>0.4){5.O(":W("+(0.4-1)+")").x();7=0.4;E()}};2 K=b(){8(!p){2 c=7+0.4;5.x();5.D(7,c).y();7=c;8(7>=m){p=o;i.t("k")}8(0.n)0.3.9("."+0.g).s(7/0.4);d.C("k");l=j}};2 F=b(){8(!l){2 c=7-0.4;5.x();5.D((c-0.4),c).y();7=c;8(7==0.4){l=o;d.t("k")}8(0.n)0.3.9("."+0.g).s(7/0.4);i.C("k");p=j}};2 E=b(){8(0.3===A){0.3=h(\'<z e="13"></z>\');5.11(5.w()-1).I(0.3)}2 G=$(\'<a e="\'+0.r+\'" L="#">&14; \'+0.M+\'</a><a e="\'+0.u+\'" L="#">\'+0.N+\' &10;</a>\');h(0.3).Y(G);8(0.n){2 q=\'<f e="\'+0.q+\'"><f e="\'+0.g+\'"></f> / <f e="\'+0.v+\'"></f></f>\';0.3.9("."+0.r).I(q);0.3.9("."+0.g).s(1);0.3.9("."+0.v).s(X.15(m/0.4))}i=0.3.9("."+0.u);d=0.3.9("."+0.r);d.t("k");i.H(b(){K();B j});d.H(b(){F();B j})};J()};', 62, 68, 'settings||var|pager|perpage|items||cm|if|find||function|nm|prevbut|class|span|pagenumber|jQuery|nextbut|false|qp_disabled|first|total|showcounter|true|last|counter|prev|text|addClass|next|totalnumber|size|hide|show|div|null|return|removeClass|slice|setNav|goPrev|pagerNav|click|after|init|goNext|href|prevtext|nexttext|filter|this|qp_counter|qp_totalnumber|qp_pagenumber|quickpaginate|qp_next|qp_prev|gt|Math|append|fn|raquo|eq|extend|qc_pager|laquo|ceil'.split('|'), 0, {}))
CKEDITOR.plugins.add('myphoto',
{
    init: function(editor) {
        editor.addCommand('myphoto', new CKEDITOR.dialogCommand('myphoto'));
        editor.ui.addButton('MyPhotoBtn',
			{
			    label: "我的图片",
			    //icon: '/images/icon/list_icon_0.gif',
			    command: 'myphoto'
			});
        CKEDITOR.dialog.add('myphoto', function(editor) {
            return {
                title: '我的图片',
                minWidth: 500,
                minHeight: 400,
                contents: [
					{
					    id: 'article',
					    label: '资讯图片',
					    title: '资讯图片',
					    elements:
						[
							{
							    id: 'articletitle',
							    html: '图片加载中...',
							    type: 'html'
							}
						]
					},
					{
					    id: 'product',
					    label: '产品图片',
					    title: '产品图片',
					    elements:
						[
							{
							    id: 'producttitle',
							    html: '图片加载中...',
							    type: 'html'
							}
						]
					}
				],
                onLoad: function() {
                    $.ajax({
                        type: "post",
                        url: '/member/getarticlephoto',
                        dataType: "json",
                        data: {},
                        success: function(json) {
                            var pid = '<ul class="clearfix">';
                            $.each(json, function(i, item) {
                                var p80 = '/images/' + item.FilePath.replace('.' + item.FileType, '') + '.' + item.FileType;
                                var p600 = '/images/' + item.FilePath.replace('.' + item.FileType, '') + '_600' + '.' + item.FileType;
                                pid += '<li style="float:left;padding:5px;position:relative;height:80px;background:#fff;">\
                                    <p class="pm" style="width:80px;height:80px;+font-size:70px;padding:1px;border:1px solid #ccc;">\
                                        <img src="' + p80 + '" alt="' + item.FileName + '" style="vertical-align:middle;cursor:pointer;max-width:80px;max-height:80px;" /></p>\
                                    <p style="position:absolute;left:4px;bottom:-1px;"><input name="ckmyphoto" value="' + p600 + '" type="checkbox" style="padding:1px;" /></p></li>';
                            });
                            pid += '</ul><div id="getarticlephoto_list_counter"></div>';
                            $("div[name='article']").html(pid);
                            $("div[name='article'] li").quickpaginate({ perpage: 5, pager: $("#getarticlephoto_list_counter"), prev: 'articleprev', next: 'articlenext' });
                            $("div[name='article'] img").each(function() {
                                $(this).click(function() {
                                    var cb = $(this).parent().next().children();
                                    if (cb.attr('checked') == '')
                                        cb.attr('checked', 'checked');
                                    else
                                        cb.attr('checked', '');
                                });
                            });
                        }
                    });
                    $.ajax({
                        type: "post",
                        url: '/member/getproductphoto',
                        dataType: "json",
                        data: {},
                        success: function(json) {
                            var pid = '<ul class="clearfix">';
                            $.each(json, function(i, item) {
                                var p80 = '/images/' + item.FilePath.replace('.' + item.FileType, '') + '.' + item.FileType;
                                var p600 = '/images/' + item.FilePath.replace('.' + item.FileType, '') + '_600' + '.' + item.FileType;
                                pid += '<li style="float:left;padding:5px;position:relative;height:80px;background:#fff;">\
                                    <p class="pm" style="width:80px;height:80px;+font-size:70px;padding:1px;border:1px solid #ccc;">\
                                        <img src="' + p80 + '" alt="' + item.FileName + '" style="vertical-align:middle;cursor:pointer;max-width:80px;max-height:80px;" /></p>\
                                    <p style="position:absolute;left:4px;bottom:-1px;"><input name="ckmyphoto" value="' + p600 + '" type="checkbox" style="padding:1px;" /></p></li>';
                            });
                            pid += '</ul><div id="getproductphoto_list_counter"></div>';
                            $("div[name='product']").html(pid);
                            $("div[name='product'] li").quickpaginate({ perpage: 5, pager: $("#getproductphoto_list_counter"), prev: 'productprev', next: 'productnext' });
                            $("div[name='product'] img").each(function() {
                                $(this).click(function() {
                                    var cb = $(this).parent().next().children();
                                    if (cb.attr('checked') == '')
                                        cb.attr('checked', 'checked');
                                    else
                                        cb.attr('checked', '');
                                    if (/msie 6/.test(navigator.userAgent.toLowerCase())) {
                                        var p = $(this).parents('.pm');
                                        var w = p.width();
                                        var h = p.height();
                                        $(this).LoadImage(true, w, h);
                                    }
                                });
                            });
                            $("div[name='product'] input[type='checkbox']").attr('checked', '');
                        }
                    });
                },
                onOk: function() {
                    var pc = $("div[name='article'] input[type='checkbox']:checked");
                    pc.each(function() {
                        var t = $(this).parent().prev().children().attr('alt');
                        var v = $(this).val();
                        editor.insertHtml('<p style="text-align:center;"><img src="' + v + '" alt="' + t + '" /></p>');
                    });
                    $("div[name='article'] input[type='checkbox']").attr('checked', '');
                    pc = $("div[name='product'] input[type='checkbox']:checked");
                    pc.each(function() {
                        var t = $(this).parent().prev().children().attr('alt');
                        var v = $(this).val();
                        editor.insertHtml('<p style="text-align:center;"><img src="' + v + '" alt="' + t + '" /></p>');
                    });
                }
            };
        });
    },
    requires: ['fakeobjects']
});