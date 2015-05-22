<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收藏用户列表</title>
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
        $(document).ready(function () {
            SimplyButtons.init();
            var pid = '<%=ViewData["id"]%>';
            //var TableName = '<%=ViewData["TableName"]%>';
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("OutPackageCollectionList")%>/' + '<% =ViewData["id"] %>',
                colModel: [
                    { display: 'clt_id', name: 'clt_id', width: 100, sortable: false, align: 'left',hide:true },
                    { display: '用户名称', name: 'uname', width: 100, sortable: false, align: 'left' },
                    { display: '客户端类型', name: 'ClientType', width: 130, sortable: false, align: 'left' },
                    { display: '收藏日期', name: 'clt_CollectDate', width: 340, sortable: true, align: 'left' },
                    { display: 'ID', name: 'ID', width: 40, sortable: false, align: 'left', hide: true }
				],
                usepager: true,
                rp: 20,
                rowbinddata: true
            };

            var grid = $("#ApplicationPackageList").flexigrid(option);

            $("span.Back").click(function () {              
                url = '<%=Url.Action("ApplicationExt") %>';             
                window.location.href = url;
            });


        });    // end of ready
        


        function flushGrid() {
            $("#ApplicationPackageList").flexReload();
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
        .BatchAdd
        {
            padding-left: 20px;
            /*background: url(<%=Url.Content("~/images/icons/add.png")%>) no-repeat 1px;*/
        }
    </style>
</head>
<body>
<div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
    <div style="padding: 1px;">  
      <%--  <div id="caltoolbar" class="ctoolbar">
            <div id="btnBack" class="fbutton">
                <div>
                    <span title='返回' class="Back">返回</span></div>
            </div>             
        </div>    --%>
        <div>
            <table id="ApplicationPackageList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
