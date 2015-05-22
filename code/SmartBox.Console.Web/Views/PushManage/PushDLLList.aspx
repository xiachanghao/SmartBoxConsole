<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PushDLLList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--<div class="easyui-panel" data-options="" style="display:none;text-align: center; border-width:1px; margin: 0 auto;">
        <div class="easyui-panel cHead" data-options="" style="text-align: center; border-width:0px; margin: 0 auto;">
        <div class="ftitle">推送插件管理</div>
        </div>
    </div>--%>
    <div id="tb">
<a id="btn_upload" onclick="javascript:return UploadDll();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增插件</a>
<a onclick="javascript:return DeleteDll();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除插件</a>
</div>
     <table id="grid" title="推送插件列表" class="easyui-datagrid" style="width:100%;"
					data-options="url:'<%=Url.Content("~/pushmanage/GetPushDLLList") %>',idField:'pd_id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb'">
				<thead>
					<tr>
                        <th data-options="field:'pd_id',checkbox:true"></th>
						<th data-options="field:'pd_name'" width="50">姓名</th>
                        <th data-options="field:'pd_path'" width="180">路径</th>
                        <th data-options="field:'pd_dll_status'" width="30">状态</th>
						<th data-options="field:'pdid',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return ModifyEntity('+value+');\'>修改</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;AddJobPluginGroup&quot;)\'>载入</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RemoveJobPluginGroup&quot;)\'>载出</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;RestartJobPluginGroup&quot;)\'>重载</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;SetTargetJobTime&quot;)\'>运行时间</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;PauseTargetJob&quot;)\'>暂停</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return OperateDll('+value+',&quot;ContinueTargetJob&quot;)\'>继续</a>';
			}" width="80">操作</th>
						<%--<th data-options="field:'unitcost',align:'right'" width="80">Unit Cost</th>
						<th data-options="field:'attr1'" width="150">Attribute</th>
						<th data-options="field:'status',align:'center'" width="60">Status</th>--%>
					</tr>
				</thead>
			</table>

            <script type="text/javascript">
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
                                //privilegeCode: '<%=Request.QueryString["privilegecode"]%>',
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
                        //privilegeCode: '<%=Request.QueryString["privilegecode"]%>',
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

                    $('#grid').datagrid({
                        loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                data.rows[i].pd_status_text = data.rows[i].pd_status ? '已启用' : '已禁用';
                                data.rows[i].pdid = data.rows[i].pd_id;
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
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
