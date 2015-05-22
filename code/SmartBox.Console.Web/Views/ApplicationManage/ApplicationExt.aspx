<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	发布管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:131px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>发布管理>>发布管理</span>
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
                        <input id="tbPack" name="tbPack" type="text" />
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
                <tr style="display:none;">
                    <td>
                        同步状态：
                    </td>
                    <td>
                        <select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">未同步</option>
		                <option value="DISABLE">同步成功</option>
                        <option value="DISABLE">部分同步</option>
                        </select>
                    </td>
                    <td>单位：</td>
                    <td><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/") %>Demo/GetUnitData',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;"></td>
                </tr>
                <tr style="display:none;">
                    <td>
                        提交时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        上架状态：
                    </td>
                    <td>
                        <select id="Select1" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">待上架</option>
		                <option value="DISABLE">已上架</option>
                        <option value="DISABLE">已下加</option>
                        </select>
                    </td>
                </tr><tr style="display:none;">
                    <td>
                        审核时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                    <td>
                        最后同步时间：
                    </td>
                    <td>
                        <input id="tbTimeStart2" name="tbTimeStart2" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd2" name="tbTimeEnd2" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
                    </td>
                </tr><tr style="display:none;">
                    <td>
                        下架时间：
                    </td>
                    <td>
                        <input id="tbTimeStart3" name="tbTimeStart3" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd3" name="tbTimeEnd3" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>
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
<a id="btn_upload" onclick="javascript:return addWeb();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增轻应用</a>
<a id="btn_upload" onclick="javascript:return addPackage();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增app</a>
<a id="btn_upload" onclick="javascript:return importPackage();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">导入历史app</a>
<a onclick="javascript:return addOutPackage();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">新增外部app</a>
<a onclick="javascript:return editIOSOutsideApp();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">新增内联应用（ios）</a>
<a onclick="javascript:return sjselected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">上架</a>
<a onclick="javascript:return xjselected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">下架</a>
<a onclick="javascript:return scselected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
<a id="A1" onclick="javascript:return copyRelease();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">发布复制</a>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetApplicationExtList") %>',idField:'pe_id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:false,toolbar: '#tb',loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                //data.rows[i].applytime2 = parseDate(data.rows[i].applytime);
                                data.rows[i].pe_id2 = data.rows[i].pe_id;
                                data.rows[i].apptype = '轻应用';

                                 switch (data.rows[i].TableName) {
                                    case 'IOSOutsideApplication':
                                        data.rows[i].apptype = '内联应用（ios）';
                                        break;
                                    case 'WebApplication':
                                        data.rows[i].apptype = '轻应用';
                                        break;
                                    case 'Package4AI':
                                        data.rows[i].apptype = '应用';
                                        break;
                                    case 'SMC_Package4Out':
                                        data.rows[i].apptype = '外部应用';
                                        break;
                                }

                                //data.rows[i].submit_time2 = data.rows[i].submit_time;
                                //data.rows[i].submit_time3 = data.rows[i].submit_time;
                                //data.rows[i].submit_time4 = data.rows[i].submit_time;
                                data.rows[i].tj = data.rows[i].pe_IsTJ == true ? '是' : '否';
                                data.rows[i].bb = data.rows[i].pe_IsBB == true ? '是' : '否';

                                switch(data.rows[i].pe_SyncStatus) {
                                    case null:
                                        break;
                                    case 0:
                                        data.rows[i].status_sync = '待同步';
                                        break;
                                    case 1:
                                        data.rows[i].status_sync = '同步成功';
                                        break;
                                    case 2:
                                        data.rows[i].status_sync = '同步失败';
                                        break;
                                }
                                switch(data.rows[i].pe_UsefulStstus) {
                                    case false:
                                        data.rows[i].status_sj = '待上架';
                                        break;
                                    case true:
                                        data.rows[i].status_sj = '已上架';
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
                            sortStr = 'pe.' + sort + ' ' + order;
                            Search();
                        }">
                        <thead data-options="frozen:true">
			<tr><th data-options="field:'pe_id',checkbox:true"></th>
						<th data-options="field:'id2',formatter: function(value,row,index){
                        var s = '&nbsp;';
                        if(row.status_sj != '待上架') {
                            s =  '&nbsp;<a href=\'#\' onclick=\'javascript:return update('+row.pe_id+')\'>升级</a>';
                        }
                        var sxg = '&nbsp;<a href=\'#\' onclick=\'javascript:return modify('+row.pe_id+',&quot;'+row.TableName+'&quot;,&quot;'+row.TableID+'&quot;)\'>修改</a>';
                        if (row.TableName == 'Package4AI' && row.TableID == '0') {
                            sxg = '';
                        }
				return '' +
                '<a href=\'#\' onclick=\'javascript:return view('+row.pe_id+')\'>查看</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return EnableClick('+row.pe_id+')\'>上架</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return DisEnableClick('+row.pe_id+')\'>下架</a>' + 
               s + 
                sxg + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return deletePackage('+row.pe_id+');\'>删除</a>' + 
                
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '';
			}" width="180">操作</th>	</tr></thead>
				<thead>
					<tr>
                        					
                        <th data-options="field:'pe_DisplayName',sortable:true" width="150">app名称</th>
            <th data-options="field:'pe_Version',sortable:true" width="120">版本</th>
                        
                        <th data-options="field:'pe_ClientType',sortable:true" width="130">客户端</th>
                        <th data-options="field:'status_sync',styler: function(value,row,index){
				if (value == '部分同步'){
					return 'background-color:#ffee00;color:red;';
				} else if (value == '同步成功'){
					return 'background-color:green;color:#FFFFFF;';
				}
			}" width="60">同步状态</th>
                        <th data-options="field:'status_sj',styler: function(value,row,index){
				if (value == '待上架'){
					return 'background-color:#ffee00;color:red;';
				} else if (value == '已上架'){
					return 'background-color:green;color:#FFFFFF;';
				}
			}" width="60">上架状态</th>
                        <th data-options="field:'apptype',sortable:true" width="60">类型</th>
                        <th data-options="field:'pe_LastVersion'" width="130">上一版本</th>
                        <th data-options="field:'pe_UnitName',sortable:true" width="150">单位</th> 
                        <th data-options="field:'pe_ApplicationName'" width="150">应用清单</th>                        
                        <%--<th data-options="field:'submit_time'" width="60">提交时间</th>
                        <th data-options="field:'submit_time2'" width="60">审核时间</th>
                        <th data-options="field:'submit_time3'" width="60">最后同步时间</th>
                        <th data-options="field:'submit_time4'" width="60">下架时间</th> --%>                       
                        <th data-options="field:'pe_Category'" width="130">类别</th>
                        <th data-options="field:'tj'" width="30">推荐</th>
                        <th data-options="field:'bb'" width="30">必备</th>
                        
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

                function ModifyEntity(pd_id) {
                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd?pd_id=' + pd_id;
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                    return false;
                }

                function copyRelease() {
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("CopyAppFilesToAppCenterServer") %>',
                        data: {  },
                        dataType: "json",
                        success: function (data) {
                            if (data.r) {
                                $.messager.alert('提示', data.d, 'info', function () {
                                    //var dg = $('#grid');
                                    //$('#grid').datagrid('reload');
                                });
                            }
                            else {
                                $.messager.alert('提示', "操作失败，可能的原因" + data.d, 'info', function () {
                                });
                            }
                        }
                    });
                }

                function EnableClick(id) {
                    $('#w2').window('open');
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("SetUserfulStatus") %>',
                        data: { id: id, Operation: "ENABLE" },
                        dataType: "json",
                        success: function (data) {
                            $('#w2').window('close');
                            if (data.IsSuccess) {
                                $.messager.alert('提示', "操作成功!", 'info', function () {
                                    var dg = $('#grid');
                                    $('#grid').datagrid('reload');
                                });
                            }
                            else {
                                $.messager.alert('提示', "操作失败，可能的原因" + data.Msg, 'info', function () {
                                });
                            }
                        }
                    });
                }

                function DisEnableClick(id) {
                    
                    $.messager.confirm('确认', '确定要下架所勾选的app吗?下架后再上架需要重新审核！', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("SetUserfulStatus") %>',
                                data: { id: id, Operation: "DISABLE" },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', "操作成功!", 'info', function () {
                                            var dg = $('#grid');
                                            $('#grid').datagrid('reload');
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', "操作失败，可能的原因" + data.Msg, 'info', function () {
                                        });
                                    }
                                }
                            });
                        }
                    });
                }

                //批量删除
                function scselected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的app吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].pe_id;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Content("~/") %>ApplicationManage/DeletePackageExts',
                                data: { pe_ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', data.Msg, 'info', function () {
                                            var dg = $('#grid');
                                            $('#grid').datagrid('reload');
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', "操作失败，可能的原因", 'info', function () {
                                        });
                                    }
                                }
                            });
                        }
                    });
                }

                //批量上架
                function sjselected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要上架所勾选的app吗?', function (r) {
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
                                url: '<%=Url.Action("SetUserfulStatusx") %>',
                                data: { ids: ids, Operation: "ENABLE" },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', data.Msg, 'info', function () {
                                            var dg = $('#grid');
                                            $('#grid').datagrid('reload');
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', "操作失败，可能的原因" + data.Msg, 'info', function () {
                                        });
                                    }
                                }
                            });
                        }
                    });
                }

                //批量下架
                function xjselected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要下架所勾选的app吗?', function (r) {
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
                                url: '<%=Url.Action("SetUserfulStatusx") %>',
                                data: { ids: ids, Operation: "DISABLE" },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', data.Msg, 'info', function () {
                                            var dg = $('#grid');
                                            $('#grid').datagrid('reload');
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', "操作失败，可能的原因" + data.Msg, 'info', function () {
                                        });
                                    }
                                }
                            });
                        }
                    });
                }

                function addWeb() {
                    var url = '<%=Url.Content("~/") %>ApplicationManage/EditWebApplication';
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                    return false;
                }
                
                function addPackage() {
                    var url = '<%=Url.Content("~/") %>ApplicationManage/CreateApplicationPackage';
                    $('#wif')[0].src = url;
                    //$('#w').window('open').height('100');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                    return false;
                }
                
                function addOutPackage() {
                    var url = '<%=Url.Content("~/") %>ApplicationManage/CreateOutApplicationPackage';
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                    return false;
                }

                function editIOSOutsideApp() {
                    var url = '<%=Url.Content("~/") %>IOSOutsideAppManage/EditIOSOutsideApp';
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                    return false;
                }

                function importPackage() {
                    var url = '<%=Url.Content("~/") %>ApplicationManage/ImportPackage';
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
                    return false;
                }

                function view(id) {
                    var url = '<%=Url.Content("~/") %>ApplicationManage/ViewPackage2/' + id + '';
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');

                    return false;
                }

                function modify(pe_id, tableName, tableId) {
                    
                    var url = '<%=Url.Content("~/") %>ApplicationManage/ModifyPackageExt/' + pe_id + '';
                    switch (tableName) {
                        case "IOSOutsideApplication":
                            url = '<%=Url.Content("~/") %>IOSOutsideAppManage/EditIOSOutsideApp?id=' + tableId;
                            break;
                        case "WebApplication":
                            url = '<%=Url.Content("~/") %>ApplicationManage/EditWebApplication?id=' + tableId;
                            break;
                        case "Package4AI":
                            url = '<%=Url.Content("~/") %>ApplicationManage/EditApplicationPackage?id=' + tableId;
                            break;
                        case "SMC_Package4Out":
                            url = '<%=Url.Content("~/") %>ApplicationManage/EditOutPackage?id=' + tableId;
                            break;
                    }
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');

                    return false;
                }

                function update(id) {
                    var url = '<%=Url.Content("~/") %>ApplicationManage/UpdatePackage/' + id + '';
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');

                    return false;
                }

                function deletePackage(id) {
                    $.messager.confirm('确认', '确定要删除所勾选的app吗?app相关的同步记录、截图、附件等信息都将删除且不可恢复！', function (r) {
                        if (r) {
                            $('#w2').window('open');
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Content("~/") %>ApplicationManage/DeletePackageExt',
                                data: { pe_id: id },
                                dataType: "json",
                                success: function (data) {
                                    $('#w2').window('close');
                                    if (data.IsSuccess) {
                                        $.messager.alert('提示', "操作成功", 'info', function () {
                                            var dg = $('#grid');
                                            $('#grid').datagrid('reload');
                                        });
                                    }
                                    else {
                                        $.messager.alert('提示', "操作失败，可能的原因", 'info', function () {
                                        });
                                    }
                                }
                            });
                        }
                    });
                    
                    return false;
                }

                function OperateDll(pd_id, op) {
                    var url = '<%=Url.Content("~/") %>PushManage/JobCommandCtrl?pd_id=' + pd_id + '&operate=' + op;
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');

                    return false;
                }
                var sortStr = 'pe_id desc';
                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var appName = $('#selApp').combobox('getText');
                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        appName: $('#tbPack').val(),
                        application: appName,
                        u_unitcode: '',
                        u_auth_submit_time_start: $('#tbTimeStart').val(),
                        u_auth_submit_time_end: endTime,
                        categoryID: '',
                        orderby: sortStr,
                        pageIndex: 0,
                        pageSize: opts.pageSize
                    });
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
                                appName: '',
                                application: '',
                                u_unitcode: '',
                                u_auth_submit_time_start: $('#tbTimeStart').val(),
                                u_auth_submit_time_end: endTime,
                                categoryID: '',
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
                        appName: '',
                        application: '',
                        u_unitcode: '',
                        u_auth_submit_time_start: $('#tbTimeStart').val(),
                        u_auth_submit_time_end: endTime,
                        categoryID: '',
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });
                });

                function UploadDll() {
                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd';
                    $('#wif')[0].src = url;
                    //$('#w').window('open');
                    var height = $(document).height() - 20;
                    var width = $(document).width() - 20;
                    $('#w').window({
                        width: width,
                        height: height
                    }).window('open').window('center');
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
    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:850px;height:520px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
