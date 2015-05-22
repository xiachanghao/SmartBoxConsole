<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	黑白名单管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<form action="/DeviceBindManage/ViewDeviceBind2">
    <div class="box_title">
    <div class="box_title_jswh">
        黑白名单管理
    </div>
        <div class="box_title_rightbar">
            
     <%--<a href="/Admin/Home/add" class="btnskin_b">添加</a>--%>

        </div>
    </div>

    <div class="table_box">
    <h4>查询条件</h4>
    <div class="table_toolbar">
        <%--<a href="#" class="icon_add" title="添加">&nbsp;</a>
        <a href="#" class="icon_add2" title="添加">&nbsp;</a>
        <a href="#" class="icon_close" title="关闭">&nbsp;</a>
        <a href="#" class="icon_close2" title="关闭">&nbsp;</a>
        <a href="#" class="icon_del" title="删除">&nbsp;</a>
        <a href="#" class="icon_edit" title="修改">&nbsp;</a>
        <a href="#" class="icon_list" title="列表">&nbsp;</a>
        <a href="#" class="icon_more" title="更多">&nbsp;</a>
        <a href="#" class="icon_set" title="设置">&nbsp;</a>
        <a href="#" class="icon_view" title="查看">&nbsp;</a>
        <a href="#" class="icon_viewcon" title="内容视图">&nbsp;</a>
        <a href="#" class="icon_viewimg" title="图片视图">&nbsp;</a>--%>
    </div>
    <div class="table_box_data">
        <table border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td>
                        用户标识：
                    </td>
                    <td>
                        <input name="uid" type="text" />
                    </td>
                    <td>
                        描述：
                    </td>
                    <td>
                        <input name="desc" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        状态：
                    </td>
                    <td>
                        <select id="selState" class="easyui-combobox" data-options="panelHeight:'auto'" name="state" style="width:184px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">已启用</option>
		                <option value="DISABLE">已禁用</option>
                        <option value="LOST">遗失</option>
                        </select>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <input type="submit" class="btnskin_b" value="查询" />
                            <input type="reset" class="btnskin_b" value="重置" />
                        </td>
                    </tr>
                </tfoot>
            </tbody>
        </table>
    </div>
</div>

<div style="height:10px;"></div>

    <div class="table_box" style="display:;">
    <h4><%--=(ViewData["mode"].ToString() == "black" ? "黑名单模式" : "白名单模式")--%>模式 <select id="selMode" data-options="panelHeight:'auto'" class="easyui-combobox" name="mode" style="width:70px;">
		                <option value="black">黑名单</option>
		                <option value="white">白名单</option>
                        </select></h4>
    <div class="table_toolbar">
        <a href="javascript:void(0);" onclick="javascript:return saveMode();" class="icon_save" title="保存">保存</a>
        <!--a href="" onclick="return false;" class="icon_del" title="删除">删除</a-->
    </div>
    <div class="table_box_data ">
        <table id="dataGrid" width="100%" border="0" cellspacing="0" cellpadding="0" class="tablefixed">
            <thead>
                <tr>
                    <th width="40">序号</th>
                    <th width="80">用户标识</th>
                    <th width="300">设备号</th>
                    <th width="80">状态</th>
                    <th>设备描述</th>
                    <th width="110">操作</th>
                </tr>
            </thead>
            <tbody>
                <tr id="trTemplate" style="display:none;">
                    <td align="left">{xuhao}</td>
                    <td align="left">{biaoshi}</td>
                    <td align="left" class="nowrap">{shebeihao}</td>
                    <td>{zhuangtai}</td>
                    <td>{miaoshu}</td>
                    <td valign="middle">
                        <a href="javascript:void(0)" onclick="javascript:return EnableClick('{id}',this);" class="atoolbar icon_unlock" style="display:{display_enable}" title="启用">启用</a>
                        <a href="javascript:void(0)" onclick="javascript:return DisEnableClick('{id}',this);" class="atoolbar icon_lock" style="display:{display_disable}" title="禁用">禁用</a>
                        <a href="javascript:void(0)" onclick="javascript:return SetLost('{id}',this);" class="atoolbar icon_lost" style="display:{display_lost}" title="遗失">遗失</a>
                        <%--<a href="/Admin/Home/Details" class="atoolbar icon_view" title="查看">查看</a>--%>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8">
                        <div  id="pager">
                            
                        </div>
                    </td>
                </tr>
            </tfoot>
        </table>

        
        
            
        
    </div>
</div>
</form>
<div style="display:none;">
<table class="easyui-datagrid" title="Basic DataGrid" style="display:none;width:700px;height:350px"
			data-options="collapsible:true,url:'/datagrid_data1.json',method:'get',rownumbers:true,checkOnSelect:true,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true">
		<thead>
			<tr>
				<th data-options="field:'itemid',width:80,checkbox:true">Item ID</th>
				<th data-options="field:'productid',width:100">Product</th>
				<th data-options="field:'listprice',width:80,align:'right'">List Price</th>
				<th data-options="field:'unitcost',width:80,align:'right'">Unit Cost</th>
				<th data-options="field:'attr1',width:250">Attribute</th>
				<th data-options="field:'status',width:60,align:'center'">Status</th>
			</tr>
		</thead>
	</table>
