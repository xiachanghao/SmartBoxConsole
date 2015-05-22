<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	单位访问量
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:131px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>统计管理>>单位访问量</span>
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
                        单位：
                    </td>
                    <td>
                        <select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
            var item_length = $('div[id^=\'_easyui_combobox_i2_\']').length;
            if (item_length > 10) {
               // $('div[id^=\'_easyui_combobox_i2_\']').parent().parent().css('panelHeight',100);
                //$('.combo-panel').css('height', 100);
            }
        }" name="state" style="width:184px;">
                        </select>
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr style="display:none;">
                    <td>
                        登录时间：用户账号：<input id="tbUID" name="tbUID" type="text" />
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td>
                        状态：设备类型：<input id="tbModel" name="tbModel" type="text" />
                    </td>
                    <td>
                        <select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
                        <option value="1">已锁定</option>
		                <option value="2">已挂失</option>
                        <option value="0">正常</option>
                        </select>
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
            
    <div id="tb" style="text-align:right;">&nbsp;
<%--<a id="btn_upload" onclick="javascript:return LostSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">挂失</a>
<a onclick="javascript:return LostSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">挂失</a>
<a onclick="javascript:return UnLostSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">解除挂失</a>
<a onclick="javascript:return LockSelectedDevice();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">锁定</a>
<a onclick="javascript:return UnLockSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">解除锁定</a>

<a onclick="javascript:return void();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">解除绑定</a>--%>
</div>
     <table id="grid" class="easyui-datagrid" sortName=losttime sortOrder=desc style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetStatisticsByUnit") %>',idField:'deviceid',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:false,toolbar: '#tb',loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                
                            }
                            return data;
                        },onSortColumn:function(sort, order) {
                            if (sort == '')
                                return;
                            if (sort.indexOf('2') == sort.length - 1) {
                                sort = sort.substring(0, sort.length - 1);
                            }
                            if (sort == 'PadiOS') {
                                sort = 'PadiOS';
                            }
                            
                            if (sort == 'PhoneiOS')
                                sort = 'PhoneiOS';

                            if (sort == 'PadAndroid')
                                sort = 'PadAndroid';
                                
                            if (sort == 'PhoneAndroid')
                                sort = 'PhoneAndroid';
                            
                            sortStr = sort + ' ' + order;

                            Search();
                            
                        },onBeforeLoad:function(param) {
                            //if (sortStr.indexOf('asc') != -1)
                            //    sortStr = 'u.u_uid desc';
                            //alert(param);
                        }">
                        <thead data-options="frozen:true">
			<tr><th data-options="field:'deviceid2',checkbox:true"></th><th data-options="hidden:true,field:'deviceid3',formatter: function(value,row,index){
				return '' +
                //'<a href=\'#\' onclick=\'javascript:return Lost(&quot;'+value+'&quot;);\'>挂失</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return UnLost(&quot;'+value+'&quot;);\'>解除挂失</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return Lock(&quot;'+value+'&quot;);\'>锁定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return UnLock(&quot;'+value+'&quot;);\'>解除锁定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return UnBind(&quot;'+row.deviceuser_id+'&quot;);\'>解除绑定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+')\'>解除绑定</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RestartJobPluginGroup&quot;)\'>重载</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '';
			}" width="170">操作</th></tr>
            </thead>
				<thead>
                
					<tr>
                        
                        <th data-options="field:'UnitName',sortable:true" width="250">单位名称</th>
                        <th data-options="field:'UserCount',sortable:true" width="90">访问账号数量</th>
                        <th data-options="field:'PadAndroid',sortable:true" width="120">Pad/Android(小时)</th> 
                        <th data-options="field:'PadiOS',sortable:true" width="100">Pad/iOS(小时)</th>
                        <th data-options="field:'PCWindows',sortable:true" width="120">PC/Windows(小时)</th>
                        <th data-options="field:'PhoneAndroid',sortable:true" width="130">Phone/Android(小时)</th>
                        <th data-options="field:'PhoneiOS',sortable:true" width="100">Phone/iOS(小时)</th>
                        <th data-options="field:'UsageCount',sortable:true" width="100">总计访问次数</th>
						
					</tr>
				</thead>
			</table>
    </div>
