<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	监控定义
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="_layout" class="easyui-layout" data-options="fit:true" style="">
    <div data-options="region:'north',split:false" style="height:197px;overflow:hidden;border:0px solid #DDDDDD;">
        <div class="easyui-panel cHead" data-options="" style="display:;font-size:12px;color:#528FB6;text-align: left; border:1px solid #DDDDDD;padding-left:5px;">
            <img src="../../themes/default/images/flexigrid/grid.png" /><span>服务监控>>监控定义</span>
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
                        发送开始时间：
                    </td>
                    <td>
                        <input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="    WdatePicker()"/>
                    </td>
                    <td>
                        对象：
                    </td>
                    <td>
                        <input id="tb_item" name="tb_item" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        监控类别：
                    </td>
                    <td>
                        <select id="sel_df_kind" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_df_kind" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="0">sbslink</option>
		                <option value="1">sbsmem</option>
                        </select>
                    </td>
                    <td>
                        代码：
                    </td>
                    <td>
                        <input id="tb_df_code" name="tb_df_code" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        是否已发送：
                    </td>
                    <td>
                        <select id="sel_issend" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_issend" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">已发送</option>
		                <option value="DISABLE">未发送</option>
                        </select>
                    </td>
                    <td>警报级别：</td>
                    <td><select id="sel_level" class="easyui-combobox" data-options="panelHeight:'auto'" name="sel_level" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="pdhbj">黄色</option>
		                <option value="pdgsj">蓝色</option>
                        <option value="pdgsj">红色</option>
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
    <a id="A1" onclick="javascript:return MonitorDefinedAdd();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a>
<a id="btn_upload" onclick="javascript:return MonitorDefinedDeleteSelected();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">删除</a>
</div>
     <table id="grid" class="easyui-datagrid" style="width:100%;border-top:1px solid #DDDDDD"
					data-options="url:'<%=Url.Content("~/Demo/GetMonitorDefinedList") %>',idField:'df_id',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:false,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,toolbar: '#tb'">
				<thead>
					<tr>
                        <th data-options="field:'df_id',checkbox:true"></th>						
                        <th data-options="field:'df_kind'" width="40">类别</th>
                        <th data-options="field:'df_item'" width="30">对象</th>
                        <th data-options="field:'df_maxvalue'" width="30">最大值</th>
                        <th data-options="field:'df_minvalue'" width="30">最小值</th>
                        <th data-options="field:'df_lever'" width="30">警报级别</th>
                        <th data-options="field:'df_code'" width="30">代码</th>
                        <th data-options="field:'df_sendway'" width="30">发送方式</th>
                        <th data-options="field:'df_receptman'" width="30">接收人</th>
                        <th data-options="field:'df_startsenddate'" width="30">发送开始时间</th>
                        <th data-options="field:'df_endsenddate'" width="30">发送结束时间</th>
                        <th data-options="field:'df_issend'" width="30">是否已发送</th>
						<th data-options="field:'dfid',formatter: function(value,row,index){
				return '<a href=\'#\' onclick=\'javascript:return MonitorDefinedAdd('+row.df_id+');\'>修改</a>' + 
                '&nbsp;<a href=\'#\' onclick=\'javascript:return MonitorDefinedDelete('+row.df_id+')\'>删除</a>' + 
                '';
			}" width="80">操作</th>
					</tr>
				</thead>
			</table>
    </div>
