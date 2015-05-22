<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>子系统访问量统计</title>
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
    <%--<link href="<%=Url.Content("~/Themes/Default/jquery-ui.css") %>" rel="stylesheet" type="text/css" />--%>
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery-1.8.3.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.autocomplete.js")%>" type="text/javascript"
        defer="defer"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery-ui.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ui.datepicker-zh-CN.js")%>"
        type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <script type="text/javascript">
//        $(function () {
//            $("#startDate").datepicker();
//            $("#endDate").datepicker();
//        });

        $(document).ready(function () {
            SimplyButtons.init();
            var al = '<% =ViewData["AppNameList"] %>'.split(','); //app
            var adl = '<% =ViewData["AppDisplayNamelist"] %>'.split(','); //appDisplayName
            var appNum = al.length;
            var colModelvalue = '[' + '\n';
            colModelvalue += '{ display: \'用户名\', name: \'UserName\', width: 90, sortable: false, align: \'left\' },' + '\n';
            colModelvalue += '{ display: \'所属单位\', name: \'UnitName\', width: 80, sortable: false, align: \'left\' },' + '\n';
            for (var i = 0; i < al.length; i++) {
                var _width = 50;
                if (adl[i].length > 4 && adl[i].length <=6)
                    _width = 70;
                else if (adl[i].length > 6)
                    _width = 90;
                if (i < al.length - 1) {
                    colModelvalue += '{ display: \'' + adl[i] + '\', name: \'' + al[i] + '\', width: ' + _width + ', sortable: false, align: \'left\' },' + '\n';
                } else {
                    colModelvalue += '{ display: \'' + adl[i] + '\', name: \'' + al[i] + '\', width: ' + _width + ', sortable: false, align: \'left\' }' + '\n';
                }
            }
            colModelvalue += ']';
            var mod = eval(colModelvalue); //转为json对象
            //alert(colModelvalue);

            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 5; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryAppName")%>',
                colModel: mod,
                sortname: "UnitName",
                sortorder: "asc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };

            var grid = $("#ShowList").flexigrid(option);
            autosize_flexgrid("#ShowList", 106);
            $(window).resize(function () {
                autosize_flexgrid("#ShowList", 106);
            });

            $("span.Approve").click(function () {
                var userName = $("#userName").val();
                url = '<%=Url.Action("SearchShow") %>' + '?userName=' + userName;
                window.location.href = url;
            });
        });                         // end of ready

        function flushGrid() {
            $("#WebApplicationList").flexReload();
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
                <span id="departmentName">子系统访问量统计</span>
            </div>
        </div>
        <div style="height:10px;"></div>
        <%--<div id="caltoolbar" class="ctoolbar">
            <div id="btnQuery" class="fbutton">
                <div>
                    <span title='查询' class="Approve">查询</span></div>
            </div>
            &nbsp;&nbsp;&nbsp;&nbsp 用户名:
            <input type="text" id="userName" />
            访问时间:
            <input type="text" id="startDate" />
            至
            <input type="text" id="endDate" />
        </div>--%>
        <div>
            <table id="ShowList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
