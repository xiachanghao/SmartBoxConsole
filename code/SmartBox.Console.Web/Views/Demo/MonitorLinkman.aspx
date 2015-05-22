<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	警报接收人
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:131px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>服务监控>>警报接收人</span>
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
                        名称：
                    </td>
                    <td>
                        <input id="lm_uname" name="lm_uname" type="text" />
                    </td>
                    <td>单位：</td>
                    <td><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'200',url:'<%=Url.Content("~/Demo/GetUnitData") %>',
    valueField:'Unit_ID',
    textField:'Unit_Name',onSelect: function(org){

        },onLoadSuccess:function() {
        }" name="state" style="width:184px;">
                        </select></td>
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
    <a id="A1" onclick="javascript:return MonitorLinkman();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a>
<a id="btn_upload" onclick="javascript:return MonitorLinkmanDeleteSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">删除</a>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetMonitorLinkmanList") %>',idField:'lm_id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb'">
				<thead>
					<tr>
                        <th data-options="field:'lm_id',checkbox:true"></th>						
                        <th data-options="field:'lm_uid'" width="40">接收人账号</th>
                        <th data-options="field:'lm_uname'" width="30">名称</th>
                        <th data-options="field:'lm_udept'" width="30">单位</th>
                        <th data-options="field:'lm_mobile'" width="30">手机号码</th>
                        <th data-options="field:'lm_email'" width="30">电子邮件</th>
						<th data-options="field:'lmid',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return MonitorLinkman('+value+');\'>修改</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return MonitorLinkmanDelete('+value+')\'>删除</a>' + 
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
                var sortStr = 'lm_id';

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var lm_uname = $('#lm_uname').val();
                    var lm_udept = $('#selUnit').combobox('getValue');

                    $('#grid').datagrid('load', {
                        lm_uname: lm_uname,
                        lm_udeptstring: lm_udept,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });
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

                function resetSearch() {
                    $('.table_box_data input').not('.btnskin_b').val('');
                    $('.table_box_data select').combobox('setValue', '')
                }

                function MonitorLinkman(id) {
                    if (id === undefined)
                        id = '';
                    var url = '<%=Url.Content("~/") %>Demo/MonitorLinkmanAdd?id=' + id;
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
                    var lm_uname = $('#lm_uname').val();
                    var lm_udept = $('#selUnit').combobox('getValue');

                    pager.pagination({
                        onSelectPage: function (pageNum, pageSize) {
                            page = pageNum;

                            var lm_uname = $('#lm_uname').val();
                            var lm_udept = $('#selUnit').combobox('getValue');

                            opts.pageNumber = pageNum;
                            opts.pageSize = pageSize;

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });

                            $('#grid').datagrid('load', {
                                lm_uname: lm_uname,
                                lm_udeptstring: lm_udept,
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
                        lm_uname: lm_uname,
                        lm_udeptstring: lm_udept,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

                    $('#grid').datagrid({
                        loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                //data.rows[i].df_issend = data.rows[i].df_issend == 'y' ? '已发送' : '未发送';
                                data.rows[i].lmid = data.rows[i].lm_id;
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

                function MonitorLinkmanDeleteSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的联系人吗?', function (r) {
                        if (r) {
                            var ids = '';


                            for (var i = 0; i < rows.length; ++i) {


                                ids += rows[i].lm_id;
                                if (i < rows.length - 1)
                                    ids += ',';

                            }


                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorLinkmanSelected") %>',
                                data: { ids: ids },
                                dataType: "json",
                                success: function (data) {
                                    var _msg = data.d;
                                    //if (not_del_cnt > 0)
                                    //    _msg = _msg + not_del_msg;
                                    $.messager.alert('提示', _msg, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });


                                }
                            });
                        }
                    });
                }

                function MonitorLinkmanDelete(id) {

                    $.messager.confirm('确认', '确定要删除所选择的联系人吗?', function (r) {
                        if (r) {

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorLinkman") %>',
                                data: { id: id },
                                dataType: "json",
                                success: function (data) {
                                    var _msg = data.d;
                                    $.messager.alert('提示', _msg, 'info', function () {
                                        var dg = $('#grid');
                                        $('#grid').datagrid('reload');
                                    });


                                }
                            });
                        }
                    });
                }
    </script>
    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
