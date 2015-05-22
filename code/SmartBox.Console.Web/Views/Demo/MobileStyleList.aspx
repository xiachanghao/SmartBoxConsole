<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	移动界面布局
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:171px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>界面>>移动界面布局</span>
        </div>
        <div style="height:7px;display:;"></div>

    <div class="table_box" style="display:;">
    <h4>查询条件</h4>
    <div class="table_toolbar">
        
    </div>
    <div class="table_box_data">
        <table border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td>
                        代码：
                    </td>
                    <td>
                        <input id="tbCode" name="tbCode" type="text" />
                    </td>
                    <td>
                        显示名称：
                    </td>
                    <td>
                        <input id="tbDisplayName" name="tbDisplayName" type="text" />
                    </td>
                </tr>
                <tr>                    
                    <td>客户端类型：</td>
                    <td><select id="selClientType" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="Pad/Android">Pad/Android</option>
		                <option value="Pad/iOS">Pad/iOS</option>
                        <option value="Phone/Android">Phone/Android</option>
                        <option value="Phone/Android">Phone/iOS</option>
                        </select>
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
<a id="btn_upload" onclick="javascript:return DeleteSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">删除</a>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetMobileStyleList") %>',idField:'ID',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb',loadFilter: function (data) { for (var i = 0; i < data.rows.length; ++i) {  data.rows[i]._ID = data.rows[i].ID;  }    return data; }">
				<thead>
					<tr>
                        <th data-options="field:'ID',checkbox:true"></th>
						<th data-options="field:'Code'" width="50">代码</th>
                        <th data-options="field:'DisplayName'" width="50">显示名称</th>
                        <th data-options="field:'ClientType'" width="120">客户端类型</th>
						<th data-options="field:'_ID',formatter: function(value,row,index){
				//return '<a href=\'#\' onclick=\'javascript:return ModifyEntity('+value+');\'>修改</a>' + 
                return '<a href=\'#\' onclick=\'javascript:return DelItem('+value+')\'>删除</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return AddStyleItem('+value+')\'>添加布局设置</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return ViewItemList('+value+')\'>查看布局设置列表</a>' + 
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
                function ModifyEntity(pd_id) {
                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd?pd_id=' + pd_id;
                    $('#wif')[0].src = url;
                    $('#w').window('open');
                    return false;
                }

                function AddStyleItem(id) {
                    var url = '<%=Url.Content("~/") %>Demo/MobileStyleHomeItemAdd?App4AIID=' + id + '&StyleID=0';
                    $('#wif')[0].src = url;
                    $('#w').window('open');
                    return false;
                }

                function ViewItemList(id) {
                    var url = '<%=Url.Content("~/") %>Demo/MobileStyleHomeItemList?id=' + id + '&appId=';
                    $('#wif')[0].src = url;
                    $('#w').window('open');
                    return false;
                }

                function DelItem(Id) {
                    alert('Del');
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("StyleDeleteItem") %>',
                        data: { id: Id },
                        dataType: "json",
                        success: function (data) {
                            $.messager.alert('提示', data.d, 'info', function () {
                                var dg = $('#grid');
                                $('#grid').datagrid('reload');
                            });                            
                        }
                    });
                }

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var clientTypeCode = $('#selClientType').combobox('getValue');
                    var clientTypeText = $('#selClientType').combobox('getText');

                    $('#grid').datagrid('load', {
                        pageIndex: 0,
                        pageSize: opts.pageSize,
                        clientType: clientTypeCode,
                        code:$('#tbCode').val().trim(),
                        displayName: $('#tbDisplayName').val().trim()
                    });
                }

                var page = 0;
                $(document).ready(function () {
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    pager.pagination({
                        onSelectPage: function (pageNum, pageSize) {
                            page = pageNum;
                            //alert(pageNum);
                            opts.pageNumber = pageNum;
                            opts.pageSize = pageSize;

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });

                            $('#grid').datagrid('load', {
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
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

                    /*$('#grid').datagrid({
                    loadFilter: function (data) {
                    for (var i = 0; i < data.rows.length; ++i) {
                    data.rows[i]._ID = data.rows[i].ID;
                    }
                    return data;
                    }
                    });*/

                });

//                function UploadDll() {
//                    var url = '<%=Url.Content("~/") %>PushManage/PushDllAdd';
//                    $('#wif')[0].src = url;
//                    $('#w').window('open');
//                    return false;
//                }

                function CloseWind(refreshGrid) {
                    if (refreshGrid) {
                        var dg = $('#grid');
                        $('#grid').datagrid('reload');
                    }
                    $('#w').window('close');
                }

                function DeleteSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的插件吗?', function (r) {
                        if (r) {
                            var ids = '';

                            for (var i = 0; i < rows.length; ++i) {
                                ids += rows[i].ID;
                                if (i < rows.length - 1)
                                    ids += ',';
                            }
                            //not_del_msg += '未从推送服务里载出，不能删除！';

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("StyleDeleteItems") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    $.messager.alert('提示', data.d, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    }); 
                                }
                            });
                        }
                    });



                }
    </script>
    <div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:740px;height:490px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
