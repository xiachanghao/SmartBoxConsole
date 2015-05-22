/*! Beyondbit.Web.Windows - v1.0 - 2013-07-08
* http://www.beyondbit.com
* Copyright (c) 2013 Beyondbit.com */
(function ($) {
    Beyondbit.register("Web", function () {
        return {
            WindowManager: function () {
                var stance = new Object();
                stance.findByID = function (id, context) {
                    context = context || window;
                    return context.Beyondbit.Web.WindowManagerHelper.findWindow(id);
                };
                stance.findForParentByID = function (id) {
                    return this.findByID(id, parent);
                };
                stance.create = function (options, context) {
                    context = context || window;
                    return new context.Beyondbit.Web.Window(options);
                };
                return stance;
            } (),
            Window: function (options) {
                var options = $.extend({
                    id: "",
                    shadow: false, 		//是否阴影
                    draggable: true, 	//是否拖动
                    loading: "none", 	//加载效果,三种可选值,none,away,normal
                    width: 400, 	    //宽度
                    height: 100, 		//高	度
                    content: null, 		//内容，如果此不为空，则为文本内容显示，否则为Ifame方式显示
                    title: "", 			//显示
                    url: "", 			//iframe url
                    data: {},
                    requestData: {}, 	//url 请示的Request数据
                    onOk: null, 			// 调用ok()方法后，回调方法	
                    onClose: null, 		// 调用close(code)方法后，回调方法	
                    onCancel: null			// 调用cancel()方法后，回调方法	
                }, options);

                this.ID = options.id;
                if (!this.ID || this.ID == "") {
                    throw "组件Id不能为空";
                }
                var loader = this;
                this.Data = options.data;

                var manager = Beyondbit.Web.WindowManager;
                var tempZjObj = manager.findByID(this.ID);
                if (tempZjObj) {
                    return tempZjObj;
                }
                Beyondbit.Web.WindowManagerHelper.addWindow(this.ID, this);

                var helper = Beyondbit.Web.WindowHelper;

                this.ElementID = 'mzj_' + this.ID + '_box';
                var defineProp = { frameUrl: "", rootElement: null, contentMode: "iframe" };

                var tDiv = "";
                tDiv = tDiv + '<div id="' + this.ElementID + '_shadow" class="dialogskin_b_shadow hide"></div>';
                tDiv = tDiv + '<div id="' + this.ElementID + '" class="dialogskin_b dialogskin_b_box_shadow" style="display:none;">'
                                + '<div class="dialog_box">'
                                 + '<div class="dialog_header">'
                                 + '<h3>' + options.title + '</h3>'
                                 + '<a href="" class="dialog_btn_back" title="后退" onclick="return false;">&nbsp;</a>'
                                 + '<a href="" class="dialog_btn_close" title="关闭" onclick="return false;">&nbsp;</a>'
                                 + '</div><div class="dialog_container">'
                                 + '<iframe src=""  frameborder="0" frameborder="0" scrolling="auto"  /><div class="dialog_container_content"></div>'
                                 + '</div><div class="dialog_footer"></div><div class="dialog_loader hide">'
								+ '		<div class="dialog_loadbox">'
								+ '			<h4>正在加载中...</h4>'
								+ '			<p></p>'
								+ '		</div>'
								+ '	</div>'
                             + '</div>';
                $("body").append(tDiv);

                var rootElement = defineProp.rootElement = $("#" + this.ElementID);
                var rootElementShadow = $("#" + this.ElementID + "_shadow");

                $("a.dialog_btn_close", rootElement).click(function () {
                    if (options.onClose) {
                        options.onClose("close");
                    }
                    loader.hide();
                });
                if (options.draggable && rootElement.draggable) {
                    $(".dialog_header > h3", rootElement).css({ cursor: "move" });
                    rootElement.draggable({ cursor: "move", handle: ".dialog_header > h3", containment: "body" });
                }
                this.getOptions = function () {
                    return options;
                };
                this.show = function () {
                    loader.message({ hide: true });
                    rootElement.css("z-index", Beyondbit.Web.WindowManagerHelper.zIndex).show();
                    if (options.loading !== "none") {
                        loader.message(loader, { title: "正在加载中...", content: "" }, defineProp);
                    }
                    if (options.shadow) {
                        rootElement.removeClass("dialogskin_b_box_shadow");
                        rootElementShadow.css("z-index", Beyondbit.Web.WindowManagerHelper.zIndex).show();
                    } else {
                        rootElement.addClass("dialogskin_b_box_shadow");
                    }
                    Beyondbit.Web.WindowManagerHelper.zIndex++;
                    
                    if (defineProp.contentMode == "iframe") {
                        rootElement.find("iframe").attr("src", defineProp.frameUrl).show().siblings().hide();
                    } else {
                        rootElement.find(".dialog_container_content").show().siblings().hide();
                    }
                    return loader;
                };
                this.message = function (options) {
                    var config = $.extend({
                        hide: false,
                        title: "正在加载中...",
                        content: ""
                    }, options);
                    helper.message(loader, config, defineProp);
                    rootElement.show();
                    return loader;
                };

                this.setOptions = function (option) {
                    options = $.extend(options, option);
                    helper.initWindow(loader, options, defineProp);
                    return loader;
                };

                this.close = function (resultCode, data) {
                    if (options.onClose) {
                        options.onClose(resultCode, data);
                    }
                    this.hide();
                    return loader;
                };
                this.ok = function (data) {
                    var isHandler = false;
                    if (options.onOk) {
                        isHandler = true;
                        options.onOk(data);
                    }
                    if (options.onClose) {
                        isHandler = true;
                        options.onClose("ok", data);
                    }
                    if (!isHandler) {
                        this.hide();
                    }
                    return loader;
                };
                this.cancel = function (data) {
                    var isHandler = false;
                    if (options.onCancel) {
                        isHandler = true;
                        options.onCancel(data);
                    }
                    if (options.onClose) {
                        isHandler = true;
                        options.onClose("cancel", data);
                    }
                    if (!isHandler) {
                        this.hide();
                    }
                    return loader;
                };
                this.hide = function () {
                    rootElement.hide();
                    rootElementShadow.hide();
                    return loader;
                };

                helper.initWindow(loader, options, defineProp);

                return loader;
            },
            WindowHelper: {
                initWindow: function (sender, options, defineProp) {
                    var contentMode = options.content ? "content" : "iframe";
                    var tElement = defineProp.rootElement;

                    var tempTems = options.url.indexOf('?') > -1 ? "&" : "?";
                    var strUrl = options.url + tempTems + "DialogId=" + sender.ID;
                    for (var key in options.requestData) {
                        var item = options.requestData[key];
                        strUrl = strUrl + "&" + key + "=" + encodeURIComponent(item);
                    }
                    defineProp.frameUrl = strUrl;
                    defineProp.contentMode = contentMode;
                    if (contentMode == "iframe") {
                        if ($("iframe", tElement).length > 0) {
                            $("iframe", tElement)[0].onreadystatechange = $("iframe", tElement)[0].onload = function () {
                                if (this.readyState && this.readyState != 'complete') {
                                    if (options.loading == "away") {
                                        //$(".dialog_loader", tElement).show();
                                        Beyondbit.Web.WindowHelper.message(sender, { title: "正在加载中...", content: "" }, defineProp);
                                    }
                                    return;
                                }
                                else {
                                    //$(".dialog_loader", tElement).hide();
                                    Beyondbit.Web.WindowHelper.message(sender, { hide: true }, defineProp);
                                }
                            };
                        }
                    }
                    $("div.dialog_container_content", tElement).html(options.content);
                    $(".dialog_header h3", tElement).html(options.title);

                    var _height = $(window).height() - 20;
                    if (_height > options.height) {
                        _height = options.height;
                    }
                    var _width = $(window).width() - 20;
                    if (_width > options.width) {
                        _width = options.width;
                    }

                    tElement.css({ "width": _width + "px", "height": _height + "px", "margin-left": "-" + _width / 2 + "px", "margin-top": "-" + _height / 2 + "px" });
                    tElement.find("div.dialog_container").height(_height - $(".dialog_header", tElement).height());

                    return sender;
                },
                message: function (sender, config, defineProp) {
                    if (config.hide == true) {
                        $(".dialog_loader", defineProp.rootElement).hide();
                    } else {
                        $(".dialog_loader", defineProp.rootElement).find("h4").html(config.title).end().find("p").html(config.content);
                        $(".dialog_loader", defineProp.rootElement).show();
                    }
                }
            },
            WindowManagerHelper: {
                zIndex: 100,
                _Queue: {},
                addWindow: function (key, objModel) {
                    Beyondbit.Web.WindowManagerHelper._Queue[key] = objModel;
                },
                findWindow: function (key) {
                    var helper = Beyondbit.Web.WindowManagerHelper;
                    for (var item in helper._Queue) {
                        if (item == key) {
                            return helper._Queue[key];
                        }
                    }
                    return null;
                }

            }
        };
    });
})(jQuery);