<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	设备在线纪录
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:29px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>用户管理>>设备在线纪录</span>
        </div>
        <div style="height:3px;display:;"></div>

    <div class="table_box" style="display:none;">
    <h4>查询条件</h4>
    <div class="table_toolbar">
        
    </div>
    <div class="table_box_data">
        <table border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td>
                        用户账号：
                    </td>
                    <td>
                        <input id="tbUID" name="tbUID" type="text" />
                    </td>
                    <td>
                        姓名：
                    </td>
                    <td>
                        <input id="tbUName" name="tbUName" type="text" />
                    </td>
                </tr>
                <tr>
                    <%--<td>
                        状态：
                    </td>
                    <td>
                        
                    </td>--%>
                    <td>单位：</td>
                    <td><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">

                        </select></td>
                        <td></td>
                        <td style="display:none;"><select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="-1">请选择</option>
		                <option value="1">在线</option>
		                <option value="0">离线</option>
                        </select></td>
                </tr>
                <tr style="display:none;">
                    <td>
                        启用时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        禁用时间：
                    </td>
                    <td>
                        <input id="tbTimeStartDisable" name="tbTimeStartDisable" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEndDisable" name="tbTimeEndDisable" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <input id="btnSearch" type="submit" class="btnskin_b" value="查询" />
                            <input type="reset" class="btnskin_b" onclick="resetSearch();" value="重置" />
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
            
    <div id="tb" style="text-align:right;">
    &nbsp;
<%--<a id="btn_upload" onclick="javascript:return PassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">启用</a>
<a onclick="javascript:return NotPassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">禁用</a>
<a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">锁定</a>
<a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">默认审核通过</a>
<a onclick="javascript:return KickoutSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">强制退出</a>
<a onclick="javascript:return SyncUser();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">同步BUA用户</a>
<a onclick="javascript:return KickoutSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">退出登录</a>--%>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetUserDeviceOnline") %>',idField:'U_ID',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',
                    loadFilter: function (data) {  
                        for (var i = 0; i < data.rows.length; ++i) {
                            data.rows[i].LastLoginTime2 = parseDate(data.rows[i].LastLoginTime);
                            data.rows[i].LastLogoutTime2 = parseDate(data.rows[i].LastLogoutTime);
                            data.rows[i].U_UID2 = data.rows[i].U_UID;
                            data.rows[i].lastusetime2 = parseDate(data.rows[i].lastusetime);

                            data.rows[i].Status2 = data.rows[i].Status == 1 ? '在线' : '离线';
                            switch(data.rows[i].Status) {
                                case '2':
                                    data.rows[i].Status2 = '离线';
                                    break;
                                case '1':
                                    data.rows[i].Status2 = '在线';
                                    break;
                            }
                         }   
                        return data;  
                     },onSortColumn:function(sort, order) {
                        sort = sort.replace('2', '');
                        sortStr = sort + ' ' + order;
                        Search();
                     }">
				<thead>
					<tr>
                        <th data-options="field:'ID',checkbox:true"></th>
                        <th data-options="field:'UID'" width="50">账号</th>
                        <th data-options="field:'DeviceID'" width="80">设备ID</th>
                        <th data-options="field:'ClientType'" width="80">设备类型</th>
                        <th data-options="field:'Status2',sortable:true" width="20">状态</th>
                        <th data-options="field:'LastLoginTime2',sortable:true" width="50">最后使用时间</th>
                        <th data-options="field:'LastLoginTime2'" width="40">最后登陆时间</th>
                        <th data-options="field:'LastLogoutTime2'" width="40">最后退出时间</th>
						
						<%--<th data-options="field:'unitcost',align:'right'" width="80">Unit Cost</th>
						<th data-options="field:'attr1'" width="150">Attribute</th>
						<th data-options="field:'status',align:'center'" width="60">Status</th>--%>
					</tr>
				</thead>
			</table>
    </div>
</div>



    
    

            <script type="text/javascript">
                var sortStr = 'LastLoginTime desc';

