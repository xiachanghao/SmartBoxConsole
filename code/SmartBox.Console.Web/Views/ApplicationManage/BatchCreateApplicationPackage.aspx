<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BatchCreateApplicationPackage</title>
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
            border: 1px solid #000000;
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
            background-color: #bebebe;
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
        .sp-form .sp-form-cell-name p
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
        $(document).ready(function () {
            $("#CloseImgBtn1").click(function () {
                CloseModelWindow(null, true);
            });
            //文件上传
            swfu = new SWFUpload({
                upload_url: '<%= Url.Action("BatchCreateApplicationPackage","ApplicationManage") %>',
                flash_url: "/Javascripts/SwfUpload/Flash/swfupload.swf",
                file_size_limit: "1500 MB",
                file_types: "*.*",
                file_types_description: "All Files",
                file_upload_limit: 0,
                file_queue_limit: 1,
                debug: false,

                // Button settings
                button_image_url: "/Javascripts/SwfUpload/Images/TestImageNoText_65x29.png",
                button_width: "65",
                button_height: "29",
                button_placeholder_id: "spanButtonPlaceHolder",
                button_text: '<span class="theFont">浏览</span>',
                button_text_style: ".theFont { font-size: 16; }",
                button_text_left_padding: 12,
                button_text_top_padding: 3,


                file_dialog_complete_handler: function (numFilesSelected, numFilesQueued) {
                },
                file_queued_handler: function (file) {
                    $("#packageUpload").val(file.name);
                },
                upload_progress_handler: function (file, complete, total) {
                    var value = complete / total * 100;
                    $("#progressbar").progressbar("value", value);
                    $("#CurrentSpeed").html(SWFUpload.speed.formatBPS(file.currentSpeed));
                },
                upload_success_handler: function (file, data) {
                    var json;
                    eval("json=" + data);
                    $("#fileProgress").dialog("close");
                    if (json.IsSuccess) {
                        hiAlert("操作成功:\r\n" + json.Msg, '提示', function () { CloseModelWindow(null, true); });
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + json.Msg, '提示', function () { });
                    }
                }

            });

            $("#cancelButton").click(function () {
                swfu.stopUpload();
            });

            $("#btnUpload").click(function () {
                if (swfu.getStats().files_queued > 0) {
                    $("#fileProgress").dialog({
                        modal: true,
                        width: 400,
                        open: function () {
                            $("#progressbar").progressbar({
                                value: 0
                            });

                            swfu.startUpload();

                        }
                    });
                }
                //$("#frmUploadPackage").submit();
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
                        success: function (data) {
                            if (data.IsSuccess) {
                                hiAlert("操作成功:\r\n" + data.Msg, '提示', function () { CloseModelWindow(null, true); });
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                            }
                        },
                        error: function (opt, type, massage) {
                            alert(massage);
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
        <a id="CloseImgBtn1" class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
    </div>
    <% using (Html.BeginForm("BatchCreateApplicationPackage", "ApplicationManage", FormMethod.Post, new { id = "frmUploadPackage", enctype = "multipart/form-data" }))
       {
           Html.AntiForgeryToken();
           Html.ValidationSummary(true);
           %>
    <table class="sp-form" width="100%" cellspacing="1" cellpadding="0">
        <tr>
            <td class="sp-form-cell-name">
                <b>上传文件：</b>
                <p>
                    只能上传zip格式的压缩包,压缩包中只能包含apk或ipa文件</p>
            </td>
            <td class="sp-form-cell-value">
                <input type="text" id="packageUpload" name="packageUpload" readonly="readonly" /><span id="spanButtonPlaceHolder">浏览</span><input
                        type="button" value="上传" id="btnUpload" />
            </td>
        </tr>
    </table>
    <%} %>
</body>
</html>