</div>



    
    

            <script type="text/javascript">
                var sortStr = 'UnitName Asc';

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

                    var endTime2 = $('#tbTimeEnd2').val();
                    endTime2 = $.trim(endTime2);
                    if (endTime2 != '') {
                        endTime2 = endTime2 + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        uid: $('#tbUID').val(),
                        model: $('#tbModel').val(),
                        u_unitcode: unitCode,
                        lost_time_start: $('#tbTimeStart').val(),
                        lost_time_end: endTime,
                        unlost_time_start: $('#tbTimeStart2').val(),
                        unlost_time_end: endTime2,
                        status: statusCode,
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
                }

                function UnLockSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要解除锁定所勾选的设备吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].deviceid;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("UnLockSelectedDevice") %>',
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

                function UnLock(id) {
                    $.messager.confirm('确认', '确定要解除锁定设备吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("UnLockDevice") %>',
                                data: { deviceid: id },
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

                function UnBind(deviceuser_id) {
                    //alert(deviceuser_id);

                    $.messager.confirm('确认', '确定要解除锁定绑定吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("UnBindDevice") %>',
                                data: { deviceuser_id: deviceuser_id },
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

                function LostSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要挂失所勾选的设备吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
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

                function Lock(deviceid) {
                    $.messager.prompt('锁定时长', '请录入锁定多长时间（单位：小时）:', function (r) {
                        if (r) {
                            if ($.validate.isInt(r) == false) {
                                $.messager.alert('提示', '请录入正确的时长', 'info', function () {
                                });
                            } else {
                                $('#w2').window('open');
                                $.ajax({
                                    type: "POST",
                                    url: '<%=Url.Action("LockDevice") %>',
                                    data: { id: deviceid, hour: r },
                                    dataType: "json",
                                    success: function (data) {
                                        $('#w2').window('close');
                                        $.messager.alert('提示', data.d, 'info', function () {
                                        });

                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    }, error: function (data) {
                                        $('#w2').window('close');
                                        //$.messager.alert('提示', data.responseText, 'info', function () {
                                        //});
                                        $.messager.alert('提示', data.statusText, 'info', function () {
                                        });
                                    }
                                });
                            }
                        }
                    });
                }

                function LockSelectedDevice() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    alert(rows.length);
                    var ids = '';
                    for (var i = 0; i < rows.length; ++i) {
                        ids += rows[i].deviceid;
                        alert(rows[i].deviceid);
                        if (i < rows.length - 1)
                            ids += ',';
                    }
                    alert(ids);

                    $.messager.prompt('锁定时长', '请录入锁定多长时间（单位：小时）:', function (r) {
                        if (r) {
                            if ($.validate.isInt(r) == false) {
                                $.messager.alert('提示', '请录入正确的时长', 'info', function () {
                                });
                            } else {
                                $('#w2').window('open');


                                $.ajax({
                                    type: "POST",
                                    url: '<%=Url.Action("LockSelectedDevice") %>',
                                    data: { ids: ids, hour: r },
                                    dataType: "json",
                                    success: function (data) {
                                        $('#w2').window('close');
                                        $.messager.alert('提示', data.d, 'info', function () {
                                        });

                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    }, error: function (data) {
                                        $('#w2').window('close');
                                        //$.messager.alert('提示', data.responseText, 'info', function () {
                                        //});
                                        $.messager.alert('提示', data.statusText, 'info', function () {
                                        });
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
                            $('#w2').window('open');
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

                function Lost(id) {
                    $.messager.confirm('确认', '确定要挂失吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("LostDevice") %>',
                                data: { deviceid: id },
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

                function UnLost(id) {
                    $.messager.confirm('确认', '确定要解除挂失设备吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("UnLostDevice") %>',
                                data: { deviceid: id },
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

                    var viewuserdevice = '<%=Request.QueryString["viewuserdevice"] %>';
                    if (viewuserdevice.toLowerCase() == 'true') {
                        //$('.table_box').hide();
                        //$('div.layout-panel-north div').height(0);
                        //$('div.layout-panel-center').css('top', 0);
                        //$('div.layout-panel-center div').height(300);
                        var uid = '<%=Request.QueryString["uid"] %>';
                        $('#tbUID').val(uid);
                    }

                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    var endTime2 = $('#tbTimeEnd2').val();
                    endTime2 = $.trim(endTime2);
                    if (endTime2 != '') {
                        endTime2 = endTime2 + ' 23:59:59';
                    }

                    var statusCode = $('#selState').combobox('getValue');
                    if (statusCode == '')
                        statusCode = -1;
                    else
                        statusCode = parseInt(statusCode);

                    var unitCode = $('#selUnit').combobox('getValue');

                    //var sortName = $('#grid').datagrid('sortName');
                    //var sortOrder = $('#grid').datagrid('sortOrder');

                    //if (sortName != '')
                    //sortStr = sortName + ' ' + sortOrder;

                    pager.pagination({
                        onSelectPage: function (pageNum, pageSize) {
                            page = pageNum;

                            opts.pageNumber = pageNum;
                            opts.pageSize = pageSize;

                            var endTime = $('#tbTimeEnd').val();
                            endTime = $.trim(endTime);
                            if (endTime != '') {
                                endTime = endTime + ' 23:59:59';
                            }

                            var endTime2 = $('#tbTimeEnd2').val();
                            endTime2 = $.trim(endTime2);
                            if (endTime2 != '') {
                                endTime2 = endTime2 + ' 23:59:59';
                            }

                            var statusCode = $('#selState').combobox('getValue');
                            if (statusCode == '')
                                statusCode = -1;
                            else
                                statusCode = parseInt(statusCode);

                            var unitCode = $('#selUnit').combobox('getValue');

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });



                            $('#grid').datagrid('load', {
                                uid: $('#tbUID').val(),
                                model: $('#tbModel').val(),
                                u_unitcode: unitCode,
                                lost_time_start: $('#tbTimeStart').val(),
                                lost_time_end: endTime,
                                unlost_time_start: $('#tbTimeStart2').val(),
                                unlost_time_end: endTime2,
                                status: statusCode,
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

                    //var s = 'x:' + $('#grid').datagrid('sortName');
                    //alert(s);

                    $('#grid').datagrid('load', {
                        uid: $('#tbUID').val(),
                        model: $('#tbModel').val(),
                        u_unitcode: unitCode,
                        lost_time_start: $('#tbTimeStart').val(),
                        lost_time_end: endTime,
                        unlost_time_start: $('#tbTimeStart2').val(),
                        unlost_time_end: endTime2,
                        status: statusCode,
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
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
