(function(a){a.fn.ajaxSubmit=function(c){if(!this.length){b("ajaxSubmit: skipping submit process - no element selected");return this}if(typeof c=="function")c={success:c};c=a.extend({url:this.attr("action")||window.location.toString(),type:this.attr("method")||"GET"},c||{});var h={};this.trigger("form-pre-serialize",[this,c,h]);if(h.veto){b("ajaxSubmit: submit vetoed via form-pre-serialize trigger");return this}if(c.beforeSerialize&&c.beforeSerialize(this,c)===false){b("ajaxSubmit: submit aborted via beforeSerialize callback");return this}var g=this.formToArray(c.semantic);if(c.data){c.extraData=c.data;for(var f in c.data)if(c.data[f]instanceof Array)for(var o in c.data[f])g.push({name:f,value:c.data[f][o]});else g.push({name:f,value:c.data[f]})}if(c.beforeSubmit&&c.beforeSubmit(g,this,c)===false){b("ajaxSubmit: submit aborted via beforeSubmit callback");return this}this.trigger("form-submit-validate",[g,this,c,h]);if(h.veto){b("ajaxSubmit: submit vetoed via form-submit-validate trigger");return this}var m=a.param(g);if(c.type.toUpperCase()=="GET"){c.url+=(c.url.indexOf("?")>=0?"&":"?")+m;c.data=null}else c.data=m;var d=this,e=[];c.resetForm&&e.push(function(){d.resetForm()});c.clearForm&&e.push(function(){d.clearForm()});if(!c.dataType&&c.target){var n=c.success||function(){};e.push(function(b){a(c.target).html(b).each(n,arguments)})}else c.success&&e.push(c.success);c.success=function(f,b){for(var a=0,g=e.length;a<g;a++)e[a].apply(c,[f,b,d])};for(var k=a("input:file",this).fieldValue(),l=false,i=0;i<k.length;i++)if(k[i])l=true;if(c.iframe||l)if(a.browser.safari&&c.closeKeepAlive)a.get(c.closeKeepAlive,j);else j();else a.ajax(c);this.trigger("form-submit-notify",[this,c]);return this;function j(){var g=d[0];if(a(":input[name=submit]",g).length){alert('Error: Form elements must not be named "submit".');return}var e=a.extend({},a.ajaxSettings,c),l=jQuery.extend(true,{},a.extend(true,{},a.ajaxSettings),e),m="jqFormIO"+(new Date).getTime(),i=a('<iframe id="'+m+'" name="'+m+'" />'),f=i[0];if(a.browser.msie||a.browser.opera)f.src='javascript:false;document.write("");';i.css({position:"absolute",top:"-1000px",left:"-1000px"});var b={aborted:0,responseText:null,responseXML:null,status:0,statusText:"n/a",getAllResponseHeaders:function(){},getResponseHeader:function(){},setRequestHeader:function(){},abort:function(){this.aborted=1;i.attr("src","about:blank")}},k=e.global;k&&!a.active++&&a.event.trigger("ajaxStart");k&&a.event.trigger("ajaxSend",[b,e]);if(l.beforeSend&&l.beforeSend(b,l)===false){l.global&&jQuery.active--;return}if(b.aborted)return;var n=0,o=0,j=g.clk;if(j){var p=j.name;if(p&&!j.disabled){c.extraData=c.extraData||{};c.extraData[p]=j.value;if(j.type=="image"){c.extraData[name+".x"]=g.clk_x;c.extraData[name+".y"]=g.clk_y}}}setTimeout(function(){var k=d.attr("target"),l=d.attr("action");d.attr({target:m,method:"POST",action:e.url});!c.skipEncodingOverride&&d.attr({encoding:"multipart/form-data",enctype:"multipart/form-data"});e.timeout&&setTimeout(function(){o=true;h()},e.timeout);var b=[];try{if(c.extraData)for(var j in c.extraData)b.push(a('<input type="hidden" name="'+j+'" value="'+c.extraData[j]+'" />').appendTo(g)[0]);i.appendTo("body");f.attachEvent?f.attachEvent("onload",h):f.addEventListener("load",h,false);g.submit()}finally{d.attr("action",l);k?d.attr("target",k):d.removeAttr("target");a(b).remove()}},10);function h(){if(n++)return;f.detachEvent?f.detachEvent("onload",h):f.removeEventListener("load",h,false);var g=0,d=true;try{if(o)throw"timeout";var j,c;c=f.contentWindow?f.contentWindow.document:f.contentDocument?f.contentDocument:f.document;if(c.body==null&&!g&&a.browser.opera){g=1;n--;setTimeout(h,100);return}b.responseText=c.body?c.body.innerHTML:null;b.responseXML=c.XMLDocument?c.XMLDocument:c;b.getResponseHeader=function(b){var a={"content-type":e.dataType};return a[b]};if(e.dataType=="json"||e.dataType=="script"){var l=c.getElementsByTagName("textarea")[0];b.responseText=l?l.value:b.responseText}else if(e.dataType=="xml"&&!b.responseXML&&b.responseText!=null)b.responseXML=q(b.responseText);j=a.httpData(b,e.dataType)}catch(m){d=false;a.handleError(e,b,"error",m)}if(d){e.success(j,"success");k&&a.event.trigger("ajaxSuccess",[b,e])}k&&a.event.trigger("ajaxComplete",[b,e]);k&&!--a.active&&a.event.trigger("ajaxStop");e.complete&&e.complete(b,d?"success":"error");setTimeout(function(){i.remove();b.responseXML=null},100)}function q(b,a){if(window.ActiveXObject){a=new ActiveXObject("Microsoft.XMLDOM");a.async="false";a.loadXML(b)}else a=(new DOMParser).parseFromString(b,"text/xml");return a&&a.documentElement&&a.documentElement.tagName!="parsererror"?a:null}}};a.fn.ajaxForm=function(b){return this.ajaxFormUnbind().bind("submit.form-plugin",function(){a(this).ajaxSubmit(b);return false}).each(function(){a(":submit,input:image",this).bind("click.form-plugin",function(c){var b=this.form;b.clk=this;if(this.type=="image")if(c.offsetX!=undefined){b.clk_x=c.offsetX;b.clk_y=c.offsetY}else if(typeof a.fn.offset=="function"){var d=a(this).offset();b.clk_x=c.pageX-d.left;b.clk_y=c.pageY-d.top}else{b.clk_x=c.pageX-this.offsetLeft;b.clk_y=c.pageY-this.offsetTop}setTimeout(function(){b.clk=b.clk_x=b.clk_y=null},10)})})};a.fn.ajaxFormUnbind=function(){this.unbind("submit.form-plugin");return this.each(function(){a(":submit,input:image",this).unbind("click.form-plugin")})};a.fn.formToArray=function(i){var d=[];if(this.length==0)return d;var b=this[0],j=i?b.getElementsByTagName("*"):b.elements;if(!j)return d;for(var f=0,m=j.length;f<m;f++){var g=j[f],c=g.name;if(!c)continue;if(i&&b.clk&&g.type=="image"){!g.disabled&&b.clk==g&&d.push({name:c+".x",value:b.clk_x},{name:c+".y",value:b.clk_y});continue}var e=a.fieldValue(g,true);if(e&&e.constructor==Array)for(var k=0,n=e.length;k<n;k++)d.push({name:c,value:e[k]});else e!==null&&typeof e!="undefined"&&d.push({name:c,value:e})}if(!i&&b.clk)for(var l=b.getElementsByTagName("input"),f=0,m=l.length;f<m;f++){var h=l[f],c=h.name;c&&!h.disabled&&h.type=="image"&&b.clk==h&&d.push({name:c+".x",value:b.clk_x},{name:c+".y",value:b.clk_y})}return d};a.fn.formSerialize=function(b){return a.param(this.formToArray(b))};a.fn.fieldSerialize=function(c){var b=[];this.each(function(){var f=this.name;if(!f)return;var d=a.fieldValue(this,c);if(d&&d.constructor==Array)for(var e=0,g=d.length;e<g;e++)b.push({name:f,value:d[e]});else d!==null&&typeof d!="undefined"&&b.push({name:this.name,value:d})});return a.param(b)};a.fn.fieldValue=function(e){for(var c=[],d=0,f=this.length;d<f;d++){var g=this[d],b=a.fieldValue(g,e);if(b===null||typeof b=="undefined"||b.constructor==Array&&!b.length)continue;b.constructor==Array?a.merge(c,b):c.push(b)}return c};a.fieldValue=function(b,e){var n=b.name,c=b.type,j=b.tagName.toLowerCase();if(typeof e=="undefined")e=true;if(e&&(!n||b.disabled||c=="reset"||c=="button"||(c=="checkbox"||c=="radio")&&!b.checked||(c=="submit"||c=="image")&&b.form&&b.form.clk!=b||j=="select"&&b.selectedIndex==-1))return null;if(j=="select"){var f=b.selectedIndex;if(f<0)return null;for(var k=[],i=b.options,g=c=="select-one",m=g?f+1:i.length,h=g?f:0;h<m;h++){var d=i[h];if(d.selected){var l=a.browser.msie&&!d.attributes["value"].specified?d.text:d.value;if(g)return l;k.push(l)}}return k}return b.value};a.fn.clearForm=function(){return this.each(function(){a("input,select,textarea",this).clearFields()})};a.fn.clearFields=a.fn.clearInputs=function(){return this.each(function(){var a=this.type,b=this.tagName.toLowerCase();if(a=="text"||a=="password"||b=="textarea")this.value="";else if(a=="checkbox"||a=="radio")this.checked=false;else if(b=="select")this.selectedIndex=-1})};a.fn.resetForm=function(){return this.each(function(){(typeof this.reset=="function"||typeof this.reset=="object"&&!this.reset.nodeType)&&this.reset()})};a.fn.enable=function(a){if(a==undefined)a=true;return this.each(function(){this.disabled=!a})};a.fn.selected=function(b){if(b==undefined)b=true;return this.each(function(){var d=this.type;if(d=="checkbox"||d=="radio")this.checked=b;else if(this.tagName.toLowerCase()=="option"){var c=a(this).parent("select");b&&c[0]&&c[0].type=="select-one"&&c.find("option").selected(false);this.selected=b}})};function b(){a.fn.ajaxSubmit.debug&&window.console&&window.console.log&&window.console.log("[jquery.form] "+Array.prototype.join.call(arguments,""))}})(jQuery);