</div>
            <script type="text/javascript">
                var sortStr = 'df_id';

                function Search() {
                    var dg = $('#grid');
                    var opts = dg.datagrid('options');

                    var item = $('#tb_item').val();
                    var df_kind = $('#sel_df_kind').combobox('getValue');
                    var df_code = $('#tb_df_code').val();
                    var issend = $('#sel_issend').combobox('getValue');
                    var level = $('#sel_level').combobox('getValue');


                    var startTime = $('#tbTimeStart').val();
                    startTime = $.trim(startTime);
                    if (startTime != '') {
                        startTime = startTime + ' 23:59:59';
                    }

                    var endTime = $('#tbTimeEnd').val();
                    endTime = $.trim(endTime);
                    if (endTime != '') {
                        endTime = endTime + ' 23:59:59';
                    }

                    $('#grid').datagrid('load', {
                        df_kind: df_kind,
                        df_startsenddate_start: startTime,
                        df_startsenddate_end: endTime,
                        df_item: item,
                        df_lever: level,
                        df_code: df_code,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });
                }

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

                function OperateDll(pd_id, op) {
                    var url = '<%=Url.Content("~/") %>PushManage/JobCommandCtrl?pd_id=' + pd_id + '&operate=' + op;
                    $('#wif')[0].src = url;
                    $('#w').window('open');

                    return false;
                }
                function MonitorDefinedAdd(id) {
                    if (id === undefined)
                        id = '';
                    var url = '<%=Url.Content("~/") %>demo/MonitorDefinedAdd?df_id=' + id;
                    $('#wif')[0].src = url;
                    $('#w').window('open');

                    return false;
                }
                function MonitorDefinedDeleteSelected() {
                    var rows = $('#grid').datagrid('getChecked');
                    if (rows.length < 1)
                        return;
                    $.messager.confirm('确认', '确定要删除所勾选的项吗?', function (r) {
                        if (r) {
                            var ids = '';


                            for (var i = 0; i < rows.length; ++i) {


                                ids += rows[i].df_id;
                                if (i < rows.length - 1)
                                    ids += ',';

                            }


                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorDefinedSelected") %>',
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

                function MonitorDefinedDelete(id) {

                    $.messager.confirm('确认', '确定要删除所选择的项吗?', function (r) {
                        if (r) {

                            $.ajax({
                                type: "POST",
                                url: '<%=Url.Action("DelMonitorDefined") %>',
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


                var page = 0;
                $(document).ready(function () {
                    $('#btnSearch').bind('click', Search);
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    var item = $('#tb_item').val();
                    var df_kind = $('#sel_df_kind').combobox('getValue');
                    var df_code = $('#tb_df_code').val();
                    var issend = $('#sel_issend').combobox('getValue');
                    var level = $('#sel_level').combobox('getValue');


                    var startTime = $('#tbTimeStart').val();
                    startTime = $.trim(startTime);
                    if (startTime != '') {
                        startTime = startTime + ' 23:59:59';
                    }

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

                            var item = $('#tb_item').val();
                            var df_kind = $('#sel_df_kind').combobox('getValue');
                            var df_code = $('#tb_df_code').val();
                            var issend = $('#sel_issend').combobox('getValue');
                            var level = $('#sel_level').combobox('getValue');


                            var startTime = $('#tbTimeStart').val();
                            startTime = $.trim(startTime);
                            if (startTime != '') {
                                startTime = startTime + ' 23:59:59';
                            }

                            var endTime = $('#tbTimeEnd').val();
                            endTime = $.trim(endTime);
                            if (endTime != '') {
                                endTime = endTime + ' 23:59:59';
                            }

                            pager.pagination('refresh', {
                                pageNumber: pageNum,
                                pageSize: pageSize
                            });

                            $('#grid').datagrid('load', {
                                df_kind: df_kind,
                                df_startsenddate_start: startTime,
                                df_startsenddate_end: endTime,
                                df_item: item,
                                df_lever: level,
                                df_code: df_code,
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
                        df_kind: df_kind,
                        df_startsenddate_start: startTime,
                        df_startsenddate_end: endTime,
                        df_item: item,
                        df_lever: level,
                        df_code: df_code,
                        orderby: sortStr,
                        pageIndex: page,
                        pageSize: opts.pageSize
                    });

                    $('#grid').datagrid({
                        loadFilter: function (data) {
                            for (var i = 0; i < data.rows.length; ++i) {
                                data.rows[i].df_issend = data.rows[i].df_issend == 'y' ? '已发送' : '未发送';
                                data.rows[i].dfid = data.rows[i].df_id;
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
    <div id="w" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:493px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
