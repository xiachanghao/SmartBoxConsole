(function ($) {
    /**
    @class 分页控件
    @constructs
    @param {Object} options 分页选项
    @exports pager as $.fn.pager
    */
    $.fn.pager = function (options) {
        // 数据传输方法枚举
        var HttpMethod = { GET: "get", POST: "post" };

        // 默认选项
        var defaults = {
            // 对方方式
            align: "center",
            // 页大小
            pageSize: 10,
            // 记录数
            count: 0,
            // 当前页索引
            currentPageIndex: 0,
            // 页按钮数
            pageButtonCount: 10,
            // 分页链接
            pagingUrl: window.location.href,
            // 提交方式
            type: HttpMethod.POST,
            // 附件url参数的元素标识
            urlEncodedDomIds: [],
            // 分页函数
            pageIndexChanging: null,
            // 上页文本
            previousPageText: "上页",
            // 下页文本
            nextPageText: "下页",
            // 首页文本
            firstPageText: "首页",
            // 尾页文本
            lastPageText: "尾页",
            //当前页参数名称
            pageIndexParamName: "pageIndex",
            //模式
            mode: 0
        };

        /**
        @static
        @class 工具类
        @exports util as $.pager.util
        */
        var util = function () {
            return {
                /**
                @static
                @param {String} url URL
                @param {String} name 名称
                @param {String} value 值
                @return {String}
                @description 添加URL参数
                */
                addUrlParam: function (url, name, value) {
                    var str = name + "=" + escape(value);
                    var strArray = url.split("?");
                    var urlSegment = strArray[0];
                    var querySegment = (strArray.length > 1 ? strArray[1] : "");
                    var strArray2 = null;

                    if (querySegment.length > 0) {
                        if (querySegment.indexOf(name) == -1)
                            return (url + "&" + str);

                        strArray2 = querySegment.split("&");
                    }
                    else
                        return (url + "?" + str);

                    var i = 0;
                    var buffer = "";

                    while (true) {
                        if (i >= strArray2.length) {
                            buffer += str;

                            return (urlSegment + "?" + buffer);
                        }

                        var str4 = strArray2[i];

                        if (str4.indexOf(name + "=") != 0 && (str4.length != 0))
                            buffer += (str4 + "&");

                        ++i;
                    }
                },

                /**
                @static
                @param {String} pagingUrl 分页链接
                @param {Int32} pageIndex 当前页索引
                @param {Array} formDomIds 表单元素标识
                @return {String}
                @description 制作分页链接
                */
                makePagingUrl: function (pagingUrl, pageIndex, formDomIds) {
                    var newPagingUrl = pagingUrl;
                    var map = {};
                    map[options.pageIndexParamName] = pageIndex;

                    for (var i = 0; i < formDomIds.length; ++i) {
                        var domId = formDomIds[i];
                        var dom = document.getElementById(domId);

                        if (dom == null)
                            dom = document.getElementsByName(domId);

                        if (dom != null)
                            map[domId] = $(dom).val();
                    }

                    for (var name in map)
                        newPagingUrl = this.addUrlParam(newPagingUrl, name, map[name]);

                    return newPagingUrl;
                },

                /**
                @static
                @param {String} pagingUrl 分页链接
                @param {String} pageText 页文本
                @param {Int32} pageIndex 当前页索引
                @param {Int32} pageSize 页大小
                @return {String}
                @description 制作页呈现
                */
                makePage: function (pagingUrl, pageText, pageIndex, pageSize) {
                    return "<li><a " + options.pageIndexParamName + "=" + pageIndex + ">" + pageText + "</a></li>";
                },

                /**
                @static
                @param {String} pagingUrl 分页链接
                @param {String} pageText 页文本
                @param {Int32} pageIndex 当前页索引
                @param {Int32} pageSize 页大小
                @return {String}
                @description 制作当前页呈现
                */
                makeCurrentPage: function (pagingUrl, pageText, pageIndex, pageSize) {
                    return "<li><span>" + pageText + "</span></li>";
                },
                /**
                @static
                @param {Int32} pageIndex 当前页索引
                @param {Int32} ipian 当前页偏移量，可为负数
                @param {Int32} mode 计算页面索引模式
                @return {int}
                @description 制作下一个索引
                */
                makePageIndex: function (pageIndex, ipian, mode) {
                    return mode == 1 ? pageIndex + 1 + ipian : pageIndex + ipian;
                }
            };
        } ();

        var options = $.extend(defaults, options);
        var selector = $("<ul></ul>");
        $(this).addClass("ui-pager").append(selector);
        var count = options.count;
        var pageSize = options.pageSize;
        var currentPageIndex = options.currentPageIndex;
        var pagingUrl = options.pagingUrl;
        var pageButtonCount = options.pageButtonCount;
        var type = options.type.toLowerCase();
        var urlEncodedDomIds = options.urlEncodedDomIds;
        var pageIndexChanging = options.pageIndexChanging;

        if ((count <= 0) || (pageSize <= 0) || (pageButtonCount <= 0))
            return;

        // 计算页数
        var pageIndex = Math.floor((pageSize + count - 1) / pageSize) - 1;

        if (currentPageIndex > pageIndex)
            return;

        var formFirst = $("form:first");

        if (formFirst.length == 0)
            type = HttpMethod.GET;

        // 输出上页
        if (currentPageIndex > 0)
            selector.append(util.makePage(pagingUrl, options.previousPageText, util.makePageIndex(currentPageIndex, -1, options.mode), pageSize));

        // 计算页边界
        var i = Math.floor(currentPageIndex / pageButtonCount) * pageButtonCount;
        var bound = i + pageButtonCount - 1;

        if (bound > pageIndex)
            bound = pageIndex;

        // 输出页
        for (; i <= bound; ++i) {
            if (i == currentPageIndex)
                selector.append(util.makeCurrentPage(pagingUrl, i + 1, util.makePageIndex(i, 0, options.mode), pageSize));
            else
                selector.append(util.makePage(pagingUrl, i + 1, util.makePageIndex(i, 0, options.mode), pageSize));
        }

        // 输出下页
        if (currentPageIndex < pageIndex)
            selector.append(util.makePage(pagingUrl, options.nextPageText, util.makePageIndex(currentPageIndex, 1, options.mode), pageSize));

        if (options.align == "center") {
            selector.addClass("txtcenter");
        } else if (options.align == "right") {
            selector.addClass("txtright");
        }

        selector
            .find("li > a")
            .click(function () {
                var pageIndex = $(this).attr(options.pageIndexParamName);

                if ($.isFunction(pageIndexChanging)) {
                    pageIndexChanging.call(this, pageIndex, options);
                    return;
                }

                if (type == HttpMethod.POST)
                    formFirst.attr({ "method": type, "action": util.makePagingUrl(pagingUrl, pageIndex, urlEncodedDomIds) }).submit();
                else if (type == HttpMethod.GET)
                    window.location.href = util.makePagingUrl(pagingUrl, pageIndex, urlEncodedDomIds);
            });
    }
})(jQuery);