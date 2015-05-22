﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CreateApplicationPackage</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dp.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="Stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/jquery.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/pluginssource/jquery.form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.datepicker.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.dropdown.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <link href="<%=Url.Content("~/Javascripts/JqueryUI/css/start/jquery-ui-1.8.13.custom.css")%>"
        rel="stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/JqueryUI/jquery-ui-1.8.13.custom.min.js")%>"
        type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/SwfUpload/swfupload.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/SwfUpload/plugins/swfupload.queue.js") %>"
        type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/SwfUpload/plugins/swfupload.speed.js") %>"
        type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <style type="text/css">
        .title
        {
            background-color: #60a6cf;
            height: 24px;
            line-height: 24px;
            font-size: 12px;
        }
        .title strong
        {
            font-size: 16px;
        }
        table, tr
        {
            border-top: 1px;
            border-bottom: 1px;
            border-left: 0px;
            border-right: 0px;
            border-style: solid;
            border-collapse: collapse;
            border-bottom-color:#cdcdcd;
            border-top-color:#cdcdcd;
        }
        .form
        {
            width: 100%;
            background-color: #fff;
            margin: 0px;
            padding: 0px;
        }
        .notNull
        {
            color: #F00;
        }
        fieldset
        {
            border: 1px solid #cdcdcd;
            margin: 5px;
            padding: 2px 0px 5px 0px;
        }
        fieldset legend
        {
            margin-left: 10px;
            font-weight: bold;
        }
        .sp-form .sp-form-cell-name
        {
            padding: 5px 6px;
            background-color: #E6F0F6;
            text-align: right;
            width: 19%;
        }
        .sp-form-action-cell-name
        {
            padding: 5px 6px;
            background-color: #ffffff;
            text-align: right;
            width: 19%;
        }
        .sp-form .sp-form .sp-form-cell-name p
        {
            margin: 5px;
            text-align: left;
        }
        .sp-form .sp-form-cell-value
        {
            padding: 5px 3px;
            background-color: #ffffff;
        }
        div.checkboxlist div.list
        {
            float: left;
            margin: 2px 2px 0px 3px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $("document").ready(function () {
            $("#frmApplicationPackage").hide();
            $("#Save").hide();
            $("#CloseImgBtn1").click(function () {
                //CloseModelWindow(null, true);
                parent.CloseWind(false);
            });

            $("a#Save").click(function () {
                $("#frmApplicationPackage").submit();
            });
            //文件上传
//            swfu = new SWFUpload({
//                upload_url: '<%= Url.Action("UploadUpdatePackage","ApplicationManage") %>?packageID=<%=ViewData["packageID"] %>&filePath=',
//                flash_url: "/Javascripts/SwfUpload/Flash/swfupload.swf",
//                file_size_limit: "1500 MB",
//                file_types: "*.*",
//                file_types_description: "All Files",
//                file_upload_limit: 0,
//                file_queue_limit: 1,
//                debug: false,

//                // Button settings
//                button_image_url: "/Javascripts/SwfUpload/Images/TestImageNoText_65x29.png",
//                button_width: "65",
//                button_height: "29",
//                button_placeholder_id: "spanButtonPlaceHolder",
//                button_text: '<span class="theFont">浏览</span>',
//                button_text_style: ".theFont { font-size: 16; }",
//                button_text_left_padding: 12,
//                button_text_top_padding: 3,


//                file_dialog_complete_handler: function (numFilesSelected, numFilesQueued) {
//                },
//                file_queued_handler: function (file) {
//                    $("#packageUpload").val(file.name);
//                },
//                upload_progress_handler: function (file, complete, total) {
//                    var value = complete / total * 100;
//                    $("#progressbar").progressbar("value", value);
//                    $("#CurrentSpeed").html(SWFUpload.speed.formatBPS(file.currentSpeed));
//                },
//                upload_success_handler: function (file, data) {
//                    var json;
//                    eval("json=" + data);
//                    $("#fileProgress").dialog("close");
//                    submitResult(json);
//                }

//            });

            $("#cancelButton").click(function () {
                swfu.stopUpload();
            });

            $("#btnUpload").click(function () {
//                if (swfu.getStats().files_queued > 0) {
//                    $("#fileProgress").dialog({
//                        modal: true,
//                        width: 400,
//                        open: function () {
//                            $("#progressbar").progressbar({
//                                value: 0
//                            });

//                            swfu.startUpload();

//                        }
//                    });
//                }
                $("#frmUploadPackage").submit();
            });

            $("#btnSubmit").click(function () {
                var path = $("#packagePath").val();
                if (path != "") {
                    $.ajax({
                        url: '<%= Url.Action("UploadUpdatePackage","ApplicationManage") %>',
                        dataType: "json",
                        type: "post",
                        data: { filePath: path, packageID:'<%=ViewData["packageID"] %>' },
                        success: function (data) {
                            submitResult(data);
                        },
                        error: function (opt, type, massage) {
                            alert(massage);
                        }
                    });
                }
                else {
                    hiAlert("必须填写服务器的文件路径", '提示', function () { });
                }
            });

            //上传之验证
            $("#frmUploadPackage").validate({
                rules: { packageUpload: { required: true} },
                messages: { packageUpload: { required: "请先选择要上传的文件!!"} },
                submitHandler: function (form) {
                    $("#frmUploadPackage").ajaxSubmit({
                        beforeSumbit: function () {
                            $("#loadingpannel").html("正在保存数据...").show();
                            return true;
                        },
                        dataType: "json",
                        data: { filePath: "", useLocalPath: false },
                        success: function (data) {
                            submitResult(data);
                        }
                    });
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: function showerror(error, target) {
                    var pos = target.position();
                    var height = target.height();
                    var newpos = { left: pos.left, top: pos.top + height + 2 }
                    var form = $("#frmUploadPackage");
                    var v = getiev();
                    if (v <= 6) {
                        var t = error.text();
                        error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                    }
                    error.appendTo(form).css(newpos);
                }
            });

            //保存之验证
            $("#frmApplicationPackage").validate({
                rules: {
                },
                messages: {
                },
                submitHandler: function (form) {
                    $("#frmApplicationPackage").ajaxSubmit({
                        beforeSumbit: function () {
                            $("#loadingpannel").html("正在保存数据...").show();
                            return true;
                        },
                        dataType: "json",
                        success: function (data) {
                            if (data.IsSuccess) {
                                $("#loadingpannel").hide();
                                hiAlert(data.Msg, '提示', function () {
                                    parent.$.closeIfrm(null, true);
                                });
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                            }
                        }
                    });
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: function (error, target) {
                    var pos = target.position();
                    var height = target.height();
                    var newpos = { left: pos.left, top: pos.top + height + 2 }
                    var form = $("#frmApplicationPackage");
                    var v = getiev();
                    if (v <= 6) {
                        var t = error.text();
                        error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                    }
                    error.appendTo(form).css(newpos);
                }
            });
            bindClassValidate("PackageName", $.validator.methods.required, "没有安装包名称,请上传正确的安装包");
            bindClassValidate("PackageType", $.validator.methods.required, "没有安装包类型,请上传正确的安装包");
            bindClassValidate("PackageClientType", $.validator.methods.required, "没有安装包客户端类型,请上传正确的安装包");
            bindClassValidate("PackageVersion", $.validator.methods.required, "没有安装包名称,请上传正确的安装包");
            bindClassValidate("PackageBuildVer", $.validator.methods.required, "没有安装包内部版本号,请上传正确的安装包");
            bindClassValidate("PackageDisplayName", $.validator.methods.required, "请填写安装包显示名称");
            bindClassValidate("AppClientType", $.validator.methods.required, "没有应用客户端类型,请上传正确的安装包");
            bindClassValidate("ActivityShortName", $.validator.methods.required, "没有Action简称,请上传正确的安装包");
            bindClassValidate("ActivityName", $.validator.methods.required, "没有Action名称,请上传正确的安装包");
            bindClassValidate("ActivityDisplayName", $.validator.methods.required, "请填写显示名称");
            bindClassValidate("ActivitySeq", $.validator.methods.required, "请填写排序号");
            bindClassValidate("ActivitySeq", $.validator.methods.number, "排序号必须是数字");
            function bindClassValidate(name, method, message) {
                $.validator.methods[name] = method;
                $.validator.messages[name] = message;
                $.validator.addClassRules(name, $.validator.normalizeRule(name));
            }

            function submitResult(data) {
                if (data.IsSuccess) {
                    $("#loadingpannel").hide();
                    $("#appinfo").html("");
                    $("frmApplicationPackage").show();
                    $("#Save").show()

                    data = data.Data;
                    $("#packageDisplayName").val(data.DisplayName);
                    $("#packageName").val(data.Name);
                    var packageClientType = data.ClientType.split("|");
                    $(".checkboxlist").find("input[name='packageViewClientType']").change(function () {
                        var value = new Array();
                        $("input:checked[name='packageViewClientType']").each(function () {
                            value.push($(this).val());
                        });
                        $("#packageClientType").val(value.join('|'));
                    }).each(function () {
                        var curChk = $(this);
                        for (var cindex = 0; cindex < packageClientType.length; cindex++) {
                            if (packageClientType[cindex].toLowerCase() == curChk.val().toLowerCase()) {
                                curChk.attr("checked", "checked");
                                return true;
                            }
                        }
                    });
                    $("#packageClientType").val(data.ClientType);
                    $("#packageType").val(data.Type);
                    $("#packageVersion").val(data.Version);
                    $("#packageBuildVer").val(data.BuildVer);
                    $("#packageDescription").val(data.Description);
                    $("#applicationCount").val(data.App4AIList.length);
                    $("#packageFileName").val(data.DownloadUri);
                    var createtime = new Function('return ' + data.CreateTime.substr(1, data.CreateTime.length - 2))();
                    $("#packageCreateDateTime").val(new Date(createtime).Format("yyyy-MM-dd hh:mm:ss"));
                    $("#packageCreateUser").val(data.CreateUid);
                    $("#packageID").val(data.ID);

                    for (var i = 0; i < data.App4AIList.length; i++) {
                        var applicationTable = $("#ApplicationTemplate").eq(0).children(0).clone();
                        var app4AI = data.App4AIList[i];
                        applicationTable.find("#AppCode_").attr("id", "AppCode_" + i).attr("name", "AppCode_" + i).find("option").each(function () {
                            var cop = $(this);
                            if ((app4AI.AppID >= 0 && cop.val() == app4AI.AppID)
                                            || (app4AI.AppName != null && app4AI.AppName != "" && cop.text().toLowerCase() == app4AI.AppName.toLowerCase())
                                            || (cop.text().toLowerCase() == app4AI.AppCode.toLowerCase())) {
                                cop.attr("selected", "selected");
                                return false;
                            }
                        });
                        applicationTable.find("#AppTitle_").attr("id", "AppTitle_" + i).html(app4AI.AppCode);
                        applicationTable.find("#AppName_").attr("id", "AppName_" + i).attr("name", "AppName_" + i).val(app4AI.AppCode);
                        var clientType = app4AI.ClientType.split("|");
                        var appCTIndex = 0;
                        applicationTable.find("#AppCheckClentType_").attr("id", "AppCheckClentType_" + i).attr("name", "AppCheckClentType_" + i).val(app4AI.ClientType);
                        applicationTable.find(".checkboxlist").find("input[name='AppClientType_']").attr("name", "AppClientType_" + i).each(function () {
                            var curChk = $(this);

                            applicationTable.find(".checkboxlist").find("label[for='" + curChk.attr("id") + "']").attr("for", "AppClientType_" + i + "_" + appCTIndex);
                            curChk.attr("id", "AppClientType_" + i + "_" + appCTIndex).change(function () {
                                var curChkName = $(this).attr("name");
                                var cruCheckClientType = "AppCheckClentType_" + curChkName.split("_")[1];
                                var value = new Array();
                                $("input:checked[name='" + curChkName + "']").each(function () {
                                    value.push($(this).val());
                                });
                                $("#" + cruCheckClientType).val(value.join('|'));
                            });
                            appCTIndex++;
                            for (var cindex = 0; cindex < clientType.length; cindex++) {
                                if (clientType[cindex].toLowerCase() == curChk.val().toLowerCase()) {
                                    curChk.attr("checked", "checked");
                                    return true;
                                }
                            }
                        });
                        applicationTable.find("#AppIco_").attr("id", "AppIco_" + i).attr("name", "AppIco_" + i).val(app4AI.IconUri);
                        applicationTable.find("#btnSelectAppIco").attr("txtIco", "AppIco_" + i).click(function () { SelectIcoClick(this); });
                        applicationTable.find("#activityCount_").attr("id", "activityCount_" + i).attr("name", "activityCount_" + i).val(app4AI.ActionList.length);
                        var ActivityTD = applicationTable.find("#ActivityTD_").attr("id", "ActivityTD_" + i).attr("name", "ActivityTD_" + i);

                        for (var j = 0; j < app4AI.ActionList.length; j++) {
                            var activityTable = $("#ActivityTemplate").eq(0).children(0).clone();
                            var action = app4AI.ActionList[j];
                            activityTable.find("#ActivityShortName_").attr("id", "ActivityShortName_" + i + "_" + j).attr("name", "ActivityShortName_" + i + "_" + j).val(action.ShortName);
                            activityTable.find("#ActivityName_").attr("id", "ActivityName_" + i + "_" + j).attr("name", "ActivityName_" + i + "_" + j).val(action.Name);
                            activityTable.find("#ActivityIco_").attr("id", "ActivityIco_" + i + "_" + j).attr("name", "ActivityIco_" + i + "_" + j).val(action.IconUri);
                            activityTable.find("#ActivityDisplayName_").attr("id", "ActivityDisplayName_" + i + "_" + j).attr("name", "ActivityDisplayName_" + i + "_" + j).val(action.DisplayName);
                            var enableLaunch = activityTable.find("#EnableLaunch_").attr("id", "EnableLaunch_" + i + "_" + j).attr("name", "ActivityLaunch_" + i + "_" + j);
                            var desableLaunch = activityTable.find("#DesableLaunch_").attr("id", "DesableLaunch_" + i + "_" + j).attr("name", "ActivityLaunch_" + i + "_" + j);
                            activityTable.find("#labEnableLaunch_").attr("id", "labEnableLaunch_" + i + "_" + j).attr("for", "EnableLaunch_" + i + "_" + j);
                            activityTable.find("#labDesableLaunch_").attr("id", "labDesableLaunch_" + i + "_" + j).attr("for", "DesableLaunch_" + i + "_" + j);
                            if (action.IsLaunch) {
                                enableLaunch.attr("checked", "chedked");
                            }
                            else {
                                desableLaunch.attr("checked", "chedked");
                            }
                            activityTable.find("#ActivitySeq_").attr("name", "ActivitySeq_" + i + "_" + j).val(action.Seq)
                            activityTable.find("#btnSelectActivityIco").attr("txtIco", "ActivityIco_" + i + "_" + j).click(function () { SelectIcoClick(this); });
                            ActivityTD.append(activityTable);
                        }
                        $("#appinfo").append(applicationTable);
                    }
                    $("#frmApplicationPackage").show();
                }
                else {
                    hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                }
            }
            function SelectIcoClick(obj) {
                SelectIco($(obj).attr("txtIco"));
            }
            function SelectIco(txtIco) {
                if (!txtIco || txtIco == "") {
                    return;
                }
                var url = '<%= Url.Action("SelectImage","ImageManage") %>';
                var imgID = showModalDialog(url, "", 'dialogHeight=500;dialogWidth=700;center=yes;help=no; scroll=yes;resizable=no;status=no');
                if (imgID) {
                    $("#" + txtIco).val('Server://beyondbit.smartbox.server.image/' + imgID);
                }
            }
        });
    </script>
</head>
<body>
    <div class="ajaxmsgpanel">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>
        <div title="文件上传进度" id="fileProgress" style="display: none">
            <div id="progressbar">
            </div>
            <button id="cancelButton">
                取消上传</button>
            当前上传速度： <span id="CurrentSpeed"></span>
        </div>
    </div>
    <div class="toolBotton">
        <a id="Save" class="imgbtn"><span class="Save" title="保存">保存</span></a> <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
    </div>
    <fieldset>
        <legend>文件上传</legend>
        <% using (Html.BeginForm("UploadUpdatePackage", "ApplicationManage", FormMethod.Post, new { id = "frmUploadPackage", enctype = "multipart/form-data" }))
           { %>
        <table class="sp-form" width="100%" cellspacing="1" cellpadding="0">
            <tr>
                <td class="sp-form-cell-name">
                    上传文件：
                </td>
                <td class="sp-form-cell-value">
                    <%--<input type="text" id="packageUpload" name="packageUpload" readonly="readonly" /><span
                        id="spanButtonPlaceHolder">浏览</span>--%>
                        <input type="file" id="packageUpload" name="packageUpload"  />
                        <input type="button" value="上传" id="btnUpload" />
                    <%=Html.Hidden("packageID") %>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    服务器文件：
                </td>
                <td class="sp-form-cell-value">
                    <input type="text" id="packagePath" name="packagePath" /><input type="button" value="提交"
                        id="btnSubmit" />
                </td>
            </tr>
        </table>
        <%} %>
    </fieldset>
    <% using (Html.BeginForm("UpdatePackage", "ApplicationManage", FormMethod.Post, new { id = "frmApplicationPackage" }))
       { %>
    <fieldset>
        <legend>安装包信息</legend>
        <table class="sp-form" width="100%" cellspacing="" cellpadding="1">
            <tr>
                <td class="sp-form-cell-name">
                    安装包显示名称：
                </td>
                <td class="sp-form-cell-value" colspan="3">
                    <%= Html.TextBox("packageDisplayName", null, new { @Style = "width:98%;", @Class = "PackageDisplayName" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    安装包名称：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageName", null, new { @Readonly = "readonly", @Style = "border:0px;width:98%;", @Class = "PackageName" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    安装包类型：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageType", null, new { @Readonly = "readonly", @Style = "border:0px;width:80px;", @Class = "PackageType" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    客户端类型：
                </td>
                <td class="sp-form-cell-value" colspan="3">
                    <%=Html.CheckBoxList("packageViewClientType", ViewData["ClientType"] as IList<SelectListItem>, new { @disabled = "disabled" })%>
                    <%=Html.Hidden("packageClientType", null, new { @Class = "PackageClientType" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    发布版本号：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageVersion", null, new { @Readonly = "readonly", @Style = "border:0px;width:98%;", @Class = "PackageVersion" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    内部版本号：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageBuildVer", null, new { @Readonly = "readonly", @Style = "border:0px;width:98%;", @Class = "PackageBuildVer" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    备注：
                </td>
                <td class="sp-form-cell-value" colspan="3">
                    <%= Html.TextArea("packageDescription", null, new { @Style = "width:98%;" })%>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>应用信息</legend>
        <div id="appinfo">
        </div>
    </fieldset>
    <%= Html.Hidden("applicationCount") %>
    <%= Html.Hidden("packageCreateUser") %>
    <%= Html.Hidden("packageCreateDateTime") %>
    <%= Html.Hidden("packageUpdateUser") %>
    <%= Html.Hidden("packageUpdateDateTime") %>
    <%= Html.Hidden("packageFileName") %>
    <%= Html.Hidden("packageID") %>
    <% } %>
    <div id="ApplicationTemplate" style="display: none">
        <fieldset>
            <legend id="AppTitle_"></legend>
            <table width="100%" cellpadding="0" cellspacing="1" style="margin-bottom: 10px;"
                class="sp-form">
                <tr>
                    <td class="sp-form-cell-name">
                        关联应用：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("AppCode_") %>
                        <%= Html.Hidden("AppName_")%>
                        <%= Html.Hidden("activityCount_") %>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        图标地址：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.TextBox("AppIco_", null, new { @Style = "width:85%;", @Class = "AppIco" })%>
                        <input type="button" id="btnSelectAppIco" value="…" style="width: 5%;" />
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        客户端类型：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.CheckBoxList("AppClientType_", ViewData["ClientType"] as IList<SelectListItem>, new { @disabled = "disabled" })%>
                        <%=Html.Hidden("AppCheckClentType_", null, new { @Class = "AppClientType" })%>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        Action：
                    </td>
                    <td class="sp-form-cell-value" id="ActivityTD_">
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="ActivityTemplate" style="display: none">
        <table width="100%" cellpadding="0" cellspacing="1" class="sp-form">
            <tr style="display: none">
                <td class="sp-form-cell-name">
                    Action简称:
                </td>
                <td class="sp-form-cell-value">
                    <%=Html.TextBox("ActivityShortName_", null, new { @Readonly = "readonly", @Style = "border:0px;width:90%;", @Class = "ActivityShortName" })%>
                </td>
            </tr>
            <tr style="display: none">
                <td class="sp-form-cell-name">
                    Action名称:
                </td>
                <td class="sp-form-cell-value">
                    <%=Html.TextBox("ActivityName_", null, new { @Readonly = "readonly", @Style = "border:0px;width:90%;", @Class = "ActivityName" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-action-cell-name">
                    Action显示名称:
                </td>
                <td class="sp-form-cell-value">
                    <%=Html.TextBox("ActivityDisplayName_", null, new { @Style = "width:90%;", @Class = "ActivityDisplayName" })%>
                </td>
                <td class="sp-form-action-cell-name">
                    Action排序号:
                </td>
                <td class="sp-form-cell-value">
                    <%=Html.TextBox("ActivitySeq_", null, new { @Style = "width:50px;", @Class = "ActivitySeq" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-action-cell-name">
                    Action图标:
                </td>
                <td class="sp-form-cell-value" colspan="3">
                    <%=Html.TextBox("ActivityIco_", null, new { @Style = "width:85%;", @Class = "ActivityIco" })%>
                    <input type="button" id="btnSelectActivityIco" value="…" style="width: 5%;" />
                </td>
            </tr>
            <tr style="display: none">
                <td class="sp-form-cell-name">
                    启用Action:
                </td>
                <td class="sp-form-cell-value">
                    <%=Html.RadioButton("ActivityLaunch_", true, new { id="EnableLaunch_" })%><label
                        id="labEnableLaunch_" for="EnableLaunch_">启用</label>
                    <%=Html.RadioButton("ActivityLaunch_", false, new { id = "DesableLaunch_" })%><label
                        id="labDesableLaunch_" for="DesableLaunch_">禁用</label>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
