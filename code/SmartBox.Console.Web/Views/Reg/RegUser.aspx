<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_UserList>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SmartBox控制台用户注册</title>
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
    <script type="text/javascript">
        function save() {
            $("#frmUser").submit();
        }

        $("document").ready(function () {
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            $("a#Save").click(save);

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
                            window.history.back(-1); 
                        });
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                    }
                }
            };

            //保存之验证
            $("#frmUser").validate({
                rules: {
                    UL_UID: { required: true,minlength:6},
                    UL_PWD: { required: true },
                    UL_RPWD: { equalTo: "#UL_PWD" },
                    UL_MailAddress: { required: true, email: true },
                    UL_MobilePhone: { required: true, isMobile: true },
                    UL_Sequence: { digits: true, min: 1 }
                },
                messages: {
                    UL_UID: { required: "请填写账号", minlength: "长度至少为6个字符" },
                    UL_PWD: { required: "请填写密码" },
                    UL_RPWD: { required: "请填写密码", equalTo: "请填写相同的密码" },
                    UL_MailAddress: { required: "请填写e-mail地址" },
                    UL_MobilePhone: { required: "请输入手机号码", isMobile: "请输入11位手机号码" },
                    UL_Sequence: { min: "请输入顺序号(正整数)" }
                },
                submitHandler: function (form) {
                    $("#frmUser").ajaxSubmit(saveoptions);
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: showerror
            });

            jQuery.validator.addMethod("isMobile", function (value, element) {
                var length = value.length;
                var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
                return this.optional(element) || (length == 11 && mobile.test(value));
            }); //"请输入手机号码"

            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#frmUser");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }
        });
    </script>
</head>
<body>
    <div id="main_content">
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
        <div id="divForm">
            <% using (Html.BeginForm("RegUser", "Reg", FormMethod.Post, new { id = "frmUser" }))
               {
                   Html.AntiForgeryToken();
                   Html.ValidationSummary(true);%>            
            <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        用户账号：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_UID", (Model == null ? "" : Model.UL_UID), new { @Style = "width:95%;", @Class = "Unit_Name" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        姓名：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_Name", (Model == null ? "" : Model.UL_Name), new { @Style = "width:95%;", @Class = "Unit_Name" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        密码：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_PWD", (Model == null ? "" : Model.UL_PWD), new { @Style = "width:95%;", @Class = "Unit_Name", @Type = "password" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        确认密码：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_RPWD", (Model == null ? "" : Model.UL_PWD), new { @Style = "width:95%;", @Class = "Unit_Name", @Type = "password" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        电话号码：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_MobilePhone", (Model == null ? "" : Model.UL_MobilePhone), new { @Style = "width:95%;", @Class = "Unit_Name" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        电子邮件：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_MailAddress", (Model == null ? "" : Model.UL_MailAddress), new { @Style = "width:95%;", @Class = "Unit_Name" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        所属单位：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.DropDownList("Unit_ID", ViewData["Unit_ID"] as SelectList, new { @Style = "width:95%;", @Class = "Unit_Name" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        排序号：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_Sequence", (Model == null ? 0 : Model.UL_Sequence), new { @Style = "width:95%;", @Class = "Unit_Sequence" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        备注：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextArea("UL_Demo", (Model == null ? "" : Model.UL_Demo), new { @Style = "width:95%;", @Class = "Unit_Demo" })%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        性别：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.DropDownList("UL_Gender", ViewData["UL_Gender"] as SelectList, new { @Style = "width:95%;", @Class = "Unit_Name" })%>
                    </td>
                </tr>
            </table>
            <% } %>
        </div>
    </div>
</body>
</html>
