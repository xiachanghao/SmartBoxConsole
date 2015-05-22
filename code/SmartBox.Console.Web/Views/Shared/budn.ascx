<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="<%=Url.Content("~/") %>Content/themes/console/css/main.css" rel="stylesheet" type="text/css" />
<link href="<%=Url.Content("~/") %>Content/themes/base/css/bucp.css" rel="stylesheet" type="text/css" />
<link href="<%=Url.Content("~/") %>Content/themes/console/css/bucp.css" rel="stylesheet" type="text/css" />

<script src="<%=Url.Content("~/") %>Javascripts/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="<%=Url.Content("~/") %>Javascripts/validate/1.9.0/jquery.validate.min.js" type="text/javascript"></script>
<script src="<%=Url.Content("~/") %>Javascripts/jquery.common.js" type="text/javascript"></script>
<script src="<%=Url.Content("~/") %>Javascripts/jquery.ui.pager.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        //$("#pager").pager({ align: "center", pageSize: 15, count: 300, currentPageIndex: 3, type: "get", pageIndexParamName: "page", mode: 1 });

        $(".tab_box_header > li").each(function (index) {
            $(this).click(function () {
                $(this).addClass("hover").siblings().removeClass("hover");
                $(".tab_box_container").addClass("hide").eq(index).removeClass("hide");
            });
        });
    })
   </script>

