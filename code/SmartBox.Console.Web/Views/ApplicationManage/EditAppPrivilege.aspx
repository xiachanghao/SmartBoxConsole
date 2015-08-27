<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    应用授权
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>应用授权</h4>
        <div class="table_box_data table_box_data_model_details">
            <table cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th width="30%">应用名称：</th>
                        <td class=""><span id="tbAppName" /></td>
                    </tr>
                    <tr style="display: none;">
                        <th width="30%">BUA应用系统：</th>
                        <td class="">
                            <select id="selBUAApp" class="easyui-combobox" data-options='panelHeight:"120",valueField: "AppCode",textField: "AppName",data: <%=ViewData["apps"]%>' style="width: 184px;">
                                <option value="">请选择</option>
                            </select></td>
                    </tr>
                    <tr>
                        <th width="30%">BUA应用系统权限：</th>
                        <td class="">
                            <select id="selBUAPrivilege" class="easyui-combobox" data-options='panelHeight:"120",valueField: "privilegeid",textField: "privilegename",data: <%=ViewData["privilegs"]%>' style="width: 184px;">
                            </select>只支持类型为Privilege的权限,其他类型的权限不支持!</td>
                    </tr>
                    <tr style="display: ;">
                        <th>启用同步:</th>
                        <td>
                            <select id="selAutoSync" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width: 284px;">
                                <option value="ENABLE">启用</option>
                                <option value="DISABLE">禁用</option>
                            </select>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <th width="30%">同步时间间隔：</th>
                        <td class="">
                            <input id="SyncInterval" />小时</td>
                    </tr>
                    <tr style="display: none;">
                        <th width="30%">发送日期：</th>
                        <td class="">
                            <input id="tbTimeStart" name="tbTimeStart" readonly="readonly" type="text" class="Wdate" onclick="WdatePicker()" />-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly" type="text" class="Wdate" onclick="WdatePicker()" /></td>
                    </tr>
                    <tr style="display: none;">
                        <th width="30%">执行日期：</th>
                        <td class="">
                            <input id="tbTimeStart" name="tbTimeStart" readonly="readonly" type="text" class="Wdate" onclick="WdatePicker()" />-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly" type="text" class="Wdate" onclick="WdatePicker()" /></td>
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


    <script type="text/javascript">

    var entity = {};
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

    

    function parseEntity() {return;
        $('#pd_id').val(entity.pd_id);

        if (entity.pd_name)
            $('#pd_name').val(entity.pd_name);
        if (entity.pd_dll_filename)
            $('#pd_dll_filename').val(entity.pd_dll_filename);
        if (entity.pd_xml_filename)
            $('#pd_xml_filename').val(entity.pd_xml_filename);
    }

    $(document).ready(function () {

        var appid = '<%=Server.UrlEncode(Request.QueryString["appid"] )%>';
        var buaappid = '<%=Server.UrlEncode(Request.QueryString["buaappid"]) %>';
        var privilege = '<%=Server.UrlEncode(Request.QueryString["privilege"]) %>';
        var appName = '<%=Server.UrlEncode(Request.QueryString["appName"]) %>';
        var appCode = '<%=Server.UrlEncode(Request.QueryString["appCode"]) %>';
        var BuaPrivilegeCode = '<%=Server.UrlEncode(Request.QueryString["BuaPrivilegeCode"]) %>';
        var SyncIntervalTime = '<%=Server.UrlEncode(Request.QueryString["SyncIntervalTime"]) %>';
        if (SyncIntervalTime == 'null')
            SyncIntervalTime = '';
        if (buaappid == 'null')
            buaappid = '';
        if (BuaPrivilegeCode == 'null')
            BuaPrivilegeCode = '';
        parseEntity();
        $('#tbAppName').html(appName);
        $('#selBUAApp').combobox('setValue', buaappid);
        $('#selBUAPrivilege').combobox('setValue', BuaPrivilegeCode);
        $('#SyncInterval').val(SyncIntervalTime);
        $('#btnSave').click(function () {
            var o = {};
            o.appid = appid;
            o.appCode = appCode;
            o.appName = appName;
            o.buaappid = '';// $('#selBUAApp').combobox('getValue');
            o.buaprivilege = $('#selBUAPrivilege').combobox('getValue');
            o.buaprivilegetext = $('#selBUAPrivilege').combobox('getText');

            if ((o.buaprivilege === undefined || o.buaprivilege == '') && (o.buaprivilegetext != '' && o.buaprivilegetext != '所有人权限')) {
                o.buaprivilege = o.buaprivilegetext;
            }

            o.enablesync = $('#selAutoSync').combobox('getValue');
            o.syncinterval = $('#SyncInterval').val();

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("AppPrivilegeAddPost") %>',
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
