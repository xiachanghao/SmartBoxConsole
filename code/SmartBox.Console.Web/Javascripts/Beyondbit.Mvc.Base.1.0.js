(function ($) {
    $.extend($.fn, {
        dropDownVir: function () {
            if (!this.length) {
                return null;
            }
            var dropdownvir = $.data(this[0], 'dropdownvir');
            if (dropdownvir) {
                return dropdownvir;
            }
            dropdownvir = new Beyondbit.Mvc.Controls.DropDownVir(this.eq(0));
            $.data(this[0], 'dropdownvir', dropdownvir);

            return dropdownvir;
        },
        initMvcForm: function (jsondata) {
            var $from = this;
            if ($from.length == 0) {
                return false;
            }
            var fromJson = jsondata;
            for (var item in fromJson) {
                var hElements = $from.find("[name=" + item + "]");
                if (hElements.length == 0) {
                    continue;
                }

                var hElement = hElements.eq(0);
                if (hElement.is("div")) {
                    if (hElement.attr("dropdownvir")) {
                        hElement.dropDownVir().val(fromJson[item], true);
                    }

                }
                else if (hElement.is(":radio")) {
                    hElements.filter("[value=" + fromJson[item] + "]").attr("checked", "checked");
                }
                else if (hElement.is(":checkbox")) {
                    hElements.filter("[value=" + fromJson[item] + "]").attr("checked", "true");
                }
                else if (hElement.is(":hidden")) {
                    if (hElement.attr("dropdownvir")) {
                        continue;
                    }
                }
                else if (hElement.is("select") || hElement.is("textarea") || hElement.is(":text")) {
                    hElements.val(fromJson[item]);
                }
            }
            return $from;
        }
    });

    Beyondbit.register("Mvc.Controls", function () {
        return {
            DropDownVir: function (sender) {
                var targetElement = sender;
                var loader = this;
                targetElement.addClass("from_dropdownvir").click(function (event) {
                    var element = $(event.target);
                    if ($.browser.msie && $.browser.version == "6.0") {
                        element.addClass("selected").siblings(".selected").removeClass("selected").end().siblings("input").val(element.attr("value"));
                    } else {
                        element.attr("selected", "selected").siblings("[selected]").removeAttr("selected").end().siblings("input").val(element.attr("value"));
                    }
                    onChangeProxy.apply(loader);
                });
                var onChangeProxy = null;
                this.change = function (callback) {
                    onChangeProxy = callback;
                };

                this.val = function (value, isdefault) {
                    var tInputValue = $("input[dropdownvir]", targetElement);
                    if (!value && value != false) {
                        return tInputValue.val();
                    } else {
                        tInputValue.val(value).siblings("a[value=" + value + "]").trigger("click");
                        if (isdefault == true) {
                            tInputValue.attr("data-ctl-defalutvalue", value);
                        }
                    }
                    return loader;
                };

                this.reset = function () {
                    var tInputValue = $("input[dropdownvir]", targetElement);
                    var tempValue = tInputValue.attr("data-ctl-defalutvalue") || "";
                    loader.val(tempValue);
                    return loader;
                };

            }
        };
    });

    Beyondbit.register("Mvc.Form", function () {
        return {
            initFrom: function (formId, jsondata) {
                var $from = $("#" + formId);
                if ($from.length == 0) {
                    return false;
                }
                $from.initMvcForm(jsondata);
                return $from;
            }
        };
    });

})(jQuery);