<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.LogInfo>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>日志详细信息</title>
     <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
     
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#bbit-form-table tr:even").addClass("even");
            $("#bbit-form-table tr:odd").addClass("odd");
            $("#CloseImgBtn1").click(function() {
                CloseModelWindow();
            });
            $.hotkeys.add('esc', function() {
                CloseModelWindow();
            });
        }); // ready
    </script>
</head>
<body>
    <div class="bbit-main">
        <div class="toolBotton">
            <a id="CloseImgBtn1" class="imgbtn" href="javascript:void(0);"><span class="Close"
                title="关闭">关闭(<u>Esc</u>)</span></a>
        </div>
         <div class="bbit-categorycontainer">
        <table id="bbit-form-table" width="100%" class="bbit-form" cellspacing="0"  cellpadding="1">
             <tr>
                <td class="bbit-form-cell-name tdleft">
                    IP：
                </td>
                <td class="bbit-form-cell-value tdright">
                    <%=Html.Encode(Model.Ip) %>
                </td>
            </tr>
            <tr>
                <td class="bbit-form-cell-name tdtop tdleft">
                    操作帐号：
                </td>
                 <td class="bbit-form-cell-value tdtop tdright">
                    <%=Html.Encode(Model.UserUid) %>
                </td>
            </tr>
            <tr>
                <td class="bbit-form-cell-name tdleft">
                    日志类型：
                </td>
                <td class="bbit-form-cell-value tdright">
                    <%=Html.Encode(Model.Type) %>
                </td>
            </tr>
            <tr>
               <td class="bbit-form-cell-name tdleft">
                    操作时间：
                </td>
                <td class="bbit-form-cell-value tdright">
                    <%=Html.Encode(Model.Time)%>
                </td>
            </tr>
           <tr>
                 <td class="bbit-form-cell-name tdleft">
                    日志信息：
                </td>
                 <td class="bbit-form-cell-value tdright">
                    <%=Html.Encode(Model.Msg) %>
                </td>
            </tr>
        </table>
        </div>
    </div>
</body>
</html>
