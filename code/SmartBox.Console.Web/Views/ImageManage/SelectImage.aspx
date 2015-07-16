<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择图片</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dp.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="Stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.datepicker.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.dropdown.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <style type="text/css">
        img
        {
            margin: 5px;
            display: inline-block;
        }
        .cell-name
        {
            width: 150px;
            text-align: right;
        }
        .cell-value
        {
            float: left;
        }
        .cell-value img
        {
            margin: 5px;
            width: 100px;
            height: 100px;
            cursor: pointer;
        }
        .cell-value img:hover
        {
            border: 1px solid #000000;
        }
        
        body {
background-color: #f0f0f0;
}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSubmit").click(function () {
                if ($("#fileImage").val() == "") {
                    alert("请选择一张图片");
                    return;
                }
                $("#frmImage").ajaxSubmit({
                    dataType: "json",
                    success: function (data) {
                        if (data.IsSuccess) {
                            window.location.reload();
                        }
                        else {
                            hiAlert("操作失败\r\n" + data.Msg, '提示', function () { });
                        }
                    }
                });
            });

            $(".cell-value").find("img").click(function () {
                var modal = '<%=Request.QueryString["modal"] %>';
                var elementid = '<%=Request.QueryString["elementid"] %>';
                if (modal != '0') {
                    window.returnValue = $(this).attr("imgid");
                    window.close();
                }
                else {
                    parent.selectImage(elementid, $(this).attr("imgid"));
                    parent.CloseWind();
                }
            });
        });
    </script>
</head>
<body>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr style="height: 40px;">
            <td class="cell-name">
                <b>上传新图片:</b>
            </td>
            <td>
                <%  using (Html.BeginForm("UpdateImage", "ImageManage", FormMethod.Post, new { id = "frmImage", enctype = "multipart/form-data" }))
                    {
                        Html.AntiForgeryToken();
                        Html.ValidationSummary(true);%>
                <input type="file" id="fileImage" name="fileImage" />&nbsp;<input type="button" id="btnSubmit"
                    value="上传" />
                <%} %>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="cell-value">
                <% IList<SmartBox.Console.Common.Entities.Image> imageList = ViewData["ImageList"] as IList<SmartBox.Console.Common.Entities.Image>; %>
                <% foreach (SmartBox.Console.Common.Entities.Image image in imageList)
                   {%>
                <img imgid='<%= image.ID %>' src='<%= Url.Action("ViewImage","ImageManage") %>/<%=image.ID %>'
                    alt="" />
                <%  } %>
            </td>
        </tr>
    </table>
</body>
</html>
