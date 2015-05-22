/// <reference path="../intellisense/jquery-1.2.6-vsdoc-cn.js" />
; (function($) {
    if (!$.Autocompleter) {
        alert("请先引用jquery.autocomplete.js");
        return;
    }
    $.fn.usbox = function(o) {
        var def = {
            urlOrData: "",
            width: "90%", //宽度
            imageUrl: '../../Themes/Shared/images/s.gif',
            addItem: false,
            removeItem: false,
            clickItem: function() { },
            completeOp: {}
        };
        $.extend(def, o);
        var co = $.extend({ scroll: false, formatItem: function(row, i, max) { return row[0] + "[" + row[1] + "]"; } }, def.completeOp);

        var temp = "<div class='bbit-usbox-item'><a href='javascript:void(0);'><span>${text}</span><input type='hidden' value='${value}'/><img src='" + def.imageUrl + "' alt='点击删除' class='bbit-usbox-del'/></a></div>";

        return this.each(function(e) {
            var me = $(this);
            var id = me.attr("id");
            if (id == null || id == "") {
                id = "usbox_" + new Date().getTime();
            }
            var boxid = id + "_box";
            var inc = $("<div class='bbit-usbox-boxc'/>");
            var input = $("<input type='text' id='" + id + "_inbox' class='bbit-usbox-box' />").appendTo(inc);
            me.addClass("bbit-usbox").width(def.width).append(inc);
            input.autocomplete(def.urlOrData, co).result(function(event, data, formatted) {
                $(this).val("");
                additem(this, [data]);
            });
            me.bind("addboxitem", function(e, data, legnth) { additem(input, data, length); });
            me.bind("clearitems", function(e) { removeAll(); });
            me.click(function() {
                input.focus();
            });
            function removeAll() {
                var box = $("#" + boxid).remove();
            }
            function addToBox(item) {
                var box = $("#" + boxid);
                if (box.length == 0) {
                    box = $("<div id='" + boxid + "' class='bbit-usbox-box'/>").prependTo(me);
                }
                for (var i = 0, l = item.length; i < l; i++) {
                    box.append(item[i]);
                }
            }
            function additem(inc, datas) {
                var r;
                if (def.addItem) {
                    r = def.addItem(datas);
                }
                else {
                    r = datas;
                }
                var length = r.length;
                var items = [];
                for (var i = 0; i < length; i++) {
                    var data = r[i];
                    var tp = $(temp.replace(/\$\{([\w]+)\}/g, function(s1, s2) {
                        if (s2 == "text") {
                            return data[0];
                        }
                        else if (s2 == "value") {
                            return data.join("|");
                        }
                        else {
                            return s1;
                        }
                    }));
                    tp.click(def.clickItem).find("img.bbit-usbox-del").click(removeitem);
                    items.push(tp);
                }
                addToBox(items);

            }
            function removeitem() {
                var r = true;
                var p = $(this).prev();
                var v = p.val();
                var arr = v.split("|");
                if (def.removeItem) {
                    r = def.removeItem(arr);
                }
                if (r != false) {

                    $(this).parent().parent().remove();
                }
            }
            return me;
        });
    };
    $.fn.addboxitem = function(op) {
        $(this).trigger("addboxitem", [op]);
    };
    $.fn.clearitems = function() {
        $(this).trigger("clearitems");
    };
})(jQuery)