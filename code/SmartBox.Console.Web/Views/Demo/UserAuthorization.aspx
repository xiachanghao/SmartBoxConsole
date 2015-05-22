<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	用户审核
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">    <div data-options="region:'north',split:false" style="height:195px;overflow:hidden;border:0px solid #DDDDDD;">        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>用户管理>>用户审核</span>
        </div>        <div style="height:3px;display:;"></div>

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
                        <input id="tbUName" name="tbUName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        状态：
                    </td>
                    <td>
                        <select id="selEnableState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
		                <option value="2">待审核</option>
                        </select>
                    </td>
                    <td>单位：</td>
                    <td><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">
                        </select></td>
                </tr><tr>
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
    </div>    </div>
    <div style="height:3px;display:;"></div>    </div>    <div data-options="region:'center'" style="width:100%;">
                <div id="tb" style="text-align:right;">
<a id="btn_upload" onclick="javascript:return PassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">通过</a>
<a onclick="javascript:return NotPassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">不通过</a>
<%--<a onclick="javascript:return DeleteDll();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">强制退出</a>
<a onclick="javascript:return DeleteDll();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">删除用户</a>--%>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetUserAuthorizationList") %>',idField:'U_ID',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',
                    loadFilter: function (data) {  
                        for (var i = 0; i < data.rows.length; ++i) {  
                            data.rows[i].u_auth_submit_time2 = parseDate(data.rows[i].u_auth_submit_time);
                            data.rows[i].U_UID2 = data.rows[i].U_UID;
                            data.rows[i].u_auth_submit_man = '单位管理员';
                            switch(data.rows[i].u_enable_status) {
                                case 0:
                                    data.rows[i].u_enable_status2 = '已禁用';
                                    break;
                                case 1:
                                    data.rows[i].u_enable_status2 = '已启用';
                                    break;
                                case 2:
                                    data.rows[i].u_enable_status2 = '待审核';
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
                        <th data-options="field:'U_ID',checkbox:true"></th>
						<th data-options="field:'U_NAME'" width="30">姓名</th>
                        <th data-options="field:'U_UID'" width="30">账号</th>
                        <th data-options="field:'u_unitname'" width="50">单位</th>
                        <th data-options="field:'u_auth_submit_man'" width="30">提交人</th>
                        <th data-options="field:'u_auth_submit_time2',sortable:true" width="30">提交时间</th>
                        <th data-options="field:'u_enable_status2'" width="20">启用状态</th>
						<th data-options="field:'U_UID2',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return Pass(&quot;'+row.U_UID+'&quot;,&quot;'+row.U_NAME+'&quot;);\'>通过</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return NotPass(&quot;'+row.U_UID+'&quot;,&quot;'+row.U_NAME+'&quot;)\'>不通过</a>' + 
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
			</table>    </div></div>



    
    

            <script type="text/javascript">
                function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
                }

                function NotPassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要不通过所勾选的用户吗?', function (r) {
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
                                url: '<%=Url.Action("NotPassSelectedUser") %>',
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

                function PassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要通过所勾选的用户吗?', function (r) {
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
                                url: '<%=Url.Action("PassSelectedUser") %>',
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

                function Pass(uid,uname) {
//                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd?pd_id=' + pd_id;
//                    $('#wif')[0].src = url;
                    //                    $('#w').window('open');
                    $.messager.confirm('确认', '确定要通过用户' + uname + '吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("PassUser") %>',
                                data: { uid: uid, uname: uname },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    var s = data.d;
                                    if (data.r) {
                                        s += ',已通过审核!';
                                    }
                                    $.messager.alert('提示', s, 'info', function () {
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

                function NotPass(uid, uname) {
                    $.messager.confirm('确认', '确定不通过用户' + uname + '吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("NotPassUser") %>',
                                data: { uid: uid, uname: uname },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    var s = data.d;
                                    if (data.r) {
                                        s += ',未通过审核';
                                    }
                                    $.messager.alert('提示', s, 'info', function () {
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
                    return result;
                }
                var page = 0;
                var sortStr = 'u_auth_submit_time desc';
                $(document).ready(function () {
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    var enableStatusCode = $('#selEnableState').combobox('getValue');
                    var enableStatusText = $('#selEnableState').combobox('getText');

                    var unitCode = $('#selUnit').combobox('getValue');
                    var unitText = $('#selUnit').combobox('getText');

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    if (enableStatusCode == '')
                        enableStatusCode = -1;
                    else
                        enableStatusCode = parseInt(enableStatusCode);

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
                                u_auth_submit_time_start: $('#tbTimeStart').val(),
                                u_auth_submit_time_end: endTime,
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
                        lockStatus: -1,
                        enabledStatus: enableStatusCode,
                        authStatus: -1,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

                    /*$('#grid').datagrid({
                    loadFilter: function (data) {
                    for (var i = 0; i < data.rows.length; ++i) {
                    data.rows[i].pd_status_text = data.rows[i].pd_status ? '已启用' : '已禁用';
                    data.rows[i].pdid = data.rows[i].pd_id;
                    }
                    return data;
                    }
                    });*/

                });

                function UploadDll() {
//                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd';
//                    $('#wif')[0].src = url;
//                    $('#w').window('open');
                    return false;
                }

                function CloseWind(refreshGrid) {
                    if (refreshGrid) {
                        var dg = $('#grid');
                        $('#grid').datagrid('reload');
                    }
                    $('#w').window('close');
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
<script type="text/javascript">
    function Search() {
        var dg = $('#grid');
        var opts = dg.datagrid('options');

        var enableStatusCode = $('#selEnableState').combobox('getValue');
        var enableStatusText = $('#selEnableState').combobox('getText');
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

        $('#grid').datagrid('load', {
            uid: $('#tbUID').val(),
            uname: $('#tbUName').val(),
            u_unitcode: unitCode,
            u_auth_submit_time_start: $('#tbTimeStart').val(),
            u_auth_submit_time_end: endTime,
            lockStatus: -1,
            enabledStatus: enableStatusCode,
            authStatus: -1,
            orderby: sortStr,
            pageIndex: 0,
            pageSize: opts.pageSize
        });
    }
</script>
</asp:Content>
