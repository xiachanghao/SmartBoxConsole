<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>插件用户信息管理</title>
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

            var pid = '<%=ViewData["pid"]%>';
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight+26;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetUserInfo")%>/' + escape(pid),
                colModel: [
                    { display: '用户帐号', name: 'UserUId', width: 100, sortable: false, align: 'left' },
                    { display: '用户性别', name: 'Gender', width: 100, sortable: false, process: Aclick, align: 'left' },
                    { display: '用户姓名', name: 'UserName', width: 150, sortable: false, align: 'left' }
				],
                sortname: "UserUId",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                rowhandler: contextmenu,
                showcheckbox: true
            };

            var grid = $("#ManageUserInfoList").flexigrid(option);

            function Aclick(pid, obj) {
                if (obj == 'male')
                    return "男";
                else if (obj == "female")
                    return "女";
                else
                    return "";
            }

            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });
            $("#btndel").click(function() { toolbarItem_onclick("del") });

            function contextmenu(row) {
                var menu = { width: 150, items: [] };
                menu.items.push({ text: "删除", icon: '<%=Url.Content("~/images/icons/delete.png")%>', alias: "contextmenu-update", action: contextMenuItem_click });
                menu.items.push({ text: "刷新", icon: '<%=Url.Content("~/images/icons/table_refresh.png")%>', alias: "contextmenu-reflash", action: contextMenuItem_click });

                $(row).contextmenu(menu);
            } //

            function contextMenuItem_click(target) {
                var id = $(target).attr("id").substr(3);
                var chs = $(target).attr("ch").split('_FG$SP_');
                var cmd = this.data.alias;
                if (cmd == "contextmenu-update") {
                    var pid = '<%=ViewData["pid"]%>';
                    openWindowsclickdel(id, pid);
                } else if (cmd == "contextmenu-reflash") {
                    $("#ManageUserInfoList").flexReload();
                }
            }


        });                                                   // end of ready

        function toolbarItem_onclick(cmd, grid) {
            var pid = '<%=ViewData["pid"]%>';
            if (cmd == "Add") {
                $.ShowIfrmDailog('<%=Url.Action("AddUserInfo") %>/?pid=' + pid, { width: 600, height: 370, caption: "新增用户", onclose: function() { flushGrid(); } });
            }
            else if (cmd == "del") {
                var ids = $("#ManageUserInfoList").getCheckedRows(); //获取选中的checkbox，返回选中的id数组
                if (ids.length < 1) {
                    hiAlert('请选择您想要删除的配置信息', '提示');
                    return false;
                }
                var strIds = ids.join(',');
                hiConfirm("你确认要删除吗？", "提示", function(btn) {
                    if (btn == true) {
                        $("#loadingpannel").html("正在删除......").show();
                        $.ajax({
                            type: "POST",
                            url: '<%=Url.Action("DelSomeUserInfos") %>',
                            data: { id: strIds,pid:pid },
                            dataType: "json",
                            success: function(data) {
                                $("#loadingpannel").hide();
                                if (data.IsSuccess) {
                                    flushGrid();
                                }
                                else {
                                    hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                                }
                            }
                        }); //end if $.ajax
                    }
                }); // end of hiConfirm
            }
        }  // end of toolbarItem_on

        function flushGrid() {
            $("#ManageUserInfoList").flexReload();
        } // end of flushGrid
        //修改

        function openWindowsclickdel(Id,pids) {
            hiConfirm("你确定要删除该信息吗？", '确认', function(r) {
                if (r == true) //
                {
                    $("#loadingpannel").html("正在删除......").show();
                    $.post('<%=Url.Action("DelSomeUserInfos") %>', { id: Id,pid:pids },
                                                        function(data) {
                                                            $("#loadingpannel").hide();
                                                            if (data.IsSuccess) {
                                                                hiAlert("删除成功", '提示', function() { flushGrid(); });
                                                            }
                                                            else {
                                                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                                                            }
                                                        },
                                                    "json"
                                                    );
                }
            });

        }  // contextMenuItem_clic
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
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
            <div id="btndel" class="fbutton">
                <div>
                    <span title='删除' class="Delete">删除</span></div>
            </div>
        </div>
        <div>
            <table id="ManageUserInfoList" style="display: none;">
            </table>
        </div>
        <div class="ajaxmsgpanel">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                正在保存数据...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                非常抱歉，无法执行您的操作，请稍后再试</div>
            <div id="tdiv" style="display: none;">
            </div>
        </div>
    </div>
</body>
</html>
