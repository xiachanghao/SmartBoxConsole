<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.VersionTrack>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EditVerInfo</title>
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

            var IsAdd = '<%=ViewData["IsAdd"]%>';


            $("#Butsubmit").click(LoadFileToMem);

            $("#FileUpload1").change(function() {

                var v = $(this).val();
                if (/.+\.zip$/i.test(v) == false) {
                    alert("请选择zip或ZIP文件上传");
                    return false;
                }
            });


            //上传到内存中
            function LoadFileToMem() {
                if (IsAdd == "1")//若为新增或者升级 
                {
                    var v = $("#FileUpload1").val();
                    if ($("#FileUpload1").val() == "") {
                        alert('请选择要上传的文件');
                        return false;
                    }
                    else if (/.+\.zip$/i.test(v) == false) {
                        alert("请选择zip或ZIP文件上传")
                        return false;
                    }
                    else {
                        hiConfirm("确定要上传文件吗？", "提示", function(btn) {
                            if (btn == true) {
                                $("#fmEdit").submit();
                            }
                        }); // end of hiConfirm
                    }

                }
                else {
                    if ($("#FileUpload1").val() != "") {
                        hiConfirm("确定要重新上传文件吗,原上传文件将被删除", "提示", function(btn) {
                            if (btn == true) {
                                $("#fmEdit").submit();
                            }
                        }); // end of hiConfirm
                    }
                    else {
                        hiConfirm("您没有选择上传文件，程序将使用您上一次上传的文件 ", "提示", function(btn) {
                            if (btn == true) {
                                $("#fmEdit").submit();
                            }
                        }); // end of hiConfirm
                    }
                }

            }

            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("正在保存数据......").show();

                    return true;
                },
                dataType: "json",
                error: function(xhr, status, e) {
                    xhr.abort();
                    hiAlert("上传失败,文件太大(上限10M)或网络错误", '提示');
                    $("#loadingpannel").hide();

                },
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        var vid = "";
                        vid = data.Msg;
                        $("#VID").val(vid); //若是新增。得到对应版本ID，从而可以获取插件ID
                        openWindowsclick();

                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function() {
                        });

                    }
                }
            };
            $("#fmEdit").ajaxForm(options);

        });                        // ready

        function openWindowsclick() {
                var Id = $("#VID").val();
                var url = '<%=Url.Action("UpdatePluginInfo") %>/?verid=' + Id;
                window.location = url;
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
            <% using (Html.BeginForm("SaveVerInfo", "PluginManage", new { IsAdd = ViewData["IsAdd"] }, FormMethod.Post, new { id = "fmEdit", enctype = "multipart/form-data" }))
               {%>
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr>
                    <td>
                        <span style="color: Red">1.若是新增或者升级,请先上传文件</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">2.若已存在上传文件，原由文件将被删除(可直接下一步)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">3.请检查上传文件的指定格式：ZIP文件，请确保上传文件中的指定配置信息是否齐全(XML文件等格式正确)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">4.请确保上传文件中的指定配置信息是否齐全(XML文件等格式正确)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">5.上传文件后，上传的文件将存放在指定目录下创建的，以版本为名的新文件夹</span>
                    </td>
                </tr>
                 <tr>
                     <td align="center">
                        <div style="width: 79%; float: left">请选择上传文件：
                            <input type="file" name="FileUpload1" id="FileUpload1" style="width: 40%" />
                        </div>
                        <div style="width: 20%; text-align: center; float:right">
                        
                          <input type="button" id="Butsubmit" value="下一步" />
                        </div>
                    </td>
                </tr>
            </table>
            <%=Html.Hidden("VID", ViewData["versionId"])%>
            <%=Html.Hidden("VersionId", ViewData["versionId"])%>
            <%}%>
        </div>
    </div>
</body>
</html>
