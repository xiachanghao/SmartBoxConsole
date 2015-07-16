<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/pluginssource/jquery.form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/toolbuttonfix.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />

    <script>
        $(document).ready(function () {
            $("#CloseImgBtn1").click(function () {
                CloseModelWindow(null, true);
            });

            $("#SaveImgBtn").click(function () {
                hiAlert("", '提示', function () {  });
                $("#frmChangePackage").ajaxSubmit({
                    beforeSumbit: function () {
                        $("#loadingpannel").html("正在保存数据...").show();
                        return true;
                    },
                    dataType: "json",
                    success: function (data) {
                        if (data.IsSuccess) {
                            $("#loadingpannel").hide();
                            hiAlert(data.Msg, '提示', function () {
                                CloseModelWindow(null, true);
                            });
                        }
                        else {
                            hiAlert(data.Msg, '提示', function () { $("#loadingpannel").hide(); });
                        }
                    }
                });
            });

        });
    </script>
</head>
<body>
    <div>
        <div class="ajaxmsgpanel">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                正在保存数据...
            </div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                非常抱歉，无法执行您的操作，请稍后再试
            </div>
            <div title="文件上传进度" id="fileProgress" style="display: none">
                <div id="progressbar">
                </div>
                <button id="cancelButton">
                    取消上传</button>
                当前上传速度： <span id="CurrentSpeed"></span>
            </div>
        </div>
        <div class="toolBotton">
            <a id="SaveImgBtn" class="imgbtn"><span class="Save" title="保存">保存</span></a> <a id="CloseImgBtn1"
                class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
        </div>
        <% using (Html.BeginForm("ChangePassword","Home", FormMethod.Post, new { id = "frmChangePackage", enctype = "multipart/form-data" }))
           {
               Html.AntiForgeryToken();
               Html.ValidationSummary(true);%>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>旧密码：</td>
                <td><%= Html.Password("oldPassword") %></td>
            </tr>
            <tr>
                <td>新密码：</td>
                <td><%= Html.Password("newPassword") %></td>
            </tr>
            <tr>
                <td>确认新密码：</td>
                <td><%= Html.Password("renewPassword") %></td>
            </tr>
        </table>
        <%} %>
    </div>
</body>
</html>
