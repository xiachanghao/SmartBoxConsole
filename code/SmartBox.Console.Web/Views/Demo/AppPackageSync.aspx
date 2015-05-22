<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	发布同步
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:195px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>发布管理>>发布同步</span>
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
                        app名称：
                    </td>
                    <td>
                        <input id="tbAppName" type="text" />
                    </td>
                    <td>
                        应用名称：
                    </td>
                    <td>
                        <select id="selApp" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/") %>Demo/GetApplicationData',
    valueField:'Name',
    textField:'DisplayName',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        同步状态：
                    </td>
                    <td>
                        <select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="0">待同步</option>
		                <option value="1">同步成功</option>
                        <option value="2">同步失败</option>
                        </select>
                    </td>
                    <td>单位：</td>
                    <td><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">></td>
                </tr>
                <tr>
                    <td>
                        审核时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        
                    </td>
                    <td>

                    </td>
                </tr><tr style="display:none;">
                    <td>
                        提交时间：
                    </td>
                    <td>
                        <input id="Text1" name="Text1" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="Text2" name="Text2" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        最后同步时间：
                    </td>
                    <td>
                        <input id="Text3" name="Text3" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="Text4" name="Text4" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                </tr><tr style="display:none;">
                    <td>
                        下架时间：
                    </td>
                    <td>
                        
                    </td>
                    <td colspan="2">
                        
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
<a id="btn_upload" onclick="javascript:return SyncPackageExtSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">同步</a>

</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetAppPackageSyncList") %>',idField:'pe_id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                data.rows[i].pe_id2 = data.rows[i].pe_id;
                                //data.rows[i].unitname = '区环保局';
                                //data.rows[i].appname = '通讯录、日程、会议';
                                //data.rows[i].pd_status_text = data.rows[i].pd_status ? '已启用' : '已禁用';
                                data.rows[i].tj = data.rows[i].pe_IsTJ ? '是' : '否';
                                data.rows[i].bb = data.rows[i].pe_IsBB ? '是' : '否';
                                if (data.rows[i].pe_SyncStatus != null) {
                                    switch (data.rows[i].pe_SyncStatus) {
                                        case 0:
                                            data.rows[i].pe_SyncStatus2 = '待同步'; 
                                            break;
                                        case 1:
                                            data.rows[i].pe_SyncStatus2 = '同步成功';
                                            break;
                                        case 2:
                                            data.rows[i].pe_SyncStatus2 = '同步失败';
                                            break;
                                    }
                                }
                                else {
                                    data.rows[i].pe_SyncStatus2 = '待同步'; 
                                }
                                //data.rows[i].pdid = data.rows[i].pd_id;
                                if (data.rows[i].pe_AuthTime == null)
                                    data.rows[i].pe_AuthTime2 = '';
                                else
                                    data.rows[i].pe_AuthTime2 = parseDate(data.rows[i].pe_AuthTime);
                                //data.rows[i].submit_time3 = parseDate(data.rows[i].submit_time);
                                //data.rows[i].submit_time4 = parseDate(data.rows[i].submit_time);
                            }
                            return data;
                        }">
				<thead>
					<tr>
                        <th data-options="field:'pe_id',checkbox:true"></th>						
                        <th data-options="field:'pe_DisplayName'" width="50">app名称</th>
                        <th data-options="field:'pe_ClientType'" width="30">客户端</th>
                        <th data-options="field:'pe_UnitName'" width="50">单位</th> 
                        <th data-options="field:'pe_ApplicationName'" width="50">应用清单</th>                        
                        <th data-options="field:'pe_AuthTime2'" width="60">审核时间</th>                  
                        <th data-options="field:'pe_Category'" width="30">类别</th>
                        <th data-options="field:'tj'" width="30">推荐</th>
                        <th data-options="field:'bb'" width="30">必备</th>
                        <th data-options="field:'pe_SyncStatus2',styler: function(value,row,index){
				if (value == 2){
					return 'background-color:#ffee00;color:red;';
				} else if (value == 1){
					return 'background-color:green;color:#FFFFFF;';
				}
			}" width="30">同步状态</th>
            <th data-options="field:'pe_Direction'" width="30">同步方向</th>
                        <!-- th data-options="field:'status_sj',styler: function(value,row,index){
				if (value == '待上架'){
					return 'background-color:#ffee00;color:red;';
				} else if (value == '已上架'){
					return 'background-color:green;color:#FFFFFF;';
				}
			}" width="30">上架状态</th-->
						<th data-options="field:'pe_id2',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return SyncPackageExt('+value+');\'>同步</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return void('+value+',&quot;AddJobPluginGroup&quot;)\'>下架</a>' + 
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
                function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
                }

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var unitCode = $('#selUnit').combobox('getValue');
                    var statusCode = $('#selState').combobox('getValue');
                    var appName = $('#selApp').combobox('getText');
                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        appName: $('#tbAppName').val(),
                        application: appName,
                        unitcode: unitCode,
                        auth_time_start: $('#tbTimeStart').val(),
                        auth_time_end: endTime,
                        syncstatus: statusCode,
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
                }

                function SyncPackageExt(id) {
                    $.messager.confirm('确认', '确定要同步吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("SyncPackageExt") %>',
                                data: { id: id },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                },
                                error: function (data) {
                                    $('#w2').window('close');
                                }
                            });
                        }
                    });
                    return false;
                }

                function SyncPackageExtSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要同步所勾选的app吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].pe_id;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("SyncPackageExts") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
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
                var sortStr = 'pe.pe_AuthTime desc';
                $(document).ready(function () {
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');
                    var unitCode = $('#selUnit').combobox('getValue');
                    var statusCode = $('#selState').combobox('getValue');
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

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });

                            var appName = $('#selApp').combobox('getText');

                            $('#grid').datagrid('load', {
                                appName: $('#tbAppName').val(),
                                application: appName,
                                unitcode: unitCode,
                                auth_time_start: $('#tbTimeStart').val(),
                                auth_time_end: endTime,
                                syncstatus: statusCode,
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
                    var appName = $('#selApp').combobox('getText');
                    $('#grid').datagrid('load', {
                        appName: $('#tbAppName').val(),
                        application: appName,
                        unitcode: unitCode,
                        auth_time_start: $('#tbTimeStart').val(),
                        auth_time_end: endTime,
                        syncstatus: statusCode,
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
    <div id="w2" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){ return false}" style="width:150px;height:70px;padding:3px;">
		<img src="../../Images/loading.gif" />处理中,请等待...
	</div>
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
