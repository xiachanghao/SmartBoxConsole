<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

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
        var unitData = <%=ViewData["unitData"]%>;
        $("document").ready(function () {

            $("#CloseImgBtn1").click(function () {
                //CloseModelWindow(null, true);
                window.parent.CloseWind();
            });

            $("a#Save").click(function () {
                $("#frmUploadPackage").submit();
            });

            $('input[id^="ClientType_"]').click(function() {
                
                $('input[name="ClientType"]').each(function() {
                    $(this)[0].checked = false;
                });
                $(this)[0].checked = true;
            });

            //上传之验证
            $("#frmUploadPackage").validate({
                rules: {
                    packageUpload: { required: true, accept: "ipa|apk" },
                    AppIcon: { required: true, accept: "png" },
                    ClientType: { required: true }
                },
                messages: {
                    packageUpload: { required: "请先选择要上传的文件!!", accept: "请选择IOS或Android软件包!!" },
                    AppIcon: { required: "请先选择要上传的图标!", accept: "请选择PNG文件!!" },
                    ClientType: {required:"请选择客户端类型"}
                },
                submitHandler: function (form) {
                    $("#frmUploadPackage").ajaxSubmit({
                        beforeSumbit: function () {
                            $("#loadingpannel").html("正在保存数据...").show();
                            return true;
                        },
                        dataType: "json",
                        success: function (data) {
                            if (data.IsSuccess) {
                                $("#loadingpannel").hide();
                                hiAlert(data.Msg, '提示', function () {
                                    //parent.$.closeIfrm(null, true);
                                    parent.CloseWind(true);
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
        <legend>文件上传</legend>
        <%
        var SourceApplication = ViewData["Application"] as System.Data.DataTable;
    %>
        <% using (Html.BeginForm("UploadOutPackage", "ApplicationManage", FormMethod.Post, new { id = "frmUploadPackage", enctype = "multipart/form-data" }))
           {
               Html.AntiForgeryToken();
               Html.ValidationSummary(true);
               %>
         <div class="toolBotton">
            <a id="Save" class="imgbtn"><span class="Save" title="保存">保存</span></a> <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
        </div>   
        <table class="sp-form" width="100%" cellspacing="1" cellpadding="0">
            <tr>
                <td class="sp-form-cell-name">
                    上传外部应用：
                </td>
                <td class="sp-form-cell-value">
                    <input type="file" id="packageUpload" name="packageUpload"  />
                </td>
            </tr>
            <tr>
                    <td class="sp-form-cell-name">
                        显示名称：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("DisplayName", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
            <tr>
                    <td class="sp-form-cell-name">
                        关联应用：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.CheckBoxList("ApplicationID" , new SelectHelper(SourceApplication, "未指定", "").GetSelectList("DisplayName", "ID", "", true))%>
                        <%--<%= Html.Hidden("AppName_" + appIndexStr, application.AppCode)%>
                        <%= Html.Hidden("activityCount_" + appIndexStr,application.ActionList.Count)%>--%>
                    </td>
                </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        是否推荐：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("IsRecom") %>
                    </td>
             </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        是否必备：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("IsMust") %>
                    </td>
              </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        分类：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("AppID")%>
                    </td>
               </tr>
               <tr>
                     <td class="sp-form-cell-name">
                        所属单位：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("Unit", ViewData["Unit"] as SelectList, new { @Style = "width:95%;", @Class = "EnableType" })%>
                    </td>
              </tr>
              <tr>
                    <td class="sp-form-cell-name">
                        客户端类型：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.CheckBoxList("ClientType", ViewData["ClientType"] as IEnumerable<SelectListItem>)%>
                     </td>
             </tr>
             <tr>
                     <td class="sp-form-cell-name">
                        应用图标地址：
                    </td>
                   <td class="sp-form-cell-value">
                        <input type="file" id="AppIcon" name="AppIcon"  />                      
                    </td>
            </tr>
              <tr>
                    <td class="sp-form-cell-name">
                        支持固件：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Firmware", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>              
              <tr>
                    <td class="sp-form-cell-name">
                        描述：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Description", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
              <tr>
                    <td class="sp-form-cell-name">
                        版本号：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Version", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
        </table>
        <%} %>
    </fieldset>
        
</body>
</html>
