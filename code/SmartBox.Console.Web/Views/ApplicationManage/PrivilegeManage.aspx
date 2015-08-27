<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	应用授权管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:131px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>应用管理>>应用授权管理</span>
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
                        应用名称：
                    </td>
                    <td>
                        <input id="tbApp" name="tbApp" type="text" />
                    </td>
                    <td>
                        单位：
                    </td>
                    <td>
                        <%--<input id="tbUName" name="tbUName" type="text" />--%>
                        <select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">

                        </select>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td>
                        状态：
                    </td>
                    <td>
                        <select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="-1">请选择</option>
		                <option value="1">在线</option>
		                <option value="0">离线</option>
                        </select>
                    </td>
                    <td>单位：</td>
                    <td></td>
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
<%--<a href="#" onclick="javascript:return NewAppPrivilege();" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">新增</a>
<a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>--%>
<a href="#" onclick='javascript:return jumpToBUAAuth();'; class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">授权</a>
<a href="#" onclick="javascript:return AsyncSelectedPrivilege();" class="easyui-linkbutton" title="将已授权用户同步至本地数据库" data-options="iconCls:'icon-remove',plain:true">同步</a>
<%--<a id="btn_upload" onclick="javascript:return PassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">解除授权</a>
<a onclick="javascript:return NotPassSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">禁用</a>

