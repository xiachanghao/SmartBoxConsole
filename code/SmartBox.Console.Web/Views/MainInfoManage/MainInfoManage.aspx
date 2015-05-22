<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主程序管理</title>
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
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>

    <% if (false)
       { %>

    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <%} %>

    <script type="text/javascript">
        var maiheight = document.documentElement.clientHeight;
        var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
        $(document).ready(function () {
            var type = '<% = ViewData["clientType"] %>';          
            SimplyButtons.init();
            $("#btnDelete").hide();
            $("#btnAdd").show();
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetMiansInfo")%>?type=' + type,
                colModel: [
                    { display: '版本', name: 'Version', width: 90, sortable: false, align: 'left' },
                    { display: '版本说明', name: 'VersionSummary', width: 180, sortable: false, align: 'left' },
			        { display: '状态', name: 'VersionStatus1', width: 90, sortable: false, process: GetName, align: 'left' },
			        { display: '发布者', name: 'CreateUid', width: 80, sortable: false, align: 'left' },
			        { display: '发布时间', name: 'CreateTime', width: 150, sortable: false, align: 'left' },
			        { display: '操作', name: 'VersionStatus', width: 160, sortable: false, process: Aclick, align: 'center' }
				],

                sortname: "CreateTime",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                rowhandler: contextmenu
            };
            var grid = $("#ManageVerList").flexigrid(option);
            autosize_flexgrid("#ManageVerList");
            $(window).resize(function () {
                autosize_flexgrid("#ManageVerList");
            });

            function GetName(pid, obj) {
                if (pid == "0" || pid == "3")
                    return "未发布";
                else if (pid == "1")
                    return "正在使用";
                else if (pid == "2")
                    return "已过期";
            }

            function Aclick(pid, obj) {
                var arr = pid.split(',');

                if (arr[0] == "1")//正在使用
                {
                    if (arr[1] == "1")
                        return "<a onclick='openAddClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >升级</a>&nbsp;&nbsp;&nbsp;<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改配置</a>&nbsp;&nbsp;&nbsp;<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >下载</a>&nbsp;&nbsp;&nbsp;<a onclick='DelClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
                    else
                        return "<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改配置</a>&nbsp;&nbsp;&nbsp;<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >下载</a>&nbsp;&nbsp;&nbsp;<a onclick='DelClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
                }
                else if (arr[0] == "3")//未发布（新增）
                {
                    return "<a onclick='UpdateInfoClick(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color:blue' >修改</a>";
                }
                else if (arr[0] == "0")//未发布（升级）
                {
                    return "<a onclick='UpdateInfoClick(\"" + obj + "\",\"" + false + "\")' style='cursor:pointer;color:blue' >修改</a>";
                }
                else if (arr[0] == "2") //过期
                {
                    return "<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >下载</a>&nbsp;&nbsp;&nbsp;<a onclick='DelClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>&nbsp;&nbsp;&nbsp;<a onclick='resumeClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >恢复</a>";
                }
                else if (arr[0] == "4")//新增未上传修改
                {
                    return "<a onclick='UpdateInfoClick1(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color:blue' >修改</a>";
                }
                else if (arr[0] == "5")//升级未上传修改
                {
                    return "<a onclick='UpdateInfoClick1(\"" + obj + "\",\"" + false + "\")' style='cursor:pointer;color:blue' >修改</a>";
                }
                else {
                    return "";
                }
            }

            $("#btnAdd").click(function () { toolbarItem_onclick("Add") });

            $("#btnDelete").click(function () { toolbarItem_onclick("Delete") });

            $("#btnReflush").click(function () { toolbarItem_onclick("Reflush") });
            function contextmenu(row) {
                $("#btnAdd").hide();
                $("#btnDelete").show();
                var ch = row.getAttribute("ch");
                var cell = ch.split("_FG$SP_");
                if (cell) {
                    kinds = cell[4].split(',')[0];
                }
                if (kinds == 0 || kinds == 3 || kinds == 4 || kinds == 5) {
                    $(row).find("td").css("color", "red");
                }
            } //


        });                                                                         // end of ready

        //新增
        function toolbarItem_onclick(cmd, grid) {
            if (cmd == "Add") {
                url = '<%=Url.Action("PluginWizard") %>/?IsAdd=1'; //1表示新增(无ID)
                OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "主程序发布向导", onclose: function () { $("#ManageVerList").flexReload(); } });
            }
            else if (cmd == "Delete") {
                var urls = '<%=Url.Action("DelAllVersions") %>';

                hiConfirm("你确定要删除该主程序吗?", "提示", function (btn) {
                    if (btn == true) {
                        $("#loadingpannel").html("正在删除......").show();
                        $.ajax({
                            type: "POST",
                            url: urls,
                            data: {},
                            dataType: "json",
                            success: function (data) {
                                $("#loadingpannel").hide();
                                if (data.IsSuccess) {
                                    hiAlert("删除成功", '提示', function () { $("#ManageVerList").flexReload(); $("#btnDelete").hide(); $("#btnAdd").show(); });
                                }
                                else {
                                    hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                                }
                            }
                        }); //end if $.ajax
                    }
                });    // end of hiConfirm
            }
            else if (cmd == "Reflush") {
            hiConfirm("你确定要重算客户端Hash值吗?", "提示", function (btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在重算......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("Reflush") %>',
                        data: {},
                        dataType: "json",
                        success: function (data) {
                            $("#loadingpannel").hide();
                            if (data.IsSuccess) {
                                hiAlert("重算成功", '提示', function () { $("#ManageVerList").flexReload(); });
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                            }
                        }
                    }); //end if $.ajax
                }
            });
            }
        }  // end of toolbarItem_on

        //升级
        function openAddClick(Id) {
            var url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=0';  //0表示升级
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "主程序发布向导", onclose: function() { $("#ManageVerList").flexReload(); } });
        } // contextMenuItem_click

        //修改
        function UpdateInfoClick(Id, isAdd) {
            var url = "";
            if (isAdd == "true")//新增
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=1&IsUpdate=1'; //1表示新增
            else
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=0&IsUpdate=1'; //0表示升级
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "主程序发布向导", onclose: function() { $("#ManageVerList").flexReload(); } });
        }

        //未发布修改
        function UpdateInfoClick1(Id, isAdd) {
            var url = "";
            if (isAdd == "true")//新增
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=1&IsUpdate=2'; //1表示新增
            else
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=0&IsUpdate=2'; //0表示升级
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "主程序发布向导", onclose: function() { $("#ManageVerList").flexReload(); } });
        }

        //修改配置
        function UpdateConfigClick(Id) {
            var ul = $("a.select").attr("href");

            var url = '<%=Url.Action("UpdateConfigInfo") %>/' + escape(Id) + '?type='+'<% =ViewData["clientType"] %>';  //2表示修改配置            
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "修改配置", onclose: function() { $("#ManageVerList").flexReload(); } });
        }

        function flushGrid() {
            $("#ManageVerList").flexReload();
        } // end of flushGrid


        function openClick(ojb) {
            $("#vid").val(ojb);
            $("#fmEdit").submit();
        }

        function DelClick(Id) {
            var length = $("#ManageVerList")[0].rows.length;
            var urls = '<%=Url.Action("DelVersions") %>';
            if (length == 1) {
                alert('当前已是最后一个版本,请删除整个主程序');
                return false;
            }
            hiConfirm("你确定要删除该版本吗?", "提示", function(btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在删除......").show();
                    $.ajax({
                        type: "POST",
                        url: urls,
                        data: { vid: Id },
                        dataType: "json",
                        success: function(data) {
                            $("#loadingpannel").hide();
                            if (data.IsSuccess) {
                                hiAlert("删除成功", '提示', function() { $("#ManageVerList").flexReload(); });
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                            }
                        }
                    }); //end if $.ajax
                }
            });   // end of hiConfirm

        }


        function resumeClick(Id) {

            var urls = '<%=Url.Action("ResumeVersions") %>';

            hiConfirm("你确定要恢复该版本吗?", "提示", function(btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在恢复......").show();
                    $.ajax({
                        type: "POST",
                        url: urls,
                        data: { vid: Id },
                        dataType: "json",
                        success: function(data) {
                            $("#loadingpannel").hide();
                            if (data.IsSuccess) {
                                hiAlert("恢复成功", '提示', function() { $("#ManageVerList").flexReload(); });
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                            }
                        }
                    }); //end if $.ajax
                }
            });   // end of hiConfirm
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
    <% using (Html.BeginForm("GetDownLoadFile", "MainInfoManage", FormMethod.Post, new { id = "fmEdit" }))
       {%>
    <div style="padding: 1px;">
        <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">主程序管理</span>
            </div>
        </div>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
            <div id="btnDelete" class="fbutton">
                <div>
                    <span title='删除' class="Delete">删除</span></div>
            </div>
            <div id="btnReflush" class="fbutton">
                <div>
                    <span title='重算Hash值' class="btnreset">重算Hash值</span></div>
            </div>
        </div>
        <div>
            <table id="ManageVerList" style="display: none;">
            </table>
        </div>
    </div>
    <%=Html.Hidden("vid")%>
    <%} %>
</body>
</html>
