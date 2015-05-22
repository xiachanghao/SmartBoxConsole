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

            var vid = '<%=ViewData["versionId"]%>';


            $("#Butsubmit").click(LoadFileToMem);


            //上传到内存中
            function LoadFileToMem() {
                if (vid == "")//如果是新上传文件
                {
                    var v = $("#FileUpload1").val();
                    if ($("#FileUpload1").val() == "") {
                        hiAlert('请选择要上传的文件');
                        return false;
                    }
                    else if (/.+\.zip$/i.test(v) == false) {
                        hiAlert("请选择zip或ZIP类型文件上传")
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
                else 
                {
                    if ($("#FileUpload1").val() != "") {
                        hiConfirm("确定要重新上传文件吗,上一次上传的文件将被删除", "提示", function(btn) {
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
                        openWindowsclick(data.Msg);

                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function() {
                        });
                    }
                }
            };

            $("#fmEdit").ajaxForm(options);

            $("#FileUpload1").change(function() {
                var v = $(this).val();
                if (/.+\.zip$/i.test(v) == false) {
                    alert("请选择zip或ZIP文件上传");
                    clearFileInput(document.getElementById('FileUpload1'));
                    return false;
                }
            });

        });                           // ready


        //清空文件上传框,file为上传表单对像 
        function clearFileInput(file) {
            var form = document.createElement('form');
            document.body.appendChild(form);
            //记住file在旧表单中的的位置 
            var pos = file.nextSibling;
            form.appendChild(file);
            form.reset();
            pos.parentNode.insertBefore(file, pos);
            document.body.removeChild(form);
        }

        function openWindowsclick(Id) {
            var url = '<%=Url.Action("UpdateConfigInfo") %>/?verid=' + Id;
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
       <%-- <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">主程序上传</span>
            </div>
        </div>--%>
        <div class="bbit-categorycontainer">
            <% using (Html.BeginForm("SaveVerInfo", "MainProgramManage",  FormMethod.Post, new { id = "fmEdit", enctype = "multipart/form-data" }))
               {%>
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr>
                    <td>
                        <span style="color: Red">1.请先上传文件,若已存在上传文件，原由文件将被删除(可直接下一步)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">2.请检查上传文件的指定格式：ZIP文件，请确保上传文件中的指定配置信息是否齐全(XML文件等格式正确)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">3.请确保上传文件中的指定配置信息是否齐全(XML文件等格式正确)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">4.上传文件后，上传的文件将存放在指定目录下创建的，以版本为名的新文件夹</span>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div style="width: 79%; float: left">
                            请选择上传文件：
                            <input type="file" name="FileUpload1" id="FileUpload1" style="width: 40%" />
                        </div>
                        <div style="width: 20%; text-align: center; float: right">
                            <input type="button" id="Butsubmit" value="下一步" />
                        </div>
                    </td>
                </tr>
            </table>
            <%=Html.Hidden("VersionId", ViewData["versionId"])%>
            <%}%>
        </div>
    </div>
</body>
</html>
