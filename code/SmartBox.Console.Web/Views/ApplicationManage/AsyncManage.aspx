<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>同步数据</title>
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
    <script language="javascript" type="text/javascript">
        $("document").ready(function () {
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryPackageExtAsyncList")%>',
                colModel: [                    
                    { display: '操作', name: 'operate', width: 25, sortable: false, align: 'center', process: function (value, pid) {
                        var op = new Array();
                        op.push("<input type='checkbox' id=" + pid + " \/>");
                        return op.join('&nbsp;&nbsp;')
                    }
                    },
                    { display: '安装包类型', name: 'TableName', width: 100, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        switch (value) {
                            case "SMC_Package4Out":
                                return "外部安装包";
                            case "Package4AI":
                                return "内部安装包";
                            case "WebApplication":
                                return "Web应用";
                            default:
                                return "";
                        }
                    }
                    },
                    { display: '安装包名称', name: 'pe_Name', width: 140, sortable: false, align: 'left' },
//                    { display: '同步状态', name: 'pe_AsyncStatus', width: 70, sortable: false, align: 'left', process: function (value, pid) {
//                        var op = new Array();
//                        switch (value) {
//                            case "nonasync":
//                                return "未同步";
//                            case "async":
//                                return "已同步";                         
//                            default:
//                                return "未同步";
//                        }
//                    }
//                    },
                    { display: '客户端类型', name: 'pe_ClientType', width: 100, sortable: false, align: 'left' },
                    { display: '分类', name: 'pe_Category', width: 100, sortable: false, align: 'left' },
                    { display: '创建时间', name: 'pe_CreatedTime', width: 120, sortable: true, align: 'left' },
                    { display: 'ID', name: 'ID', width: 50, sortable: false, align: 'left', hide: true }
				],
                usepager: true,
                rp: 20,
                rowbinddata: true
            };

            var grid = $("#ApplicationPackageList").flexigrid(option);

            $("div#Save").click(function () {
                $("#loadingpannel").html("正在同步数据...").show();
                var ids;
                var cb = document.getElementsByTagName("input");
                for (var i = 0; i < cb.length; i++) {
                    if (cb[i].type == "checkbox") {
                        if (cb[i].checked == true) {
                            ids += cb[i].id + ',';
                        }
                    }
                }

                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("AsyncManage") %>',
                    data: { pe_ids: ids },
                    dataType: "json",
                    success: function (data) {
                        $("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            hiAlert(data.Msg, "操作成功");
                        }
                        else {
                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });
            });

            autosize_flexgrid("#ApplicationPackageList");
            $(window).resize(function () {
                autosize_flexgrid("#ApplicationPackageList");
            });
        });
        function DoSelect() {
            var c = document.getElementById("SelectAll").checked;           
            var cb = document.getElementsByTagName("input");
            if (c == true) {
                for (var i = 0; i < cb.length; i++) {
                    if (cb[i].type == "checkbox")
                        cb[i].checked = true;
                }　　
            } else {
                for (var i = 0; i < cb.length; i++) {
                    if (cb[i].type == "checkbox")
                        cb[i].checked = false;
                }　　
             }
        }        
    </script>
</head>
<body>
    <div class="ajaxmsgpanel">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在同步数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>        
    </div>     
     <div style="padding: 1px;">
           <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">同步管理</span>
            </div>
        </div>  

        <div id="caltoolbar" class="ctoolbar">
            <div id="Save" class="fbutton">
                <div>
                    <span title='保存' class="Save">同步选中项</span></div>
            </div>   
            <div style="padding-top:8px">  
                  <input type='checkbox'  id='SelectAll' onchange='DoSelect()'/>全选
            </div>
        </div>
        <div>
            <table id="ApplicationPackageList" style="display: none;">
            </table>
        </div>
    </div>
                    
</body>
</html>
