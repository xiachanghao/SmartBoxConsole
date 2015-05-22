<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.IOSOutsideApp>" %>
<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EditIOSOutsideApp</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dp.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="Stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/jquery.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/pluginssource/jquery.form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <%--<script src="<%=Url.Content("~/Javascripts/Plugins/jquery.metadata.js")%>" type="text/javascript"></script>--%>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.datepicker.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.dropdown.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="<%=Url.Content("~/")%>jquery-easyui-1.3.6/themes/bootstrap/easyui.css">
    <link rel="stylesheet" type="text/css" href="<%=Url.Content("~/")%>jquery-easyui-1.3.6/themes/icon.css">
    <script src="<%=Url.Content("~/jquery-easyui-1.3.6/jquery.easyui.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Url.Content("~/")%>jquery-easyui-1.3.6/locale/easyui-lang-zh_CN.js"></script>

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
                window.parent.CloseWind();
            });

            $("a#Save").click(function () {
                $("#frmApplicationPackage").submit();
            });

            $('input[id^="ClientType_"]').click(function () {

                $('input[name="ClientType"]').each(function () {
                    $(this)[0].checked = false;
                });
                $(this)[0].checked = true;
            });

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
                                //$("#loadingpannel").hide();
                                hiAlert(data.Msg, '提示', function () {
                                    parent.CloseWind(true);
                                });
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                            }
                        }, error: function (data) {
                            var o = eval("(" + data.responseText.replace(/<[^>]+>/img, '') + ")");

                            //                            var o = eval(data.responseText.replace('<pre>', '').replace('</pre>', ''));
                            hiAlert(o.Msg, '提示', function () {
                                parent.CloseWind(false);
                            });
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
            //bindClassValidate("DisplayName", $.validator.methods.required, "没有安装包名称,请输入正确的安装包名！");
            //bindClassValidate("Unit", $.validator.methods.required, "请选择单位");
            //bindClassValidate("Firmware", $.validator.methods.required, "请录入支持的固件！");
            //bindClassValidate("Scheme", $.validator.methods.required, "请录入Scheme！");
            //            bindClassValidate("PackageClientType", $.validator.methods.required, "没有安装包客户端类型,请上传正确的安装包");
            //            bindClassValidate("PackageVersion", $.validator.methods.required, "没有安装包名称,请上传正确的安装包");
            //            bindClassValidate("PackageBuildVer", $.validator.methods.required, "没有安装包内部版本号,请上传正确的安装包");
            //            bindClassValidate("PackageDisplayName", $.validator.methods.required, "请填写安装包显示名称");
            //            bindClassValidate("AppClientType", $.validator.methods.required, "没有应用客户端类型,请上传正确的安装包");
            //            bindClassValidate("ActivityShortName", $.validator.methods.required, "没有Action简称,请上传正确的安装包");
            //            bindClassValidate("ActivityName", $.validator.methods.required, "没有Action名称,请上传正确的安装包");
            //            bindClassValidate("ActivityDisplayName", $.validator.methods.required, "请填写显示名称");
            //            bindClassValidate("ActivitySeq", $.validator.methods.required, "请填写排序号");
            //            bindClassValidate("ActivitySeq", $.validator.methods.number, "排序号必须是数字");
            //            bindClassValidate("AppSeq", $.validator.methods.required, "请填写排序号");
            //            bindClassValidate("AppSeq", $.validator.methods.number, "排序号必须是数字");
            function bindClassValidate(name, method, message) {
                $.validator.methods[name] = method;
                $.validator.messages[name] = message;
                $.validator.addClassRules(name, $.validator.normalizeRule(name));
            }
        });

        function selectImage(elementid, imgid) {
            $("#" + elementid).val('Server://beyondbit.smartbox.server.image/' + imgid);
        }

        function CloseWind() {
            $('#w').window('close');
        }

        function SelectIco(txtIco) {
            if (!txtIco || txtIco == "") {
                return;
            }

            var url = '<%= Url.Action("SelectImage","ImageManage") %>?modal=0&elementid=' + txtIco;

            $('#wif')[0].src = url;
            $('#w').window('open');
            var height = document.body.scrollHeight || document.documentElement.scrollHeight || 0;
            $('.window-mask').height(height);
            $('#w').window('vcenter');
            return;

//            var url = '<%= Url.Action("SelectImage","ImageManage") %>';
//            var imgID = showModalDialog(url, "", 'dialogHeight=500;dialogWidth=700;center=yes;help=no; scroll=yes;resizable=no;status=no');
//            if (imgID) {
//                $("#" + txtIco).val('Server://beyondbit.smartbox.server.image/' + imgID);
//            }
        }
    </script>
