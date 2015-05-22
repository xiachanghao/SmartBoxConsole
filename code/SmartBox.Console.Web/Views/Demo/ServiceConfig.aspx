<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	服务参数设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">    <div data-options="region:'north',split:false" style="height:131px;overflow:hidden;border:0px solid #DDDDDD;">        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>全局参数>>服务参数设置</span>
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
                        关键字：
                    </td>
                    <td>
                        <input id="tbUID" name="tbUID" type="text" />
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
<a id="btn_upload" onclick="javascript:return AddConfig('');" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a>
<a onclick="javascript:return DelSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
<a onclick="javascript:return ResetConfigs();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">重置客户端配置</a>
<a onclick="javascript:return ResetClientVer();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">重置客户端版本</a>
<%--<a onclick="javascript:return DeleteDll();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">删除用户</a>--%>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetServiceConfigList") %>',idField:'Key',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',
                    loadFilter: function (data) {  
                        for (var i = 0; i < data.rows.length; ++i) {  
                            data.rows[i].Key2 = data.rows[i].Key;
                            data.rows[i].Key3 = data.rows[i].Key;
                         }   
                        return data;  
                     },onSortColumn:function(sort, order) {
                        sort = sort.replace('2', '');
                        sortStr = sort + ' ' + order;
                        Search();
                     }">
				<thead>
					<tr>
                        <th data-options="field:'Key',checkbox:true"></th>
						<th data-options="field:'Key2'" width="30">关键字</th>
                        <th data-options="field:'Value'" width="50">值</th>
						<th data-options="field:'Key3',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return AddConfig(&quot;'+row.Key3+'&quot;);\'>修改</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return DelConfig(&quot;'+row.Key3+'&quot;)\'>删除</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RemoveJobPluginGroup&quot;)\'>载出</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RestartJobPluginGroup&quot;)\'>重载</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                //'&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '';
			}" width="10">操作</th>
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
                function DelSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的服务参数吗?', function (r) {
                        if (r) {
                            var ids = '';
                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].Key3;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelConfigs") %>',
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

                function ResetConfigs() {
                    $.messager.confirm('确认', '确定重置客户端配置吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("ResetConfigs") %>',
                                data: {  },
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

                function ResetClientVer() {
                    $.messager.confirm('确认', '确定重置客户端版本吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("ResetClientVer") %>',
                                data: {},
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

                function DelConfig(key) {
                    $.messager.confirm('确认', '确定删除' + key + '吗?', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelConfig") %>',
                                data: { key: key},
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

                function AddConfig(key) {
                    var url = '<%=Url.Content("~/") %>demo/serviceconfigadd?key=' + key;
                    $('#wif')[0].src = url;
                    $('#w').window('open');

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
                var sortStr = '[key] desc';
                $(document).ready(function () {
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');
                    
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
                                key: $('#tbUID').val(),
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
                        key: $('#tbUID').val(),
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
    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:194px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    function Search() {
        var dg = $('#grid');
        var opts = dg.datagrid('options');

        $('#grid').datagrid('load', {
            key: $('#tbUID').val(),
            orderby: sortStr,
            pageIndex: 0,
            pageSize: opts.pageSize
        });
    }
</script>
</asp:Content>
