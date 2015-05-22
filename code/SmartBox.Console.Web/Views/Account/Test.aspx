<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<link href="<%=Url.Content("~/Themes/Default/uploadify.css")%>" rel="stylesheet"
    type="text/css" />

<script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

<script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

<script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>

<script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>

<script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>

<script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>

<script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>

<script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

<script type="text/javascript" src="<%=Url.Content("~/Javascripts/UploadFile/swfobject.js")%>"></script>

<script type="text/javascript" src="<%=Url.Content("~/Javascripts/UploadFile/jquery.uploadify.v2.1.0.min.js")%>"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Uploadify</title>

    <script type="text/javascript">
　        $(document).ready(   function () {
　        var path = "d:\\a\\upload";
　        
　        
              $("#uploadify").uploadify({
                'uploader': '<%=Url.Content("~/Javascripts/UploadFile/uploadify.swf")%>',
                'script': '/MainProgramManage/SaveVerInfo',
                'cancelImg': '<%=Url.Content("~/Images/UploadFile/cancel.png")%>',
                'folder': path,
                'queueID': 'fileQueue',
                'auto': false,
                'multi': true,
                'sizeLimit ': 2000
            });

    </script>

</head>
<body>
    <div id="fileQueue">
    </div>
    <input type="file" name="uploadify" id="uploadify" />
    <p>
        <a onclick="a();  $('#uploadify').uploadifyUpload()">上传</a>| <a href="javascript:$('#uploadify').uploadifyClearQueue()">
            取消上传</a>
    </p>
</body>
</html>
