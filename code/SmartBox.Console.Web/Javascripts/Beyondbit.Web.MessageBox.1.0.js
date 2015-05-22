/*! Beyondbit.Web.MessageBox - v1.0 - 2013-07-08
* http://www.beyondbit.com
* Copyright (c) 2013 Beyondbit.com */
(function ($) {
    Beyondbit.register("Web.MessageBox", function () {
        return {
            messages:{
                ok: "确定",
                cancel: "取消",
                yes: "是",
                no: "否"
            },
            Buttons: {
                OK: "ok",
                Cancel: "cancel",
                YES: "yes",
                NO: "no"
            },
            Icons: {
                SUCCESS: "success",
                ERROR: "error",
                INFO: "info",
                QUESTION: "question",
                WARNING: "warning",
                LOADING:"loading"
            },
            sysTips:{
                title:"系统提示",
                loadingContent:"加载中.."
            }
        };
    });
    Beyondbit.register("Web.MessageBox", function () {
        return {
            Manager: function(){
                var stance = new Object();
                    stance.findByID = function (id, context) {
                        context = context || window;
                        return context.Beyondbit.Web.MessageBox.ManagerHelper.findMessageBox(id);
                    };
                    
                    stance.create = function (options, context) {
                        context = context || window;
                        return new context.Beyondbit.Web.MessageBox.init(options);
                    };
                    return stance;
            }()
        }
    });
    Beyondbit.register("Web.MessageBox", function () {
        return {
            ManagerHelper: {
                get_zIndex: function(){
                    if (Beyondbit.Web.get_GlobalzIndex) {
                        return Beyondbit.Web.get_GlobalzIndex;
                    };
                    var zindex = Beyondbit.Web.GlobalzIndex || 100;
                    Beyondbit.Web.GlobalzIndex = zindex + 1;
                    return zindex;
                },
                _Queue: {},
                addMessageBox: function (key, objModel) {
                    Beyondbit.Web.MessageBox.ManagerHelper._Queue[key] = objModel;
                },
                findMessageBox: function (key) {
                    var helper = Beyondbit.Web.MessageBox.ManagerHelper;
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
    Beyondbit.register("Web.MessageBox", function () {
        return {
            alert: function (title, message,  text, icon,onsuccess) {
                 var unqueid = "alert";
                if (arguments.length==0) {
                    return Beyondbit.Web.MessageBox.Manager.findByID(unqueid);
                };
                var _title, _message, _icon, _text ;
                var callback;
                var length = arguments.length;
                if (length > 1) {
                    if (arguments[length - 1] && $.isFunction(arguments[length - 1])) {
                        callback = arguments[length - 1];
                        switch (length) {
                            case 2:
                                _message = title;
                                break;
                            case 3:
                                _title = title;
                                _message = message;
                                break;
                            case 4:
                                _title = title;
                                _message = message;
                                _text = text;
                                break;
                            default:
                                _title = title;
                                _message = message;
                                _text = text;
                                _icon = icon;
                                break;
                        }
                    }
                    else {
                        _title = title;
                        _message = message;
                        _icon = icon
                        _text = text;
                    }
                }
                else {
                    _message = title;
                }
                _title = _title || Beyondbit.Web.MessageBox.sysTips.title;
                _text = _text || Beyondbit.Web.MessageBox.messages.ok;
                _icon = _icon || Beyondbit.Web.MessageBox.Icons.INFO;

                return Beyondbit.Web.MessageBox.show({
                    id:unqueid,
                    content: _message,
                    title: _title,
                    icon:_icon,
                    btns: [{
                            text: _text,
                            callback: callback
                        }
                    ],
                    draggable:true,
                    closeBtn: { show: true, callback: callback,defindValue:null }
                });
            },
            confirm: function (title, message, text, icon,onsuccess) {
                var unqueid = "confirm";
                if (arguments.length==0) {
                    return Beyondbit.Web.MessageBox.Manager.findByID(unqueid);
                };
                var _title, _message,_text,_icon;
                var callback;
                var length = arguments.length;
                if (length > 1) {
                    if (arguments[length - 1] && $.isFunction(arguments[length - 1])) {
                        callback = arguments[length - 1];
                        switch (length) {
                            case 2:
                                _message = title;
                                break;
                            case 3:
                                _title = title;
                                _message = message;
                                break;
                            case 4:
                                _title = title;
                                _message = message;
                                _text = text;
                                break;
                            default:
                                _title = title;
                                _message = message;
                                _text = text;
                                _icon = icon;
                                break;
                        }
                    }
                    else {
                        _title = title;
                        _message = message;
                        _text = text;
                        _icon = icon;
                    }
                }
                else {
                    _message = title;
                }
                _text = _text || [Beyondbit.Web.MessageBox.messages.ok, Beyondbit.Web.MessageBox.messages.cancel];
                _icon = _icon || Beyondbit.Web.MessageBox.Icons.QUESTION;
                return Beyondbit.Web.MessageBox.show({
                    id:unqueid,
                    content: _message,
                    title: _title,
                    icon: _icon,
                    draggable:true,
                    btns: [
                        {
                            text: _text[0],
                            callback: callback,
                            defindValue: true
                        }
                        ,
                        {
                            text: _text[1],
                            callback: callback,
                            defindValue: false
                        }
                    ],
                    closeBtn: { show: true, callback: callback, defindValue: false }
                });
            },
            prompt: function (title, text, defaultText,icon,onsuccess) {
                var _title,_text,_defaultText,_icon;
                var unqueid = "prompt";
                var length = arguments.length;
                if (length == 0) {
                    return Beyondbit.Web.MessageBox.Manager.findByID(unqueid);
                };
                if (length == 1) {
                    _text = title;
                }
                else{
                    if (arguments[length - 1] && $.isFunction(arguments[length - 1])) 
                    {
                        onsuccess = arguments[arguments.length - 1];
                        switch (length) {
                            case 2:
                                _text = title;
                                break;
                            case 3:
                                _title = title;
                                _text = text;
                                break;
                            case 4:
                                _title = title;
                                _text = text;
                                _defaultText = defaultText;
                                break;
                            default:
                                _title = title;
                                _text = text;
                                _defaultText = defaultText;
                                _icon = icon;
                                break;
                        }
                    }
                    else {
                        _title = title;
                        _text = text;
                        _defaultText = defaultText;
                        _icon = icon;
                    }
                }

                _defaultText = _defaultText || "";
               
                var _content = text + '\n<input type="text" name="txtpromptcontent" value="'+ _defaultText +'" />';
                
                _icon = _icon || Beyondbit.Web.MessageBox.Icons.QUESTION;
                
                return Beyondbit.Web.MessageBox.show({
                    id:unqueid,
                    title: _title,
                    content:_content,
                    icon:_icon,
                    draggable:true,
                    btns:[
                        {
                            text: Beyondbit.Web.MessageBox.messages.ok,
                            callback: function(){
                                if(onsuccess){
                                    return onsuccess(this.rootNode.find(":text[name=txtpromptcontent]").val());
                                }
                                return;
                            },
                            defindValue: ""
                        }
                    ],
                    closeBtn: { show: true, callback: onsuccess, defindValue: "" }
                });
            },
            loading: function (title, message, closeSpeed, closeFn) {
                var unqueid = "loading";
                var _closeSpeed,_closeFn,_title,_message;
                if ($.isNumeric(title)) {
                    // 如果第一个参数为数字，则为关闭时间
                    _closeSpeed = title;
                    _closeFn = message;
                }else{
                    if (title == false) {
                        // 关闭Loading窗口
                        return Beyondbit.Web.MessageBox.hide(unqueid);
                    }
                    _title = title;
                    _message = message;
                    _closeSpeed = closeSpeed; _closeFn = closeFn;
                }

                // 显示Loading窗口 
                if (_closeSpeed) {
                    setTimeout(function () {
                        var closeReturn = true;
                        if (_closeFn) {
                            closeReturn = _closeFn();
                        }
                        if(!(closeReturn == false)){
                            Beyondbit.Web.MessageBox.loading(false);
                        }
                    }, _closeSpeed);
                }
                return Beyondbit.Web.MessageBox.show({
                        id:unqueid,
                        icon:"loading",
                        closeBtn:{show:false},
                        title: _title || Beyondbit.Web.MessageBox.sysTips.title,
                        content: _message || Beyondbit.Web.MessageBox.sysTips.loadingContent
                    }).show();
            },
            hide: function(id){
                id = id || "gloabid";
                var mgb = Beyondbit.Web.MessageBox.Manager.findByID(id);
                if (mgb) {
                    mgb.hide();
                };
                return mgb;
            },
            show: function(options){
                return Beyondbit.Web.MessageBox.Manager.create(options).show();
            },
            init: function (options) {
                var options = $.extend({
                    id:"gloabid",
                    title: "",          // 标题
                    content: null,      // 内容，如果此不为空，则为文本内容显示，否则为Ifame方式显示
                    btns: null,         // 按钮集合，可系统按钮，或者自定义一个按钮集合，自定方格式为["确定","取消"]
                    icon: null,         // 图标，可系统图标，或者自定义一个图片地址,
                    closeBtn: { show: true, callback: null, defindValue: null },    // 是否显示关闭按钮
                    width:300,
                    shadow: true,      //是否阴影
                    draggable: false     //是否拖动
                }, options);
                var findObj = Beyondbit.Web.MessageBox.Manager.findByID(options.id);
                if(findObj){
                    findObj._initData.options = options;
                    findObj._init(findObj._initData);
                    return findObj;
                };
                var that = this;
                Beyondbit.Web.MessageBox.ManagerHelper.addMessageBox(options.id, this);

               
                var id = "fontend_messagebox_" + options.id;
                var idShadow = id + "_shadow";

                // 初始化HTML
                var root = $("#" + id);
                if (root.length == 0) {
                    $("body").append(' <div class="shadowskin_b fontend_mgboxskin_b_shadow" id="' + idShadow + '"></div>'
                            +' <div class="fontend_mgboxskin_b" id="'+ id +'">'
                            +'     <div class="fontend_mgbox_header"><h4>&nbsp;</h4></div>'
                            +'     <a href="#" class="fontend_mgbox_btn_close">X</a>'
                            +'     <div class="fontend_mgbox_bodyer  ">'
                            +'         <div class="fontend_mgbox_container"></div>'
                            +'         <div class="fontend_mgbox_buttoner"></div>'
                            +'     </div></div>')
                    root = $("#" + id);
                }

                var rootShadow = $("#" + idShadow);
                
                that.rootNode = root;
                that._initData  = {root:root,rootShadow:rootShadow,options:options};
                // 初始化封装
                that._init = function(initdata){
                    var sender = this;
                    var options = initdata.options;
                    var root = initdata.root;
                    var _zindex = Beyondbit.Web.MessageBox.ManagerHelper.get_zIndex();
                    rootShadow.css("z-index", _zindex);
                    root.css("z-index", _zindex);

                    // 是否需要关闭按钮
                    if (options.closeBtn.show) {
                        root.find(" > .fontend_mgbox_btn_close").off("click").click(function () {
                            var closeReturn = true;
                            if(options.closeBtn.callback){ closeReturn = options.closeBtn.callback.call(sender,options.closeBtn.defindValue);}
                            if(!(closeReturn==false)){
                                sender.hide();
                            }
                            return false;
                        });
                    } else {
                        root.find(" > .fontend_mgbox_btn_close").hide();
                    }

                    // 是否可拖动
                    if (options.draggable && root.draggable && sender.draggableDone != true) {
                        $(" > .fontend_mgbox_header > h4", root).css({ cursor: "move" });
                        root.draggable({ cursor: "move", handle: "> .fontend_mgbox_header > h4", containment: "body" });
                        sender.draggableDone = true;
                    }

                    root.find(" > .fontend_mgbox_header > h4").html(options.title || "&nbsp;");
                    
                    
                    var $container = $(".fontend_mgbox_container", root).html(options.content);

                    $container.attr({
                        "class":"fontend_mgbox_container",
                        "style":""
                    });
                    // 加载图标
                    if (options.icon) {
                        $container.addClass("fontend_mgbox_container_icon");

                        var isSysIcon = false;
                        if(options.icon.length<10){
                            // 长字符一定不是系统Icon
                            $.each(Beyondbit.Web.MessageBox.Icons,function(i,v){
                                if (v==options.icon) {
                                    isSysIcon = true;
                                    return;
                                };
                            })
                        }
                        if (isSysIcon) {
                            $container.addClass("icon_tips_" + options.icon);
                            if (options.icon==Beyondbit.Web.MessageBox.Icons.LOADING) {
                                $container.addClass("fontend_mgbox_container_singleline");
                            };
                        }
                        else
                        {
                            $container.attr("style",$container.attr("style")+";" + options.icon + ";") 
                        }
                    }

                    // 加载按钮
                    var $btnsArea = $(".fontend_mgbox_buttoner", root).html("");
                    if(options.btns && options.btns.length>0){
                        $btnsArea.show();
                        $.each(options.btns, function (i, v) {
                            $btnsArea.append($('<input type="button" class="btnskin_b ' + v.iconclass + '" value="' + v.text + '" />')
                                .click(function () {
                                    var closeReturn = true;
                                    if(v.callback){closeReturn = v.callback.call(sender,v.defindValue);}
                                    if(!(closeReturn==false)){
                                        sender.hide();
                                    }
                                }));
                        });
                    }else
                    {
                        $btnsArea.hide();
                    };

                    root.css({width:options.width+"px","margin-left":"-"+ options.width / 2+"px"});
                    var _height = root.height();
                    root.css({"margin-top":"-"+ _height / 2+"px"});
                }

                that._init(that._initData);

                that.hide = function(){
                    root.hide(); rootShadow.hide();
                    return that;
                };
                that.show = function(){
                    root.show(); 
                    // 是否需要遮挡层
                    if(options.shadow){ rootShadow.show();}else{rootShadow.hide();}
                    return that;
                }

                that.show();
                return that;
            }
        };
    });

})(jQuery);

            
