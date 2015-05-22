<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	布局设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>布局设置</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr>
                        <th width="30%">显示名称：</th>
                        <td class=""><input id="tbDisplayName" /></td>
                    </tr>
                    <tr>
                        <th width="30%">应用名称：</th>
                        <td class=""><select id="selApplication" class="easyui-combobox" data-options="url:'<%=Url.Content("~/Demo/GetApplicationData") %>',
    valueField:'ID',
    textField:'DisplayName',onSelect: function(org){

        }" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">通讯录</option>
		                <option value="DISABLE">日程</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">图片地址：</th>
                        <td class=""><input id="tbImageAddress" /><input type="button" id="btnUploadImage" class="btnskin_b" value="上传图片" /></td>
                    </tr>
                    <tr>
                        <th width="30%">排序号：</th>
                        <td class=""><input id="tbSeq" /></td>
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
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
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
//var entity = {};
var initialFiles = [];
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

    function showUploadImage() {
        var url = '<%=Url.Content("~/") %>ImageManage/SelectImage';
        $('#wif')[0].src = url;
        $('#w').window('open');
        return false;
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

        if (entity) {
            $('#tbDisplayName').val(entity.DispalyName);
            $('#tbImageAddress').val(entity.Image);
            $('#tbSeq').val(entity.Seq);
            if (entity.App4AIID != '0')
                $('#selApplication').combobox('select', entity.App4AIID);
        }
    }

    $(document).ready(function () {
        parseEntity();
        $('#btnUploadImage').click(function() {
            showUploadImage();
        });
        $('#btnSave').click(function () {
            var appId = $('#selApplication').combobox('getValue');
            var validated = true;
            var msg = '';
            var o = {};
            o.StyieID = '<%=Request.QueryString["StyleID"] %>';
            o.DisplayName = $('#tbDisplayName').val();
            o.ImageAddress = $('#tbImageAddress').val();
            o.Seq = $('#tbSeq').val();
            o.App4AIID = appId;
            if (o.Seq == '') {
                o.Seq = 1;
            }
            if (appId == '') {
                validated = false;
                msg += '请选择应用!';
            }
            if (o.DisplayName == '') {
                validated = false;
                msg += '请输入显示名!';
            }
            if (o.ImageAddress == '') {
                validated = false;
                msg += '请输入图片地址!';
            }
            
            if (validated) {
                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("StyleHomeItemPost") %>',
                    data: o,
                    dataType: "json",
                    success: function (data) {
                        $.messager.alert('提示', data.d, 'info', function() {
                        });
                    }
                });
            } else {
                $.messager.alert('提示', msg, 'info', function() {
                });
            }            
            return false;
        });

        $("#files").kendoUpload({
                        multiple: false,
                        async: {
                            saveUrl: "<%=Url.Content("~/PushManage/PushDllUploadFile") %>?pd_id=",
                            removeUrl: "<%=Url.Content("~/PushManage/PushDllDeleteFile") %>?pd_id=",
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
