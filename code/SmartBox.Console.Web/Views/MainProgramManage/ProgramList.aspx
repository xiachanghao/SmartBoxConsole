<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主程序版本管理</title>
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
        $(document).ready(function() {
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetVersionTrackList")%>',
                colModel: [
                    { display: '版本号', name: 'VersionName', width: 100, sortable: false, align: 'left' },
                    { display: '文件路径', name: 'FilePath', width: 200, sortable: false, align: 'left' },
                    { display: '上一版本号', name: 'PreVersionId', width: 100, sortable: false, hide: true, align: 'left' },
                    { display: '状态', name: 'VersionStatus', width: 130, sortable: false, process: viewStatus, align: 'left' },
			        { display: '创建者', name: 'CreateUid', width: 100, sortable: false, align: 'left' },
			        { display: '创建时间', name: 'CreateTime', width: 130, sortable: false, align: 'left' },
			        { display: '操作', name: 'VersionStatus1', width: 70, sortable: false, process: Aclick, align: 'center' }

				],

                sortname: "CreateTime",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                rowhandler: contextmenu
            };

            var grid = $("#ManageVerList").flexigrid(option);

            function Aclick(pid, obj) {
                if (pid == "0")
                    return "<a   href=\"#\" onclick='openWindowsclick(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color:blue' >修改</a>";
                else if (pid == "1")
                    return "<a   href=\"#\" onclick='LoadWindowsclick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改配置</a>";
            }

            function viewStatus(pid, obj) {
                if (pid == "0")
                    return "未发布";
                else if (pid == "1")
                    return "正在使用";
                else if (pid == "2")
                    return "已过期";
            }

            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });

            function contextmenu(row) {
                var ch =  row.getAttribute("ch");
                var cell = ch.split("_FG$SP_");
                if (cell) {
                    kinds = cell[3];
                }
                if (kinds == 0)
                    $(row).find("td").css("background-color", "GreenYellow");
                else if (kinds == 1)
                    $(row).find("td").css("background-color", "LightBlue");
                else
                    $(row).find("td").css("background-color", "Silver");
            } //

            DisplayBtn();
        });                                              // end of ready

        function DisplayBtn() {
            if ('<%=ViewData["versionId"]%>' == "")
                $("#caltoolbar").show();
            else
                $("#caltoolbar").hide();
        }

        function toolbarItem_onclick(cmd, grid) {
            if (cmd == "Add") {
                window.location = '<%=Url.Action("UploadVersionFile") %>';
            }
        }  // end of toolbarItem_on 

        function flushGrid() {
            $("#ManageVerList").flexReload();
        } // end of flushGrid
        //修改
        function openWindowsclick(Id) {
            var url = '<%=Url.Action("UploadVersionFile") %>/' + escape(Id);
            window.location = url;
        } // contextMenuItem_click

        function LoadWindowsclick(Id) {
            var url = '<%=Url.Action("UpdateConfigInfo") %>/?verid=' + Id + '&IsLoadFile=1'; //是否单个上传文件
            window.location = url;
        }

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
       <%-- <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">主程序版本管理</span>
            </div>
        </div>--%>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='版本升级' class="Add">版本升级</span></div>
            </div>
        </div>
        <div>
            <table id="ManageVerList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
