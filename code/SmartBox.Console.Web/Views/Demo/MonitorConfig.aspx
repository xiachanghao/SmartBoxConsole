<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	监控注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:165px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>服务监控>>监控注册</span>
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
                        启用日期：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        主机名：
                    </td>
                    <td>
                        <input name="tbHostName" id="tbHostName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        是否启用：
                    </td>
                    <td>
                        <select id="selEnableState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="1">已启用</option>
		                <option value="0">已禁用</option>
                        </select>
                    </td>
                    <td>更新状态：</td>
                    <td><select id="selUpdatestatus" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="1">成功</option>
		                <option value="0">失败</option>
                        </select></td>
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
<%--    <a id="A2" onclick="javascript:return UploadDll();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">启用</a>
    <a id="A3" onclick="javascript:return UploadDll();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">禁用</a>--%>
    <a id="A1" onclick="javascript:return MonitorConfigAdd();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a>
<a id="btn_upload" onclick="javascript:return MonitorConfigDeleteSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">删除</a>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetMonitorConfigList") %>',idField:'cfg_id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb'">
				<thead>
					<tr>
                        <th data-options="field:'cfg_id',checkbox:true"></th>						
                        <%--<th data-options="field:'sw_log_id'" width="80">账号</th>--%>
                        <th data-options="field:'cfg_hostname'" width="40">主机名</th>
                        <th data-options="field:'cfg_hostip'" width="30">主机IP地址</th>
                        <th data-options="field:'cfg_file'" width="30">实体文本</th>
                        <th data-options="field:'cfg_createdate2'" width="30">创建日期</th>
                        <th data-options="field:'cfg_createman'" width="30">创建人</th>
                        <th data-options="field:'cfg_updatedate2'" width="30">更新日期</th>
                        <th data-options="field:'cfg_updateman'" width="30">更新人</th>
                        <th data-options="field:'cfg_updatestatus'" width="30">更新状态</th>
                        <th data-options="field:'cfg_isuse'" width="30">是否启用</th>
                        <th data-options="field:'cfg_usedate2'" width="30">启用日期</th>
						<th data-options="field:'cfgid',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return MonitorConfigAdd('+row.cfg_id+');\'>修改</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return MonitorConfigDelete('+row.cfg_id+')\'>删除</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RemoveJobPluginGroup&quot;)\'>载出</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RestartJobPluginGroup&quot;)\'>重载</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '';
			}" width="80">操作</th>
						<%--<th data-options="field:'unitcost',align:'right'" width="80">Unit Cost</th>
						<th data-options="field:'attr1'" width="150">Attribute</th>
						<th data-options="field:'status',align:'center'" width="60">Status</th>--%>
					</tr>
				</thead>
			</table>
    </div>
</div>



    
    

            <script type="text/javascript">
                var sortStr = 'cfg_id';

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var hostname = $('#tbHostName').val();
                    var updatestatus = $('#selUpdatestatus').combobox('getValue');
                    var enablestatus = $('#selEnableState').combobox('getValue');

                    var startTime = $('#tbTimeStart').val();
                    startTime = $.trim(startTime);
                    if (startTime != '') {
                        startTime = startTime + ' 23:59:59';
                    }

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        hostname: hostname,
                        enalbe_time_start: startTime,
                        enalbe_time_end: endTime,
                        updatestatus: updatestatus,
                        isuse: enablestatus,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });
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

                function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
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

                function MonitorConfigDeleteSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的配置吗?', function (r) {
                        if (r) {
                            var ids = '';


                            for (var i = 0; i < rows.length; ++i) {


                                    ids += rows[i].cfg_id;
                                    if (i < rows.length - 1)
                                        ids += ',';

                            }


                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorConfigSelected") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    var _msg = data.d;
                                    //if (not_del_cnt > 0)
                                    //    _msg = _msg + not_del_msg;
                                    $.messager.alert('提示', _msg, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });

                                    
                                }
                            });
                        }
                    });
                }

                function MonitorConfigDelete(id) {

                    $.messager.confirm('确认', '确定要删除所选择的配置吗?', function (r) {
                        if (r) {

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorConfig") %>',
                                data: { id: id },
                                dataType: "json",
                                success: function (data) {
                                    var _msg = data.d;
                                    $.messager.alert('提示', _msg, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });


                                }
                            });
                        }
                    });
                }

                function MonitorConfigAdd(id) {
                    if (id === undefined)
                        id = 0;
                    var url = '<%=Url.Content("~/") %>demo/MonitorConfigAdd?id=' + id;
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
                    var hostname = $('#tbHostName').val();
                    var updatestatus = $('#selUpdatestatus').combobox('getValue');
                    var enablestatus = $('#selEnableState').combobox('getValue');

                    var startTime = $('#tbTimeStart').val();
                    startTime = $.trim(startTime);
                    if (startTime != '') {
                        startTime = startTime + ' 23:59:59';
                    }

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    pager.pagination({
                        onSelectPage: function (pageNum, pageSize) {
                            page = pageNum;

                            opts.pageNumber = pageNum;
                            opts.pageSize = pageSize;

                            var hostname = $('#tbHostName').val();
                            var updatestatus = $('#selUpdatestatus').combobox('getValue');
                            var enablestatus = $('#selEnableState').combobox('getValue');

                            var startTime = $('#tbTimeStart').val();
                            startTime = $.trim(startTime);
                            if (startTime != '') {
                                startTime = startTime + ' 23:59:59';
                            }

                            var endTime = $('#tbTimeEnd').val();
                            endTime = $.trim(endTime);
                            if (endTime != '') {
                                endTime = endTime + ' 23:59:59';
                            }

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });

                            $('#grid').datagrid('load', {
                                hostname: hostname,
                                enalbe_time_start: startTime,
                                enalbe_time_end: endTime,
                                updatestatus: updatestatus,
                                isuse: enablestatus,
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
                        hostname: hostname,
                        enalbe_time_start: startTime,
                        enalbe_time_end: endTime,
                        updatestatus:updatestatus,
                        isuse:enablestatus,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

                    $('#grid').datagrid({
                        loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                data.rows[i].cfg_isuse = data.rows[i].cfg_isuse == 1 ? '已启用' : '已禁用';
                                data.rows[i].cfg_updatedate2 = parseDate(data.rows[i].cfg_updatedate);
                                data.rows[i].cfg_createdate2 = parseDate(data.rows[i].cfg_createdate);
                                data.rows[i].cfg_usedate2 = parseDate(data.rows[i].cfg_usedate);
                                
                            }
                            return data;
                        }
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
    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
