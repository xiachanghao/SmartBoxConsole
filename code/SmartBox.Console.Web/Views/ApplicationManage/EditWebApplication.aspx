<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.WebApplication>" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑Application</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dp.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="Stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.datepicker.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.dropdown.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
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
        table
        {
            border: 1px;
            border-style: solid;
            border-collapse: collapse;
            line-height: 30px;
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
        div.checkboxlist div.list
        {
            float: left;
            margin: 2px 2px 0px 3px;
        }
        img#imgIcon 
        {
            width:65px;
            height:65px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function icon_onselect() {
            return;
            $('img#imgIcon').attr('src', getPath(document.forms[0].AppIcon)); // $('input#AppIcon').val()
        }

        function getPath(obj) {
            if (obj) {
                if (window.navigator.userAgent.indexOf("MSIE") >= 1) {
                    obj.select();
                    return document.selection.createRange().text;
                } else if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
                    if (obj.files) {
                        return obj.files.item(0).getAsDataURL();
                    }
                    return obj.value;
                }
                return obj.value;
            }
        }
        $("document").ready(function () {

            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            $("a#Save").click(save);
            $("#CloseImgBtn1").click(function () {
                //CloseModelWindow(null, true);
                window.parent.CloseWind();
            });

            $('input[id^="ClientType_"]').click(function () {

                $('input[name="ClientType"]').each(function () {
                    $(this)[0].checked = false;
                });
                $(this)[0].checked = true;
            });

            function save() {
//                var category = new Array();
//                $("input[name='cType']").each(function () {
//                    if (this.checked) {
//                        category.push($(this).val());
//                    }
//                });

                //                $("#ClientType").val(category.join('|'));   
                //window.parent.CloseWind(true);             
                $("#frmWebApplicationInfo").submit();
            }

            var saveoptions =
            {
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
                            window.parent.CloseWind(true);
                        });
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                    }
                }
            };

            //保存之验证
            $("#frmWebApplicationInfo").validate({
                rules: {
                    ShortName: { required: true },
                    Uri: { required: true, url: true },
                    Seq: { number: true, required: true },
                    AppIcon: { accept: "png" },
                    ClientType: { required: true },
                    Unit: { required: true }
                },
                messages: {
                    ShortName: { required: "请填写应用简称!" },
                    Uri: { required: "请填写应用地址!", url: "请输入合法网址!" },
                    Seq: { number: "排序号必须是数字", required: "请填写排序号" },
                    AppIcon: { accept: "请选择PNG文件!!" },
                    ClientType: { required: "请选择客户端类型!" },
                    Unit: { required: "请选择单位!" }
                },
                submitHandler: function (form) {
                    $("#frmWebApplicationInfo").ajaxSubmit(saveoptions);
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: showerror
            });

            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#frmWebApplicationInfo");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }

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
    <link href="<%=Url.Content("~/Themes/Default/toolbuttonfix.css")%>" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="ajaxmsgpanel">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>
    </div>
    <div class="toolBotton">
        <a id="Save" class="imgbtn"><span class="Save" title="保存">保存</span></a> <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
    </div>
    <%
        var SourceApplication = ViewData["Application"] as System.Data.DataTable;
    %>
    <% using (Html.BeginForm("EditWebApplication", "ApplicationManage", FormMethod.Post, new { id = "frmWebApplicationInfo" }))
       { %>
    <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                应用简称：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%=Html.TextBox("ShortName", Model == null ? (ViewData["entity"] != null ? (((SmartBox.Console.Common.Entities.SMC_PackageExt)ViewData["entity"]).pe_DisplayName ?? "") : "" ) : Model.ShortName)%>
            </td>
        </tr>
        <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        关联应用：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.CheckBoxList("ApplicationID" , new SelectHelper(SourceApplication, "未指定", "").GetSelectList("DisplayName", "ID", Model.AppID, true))%>
                        <%--<%= Html.Hidden("AppName_" + appIndexStr, application.AppCode)%>
                        <%= Html.Hidden("activityCount_" + appIndexStr,application.ActionList.Count)%>--%>
                    </td>
                </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                应用分类：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">

                <%= Html.DropDownList("AppID")%>

            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                所属单位：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.DropDownList("Unit", ViewData["Unit"] as SelectList, new { @Style = "width:95%;", @Class = "EnableType" })%>
            </td>
        </tr>
         <tr>
                <td class="bbit-form-cell-name tdtop tdleft tdright">
                        客户端类型：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%=Html.CheckBoxList("ClientType", ViewData["ClientType"] as IEnumerable<SelectListItem>)%>
                    </td>
              </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                应用地址：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%=Html.TextBox("Uri", Model == null ? (ViewData["entity"] != null ? (((SmartBox.Console.Common.Entities.SMC_PackageExt)ViewData["entity"]).pe_DownloadUri ?? "") : "") : Model.Uri)%>
            </td>
        </tr>
         <tr>
                     <td class="bbit-form-cell-name tdtop tdleft tdright">
                        应用图标：
                    </td>
                   <td class="bbit-form-cell-value tdtop tdright">
                        <input type="file" id="AppIcon" name="AppIcon" onchange="javascript:return icon_onselect()"  />   
                        <div>
                        <%if (ViewData["IconUrl"] != null && !String.IsNullOrEmpty(ViewData["IconUrl"].ToString()))
                          {%>
                        <img id="imgIcon" src="<%=Url.Content(ViewData["IconUrl"].ToString()) %>" />
                        <%} %>
                        </div>                   
                    </td>
          </tr>
         <tr>
                  <td class="bbit-form-cell-name tdtop tdleft tdright">
                        是否推荐：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                        <%= Html.DropDownList("pe_IsTJ", ViewData["pe_IsTJ"] as SelectList, new { @Style = "width:95%;", @Class = "EnableType" })%>
                    </td>
             </tr>
             <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        是否必备：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                     <%= Html.DropDownList("pe_IsBB", ViewData["pe_IsBB"] as SelectList, new { @Style = "width:95%;", @Class = "EnableType" })%>
                    </td>
              </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                排序号：
            </td>
            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                <%=Html.TextBox("Seq", Model != null ? Model.Seq : 0)%>
            </td>
        </tr>
    </table>
    <input name="id" value="<%=Request.QueryString["id"] %>" type="hidden" />
    <%--<%= Html.Hidden("ID", Model == null ? (ViewData["entity"] != null ? ((SmartBox.Console.Common.Entities.SMC_PackageExt)ViewData["entity"]).TableID : 0 ) :Model.ID)%>--%>
    <%= Html.Hidden("CreateTime", Model == null ? (ViewData["entity"] != null ? ((SmartBox.Console.Common.Entities.SMC_PackageExt)ViewData["entity"]).pe_CreatedTime : DateTime.Now) : Model.CreateTime)%>
    <%= Html.Hidden("CreateUid", Model == null ? (ViewData["entity"] != null ? (((SmartBox.Console.Common.Entities.SMC_PackageExt)ViewData["entity"]).pe_CreateUid ?? "") : "" ) : Model.CreateUid)%>
    <%= Html.Hidden("ClientType", Model == null ? (ViewData["entity"] != null ? (((SmartBox.Console.Common.Entities.SMC_PackageExt)ViewData["entity"]).pe_ClientType ?? "") : "" ) :Model.ClientType)%>
    <% } %>
</body>
</html>
