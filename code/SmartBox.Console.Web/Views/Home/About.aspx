<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/common.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/home.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#FileUpload1").change(function() {

                var v = $(this).val();
                if (/.+\.csv$/i.test(v) == false) {
                    alert("请选择csv或CSV文件上传")
                //    document.getElementById("FileUpload1").reset();
                    return false;
                }
            });
        });
     </script>

    <h2>About</h2>
    <p>
      <input type="file" name="FileUpload1" id="FileUpload1" style="width: 80%" />
    </p>
</asp:Content>
