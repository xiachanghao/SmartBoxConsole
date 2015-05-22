<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主程序配置管理</title>
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

            var pid = '<%=ViewData["pId"]%>';
            var vid = '<%=ViewData["verid"]%>';
            $("#btnNext").click(function() { NextWindowsclick(vid) });
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetConfigInfo")%>/' + escape(pid),
                colModel: [
                    { display: '键', name: 'Key', width: 100, sortable: false, align: 'left' },
                    { display: '值', name: 'Value', width: 100, sortable: false, align: 'left' },
                    { display: '描述', name: 'Summary', width: 150, sortable: false, align: 'left' },
			        { display: '操作', name: 'ConfigId', width: 70, sortable: false, process: Aclick, align: 'center' }
				],
                sortname: "ConfigId",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                rowhandler: contextmenu,
                showcheckbox: true
            };

            var grid = $("#ManageConfigInfoList").flexigrid(option);

            function Aclick(pid, obj) {
                return "<a onclick='openWindowsclick(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color=blue' >修改</a>&nbsp;&nbsp;<a onclick='openWindowsclickdel(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color=blue' >删除</a>";
            }

            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });
            $("#btndel").click(function() { toolbarItem_onclick("del") });

            function contextmenu(row) {
            } //

        });                                                // end of ready

        function toolbarItem_onclick(cmd, grid) {
            var pid = '<%=ViewData["pId"]%>';
            if (cmd == "Add") {
                OpenModelWindow('<%=Url.Action("AddConfigInfo") %>/?pid=' + pid, { width: 400, height: 180, caption: "新增信息", onclose: function() { flushGrid(); } });
            }
            else if (cmd == "del") {
                var ids = $("#ManageConfigInfoList").getCheckedRows(); //获取选中的checkbox，返回选中的id数组
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
                            url: '<%=Url.Action("DelSomeConfigInfos") %>',
                            data: { id: strIds },
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
            $("#ManageConfigInfoList").flexReload();
        } // end of flushGrid
        //修改
        function openWindowsclick(Id) {
            var pid = '<%=ViewData["pId"]%>';
            var url = '<%=Url.Action("AddConfigInfo") %>/' + escape(Id) + '/?pid=' + pid;
            OpenModelWindow(url, { width: 400, height: 180, caption: "修改信息", onclose: function() {  flushGrid();  } });
        } // contextMenuItem_click

        function NextWindowsclick(Id) {
            var isloadfile = '<%=ViewData["IsLoadFile"]%>';//是否单个上传文件
            var url = '<%=Url.Action("PublishInfo") %>/' + escape(Id) + '/?IsLoadFile=' + isloadfile;
            window.location = url;
        }

        function openWindowsclickdel(Id) {
            hiConfirm("你确定要删除该信息吗？", '确认', function(r) {
                if (r == true) //
                {
                    $("#loadingpannel").html("正在删除......").show();
                    $.post('<%=Url.Action("DelSomeConfigInfos") %>', { id: Id },
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
        <%--<div class="cHead">
            <div class="ftitle">
                <span id="departmentName">主程序配置管理</span>
            </div>
        </div>--%>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
            <div id="btndel" class="fbutton">
                <div>
                    <span title='删除' class="Delete">删除</span></div>
            </div>
            <div id="Div1" style="float:right">
                <input type="button" id="btnNext" value="下一步" />
            </div>
        </div>
        <div>
            <table id="ManageConfigInfoList" style="display: none;">
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
