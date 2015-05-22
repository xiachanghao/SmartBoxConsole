<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.PluginInfo>" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EditPluginInfo</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/simplybuttons.css")%>" rel="Stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/uploadify.css")%>" rel="stylesheet"
        type="text/css" />

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

    <style type="text/css">
        .bspan
        {
            background-color: #E8F1F8 !important;
        }
        a
        {
            cursor: pointer !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            //初始化按钮样式
            SimplyButtons.init();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");

            $("#btnNext").click(function() { NextC() });



            var versionId = '<%=ViewData["versionId"]%>';

            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("正在保存数据......").show();

                },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        var url = '<%=Url.Action("UpdatePluginConfig") %>/' + escape(versionId);
                        window.location = url;
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function() {
                        });

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
                     Version: { maxlength: 32 },
                     DisplayName: { required: true, maxlength: 256 },  //minlength: 6
                     TypeFullName: { maxlength: 512 },
                     FileName: { maxlength: 256 },
                     PluginUrl: { maxlength: 1024 }
                 },
                 messages: {
                     DisplayName: { required: "插件名不能为空" }

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
                    pos.left = document.documentElement.clientWidth - 156 - 4;
                }
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#fmEdit");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:100%;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }  // end of showerror

        });                                // ready


        function NextC() {
            hiConfirm("确认要保存当前基本信息执行下一步吗？", "提示", function(btn) {
                if (btn == true) {
                    $("#fmEdit").submit();
                }
            }); // end of hiConfirm
        }
        

  
    </script>

</head>
<body>
    <div>
        <div class="ajaxmsgpanel" style="border: none;">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                正在保存数据...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                非常抱歉，无法执行您的操作，请稍后再试</div>
        </div>
        <div class="bbit-categorycontainer">
            <% using (Html.BeginForm("SavePluginInfo", "PluginManage", FormMethod.Post, new { id = "fmEdit" }))
               {%>
            <table width="100%" id="Table2" cellspacing="0" cellpadding="1">
                <tr>
                    <td align="right">
                        <input type="button" id="btnNext" value="下一步" />
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend class="title">插件版本信息</legend>
                <table width="100%" id="Table3" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                版本号：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextBox("VersionName", ViewData["vername"], new { style = "background:Silver", @readonly = true })%>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend class="title">插件基本信息</legend>
                <table width="100%" id="basicinfotable" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件标识：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.TextBox("PluginCode", Model.PluginCode, new { style = "background:Silver", @readonly = true })%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                插件名：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextBox("DisplayName", Model.DisplayName, new { style = " ", Class = "required safe" })%>
                            <span class="bbit-formItemMust">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件分类：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.ResourceDropDownList("PluginCateCode", (IList<Beyondbit.Framework.Biz.Resource.IResourceData>)ViewData["list"], Model.PluginCateCode, new { })%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                版本：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextBox("Version", Model.Version, new { style = " ", Class = "safe" })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件全名：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.TextBox("TypeFullName", Model.TypeFullName, new { style = "width:280px", Class = "safe" })%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                文件名：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextBox("FileName", Model.FileName, new { style = "width:280px", Class = "safe" })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                是否必须：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.RadioButton("IsNeed", Model.IsNeed, Model.IsNeed == true ? true : false)%>是
                            <%=Html.RadioButton("IsNeed", Model.IsNeed, Model.IsNeed == false ? true : false)%>否
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                是否默认：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.RadioButton("IsDefault", Model.IsDefault, Model.IsDefault == true ? true : false)%>是
                            <%=Html.RadioButton("IsDefault", Model.IsDefault, Model.IsDefault == false ? true : false)%>否
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                排序号：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Html.TextBox("Sequence", Model.Sequence, new { style = "width:6%", Class = "digits" })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                描述：</label>
                        </td>
                        <td class=" tdtop tdright" style="border-bottom: #cccccc 1px solid" colspan="3">
                            <%=Html.TextArea("Summary", Model.Summary, new { style = "width:70%",rows=3 })%>
                    </tr>
                </table>
            </fieldset>
            <fieldset id="actionDiv">
                <legend class="title">插件扩展信息</legend>
                <table width="100%" id="Table4" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                扩展标识：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.TextBox("ActionCode", ViewData["actionCode"], new { Class = "IsNull safe" })%>
                            <span class="bbit-formItemMust">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                扩展描述：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextArea("ESummary", ViewData["summary"].ToString(), new { style = "width:70%", rows = 3 })%>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%=Html.Hidden("PluginCode", Model.PluginCode)%>
            <%=Html.Hidden("HashCode", Model.HashCode)%>
            <%=Html.Hidden("CreateTime", Model.CreateTime)%>
            <%=Html.Hidden("CreateUid", Model.CreateUid)%>
            <%=Html.Hidden("LastModTime", Model.LastModTime)%>
            <%=Html.Hidden("LastModUid", Model.LastModUid)%>
            <%}%>
        </div>
    </div>
</body>
</html>
