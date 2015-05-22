<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_Package4Out>" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改外部安装包</title>
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
        .link_down 
        {
            color:#0001FD;
            text-decoration:"underline";
        }
        img#imgIcon 
        {
            width:65px;
            height:65px;
            margin-top:5px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $("document").ready(function () {

            $("#CloseImgBtn1").click(function () {
                //CloseModelWindow(null, true);
                parent.CloseWind(false);
            });

            $("a#Save").click(function () {
                $("#frmUploadPackage").submit();
            });


            //上传之验证
            $("#frmUploadPackage").validate({
                rules: {
                    packageUpload: { accept: "ipa|apk" },
                    AppIcon: { accept: "png" },
                    ClientType: { required: true }
                },
                messages: {
                    packageUpload: { accept: "请选择IOS或Android软件包!!" },
                    AppIcon: { accept: "请选择PNG文件!!" },
                    ClientType: { required: "请选择客户端类型" }
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
        <% using (Html.BeginForm("EditOutPackage", "ApplicationManage", FormMethod.Post, new { id = "frmUploadPackage", enctype = "multipart/form-data" }))
           { %>
          
         <div class="toolBotton">
            <a id="Save" class="imgbtn"><span class="Save" title="保存">保存</span></a> <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
        </div>   
        <table class="sp-form" width="100%" cellspacing="1" cellpadding="0">
            <tr>
                <td class="sp-form-cell-name">
                    上传新外部安装包：
                </td>
                <td class="sp-form-cell-value">
                    <input type="file" id="packageUpload" name="packageUpload"  />
                    <div>
                    下载地址：<a class="link_down" href="<%=ViewData["DownUrl"].ToString()%>" target=_blank><%=ViewData["DownUrl"].ToString()%></a>
                    </div>
                </td>
            </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        是否推荐：
                    </td>
                    <td class="sp-form-cell-value">
                         <%= Html.DropDownList("pe_IsTJ", ViewData["pe_IsTJ"] as SelectList, new { @Style = "width:95%;", @Class = "EnableType" })%>
                    </td>
             </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        是否必备：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("pe_IsBB", ViewData["pe_IsBB"] as SelectList, new { @Style = "width:95%;", @Class = "EnableType" })%>
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
                        <%=Html.CheckBoxList("cType", ViewData["ClientType"] as IEnumerable<SelectListItem>)%>
                    </td>
              </tr>
                 <tr>
                     <td class="sp-form-cell-name">
                        应用图标：
                    </td>
                   <td class="sp-form-cell-value">
                        <input type="file" id="AppIcon" name="AppIcon"  />  
                        <div>
                        <%if (ViewData["IconUrl"] != null && !String.IsNullOrEmpty(ViewData["IconUrl"].ToString()))
                          {%>
                        <img id="imgIcon" src="<%=Url.Content(ViewData["IconUrl"].ToString()) %>" />
                        <%} %>
                        </div>                     
                    </td>
                </tr>
              <tr>
                    <td class="sp-form-cell-name">
                        显示名称：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("DisplayName", (Model == null ? "" : Model.DisplayName), new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
              <tr>
                    <td class="sp-form-cell-name">
                        描述：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Description", (Model == null ? "" : Model.Description), new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
              <tr>
                    <td class="sp-form-cell-name">
                        版本号：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Version", (Model == null ? "" : Model.Version), new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
              <tr style="display:none">
                    <td class="sp-form-cell-name">
                       ID：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("po_ID", (Model == null ? "" : Model.po_ID.ToString()), new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
        </table>
        <%} %>
    </fieldset>
        
</body>
</html>

