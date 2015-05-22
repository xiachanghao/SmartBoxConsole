<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>同步结果</title>
    <metahttp-equiv="X-UA-Compatible"content="IE=edge"/>
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
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/My97DatePicker/WdatePicker.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>

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
            border-width:0px !important;
        }
        input.Wdate 
        {
            width:144px;
        }

    </style>
    <script language="javascript" type="text/javascript">
        function loadData() {
            
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // 减去边框和左边的宽度
            var otherpm = 0;
            var gh = maiheight - otherpm;
            var packageName = $('#tbName').val();
            packageName = encodeURIComponent(packageName);
            var option = {
                
                width: mainWidth,
                url: '<%=Url.Action("QueryPackageAsyncResultList")%>?sync_bat_no=<%=Request.QueryString["sync_bat_no"]%>&sync_time_start=<%=Request.QueryString["sync_time_start"]%>&sync_time_end=<%=Request.QueryString["sync_time_end"]%>&sync_status=<%=Request.QueryString["sync_status"]%>&packageName=' + packageName,
                colModel: [
                    { display: '操作', name: 'id', width: 25, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        op.push("<input type='checkbox' id=" + pid + " \/>");
                        return op.join('&nbsp;&nbsp;')
                    }
                    },
                    { display: '安装包编码', name: 'pe_id', width: 140, sortable: false, align: 'left' },
                    { display: '安装包名称', name: 'pe_name', width: 100, sortable: false, align: 'left' },
                    { display: '批次', name: 'sync_bat_no', width: 140, sortable: false, align: 'left' },
                    { display: '同步时间', name: 'sync_Time', width: 120, sortable: true, align: 'left' },
                    { display: '状态', name: 'sync_status', width: 100, sortable: false, align: 'left', process: function (value, pid) {
                        var op = new Array();
                        //alert(value);
                        switch (value.toLowerCase()) {
                            case "false":
                                return "失败";
                            case "true":
                                return "成功";
                            default:
                                return "";
                        }
                    }
                    },
                //                    { display: '操作人账号', name: 'sync_user_uid', hide: true,width: 140, sortable: false, align: 'left' },
                //                    { display: '操作人名称', name: 'sync_user_name', hide: true, width: 140, sortable: false, align: 'left' },
                    {display: '说明', name: 'description', width: 100, sortable: false, align: 'left' }

				],
                usepager: true,
                rp: 10,
                rowbinddata: true

            };

            var grid = $("#ApplicationPackageList").flexigrid(option);            
        }
        $("document").ready(function () {
            SimplyButtons.init();
            var packageName = '<%=Request.QueryString["packageName"]%>';
            $('#tbName').val(packageName);
            var sync_time_start = '<%=Request.QueryString["sync_time_start"]%>';
            $('#tbTimeStart').val(sync_time_start);
            var sync_time_end = '<%=Request.QueryString["sync_time_end"]%>';
            $('#tbTimeEnd').val(sync_time_end);
            var sync_status = '<%=Request.QueryString["sync_status"]%>';
            $('#selStatus option[value="' + sync_status + '"]').attr('selected', 'selected');
            var sync_bat_no = '<%=Request.QueryString["sync_bat_no"]%>';
            $('#tbBatNo').val(sync_bat_no);

            loadData();

            

            $('div#Search').click(function () {
                var batNo = $('#tbBatNo').val();
                var packageName = $('#tbName').val();
                packageName = encodeURIComponent(packageName);
                var sync_status = $('#selStatus').val();
                var sync_time_start = $('#tbTimeStart').val();
                var sync_time_end = $('#tbTimeEnd').val();
                var url = '<%=Url.Content("~/ApplicationManage/AsyncResultList")%>?sync_bat_no=' + batNo + '&packageName=' + packageName + '&sync_status=' + sync_status + '&sync_time_start=' + sync_time_start + '&sync_time_end=' + sync_time_end;
                window.location = url;
            });

            $("div#Save").click(function () {
                $("#loadingpannel").html("正在删除数据...").show();
                var ids = '';
                var cb = document.getElementsByTagName("input");
                for (var i = 0; i < cb.length; i++) {
                    if (cb[i].type == "checkbox") {
                        if (cb[i].checked == true) {
                            if (cb[i].id == 'SelectAll' || cb[i].id == '')
                                continue;
                            ids += cb[i].id + ',';
                        }
                    }
                }

                if (ids == '') {
                    $("#loadingpannel").html("正在删除数据...").hide();
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("DeleteAsyncResult") %>',
                    data: { ids: ids },
                    dataType: "json",
                    success: function (data) {
                        $("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            hiAlert(data.Msg, "操作成功", function () { window.location.reload(); });

                        }
                        else {
                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });
            });

            $('div.hDivBox table th:nth-child(1) div').html('<INPUT id=SelectAll onclick=DoSelect() value="" type=checkbox nodeIndex="2">');
            autosize_flexgrid("#ApplicationPackageList",246);
            $(window).resize(function () {
                autosize_flexgrid("#ApplicationPackageList", 246);
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
    <link href="<%=Url.Content("~/Themes/Shared/SearchPanel.css") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="ajaxmsgpanel">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在同步数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>        
    </div>   
    
    <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">安装包同步结果查询</span>
            </div>
        </div>  
          
     <div style="padding: 1px;">
           <table>
                <tr>
                    <td>名称：</td>
                    <td><input id="tbName" /></td>
                    <td>批次：</td>
                    <td><input id="tbBatNo" /></td>
                </tr>
                <tr>
                    <td>状态：</td>
                    <td>
                        <select id="selStatus">
                            <option value=''>全部</option>
                            <option value='1'>成功</option>
                            <option value='0'>失败</option>
                        </select>
                    </td>
                    <td>时间：</td>
                    <td><input id="tbTimeStart" name="tbTimeStart" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/>-<input id="tbTimeEnd" name="tbTimeEnd" readonly="readonly"  type="text"  class="Wdate" onClick="WdatePicker()"/></td>
                </tr>
                </table>
        <div id="caltoolbar" class="ctoolbar">
            <div id="Search" class="fbutton">
                <div>                
                    <span title='删除' class="Search">查询</span>
                </div>
            </div>
            <div id="Save" class="fbutton">
                <div>                
                    <span title='删除' class="Save">删除选中项</span>
                </div>
            </div>
        </div>
        <div>
            <table id="ApplicationPackageList" style="display: none;">
            </table>
        </div>
    </div>      
</body>
</html>
