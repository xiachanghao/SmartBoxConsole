<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>权限用户列表</title>
    <link rel="stylesheet" type="text/css" href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/themes/bootstrap/easyui.css">
	<link rel="stylesheet" type="text/css" href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/themes/icon.css">
	<%--<script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery.min.js"></script>--%>
    <script src="<%=Url.Content("~/") %>Javascripts/jquery-1.10.2.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <div class="easyui-panel" data-options="fit:true,border:false">
    <div data-options="region:'north',split:true" style="height:28px;overflow:hidden;border:0px solid #DDDDDD;">
    
        <div id="tb" style="text-align:right;">
            <a onclick="javascript:return ClearPrivilegeUser();" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">清除所有用户</a>
        </div>
    </div>
    <div data-options="region:'center'" style="width:100%;height:89%;">
        <table id="grid" class="easyui-datagrid" style="width:100%;"
					data-options="url:'<%=Url.Content("~/Applicationmanage/GetPrivilegeUser") %>',idField:'ID',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:true,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true">
				<thead>
					<tr>
                        <th data-options="field:'ID',checkbox:true"></th>
						<th data-options="field:'Uid'" width="80">姓名</th>
						<!--th data-options="field:'_uid'" width="100">账号</th>
                        <th data-options="field:'_sex'" width="100">性别</th-->
						<%--<th data-options="field:'UserEmail',align:'left'" width="80">邮件地址</th>
						<th data-options="field:'unitcost',align:'right'" width="80">Unit Cost</th>
						<th data-options="field:'attr1'" width="150">Attribute</th>
						<th data-options="field:'status',align:'center'" width="60">Status</th>--%>
					</tr>
				</thead>
			</table>
            </div>
    </div>
    <script type="text/javascript">

        var page = 0;
        function ClearPrivilegeUser() {
            var privilegecode = '<%=Request.QueryString["privilegecode"] %>';
            if (privilegecode !== undefined && privilegecode != '' && privilegecode != 'undefined') {
                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("ClearPrivilegeUser") %>',
                    data: { privilegecode: privilegecode },
                    dataType: "json",
                    success: function (data) {
                        $.messager.alert('提示', data.d, 'info', function () {
                        });

                        var dg = $('#grid');
                        $('#grid').datagrid('reload');
                    }
                });
            }
        }

        $(document).ready(function () {
            var dg = $('#grid');
            var pager = dg.datagrid('getPager');
            var opts = dg.datagrid('options');

            pager.pagination({
                onSelectPage: function (pageNum, pageSize) {
                    page = pageNum;
                    //alert(page);
                    opts.pageNumber = pageNum;
                    opts.pageSize = pageSize;

                    pager.pagination('refresh', {
                        pageNumber: pageNum,
                        pageSize: pageSize
                    });

                    $('#grid').datagrid('load', {
                        privilegeCode: '<%=Request.QueryString["privilegecode"]%>',
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
                privilegeCode: '<%=Request.QueryString["privilegecode"]%>',
                pageIndex: page,
                pageSize: opts.pageSize
            });
        });
            

            //            $('#grid').datagrid({
            //                queryParams: {
            //                    privilegeCode: '<%=Request.QueryString["privilegecode"]%>',
            //                    pageIndex: $('input.pagination-num').val(),
            //                    pageSize: pageSize
            //                },
            //                onLoadSuccess: function (data) {
            //                    var grid = $(this);
            //                },
            //                onBeforeLoad: function (parm) {
            //                    //                    var p = $('input.pagination-num').val();

            //                    //                    if (parm.page)
            //                    //                        parm.page = p;
            //                    //                    if (parm.pageIndex)
            //                    //                        parm.pageIndex = p;
            //                },
            //                onLoadError: function (obj, type, errmsg) {

            //                },
            //                onCheck: function (rowIndex, rowData) {

            //                },
            //                onUncheck: function (rowIndex, rowData) {

            //                },
            //                onSelect: function (rowIndex, rowData) {
            //                    if (item.length == 0 || item == null || undefined === item) {

            //                    }
            //                },
            //                onSelectAll: function (rows) {
            //                    if (rows.length > 0) {

            //                    }
            //                },
            //                onUnselectAll: function (rows) {
            //                    if (rows.length > 0) {

            //                    }
            //                }

            //            });

            //            $('#grid').datagrid('load', {
            //                privilegeCode: '<%=Request.QueryString["privilegecode"]%>',
            //                pageIndex: $('input.pagination-num').val(),
            //                pageSize: pageSize
            //            });
        
    </script>
</body>
</html>
