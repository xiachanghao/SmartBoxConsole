<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分类管理</title>
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
    <%--<script type="text/javascript" src="<%=Url.Content("~/") %>jquery-easyui-1.3.6/jquery.min.js"></script>--%>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.autocomplete.js")%>" type="text/javascript"
        defer="defer"></script>
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 5; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryApplicationCategoryList")%>',
                colModel: [
                    { display: '操作', name: 'Operate', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<a onclick='UpdateCategoryClick(\"" + value + "\")' style='cursor:pointer;color:blue' >修改</a>");
                        op.push("<a onclick='DeleteCategoryClick(\"" + value + "\")' style='cursor:pointer;color:blue' >删除</a>");
                        return op.join('&nbsp;');
                    }
                    },
                    { display: '名称', name: 'Name', width: 120, sortable: false, align: 'left' },
                    { display: '显示名称', name: 'DisplayName', width: 120, sortable: false, align: 'left' },
                    { display: '排序号', name: 'Seq', width: 100, sortable: false, align: 'left' },
                    { display: '创建者', name: 'CreateUid', width: 120, sortable: false, align: 'left' },
                    { display: '创建时间', name: 'CreateTime', width: 120, sortable: false, align: 'left' },
                    { display: '修改者', name: 'UpdateUid', width: 120, sortable: false, hide: true, align: 'left' },
                    { display: '修改时间', name: 'UpdateTime', width: 120, sortable: false, hide: true, align: 'left' },
                    { display: 'ID', name: 'PublishId', width: 120, sortable: false, align: 'left', hide: true }
				],
                sortname: "CreateTime",
                sortorder: "asc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };

            var grid = $("#CategoryList").flexigrid(option);

            $("span.Add").click(function () {
                url = '<%=Url.Action("EditApplicationCategory") %>';
                OpenModelWindow(url, { width: 400, height: 300, caption: "新增", onclose: function () { $("#CategoryList").flexReload(); } });
            });
            autosize_flexgrid("#CategoryList");
            $(window).resize(function () {
                autosize_flexgrid("#CategoryList");
            });
        }); // end of ready

        function UpdateCategoryClick(id) {
            url = '<%=Url.Action("EditApplicationCategory") %>/' + id;
            OpenModelWindow(url, { width: 400, height: 300, caption: "修改", onclose: function () { $("#CategoryList").flexReload(); } });
        }

        function DeleteCategoryClick(id) {
            hiConfirm("你确定要删除该分类吗？", "提示", function (btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在执行......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("DeleteApplicationCategory") %>',
                        data: { id: id },
                        dataType: "json",
                        success: function (data) {
                            $("#loadingpannel").hide();
                            if (data.IsSuccess) {
                                hiAlert("操作成功", true);
                                flushGrid();
                            }
                            else {
                                hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                            }
                        }
                    }); //end if $.ajax
                }
            });
        }

        function flushGrid() {
            $("#CategoryList").flexReload();
        } // end of flushGrid
        //查看详细信息

    </script>
    <style type="text/css">
        .tdlabel
        {
            width: 12%;
            text-align: right;
        }
        .tdinput
        {
            width: 30%;
        }
        .tdquery
        {
            text-align: center;
        }
        .qclass
        {
            border: solid 1px #99bbe8;
            border-top: none;
        }
        .qclass input
        {
            width: 90%;
            border: solid 1px #ccc;
        }
        
        input.autocomplete
        {
            border: solid 1px #99bbe8;
        }
        tr.trtop td
        {
            border-top: solid 1px #ccc;
        }
        td.querytd
        {
            border-left: solid 1px #ccc;
            text-align: center;
        }
        .bspan
        {
            background-color: #E8F1F8 !important;
        }
    </style>
</head>
<body>
<div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
    <div style="padding: 1px;">
        <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">应用分类管理</span>
            </div>
        </div>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
        </div>
        <div>
            <table id="CategoryList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
