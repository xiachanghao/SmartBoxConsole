<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/AddModifyPage.Master" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_Functions>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	权限管理
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
            <% using (Html.BeginForm("AddModifyFunctionSave", "AuthManage", FormMethod.Post, new { id = "frmFunction" }))
       { %>

       <%=Html.Hidden("Upper_FN_ID", (String.IsNullOrEmpty(Request.QueryString["Upper_FN_ID"]) ? "0" : Request.QueryString["Upper_FN_ID"]))%>
       <%=Html.Hidden("FN_ID", (String.IsNullOrEmpty(Request.QueryString["FN_ID"]) ? "0" : Request.QueryString["FN_ID"]))%>
       <%=Html.Hidden("Unit_ID", (String.IsNullOrEmpty(Request.QueryString["Unit_ID"]) ? "0" : Request.QueryString["Unit_ID"]))%>
    <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                上级权限：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">

                <%--<%= Html.TextBox("Upper_FN_Name", ViewData["Upper_FN_Name"], new { @Style = "width:95%;", @Class = "FN_Name" })%>--%>
                <%= Html.DropDownList("Upper_FN_Name", ViewData["Upper_FN_Name"] as SelectList, new { @Style = "width:95%;", @Class = "FN_Name" })%>
<%--               <div id="menuContent" class="menuContent" style="display:none; position: absolute;">
	                <ul id="treeDemo" class="ztree" style="margin-top:0; width:160px;"></ul>
               </div>--%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                权限名称：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">

                <%= Html.TextBox("FN_Name", (Model == null ? "" : Model.FN_Name), new { @Style = "width:95%;", @Class = "FN_Name" })%>
 
                   
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                权限Code：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextBox("FN_Code", (Model == null ? "" : Model.FN_Code), new { @Style = "width:95%;", @Class = "FN_Code" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                权限类型：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">              
                <%= Html.DropDownList("fn_type", ViewData["fn_type"] as SelectList, new {@onchange="ChangeRule(value)", @Style = "width:30%;", @Class = "Unit_Name" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                是否锁定：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">              
                <%= Html.DropDownList("fn_disabled", ViewData["fn_disabled"] as SelectList, new { @onchange = "", @Style = "width:30%;", @Class = "Unit_Name" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                可见类型：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">              
                <%= Html.DropDownList("fn_visibletype", ViewData["fn_visibletype"] as SelectList, new { @onchange = "", @Style = "width:30%;", @Class = "Unit_Name" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                权限Url：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextBox("FN_Url", (Model == null ? "" : Model.FN_Url), new { @Style = "width:90%;", @Class = "FN_Url" })%>
            </td>
        </tr>        
          <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                模块小图标：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <img src="../../Images/FuncImages/<%=Request.QueryString["FN_ID"] %>_SIcon.png" onerror="src='../../Images/upno.png'"/>
                <input type="file" id="SFuncIcon" name="SFuncIcon"  />
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                模块图标：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <img src="../../Images/FuncImages/<%=Request.QueryString["FN_ID"] %>_Icon.png" onerror="src='../../Images/upno.png'"/>
                <input type="file" id="FuncIcon" name="FuncIcon"  />
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                排序号：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextBox("FN_Sequence", (Model == null ? 0 : Model.FN_Sequence), new { @Style = "width:95%;", @Class = "FN_Sequence" })%>
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                备注：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">
                <%= Html.TextArea("FN_Demo", (Model == null ? "" : Model.FN_Demo), new { @Style = "width:95%;", @Class = "FN_Demo" })%>
            </td>
        </tr>
    </table>
    <% } %>
            </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
function save() {   $("#loadingpannel").html("正在保存数据...").show();      
    $("#frmFunction").submit();
}

function close_window() {
    CloseModelWindow(null, true);
}

function ChangeRule(value){
    if(value=='menu')
    {
        $("#FN_Url").rules("add", {   
    required: true,   
    messages: {   
    required: "请填写权限Url！"  
 }   
});   

    }
    else
    {
        $("#FN_Url").rules("remove");
    }
}

$("document").ready(function () {
   
    $(".bbit-form tr:even").addClass("even");
    $(".bbit-form tr:odd").addClass("odd");
    $("a#Save").click(save);
    $("a#CloseImgBtn1").click(close_window);

    var functionType = '<%=Model == null ? "menu" : Model.FN_Type%>';
    if (functionType == 'function')
        $('select#fn_type option[value="function"]').attr('selected', true);

var visibleType = '<%=Model == null ? "0" : Model.FN_VisibleType%>';

        $('select#fn_visibletype option[value="'+visibleType+'"]').attr('selected', true);
var fnDisabled = '<%=Model == null ? "0" : (Model.FN_Disabled ? "1" : "0")%>';
$('select#fn_disabled option[value="'+fnDisabled+'"]').attr('selected', true);
    var saveoptions =
            {
                beforeSumbit: function () {
                    
                    return true;
                },
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess) {
                        
                        hiAlert(data.Msg, '提示', function () {
                            $("#loadingpannel").hide(); 
                            parent.$.closeIfrm(null, true);  
                                                    
                        });                                             
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () {$("#loadingpannel").hide(); });
                    }
                }
            };

        //保存之验证
            $("#frmFunction").validate({
                rules: {
                    FN_Name: { required: true },
                    FN_Url: { required: true }
                },
                messages: {
                    FN_Name: { required: "请填写权限名称！" },
                    FN_Url: { required: "请填写权限Url！" }
                },
                submitHandler: function (form) {
                    $("#frmFunction").ajaxSubmit(saveoptions);
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: showerror
            });

            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#frmFunction");
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
