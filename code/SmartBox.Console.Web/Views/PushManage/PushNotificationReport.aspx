<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>推送日志查询</title>
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
    <script src="../../Javascripts/JqueryUI/jquery-ui-1.8.13.custom.min.js" type="text/javascript"></script>
    <link href="../../Javascripts/JqueryUI/css/start/jquery-ui-1.8.13.custom.css" rel="stylesheet"
        type="text/css" />
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
        .ctoolbar 
        {
            line-height:50px;
            vertical-align:middle;
            padding-top:0px;
        }
        input#SelectAll 
        {
            padding-top:0px;
        }

    </style>
    <script language="javascript" type="text/javascript">
        function ViewBodyClick(id) {
            var txt = $('#ApplicationPackageList tr#row' + id + ' td:nth-child(7) div').html();
            $('#dialogMsg').html(txt);
            $('#dialogMsg').dialog('open');
            
        }

        $("document").ready(function () {
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 174;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("QueryPushNotificationList")%>',
                colModel: [
                    { display: '操作', name: 'operate', width: 105, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<input type='checkbox' id=" + pid + " \/>");
                        op.push("<a onclick='ViewBodyClick(\"" + pid + "\")' style='cursor:pointer;color:blue' >查看消息体</a>");
                        return op.join('&nbsp;&nbsp;')
                    }
                    },

                /*{ display: 'id', name: 'id', width: 140, sortable: false, align: 'left' },*/
                    {display: '消息ID', name: 'NotificationID', width: 50, sortable: false, align: 'left' },
                    { display: '推送结果代码', name: 'ReportCode', width: 100, sortable: false, align: 'left' },
                    { display: '推送结果说明', name: 'ReportMessage', width: 100, sortable: false, align: 'left' },
                    { display: '应用代码', name: 'AppCode', width: 100, sortable: false, align: 'left' },
                    { display: '设备唯一序列号', name: 'DeviceToken', width: 130, sortable: false, align: 'left' },
                    { display: '发送的消息体', name: 'Payload', width: 380, sortable: false, align: 'left' },
                    { display: '消息流水号', name: 'NotificationIdentifier', width: 130, sortable: false, align: 'left' },
                    { display: '消息推送过期时间', name: 'ExpirationData', width: 130, sortable: false, align: 'left' },
                    { display: '优先级', name: 'Priority', width: 50, sortable: false, align: 'left'}/*, 
                    { display: '类型', name: 'type', width: 70, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        switch (value) {
                            case "Plugin":
                                return "插件";
                            case "Main":
                                return "主程序";
                            default:
                                return "未同步";
                        }
                    }
                    }*/
				],
                usepager: true,
                rp: 9,
                rowbinddata: true
            };

            var grid = $("#ApplicationPackageList").flexigrid(option);
            autosize_flexgrid("#ApplicationPackageList");
            $(window).resize(function () {
                autosize_flexgrid("#ApplicationPackageList");
            });

            $("div#Save").click(function () {
                if (!confirm('确定删除吗？'))
                    return;
                $("#loadingpannel").html("正在处理数据...").show();
                var ids;
                var cb = document.getElementsByTagName("input");
                for (var i = 0; i < cb.length; i++) {
                    if (cb[i] !== undefined && cb[i].type == "checkbox") {
                        if (cb[i].id != 'undefined' && cb[i].id != 'SelectAll' && cb[i].checked == true) {
                            ids += cb[i].id + ',';
                        }
                    }
                }
                ids = ids.replace('undefined,,,,,,,,', '');

                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("DeletePushNotifications") %>',
                    data: { ids: ids },
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

            //$('.Save').css('margin-top', '7px');
            //$('div.bbit-grid').css('height', '392px');
            //$('div.bbit-grid .bDiv').css('height', '329px');

            $('#dialogMsg').dialog({
                title: "消息体",
                autoOpen: false
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
            正在处理数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>        
    </div>     
     <div style="padding: 1px;">
           <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">推送日志查询</span>
            </div>
        </div> 
        <div id="caltoolbar" class="ctoolbar">
            <div id="Save" class="fbutton">
                <div>
                    <span title='保存' class="Save">删除</span></div>
            </div>     
            <div style="margin-top:-8px">
                  <input type='checkbox'  id='SelectAll' onchange='DoSelect()'/>全选
            </div>
        </div>
        <div>
            <table id="ApplicationPackageList" style="display: none;">
            </table>
        </div>
    </div>

    <div id="dialogMsg">
    asdf
    </div>
          <style>
.Save 
        {
            margin-top:20px;
        }
    </style>          
</body>
</html>