//                function get_user_info(tp, uid) {
//                    $.ajax({
//                        type: "POST",
//                        url: '<%=Url.Action("NotPassSelectedUser") %>',
//                        data: { ids: ids },
//                        dataType: "json",
//                        success: function (data) {
//                            $.messager.alert('提示', data.d, 'info', function () {
//                            });

//                            var dg = $('#grid');
//                            $('#grid').datagrid('reload');
//                        }
//                    });
//                }

                function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
                }

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var enableStatusCode = $('#selState').combobox('getValue');
                    var enableStatusText = $('#selState').combobox('getText');
                    if (enableStatusCode == '')
                        enableStatusCode = -1;
                    else
                        enableStatusCode = parseInt(enableStatusCode);

                    var unitCode = $('#selUnit').combobox('getValue');
                    var unitText = $('#selUnit').combobox('getText');

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    var endTimeDisable = $('#tbTimeEndDisable').val();
                    endTimeDisable = $.trim(endTimeDisable);
                    if (endTimeDisable != '') {
                        endTimeDisable = endTimeDisable + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        uid: $('#tbUID').val(),
                        uname: $('#tbUName').val(),
                        u_unitcode: unitCode,
                        u_enable_time_start: $('#tbTimeStart').val(),
                        u_enable_time_end: endTime,
                        u_disable_time_start: $('#tbTimeStartDisable').val(),
                        u_disable_time_end: endTimeDisable,
                        lockStatus: -1,
                        enabledStatus: enableStatusCode,
                        authStatus: -1,
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
                }

                function NotPassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要禁用所勾选的用户吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].U_UID;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("NotPassSelectedUser") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
                }

                function PassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要启用所勾选的用户吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].U_UID;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("PassSelectedUser") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
                }

                function Pass(uid, uname) {
                    $.messager.confirm('确认', '确定要启用用户' + uname + '吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("PassUser") %>',
                                data: { uid: uid, uname: uname },
                                dataType: "json",
                                success: function (data) {
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
                    return false;
                }

                function Kickout(uid, uname) {
                    $.messager.confirm('确认', '确定要强制用户' + uname + '退出吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Content("~/Demo/Kickout") %>',
                                data: { uid: uid, uname: uname },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }, error: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.statusText, 'info', function () {
                                    });
                                }
                            });
                        }
                    });
                    return false;
                }

                function KickoutSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要强制退出所勾选的用户吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].U_UID;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Content("~/Demo/KickoutSelected") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }, error: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.statusText, 'info', function () {
                                    });
                                }
                            });
                        }
                    });
                }

                function NotPass(uid, uname) {
                    $.messager.confirm('确认', '确定要禁用用户' + uname + '吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("NotPassUser") %>',
                                data: { uid: uid, uname: uname },
                                dataType: "json",
                                success: function (data) {
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
                    return false;
                }

                function ModifyEntity(pd_id) {
                    //                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd?pd_id=' + pd_id;
                    //                    $('#wif')[0].src = url;
                    //                    $('#w').window('open');
                    return false;
                }

                function SyncUser() {
                    var url = '<%=Url.Content("~/") %>authmanage/syncbuausertoinside';
                    $('#wif')[0].src = url;
                    $('#w').window('open');
                    return false;
                }

                function OperateDll(pd_id, op) {
                    //                    var url = '<%=Url.Content("~/") %>PushManage/JobCommandCtrl?pd_id=' + pd_id + '&operate=' + op;
                    //                    $('#wif')[0].src = url;
                    //                    $('#w').window('open');

                    return false;
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

                var page = 0;
                $(document).ready(function () {
                    $('#tbUID').val('<%=Request.QueryString["uid"] %>');
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    var enableStatusCode = $('#selState').combobox('getValue');
                    var enableStatusText = $('#selState').combobox('getText');
                    if (enableStatusCode == '')
                        enableStatusCode = -1;
                    else
                        enableStatusCode = parseInt(enableStatusCode);

                    var unitCode = $('#selUnit').combobox('getValue');
                    var unitText = $('#selUnit').combobox('getText');

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    var endTimeDisable = $('#tbTimeEndDisable').val();
                    endTimeDisable = $.trim(endTimeDisable);
                    if (endTimeDisable != '') {
                        endTimeDisable = endTimeDisable + ' 23:59:59';
                    }

                    pager.pagination({
                        onSelectPage: function (pageNum, pageSize) {
                            page = pageNum;

                            opts.pageNumber = pageNum;
                            opts.pageSize = pageSize;

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });

                            $('#grid').datagrid('load', {
                                uid: $('#tbUID').val(),
                                uname: $('#tbUName').val(),
                                u_unitcode: unitCode,
                                u_enable_time_start: $('#tbTimeStart').val(),
                                u_enable_time_end: endTime,
                                u_disable_time_start: $('#tbTimeStartDisable').val(),
                                u_disable_time_end: endTimeDisable,
                                lockStatus: -1,
                                enabledStatus: enableStatusCode,
                                authStatus: -1,
                                orderby: sortStr,
                                pageIndex: page - 1,
                                pageSize: opts.pageSize
                            });

                            pager.pagination('refresh', {
                                pageNumber: page,
                                pageSize: pageSize
                            });
                        },
                        pageList: [10, 30, 50, 100], //可以设置每页记录条数的列表           
                        beforePageText: '第', //页数文本框前显示的汉字           
                        afterPageText: '页    共 {pages} 页',
                        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
                    });

                    $('#grid').datagrid('load', {
                        uid: $('#tbUID').val(),
                        uname: $('#tbUName').val(),
                        u_unitcode: unitCode,
                        u_auth_submit_time_start: $('#tbTimeStart').val(),
                        u_auth_submit_time_end: endTime,
                        u_disable_time_start: $('#tbTimeStartDisable').val(),
                        u_disable_time_end: endTimeDisable,
                        lockStatus: -1,
                        enabledStatus: enableStatusCode,
                        authStatus: -1,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

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
                    //                    var rows = $('#grid').datagrid('getChecked');
                    //                    if (rows.length < 1)
                    //                        return;
                    //                    $.messager.confirm('确认', '确定要删除所勾选的插件吗?', function (r) {
                    //                        if (r) {
                    //                            var ids = '';
                    //                            var not_del_msg = '插件';
                    //                            var not_del_cnt = 0;
                    //                            for (var i = 0; i < rows.length; ++i) {
                    //                                
                    //                                if (rows[i].pd_dll_status != '已载出') {
                    //                                    not_del_msg += rows[i].pd_name + '、';
                    //                                    ++not_del_cnt;
                    //                                } else {
                    //                                    ids += rows[i].pd_id;
                    //                                    if (i < rows.length - 1)
                    //                                        ids += ',';
                    //                                }
                    //                            }
                    //                            not_del_msg += '未从推送服务里载出，不能删除！';
                    //                            
                    //                            $.ajax({
                    //                                type: "POST",
                    //                                url: '<%=Url.Action("PushDLLDelete") %>',
                    //                                data: { ids: ids },
                    //                                dataType: "json",
                    //                                success: function (data) {
                    //                                    var _msg = data.Msg;
                    //                                    if (not_del_cnt > 0)
                    //                                        _msg = _msg + not_del_msg;
                    //                                    $.messager.show({
                    //                                        title: '提示',
                    //                                        msg: _msg,
                    //                                        timeout: 2000,
                    //                                        showType: 'slide'
                    //                                    });

                    //                                    var dg = $('#grid');
                    //                                    $('#grid').datagrid('reload');
                    //                                }
                    //                            });
                    //                        }
                    //                    });                    
                }
    </script>
    <div id="w2" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){ return false}" style="width:150px;height:70px;padding:3px;">
		<img src="../../Images/loading.gif" />处理中,请等待...
	</div>
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:790px;height:420px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
