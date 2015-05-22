<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>插件管理</title>
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
        $(document).ready(function() {
            SimplyButtons.init();
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetPluginInfo")%>',
                colModel: [
                    { display: '插件名称', name: 'DisplayName', width: 80, sortable: false, align: 'left' },
                    { display: '插件标识', name: 'PluginCode', width: 80, sortable: false, process: Dclick, align: 'left' },
                    { display: '版本', name: 'Version', width: 50, sortable: false, align: 'left' },
                    { display: '版本说明', name: 'VersionSummary', width: 100, sortable: false, align: 'left' },
                    { display: '插件类型', name: 'PDisplayName', width: 80, sortable: false, align: 'left' },
                    { display: '开发商', name: 'CompanyName', width: 80, sortable: false, align: 'left' },
			        { display: '发布者', name: 'CreateUid', width: 80, sortable: false, align: 'left' },
			        { display: '发布时间', name: 'CreateTime', width: 130, sortable: false, hide: true, align: 'left' },
			        { display: '操作', name: 'VersionStatuss', width: 180, sortable: false, process: Aclick, align: 'center' }
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

            function Dclick(pid, obj) {
                return "<a onclick='ViewDetails(\"" + obj + "\")' style='cursor:pointer;color:blue' >" + pid + "</a>";
            }

            function Aclick(pid, obj) {
                var arr = pid.split(',');
                var text = "";
                if (arr[0] == "1")//正在使用
                {
                    if (arr[3] == "WebPlugin")
                        text = "<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改配置</a>";
                    else
                        text = "<a onclick='openAddClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >升级</a>&nbsp;&nbsp;&nbsp;<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改配置</a>";
                }
                else if (arr[0] == "3")//未发布（新增）
                {
                    text = "<a onclick='UpdateInfoClick(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color:blue' >修改</a>";
                }
                else if (arr[0] == "0")//未发布（升级）
                {
                    text = "<a onclick='UpdateInfoClick(\"" + obj + "\",\"" + false + "\")' style='cursor:pointer;color:blue' >修改</a>&nbsp;&nbsp;&nbsp;<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改配置</a>";
                }
                else {
                    text = "";
                }
                var Sets = "启用";
                if (arr[1] == true) {
                    Sets = "禁用";
                }
                text += "&nbsp;&nbsp;&nbsp;<a onclick='UpdateIsUseClick(\"" + arr[2] + "\",\"" + Sets + "\",\"" + arr[1] + "\")' style='cursor:pointer;color:blue' >" + Sets + "</a>";
                //2.0.1.0
                text += "&nbsp;&nbsp;&nbsp;<a onclick='DelClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
                //2.0.1.0
                return text;
            }

            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });

            function contextmenu(row) {
                var ch =row.getAttribute("ch");
                var cell = ch.split("_FG$SP_");
                var kinds=0;
                if (cell&&cell.length>1) {
                    kinds = cell[8].split(',')[0];
                }
                if (kinds == 0 || kinds == 3) {
                    $(row).find("td").css("color", "red");
                }
            } //


        });                                                                         // end of ready

        function DelClick(Id) {
            var pluginNames = "";

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("GetPluginNames") %>',
                data: { vid: Id },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        pluginNames = data.Msg.split('|')[0];
                        urls = '<%=Url.Action("DelAllVersions") %>';
                        hiConfirm("你确定要删除" + pluginNames + "插件吗?", "提示", function(btn) {
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
                    else {
                        hiAlert("操作失败");
                    }
                }
            });      //end if $.ajax
        }

        //新增
        function toolbarItem_onclick(cmd, grid) {
            if (cmd == "Add") {
                url = '<%=Url.Action("PluginWizard") %>/?IsAdd=1&IsCate=1'; //1表示新增(无ID)iscate标识是否能够修改分类
                OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "插件发布向导", onclose: function() { $("#ManageVerList").flexReload(); } });
            }
        }  // end of toolbarItem_on


        //升级
        function openAddClick(Id) {
            var url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=0&IsCate=0';  //0表示升级,iscate标识是否能够修改分类
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "插件发布向导", onclose: function() { $("#ManageVerList").flexReload(); } });
        } // contextMenuItem_click


        //修改
        function UpdateInfoClick(Id, isAdd) {
            var url = "";
            if (isAdd == "true")//新增
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=1&IsCate=0&IsUpdate=1'; //1表示新增
            else
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=0&IsCate=0&IsUpdate=1'; //0表示升级
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "插件发布向导", onclose: function() { $("#ManageVerList").flexReload(); } });
        }

        //修改配置
        function UpdateConfigClick(Id) {
            var url = '<%=Url.Action("UpdateConfigInfo") %>/' + escape(Id) ;  //2表示修改配置
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "插件发布向导", onclose: function() { $("#ManageVerList").flexReload(); } });
        }
        //查看插件详细信息
        function ViewDetails(Id) {
            var url = '<%=Url.Action("ViewPluginInfo") %>/' + escape(Id);  
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "查看插件详细信息", onclose: function() { $("#ManageVerList").flexReload(); } });
        }
        //修改禁用状态
        function UpdateIsUseClick(code,str,status) {
            hiConfirm("你确定要" + str + "该插件吗？", "提示", function(btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在执行......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("SetDisableStatus") %>',
                        data: { pid: code, status: status },
                        dataType: "json",
                        success: function(data) {
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
            });   // end of hiConfirm
        }    
        
        function flushGrid() {
            $("#ManageVerList").flexReload();
        } // end of flushGrid
        
    
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
                <span id="departmentName">插件管理</span>
            </div>
        </div>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
        </div>
        <div>
            <table id="ManageVerList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>

