<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户摘要</title>
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
                url: '<%=Url.Action("GetUserInfo")%>',
                colModel: [
                    { display: '状态', name: 'Status', width: 80, sortable: false, process: Dclick, align: 'center' },
                    { display: '用户标识', name: 'UserUID', width: 100, sortable: false, align: 'left' },
                    { display: '姓名', name: 'UserName', width: 100, sortable: false, align: 'left' },
                    { display: '最后登陆IP', name: 'UserUid', width: 150, sortable: false, align: 'left' },
                    { display: '最后登陆时间', name: 'LastLoginTime', width: 120, sortable: false, align: 'left' },
                    { display: '最后退出时间', name: 'LastLogoutTime', width: 120, sortable: false, align: 'left' }
				],
                sortname: "LastLoginTime",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                extParam: [
                        { name: "UserStatus", value: $("#UserStatus").val() }
                     ],
                rowhandler: contextmenu
            };
            var grid = $("#ManageVerList").flexigrid(option);
            autosize_flexgrid("#ManageVerList");
            $(window).resize(function () {
                autosize_flexgrid("#ManageVerList");
            });

            function Dclick(pid, obj) {
                if (pid == "online")
                    return '<img src=\"<%=Url.Content("~/Images/icons/online16x16.png")%>\" />';
                else if (pid == "offline")
                    return '<img src=\"<%=Url.Content("~/Images/icons/offline16x16.png")%>\" />';
            }

            function contextmenu(row) {
            } //


            $("#UserStatus").change(function() {
                flexigrid();
            })

            function flexigrid() {
                var p = { extParam: [
                        { name: "UserStatus", value: $("#UserStatus").val() }
                     ]
                };
                p.newp = 1;
                $("#ManageVerList").flexOptions(p).flexReload();
            }

        });                                                                         // end of ready
   
        
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
                <span id="departmentName">用户摘要</span>
            </div>
        </div>
        <div id="caltoolbar" class="ctoolbar">
          &nbsp;&nbsp;&nbsp;用户状态：&nbsp;&nbsp;&nbsp;<%=Html.DropDownList("UserStatus", (IEnumerable<SelectListItem>)ViewData["list"], new { style = "margin-top: 2px" })%>
        </div>
        <div>
            <table id="ManageVerList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>

