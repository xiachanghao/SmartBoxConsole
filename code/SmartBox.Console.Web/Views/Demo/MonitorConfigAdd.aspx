<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	监控注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>监控注册</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr>
                        <th width="30%">主机名：</th>
                        <td class=""><input id="tb_cfg_hostname" name="tb_cfg_hostname" /></td>
                    </tr>
                    <tr>
                        <th width="30%">主机IP地址：</th>
                        <td class=""><input id="tb_cfg_hostip" name="tb_cfg_hostip" /></td>
                    </tr>
                    <tr>
                        <th width="30%">内容：</th>
                        <td class=""><input id="tb_cfg_file" name="tb_cfg_file" /></td>
                    </tr>
                    <tr>
                        <th width="30%">是否启用：</th>
                        <td class=""><select id="selUseState" class="easyui-combobox" data-options="panelHeight:'auto'" name="selUseState" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="1">是</option>
		                <option value="0">否</option>
                        </select></td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">发送日期：</th>
                        <td class=""><input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/></td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">执行日期：</th>
                        <td class=""><input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/></td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">主机名：</th>
                        <td class=""><input /></td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">主机IP：</th>
                        <td class=""><input /></td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">说明：</th>
                        <td class=""><input /></td>
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

var entity = <%=ViewData["entity"]%>;
function closeWin() {
    if (parent)
        parent.CloseWind(false);
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

    function parseEntity() {
        if (entity.cfg_hostname)
            $('#tb_cfg_hostname').val(entity.cfg_hostname);

        if (entity.cfg_hostip)
            $('#tb_cfg_hostip').val(entity.cfg_hostip);
            
        if (entity.cfg_file)
            $('#tb_cfg_file').val(entity.cfg_file);

        $('#selUseState').combobox('setValue', entity.cfg_isuse);
    }

    function html_encode(str)   
{   
  var s = "";   
  if (str.length == 0) return "";   
  s = str.replace(/&/g, "&gt;");   
  s = s.replace(/</g, "&lt;");   
  s = s.replace(/>/g, "&gt;");   
  s = s.replace(/ /g, "&nbsp;");   
  s = s.replace(/\'/g, "&#39;");   
  s = s.replace(/\"/g, "&quot;");   
  s = s.replace(/\n/g, "<br>");   
  return s;   
}   

function html_decode(str)   
{   
  var s = "";   
  if (str.length == 0) return "";   
  s = str.replace(/&gt;/g, "&");   
  s = s.replace(/&lt;/g, "<");   
  s = s.replace(/&gt;/g, ">");   
  s = s.replace(/&nbsp;/g, " ");   
  s = s.replace(/&#39;/g, "\'");   
  s = s.replace(/&quot;/g, "\"");   
  s = s.replace(/<br>/g, "\n");   
  return s;   
} 

    $(document).ready(function () {
        parseEntity();

        $('#btnSave').click(function () {
            var o = {};
            o.cfg_id = entity.cfg_id;
            o.cfg_hostname = $('#tb_cfg_hostname').val();
            o.cfg_hostip = $('#tb_cfg_hostip').val();
            o.cfg_file = $('#tb_cfg_file').val();
            o.cfg_file = html_encode(o.cfg_file);
            var is_use = $('#selUseState').combobox('getValue');
            o.cfg_isuse = is_use;

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("MonitorConfigAddPost") %>',
                data: o,
                dataType: "json",
                success: function (data) {


                      $.messager.alert('提示', data.d, 'info', function() {
                            if (data.r && parent)
                                parent.CloseWind(true);
                            if (!data.r && parent)
                                parent.CloseWind(false);
                        });
                },error: function (e) {
                    $.messager.alert('提示', e.statusText + '  ' + e.responseText, 'info');
                    
                }
            });
            return false;
        });

        $("#files").kendoUpload({
                        multiple: false,
                        async: {
                            saveUrl: "<%=Url.Content("~/PushManage/PushDllUploadFile") %>?pd_id=" + entity.pd_id,
                            removeUrl: "<%=Url.Content("~/PushManage/PushDllDeleteFile") %>?pd_id=" + entity.pd_id,
                            autoUpload: true
                        },
                        files: initialFiles,
                        error: onError,
                        cancel: onCancel,
                        complete: onComplete,
                        progress: onProgress,
                        success: onSuccess,
                        select: onSelect,
                        remove: onRemove,
                        upload: onUpload,
                            localization: {
                                select: "选择文件",
                                uploadSelectedFiles: "上传",
                                statusUploading: "文件上传中...",
                                statusUploaded: "文件上传完成！",
                                statusFailed: "上传失败",
                                retry: "重试",
                                remove: "移除",
                                headerStatusUploading: "上传中",
                                headerStatusUploaded: "上传完成",
                                dropFilesHere: "将文件拖放于此",
                                cancel: "取消"
                            },
                            showFileList: true
                    });
    });

</script>
</asp:Content>
