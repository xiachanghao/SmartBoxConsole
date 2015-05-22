<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>已绑定设备管理</title>
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
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>
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
    <script type="text/javascript">
        $(document).ready(function () {
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 280;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryDeviceBindList")%>',
                colModel: [
                    { display: '操作', name: 'Operate', width: 80, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        if (value == "ENABLE") {
                            op.push("<a onclick='DisEnableClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >禁用</a>");
                            op.push("<a onclick='SetLost(\"" + pid + "\")' style='cursor:pointer;color:blue' >遗失</a>");
                        }
                        else if (value == "DISABLE") {
                            op.push("<a onclick='EnableClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >启用</a>");
                            op.push("<a onclick='SetLost(\"" + pid + "\")' style='cursor:pointer;color:blue' >遗失</a>");
                        }
                        return op.join('&nbsp;')
                    }
                    },
                    { display: '用户标识', name: 'UserUid', width: 150, sortable: false, align: 'left' },
                    { display: '设备号', name: 'DeviceId', width: 320, sortable: false, align: 'left' },
                    { display: '状态', name: 'Status', width: 80, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        switch (value) {
                            case "ENABLE":
                                return "已启用";
                            case "DISABLE":
                                return "已禁用";
                            case "LOST":
                                return "遗失";
                            default:
                                return "";
                        }
                    }
                    },
                    { display: '设备描述', name: 'Description', width: 140, sortable: false, align: 'left' },
                    { display: 'ID', name: 'ID', width: 50, sortable: false, align: 'left', hide: true }
				],
                sortname: "UserUid",
                sortorder: "asc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };
            var grid = $("#ApplicationList").flexigrid(option);

            var theMode = document.getElementById("Mode").value;
            $("#selectMode").attr("value", theMode);

            $("span.Approve").click(function () {
                var UserId = encodeURI($("#UserId").val());
                var Description = encodeURI($("#Description").val());
                var selectStatus = encodeURI($("#selectStatus").val());
                searchurl = '<%=Url.Action("SearchDeviceBind") %>' + '?UserId=' + UserId + '&&Description=' + Description + '&&Status=' + selectStatus;

                SimplyButtons.init();
                var maiheight = document.documentElement.clientHeight;
                var mainWidth = document.documentElement.clientWidth - 5; // 减去边框和左边的宽度
                var otherpm = 210;
                var gh = maiheight - otherpm;
                var option = {
                    height: gh,
                    width: mainWidth,
                    url: searchurl,
                    colModel: [
                    { display: '操作', name: 'Operate', width: 100, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        if (value == "ENABLE") {
                            op.push("<a onclick='DisEnableClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >禁用</a>");
                            op.push("<a onclick='SetLost(\"" + pid + "\")' style='cursor:pointer;color:blue' >遗失</a>");
                        }
                        else if (value == "DISABLE") {
                            op.push("<a onclick='EnableClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >启用</a>");
                            op.push("<a onclick='SetLost(\"" + pid + "\")' style='cursor:pointer;color:blue' >遗失</a>");
                        } 

                        return op.join('&nbsp;')
                    }
                    },
                    { display: '用户标识', name: 'UserUid', width: 150, sortable: false, align: 'left' },
                    { display: '设备号', name: 'DeviceId', width: 200, sortable: false, align: 'left' },
                    { display: '状态', name: 'Status', width: 70, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        switch (value) {
                            case "ENABLE":
                                return "已启用";
                            case "DISABLE":
                                return "已禁用";
                            case "LOST":
                                return "遗失";
                            default:
                                return "";
                        }
                    }
                    },
                    { display: '设备描述', name: 'Description', width: 200, sortable: false, align: 'left' },
                    { display: 'ID', name: 'ID', width: 50, sortable: false, align: 'left', hide: true }
				],
                    sortname: "UserUid",
                    sortorder: "asc",
                    usepager: true,
                    rp: 15,
                    rowbinddata: true
                };
                //清空查询条件
                $("#UserId").val("");
                $("#Description").val("");
                $("#selectStatus").val("");
                var grid = $("#ApplicationList").flexOptions(option).flexReload();
            });


            $("span.btnreset").click(function () {
                var selectMode = encodeURI($("#selectMode").val());
                var Mode = "白名单";
                if (selectMode == "black") Mode = "黑名单";

                $("#loadingpannel").html("正在执行......").show();
                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("SetMode") %>',
                    data: { Mode: selectMode },
                    dataType: "json",
                    success: function (data) {
                        $("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            hiAlert("操作成功", "名单模式设置为" + Mode);
                            //flushGrid();
                            InitMode(Mode);
                        }
                        else {
                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });
            });


        });
        // end of ready

        function InitMode(mode) {
            var selectValue = 'white';
            if (mode == '黑名单') selectValue = 'black';

            $("#selectMode").attr("value", selectValue);
        }

        function SetLost(id) {
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetStatus") %>',
                data: { id: id, Operation: "LOST" },
                dataType: "json",
                success: function (data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        flushGrid();
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            });
        }

        function EnableClick(id) {
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetStatus") %>',
                data: { id: id, Operation: "ENABLE" },
                dataType: "json",
                success: function (data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        flushGrid();
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            });
        }

        function DisEnableClick(id) {
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetStatus") %>',
                data: { id: id, Operation: "DISABLE" },
                dataType: "json",
                success: function (data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        flushGrid();
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            });
        }

        function flushGrid() {
            $("#ApplicationList").flexReload();
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
    <div style="padding: 1px;"> 
        <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">黑白名单管理</span>
            </div>
        </div>      
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnQuery" class="fbutton">
                <div>
                    <span title='查询' class="Approve">查询</span></div>
            </div>
            <div style="padding-top:8px">
            &nbsp;&nbsp;&nbsp; <span>状态:</span>
            <select name="selectStatus" id="selectStatus">
                <option value="">请选择</option>
                <option value="ENABLE">已启用</option>
                <option value="DISABLE">已禁用</option>
                <option value="LOST">遗失</option>
            </select>
            <span>用户标识:</span>
            <input type="text" id="UserId" style="width: 80px" />
            <span>描述:</span>
            <input type="text" id="Description" style="width: 80px" />
            </div>
        </div>
        <div id="settoolbar" class="ctoolbar">
            <div id="btnSetMode" class="fbutton">
                <div>
                    <span title='保存' class="btnreset">保存</span></div>
            </div>
            <div style="padding-top:8px">
            &nbsp;&nbsp;&nbsp; <span>模式:</span><input id="Mode" value="<% =ViewData["Mode"]%>" type="hidden"/>     
            <select name="selectMode" id="selectMode">
                <option value="black">黑名单</option>
                <option value="white">白名单</option>
            </select>
            </div>
        </div>
        <div>
            <table id="ApplicationList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
