<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AddModifyPage.Master" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_Unit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	单位管理
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
            <% using (Html.BeginForm("AddModifyUnitSave", "AuthManage", FormMethod.Post, new { id = "frmUnit" }))
       { %>

       <%=Html.Hidden("Upper_Unit_ID", (Model == null ? (!String.IsNullOrEmpty(Request.QueryString["Upper_Unit_ID"]) ? Request.QueryString["Upper_Unit_ID"] : "") : Model.Upper_Unit_ID))%>
       <%=Html.Hidden("Unit_ID", (Model == null ? "" : Model.Unit_ID))%>
    <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                上级单位：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">

                <%= Html.TextBox("upper_unit_name", ViewData["upper_unit_name"], new { @Style = "width:95%;", @Class = "FN_Name", @disabled = "disabled" })%>
 
                   
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                单位名称：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">

                <%= Html.TextBox("Unit_Name", (Model == null ? "" : Model.Unit_Name), new { @Style = "width:95%;", @Class = "Unit_Name" })%>
 
                   
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                排序号：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextBox("Unit_Sequence", (Model == null ? 0 : Model.Unit_Sequence), new { @Style = "width:95%;", @Class = "Unit_Sequence" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                备注：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextArea("Unit_Demo", (Model == null ? "" : Model.Unit_Demo), new { @Style = "width:95%;", @Class = "Unit_Demo" })%>
            </td>
        </tr>
    </table>
    <% } %>
            </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
function save() {                
    $("#frmUnit").submit();
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
            $("#frmUnit").validate({
                rules: {
                    Unit_Name: { required: true }
                },
                messages: {
                    Unit_Name: { required: "请填写单位名称！" }
                },
                submitHandler: function (form) {
                    $("#frmUnit").ajaxSubmit(saveoptions);
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: showerror
            });

            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#frmUnit");
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
