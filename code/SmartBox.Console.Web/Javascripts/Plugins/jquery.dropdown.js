(function(a){a.fn.DhoverClass=function(b){return a(this).hover(function(){a(this).addClass(b)},function(){a(this).removeClass(b)})};function b(e,g,f){var c=e.offset(),h=e.outerHeight(),b={left:c.left,top:c.top+h-1},d=a(document).width(),i=a(document).height();if(b.left+g>=d)b.left=d-g-2;if(b.top+f>=i&&d>b.top)b.top=c.top-f-2;return b}a.fn.dropdown=function(i){var c=a.extend({vinputid:null,cssClass:"bbit-dropdown",containerCssClass:"dropdowncontainer",dropwidth:false,dropheight:"auto",autoheight:true,selectedchange:false,items:[],selecteditem:false,parse:{name:"list",render:function(c){var d=this.target,b=a("<ul/>");this.items&&this.items.length>0&&a.each(this.items,function(){var c=this,e=a("<div/>").html(c.text),f=a("<li/>").DhoverClass("hover").append(e).click(function(){d.SelectedChanged(c)});c.classes&&c.classes!=""&&e.addClass(c.classes);b.append(f)});c.append(b)},items:[],setValue:function(){},target:null}},i),e=a(this),f;if(c.vinputid)f=a("#"+c.vinputid);if(c.selecteditem){e.val(c.selecteditem.text);f&&c.selecteditem.value&&f.val(c.selecteditem.value)}var h={height:18,"padding-top":"1px","padding-bottom":"1px"};e.css(h).addClass(c.cssClass).DhoverClass("hover");if(!c.dropwidth)c.dropwidth=e.outerWidth();var d=a("<div/>").addClass(c.containerCssClass).css({position:"absolute","z-index":"999",overflow:"auto",width:c.dropwidth,display:"none",border:"solid 1px #555",background:"#fff"}).click(function(a){a.stopPropagation()}).appendTo(a("body"));if(c.autoheight)d.css("max-height",c.dropheight);else d.css("height",c.dropheight);if(a.browser.msie)if(parseFloat(a.browser.version)<=6){var g=a("<div/>").css({position:"absolute","z-index":"-2",overflow:"hidden",height:"100%",width:"100%"});g.append(a('<iframe style="position:absolute;z-index:-1;width:100%;height:100%;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe>'));d.append(g)}e.click(function(){var h=this;if(d.attr("isinited")!="true"){c.parse.items=c.items;c.selecteditem&&c.parse.setValue.call(d,c.selecteditem);c.parse.render(d);d.attr("isinited","true")}var f=b(e,c.dropwidth,c.dropheight);d.css(f);d.show();if(a.browser.msie)if(parseFloat(a.browser.version)<=6){var g=d.height();g>c.dropheight&&d.height(c.dropheight)}a(document).one("click",function(){d.hide()});return false});e.SelectedChanged=function(a){e.val(a.text);f&&a.value&&f.val(a.value);d.hide();c.selectedchange&&c.selectedchange.apply(e)};e.Cancel=function(){d.hide()};c.parse.target=e;return e}})(jQuery);