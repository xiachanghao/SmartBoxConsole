<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	监控联系人
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>监控联系人</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr>
                        <th width="30%">接收人账号：</th>
                        <td class=""><input id="tb_lm_id" type="hidden" /><input id="tb_lm_uid" /></td>
                    </tr>
                    <tr>
                        <th width="30%">名称：</th>
                        <td class=""><span id="tb_lm_uname" /></td>
                    </tr>
                    <tr>
                        <th width="30%">单位：</th>
                        <td class=""><select id="sel_lm_udept" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="sel_lm_udept" style="width:184px;">
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">手机号码：</th>
                        <td class=""><input id="tb_lm_mobile" /></td>
                    </tr>
                    <tr>
                        <th width="30%">电子邮件：</th>
                        <td class=""><input id="tb_lm_email" /></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <input type="button" id="btnSave" class="btnskin_b" value="保存" />
                            <input type="button" class="btnskin_b" value="取消" onclick="javascript:closeWin();" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<link href="../../telerik/styles/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="../../telerik/styles/kendo.silver.min.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../telerik/js/jquery.min.js" type="text/javascript"></script>--%>
    <script src="../../telerik/js/kendo.all.min.js" type="text/javascript"></script>

    <style>
	html,body {font-size:12px;}
	.k-upload {background-color:transparent;border-width:0px;}
	k-state-focused {box-shadow:0px 0px 6px 0px #CDCDCD;}
	h4.file-name-heading {margin-top:20px;}
	.k-upload-button {margin-left:0px!important;padding-left:0px!important;}
	.k-dropzone {padding-left:0px;}
	.k-file {padding-lft:0px;}
	<!--[if IE]>
	.k-upload-status-total {padding-top:10px;}
	.k-upload-status {padding-top:5px;} 
	<![endif]-->
	
	</style>

<script type="text/javascript">

var entity = <%=ViewData["entity"]%>;
function closeWin() {
    if (parent)
        parent.CloseWind(false);
}
   
    function parseEntity() {
        if (entity.lm_id)
            $('#tb_lm_id').val(entity.lm_id);

        if (entity.lm_uid)
            $('#tb_lm_uid').val(entity.lm_uid);
            
        if (entity.lm_uname)
            $('#tb_lm_uname').html(entity.lm_uname);
        if (entity.lm_mobile)
            $('#tb_lm_mobile').val(entity.lm_mobile);
        if (entity.lm_email)
            $('#tb_lm_email').val(entity.lm_email);

        $('#sel_lm_udept').combobox('setValue', entity.lm_udept);
    }

    $(document).ready(function () {
        parseEntity();

        $('#btnSave').click(function () {
            var o = {};
            o.lm_id = entity.lm_id;
            o.lm_uid = $('#tb_lm_uid').val();
            o.lm_uname = entity.lm_uname;
            o.lm_mobile = $('#tb_lm_mobile').val();
            o.lm_email = $('#tb_lm_email').val();
            o.lm_udept = $('#sel_lm_udept').combobox('getValue');

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("MonitorLinkmanAddPost") %>',
                data: o,
                dataType: "json",
                success: function (data) {
//                    $.messager.show({
//                        title: '提示',
//                        msg: msg,
//                        timeout: 1500,
//                        showType: 'slide'
//                    });
                    if (!data.r) {
                        $.ajax({
                            type: "POST",
                            url: '<%=Url.Action("CleanTrashPushDll") %>',
                            data: o,
                            dataType: "json",
                            success: function (data) {
                      
                            }
                        });
                    }
                      $.messager.alert('提示', data.d, 'info', function() {
                            if (data.r && parent)
                                parent.CloseWind(true);
                            if (!data.r && parent)
                                parent.CloseWind(false);
                        });
                }
            });
            return false;
        });

        
    });

</script>
</asp:Content>
