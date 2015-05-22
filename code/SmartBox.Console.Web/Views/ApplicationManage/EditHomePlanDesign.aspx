<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EditHomePlanDesign</title>
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
    <%}
       int cellWidth = 110; %>
    <style type="text/css">
        .title
        {
            background-color: #60a6cf;
            height: 24px;
            line-height: 24px;
            font-size: 12px;
        }
        .title strong
        {
            font-size: 16px;
        }
        div#divPlans
        {
            height: 170px;
            margin: 5px 0px;
            padding: 0px 10px;
            width: <%= (cellWidth+7)*Convert.ToInt32(ViewData["PageW"])*6 %>px;
        }
        div#divPlans table
        {
            float: left;
            margin: 5px;
        }
        table, td
        {
            border: 1px;
            border-style: solid;
            border-collapse: collapse;
        }
        .tabCell, .tabCell td
        {
            border: 0px;
            border-style: none;
            border-collapse: collapse;
        }
        #divPlans td
        {
            width: <%= cellWidth %>px;
            height: <%= cellWidth %>px;
            text-align:center;
            vertical-align:middle;
        }
        .divOperate
        {
            position: absolute;
            display: block;
            z-index: 2;
            background-image: url(<%= Url.Content("~/Images/bgs/bg-blank.gif")%>);
        }
        
        #divCreatePlan
        {
            display: inline;
            width: 300px;
            height: 400px;
            position: absolute;
            z-index: 9999;
            background-color: White;
        }
        #divCreatePlan .Design
        {
            display: inline-block;
            width: 105px;
            height: 105px;
            text-align: left;
            vertical-align: top;
            border: 1px;
            border-style: solid;
            border-collapse: collapse;
            margin: 5px;
        }
        #divCreatePlan .Design td
        {
            width: 50px;
            height: 50px;
            background-color: Gray;
        }
        .form
        {
            width: 100%;
            background-color: #fff;
            margin: 0px;
            padding: 0px;
        }
        .notNull
        {
            color: #F00;
        }
        div.checkboxlist div.list
        {
            float: left;
            margin: 2px 2px 0px 3px;
        }
        .planDiv
        {
            background-color: #ffffff;
            position: relative;
            color: #000000;
            margin: 0px;
            padding: 0px;
            border: 0px;
            border-style: solid;
            border-collapse: collapse;
            margin: 0px;
            position: absolute;
            display: block;
            text-align: center;
            vertical-align: middle;
        }
        .cell-name
        {
            text-align: right;
            vertical-align: middle;
        }
        .cell-value
        {
            text-align: left;
        }
    </style>
    <script language="javascript" type="text/javascript">
    <%  int pageWidth = Convert.ToInt32(ViewData["PageW"]);
        int pageHeight = Convert.ToInt32(ViewData["PageH"]); %>
        $("document").ready(function () {
            var pageW = <%=pageWidth %>, pageH = <%=pageHeight %>;
            $("a#Change").click(function() {
                var chk = new Array();
                $(".chkReplace").each(function(){
                    if ($(this).attr("checked")) {
                        chk.push($(this).val());
                    }
                });
                if (chk.length < 2) {
                    alert("必须选择两个应用才能交换位置");
                    return;
                }
                if (chk.length > 2) {
                    alert("只能选择两个应用交换位置");
                    return;
                }
                $.ajax({
                    url: '<%= Url.Action("InterchangeHomePlanDesign") %>',
                    type: "post",
                    data: {
                        pageW: <%=pageWidth %>,
                        pageH: <%=pageHeight %>,
                        planID: $("#PlanID").val(),
                        app1ID: chk[0],
                        app2ID: chk[1]
                    },
                    dataType: "json",
                    success: function (data) {
                        if (data.IsSuccess) {
                            window.location.reload();
                        }
                        else {
                            alert(data.Msg);
                        }
                    },
                    error: function () {
                        alert("发生错误");
                    }
                });
            });
            $("#CloseImgBtn1").click(function () {
                CloseModelWindow(null, true);
            });

            $(".btnCreateDesign").click(function () {
                var page = parseInt($(this).attr("page"));
                var x = parseInt($(this).attr("x"));
                var y = parseInt($(this).attr("y"));

                $("#divCreatePlan").css("top", $(this).offset().top + 10).css("left", $(this).offset().left).show().
                find(".Design").attr("x", x).attr("y", y).attr("page", page).each(function () {
                    var tpage = parseInt($(this).attr("page"));
                    var tx = parseInt($(this).attr("x"));
                    var ty = parseInt($(this).attr("y"));
                    var tw = parseInt($(this).attr("w"));
                    var th = parseInt($(this).attr("h"));
                    if (!CheckPlan(pageW, pageH, tpage, tx, ty, tw, th)) {
                        $(this).hide();
                    }
                });
            });

            $(".Design").click(function () {
                if (!$("#AppImage").attr("imgID")) {
                    alert("请选择图片");
                    return;
                }

                $.ajax({
                    url:'<%= Url.Action("CreateHomePlanDesign") %>',
                    type:"post",
                    data:{
                        pageW:3,
                        pageH:3,
                        page:parseInt($(this).attr("page")),
                        x:parseInt($(this).attr("x")),
                        y:parseInt($(this).attr("y")),
                        w:parseInt($(this).attr("w")),
                        h:parseInt($(this).attr("h")),
                        planID:$("#PlanID").val(),
                        appID:$("#AppList").val(),
                        imgID:$("#AppImage").attr("imgID")
                    },
                    dataType:"json",
                    success:function(data){
                        if (data.IsSuccess) {
                            window.location.reload();
                        }
                        else {
                            alert(data.Msg);
                        }
                    },
                    error:function(){
                        alert("发生错误");
                    }
                });
            });

            $("#btnCloseDialog").click(function () {
                ResetDesignDialog();
            });

            $("#btnAppImage").click(function () {
                var url = '<%= Url.Action("SelectImage","ImageManage") %>';
                var imgID = showModalDialog(url,"", 'dialogHeight=500;dialogWidth=700;center=yes;help=no; scroll=yes;resizable=no;status=no');
                if (imgID) {
                    $("#AppImage").attr("imgID", imgID).attr("src", '<%= Url.Action("ViewImage","ImageManage") %>/' + imgID);
                }
            });

            <% IList<SmartBox.Console.Common.Entities.HomePlanDesign> designList = ViewData["DesignList"] as IList<SmartBox.Console.Common.Entities.HomePlanDesign>;
               if (designList != null)
               {
                   foreach (SmartBox.Console.Common.Entities.HomePlanDesign design in designList)
                   {
                       string[] location = design.Location.Split(',');
                       string[] size = design.Size.Split(',');
                       int page = (Convert.ToInt32(location[0]) / 3)+1;
                       int x = Convert.ToInt32(location[0]) % 3;
                       int y = Convert.ToInt32(location[1]);
                       int w = Convert.ToInt32(size[0]);
                       int h = Convert.ToInt32(size[1]);
                       int index = design.ValueUri.LastIndexOf('/');
                       string imgID = design.ValueUri.Substring(index + 1);
                       string content = string.Format("<img class=\"imgApp\" src=\"{0}/{1}\" width=\"{2}px\" height=\"{3}px\">"+
                       "<div class=\"divOperate\" style=\"width:{2}px;height:{3}px;display:none;\">"+
                           "<input type=\"checkbox\" class=\"chkReplace\" value=\"{4}\" style=\"float:left;\" />"+
                           "<input type=\"button\" class=\"btnDelete\" appID=\"{4}\" value=\"删除\" style=\"float:right;\" />"+
                       "</div>",
                        Url.Action("ViewImage", "ImageManage"), 
                        imgID, 
                        w*cellWidth, 
                        h*cellWidth, 
                        design.AppID);
                %>
                <%= String.Format("SetPlan(3, 3,{0} , {1}, {2}, {3}, {4}, '{5}', '{6}');",page,x,y,w,h,design.AppID,content) %>
                <%}
               }%>

            $("#divPlans").find(".imgApp").mouseover(function(){
                var offset = $(this).offset();
                $(this).next(".divOperate").css("top",offset.top).css("left",offset.left).show().mouseout(function(){
                    if (!$(this).find(".chkReplace").attr("checked")) {
                        $(this).hide();
                    }
                });
            });
            
            $(".btnDelete").click(function(){
                var appid = $(this).attr("appID");
                var planid= $("#PlanID").val();
                hiConfirm("你确定要删除吗？", "提示", function (btn) {
                    if (btn == true) {
                        $.ajax({
                            url:'<%= Url.Action("DeleteHomePlanDesign") %>',
                            type:"post",
                            data:{
                                planID:planid,
                                appID:appid
                            },
                            dataType:"json",
                            success:function(data){
                                if (data.IsSuccess) {
                                    window.location.reload();
                                }
                                else {
                                    alert(data.Msg);
                                }
                            },
                            error:function(){
                                alert("发生错误");
                            }
                        });
                    }
                });
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function SetPlan(pageW, pageH, page, x, y, w, h, appID, content) {
            if (!CheckPlan(pageW, pageH, page, x, y, w, h)) {
                return;
            }
            var table = $("#divPlans").find("table[page='" + page + "']");
            for (var j = 0; j < h; j++) {
                for (var i = 0; i < w; i++) {
                    if (i == 0 && j == 0) {
                        continue;
                    }
                    $(table).find("tr[y='" + (y + j) + "']").find("td[x='" + (x + i) + "']").remove();
                }
            }
            $(table).find("tr[y='" + y + "']").find("td[x='" + x + "']").attr("rowspan", h).attr("colspan", w).attr("appID", appID).html(content);
        }

        function CheckPlan(pageW, pageH, page, x, y, w, h) {
            if (x + w > pageW) {
                return false;
            }
            if (y + h > pageH) {
                return false;
            }
            var table = $("#divPlans").find("table[page='" + page + "']");
            for (var j = y; j < y + h; j++) {
                for (var i = x; i < x + w; i++) {
                    var tr = $(table).find("tr[y='" + j + "']");
                    var td = $(tr).find("td[x='" + i + "']");
                    if (tr.length <= 0 || td.length <= 0 || td.attr("appID")) {
                        return false;
                    }
                }
            }
            return true;
        }

        function ResetDesignDialog() {
            $("#divCreatePlan").hide().
            find(".Design").removeAttr("x").removeAttr("y").removeAttr("page").each(function () {
                $(this).show();
            });
            $("#AppImage").removeAttr("imgID").removeAttr("src");
        }

    </script>
</head>
<body>
    <div class="ajaxmsgpanel">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>
    </div>
    <div class="toolBotton">
        <a id="Change" class="imgbtn"><span class="Save" title="交换">交换</span></a> <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
    </div>
    <div id="tdPlans">
        <div id="divPlans">
            <% 
                int pageCount = 6;
                int pageWidth = Convert.ToInt32(ViewData["PageW"]);
                int pageHeight = Convert.ToInt32(ViewData["PageH"]);
                for (int i = 0; i < pageCount; i++)
                { %>
            <table page="<%= i+1 %>">
                <% for (int y = 0; y < pageHeight; y++)
                   { %>
                <tr y="<%= y %>">
                    <% for (int x = 0; x < pageWidth; x++)
                       { %>
                    <td x="<%= x %>">
                        <input type="button" class="btnCreateDesign" page="<%= i+1 %>" x="<%= x %>" y="<%= y %>"
                            value="新建" />
                    </td>
                    <%} %>
                    <td style="width: 0px; margin: 0px; padding: 0px;">
                    </td>
                </tr>
                <%} %>
                <tr style="height: 0px">
                    <% for (int j = 0; j < pageWidth; j++)
                       {%>
                    <td style="height: 0px; margin: 0px; padding: 0px;">
                    </td>
                    <%} %>
                </tr>
                <tr>
                    <td colspan="<%= pageWidth %>" style="height: 15px;">
                        第<%=i+1 %>页
                    </td>
                </tr>
            </table>
            <%} %>
        </div>
    </div>
    <div id="divCreatePlan" style="display: none;">
        <table width="100%" cellspacing="1" cellpadding="0">
            <tr>
                <td class="cell-name">
                    应用:
                </td>
                <td class="cell-value">
                    <%= Html.DropDownList("AppList") %>
                </td>
            </tr>
            <tr>
                <td class="cell-name">
                    <input id="btnAppImage" type="button" value="选择图片" />
                </td>
                <td class="cell-value">
                    <img width="50px" height="50px" alt="" id="AppImage" />
                </td>
            </tr>
            <tr>
                <td class="cell-value" colspan="2">
                    <div class="Design" w="1" h="1">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Design" w="2" h="1">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Design" w="1" h="2">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Design" w="2" h="2">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="title" align="center">
                    <input type="button" id="btnCloseDialog" value="关闭" />
                </td>
            </tr>
        </table>
    </div>
    <%=Html.Hidden("PlanID", ViewData["PlanID"])%>
</body>
</html>
