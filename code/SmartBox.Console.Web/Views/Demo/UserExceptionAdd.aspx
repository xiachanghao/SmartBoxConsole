<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	例外新增
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>例外新增</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th width="30%">单位：</th>
                        <td class=""><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'auto',url:'<%=Url.Action("GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){
            var url = '<%=Url.Content("~/Loginless/GetUnitUserData2") %>?unitCode='+org.Unit_ID;
            $('#selUnitUser').combotree('reload', url);
        }" name="selUnit" style="width:284px;">
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">用户：</th>
                        <td class=""><input id="selUnitUser" class="easyui-combotree" data-options="panelHeight:'auto',valueField:'id',
    textField:'text',lines:true,onSelect:function(entity) {
        if (entity.id.indexOf('u_') != -1) {
            var uid = entity.id.replace('u_', '').replace('o_', '');
            $('#tbUserUID').val(uid);
        }
    }" style="width:284px;"></td>
                    </tr>
                    <tr style="display:none;">
                        <th>用户帐号:</th>
                        <td><input id="tbUID" style="width:284px;" /><input type="button" id="btnSearchDevice" onclick="javascript:searchUserDevice();" class="btnskin_b" value="按帐户查询用户设备" /></td>
                    </tr>
                    <tr style="display:none;">
                        <th>设备:</th>
                        <td><select id="selUserDevices" class="easyui-combobox" data-options="panelHeight:'auto',valueField:'id',
    textField:'model',onSelect:function(entity) {
        $('#tbDeviceId').val(entity.id);
    }" name="state" style="width:284px;">
                        <option value="">请选择</option>
                        </select> </td>
                    </tr>
                    <tr>
                        <th>用户帐号:</th>
                        <td><input id="tbUserUID" style="width:284px;" /></td>
                    </tr>
                    <tr style="display:none;">
                        <th>附件：</th>
                        <td style="padding-right:0px;"><input type="file" name="files" id="files" />
                        <script id="fileTemplate" type="text/x-kendo-template">
                <span class='k-progress'></span>
                <div class='file-wrapper'>
                    <span class='file-icon #=addExtensionClass(files[0].extension)#'></span>
                    <h4 class='file-heading file-name-heading'>文件名: #=name#</h4>
                    <h4 class='file-heading file-size-heading'>大小: #=size# bytes</h4>
                    <button type='button' class='k-upload-action'></button>
                </div>
            </script> </td>
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

//var entity = <%=ViewData["entity"]%>;
function closeWin() {
    if (parent)
        parent.CloseWind(false);
}

function searchUserDevice() {
    var uid = $.trim($('#tbUID').val());
    var url = '<%=Url.Content("~/Demo/GetUserDevices") %>?uid='+uid;
    $('#selUserDevices').combobox('reload', url);
}
    function onError(e) {
        // Array with information about the uploaded files
        var files = e.files;
        
        if (e.operation == "upload") {
            var filename = e.files[0].name;
            parseInputValue(filename);
            //alert("Failed to upload " + files.length + " files");
            var msg = files[0].name + '上传失败，可能是因为该插件已存在！';
            $.messager.alert('提示', msg, 'info');
            $('#btnSave').hide();
            //alert(msg);
        }
    }

    function onCancel(e) {
        // Array with information about the uploaded files
        var files = e.files;

        // Process the Cancel event
    }

    function onComplete(e) {
        // The upload is now idle
        
    }

    function onProgress(e) {
        // Array with information about the uploaded files
        var files = e.files;

        //console.log(e.percentComplete);
    }

    function onSuccess(e) {
        if (e.operation == 'upload') {
            var filename = e.files[0].name;
            parseInputValue(filename);
        }
    }

    function parseInputValue(filename) {
        if (filename.indexOf('.') != -1) {
           // filename = filename.split('.')[0];
        }
        $.ajax({
                type: "POST",
                url: '<%=Url.Action("GetPushDLLEntity") %>',
                data: {pd_id:entity.pd_id},
                dataType: "json",
                success: function (data) {
                    entity = data;
                    parseEntity();
                }
        });
        
        //$('#pd_dll_filename').val(filename);
        //$('#pd_xml_filename').val(filename);
        //$('#pd_name').val(filename);
    }

    function onSelect(e) {
        $('#btnSave').show();
    }

    function onUpload(e) {
    }

    function onRemove(e) {
        // Array with information about the removed files
        var files = e.files;

        // Process the Remove event
        // Optionally cancel the remove operation by calling
        // e.preventDefault()
    }

    //var initialFiles = <%=ViewData["f"]%>;
    

    function parseEntity() {
        /*$('#pd_id').val(entity.pd_id);

        if (entity.pd_name)
            $('#pd_name').val(entity.pd_name);
        if (entity.pd_dll_filename)
            $('#pd_dll_filename').val(entity.pd_dll_filename);
        if (entity.pd_xml_filename)
            $('#pd_xml_filename').val(entity.pd_xml_filename);*/
    }

    $(document).ready(function () {
        parseEntity();

        $('#btnSave').click(function () {
            var o = {};
            o.useruid = $('#tbUserUID').val();
            var t = '<%=Request.QueryString["t"] %>';
            o.t = t;

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("UserExceptionAddPost") %>',
                data: o,
                dataType: "json",
                success: function (data) {

                    $.messager.alert('提示', data.d, 'info', function () {
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
