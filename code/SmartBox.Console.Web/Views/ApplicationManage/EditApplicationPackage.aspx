<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.Package4AI>" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EditApplicationPackage</title>
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
        var swfu;
        $("document").ready(function () {
        <%if (Model == null) {%>

        hiAlert("未找到安装包,是否未正确发布安装包?请重新发布或更新后再执行此操作!", '提示', function () {
                                    parent.CloseWind(true);
                                });
        
        });
        </script>
        <%Response.End(); %>
        <%}%>
        
            $("#CloseImgBtn1").click(function () {
                //CloseModelWindow(null, true);
                parent.CloseWind(false);
            });

            $("a#Save").click(function () {
                $("#frmApplicationPackage").submit();
            });

            //保存之验证
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
                                    //parent.$.closeIfrm(null, true);
                                    parent.CloseWind(true);
                                });
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                            }
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

            bindClassValidate("PackageName", $.validator.methods.required, "没有安装包名称,请上传正确的安装包");
            bindClassValidate("PackageType", $.validator.methods.required, "没有安装包类型,请上传正确的安装包");
            bindClassValidate("PackageClientType", $.validator.methods.required, "没有安装包客户端类型,请上传正确的安装包");
            bindClassValidate("PackageVersion", $.validator.methods.required, "没有安装包名称,请上传正确的安装包");
            bindClassValidate("PackageBuildVer", $.validator.methods.required, "没有安装包内部版本号,请上传正确的安装包");
            bindClassValidate("PackageDisplayName", $.validator.methods.required, "请填写安装包显示名称");
            bindClassValidate("AppClientType", $.validator.methods.required, "没有应用客户端类型,请上传正确的安装包");
            bindClassValidate("ActivityShortName", $.validator.methods.required, "没有Action简称,请上传正确的安装包");
            bindClassValidate("ActivityName", $.validator.methods.required, "没有Action名称,请上传正确的安装包");
            bindClassValidate("ActivityDisplayName", $.validator.methods.required, "请填写显示名称");
            bindClassValidate("ActivitySeq", $.validator.methods.required, "请填写排序号");
            bindClassValidate("ActivitySeq", $.validator.methods.number, "排序号必须是数字");
            bindClassValidate("AppSeq", $.validator.methods.required, "请填写排序号");
            bindClassValidate("AppSeq", $.validator.methods.number, "排序号必须是数字");
            function bindClassValidate(name, method, message) {
                $.validator.methods[name] = method;
                $.validator.messages[name] = message;
                $.validator.addClassRules(name, $.validator.normalizeRule(name));
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
    <% using (Html.BeginForm("EditApplicationPackage", "ApplicationManage", FormMethod.Post, new { id = "frmApplicationPackage" }))
       { %>
    <fieldset>
        <legend>安装包信息</legend>
        <table class="sp-form" width="100%" cellspacing="" cellpadding="1">
            <tr>
                <td class="sp-form-cell-name">
                    安装包显示名称：
                </td>
                <td class="sp-form-cell-value" colspan="3">
                    <%= Html.TextBox("packageDisplayName", Model == null ? "" : (Model.DisplayName ?? ""), new { @Style = "width:98%;" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    安装包名称：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageName", Model == null ? "" : (Model.Name ?? ""), new { @Readonly = "readonly", @Style = "border:0px;width:98%;" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    安装包类型：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageType",Model.Type, new { @Readonly = "readonly", @Style = "border:0px;width:80px;"})%>
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
                <td class="sp-form-cell-value" colspan="3">
                    <%=Html.CheckBoxList("packageViewClientType", new SelectHelper(SourceClientType).GetSelectListItem("DisplayName", "ClientType", Model.ClientType.Split('|').ToList(), false), new { @disabled = "disabled" })%>
                    <%=Html.Hidden("packageClientType",Model.ClientType)%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    发布版本号：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageVersion", Model.Version, new { @Readonly = "readonly", @Style = "border:0px;width:98%;" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    内部版本号：
                </td>
                <td class="sp-form-cell-value">
                    <%= Html.TextBox("packageBuildVer", Model.BuildVer, new { @Readonly = "readonly", @Style = "border:0px;width:98%;" })%>
                </td>
            </tr>
            <tr>
                <td class="sp-form-cell-name">
                    安装包显示名称：
                </td>
                <td class="sp-form-cell-value" colspan="3">
                    <%= Html.TextArea("packageDescription", Model.Description, new { @Style = "width:98%;", @Class = "PackageDescription" })%>
                </td>
            </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        是否推荐：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("pe_IsTJ")%>
                    </td>
             </tr>
             <tr>
                    <td class="sp-form-cell-name">
                        是否必备：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("pe_IsBB")%>
                    </td>
              </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>应用信息</legend>
        <% for (int appIndex = 0; appIndex < Model.App4AIList.Count; appIndex++)
           {
               var application = Model.App4AIList[appIndex];
               string appIndexStr = appIndex.ToString();
        %>
        <fieldset>
            <legend>
                <%= application.AppCode %></legend>
            <table width="100%" cellpadding="0" cellspacing="1" style="margin-bottom: 10px;"
                class="sp-form">
                <tr>
                    <td class="sp-form-cell-name">
                        关联应用：
                    </td>
                    <td class="sp-form-cell-value">
                        <%= Html.DropDownList("AppCode_" + appIndexStr, new SelectHelper(SourceApplication, "未指定", "").GetSelectList("Name", "ID", application.AppID, true))%>
                        <%= Html.Hidden("AppName_" + appIndexStr, application.AppCode)%>
                        <%= Html.Hidden("activityCount_" + appIndexStr,application.ActionList.Count)%>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        图标：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.TextBox("AppIco_" + appIndexStr, application.IconUri, new { @Style = "width:85%;", @Class = "AppIco" })%>
                        <input type="button" value="…" style="width: 5%;" onclick="SelectIco('<%= "AppIco_" + appIndexStr %>')" />
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        应用排序号：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.TextBox("AppSeq_" + appIndexStr, application.Seq, new { @Style="width:40px;",@class="AppSeq" })%>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        客户端类型：
                    </td>
                    <td class="sp-form-cell-value">
                        <%=Html.CheckBoxList("AppClientType_" + appIndexStr, new SelectHelper(SourceClientType).GetSelectListItem("DisplayName", "ClientType", application.ClientType.Split('|').ToList(), false), new { @disabled = "disabled" })%>
                        <%=Html.Hidden("AppCheckClentType_" + appIndexStr,application.ClientType)%>
                        <%=Html.Hidden("AppID_" + appIndexStr,application.ID)%>
                        <%=Html.Hidden("AppCreateUser_" + appIndexStr,application.CreateUid)%>
                        <%=Html.Hidden("AppCreateDateTime_" + appIndexStr,application.CreateTime)%>
                        <%=Html.Hidden("AppPackageID_" + appIndexStr,application.Package4AIID)%>
                        <%=Html.Hidden("AppPackageName_"+appIndexStr,application.PackageName) %>
                    </td>
                </tr>
                <tr>
                    <td class="sp-form-cell-name">
                        Action：
                    </td>
                    <td class="sp-form-cell-value">
                        <% for (int actionIndex = 0; actionIndex < application.ActionList.Count; actionIndex++)
                           {
                               var action = application.ActionList[actionIndex];
                               string actionIndexStr = actionIndex.ToString();
                        %>
                        <table width="100%" cellpadding="0" cellspacing="1" class="sp-form">
                            <tr style="display: none">
                                <td class="sp-form-cell-name">
                                    Action简称:
                                </td>
                                <td class="sp-form-cell-value">
                                    <%=Html.TextBox("ActivityShortName_" + appIndexStr + "_" + actionIndexStr, action.ShortName, new { @Readonly = "readonly", @Style = "border:0px;width:90%;" })%>
                                </td>
                                <td class="sp-form-cell-name">
                                    Action名称:
                                </td>
                                <td class="sp-form-cell-value">
                                    <%=Html.TextBox("ActivityName_" + appIndexStr + "_" + actionIndexStr, action.Name, new { @Readonly = "readonly", @Style = "border:0px;width:90%;" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="sp-form-cell-name">
                                    显示名称:
                                </td>
                                <td class="sp-form-cell-value">
                                    <%=Html.TextBox("ActivityDisplayName_" + appIndexStr + "_" + actionIndexStr, action.DisplayName, new { @Style = "width:90%;", @Class = "ActivityDisplayName" })%>
                                </td>
                                <td class="sp-form-cell-name" style="display: none">
                                    启用Action:
                                </td>
                                <td class="sp-form-cell-value" style="display: none">
                                    <%=Html.RadioButton("ActivityLaunch_" + appIndexStr + "_" + actionIndexStr, true,action.IsLaunch, new { id = "EnableLaunch_" + appIndexStr + "_" + actionIndex.ToString() })%><label
                                        id="labEnableLaunch_" for="EnableLaunch_">启用</label>
                                    <%=Html.RadioButton("ActivityLaunch_" + appIndexStr + "_" + actionIndexStr, false,!action.IsLaunch, new { id = "DesableLaunch_" + appIndexStr + "_" + actionIndex.ToString() })%><label
                                        id="labDesableLaunch_" for="DesableLaunch_">禁用</label>
                                </td>
                                <td class="sp-form-cell-name">
                                    排序号:
                                </td>
                                <td class="sp-form-cell-value">
                                    <%=Html.TextBox("ActivitySeq_" + appIndexStr + "_" + actionIndexStr, action.Seq, new { @Style = "width:50px;", @Class = "ActivitySeq" })%>
                                    <%=Html.Hidden("ActivityAppID_" + appIndexStr + "_" + actionIndexStr,action.App4AIID)%>
                                    <%=Html.Hidden("ActivityCreateUser_" + appIndexStr + "_" + actionIndexStr,action.CreateUid)%>
                                    <%=Html.Hidden("ActivityCreateDateTime_" + appIndexStr + "_" + actionIndexStr,action.CreateTime)%>
                                </td>
                            </tr>
                            <tr>
                                <td class="sp-form-cell-name">
                                    Action图标:
                                </td>
                                <td class="sp-form-cell-value" colspan="5">
                                    <%=Html.TextBox("ActivityIco_" + appIndexStr + "_" + actionIndexStr, action.IconUri, new { @Style = "width:85%;" })%>
                                    <input type="button" value="…" style="width: 5%;" onclick="SelectIco('<%= "ActivityIco_" + appIndexStr + "_" + actionIndexStr %>')" />
                                </td>
                            </tr>
                        </table>
                        <% } %>
                    </td>
                </tr>
            </table>
        </fieldset>
        <% } %>
    </fieldset>
    <%= Html.Hidden("applicationCount",Model.App4AIList.Count) %>
    <%= Html.Hidden("packageCreateUser",Model.CreateUid) %>
    <%= Html.Hidden("packageCreateDateTime",Model.CreateTime) %>
    <%= Html.Hidden("packageDownloadUrl",Model.DownloadUri) %>
    <%= Html.Hidden("packageID",Model.ID) %>
    <% } %>
</body>
</html>
