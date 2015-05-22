﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>应用安装包管理</title>
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
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.autocomplete.js")%>" type="text/javascript"
        defer="defer"></script>
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
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryApplicationPackageList")%>',
                colModel: [
                    {display: '操作', name: 'operate', width: 260, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<a onclick='EditApplicationPackageClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >修改</a>");
                        op.push("<a onclick='UpdateApplicationPackageClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >更新</a>");
                        op.push("<a onclick='DeleteApplicationPackageClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >删除</a>");
                        op.push("<a onclick='EditPackageCodeClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >二维码</a>");
                        op.push("<a onclick='EditPackageGifClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >截图</a>");
                        op.push("<a onclick='EditPackageFAQClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >反馈</a>");
                        op.push("<a onclick='EditPackageCollectionClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >收藏</a>");
                        op.push("<a onclick='EditPackageManualClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >手册</a>");
                        return op.join('&nbsp;&nbsp;')
                    }
                },
                    { display: '安装包名称', name: 'DisplayName', width: 120, sortable: false, align: 'left' },
                    { display: '安装包类型', name: 'Type', width: 80, sortable: false, align: 'left' },
                    { display: '客户端类型', name: 'DisplayClientType', width: 120, sortable: false, align: 'left' },
                    { display: '安装包发布版本', name: 'Version', width: 100, sortable: false, align: 'left' },
                    { display: '安装包内部版本', name: 'BuildVer', width: 100, sortable: false, align: 'left' },
                    { display: '备注', name: 'Description', width: 200, sortable: false, align: 'left' },
                    { display: 'ID', name: 'ID', width: 50, sortable: false, align: 'left',hide:true }
				],
                usepager: true,
                rp: 20,
                rowbinddata: true
            };

            var grid = $("#ApplicationPackageList").flexigrid(option);

            $("span.Add").click(function () {
                url = '<%=Url.Action("CreateApplicationPackage") %>';
                OpenModelWindow(url, { width: 750, height: 400, caption: "新增应用", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });

            $("span.BatchAdd").click(function () {
                url = '<%=Url.Action("BatchCreateApplicationPackage") %>';
                OpenModelWindow(url, { width: 750, height: 400, caption: "批量新增应用", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });

            
        });                                                       // end of ready

        function EditApplicationPackageClick(id) {
            url = '<%=Url.Action("EditApplicationPackage") %>/' + id;
            OpenModelWindow(url, { width: 750, height: 400, caption: "修改应用信息", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }

        function UpdateApplicationPackageClick(id) {
            url = '<%=Url.Action("UpdatePackage") %>/' + id;
            OpenModelWindow(url, { width: 750, height: 400, caption: "更新应用信息", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }

        function EditPackageCollectionClick(id) {
            url = '<%=Url.Action("PackageCollectionList") %>/' + id + '?TableName=Package4AI';
            window.location.href = url;
            //OpenModelWindow(url, { width: 750, height: 400, caption: "收藏列表", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }

        function EditPackageGifClick(id) {
            url = '<%=Url.Action("PackageGifList") %>/' + id + '?TableName=Package4AI';
            window.location.href = url;
        }

        function EditPackageFAQClick(id) {
            url = '<%=Url.Action("PackageFAQList") %>/' + id + '?TableName=Package4AI';
            window.location.href = url;
        }

        function EditPackageManualClick(id) {
            url = '<%=Url.Action("PackageManualList") %>/' + id + '?TableName=Package4AI';
            window.location.href = url;
        }

        function EditPackageCodeClick(id) {
            url = '<%=Url.Action("PackageCode") %>/' + id + '?TableName=Package4AI';
            OpenModelWindow(url, { width: 750, height: 400, caption: "生成二维码", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }

        function DeleteApplicationPackageClick(id) {
            hiConfirm("你确定要删除该应用吗？", "提示", function (btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在执行......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("DeleteApplicationPackage") %>',
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
            $("#ApplicationPackageList").flexReload();
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
        .BatchAdd
        {
            padding-left: 20px;
            /*background: url(<%=Url.Content("~/images/icons/add.png")%>) no-repeat 1px;*/
        }
        .AddOut
        {          
          
        }
    </style>
</head>
<body>
<div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
    <div style="padding: 1px;">
        <%--<div class="cHead">
            <div class="ftitle">
                <span id="departmentName">应用扩展包列表</span>
            </div>
        </div>--%>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
            <div id="btnBatchAdd" class="fbutton">
                <div>
                    <span title='批量新增' class="BatchAdd">批量新增</span></div>
            </div>
        </div>
        <div>
            <table id="ApplicationPackageList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
