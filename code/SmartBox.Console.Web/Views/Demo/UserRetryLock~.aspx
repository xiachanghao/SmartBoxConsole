<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	用户重试锁定
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:195px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>重试锁定>>用户重试锁定</span>
        </div>
        <div style="height:3px;display:;"></div>

    <div class="table_box" style="display:;">
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
                        <input id="tbUserName" name="tbUserName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        锁定状态：
                    </td>
                    <td>
                        <select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <%--<option value="1">审核通过</option>
		                <option value="2">审核拒绝</option>--%>
                        <option value="1">已锁定</option>
                        </select>
                    </td>
                    <td>单位：</td>
                    <td><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'auto',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">
                        </select></td>
                </tr>
                <tr>
                    <td>
                        锁定时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>

                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <input id="btnSearch" type="submit" class="btnskin_b" value="查询" />
                            <input type="reset" class="btnskin_b" value="重置" />
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
<a id="btn_upload" onclick="javascript:return UnLockSelectedUser();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">解除锁定</a>
<%--<a onclick="javascript:return NotPassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">拒绝通过</a>--%>

</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetUserRetryLock") %>',idField:'id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                data.rows[i].u_lock_time2 = parseDate(data.rows[i].u_lock_time);
                                data.rows[i].u_lock_expire_time2 = parseDate(data.rows[i].u_lock_expire_time);
                                switch(data.rows[i].u_lock_status) {
                                    case 0:
                                        data.rows[i].status_text = '未锁定';
                                        break;
                                    case 1:
                                        data.rows[i].status_text = '锁定';
                                        break;
                                }
                            }
                            return data;
                        }">
				<thead>
					<tr>
                        <th data-options="field:'id',checkbox:true"></th>						
                        <th data-options="field:'useruid'" width="40">账号</th>
                        <th data-options="field:'username'" width="40">姓名</th>
                        <th data-options="field:'unitname'" width="40">单位</th>
                        <th data-options="field:'u_lock_time2'" width="50">锁定时间</th>
                        <th data-options="field:'u_lock_expire_time2'" width="50">解除锁定时间</th>
                        <th data-options="field:'status_text'" width="30">状态</th>
						<th data-options="field:'id2',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return UnlockUser(&quot;'+row.useruid+'&quot;,&quot;'+row.username+'&quot;);\'>解除锁定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return NotPass(&quot;'+row.useruid+'&quot;,&quot;'+row.username+'&quot;);\'>拒绝通过</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RemoveJobPluginGroup&quot;)\'>载出</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RestartJobPluginGroup&quot;)\'>重载</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '';
			}" width="80">操作</th>
					</tr>
				</thead>
			</table>
    </div>
</div>



    
    

            <script type="text/javascript">
                var sortStr = 'u_lock_time desc';

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var statusCode = $('#selState').combobox('getValue');
                    var enableStatusText = $('#selState').combobox('getText');
                    if (statusCode == '')
                        statusCode = -1;
                    else
                        statusCode = parseInt(statusCode);

                    var unitCode = $('#selUnit').combobox('getValue');
                    var unitText = $('#selUnit').combobox('getText');

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        uid: $('#tbUID').val(),
                        username: $('#tbUserName').val(),
                        u_unitcode: unitCode,
                        u_lock_time_start: $('#tbTimeStart').val(),
                        u_lock_time_end: endTime,
                        lockStatus: statusCode,
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
                }

                function UnLockSelectedUser() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要解除锁定所勾选的用户吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].useruid;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '/Demo/UnLockSelectedUser',
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

                function NotPassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要不通过所勾选的用户吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].useruid;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '/Demo/NotPassSelectedUser',
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

                function UnlockUser(uid, uname) {
                    $.messager.confirm('确认', '确定要解除锁定用户' + uname + '吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '/Demo/UnLockUser',
                                data: { uid: uid, uname: uname },
                                dataType: "json",
                                success: function (data) {
                                    var s = data.d;
                                    if (data.r) {
                                        s += ',已解除锁定!';
                                    }
                                    $.messager.alert('提示', s, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
                    return false;
                }

                function NotPass(uid, uname) {
                    $.messager.confirm('确认', '确定不通过用户' + uname + '吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '/Demo/NotPassUser',
                                data: { uid: uid, uname: uname },
                                dataType: "json",
                                success: function (data) {
                                    var s = data.d;
                                    if (data.r) {
                                        s += ',未通过审核';
                                    }
                                    $.messager.alert('提示', s, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
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
                function ModifyEntity(pd_id) {
                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd?pd_id=' + pd_id;
                    $('#wif')[0].src = url;
                    $('#w').window('open');
                    return false;
                }

                function OperateDll(pd_id, op) {
                    var url = '<%=Url.Content("~/") %>PushManage/JobCommandCtrl?pd_id=' + pd_id + '&operate=' + op;
                    $('#wif')[0].src = url;
                    $('#w').window('open');

                    return false;
                }
                var page = 0;
                $(document).ready(function () {
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    var unitCode = $('#selUnit').combobox('getValue');

                    var statusCode = $('#selState').combobox('getValue');
                    if (statusCode == '')
                        statusCode = -1;
                    else
                        statusCode = parseInt(statusCode);

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
                                username: $('#tbUserName').val(),
                                u_unitcode: unitCode,
                                u_lock_time_start: $('#tbTimeStart').val(),
                                u_lock_time_end: endTime,
                                lockStatus: statusCode,
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
                        username: $('#tbUserName').val(),
                        u_unitcode: unitCode,
                        u_lock_time_start: $('#tbTimeStart').val(),
                        u_lock_time_end: endTime,
                        lockStatus: statusCode,
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
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
