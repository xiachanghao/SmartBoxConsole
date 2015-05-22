

var Beyondbit = (function () {
    var that = {};
    that.register = function (ns, maker) {
        var path = ns.split(".");
        var curr = that;
        for (var i = 0, len = path.length; i < len; i += 1) {
            if (i == len - 1) {
                if (curr[path[i]] !== undefined) {
                    curr[path[i]] = $.extend({}, curr[path[i]], maker(that));
                    return true;
                }
                curr[path[i]] = maker(that);
                return true;
            }
            if (curr[path[i]] === undefined) {
                curr[path[i]] = {};
            }
            curr = curr[path[i]];
        }
    };
    that.regShort = function (ns, maker) {
        if (that[ns] !== undefined) throw "[" + ns + "] : short : 已经注册";
        that[ns] = maker;
    };
    /**
    @static
    @param {String} objectString 对象的string字符串
    @description 检测对象是否存在
    @example 
    Beyondbit.isObjectExists("Beyondbit.Portal.toLocationMenu")
    */
    that.isObjectExists = function (objectString) {
        try {
            eval("var tempObjetExists = " + objectString);
            return true;
        } catch (e) { }
        return false;
    };

    return that;
})();

