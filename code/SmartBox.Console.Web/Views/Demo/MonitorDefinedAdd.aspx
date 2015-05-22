<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	监控定义
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>监控定义</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr>
                        <th width="30%">类别：</th>
                        <td class=""><select id="sel_df_kind" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">黄色</option>
		                <option value="DISABLE">蓝色</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">对象：</th>
                        <td class=""><input id="tb_df_item" name="tb_df_item"/></td>
                    </tr>
                    <tr>
                        <th width="30%">内容：</th>
                        <td class=""><input id="tb_df_content" name="tb_df_content" /></td>
                    </tr>
                    <tr>
                        <th width="30%">最大值：</th>
                        <td class=""><input id="tb_df_maxvalue" name="tb_df_maxvalue" /></td>
                    </tr>
                    <tr>
                        <th width="30%">最小值：</th>
                        <td class=""><input id="tb_df_minvalue" name="tb_df_minvalue" /></td>
                    </tr>
                    <tr>
                        <th width="30%">代码：</th>
                        <td class=""><input id="tb_df_code" name="tb_df_code" /></td>
                    </tr>
                    <tr>
                        <th width="30%">警报级别：</th>
                        <td class=""><select id="sel_df_lever" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_df_lever" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">黄色</option>
		                <option value="DISABLE">蓝色</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">发送方式 ：</th>
                        <td class=""><select id="sel_df_sendway" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_df_sendway" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">sms</option>
		                <option value="DISABLE">mail</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">接收人 ：</th>
                        <td class=""><select id="sel_df_receptman" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_df_receptman" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">周小军</option>
		                <option value="DISABLE">杨函</option>
                        </select></td>
                    </tr>
                    <tr style="display:;">
                        <th width="30%">发送开始日期：</th>
                        <td class=""><input id="tb_df_startsenddate" name="tb_df_startsenddate" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/></td>
                    </tr>
                    <tr style="display:;">
                        <th width="30%">发送结束日期：</th>
                        <td class=""><input id="tb_df_endsenddate" name="tb_df_endsenddate" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/></td>
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

    //var initialFiles = <%=ViewData["f"]%>;
    

    function parseEntity() {
        $('#pd_id').val(entity.pd_id);

        if (entity.pd_name)
            $('#pd_name').val(entity.pd_name);
        if (entity.pd_dll_filename)
            $('#pd_dll_filename').val(entity.pd_dll_filename);
        if (entity.pd_xml_filename)
            $('#pd_xml_filename').val(entity.pd_xml_filename);
    }

    $(document).ready(function () {
        parseEntity();

        $('#btnSave').click(function () {
            var o = {};
            o.df_id = entity.df_id;
            o.df_lever = $('#sel_df_lever').combobox('getValue');
            o.df_kind = $('#sel_df_kind').combobox('getValue');
            o.df_item = $('#tb_df_item').val();
            //o.df_content = $('#tb_df_content').val();
            o.df_maxvalue = $('#tb_df_maxvalue').val();
            o.df_minvalue = $('#tb_df_minvalue').val();
            o.df_code = $('#tb_df_code').val();
            o.df_startsenddate = $('#tb_df_startsenddate').val();
            o.df_endsenddate = $('#tb_df_endsenddate').val();
            o.df_sendway = $('#sel_df_sendway').combobox('getValue');
            o.df_receptman = $('#sel_df_receptman').combobox('getValue');


            $.ajax({
                type: "POST",
                url: '<%=Url.Action("MonitorDefinedAddPost") %>',
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
