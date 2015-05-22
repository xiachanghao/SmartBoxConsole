<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	设备审核
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:195px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>设备管理>>设备审核</span>
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
                        设备类型：
                    </td>
                    <td>
                        <input id="tbModel" name="tbModel" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        状态：
                    </td>
                    <td>
                        <select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="1">审核通过</option>
		                <option value="2">审核拒绝</option>
                        <option value="0" selected>待审核</option>
                        </select>
                    </td>
                    <td>单位：</td>
                    <td><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">
                        </select></td>
                </tr>
                <tr>
                    <td>
                        提交时间：
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
<a id="btn_upload" onclick="javascript:return PassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">审核通过</a>
<a onclick="javascript:return NotPassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">拒绝通过</a>

</div>
     <table id="grid" class="easyui-datagrid" style="width:300;border-top:1px solid #DDDDDD"
					data-options="nowrap:true,url:'<%=Url.Content("~/Demo/GetDeviceAuthorizationNew") %>',idField:'id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:false,toolbar: '#tb',loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                data.rows[i].applytime2 = parseDate(data.rows[i].applytime);
                                data.rows[i].checktime2 = parseDate(data.rows[i].checktime);
                                switch(data.rows[i].status) {
                                    case 0:
                                        data.rows[i].status_text = '待审核';
                                        break;
                                    case 1:
                                        data.rows[i].status_text = '审核通过';
                                        break;
                                    case 2:
                                        data.rows[i].status_text = '审核拒绝';
                                        break;
                                    case 3:
                                        data.rows[i].status_text = '拒绝(禁用)';
                                        break;
                                }
                            }
                            return data;
                        },onSortColumn:function(sort, order) {
                            if (sort == '')
                                return;
                            if (sort.indexOf('2') == sort.length - 1) {
                                sort = sort.substring(0, sort.length - 1);
                            }
                            if (sort == 'useruid')
                                sort = 'u.u_uid';
                            
                            if (sort == 'username')
                                sort = 'u.u_name';

                            if (sort == 'unitname')
                                sort = 'u.u_unitname';
                            
                            sortStr = sort + ' ' + order;
                            Search();
                            
                        }">
                <thead data-options="frozen:true">
			<tr>
				<th data-options="field:'id',checkbox:true"></th><th data-options="field:'id2',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return Pass(&quot;'+row.id+'&quot;);\'>审核通过</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return NotPass(&quot;'+row.id+'&quot;);\'>拒绝通过</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return Memo(&quot;'+row.deviceid+'&quot;);\'>备注设备</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return ViewUserDevice(&quot;'+row.useruid+'&quot;)\'>用户设备查看</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RestartJobPluginGroup&quot;)\'>重载</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '';
			}" width="240">操作</th>
			</tr>
		</thead>
        <thead>
			<tr><th data-options="field:'useruid',sortable:true" width="140">账号</th>
                        <th data-options="field:'username',sortable:true" width="140">姓名</th>
                        <th data-options="field:'deviceid',sortable:true" width="280">设备号</th>
                        <th data-options="field:'status_text'" width="80">状态</th>
				<th data-options="field:'unitname',sortable:true" width="140">单位</th>   
                        <th data-options="field:'applytime2',sortable:true" width="150">提交时间</th>
                        <th data-options="field:'model',sortable:true" width="150">设备型号</th>
                        <th data-options="field:'description'" width="150">描述</th>
                       
                        <th data-options="field:'checktime2',sortable:true" width="150">审核通过时间</th>
			</tr>
		</thead>


			</table>
    </div>
</div>



    
    

            <script type="text/javascript">
                var sortStr = 'applytime desc';
                function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
                }

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
                        model: $('#tbModel').val(),
                        u_unitcode: unitCode,
                        u_auth_submit_time_start: $('#tbTimeStart').val(),
                        u_auth_submit_time_end: endTime,
                        deviceAuthStatus: statusCode,
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
                }

                function PassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要通过审核所勾选的设备吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].id;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("PassSelectedDevice") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });
                                }, error: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.statusText, 'info', function () {
                                    });
                                }
                            });
                        }
                    });
                }

                function NotPassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.prompt('确认', '确定要拒绝通过所勾选的设备吗?确认请输入发送给申请人的回复信息!', function (r) {
                        if (r !== undefined) {
                            if (r == '') {
                                $.messager.alert('提示', '回复信息不能为空!', 'info');
                                return;
                            }

                            $('#w2').window('open');
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].id;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("NotPassSelectedDevice") %>',
                                data: { ids: ids, refuse_msg: r },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });
                                }, error: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.statusText, 'info', function () {
                                    });
                                }
                            });
                        }
                    });
//                    $.messager.confirm('确认', '确定要拒绝通过所勾选的设备吗?', function (r) {
//                        if (r) {
//                            
//                        }
//                    });
                }

                function ViewUserDevice(uid) {
                    var url = '<%=Url.Content("~/") %>Demo/userdevice?viewuserdevice=true&uid=' + uid;
                    $('#wif')[0].src = url;

                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                }

                function Pass(id) {
                    $.messager.confirm('确认', '确定要通过审核吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("PassDevice") %>',
                                data: { id: id},
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });
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

                function Memo(id) {
                    $.messager.confirm('确认', '确定要备注设备吗?', function (r) {
                        if (r) {
                            var url = '<%=Url.Content("~/") %>demo/editdevice?deviceid=' + id;
                            $('#wif')[0].src = url;

                            var height = $(document).height() - 20;
                            var width = $(document).width() - 20;
                            $('#w').window({
                                width: width,
                                height: height
                            }).window('open').window('center');                           
                        }
                    });
                    return false;
                }

                function NotPass(id) {
                    $.messager.prompt('确认', '确定要拒绝通过设备吗?确认请输入发送给申请人的回复信息!', function (r) {
                        if (r !== undefined) {
                            if (r == '') {
                                $.messager.alert('提示', '回复信息不能为空!', 'info');
                                return;
                            }
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("NotPassDevice") %>',
                                data: { deviceuserapplyid: id, refuse_msg: r },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.d, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });
                                }, error: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.responseText, 'info', function () {
                                    });
                                }
                            });
                        }
                    });
//                    $.messager.confirm('确认', '确定要拒绝通过设备吗?', function (r) {
//                        if (r) {
//                            
//                        }
//                    });
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

                            $('#grid').datagrid('load', {
                                uid: $('#tbUID').val(),
                                model: $('#tbModel').val(),
                                u_unitcode: unitCode,
                                u_auth_submit_time_start: $('#tbTimeStart').val(),
                                u_auth_submit_time_end: endTime,
                                deviceAuthStatus: statusCode,
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
                        model: $('#tbModel').val(),
                        u_unitcode: unitCode,
                        u_auth_submit_time_start: $('#tbTimeStart').val(),
                        u_auth_submit_time_end: endTime,
                        deviceAuthStatus: statusCode,
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
    <div id="w2" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){ return false}" style="width:150px;height:70px;padding:3px;">
		<img src="../../Images/loading.gif" />处理中,请等待...
	</div>
    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:770px;height:380px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
