/// <reference name="intellisense/jquery-1.2.6-vsdoc.js"/>
; (function($) {
    $.createMenuBar = function(containerId, maxHeight) {
        var _currentUl, _currentheader, _currentIndex;
        var accordion = $("#" + containerId);
        var heads = accordion.find("div.accordionheadercontainer");
        var hl = heads.length;
        var hg = $(heads[0]).find("a>div.accordionhead").outerHeight();       
        var cheight = maxHeight - hg * hl-2;
        var bodys = accordion.find("div.accordionbody");
        _currentIndex = 0;
        _currentheader = $(heads[0]).addClass("activeheader");
        _currentbody = $(bodys[0]).css({ height: cheight, "display": "block" });
        heads.each(function(i) {
            $(this).click(function() {
                if (_currentIndex == i) {
                    return;
                }
                _currentIndex = i;
                _currentheader.removeClass("activeheader");
                _currentbody
                .animate({ height: 5 }, "normal")
                .animate({ height: 0 }, 300, function() { $(this).hide(); });
                _currentbody = $(this).addClass("activeheader").next().css({ height: 0, "display": "" })
                .animate({ height: cheight - 20 }, "normal")
                .animate({ height: cheight  }, 500); 
                _currentheader = $(this);
            });

        });
    }
})(jQuery);