</div>

   <script type="text/javascript">
   function saveMode () {
                var selectMode = encodeURI($("input[name='mode']").val());

                var Mode = "白名单";
                if (selectMode == "black") Mode = "黑名单";

                //$("#loadingpannel").html("正在执行......").show();
                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("SetMode") %>',
                    data: { Mode: selectMode },
                    dataType: "json",
                    success: function (data) {
                        //$("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            //hiAlert("操作成功", "名单模式设置为" + Mode);
                            //flushGrid();
                            $.messager.alert('提示', "名单模式已设置为" + Mode,'info');
                            InitMode(Mode);
                        }
                        else {
                            //hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });                return false;
}

function InitMode(mode) {
            var selectValue = 'white';
            if (mode == '黑名单') selectValue = 'black';

            $("#selMode").attr("value", selectValue);
        }
   function EnableClick(id, o) {
            //$("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetStatus") %>',
                data: { id: id, Operation: "ENABLE" },
                dataType: "json",
                success: function (data) {
                    //$("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        $.messager.alert('提示', "操作成功",'info');
                        window.location.reload();
                        //flushGrid();
//                        var bgUrl = $(o).css('background-image');
//                        bgUrl = bgUrl.replace('btn_lock', 'btn_unlock');
//                        $(o).attr('title', '禁用').unbind('click').bind('click', function() {
//                            DisEnableClick(id, o);
//                        }).css('background-image', bgUrl);
                    }
                    else {
                        $.messager.alert('提示', "操作失败，可能的原因:\r\n" + data.Msg,'error');
                    }
                }
            });
            return false;
        }

        function DisEnableClick(id, o) {
            //$("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetStatus") %>',
                data: { id: id, Operation: "DISABLE" },
                dataType: "json",
                success: function (data) {
                    //$("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        $.messager.alert('提示', "操作成功",'info');
                        window.location.reload();
                        //flushGrid();
//                        $(o).attr('title', '启用').unbind('click').bind('click', function() {
//                            EnableClick(id, o);
//                        });
                    }
                    else {
                        $.messager.alert('提示', "操作失败，可能的原因:\r\n" + data.Msg,'error');
                    }
                }
            });
            return false;
        }

        function SetLost(id) {
            //$("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetStatus") %>',
                data: { id: id, Operation: "LOST" },
                dataType: "json",
                success: function (data) {
                    //$("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        $.messager.alert('提示', "操作成功",'info');
                        window.location.reload();
                        //flushGrid();
                    }
                    else {
                        $.messager.alert('提示', "操作失败，可能的原因:\r\n" + data.Msg,'error');
                        //hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            });
            return false;
        }
    var data = <%=ViewData["data"] %>;
       $(function () {
            bindGridData(data);
           $(".aBtnDelete").click(function () {
               $(this).parent().parent().remove();
           })

           var state = $.request.queryString('state');
           $('#selState').combobox('select', state);
           var mode = $.request.queryString('mode');
           if (mode == '')
            mode = 'white';
           $('#selMode').combobox('select', mode);
     
           $("#pager").pager({ align: "center", pageSize: 15, count: data.total, currentPageIndex: data.page - 1, type: "get", pageIndexParamName: "page", mode: 1 });
       })

       function bindGridData(data) {
        //alert(data.rows.length);
        if (data.total > 0)
        for (var i  = 0; i < data.rows.length; ++i) {
            var obj = data.rows[i];
            var cell = obj.cell;
            var tmp = $('#dataGrid tbody tr#trTemplate').html();
            tmp = tmp.replace('{biaoshi}', cell[1]);
            tmp = tmp.replace('{xuhao}', cell[6]);
            tmp = tmp.replace('{shebeihao}', cell[2]);
            tmp = tmp.replace('{id}', cell[5]);
            tmp = tmp.replace('{id}', cell[5]);
            tmp = tmp.replace('{id}', cell[5]);
            var sStatus = '';
            switch(cell[0]) {
                case 'LOST':
                sStatus = '遗失';
                    tmp = tmp.replace('{display_disable}', 'none');
                    tmp = tmp.replace('{display_enable}', 'none');
                    tmp = tmp.replace('{display_lost}', 'none');
                    break;
                case 'ENABLE':
                sStatus = '已启用';                
                    tmp = tmp.replace('{display_disable}', '');
                    tmp = tmp.replace('{display_enable}', 'none');
                    tmp = tmp.replace('{display_lost}', '');
                //continue;
                    break;
                case 'DISABLE':
                sStatus = '已禁用';
                    tmp = tmp.replace('{display_disable}', 'none');
                    tmp = tmp.replace('{display_enable}', '');
                    tmp = tmp.replace('{display_lost}', '');
                    break;
            }
            var sDesc = '';
            if (cell[4] !== undefined && cell[4] != null)
                sDesc = cell[4];
            tmp = tmp.replace('{zhuangtai}', sStatus);
            tmp = tmp.replace('{miaoshu}', sDesc);
            tmp = '<tr>' + tmp + '</tr>';
            $('#dataGrid tbody').append(tmp);
            switch(cell[0]) {
                case 'LOST':                
                    break;
                case 'ENABLE':
                    break;
                case 'DISABLE':
                    break;
            }
        }
       }
    </script>
    
    
    


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<link href="../../jquery-easyui-1.3.6/themes/bootstrap/datagrid.css" rel="stylesheet"
        type="text/css" />--%>
    <%--<link href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/themes/bootstrap/combobox.css" rel="stylesheet"
        type="text/css" />
    <link href="../../jquery-easyui-1.3.6/themes/bootstrap/pagination.css" rel="stylesheet"
        type="text/css" />--%>
</asp:Content>