</head>
<body>
    <%  var SourceClientType = ViewData["ClientType"] as System.Data.DataTable;
        var SourceApplication = ViewData["Application"] as System.Data.DataTable;
    %>
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
    <% using (Html.BeginForm("EditIOSOutsideApp", "IOSOutsideAppManage", FormMethod.Post, new { id = "frmApplicationPackage" }))
       { %>
    <fieldset>
        <legend>IOS外部应用信息</legend>
        <fieldset>
            <table width="100%" cellpadding="0" cellspacing="1" style="margin-bottom: 10px;"
                class="sp-form">
                <tr>
                    <td class="sp-form-cell-name">
                        显示名称：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("DisplayName", ViewData["DispName"], new { @Style = "width:75%;", @Class = "{validate:{required:true}}" })%>
                    </td>
              </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        关联应用：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.CheckBoxList("AppID", new SelectHelper(SourceApplication, "未指定", "").GetSelectList("DisplayName", "ID", Model.AppID, true), new {@Class="{validate:{required:true, minlength:1}}" })%>
                        <%--<%= Html.Hidden("AppName_" + appIndexStr, application.AppCode)%>
                        <%= Html.Hidden("activityCount_" + appIndexStr,application.ActionList.Count)%>--%>
                    </td>
                </tr>
                <tr id="IconUrl">
                    <td class="sp-form-cell-name">
                        图标地址：
                    </td>
                    <td class="sp-form-cell-value">
                        <input type="file" id="AppIcon" name="AppIcon"  />                     
                    </td>
                    </tr>  
                <tr>
                    <td class="sp-form-cell-name">
                        分类：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("Cate")%>
                    </td>
               </tr>
                <tr>
                     <td class="sp-form-cell-name">
                        所属单位：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("Unit", ViewData["Unit"] as SelectList, new { @Style = "width:95%;", @Class = "{required:true}" })%>
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
                        支持固件：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Firmware", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
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
              <tr>
                    <td class="sp-form-cell-name">
                        Build：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("BuildVer", "", new { @Style = "width:75%;", @Class = "DisplayName" })%>
                    </td>
              </tr>
              
                <tr>
                    <td class="sp-form-cell-name">
                        图标：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.TextBox("IconUri" , Model.IconUri, new { @Style = "width:85%;", @Class = "AppIco" })%>
                        <input type="button" value="…" style="width: 5%;" onclick="SelectIco('<%= "IconUri"  %>')" />
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        应用排序号：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.TextBox("Seq" , Model.Seq, new { @Style="width:40px;",@class="AppSeq" })%>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        客户端类型：
                    </td>
                    <td class="sp-form-cell-value">
                        <% if (Model.ID == null || Model.ID == 0)
                           { %>
                        <%=Html.CheckBoxList("ClientType" , new SelectHelper(SourceClientType).GetSelectListItem("DisplayName", "ClientType", false))%>
                        <%}
                           else
                           { %>
                        <%=Html.CheckBoxList("ClientType" , new SelectHelper(SourceClientType).GetSelectListItem("DisplayName", "ClientType", Model.ClientType.Split('|').ToList(), false))%>
                        <% } %>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        应用下载Uri：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Uri", Model.Uri, new { @Style = "width:95%;", @Class = "DisplayName" })%>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        应用Schema：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.TextBox("Scheme", Model.Scheme, new { @Style = "width:95%;", @Class = "{validate:{required:true}}" })%>
                    </td>
                </tr>
            </table>
            <%= Html.Hidden("ID", Model.ID)%>
            <%= Html.Hidden("CreateTime", Model.CreateTime)%>
            <%= Html.Hidden("CreateUid", Model.CreateUid)%>
        </fieldset>
    </fieldset>
    <% } %>

    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</body>
</html>
