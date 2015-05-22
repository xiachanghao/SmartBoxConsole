<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>在线访问时长统计</title>
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
    <link href="<%=Url.Content("~/Themes/Default/dp.css")%>" rel="stylesheet"
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
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.datepicker.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight - 15;
            var mainWidth = document.documentElement.clientWidth - 5; // 减去边框和左边的宽度
            var otherpm = 250;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryTime")%>',
                colModel: [
                    { display: '用户账号', name: 'UserUid', width: 90, sortable: false, align: 'left' },
                    { display: '用户名称', name: 'User_Full_Name', width: 90, sortable: false, align: 'left' },
                    { display: '用户部门', name: 'ORG_NAME', width: 90, sortable: false, align: 'left' },
                    { display: 'Pad/Android(小时)', name: 'PadAndroid', width: 120, sortable: false, align: 'left' },
                    { display: 'Pad/iOS(小时)', name: 'PadiOS', width: 120, sortable: false, align: 'left' },
                    { display: 'Phone/Android(小时)', name: 'PhoneAndroid', width: 120, sortable: false, align: 'left' },
                    { display: 'Phone/iOS(小时)', name: 'PhoneiOS', width: 120, sortable: false, align: 'left' }
				],
                sortname: "UserUid",
                sortorder: "asc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };

            var grid = $("#ShowList").flexigrid(option);
            autosize_flexgrid("#ShowList", 236);
            $(window).resize(function () {
                autosize_flexgrid("#ShowList", 236);
            });

            $("#startDate,#endDate").datepicker({
                picker: '<img class="picker" align="middle" src="<%= Url.Content("~/Themes/Default/images/dp/cal.gif")%>" alt="">'
            });

            $("span.Approve").click(function () {
                var p = { extParam: [
                        { name: "StartTime", value: $("#startDate").val() },
                        { name: "EndTime", value: $("#endDate").val() }, { name: "UID", value: $("#tbUID").val() }
                ]
                };
                p.newp = 1;
                $("#ShowList").flexOptions(p).flexReload();
            });
        });     // end of ready

        function UpdateWebApplicationClick(id) {
            url = '<%=Url.Action("EditWebApplication") %>/' + id;
            OpenModelWindow(url, { width: 700, height: 400, caption: "修改", onclose: function () { $("#WebApplicationList").flexReload(); } });
        }

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
                <span id="departmentName">在线时长统计</span>
            </div>
        </div>
        <div style="height:10px;"></div>
         <div class="list_title"><span>请设置查询条件</span></div>  
        <%--<div id="caltoolbar" class="ctoolbar">
            登录时间: <input type="text" id="startDate" />
            至 <input type="text" id="endDate" />
            <div id="btnQuery" class="fbutton">
                <div>
                    <span title='查询' class="Approve">查询</span></div>
            </div>
        </div>--%>
        <div class="list_nr01">
           <table border="0" cellspacing="0" cellpadding="0">
           <tr>
                <td><span>用户账号:</span></td>
                <td colspan="4"><input id="tbUID" /></td>             
              </tr>
              <tr>
                <td><span>登录时间:</span></td>
                <td><input type="text" id="startDate" /></td>
                <td><span>至</span></td>
                <td><input type="text" id="endDate" /></td>
                <td><span class="Approve btn01">查　询</span></td>                
              </tr>
          </table>

         </div>
        <div>
            <table id="ShowList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
