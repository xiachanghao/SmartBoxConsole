<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	单位注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>单位注册</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th width="30%">单位：</th>
                        <td class=""><input id="tbUnit" style="width:236px;" /></td>
                    </tr>
                    <tr>
                        <th width="30%">单位代码：</th>
                        <td class=""><input id="tbUnitCode" style="width:236px;" /><input type="button" id="btnCalcUnitCode" class="btnskin_b" value="计算单位代码" /></td>
                    </tr>
                    <tr id="trUser" style="display:none;">
                        <th>单位代码指定:</th>
                        <td><select id="selUnitCode" name="selUnitCode" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:240px;">
		                <option value="0">自动生成</option>
		                <option value="1">输入</option>
                        </select> </td>
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
    
    </div><div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
    <script>
        var unitmode = 'old';
        var usermode = 'old';
        function new_unit() {
            //$('span.combo').hide();
            //$('#trUser').hide();
            //$('#trNewUser').show(); 
            //('#trNewUserAccount').show();
            //$('#tbUnit').show();
            unitmode = 'new';
            usermode = 'new';
        }

        function new_user() {
            $('#trUser').hide();
            //$('#trNewUser').show();
            //$('#trNewUserAccount').show();
            usermode = 'new';
        }

        function old_user() {
            //$('span.combo').show();
            $('#trUser').show();
            //$('#trNewUser').hide();
            //$('#trNewUserAccount').hide();
            usermode = 'old';
        }

        function addNewUnit() {
            //$.messager.alert('提示', '你好', 'info');
            var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd?pd_id=';
            $('#wif')[0].src = url;
            $('#w').window('open');
            return false;
        }
    </script>
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
    function onError(e) {
        // Array with information about the uploaded files
        var files = e.files;
        
        if (e.operation == "upload") {
            var filename = e.files[0].name;
            //parseInputValue(filename);
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

    function CalcUnitCode() {
        var unit = $('#tbUnit').val();
        //var unitCode = $('#tbUnitCode').val();
        $.ajax({
                type: "POST",
                url: '<%=Url.Action("PreCalcUnitCode") %>',
                data: { unitName: unit },
                dataType: "json",
                success: function (data) {
                    if (data.r) {
                        unitCode = data.unitcode;
                        //$.messager.alert('提示', data.d, 'info');
                        $('#tbUnitCode').val(unitCode);
                    } else {
                        $.messager.alert('提示', data.d, 'info');
                    }
                }
            });
    }

    $(document).ready(function () {
    $('#btnCalcUnitCode').bind('click', CalcUnitCode);
        $('#btnSave').click(function () {
            var o = {};

            //$('#selUnitCode')
            var unit = $('#tbUnit').val();
            if (unit == '') {
                $.messager.alert('提示', '请输入单位！', 'info');
                return false;
            }
            var unitCode = $('#tbUnitCode').val();
            if (unitCode == '') {
                $.messager.alert('提示', '请输入单位代码！', 'info');
                return false;
            }
            var unitCodeMode = $('#selUnitCode').combobox('getValue');
            var unitCodeModeText = $('#selUnitCode').combobox('getText');
            var needReturn = false;
            //alert(unitCodeModeText);
            //alert(unitCodeMode);
            //alert(unitCodeMode == '0');
            //if (unitCodeMode == '0') {

            //} else {
            //     if (unitCode == '') {
            //         $.messager.alert('提示', '在输入模式下单位代码必须指定！', 'info', function () {
            //             needReturn = true;
            //         });
            //     }
            // }



            o.unitcode = unitCode;
            o.unit = unit;

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("AddUnit") %>',
                data: o,
                dataType: "json",
                success: function (data) {
                    $.messager.alert('提示', data.d, 'info', function () {
                        if (data.r && parent) {
                            parent.reloadUnitList();
                        }
                        parent.CloseWind();
                    });
                }
            });
            return false;
        });


    });
    
</script>
</asp:Content>
