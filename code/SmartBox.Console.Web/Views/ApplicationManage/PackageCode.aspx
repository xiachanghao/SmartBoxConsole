﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>生成二维码</title>
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
            $("#CodePreview").hide();
            var imgsrc = '<%=ViewData["ImgSrc"]%>';
            if (imgsrc != 'NoCode') {
                //$("#ImgCode").attr("src", imgsrc);
                document.getElementById("ImgCode").src = imgsrc;
                $("#CodePreview").show();
            }

            $("#CloseImgBtn1").click(function () {
                CloseModelWindow(null, true);
            });

            $("a#Save").click(function () {
                $("#frmPackageCode").submit();
            });


            //上传之验证
            $("#frmPackageCode").validate({
                rules: {
                    pk_url: { required: true }
                },
                messages: {
                    pk_url: { required: "请输入安装包下载地址!" }
                },
                submitHandler: function (form) {
                    $("#frmPackageCode").ajaxSubmit({
                        beforeSumbit: function () {
                            $("#loadingpannel").html("正在保存数据...").show();
                            return true;
                        },
                        dataType: "json",
                        success: function (data) {
                            if (data.IsSuccess) {
                                $("#loadingpannel").hide();
                                hiAlert('创建二维码成功.', '提示', function () {
                                    //parent.$.closeIfrm(null, true);
                                    $("#Save").hide();
                                    document.getElementById("ImgCode").src = data.Msg;
                                    //$("#ImgCode").attr("src", data.Msg);
                                    $("#CodePreview").show();
                                    location.reload();
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
                    var form = $("#frmPackageCode");
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
        <legend>生成二维码</legend>
        <% using (Html.BeginForm("CreatePackageCode", "ApplicationManage", FormMethod.Post, new { id = "frmPackageCode", enctype = "multipart/form-data" }))
           {
               Html.AntiForgeryToken();
               Html.ValidationSummary(true);%>
          
         <div class="toolBotton">
            <a id="Save" class="imgbtn"><span class="Save" title="保存">生成</span></a><%-- <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>--%>
        </div>   
        <table class="sp-form" width="100%" cellspacing="1" cellpadding="0">          
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
                        安装包下载地址：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("pk_url", ViewData["url"], new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>    
              <tr id="CodePreview">
                    <td class="sp-form-cell-name">
                        二维码：
                    </td>
                    <td class="sp-form-cell-value">
                        <img id="ImgCode" src="../../Images/icons/arrow_down.png" alt="未找到"/> 
                    </td>
              </tr>               
        </table>
        <%} %>
    </fieldset>
        
</body>
</html>
