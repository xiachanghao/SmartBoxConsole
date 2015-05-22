<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.ConfigInfo>" %>
<%@ Import Namespace="SmartBox.Console.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EditConfig</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            $("a#Save").click(save);
            $("#CloseImgBtn1").click(function() {
                CloseModelWindow();
            });
            function save() {
                $("#fmEdit").submit();
            }
            $.hotkeys.add('Ctrl+s', function() {
                save();
            });
            $.hotkeys.add('esc', function() {
                CloseModelWindow();
            });
            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("正在保存数据......").show();
                    return true;
                },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        hiAlert(data.Msg, '提示', function() { CloseModelWindow(null, true); });
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            };
            //校验
            $.validator.addMethod("safe", function(value, element) {
                return this.optional(element) || /^[^$\<\>]+$/.test(value);
            }, "不能包含以下符号: $<>");
            $("#fmEdit").validate(
             {
                 rules: {
                     Key: { required: true, maxlength: 256 },
                     Value: { required: true, maxlength: 512 },
                     ValueAssembly: { maxlength: 256 },
                     Summary: { maxlength: 512 }
                 },
                 messages: {
                     Key: { required: "请输入键" },
                     Value: { required: "请输入值" }
                 },
                 submitHandler: function(form) {
                     $("#fmEdit").ajaxSubmit(options);
                 },
                 errorElement: "div",
                 errorClass: "cusErrorPanel",
                 errorPlacement: function(error, element) {
                     showerror(error, element);
                 }
             });
            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                if (pos.left + 155 >= document.documentElement.clientWidth) {
                    pos.left = document.documentElement.clientWidth - 156;
                }
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#fmEdit");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }  // end of showerror           
        });            // ready
    </script>

</head>
<body>
    <div>
        <div class="cHead" style="border: none;">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                正在保存数据...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                非常抱歉，无法执行您的操作，请稍后再试</div>
        </div>
        <div class="toolBotton">
            <a id="Save" class="imgbtn" href="javascript:void(0);"><span class="Save" title="保存">
                保存(<u>S</u>)</span></a> <a id="CloseImgBtn1" class="imgbtn" href="javascript:void(0);">
                    <span class="Close" title="关闭">关闭(<u>Esc</u>)</span></a>
        </div>
        <% using (Html.BeginForm("EditConfigs", "MainProgramManage", FormMethod.Post, new { id = "fmEdit" }))
           {%>
        <div class="bbit-categorycontainer">
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft">
                        <label>
                            键：</label>
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                    <%if (string.IsNullOrEmpty(Model.ConfigId))
                      { %>
                       <%=Html.TextBox("[Key]", Model.Key, new { style = "width:60%;", Class = "required safe"})%>
                    <%}
                      else
                      {%>
                        <%=Html.TextBox("[Key]", Model.Key, new { style = "width:60%;background:Silver", Class = "required safe", @readonly = true })%>
                        <%} %>
                        <span class="bbit-formItemMust">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdleft">
                        <label>
                            值：</label>
                    </td>
                    <td class="bbit-form-cell-value tdright">
                        <%=Html.TextBox("Value", Model.Value, new { style = "width:60%", Class = "required safe" })%>
                        <span class="bbit-formItemMust">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdleft">
                        <label>
                            分类：</label>
                    </td>
                    <td class="bbit-form-cell-value tdright">
                        <%=Html.ResourceDropDownList("ConfigCategoryCode", (IList<Beyondbit.Framework.Biz.Resource.IResourceData>)ViewData["list"], Model.ConfigCategoryCode, new { })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdleft">
                        <label>
                            描述：</label>
                    </td>
                    <td class="bbit-form-cell-value tdright">
                        <%=Html.TextArea("Summary", Model.Summary, new { style = "width:80%" })%>
                    </td>
                </tr>
            </table>
        </div>
        <%=Html.Hidden("ConfigId", Model.ConfigId)%>
        <%=Html.Hidden("UserUId", Model.UserUId)%>
        <%=Html.Hidden("PluginCode", ViewData["pid"])%>
        <%=Html.Hidden("ValueAssembly", Model.ValueAssembly)%>
        <%}%>
    </div>
</body>
</html>
