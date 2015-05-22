<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>安装包管理</title>
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
        <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {
            SimplyButtons.init();
            var ids = '<% =ViewData["ids"] %>';
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryPackageExtList")%>',
                colModel: [
                    { display: '操作', name: 'operate', width: 120, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<a onclick='DeletePackageClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >删除</a>");
                        if (ids.indexOf(pid) == -1) {
                            op.push("<a id=" + pid + " onclick='UpdatePackageClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >更新</a>");
                        }

                        op.push("<a onclick='ModifyPackageClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >修改</a>");
                        op.push("<a onclick='ViewPackageExtClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >信息</a>");

                        return op.join('&nbsp;&nbsp;')
                    }
                    },
                { display: '安装包名称', name: 'pe_Name', width: 100, sortable: false, align: 'left' },
                    { display: '安装包类型', name: 'TableName', width: 75, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        switch (value) {
                            case "SMC_Package4Out":
                                return "外部安装包";
                            case "Package4AI":
                                return "内部安装包";
                            case "WebApplication":
                                return "轻应用";
                            default:
                                return "";
                        }
                    }
                    },
                   { display: '上架状态', name: 'pe_UsefulStstus', width: 70, sortable: false, align: 'left', process: function (value, pid) {
                       var op = new Array();
                       switch (value) {
                           case "True":
                               return "已上架&nbsp;&nbsp;" + "<a onclick='DisEnableClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >下架</a>";
                           case "False":
                               return "已下架&nbsp;&nbsp;" + "<a onclick='EnableClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >上架</a>";
                           default:
                               return "已下架&nbsp;&nbsp;" + "<a onclick='EnableClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >上架</a>";
                       }
                   }
                   },
                    { display: '客户端类型', name: 'pe_ClientType', width: 100, sortable: false, align: 'left' },
                    { display: '单位名称', name: 'pe_UnitName', width: 120, sortable: false, align: 'left' },
                    { display: '分类', name: 'pe_Category', width: 100, sortable: false, align: 'left' },
                    { display: '下载量', name: 'pe_DownCount', width: 40, sortable: false, align: 'left' },
                    { display: '推荐', name: 'pe_IsTJ', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        switch (value) {
                            case "True":
                                return "推荐";
                            case "False":
                                return "不推荐";
                            default:
                                return "不推荐";
                        }
                    }
                    },
                    { display: '必备', name: 'pe_IsBB', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        switch (value) {
                            case "True":
                                return "必备";
                            case "False":
                                return "不必备";
                            default:
                                return "不必备";
                        }
                    }
                    },

                    { display: '创建时间', name: 'pe_CreatedTime', width: 100, sortable: true, align: 'left', hide: true },


                    { display: 'ID', name: 'ID', width: 50, sortable: false, align: 'left', hide: true }
				],
                usepager: true,
                rp: 20,
                rowbinddata: true,
                onSuccess: HideA
            };

            var grid = $("#ApplicationPackageList").flexigrid(option);



            $("#AddWebp").click(function () {
                url = '<%=Url.Action("EditWebApplication") %>';
                OpenModelWindow(url, { width: 700, height: 400, caption: "新增Web应用", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });

            $("#AddPin").click(function () {
                url = '<%=Url.Action("CreateApplicationPackage") %>';
                OpenModelWindow(url, { width: 700, height: 400, caption: "新增内部安装包", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });

            $("#ImportPackage").click(function () {
                url = '<%=Url.Action("ImportPackage") %>';
                OpenModelWindow(url, { width: 700, height: 450, caption: "导入内部安装包", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });

            $("#AddPout").click(function () {
                url = '<%=Url.Action("CreateOutApplicationPackage") %>';
                OpenModelWindow(url, { width: 700, height: 450, caption: "新增外部安装包", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });
            $("#AddIOSout").click(function () {
                url = '<%=Url.Content("~/IOSOutsideAppManage/EditIOSOutsideApp") %>';
                OpenModelWindow(url, { width: 700, height: 450, caption: "新增IOS外部应用", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });

            $("#AddIOS").click(function () {
                url = '<%=Url.Action("EditWebApplication") %>';
                OpenModelWindow(url, { width: 700, height: 400, caption: "IOS外部应用管理", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
            });
            autosize_flexgrid("#ApplicationPackageList");
            $(window).resize(function () {
                autosize_flexgrid("#ApplicationPackageList");
            });
        });                                                                             // end of ready

        function HideA()
        {          
            
         }

        function ModifyPackageClick(id) {
            url = '<%=Url.Action("ModifyPackageExt") %>/' + id;             
            OpenModelWindow(url, { width: 750, height: 450, caption: "修改安装包信息", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }

        function UpdatePackageClick(id) {
            url = '<%=Url.Action("UpdatePackage") %>/' + id;
            OpenModelWindow(url, { width: 750, height: 400, caption: "更新应用信息", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }

        function ViewPackageExtClick(id) {           
            url = '<%=Url.Action("ViewPackage2") %>/' + id;
            OpenModelWindow(url, { width: 750, height: 500, caption: "查看包信息", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }
        

        function EditPackageCollectionClick(id) {
            url = '<%=Url.Action("PackageCollectionList") %>/' + id;            
            window.location.href = url;
        }

        function EditPackageGifClick(id) {
            url = '<%=Url.Action("PackageGifList") %>/' + id;
            window.location.href = url;
        }

        function EditPackageFAQClick(id) {
            url = '<%=Url.Action("PackageFAQList") %>/' + id ;
            window.location.href = url;
        }

        function EditPackageManualClick(id) {
            url = '<%=Url.Action("PackageManualList") %>/' + id ;
            window.location.href = url;
        }

        function EditPackageCodeClick(id) {
            url = '<%=Url.Action("PackageCode") %>/' + id ;
            OpenModelWindow(url, { width: 750, height: 400, caption: "生成二维码", onclose: function () { $("#ApplicationPackageList").flexReload(); } });
        }

        function DeletePackageClick(id) {            
            hiConfirm("你确定要删除该安装包吗？", "提示", function (btn) {
                if (btn == true) {
                    $("#loadingpannel").html("正在执行......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("DeletePackageExt") %>',
                        data: { pe_id: id },
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

        function EnableClick(id) {
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetUserfulStatus") %>',
                data: { id: id, Operation: "ENABLE" },
                dataType: "json",
                success: function (data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        flushGrid();
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            });
        }

        function DisEnableClick(id) {
            $("#loadingpannel").html("正在执行......").show();
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("SetUserfulStatus") %>',
                data: { id: id, Operation: "DISABLE" },
                dataType: "json",
                success: function (data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        //hiAlert("操作成功", true);
                        flushGrid();
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
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
       
    </style>
</head>
<body>
<div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
    <div style="padding: 1px;">

     
    <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">应用发布管理</span>
            </div>
        </div>  

        <div id="caltoolbar" class="ctoolbar">
            <div id="AddWebp" class="fbutton">
                <div>
                    <span title='新增Web应用' class="Add">新增轻应用</span></div>
            </div> 
             <div id="AddPin" class="fbutton">
                <div>
                    <span title='新增插件' class="Add">新增app</span></div>
            </div>  
            <div id="ImportPackage" class="fbutton">
                <div>
                    <span title='导入插件' class="Add">导入app</span></div>
            </div> 
             <div id="AddPout" class="fbutton">
                <div>
                    <span title='新增外部程序' class="Add">新增外部app</span></div>
            </div> 
            <div id="AddIOSout" class="fbutton">
                <div>
                    <span title='新增ios外部程序' class="Add">新增ios外部app</span></div>
            </div>        
        </div>
        <div>
            <table id="ApplicationPackageList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>