<a onclick="javascript:return KickoutSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">强制退出</a>
<a onclick="javascript:return SyncUser();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">同步BUA用户</a>--%>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetPrivilegeManageList") %>',idField:'id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',
                    loadFilter: function (data) {  
                        for (var i = 0; i < data.rows.length; ++i) {  
                            data.rows[i].id2 = data.rows[i].id;
                            data.rows[i].id3 = data.rows[i].id;
                            if (data.rows[i].EnableSync != null)
                                data.rows[i].EnableSync = data.rows[i].EnableSync == 1 ? '启用同步' : '禁用同步';
                            else
                                data.rows[i].EnableSync = '';

                            if (data.rows[i].apid != null)
                                data.rows[i].apid = data.rows[i].apid;
                            else
                                data.rows[i].apid = '';

                            if (data.rows[i].SyncLastTime != null)
                                data.rows[i].LastSyncTime = parseDate(data.rows[i].SyncLastTime);
                            else
                                data.rows[i].LastSyncTime = '';
                         }   
                        return data;  
                     },onSortColumn:function(sort, order) {
                        sort = sort.replace('2', '');

                        if (sort.toLowerCase() == 'displayname')
                            sort = 'app.DisplayName';
                        sortStr = sort + ' ' + order;
                        Search();
                     }">
				<thead>
					<tr>
                        <th data-options="field:'id',checkbox:true">ID</th>
                        <th data-options="field:'id2'">ID</th>
                        <th data-options="field:'DisplayName',sortable:true" width="50">应用名称</th>
						<th data-options="field:'BuaPrivilegeCode'" width="50">权限代码</th>
                        <th data-options="field:'PrivilegeName'" width="50">权限名称</th>
                        <%--<th data-options="field:'EnableSync'" width="80">启用同步</th>
                        <th data-options="field:'SyncIntervalTime',sortable:false" width="40">同步间隔(小时)</th>
                        <th data-options="field:'LastSyncTime',sortable:true" width="50">最后同步时间</th>--%>
						<th data-options="field:'id3',formatter: function(value,row,index){
                            var btn = '';
                        if(row.PrivilegeName == null) 
                            btn += '&nbsp;<a href=\'#\' onclick=\'javascript:return NewAppPrivilege(&quot;'+row.id+'&quot;,&quot;'+row.Name+'&quot;,&quot;'+encodeURIComponent(row.DisplayName)+'&quot;,&quot;'+row.BuaAppCode+'&quot;,&quot;'+row.apid+'&quot;,&quot;'+row.BuaPrivilegeCode+'&quot;,&quot;'+row.SyncIntervalTime+'&quot;);\'>绑定</a>'
                        else{
                            btn += '&nbsp;<a href=\'#\' onclick=\'javascript:return NewAppPrivilege(&quot;'+row.id+'&quot;,&quot;'+row.Name+'&quot;,&quot;'+encodeURIComponent(row.DisplayName)+'&quot;,&quot;'+row.BuaAppCode+'&quot;,&quot;'+row.apid+'&quot;,&quot;'+row.BuaPrivilegeCode+'&quot;,&quot;'+row.SyncIntervalTime+'&quot;);\'>更改绑定</a>'
                            btn += '&nbsp;<a href=\'#\' onclick=\'javascript:return UnAuthAppPrivilege(&quot;'+row.apid+'&quot;)\'>解除授权</a>'
                        }
                        btn += '&nbsp;<a href=\'#\' onclick=\'javascript:return SyncPrivilegeUser(&quot;'+row.apid+'&quot;,&quot;'+row.BuaPrivilegeCode+'&quot;)\'>同步</a>'
                        btn += '&nbsp;<a href=\'#\' onclick=\'javascript:return ViewUser('+row.apid+')\'>查看用户</a>'
                            return btn;
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
                var sortStr = 'app.createtime desc';

                function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
                }

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');
                    var unitCode = $('#selUnit').combobox('getValue');
                    /*var enableStatusCode = $('#selState').combobox('getValue');
                    var enableStatusText = $('#selState').combobox('getText');
                    if (enableStatusCode == '')
                        enableStatusCode = -1;
                    else
                        enableStatusCode = parseInt(enableStatusCode);

                    
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
                    }*/

                    $('#grid').datagrid('load', {
                        app: $('#tbApp').val(),
                        unitcode:unitCode,
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
                }

                function NewAppPrivilege(id, appCode, appName, buaappcode, privilege, BuaPrivilegeCode, SyncIntervalTime) {
                    var url = '<%=Url.Content("~/applicationmanage/editappprivilege")%>?appid=' + id + '&appCode=' + appCode + '&appName=' + appName + '&buaappid=' + buaappcode + '&privilege=' + privilege + '&BuaPrivilegeCode=' + BuaPrivilegeCode + '&SyncIntervalTime=' + SyncIntervalTime;
                    $('#wif')[0].src = url;
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                }

                function UnAuthAppPrivilege(id) {
                    $.messager.confirm('确认', '确定要解除授权吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("UnAuthAppPrivilege") %>',
                                data: { id: id },
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

                function jumpToBUAAuth() {
                    var errMsg = '<%=ViewData["bua_auth_url_err_msg"] %>';
                    var err = '<%=ViewData["bua_auth_url_err"] %>';
                    if (err == 'true') {
                        $.messager.alert('提示', errMsg, 'info', function () {
                        });
                    }
                    else {
                        var url = '<%=ViewData["bua_auth_url"] %>';
                        //$('#wif')[0].src = url;
                        //$('#w').window('open');
                        window.open(url);
                    }
                }

                function NotPassSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要禁用所勾选的用户吗?', function (r) {
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

                function AsyncSelectedPrivilege() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要同步所勾选的应用的授权可访问用户吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                if (rows[i].apid == null || rows[i].apid === undefined)
                                    continue;
                                ids += rows[i].apid;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("AsyncSelectedPrivilege") %>',
                                data: { privilegeIds: ids },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    $.messager.alert('提示', data.Msg, 'info', function () {
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

                function Pass(uid, uname) {
                    $.messager.confirm('确认', '确定要启用用户' + uname + '吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("PassUser") %>',
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

                function ViewUser(id) {
                    var url = '<%=Url.Content("~/applicationmanage/privilegeuser?privilegecode=")%>' + id;
                    $('#wif')[0].src = url;
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                }

                function SyncPrivilegeUser(appPrivilegeID, buaPrivilegeCode) {
                    $.messager.confirm('确认', '确定要将bua授权的用户同步到系统吗?', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("AsyncPrivilege") %>',
                                data: { privilegeId: appPrivilegeID },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    if (data.IsSuccess)
                                        data.Msg = '同步成功！';
                                    $.messager.alert('提示', data.Msg, 'info', function () {
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
                                url: '<%=Url.Action("KickoutSelected") %>',
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
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("NotPassUser") %>',
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
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');
                    var unitCode = $('#selUnit').combobox('getValue');
                    /*var enableStatusCode = $('#selState').combobox('getValue');
                    var enableStatusText = $('#selState').combobox('getText');
                    if (enableStatusCode == '')
                        enableStatusCode = -1;
                    else
                        enableStatusCode = parseInt(enableStatusCode);

                    
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
                    }*/

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
                                app: $('#tbApp').val(),
                                unitcode: unitCode,
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
                        app: $('#tbApp').val(),
                        unitcode: unitCode,
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
    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:790px;height:294px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
