<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SmartBox.Console.Deploy.About" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>控件测试</title>
    <link rel="Stylesheet" href="Styles/jquery-ui.css"/>
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script> 
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>  
    <script src="Scripts/jquery-ui-i18n.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script> 
    <script type="text/javascript">
        $(function () {
            $("#startDate").datepicker();
            $("#endDate").datepicker();
        });
    </script>
</head>
<body>
    <div style="padding: 1px;">
        访问时间:
        <input type="text" id="startDate" />
        至
        <input type="text" id="endDate" />
    </div>
</body>
</html>
