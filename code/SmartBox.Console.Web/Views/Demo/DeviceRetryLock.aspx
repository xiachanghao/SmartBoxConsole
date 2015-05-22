<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	设备重试锁定
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>设备重试锁定</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr style="display:none;">
                        <th width="30%">app上架是否需要审核：</th>
                        <td class=""><select id="sel_app_sj_need_auth" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:151px;">
		                <option value="1">需要审核</option>
		                <option value="0">不需要审核</option>
                        </select></td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">新增用户默认状态：</th>
                        <td class=""><select id="sel_user_default_status" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:151px;">
		                <option value="1">启用</option>
		                <option value="0">禁用</option>
                        </select></td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">默认名单模式：</th>
                        <td class=""><select id="sel_namelist_black_or_white" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:151px;">
 		                <option value="black">黑名单</option>
		                <option value="white">白名单</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">设备锁定后是否自动解锁：</th>
                        <td class=""><select id="sel_user_unlock_auto_enabled" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:151px;">
		                <option value="1">是，自动解锁</option>
		                <option value="0">否，不自动解锁</option>
                        </select></td>
                    </tr>
                    <tr>
                        <th width="30%">设备锁定后自动解锁时间间隔：</th>
                        <td class=""><input class="easyui-numberbox" data-options="min:0,precision:0" id="tb_user_unlock_auto_hours" />小时</td>
                    </tr>
                    <tr>
                        <th width="30%">设备锁定计算时间间隔：</th>
                        <td class=""><input class="easyui-numberbox" data-options="min:0,precision:0" id="tb_lock_user_farto_lasttime_hours" />小时内多次输入错误密码，将导致设备被锁定</td>
                    </tr>
                    <tr>
                        <th width="30%">设备锁定计算方式：</th>
                        <td class=""><select id="sel_lock_count_mode" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:151px;">
		                <option value="ctns">连续</option>
		                <option value="acml">累计</option>
                        </select>多次输入错误密码，将导致帐户被锁定</td>
                    </tr>
                    <tr style="display:none;">
                        <th width="30%">用户登陆时被允许输入错误次数：</th>
                        <td class=""><input class="easyui-numberbox" data-options="min:0,precision:0,max:10" id="tb_allowed_wrong_times_when_login"/>次输入错误密码，将导致设备被锁定</td>
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
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <input type="button" id="btnSave" class="btnskin_b" value="保存" />
                            <input type="button" class="btnskin_b" style="display:none;" value="取消" onclick="javascript:closeWin();" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    
    </div>
    <style>
    input{margin-left:0px;border:1px solid #95B8E7;margin-right:0px;}
    </style>
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

    var globalparm = <%=ViewData["globalparm"]%>;
function closeWin() {
    if (parent)
        parent.CloseWind(false);
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

    //var initialFiles = <%=ViewData["f"]%>;
    

    function parseEntity() {
        for (var i = 0; i < globalparm.length; ++i) {
            var o = globalparm[i];
            switch (o.ConfigKey) {
                case "app_sj_need_auth":
                    //$('#sel_app_sj_need_auth').combobox('select', o.ConfigValue);
                    break;
                case "user_default_status":
                    //$('#sel_user_default_status').combobox('select', o.ConfigValue);
                    break;
                case "namelist_black_or_white":
                    //$('#sel_namelist_black_or_white').combobox('select', o.ConfigValue);
                    break;
                case "device_unlock_auto_enabled":
                    $('#sel_user_unlock_auto_enabled').combobox('select', o.ConfigValue);
                    break;
                case "lock_device_count_mode":
                    $('#sel_lock_count_mode').combobox('select', o.ConfigValue);
                    break;
                case "device_unlock_auto_hours":
                    $('#tb_user_unlock_auto_hours').val(o.ConfigValue);
                    break;
                case "lock_device_farto_lasttime_hours":
                    $('#tb_lock_user_farto_lasttime_hours').val(o.ConfigValue);
                    break;
                case "allowed_wrong_times_when_login":
                    //$('#tb_allowed_wrong_times_when_login').val(o.ConfigValue);
                    break;
            }
        }
    }

    $(document).ready(function () {
        parseEntity();

        $('#btnSave').click(function () {
            var o = {};
            //o.app_sj_need_auth = $('#sel_app_sj_need_auth').combobox('getValue');
            //o.user_default_status = $('#sel_user_default_status').combobox('getValue');
            //o.namelist_black_or_white = $('#sel_namelist_black_or_white').combobox('getValue');
            o.user_unlock_auto_enabled = $('#sel_user_unlock_auto_enabled').combobox('getValue');
            o.user_unlock_auto_hours = $('#tb_user_unlock_auto_hours').val();
            o.lock_user_farto_lasttime_hours = $('#tb_lock_user_farto_lasttime_hours').val();
            o.lock_count_mode = $('#sel_lock_count_mode').combobox('getValue');
            //o.allowed_wrong_times_when_login = $('#tb_allowed_wrong_times_when_login').val();

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("DeviceRetryLockPost") %>',
                data: o,
                dataType: "json",
                success: function (data) {
                      $.messager.alert('提示', data.d, 'info', function() {
                            if (data.r)
                                location.reload();
                        });
                }
            });
            return false;
        });

        
    });

</script>
</asp:Content>
