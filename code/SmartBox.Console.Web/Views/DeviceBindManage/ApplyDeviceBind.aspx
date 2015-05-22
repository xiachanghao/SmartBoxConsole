<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>申请设备绑定</title>
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
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>
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
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryApplyDeviceBindList")%>',
                colModel: [
                    { display: '操作', name: 'operate', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<a onclick='ApproveClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >通过</a>");
                        op.push("<a onclick='RejectClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >拒绝</a>");
                        return op.join('&nbsp;')
                    }
                    },
                    { display: '用户标识', name: 'UserUid', width: 120, sortable: false, align: 'left' },
                    { display: '设备号', name: 'DeviceId', width: 220, sortable: false, align: 'left' },
                    { display: '申请IP地址', name: 'Ip', width: 200, sortable: false, align: 'left' },
                    { display: '审核状态', name: 'Status', width: 80, sortable: false, align: 'left', process: function (value, pid) {
                        return "待审批";
                    }
                    },
                    { display: '申请时间', name: 'ApplyTime', width: 65, sortable: false, align: 'left', process: function (value, pid) {
                        return value.split(' ')[0];
                    }
                    },
                    { display: '设备描述', name: 'Description', width: 200, sortable: false, align: 'left' },
                    { display: 'ID', name: 'ID', width: 50, sortable: false, align: 'left', hide: true }
				],
                sortname: "ApplyTime",
                sortorder: "asc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };

            var grid = $("#ApplicationList").flexigrid(option);
            autosize_flexgrid("#ApplicationList", 106);
            $(window).resize(function () {
                autosize_flexgrid("#ApplicationList", 106);
            });
        });                                                      
        // end of ready

        function ApproveClick(id) {
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("ApproveApply") %>',
                data: { id: id, Operation: "1" },
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
            });
        }

        function RejectClick(id) {
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("ApproveApply") %>',
                data: { id: id, Operation: "2" },
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
                <span id="departmentName">设备绑定审核</span>
            </div>
        </div>  
        <div style="height:10px;">
        </div>      
        <div>
            <table id="ApplicationList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
