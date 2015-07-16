<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.PluginInfo>" %>

<%@ Import Namespace="SmartBox.Console.Common.Entities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息</title>
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
    <link href="<%=Url.Content("~/Themes/Default/tabs.css") %>" rel="stylesheet" type="text/css" />

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.autocomplete.js")%>" type="text/javascript"
        defer="defer"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.Tabs.js")%>" type="text/javascript"></script>

    <% if (false)
       { %>

    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <%} %>

    <script type="text/javascript">

        $(document).ready(function() {
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var mainWidth = document.documentElement.clientWidth; // 减去边框和左边的宽度
            var maiheight = document.documentElement.clientHeight - 2;

            var otherpm = 210;
            var gh = maiheight - otherpm;

            var option1 = {
                height: gh,
                width: mainWidth - 8,
                url: '<%=Url.Action("GetVersionTrackList")%>/' + '<%=Model.PluginCode%>',
                colModel: [
                    { display: '版本号', name: 'VersionName', width: 100, sortable: false, align: 'left' },
                    { display: '版本说明', name: 'VersionSummary', width: 140, sortable: false, align: 'left' },
                    { display: '状态', name: 'VersionStatus', width: 60, sortable: false, process: GetName, align: 'left' },
			        { display: '发布者', name: 'CreateUid', width: 100, sortable: false, align: 'left' },
			        { display: '发布时间', name: 'CreateTime', width: 130, sortable: false, align: 'left' },
			        { display: '操作', name: 'VersionStatus1', width: 100, sortable: false, process: GetFile, align: 'center' }

				],

                sortname: "CreateTime",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true
            };
            var grid1 = $("#VersionList").flexigrid(option1);

            function GetName(pid, obj) {
                if (pid == "0" || pid == "3")
                    return "未发布";
                else if (pid == "1")
                    return "正在使用";
                else if (pid == "2")
                    return "已过期";
            }

            function GetFile(pid, obj) {
                var arr = pid.split(',');
                if ('<%=Model.PluginCateCode%>' == '<%=Constants.PluginCateCode%>') {
                    //return "<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >下载</a>";
                    return "";
                }
                else {
                    if (arr[0] == "1")
                        return "<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >下载</a>&nbsp;&nbsp;<a onclick='delClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
                    else
                        return "<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >下载</a>&nbsp;&nbsp;<a onclick='delClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>&nbsp;&nbsp;<a onclick='resumeClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >恢复</a>";
                }
            }


            var p = mainWidth % 2;
            $("#tabs").idTabs({
                start: 0,
                width: mainWidth - (8 + p),
                height: maiheight - 70,
                mode: "showall", //全可见
                items: [
                           { contentEl: "Pinfo", name: "插件信息" },
                           { contentEl: "Vinfo", name: "版本历史" }
                        ],
                event: "!click", //"!mouseover",
                click: tabitem_click
            });
            //选项卡点击滑动效果
            function tabitem_click(id) {

                $('#PluginInfoDiv').hide();
                $('#configDiv').hide();
                $('#versionDiv').hide();
                if (id == "#Pinfo")
                    $('#PluginInfoDiv').show();
                else
                    $('#versionDiv').show();
            }

            $('#PluginInfoDiv').show();
            $('#versionDiv').hide();

            $("#tabs").height(maiheight - 100);



        });                        // end of ready

        function delClick(ojb) {
           
            var pluginNames = "";
            var length = $("#VersionList")[0].rows.length;
            var alerts = "";
            var urls = "";
            var status = "";
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("GetPluginNames") %>',
                data: { vid: ojb },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        pluginNames = data.Msg.split('|')[0];
                        status = data.Msg.split('|')[1];
                        //处理删除
                        if (length > 1) {
                            alerts = "你确认要删除" + pluginNames + "插件的当前版本吗？";
                            if (status == "1") {
                                alerts = "你确认要删除当前正在使用的" + pluginNames + "插件的版本吗,删除后该插件将使用上一版本";
                            }
                            urls = '<%=Url.Action("DelVersions") %>';
                        }
                        else {
                            alert('当前已是最后一个版本,若要删除,请删除整个插件！');
                            return false;
                        }
                        hiConfirm(alerts, "提示", function(btn) {
                            if (btn == true) {
                                $("#loadingpannel").html("正在删除......").show();
                                $.ajax({
                                    type: "POST",
                                    url: urls,
                                    data: { vid: ojb },
                                    dataType: "json",
                                    success: function(data) {
                                        $("#loadingpannel").hide();
                                        if (data.IsSuccess) {
                                            hiAlert("删除成功", '提示', function() { $("#VersionList").flexReload(); });
                                        }
                                        else {
                                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                                        }
                                    }
                                }); //end if $.ajax
                            }
                        });   // end of hiConfirm
                        //endendendendendendendendend
                    }
                    else {
                        hiAlert("操作失败");
                    }
                }
            });     //end if $.ajax

        }

        function resumeClick(ojb) {

            var pluginNames = "";
            var length = $("#VersionList")[0].rows.length;
            var alerts = "";
            var urls = "";
            var status = "";
            $.ajax({
                type: "POST",
                url: '<%=Url.Action("GetPluginNames") %>',
                data: { vid: ojb },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        pluginNames = data.Msg.split('|')[0];
                        status = data.Msg.split('|')[1];
                        //处理删除
                        alerts = "你确认要恢复" + pluginNames + "插件的版本吗？";
                        if (status != "2") {
                            alert('当前为已发布的版本或者还未发布的,请选择过期版本!');
                            return false;
                        }
                        urls = '<%=Url.Action("ResumeVersions") %>';
                        hiConfirm(alerts, "提示", function(btn) {
                            if (btn == true) {
                                $("#loadingpannel").html("正在恢复......").show();
                                $.ajax({
                                    type: "POST",
                                    url: urls,
                                    data: { vid: ojb },
                                    dataType: "json",
                                    success: function(data) {
                                        $("#loadingpannel").hide();
                                        if (data.IsSuccess) {
                                            hiAlert("恢复成功", '提示', function() { $("#VersionList").flexReload(); });
                                        }
                                        else {
                                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                                        }
                                    }
                                }); //end if $.ajax
                            }
                        });   // end of hiConfirm
                        //endendendendendendendendend
                    }
                    else {
                        hiAlert("操作失败");
                    }
                }
            });         //end if $.ajax

        }

        function openClick(ojb) {
            $("#vid").val(ojb);
            $("#fmEdit").submit();
        }

       
        
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
    <div class="cHead" style="border: none;">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>
    </div>
    <div id="tabs" class="tabs">
        <div id="tabbody" class="tabsitemcontainer">
            <% using (Html.BeginForm("GetDownLoadFile", "PluginInfoManage", FormMethod.Post, new { id = "fmEdit" }))
               {
                   Html.AntiForgeryToken();
                   Html.ValidationSummary(true);%>
            <div id="PluginInfoDiv">
                <table width="100%" id="Table2" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                开发商：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.CompanyName%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                联系人：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.CompanyLinkman%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                网址：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.CompanyHomePage%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                联系电话：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.CompanyTel%>
                        </td>
                    </tr>
                </table>
                <%
                    if (Model.PluginCateCode.Equals(Constants.PluginCateCode))//若是web插件
                    {%>
                <table width="100%" id="Table1" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件标识：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.PluginCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                插件名：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.DisplayName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件地址：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.PluginUrl%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                是否公开：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%if (Model.IsPublic == true)
                              {%>
                            是
                            <%}
                              else
                              { %>否
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                应用系统：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.AppCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                权限标识：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.PrivilegeCode%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                必须使用：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%if (Model.IsNeed == true)
                              {%>
                            是
                            <%}
                              else
                              { %>否
                            <%} %>
                        </td>
                        <td class="bbit-form-cell-name tdtop" style="width: 225px">
                            <label>
                                默认使用：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%if (Model.IsDefault == true)
                              {%>
                            是
                            <%}
                              else
                              { %>否
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                排序号：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.Sequence%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件描述：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%=Model.Summary%>
                        </td>
                    </tr>
                </table>
                <%}
                    else
                    { %>
                <table width="100%" id="basicinfotable" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件标识：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.PluginCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                插件名：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.DisplayName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                是否公开：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%if (Model.IsPublic == true)
                              {%>
                            是
                            <%}
                              else
                              { %>否
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                应用系统：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.AppCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                权限标识：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.PrivilegeCode%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                必须使用：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%if (Model.IsNeed == true)
                              {%>
                            是
                            <%}
                              else
                              { %>否
                            <%} %>
                        </td>
                        <td class="bbit-form-cell-name tdtop" style="width: 225px">
                            <label>
                                默认使用：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%if (Model.IsDefault == true)
                              {%>
                            是
                            <%}
                              else
                              { %>否
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                启动程序名：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.FileName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                程序集全名：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.TypeFullName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件类型：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.PCname%>
                        </td>
                        <td class="bbit-form-cell-name tdtop" style="width: 225px">
                            <label>
                                排序号：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.Sequence%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                插件描述：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%=Model.Summary%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                版本号：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.Version%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                版本说明：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%=Model.VersionSummary%>
                        </td>
                    </tr>
                </table>
                <%if (Model.PluginCateCode.Equals(Constants.ActionCateCode))
                  {%>
                <table width="100%" id="Table4" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                扩展标识：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.ActionCode%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                扩展描述：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.ActionSummary%>
                        </td>
                    </tr>
                </table>
                <%} %>
                <table width="100%" id="tablist" class="bbit-form" style="background-color: White"
                    cellspacing="0" cellpadding="1">
                    <tr style="background: #ccc url(../../Themes/Default/images/flexigrid/gridth.gif) repeat-x left center;">
                        <td class="tdtop tdleft" style="width: 30%">
                            关键值
                        </td>
                        <td class="tdtop" style="width: 70%">
                            上传值
                        </td>
                    </tr>
                    <%
                        IList<ConfigInfo> list = Model.configList;
                        if (list != null)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                    %>
                    <tr>
                        <td class="tdtop tdleft" style="width: 30%">
                            <%=list[j].Key1%>
                        </td>
                        <td class="tdtop" style="width: 70%">
                            <%=list[j].Value1%>
                        </td>
                    </tr>
                    <%} %>
                </table>
                <%}
                    }%>
            </div>
            <div id="versionDiv">
                <table id="VersionList" style="display: none;">
                </table>
            </div>
            <%=Html.Hidden("vid")%>
            <%} %>
        </div>
    </div>
</body>
</html>
