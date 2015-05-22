/// <reference path="../intellisense/jquery-1.2.6-vsdoc-cn.js" />
(function($) {
    $.fn.DhoverClass = function(className) {
        return $(this).hover(function() { $(this).addClass(className); }, function() { $(this).removeClass(className); });
    }
    function getDulyOffset(target, w, h) {
        var pos = target.offset();
        var height = target.outerHeight();
        var newpos = { left: pos.left, top: pos.top + height - 1 }
        var bw = $(document).width();
        var bh = $(document).height();
        if ((newpos.left + w) >= bw) {
            newpos.left = bw - w - 2;
        }
        if ((newpos.top + h) >= bh && bw > newpos.top) {
            // newpos.top = pos.top - h - 2;
            newpos.top = bh-h;
        }
        return newpos;
    }
    function returnfalse() { return false; };
    $.fn.smartselect = function(o) {
        var options = $.extend({
            vinputid: null,
            contentEl: "",
            dropheight: 170,
            dropwidth: 250,
            cssClass: "bbit-smartselect-dropdown",
            containerCssClass: "smartselect-container",
            dropwidth: false,
            dropheight: "auto",
            rp: 5,
            page: 1,
            title: [{ head: "标题1", width: "110px" }, { head: "标题2", width: "110px"}],
            autoheight: true,
            callback: false
        }, o);
        var me = $(this);
        var v;
        var rand = new Date().getTime();
        if (options.vinputid) {
            v = $("#" + options.vinputid);
        }
        var requireCss = { height: 18, "padding-top": "1px", "padding-bottom": "1px" };
        me.css(requireCss).addClass(options.cssClass).DhoverClass("hover");
        if (!options.dropwidth) {
            options.dropwidth = me.outerWidth();
        }
        var d = $("<div/>").addClass(options.containerCssClass).addClass("bbit-smartselect-grid")
                           .css({ position: "absolute", "z-index": "999", "overflow": "auto", width: options.dropwidth, display: "none", "border": "solid 1px #849BBA", background: "#fff" })
                           .click(function(event) { event.stopPropagation(); })
                           .appendTo($("body"));
        //
        var qdiv = $("<div class='query'/>");
        qdiv.html("<input id='" + ("text_" + rand) + "' type='text' class='querytext'/><input class='querybtn' type='button' id='" + ("query_" + rand) + "' value='查询' title='查询' style='margin-left:2px;'/><input class='querybtn' id='" + ("reset_" + rand) + "' type='button' value='重置' title='重置' style='margin-left:2px;'/>");
        d.append(qdiv);
        $("#text_" + rand).css({ width: options.dropwidth - 90 });
        $("#text_" + rand).keydown(function(e) {
            if (e.keyCode == 13) {
                querydata();
            }
        });
        $("#query_" + rand).click(function() {
            querydata();
        });
        $("#reset_" + rand).click(function() {
            $("#text_" + rand).val("");
        });
        var gridcontainer = $("<div class='gridcontainer'/>").css({ width: options.dropwidth, height: options.dropheight });
        d.append(gridcontainer);
        var pagehtml = '<div class="pDiv2" style="margin:0 auto; width:100%;"><div class="pGroup"><div class="pFirst pButton" title="转到第一页"><span></span></div><div class="pPrev pButton" title="转到上一页"><span></span></div> </div><div class="btnseparator"></div> <div class="pGroup"><span class="pcontrol">当前 <input type="text" size="1" value="1" /> ,总页数 <span> 1 </span></span></div><div class="btnseparator"></div><div class="pGroup"> <div class="pNext pButton" title="转到下一页"><span></span></div><div class="pLast pButton" title="转到最后一页"><span></span></div></div><div class="pGroup"><span class="pPageStat"></span></div></div>';
        var page = $("<div style='width:100%; '/>").addClass("pDiv").html(pagehtml);
        d.append(page);
        
        $('.pReload').click(function() { populate(); });
        $('.pFirst').click(function() { changePage('first'); });
        $('.pPrev').click(function() { changePage('prev'); });
        $('.pNext').click(function() { changePage('next'); });
        $('.pLast').click(function() { changePage('last'); });
        $('.pcontrol input').keydown(function(e) { if (e.keyCode == 13) changePage('input'); });
        if ($.browser.msie && $.browser.version < 7) $('.pButton').hover(function() { $(this).addClass('pBtnOver'); }, function() { $(this).removeClass('pBtnOver'); });
               if (options.autoheight) {
                  // d.css("max-height", options.dropheight);
               }
               else {
                   ///d.css("height", options.dropheight);
               }

        if ($.browser.msie) {
            if (parseFloat($.browser.version) <= 6) {
                //var ie6hack = $("<div/>").css({ position: "absolute", "z-index": "-1", "overflow": "hidden", "height": "100%", width: "100%" });
                // ie6hack.append($('<iframe style="position:absolute;z-index:-1;width:100%;height:100%;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe>'));
                // d.append(ie6hack);
            }
        }
        function querydata() {
            options.newp = 1;
            options.page = 1;
            populate();
        } //querydata
        function populate() { //get latest data
            if (!options.url) return false;
            if (!options.newp) options.newp = 1;
            if (options.page > options.pages) options.page = options.pages;
            var querytext = $("#text_" + rand).val();
            var param = [
					 { name: 'page', value: options.newp }
					, { name: 'rp', value: options.rp }
					, { name: "query", value: querytext }
				];
            if (options.extParam) {
                for (var pi = 0; pi < options.extParam.length; pi++) param[param.length] = options.extParam[pi];
            }
            $.ajax({
                type: "post",
                url: options.url,
                data: param,
                dataType: "json",
                success: function(data) { addData(data); },
                error: function(data) { alert("获取数据发生异常;"); }
            });
        };
        function changePage(ctype) { //change page
            switch (ctype) {
                case 'first': options.newp = 1; break;
                case 'prev': if (options.page > 1) options.newp = parseInt(options.page) - 1; break;
                case 'next': if (options.page < options.pages) options.newp = parseInt(options.page) + 1; break;
                case 'last': options.newp = options.pages; break;
                case 'input':
                    var nv = parseInt($('.pcontrol input').val());
                    if (isNaN(nv)) nv = 1;
                    if (nv < 1) nv = 1;
                    else if (nv > options.pages) nv = options.pages;
                    $('.pcontrol input').val(nv);
                    options.newp = nv;
                    break;
            }

            if (options.newp == options.page) return false;
            populate();

        };
        function buildpager() {
            $('.pcontrol input').val(options.newp);
            $('.pcontrol span').html(options.pages);
        };
        function addData(data) {
            var tr = [];
            var temp = options.total;
            if (data.total < 0) {
                options.total = temp;
            }
            else {
                options.total = data.total; //总数
                options.pages = Math.ceil(options.total / options.rp); // 总页数
            }
            options.page = data.page;

            var table = $("<table class='grid'/>").css({ width: options.dropwidth - 2 });
            var headt = "<thead><tr><td style='width:20px;'></td><td style='width:{0}'>{1}</td><td style='width:{2}'>{3}</td></tr></thead>";
            var titlearr = [];
            titlearr.push(options.title[0].width);
            titlearr.push(options.title[0].head);
            titlearr.push(options.title[1].width);
            titlearr.push(options.title[1].head);
            tr.push(StrFormatNoEncode(headt, titlearr));
            //tr.push("<thead><tr><td style='width:20px;'></td><td style='width:100px;'>课程</td><td style='width:100px;'>教师</td></tr></thead>");
            var te = "<tr class='{4}'><td ><label><input type='radio' class='noborder' value='{0}' key='{3}' name='radio_smartselect' /></label></td><td>{1}</td><td>{2}</td></tr>";
            $(data.rows).each(function(i, item) {
                var cell = this.cell;
                var tempc = [];
                tempc.push(cell[0]);
                tempc.push(cell[1]);
                tempc.push(cell[2]);
                var da = cell.join("_FG$SP_");
                var trclass = "null";
                tempc.push(da);
                if (i % 2) {
                    trclass = "erow";
                }
                tempc.push(trclass);
                tr.push(StrFormatNoEncode(te, tempc));
            });
            table.html("<tbody>" + tr.join('') + "<tbody/>");
            gridcontainer.html("");
            // gridcontainer.append(qdiv);
            gridcontainer.append(table);
            $("tr", table).DhoverClass("trOver");
            $("tr", table).click(function() {
                //$(":radio", this).trigger("click");
                //$(":radio", this).attr("checked",true);
            });
            //FFFFBB;
            $("table.grid :radio").click(function() {
                var id = $(this).val();
                var key = $(this).attr("key");
                var cell = key.split("_FG$SP_");
                if (options.callback)
                    options.callback(id, cell);
                d.hide();
            });
            buildpager();
        };
        me.click(function() {
            var m = this;
            options.newp = 1;
            options.page = 1;
            populate();
            var pos = getDulyOffset(me, options.dropwidth, options.dropheight+60);
            d.css(pos);
            d.show();
            $("#text_" + rand).focus();
            //            if ($.browser.msie) {
            //                if (parseFloat($.browser.version) <= 6) {
            //                    var h = d.height();
            //                    if (h > options.dropheight) {
            //                       // d.height(options.dropheight);
            //                    }
            //                }
            //            }
            $(document).one("click", function(event) { d.hide(); });
            return false;
        });
        return me;
    }

})(jQuery);