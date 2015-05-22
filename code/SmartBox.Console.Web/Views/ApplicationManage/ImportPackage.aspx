<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	app导入
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:164px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>发布管理>>历史app导入</span>
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
                        安装包名：
                    </td>
                    <td>
                        <input id="tbName" name="tbName" type="text" />
                    </td>
                    <td>
                        显示名：
                    </td>
                    <td>
                        <input id="tbDisplayName" name="tbDisplayName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        类型：
                    </td>
                    <td>
                        <select id="selType" class="easyui-combobox" data-options="panelHeight:'auto'" name="selType" style="width:184px;">
                        <option value="">请选择</option>
                        <option value="main">主程序</option>
		                <option value="plugin">插件</option>
                        </select>
                    </td>
                    <td>客户端类型：</td>
                    <td><select id="selClientType" class="easyui-combobox" data-options="panelHeight:'auto'" name="selClientType" style="width:184px;"><option value="">请选择</option>
                        <option value="Pad/Android">Pad/Android</option>
		                <option value="Phone/Android">Phone/Android</option>
                        <option value="Phone/iOS">Phone/iOS</option>
                        <option value="Pad/iOS">Pad/iOS</option>
                        </select></td>
                </tr>
                <tr style="display:none;">
                    <td>
                        审核通过时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        <%--<input id="tbTimeStart2" name="tbTimeStart2" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd2" name="tbTimeEnd2" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>--%>
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
<%--<a id="btn_upload" onclick="javascript:return LostSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">挂失</a>--%>
<a onclick="javascript:return ImportOldPackages();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">导入</a>
<%--<a onclick="javascript:return Lock();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">锁定</a>
<a onclick="javascript:return UnLockSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">解除锁定</a>
<a onclick="javascript:return void();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">挂失</a>
<a onclick="javascript:return void();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">解除绑定</a>--%>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetUnImportedAppPackages") %>',idField:'id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                //data.rows[i].losttime2 = parseDate(data.rows[i].losttime);
                                //data.rows[i].unlosttime2 = parseDate(data.rows[i].unlosttime);
                                data.rows[i].id2 = data.rows[i].id;
                                data.rows[i].id3 = data.rows[i].id;
                                /*switch(data.rows[i].status) {
                                    case 0:
                                        data.rows[i].status_text = '正常';
                                        break;
                                    case 1:
                                        data.rows[i].status_text = '锁定';
                                        break;
                                    case 2:
                                        data.rows[i].status_text = '已挂失';
                                        break;
                                }*/
                            }
                            return data;
                        }">
				<thead>
					<tr>
                        <th data-options="field:'id2',checkbox:true"></th>
                        <th data-options="field:'name'" width="80">安装包名</th>
                        <th data-options="field:'displayname'" width="50">显示名</th>
                        <th data-options="field:'type'" width="20">类型</th> 
                        <th data-options="field:'clienttype'" width="40">客户端类型</th>
                        <th data-options="field:'version'" width="30">版本</th>
                        <th data-options="field:'buildver'" width="20">buildver</th>
                        <th data-options="field:'downloaduri'" width="170">下载地址</th>
						<th data-options="field:'id3',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return ImportOldPackage(&quot;'+value+'&quot;);\'>导入</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return UnLost(&quot;'+value+'&quot;);\'>解除挂失</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return Lock(&quot;'+value+'&quot;);\'>锁定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return UnLock(&quot;'+value+'&quot;);\'>解除锁定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+')\'>解除绑定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RestartJobPluginGroup&quot;)\'>重载</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '';
			}" width="30">操作</th>
					</tr>
				</thead>
			</table>
    </div>
</div>
            <script type="text/javascript">
                var sortStr = 'id desc';

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var typeCode = $('#selType').combobox('getValue');
                    //var enableStatusText = $('#selState').combobox('getText');

                    var clientType = $('#selClientType').combobox('getValue');

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        name: $('#tbName').val(),
                        displayName: $('#tbDisplayName').val(),
                        clientType: clientType,
                        lost_time_start: $('#tbTimeStart').val(),
                        lost_time_end: endTime,
                        type: typeCode,
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
                }

                function ImportOldPackages() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要导入所勾选的app吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].id;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("ImportOldPackage4AI") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    $.messager.alert('提示', data.Msg, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
                }

                function ImportOldPackage(id) {
                    $.messager.confirm('确认', '确定要导入app吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("ImportOldPackage4AI") %>',
                                data: { ids: id },
                                dataType: "json",
                                success: function (data) {
                                    $.messager.alert('提示', data.Msg, 'info', function () {
                                    });

                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                }
                            });
                        }
                    });
                    return false;
                }

                function LostSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要通过审核所勾选的设备吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].deviceid;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("LostSelectedDevice") %>',
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

                function Lock(deviceid) {
                    $.messager.prompt('锁定时长', '请录入锁定多长时间（单位：小时）:', function (r) {
                        if (r) {
                            if ($.validate.isInt(r) == false) {
                                $.messager.alert('提示', '请录入正确的时长', 'info', function () {
                                });
                            } else {
                                $.ajax({
                                    type: "POST",
                                    url: '<%=Url.Action("LockSelectedDevice") %>',
                                    data: { id: deviceid, hour: r },
                                    dataType: "json",
                                    success: function (data) {
                                        $.messager.alert('提示', data.d, 'info', function () {
                                        });

                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    }, error: function (data) {
                                    }
                                });
                            }
                        }
                    });
                }

                function UnLostSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要解除挂失所勾选的设备吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].deviceid;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("UnLostSelectedDevice") %>',
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

                function Lost(id) {
                    $.messager.confirm('确认', '确定要挂失吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("LostDevice") %>',
                                data: { deviceid: id },
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

                function UnLost(id) {
                    $.messager.confirm('确认', '确定要解除挂失设备吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("UnLostDevice") %>',
                                data: { deviceid: id },
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

                function AddDeviceLost() {
                    var url = '<%=Url.Content("~/") %>demo/devicelostadd';
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

                    var typeCode = $('#selType').combobox('getValue');

                    var clientType = $('#selClientType').combobox('getValue');

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
                                name: $('#tbName').val(),
                                displayName: $('#tbDisplayName').val(),
                                clientType: clientType,
                                lost_time_start: $('#tbTimeStart').val(),
                                lost_time_end: endTime,
                                type: typeCode,
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
                        name: $('#tbName').val(),
                        displayName: $('#tbDisplayName').val(),
                        clientType: clientType,
                        lost_time_start: $('#tbTimeStart').val(),
                        lost_time_end: endTime,
                        type: typeCode,
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
