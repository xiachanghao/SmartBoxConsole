<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EditVerInfo</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/simplybuttons.css")%>" rel="Stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="stylesheet" type="text/css" />
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

    <style type="text/css">
        .bspan
        {
            background-color: #E8F1F8 !important;
        }
        a
        {
            cursor: pointer !important;
        }
        #txtUrl
        {
            width: 469px;
        }
        .style1
        {
            width: 169px;
        }
    </style>

    <script type="text/javascript">


        $(document).ready(function() {

            //初始化按钮样式
            SimplyButtons.init();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var IsCate = '<%=ViewData["IsCate"]%>';

            if (IsCate == "1") //若新赠
            {
                $("input[name=IsNeeds]").attr("disabled", false);
            }
            else {
                $("input[name=IsNeeds]").attr("disabled", true);
            }

            $("#Butsubmit").click(LoadFileToMem);

            if (parent.$("#PluginCateCodes").val() == "1") //设置选中
            {
                $("input[name=IsNeeds]").eq(0).attr("checked", "checked");
                $("input[name=IsNeeds]").eq(1).attr("checked", "");
            }
            else {
                $("input[name=IsNeeds]").eq(1).attr("checked", "checked");
                $("input[name=IsNeeds]").eq(0).attr("checked", "");
            }

            function LoadFileToMem() {
                var boolSelect = $("input[name=IsNeeds]:checked").val();
                parent.$("#PluginCateCodes").val(0);
                parent.$("#dicInfoName").text("2.上传插件");
 
                if (boolSelect == "1")
                    parent.$("#PluginCateCodes").val(1);
                var IsAdd = parent.$("#IsAdd").val();
                var Vid = parent.$("#Vids").val();
                var IsUpdate = parent.$("#IsUpdate").val();
                var PluginCateCode = parent.$("#PluginCateCodes").val();

                url = '<%=Url.Action("UploadVersionFile") %>/?IsAdd=' + IsAdd + '&Vid=' + Vid + '&IsUpdate=' + IsUpdate + '&PluginCateCode=' + PluginCateCode;
                window.location = url;
            }
        }); 
    </script>

</head>
<body>
    <div>
        <div class="bbit-categorycontainer">
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr id="trType">
                    <td align="right" class="style1">
                        插件分类：
                    </td>
                    <td>
                         <%=Html.RadioButton("IsNeeds", "1")%>web插件
                        <%=Html.RadioButton("IsNeeds", "0")%>非web插件
                    </td>
                    
                </tr>
                <tr>
                    <td align="center" style="text-align:center" colspan="2">
                        <input type="button" id="Butsubmit" value="下一步" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
