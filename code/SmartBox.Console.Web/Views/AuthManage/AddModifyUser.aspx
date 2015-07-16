<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AddModifyPage.Master"
    Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_UserList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户管理
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
            <% using (Html.BeginForm("AddModifyUserSave", "AuthManage", FormMethod.Post, new { id = "frmUser" }))
               {
                   Html.AntiForgeryToken();
                   Html.ValidationSummary(true);%>
            <%=Html.Hidden("user_id", ViewData["user_id"])%>
            <%=Html.Hidden("unit_id", ViewData["unit_id"])%>
            <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        用户账号：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                    <% if (Model != null)
                       { %>                   
                       <%= Html.TextBox("UL_UID", Model.UL_UID, new { @Style = "width:95%;", @Class = "Unit_Name", @disabled = "disabled" })%>    
                       <% }
                       else
                       { %>   
                       <%= Html.TextBox("UL_UID",  " ", new { @Style = "width:95%;", @Class = "Unit_Name" })%>             
                       <% } %>        
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        用户名称：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                    <% if (Model != null)
                       { %>  
                        <%= Html.TextBox("UL_Name", Model.UL_Name, new { @Style = "width:95%;", @Class = "Unit_Name" })%>
                        <% }
                       else
                       { %>   
                       <%= Html.TextBox("UL_Name",  "", new { @Style = "width:95%;", @Class = "Unit_Name" })%>             
                       <% } %>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft tdright">
                        用户密码：
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                    <% if (Model != null)
                       { %>
                        <%= Html.TextBox("UL_PWD", Model.UL_PWD, new { @Style = "width:95%;", @Class = "Unit_Name", @Type = "password", @disabled = "disabled" })%>
                         <% }
                       else
                       { %>   
                        <%= Html.TextBox("UL_PWD", "" , new { @Style = "width:95%;", @Class = "Unit_Name", @Type = "password" })%>
                       <% } %>
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
                        <%--<%= Html.TextBox("Unit_Name", ViewData["Unit_Name"], new { @Style = "width:95%;", @Class = "Unit_Name" })%>--%>
                        <%= Html.DropDownList("Unit_Name", ViewData["Unit_Name"] as SelectList, new { @Style = "width:95%;", @Class = "Unit_Name" })%>
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

            jQuery.validator.addMethod("isPhone", function(value,element) {   
               var length = value.length;   
               var mobile = /^(((1[3,5,8]{1}))+\d{9})$/;   
               var tel = /^\d{3,4}-\d{7,9}$/;   
               return this.optional(element) || (tel.test(value) || mobile.test(value));   
    
             }, "请正确填写您的联系电话");   


$("document").ready(function () {
    $(".bbit-form tr:even").addClass("even");
    $(".bbit-form tr:odd").addClass("odd");
    $("a#Save").click(save);
    $("a#CloseImgBtn1").click(close_window);
    $('select#Unit_Name').prepend('<option value=0>全局</option>');
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
                    UL_UID: { required: true },
                    UL_PWD: { required: true },
                    UL_MailAddress:{ required: true,email:true },
                    UL_MobilePhone:{isPhone:true}
                },
                messages: {
                    UL_UID: { required: "请填写用户账号！" },
                    UL_PWD: { required: "请填写用户密码！" },
                    UL_MailAddress: { required: "请填写正确的邮件地址! " },
                    UL_MobilePhone:{isPhone:"11位手机号或xxx-xxxxxxx!"}
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
