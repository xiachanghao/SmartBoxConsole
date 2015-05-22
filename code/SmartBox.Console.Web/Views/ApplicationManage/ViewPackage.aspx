<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>安装包信息查看</title>
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
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {            
            var id =<%=ViewData["PID"] %>;
            $('#ewm').click(function () {
                var url = '<%=Url.Content("~/ApplicationManage/PackageCode")%>/' + id;
                document.getElementById('ifrm').src=url;
                 $('#top div').css('background-color','rgb(255, 255, 255)');
                 $('#ewm').css('background-color','rgb(39, 121, 203)');
            })
            $('#ewm').hover(function () {
                $(this).addClass('hover');
            }, function () {
                $(this).removeClass('hover');
            });

            $('#jt').click(function () {
                var url = '<%=Url.Content("~/ApplicationManage/PackageGifList")%>/' + id;
                document.getElementById('ifrm').src=url;
                 $('#top div').css('background-color','rgb(255, 255, 255)');
                 $('#jt').css('background-color','rgb(39, 121, 203)');               
            }).hover(function () {
                $(this).addClass('hover');
            }, function () {
                $(this).removeClass('hover');
            });

            $('#fk').click(function () {
                var url = '<%=Url.Content("~/ApplicationManage/PackageFAQList")%>/' + id;
                document.getElementById('ifrm').src=url;
                $('#top div').css('background-color','rgb(255, 255, 255)');
                $('#fk').css('background-color','rgb(39, 121, 203)');
            }).hover(function () {
                $(this).addClass('hover');
            }, function () {
                $(this).removeClass('hover');
            });

            $('#sc').click(function () {
                var url = '<%=Url.Content("~/ApplicationManage/PackageCollectionList")%>/' + id;
                document.getElementById('ifrm').src=url;
                $('#top div').css('background-color','rgb(255, 255, 255)');
                $('#sc').css('background-color','rgb(39, 121, 203)');
            }).hover(function () {
                $(this).addClass('hover');
            }, function () {
                $(this).removeClass('hover');
            });

            $('#manul').click(function () {
                var url = '<%=Url.Content("~/ApplicationManage/PackageManualList")%>/' + id;
                document.getElementById('ifrm').src=url;
                $('#top div').css('background-color','rgb(255, 255, 255)');
                $('#manul').css('background-color','rgb(39, 121, 203)');
            }).hover(function () {
                $(this).addClass('hover');
            }, function () {
                $(this).removeClass('hover');
            });

            $('#ewm').click();

        });     
        // end of ready


   
       

        function flushGrid() {
            $("#ApplicationPackageList").flexReload();
        } // end of flushGrid
        //查看详细信息

    </script>
    <style type="text/css">
        .hover
        {
            cursor: pointer;
            background-color: #f0f0f0;
        }
        #c
        {
            width: 99%;
            height: 500px;
            border: 1px solid #DFDFDF;
            background-color: #FBFBFC;
        }
        
        #c #l
        {
            width: 200px;
            float: left;
            border-right: 1px solid #DFDFDF;
            background-color: #fcfcfc;
            height: 100%;
            overflow: scroll;
        }
        
        #c #r
        {
            height: 100%;
            margin-left: 0px;
        }
        
        iframe#ifrm
        {
            width: 100%;
            height: 100%;
        }
        
        #top
        {
            width: 99%;
            height: 40px;
            background-color: rgb(245, 245, 245);
            vertical-align: middle;
            line-height: 40px;
            border-top: solid #dfdfdf 1px;
            border-left: solid #dfdfdf 1px;
            border-right: solid #dfdfdf 1px;
        }
        
        
        #ewm, #jt, #fk, #sc, #manul
        {
            float:left;
            height: 30px;
            line-height: 30px;
            width: 100px;
            text-align: center;
            margin-top: 5px;
            margin-right: 15px;
            margin-left:15px;
            border: 1px solid #e0e0e0;
        }
    </style>
</head>
<body>
    <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
        正在保存数据...</div>
    <div id="top">
        <div id="ewm">
            二维码</div>
        <div id="jt">
            截图</div>
        <div id="fk">
            反馈</div>
        <div id="sc">
            收藏</div>
        <div id="manul">
            手册</div>
    </div>
    <div id="c">
        <div id="r">
            <iframe id="ifrm" name="ifrm" frameborder="0"></iframe>
        </div>
    </div>
</body>
</html>
