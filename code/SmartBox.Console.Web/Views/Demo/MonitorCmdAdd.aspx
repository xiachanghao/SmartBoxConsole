<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	监控命令
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>监控命令</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr>
                        <th width="30%">标题：</th>
                        <td class=""><input id="tb_cmd_title" /></td>
                    </tr>
                    <tr>
                        <th width="30%">代码：</th>
                        <td class=""><select id="sel_cmd_code" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_cmd_code" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="Get">Get</option>
		                <option value="Post">Post</option>
                        </select></td>
                    </tr>
                    <tr style="display:;">
                        <th width="30%">发送日期：</th>
                        <td class=""><input id="tb_cmd_senddate" name="tb_cmd_senddate" readonly=""  type="text"  class="Wdate" onClick="WdatePicker()"/></td>
                    </tr>
                    <tr style="display:;">
                        <th width="30%">执行日期：</th>
                        <td class=""><input id="tb_cmd_executedate" name="tb_cmd_executedate" readonly=""  type="text"  class="Wdate" onClick="WdatePicker()"/></td>
                    </tr>
                    <tr style="display:none;">
                        <th>执行结果:</th>
                        <td><select id="sel_cmd_executeresult" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_cmd_executeresult" style="width:284px;">
                        <option value="">请选择</option>
		                <option value="1">成功</option>
		                <option value="0">失败</option>
                        </select> </td>
                    </tr>
                    <tr>
                        <th width="30%">主机名：</th>
                        <td class=""><input id="tb_cmd_hostname" /></td>
                    </tr>
                    <tr>
                        <th width="30%">主机IP：</th>
                        <td class=""><input id="tb_cmd_hostip" /></td>
                    </tr>
                    <tr>
                        <th width="30%">说明：</th>
                        <td class=""><input id="tb_cmd_description" /></td>
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
        $('#tb_cmd_title').val(entity.cmd_title);

        if (entity.cmd_senddate)
            $('#tb_cmd_senddate').val(parseDate(entity.cmd_senddate));
        if (entity.cmd_excudedate)
            $('#tb_cmd_executedate').val(parseDate(entity.cmd_excudedate));
        if (entity.cmd_hostname)
            $('#tb_cmd_hostname').val(entity.cmd_hostname);
        if (entity.cmd_hostip)
            $('#tb_cmd_hostip').val(entity.cmd_hostip);
        if (entity.cmd_discription)
            $('#tb_cmd_description').val(entity.cmd_discription);           

            
        $('#sel_cmd_executeresult').combobox('setValue', entity.cmd_executeresult);
        $('#sel_cmd_code').combobox('setValue', entity.cmd_code);
    }

    function parseDate(d) {
                    if (d == null)
                        return '';
                    var date = new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10));
                    var result = date.getFullYear() + "-" + (date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1) + "-" + (date.getDate() < 10 ? "0" + date.getDate() : date.getDate()) + " " + (date.getHours() < 10 ? "0" + date.getHours() : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes());
                    if (result == '1-01-01 08:00')
                        return '';
                    else
                        return result;
                }

    $(document).ready(function () {
        parseEntity();

        $('#btnSave').click(function () {
            var o = {};
            o.cmd_id = entity.cmd_id;
            o.cmd_senddate = $('#tb_cmd_senddate').val();
            o.cmd_executedate = $('#tb_cmd_executedate').val();
            o.cmd_hostname = $('#tb_cmd_hostname').val();
            o.cmd_hostip = $('#tb_cmd_hostip').val();
            o.cmd_title = $('#tb_cmd_title').val();
            o.cmd_description = $('#tb_cmd_description').val();         
            o.cmd_executeresult = $('#sel_cmd_executeresult').combobox('getValue');
            o.cmd_code = $('#sel_cmd_code').combobox('getValue');


            $.ajax({
                type: "POST",
                url: '<%=Url.Action("MonitorCmdAddPost") %>',
                data: o,
                dataType: "json",
                success: function (data) {
                    
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
