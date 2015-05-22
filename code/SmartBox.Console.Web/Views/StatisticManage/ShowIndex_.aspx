<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据访问量统计</title>
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
    <script src="<%=Url.Content("~/Javascripts/jquery-1.8.3.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.autocomplete.js")%>" type="text/javascript"
        defer="defer"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.datepicker.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <script type="text/javascript">
                  
        $(document).ready(function () {

            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 5; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryShow")%>',
                showToggleBtn: false,
                striped: true,
                novstripe: false,
                colModel: [
                    { display: '单位名称', name: 'UnitName', width: 120, sortable: false, align: 'left' },
                    { display: '访问账号数量', name: 'UserCount', width: 100, sortable: false, align: 'left' },
                    { display: 'Pad/Android', name: 'PadAndroid', width: 100, sortable: false, align: 'left' },
                    { display: 'Pad/iOS', name: 'PadiOS', width: 100, sortable: false, align: 'left' },
                    { display: 'PC/Windows', name: 'PCWindows', width: 100, sortable: false, align: 'left' },
                    { display: 'Phone/Android', name: 'PhoneAndroid', width: 90, sortable: false, align: 'left' },
                    { display: 'Phone/iOS', name: 'PhoneiOS', width: 90, sortable: false, align: 'left' },
                    { display: '总计访问次数', name: 'UsageCount', width: 90, sortable: false, align: 'left' }
				],
                sortname: "UnitName",
                sortorder: "asc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };
            var grid = $("#ShowList").flexigrid(option);
            autosize_flexgrid("#ShowList",106);
            $(window).resize(function () {
                autosize_flexgrid("#ShowList", 106);
            });            

            $("span.Approve").click(function () {
                var unitName = encodeURI($("#unitName").val());
                searchurl = '<%=Url.Action("SearchShow") %>' + '?unitName=' + unitName;

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
                    { display: '单位名称', name: 'UnitName', width: 90, sortable: false, align: 'left' },
                    { display: '访问账号数量', name: 'UserCount', width: 90, sortable: false, align: 'left' },
                    { display: 'Pad/Android', name: 'PadAndroid', width: 90, sortable: false, align: 'left' },
                    { display: 'Pad/iOS', name: 'PadiOS', width: 90, sortable: false, align: 'left' },
                    { display: 'PC/Windows', name: 'PCWindows', width: 90, sortable: false, align: 'left' },
                    { display: 'Phone/Android', name: 'PhoneAndroid', width: 90, sortable: false, align: 'left' },
                    { display: 'Phone/iOS', name: 'PhoneiOS', width: 90, sortable: false, align: 'left' },
                    { display: '总计访问次数', name: 'UsageCount', width: 90, sortable: false, align: 'left' },
				],
                    sortname: "UnitName",
                    sortorder: "asc",
                    usepager: true,
                    rp: 15,
                    rowbinddata: true
                };
                //alert(option.sortname);
                var grid = $("#ShowList").flexOptions(option).flexReload();
            });



        });          // end of ready
                     
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
                <span id="departmentName">数据访问量统计</span>
            </div>
        </div>
        <div style="height:10px;"></div>
       <%-- <div id="caltoolbar" class="ctoolbar">
            单位名称:
            <input type="text" id="unitName" />
            访问时间:
            <input type="text" id="startDate" />
            至
            <input type="text" id="endDate" />
            <div id="btnQuery" class="fbutton">
                <div>
                    <span title='查询' class="Approve">查询</span></div>
            </div>
        </div>--%>
        <div>
            <table id="ShowList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
