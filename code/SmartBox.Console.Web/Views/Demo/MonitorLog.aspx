﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	监控日志
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:165px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>服务监控>>监控日志</span>
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
                        记录时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        状态：
                    </td>
                    <td>
                        <select id="sel_log_status" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_log_status" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="1">成功</option>
                        <option value="0">失败</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        级别：
                    </td>
                    <td>
                        <select id="sel_log_df_lever" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_log_df_lever" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="1">蓝色</option>
                        <option value="2">黄色</option>
		                <option value="3">红色</option>
                        </select>
                    </td>
                    <td>监控对象：</td>
                    <td><select id="sel_log_df_item" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="hostmem">hostmem</option>
                        <option value="hostcpu">hostcpu</option>
                        </select></td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <input type="submit" id="btnSearch" class="btnskin_b" value="查询" />
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
<a id="btn_upload" onclick="javascript:return MonitorLogDeleteSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetMonitorLogList") %>',idField:'log_id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb'">
				<thead>
					<tr>
                        <th data-options="field:'log_id',checkbox:true"></th>						
                        <%--<th data-options="field:'sw_log_id'" width="80">账号</th>--%>
                        <th data-options="field:'log_df_item'" width="40">监控对象</th>
                        <th data-options="field:'log_monitorvalue'" width="30">监控数据</th>
                        <th data-options="field:'log_datetime'" width="30">记录时间</th>
                        <th data-options="field:'log_df_kind'" width="30">类别</th>
                        <th data-options="field:'log_df_code'" width="30">代码</th>
                        <th data-options="field:'log_df_lever'" width="30">级别</th>
                        <th data-options="field:'log_status'" width="30">状态</th>
                        <th data-options="field:'log_hostip'" width="30">主机IP地址</th>
                        <th data-options="field:'log_hostname'" width="30">主机名称</th>
						<th data-options="field:'logid',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return MonitorLogDelete('+value+')\'>删除</a>' + 
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
                var sortStr = 'log_id';
                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var timeStart = $('#tbTimeStart').val();
                    var timeEnd = $('#tbTimeEnd').val();
                    var log_status = $('#sel_log_status').combobox('getValue');
                    var log_df_lever = $('#sel_log_df_lever').combobox('getValue');
                    var log_df_item = $('#sel_log_df_item').combobox('getValue');

                    $('#grid').datagrid('load', {
                        timeStart: timeStart,
                        timeEnd: timeEnd,
                        log_status: log_status,
                        log_df_lever: log_df_lever,
                        log_df_item: log_df_item,
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
                var page = 0;
                $(document).ready(function () {
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    //var cmd_title = $('#tb_cmd_title').val();
                    var timeStart = $('#tbTimeStart').val();
                    var timeEnd = $('#tbTimeEnd').val();
                    var log_status = $('#sel_log_status').combobox('getValue');
                    var log_df_lever = $('#sel_log_df_lever').combobox('getValue');
                    var log_df_item = $('#sel_log_df_item').combobox('getValue');

                    pager.pagination({
                        onSelectPage: function (pageNum, pageSize) {
                            page = pageNum;
                            opts.pageNumber = pageNum;
                            opts.pageSize = pageSize;

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });

                            //var cmd_title = $('#tb_cmd_title').val();
                            var timeStart = $('#tbTimeStart').val();
                            var timeEnd = $('#tbTimeEnd').val();
                            var log_status = $('#sel_log_status').combobox('getValue');
                            var log_df_lever = $('#sel_log_df_lever').combobox('getValue');
                            var log_df_item = $('#sel_log_df_item').combobox('getValue');

                            $('#grid').datagrid('load', {
                                timeStart: timeStart,
                                timeEnd: timeEnd,
                                log_status: log_status,
                                log_df_lever: log_df_lever,
                                log_df_item: log_df_item,
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
                        timeStart: timeStart,
                        timeEnd: timeEnd,
                        log_status: log_status,
                        log_df_lever: log_df_lever,
                        log_df_item: log_df_item,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

                    $('#grid').datagrid({
                        loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                data.rows[i].log_status = data.rows[i].log_status == '1' ? '成功' : '失败';
                                data.rows[i].logid = data.rows[i].log_id;
                                data.rows[i].log_datetime = parseDate(data.rows[i].log_datetime);
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

                function MonitorLogDeleteSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的Log吗?', function (r) {
                        if (r) {
                            var ids = '';


                            for (var i = 0; i < rows.length; ++i) {


                                ids += rows[i].cmd_id;
                                if (i < rows.length - 1)
                                    ids += ',';

                            }


                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorLogSelected") %>',
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

                function MonitorLogDelete(id) {

                    $.messager.confirm('确认', '确定要删除所选择的Log吗?', function (r) {
                        if (r) {

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorLog") %>',
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
    </script>
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
