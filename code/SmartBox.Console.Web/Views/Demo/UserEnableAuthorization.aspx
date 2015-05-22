<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	用户启用审核
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:132px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>用户审核>>启用审核</span>
        </div>
        <div style="height:3px;display:;"></div>

    <div class="table_box" style="display:;">
    <h4>用户审核设置</h4>
    <div class="table_toolbar">
        
    </div>
    <div class="table_box_data">
        <table border="0" cellspacing="0" cellpadding="0">
            <tbody>
                 <tr>
                    <td>
                        用户审核设置：
                    </td>
                    <td>
                        <select id="selEnableUserAuth" class="easyui-combobox" data-options="panelHeight:'auto',onSelect: selectItem" name="state" style="width:184px;">
		                <option value="1">启用审核</option>
                        <option value="0">禁用审核</option>
                        </select>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <input id="btnSave" type="submit" class="btnskin_b" value="保存" />
                            <input type="reset" class="btnskin_b" style="display:none;" onclick="resetSearch();" value="重置" />
                        </td>
                    </tr>
                </tfoot>
            </tbody>
        </table>
    </div>
    </div>
    <div style="height:3px;display:;"></div>
    </div>
    <div data-options="region:'center'" style="width:100%;">
      <iframe scrolling="auto" id='Iframe1' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
    </div>
</div>



    
    

            <script type="text/javascript">
             function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
                }
                function selectItem(val) {
                    var url = "";
                    if (val.value == "1") {
                        url = '<%=Url.Content("~/Demo/UserEnableAuthException") %>';
                    } else if (val.value == "0") {
                        url = '<%=Url.Content("~/Demo/UserDisableAuthException") %>';
                    }
                    $('#Iframe1')[0].src = url;
                }

                function save() {
                    var enableUserAuth = $('#selEnableUserAuth').combobox('getValue');
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("SaveUserAuthSetting") %>',
                        data: { enableUserAuth: enableUserAuth },
                        dataType: "json",
                        success: function (data) {
                            $.messager.alert('提示', data.d, 'info', function () {
                            });

                            var dg = $('#grid');
                            $('#grid').datagrid('reload');
                        }
                    });
                }

                $(document).ready(function () {
                    $('#btnSave').bind('click', save);
                    $('#selEnableUserAuth').combobox('select', '<%=ViewData["user_need_auth"] %>');
                    var o = {};
                    o.value = <%=ViewData["user_need_auth"] %>
                    selectItem(o);
                });

                function UploadDll() {
                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd';
                    $('#wif')[0].src = url;
                    $('#w').window('open');
                    return false;
                }

                function CloseWind(refreshGrid) {
                    if (refreshGrid) {
                        var dg = $('#grid');
                        $('#grid').datagrid('reload');
                    }
                    $('#w').window('close');
                }

                function DeleteDll() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的插件吗?', function (r) {
                        if (r) {
                            var ids = '';
                            var not_del_msg = '插件';
                            var not_del_cnt = 0;
                            for (var i = 0; i < rows.length; ++i) {

                                if (rows[i].pd_dll_status != '已载出') {
                                    not_del_msg += rows[i].pd_name + '、';
                                    ++not_del_cnt;
                                } else {
                                    ids += rows[i].pd_id;
                                    if (i < rows.length - 1)
                                        ids += ',';
                                }
                            }
                            not_del_msg += '未从推送服务里载出，不能删除！';

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("PushDLLDelete") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    var _msg = data.Msg;
                                    if (not_del_cnt > 0)
                                        _msg = _msg + not_del_msg;
                                    $.messager.show({
                                        title: '提示',
                                        msg: _msg,
                                        timeout: 2000,
                                        showType: 'slide'
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });



                }
    </script>
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:850px;height:520px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
