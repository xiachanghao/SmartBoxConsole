<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>BUA用户同步至内网</title> 
    <%--<link rel="stylesheet" type="text/css" href="../../jquery-easyui-1.3.6/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../../jquery-easyui-1.3.6/themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../../jquery-easyui-1.3.6/themes/easyui.css">--%>

    <link rel="stylesheet" type="text/css" href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/themes/bootstrap/easyui.css">
	<link rel="stylesheet" type="text/css" href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/themes/icon.css">
	<%--<link rel="stylesheet" type="text/css" href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/demo.css">--%>
	<%--<script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery.min.js"></script>--%>
    <script src="<%=Url.Content("~/") %>Javascripts/jquery-1.10.2.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/locale/easyui-lang-zh_CN.js"></script>

    <style>
    #layout 
    {
        width:1000px;
        height:600px;
    }
    #btnSync 
    {
        margin-top:4px;
        margin-left:5px;
    }
    body 
    {
        margin:5px;
    }
    </style>
    <%--<script type="text/javascript" src="../../jquery-easyui-1.3.6/jquery.min.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/jquery.easyui.min.js"></script>--%>
</head>
<body>
	<div id="_layout" class="easyui-layout" data-options="fit:true" style="width:100%;height:350px;">
		<div data-options="region:'north'" style="width:100%;height:40px;vertical-align:middle;line-height:27px;padding-left:5px;">BUA用户同步至移动平台<a id="btnSync" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'">同步</a></div>
		<%--<div data-options="region:'south',split:true" style="height:40px;width:100%;">
            
        </div>--%>
		<div data-options="region:'east',split:true" title="已选择" style="width:180px;">
            <table id="gridSelected" class="easyui-datagrid" title=""
			data-options="fit:true,rownumbers:true,singleSelect:true,method:'get',toolbar:toolbar_gridSelected,singleSelect:false,border:false,loadMsg:'正在加载...',">
		        <thead>
			        <tr>
				        <th data-options="field:'UserFullName',width:100">姓名</th>
			        </tr>
		        </thead>
	        </table>
        </div>
		<div data-options="region:'west',split:true" title="单位" style="width:180px;">
            <div class="easyui-panel" style="padding:5px;border-width:0px;" data-options="fit:true">
		        <ul id="unit_tree" class="easyui-tree" data-options="url:'<%=Url.Content("~/authmanage/GetSyncBUAUnitTree") %>',method:'get',border:false,animate:true,lines:true,fit:true,fitColumns:true"></ul>
	        </div>
        </div>
		<div data-options="region:'center',title:'用户列表',iconCls:'icon-ok'">
			<table id="grid" class="easyui-datagrid" style="width:100%;"
					data-options="url:'<%=Url.Content("~/authmanage/GetSyncBUAUser") %>',idField:'UserUID',method:'get',displayMsg:'当前{from}-{to}，共 {total}',loadMsg:'正在加载...',rownumbers:true,checkOnSelect:true,selectOnCheck:true,singleSelect:false,pagination:true,border:false,fit:true,fitColumns:true,
                    onLoadSuccess: function (data) {
                    var grid = $(this);
                    for (var i = 0; i < data.rows.length; ++i) {
                        data.rows[i]._uid = data.rows[i].UserUid;
                        data.rows[i]._sex = data.rows[i].UserSex == 1 ? '男' : '女';
                    }
                }">
				<thead>
					<tr>
                        <th data-options="field:'UserUID',checkbox:true"></th>
						<th data-options="field:'UserFullName'" width="80">姓名</th>
						<!--th data-options="field:'_uid'" width="100">账号</th>
                        <th data-options="field:'_sex'" width="100">性别</th-->
						<th data-options="field:'UserEmail',align:'left'" width="80">邮件地址</th>
						<%--<th data-options="field:'unitcost',align:'right'" width="80">Unit Cost</th>
						<th data-options="field:'attr1'" width="150">Attribute</th>
						<th data-options="field:'status',align:'center'" width="60">Status</th>--%>
					</tr>
				</thead>
			</table>
            
		</div>
	</div>
    <script type="text/javascript">
        var toolbar_gridSelected = [{
            text: '删除',
            iconCls: 'icon-cut',
            handler: function () {

                var rows = $('#gridSelected').datagrid('getSelections');
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    _data = remove(_data, 'UserUid', row.UserUid);
                }

                $('#gridSelected').datagrid({
                    data: _data
                });
            }
        }];

        var _data = [];
        /**
        * 从对象数组中删除属性为objPropery，值为objValue元素的对象
        * @param Array arrPerson 数组对象
        * @param String objPropery 对象的属性
        * @param String objPropery 对象的值
        * @return Array 过滤后数组
        */
        function remove(arrObject, objPropery, objValue) {
            return $.grep(arrObject, function (cur, i) {
                return cur[objPropery] != objValue;
            });
        }
        /**
        * 从对象数组中获取属性为objPropery，值为objValue元素的对象
        * @param Array arrPerson 数组对象
        * @param String objPropery 对象的属性
        * @param String objPropery 对象的值
        * @return Array 过滤后的数组
        */
        function get(arrObject, objPropery, objValue) {
            return $.grep(arrObject, function (cur, i) {
                return cur[objPropery] == objValue;
            });
        }
        /**
        * 显示对象数组信息
        * @param String info 提示信息
        * @param Array arrPerson 对象数组
        */
        function showArrayInfo(info, arrObject) {
            alert($.version);
            $.each(arrObject, function (index, callback) {
                info += "Person id:" + arrObject[index].id + " name:" + arrObject[index].name + " sex:" + arrObject[index].sex + " age:" + arrObject[index].age + "\r\t";
            });
            alert(info);
        }

        var selectedOrgCode = '';
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

                    if (selectedOrgCode != '') {
                        $('#grid').datagrid('load', {
                            orgCode: selectedOrgCode,
                            pageIndex: page - 1,
                            pageSize: opts.pageSize
                        });
                    }
                    pager.pagination('refresh', {
                        pageNumber: page,
                        pageSize: pageSize
                    });
                },

                //pageList: [10, 30, 50, 100], //可以设置每页记录条数的列表           
                beforePageText: '第', //页数文本框前显示的汉字           
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
            });

            $('#btnSync').click(function () {
                $.messager.progress();
                //$('#w2').window('open');
                var _ids = '';
                $.each(_data, function (index, callback) {
                    _ids += _data[index].UserUid + ",";
                });
                $.ajax({
                    type: "POST",
                    url: '<%=Url.Content("~/")%>authmanage/SyncBUAUserToInside',
                    data: { ids: _ids },
                    dataType: "json",
                    success: function (data) {
                        $.messager.progress('close');
                        //$('#w2').window('close');
                        if (data.IsSuccess) {
                            //$.messager.alert('提示', data.Msg, 'info');
                        }
                        else {
                            var msg = "操作失败，可能的原因:\r\n" + data.Msg;
                            //$.messager.alert('提示', msg, 'info');
                        }
                        //$('#w').window('open');
                        $.messager.confirm('提示', data.Msg + '是否查看同步结果?', function (r) {
                            if (r) {
                                window.location = '<%=Url.Content("~/")%>Applicationmanage/AsyncResultListBUAUserToInside';
                            }
                        });
                    }, error: function (data) {
                        //$('#w2').window('close');
                        $.messager.progress('close');
                        $.messager.alert('提示', data.statusText, 'info', function () {
                        });
                    }
                });
            });
            $('#unit_tree').tree({
                onClick: function (node) {
                    //var pager = $('#grid').datagrid().datagrid('getPager');

                    //var pageSize = $('.layout-panel-center .pagination-page-list').val();
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    $('#grid').datagrid('load', {
                        orgCode: node.id,
                        pageIndex: 1, //$('input.pagination-num').val(),
                        pageSize: opts.pageSize
                    });
                    selectedOrgCode = node.id;
                }
            });

            // when double click a cell, begin editing and make the editor get focus
            $('#grid').datagrid({
                onLoadSuccess: function () {
                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                },
                onBeforeLoad: function (parm) {
                    var p = $('input.pagination-num').val();

                    if (parm.page)
                        parm.page = p;
                    if (parm.pageIndex)
                        parm.pageIndex = p;

                    var dg = $('#grid');
                    var pager = dg.datagrid('getPager');
                    var opts = dg.datagrid('options');

                    parm.pageSize = opts.pageSize;
                },
                onUncheck: function (rowIndex, rowData) {
                    _data = remove(_data, 'UserUid', rowData.UserUid);
                    $('#gridSelected').datagrid({
                        data: _data
                    });
                    //$.messager.alert('提示', _data.length, 'error');
                },
                onSelect: function (rowIndex, rowData) {
                    var item = get(_data, 'UserUid', rowData.UserUid);
                    if (item.length == 0 || item == null || undefined === item) {
                        _data.push(rowData);
                        $('#gridSelected').datagrid({
                            data: _data
                        });
                    }
                },
                onSelectAll: function (rows) {
                    if (rows.length > 0) {
                        for (var i = 0; i < rows.length; ++i) {
                            var item = get(_data, 'UserUid', rows[i].UserUid);
                            if (item.length == 0 || item == null || undefined === item) {
                                _data.push(rows[i]);
                            }
                        }
                        $('#gridSelected').datagrid({
                            data: _data
                        });
                    }
                },
                onUnselectAll: function (rows) {
                    if (rows.length > 0) {
                        for (var i = 0; i < rows.length; ++i) {
                            _data = remove(_data, 'UserUid', rows[i].UserUid);
                        }

                        $('#gridSelected').datagrid({
                            data: _data
                        });
                    }
                }

            });


        });
    </script>
    <div id="w2" class="easyui-window" title="&nbsp;" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){ return false}" style="width:150px;height:70px;padding:3px;">
		<img src="../../Images/loading.gif" />处理中,请等待...
	</div>
    <div id="w" class="easyui-window" title="BUA用户同步至内网结果查看" data-options="modal:true,closed:true,iconCls:'icon-save'" style="width:500px;height:200px;padding:10px;">
    <div id="content" class="easyui-panel" style="height:200px"
data-options="href:'http://www.baidu.com'">
</div>
	</div>
</body>
</html>
