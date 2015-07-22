<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>Beyondbit SSO & Authentication Server</title>
    <link href="<%=Url.Content("~/Images/images/style.css") %>" rel="stylesheet" type="text/css" />   

    <script type="text/javascript">
        function RegistUser() {
            //var p = window.location.host;
            //p = 'http://' + p + '/Reg/RegUser';
            //window.location.href =  p ;
            var url = '<%=Url.Content("~/Loginless/UnitManagerAdd") %>';
            window.open(url);
         }

    </script>
</head>
<body>
    <% using (Html.BeginForm())
       {
           Html.AntiForgeryToken();
           Html.ValidationSummary(true);
           %>
    <div class="mainpanel">
        <div class="loginpanel">
            <div class="loginpanel-appname">
                <div class="loginpanel-dott">
                </div>
            </div>
            <div class="loginpanel-loginform">
                <div style="padding-top: 20px">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <span id="ExceptionMsg" style="color: Red; font-family:微软雅黑; float:left; margin-left:10px;">
                                        <%=ViewData["msg"]%></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    <span>用户名</span>
                                </td>
                                <td>
                                    <input class="loginforminput" id="username" name="username" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>密&nbsp;&nbsp;码</span>
                                </td>
                                <td>
                                    <input class="loginforminput" id="password" type="password" name="password" />
                                </td>
                            </tr>
                            <tr>                                
                                <td style="padding-left: 48px" colspan="2">
                                    <input class="loginformsubmitbtn" id="Button1" type="submit" name="Button1" value="" />
                                   <%--<input class="regsubmitbtn" id="Button2" type="button" name="Button1" value=""  onclick="RegistUser()"/>--%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="copyrigthpanel">
            <div class="copyright">
                上海互联网软件有限公司</div>
        </div>
    </div>
    <% } %>
</body>
</html>
