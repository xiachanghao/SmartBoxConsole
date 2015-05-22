<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>创建截图</title>
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
    <link href="<%=Url.Content("~/Javascripts/JqueryUI/css/start/jquery-ui-1.8.13.custom.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/JqueryUI/jquery-ui-1.8.13.custom.min.js")%>" type="text/javascript"></script>
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
            border: 1px solid #343434;
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

            $("#CloseImgBtn1").click(function () {
                CloseModelWindow(null, true);
            });

            $("a#Save").click(function () {
                $("#frmPackagePic").submit();
            });


            //上传之验证
            $("#frmPackagePic").validate({
                rules: {
                    packageUpload: { required: true, accept: "jpg|jpeg|bmp|png|gif|ico" }
                },
                messages: {
                    packageUpload: { required: "请先选择要上传的截图!", accept: "请选择图片文件!" }
                },
                submitHandler: function (form) {
                    $("#frmPackagePic").ajaxSubmit({
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
                            } else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                            }
                        }
                    });
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: function showerror(error, target) {
                    var pos = target.position();
                    var height = target.height();
                    var newpos = { left: pos.left, top: pos.top + height + 2 }
                    var form = $("#frmPackagePic");
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
    <fieldset>
        <legend>截图上传</legend>
        <% using (Html.BeginForm("CreatePackagePicture", "ApplicationManage", FormMethod.Post, new { id = "frmPackagePic", enctype = "multipart/form-data" }))
           { %>
          
         <div class="toolBotton">
            <a id="Save" class="imgbtn"><span class="Save" title="保存">保存</span></a> <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
        </div>   
        <table class="sp-form" width="100%" cellspacing="1" cellpadding="0">
            <tr>
                <td class="sp-form-cell-name">
                    上传截图：
                </td>
                <td class="sp-form-cell-value">
                    <input type="file" id="packageUpload" name="packageUpload"  />
                </td>
            </tr>
            <tr style="display:none">
                    <td class="sp-form-cell-name">
                        ID：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("pe_id", ViewData["id"], new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        显示名称：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("pp_title", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
              <tr>
                    <td class="sp-form-cell-name">
                        描述：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("pp_desc", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
        </table>
        <%} %>
    </fieldset>
        
</body>
</html>
