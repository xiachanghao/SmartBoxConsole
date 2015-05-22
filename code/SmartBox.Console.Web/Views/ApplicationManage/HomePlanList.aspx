<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HomePlanList</title>
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
                url: '<%=Url.Action("QueryHomePlanList")%>',
                colModel: [
                    { display: '操作', name: 'operate', width: 130, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<a onclick='SetHomePlanClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >设置布局</a>");
                        op.push("<a onclick='CopyHomePlanClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >复制</a>");
                        op.push("<a onclick='UpdateHomePlanClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >修改</a>");
                        op.push("<a onclick='DeleteHomePlanClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >删除</a>");
                        return op.join('&nbsp;')
                    }
                    },
                    { display: '布局标识', name: 'Code', width: 120, sortable: false, align: 'left' },
                    { display: '显示名称', name: 'DisplayName', width: 120, sortable: false, align: 'left' },
                    { display: '所有者', name: 'Owner', width: 200, sortable: false, hide: true, align: 'left' },
                    { display: '布局格式', name: 'Format', width: 50, sortable: false, align: 'left', process: function (value, pid) {
                        var format = value.split(',');
                        return format[0] + ' x '+ format[1];
                    }
                    },
                    { display: '默认布局', name: 'IsDefault', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                        if (value == "True") return "是";
                        else return "否";
                    }
                    },
                    { display: '创建者', name: 'CreateUid', width: 80, sortable: false, hide: true, align: 'left' },
                    { display: '创建时间', name: 'CreateTime', width: 65, sortable: false, hide: true, align: 'left', process: function (value, pid) {
                        return value.split(' ')[0];
                    }
                    },
                    { display: '更新者', name: 'UpdateUid', width: 80, sortable: false, align: 'left' },
                    { display: '更新时间', name: 'UpdateTime', width: 65, sortable: false, align: 'left', process: function (value, pid) {
                        return value.split(' ')[0];
                    }
                    }
				],
                usepager: true,
                rp: 15,
                rowbinddata: true
            };

            var grid = $("#HomePlanList").flexigrid(option);

            $("span.Add").click(function () {
                url = '<%=Url.Action("EditHomePlan") %>';
                OpenModelWindow(url, { width: 300, height: 200, caption: "新增布局", onclose: function () { $("#HomePlanList").flexReload(); } });
            });
            autosize_flexgrid("#HomePlanList");
            $(window).resize(function () {
                autosize_flexgrid("#HomePlanList");
            });
        });                                                       // end of ready

        function UpdateHomePlanClick(id) {
            url = '<%=Url.Action("EditHomePlan") %>/' + id;
            OpenModelWindow(url, { width: 300, height: 200, caption: "修改布局信息", onclose: function () { $("#HomePlanList").flexReload(); } });
        }

        function DeleteHomePlanClick(id) {
            hiConfirm("你确定要删除该布局吗？", "提示", function (btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在执行......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("DeleteHomePlan") %>',
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

        function CopyHomePlanClick(id) {
            url = '<%=Url.Action("CopyHomePlan") %>/' + id;
            OpenModelWindow(url, { width: 300, height: 200, caption: "复制布局信息", onclose: function () { $("#HomePlanList").flexReload(); } });
        }

        function SetHomePlanClick(id) {
            url = '<%=Url.Action("EditHomePlanDesign") %>?planID=' + id;
            OpenModelWindow(url, { width: 700, height: 430, caption: "设置布局", onclose: function () { $("#HomePlanList").flexReload(); } });
        }

        function flushGrid() {
            $("#HomePlanList").flexReload();
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
                <span id="departmentName">平板布局管理</span>
            </div>
        </div>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
        </div>
        <div>
            <table id="HomePlanList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
