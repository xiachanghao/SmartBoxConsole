<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>权限管理</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/flexigrid.css") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/contextmenu.css") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/autocomplete.css")%>" rel="stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/simplybuttons.css")%>" rel="stylesheet"
        type="text/css" />
    <%--<script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>--%>
    <%--<script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery.min.js"></script>--%>
    <script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery-1.7.2.min.js"></script>
    
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.autocomplete.js")%>" type="text/javascript"
        defer="defer"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>

    <link rel="stylesheet" type="text/css" href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/themes/bootstrap/easyui.css">
	<%--<link rel="stylesheet" type="text/css" href="<%=Url.Content("~/") %>jquery-easyui-1.3.6/themes/icon.css">--%>

	
	<script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/locale/easyui-lang-zh_CN.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {            
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 5; // 减去边框和左边的宽度
            var otherpm = 152;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: '100%',
                url: '<%=Url.Action("QueryPrivilegeList")%>',
                colModel: [
                    { display: 'ID', name: 'PublishId', width: 70, sortable: false, align: 'left' },
                    { display: '名称', name: 'Name', width: 120, sortable: false, align: 'left' },
                    { display: '显示名称', name: 'DisplayName', width: 120, sortable: false, align: 'left' },
                    { display: '对应BUA的应用系统标识', name: 'BuaAppCode', width: 120, sortable: false, hide: true, align: 'left' },
                    { display: '对应的Bua的权限标识', name: 'BuaPrivilegeCode', width: 120, sortable: false, hide: true, align: 'left' },
                    { display: '启用同步', name: 'EnableSync', width: 30, sortable: false, align: 'left', process: function (value, pid) {
                        if (value == "True") return "是";
                        else return "否";
                    }
                    },
                    { display: '同步的间隔时间', name: 'Description', width: 30, sortable: false, align: 'left' },
                    { display: '创建时间', name: 'CreateTime', width: 120, sortable: false, align: 'left' },
                    { display: '操作', name: 'Operate', width: 140, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<a onclick='UpdatePrivilegeClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >修改</a>");
                        op.push("<a onclick='DeletePrivilegeClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >删除</a>");
                        op.push("<a onclick='AsyncPrivilegeClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >同步</a>");
                        op.push("<a onclick='return PrivilegeUserClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >查看用户</a>");
                        return op.join('&nbsp;');
                    }
                    }
				],
                sortname: "CreateTime",
                sortorder: "asc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };

            var grid = $("#PrivilegeList").flexigrid(option);

            $("span.Add").click(function () {
                url = '<%=Url.Action("EditAppPrivilege") %>';
                OpenModelWindow(url, { width: 400, height: 300, caption: "新增权限", onclose: function () { $("#PrivilegeList").flexReload(); } });
            });

            $("span.btnreset").click(function () {
                AsyncPrivilegeClick('all');
            });

            autosize_flexgrid("#PrivilegeList");
            $(window).resize(function () {
                autosize_flexgrid("#PrivilegeList");
            });
        });
        // end of ready
        function PrivilegeUserClick(id) {
            var url = '<%=Url.Content("~/") %>applicationmanage/privilegeuser?privilegecode=' + id;
            $('#wif')[0].src = url;
            $('#w').window('open');
            return false;
        }

        function AsyncPrivilegeClick(id) {            
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("AsyncPrivilege") %>',
                data: { id: id },
                dataType: "json",
                success: function (data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        hiAlert("操作成功", "同步完成");
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            });
        }

        function UpdatePrivilegeClick(id) {
            url = '<%=Url.Action("EditAppPrivilege") %>/'+id;
            OpenModelWindow(url, { width: 400, height: 300, caption: "修改权限", onclose: function () { $("#PrivilegeList").flexReload(); } });
        }

        function DeletePrivilegeClick(id) {
            hiConfirm("你确定要删除该权限吗？", "提示", function (btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在执行......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("DeleteAppPrivilege") %>',
                        data: { id: id },
                        dataType: "json",
                        success: function (data) {
                            $("#loadingpannel").hide();
                            if (data.IsSuccess) {
                                hiAlert("操作成功", true);
                                flushGrid();
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                            }
                        }
                    }); //end if $.ajax
                }
            });
        }

        function flushGrid() {
            $("#PrivilegeList").flexReload();
        } // end of flushGrid
        //查看详细信息

    </script>
    <style type="text/css">
        .tdlabel
        {
            width: 12%;
            text-align: right;
        }
        .tdinput
        {
            width: 30%;
        }
        .tdquery
        {
            text-align: center;
        }
        .qclass
        {
            border: solid 1px #99bbe8;
            border-top: none;
        }
        .qclass input
        {
            width: 90%;
            border: solid 1px #ccc;
        }
        
        input.autocomplete
        {
            border: solid 1px #99bbe8;
        }
        tr.trtop td
        {
            border-top: solid 1px #ccc;
        }
        td.querytd
        {
            border-left: solid 1px #ccc;
            text-align: center;
        }
        .bspan
        {
            background-color: #E8F1F8 !important;
        }
    </style>
</head>
<body>
<div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
    <div class="easyui-panel" data-options="fit:true" style="padding: 1px;border-width:0px;">
        <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">应用权限管理 </span>
            </div>
        </div>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
             <div id="btnSetMode" class="fbutton">
                <div>
                    <span title='权限同步' class="btnreset">权限同步</span></div>
            </div>
        </div>
        <div>
            <table id="PrivilegeList" style="display: none;">
            </table>
        </div>
    </div>
    <div id="w" class="easyui-window" title="查看用户" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false" style="width:700px;height:400px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
</body>
</html>
