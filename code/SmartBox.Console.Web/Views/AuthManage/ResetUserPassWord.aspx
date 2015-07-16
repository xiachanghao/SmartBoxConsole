<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AddModifyPage.Master"
    Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_UserList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    重新设置密码
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            <% using (Html.BeginForm("ResetUserPassWordSave", "AuthManage", FormMethod.Post, new { id = "frmUser" }))
               {
                   Html.AntiForgeryToken();
                   Html.ValidationSummary(true);%>
            <%=Html.Hidden("user_id", ViewData["user_id"])%>            
            <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
             <% if (ViewData["IsManager"] != "manager")
                   { %>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        原用户密码：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_OPWD", "", new { @Style = "width:95%;", @Class = "Unit_Name", @Type = "password"})%>
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        新用户密码：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_NPWD", "", new { @Style = "width:95%;", @Class = "Unit_Name", @Type = "password" })%>
                    </td>
                </tr>
               
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        确认密码：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%= Html.TextBox("UL_RPWD", "", new { @Style = "width:95%;", @Class = "Unit_Name", @Type = "password" })%>
                    </td>
                </tr>
            </table>
            <% } %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
            function save() {
                $("#frmUser").submit();
            }

              function close_window() {
                CloseModelWindow(null, true);
            }

$("document").ready(function () {
    $(".bbit-form tr:even").addClass("even");
    $(".bbit-form tr:odd").addClass("odd");
    $("a#Save").click(save);
    $("a#CloseImgBtn1").click(close_window);

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
                            parent.$.closeIfrm(null, true);
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
                    UL_OPWD: { required: true },
                    UL_NPWD: { required: true },
                    UL_RPWD:{ required: true,equalTo: "#UL_NPWD" }
                },
                messages: {
                    UL_OPWD: { required: "请填写原密码" },
                    UL_NPWD: { required: "请填写新密码" },
                    UL_RPWD: { required: "请确认新密码", equalTo:"新密码不一致"}
                },
                submitHandler: function (form) {
                    $("#frmUser").ajaxSubmit(saveoptions);
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: showerror
            });

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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StyleContent" runat="server">
.toolBotton {
    height:32px;
}
</asp:Content>

