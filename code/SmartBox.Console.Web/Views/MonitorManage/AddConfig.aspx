<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AddModifyPage.Master" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.Monitor_Config>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StyleContent" runat="server">
	
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
	$("document").ready(function () {

            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            $("a#Save").click(save);
            $("#CloseImgBtn1").click(function () {
                CloseModelWindow(null, true);
            });
            function save() {
                var category = new Array();
                $("input[name='AppCategory']").each(function () {
                    if (this.checked) {
                        category.push($(this).val());
                    }
                });
                $("#CategoryIDs").val(category.join(','));
                $("#frmApplicationInfo").submit();
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
                            parent.$.closeIfrm(null, true);
                        });
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                    }
                }
            };

            //保存之验证
            $("#frmApplicationInfo").validate({
                rules: {
                    Name: { required: true },
                    DisplayName: { required: true },
                    Enable: { required: true }
                },
                messages: {
                    Name: { required: "请填写应用标识！" },
                    DisplayName: { required: "请填写显示名称！" },
                    Enable: { required: "请应用启用状态！" }
                },
                submitHandler: function (form) {
                    $("#frmApplicationInfo").ajaxSubmit(saveoptions);
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: showerror
            });

            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#frmApplicationInfo");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }

        });
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
    <% using (Html.BeginForm("EditConfigSave", "MonitorManage", FormMethod.Post, new { id = "frmApplicationInfo" }))
       { %>
       <%=Html.Hidden("cfg_id", Model == null ? 0 : Model.cfg_id) %>
    <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                主机名：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <% if (Model != null)
                   { %>
                <%= Html.TextBox("cfg_hostname", Model.cfg_hostname, new { @Style = "width:95%;", @Class = "Name" })%>
                <%}
                   else
                   { %>
                <%= Html.TextBox("cfg_hostname", "", new { @Style = "width:95%;", @Class = "Name" })%>
                <% } %>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                主机IP地址：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextBox("cfg_hostip", Model == null ? "" : (Model.cfg_hostip ?? ""), new { @Style = "width:95%;", @Class = "DisplayName" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                文件名称：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextBox("cfg_file", Model == null ? "" : (Model.cfg_file ?? ""), new { @Style = "width:95%;", @Class = "DisplayName" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                发布时间：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Model == null ? "" : Model.cfg_createdate.ToString("yyyy-MM-dd HH:mm")%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                发布人：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.Label(Model == null ? "" : (Model.cfg_createman ?? ""))%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                发布时间：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%=Model == null ? "" : Model.cfg_updatedate.ToString("yyyy-MM-dd HH:mm")%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                启用状态：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.RadioButton("Enable", true, Model == null ? false : Model.cfg_isuse == "1", new { @id = "rdoEnable" })%><label
                    for="rdoEnable">启用</label>
                <%= Html.RadioButton("Enable", false, Model == null ? false : Model.cfg_isuse != "1", new { @id = "rdoDesable" })%><label
                    for="rdoDesable">禁用</label>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                启用时间：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextBox("cfg_usedate", Model == null ? "" : Model.cfg_usedate.ToString("yyyy-MM-dd HH:mm"), new { @Style = "width:95%;", @Class = "DisplayName" })%>
            </td>
        </tr>        
    </table>

    <% } %>

</asp:Content>
