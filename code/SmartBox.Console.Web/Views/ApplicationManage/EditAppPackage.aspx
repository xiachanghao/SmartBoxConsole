<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_PackageExt>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	编辑app
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="table_box">
        <h4>编辑app</h4>
        <div class="table_box_data table_box_data_model_details">

        <div class="easyui-accordion" style="">
		<div title="安装包信息" data-options="iconCls:''" style="overflow:auto;padding:10px;">
			<table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr>
                        <th width="30%">安装包显示名称：</th>
                        <td class=""><input value="<%=Model.pe_DisplayName%>" /></td>
                    </tr>
                    <tr>
                        <th width="30%">安装包名称：</th>
                        <td class=""><%=ViewData["packageName"]%></td>
                    </tr>
                    <tr>
                        <th width="30%">安装包类型：</th>
                        <td class=""><%=ViewData["packageType"]%></td>
                    </tr>
                    <tr>
                        <th width="30%">分类：</th>
                        <td class=""><select id="selCategory" class="easyui-combobox" data-options="panelHeight:'auto',url:'/Demo/GetApplicationCategory',
    valueField:'id',
    textField:'displayname',onSelect: function(org){

        }" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                
                        <option value="0">待审核</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">所属单位：</th>
                        <td class=""><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/") %>Demo/GetUnitData',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">

                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">客户端类型：</th>
                        <td class=""><select id="selClientType" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="Phone/Android">Phone/Android</option>
		                <option value="Pad/Android">Pad/Android</option>
                        <option value="Phone/ios">Phone/ios</option>
		                <option value="Pad/ios">Pad/ios</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">发布版本号：</th>
                        <td class=""><%=Model.pe_Version %></td>
                    </tr>
                    <tr>
                        <th width="30%">内部版本号：</th>
                        <td class=""><%=Model.pe_BuildVer %></td>
                    </tr>
                    <tr>
                        <th width="30%">是否推荐：</th>
                        <td class=""><select id="selTJ" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="True">推荐</option>
		                <option value="False">不推荐</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">是否必备：</th>
                        <td class=""><select id="selBB" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="True">必备</option>
		                <option value="False">不必备</option>
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
                        <th>执行结果:</th>
                        <td><select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:284px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">成功</option>
		                <option value="DISABLE">失败</option>
                        </select> </td>
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
            </table>
		</div>
		<div title="应用信息" data-options="iconCls:''" style="overflow:auto;padding:10px;">
			
		</div>
	</div>
           
           
           <table  cellspacing="0" cellpadding="0">
                <tbody></tbody><tfoot>
                    <tr>
                        <td colspan="2">
                            <input type="button" id="Button1" class="btnskin_b" value="保存" />
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
<%string sentity = Model.pe_ExtentInfo ;
if (String.IsNullOrEmpty(sentity))
    sentity = "{}";
%>
var entity = <%=sentity %>;
//alert(entity.Applications.length);

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


    



    $(document).ready(function () {
        $('#selTJ').combobox('setValue', '<%=Model.pe_IsTJ %>');        $('#selBB').combobox('setValue', '<%=Model.pe_IsBB %>');        $('#selClientType').combobox('setValue', '<%=Model.pe_ClientType %>');
        $('#selUnit').combobox('setValue', '<%=Model.pe_UnitCode %>');
        $('#selCategory').combobox('setValue', '<%=Model.pe_CategoryID %>');
        
        $('#btnSave').click(function () {
            var o = {};
            o.pd_id = entity.pd_id;
            o.pd_name = $('#pd_name').val();
            o.pd_dll_filename = $('#pd_dll_filename').val();
            o.pd_xml_filename = $('#pd_xml_filename').val();

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("PushDllAddPost") %>',
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
