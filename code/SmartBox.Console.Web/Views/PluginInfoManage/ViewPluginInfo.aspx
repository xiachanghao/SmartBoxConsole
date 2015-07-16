<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.PluginInfo>" %>

<%@ Import Namespace="SmartBox.Console.Common.Entities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��Ϣ</title>
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
            var mainWidth = document.documentElement.clientWidth; // ��ȥ�߿����ߵĿ��
            var maiheight = document.documentElement.clientHeight - 2;

            var otherpm = 210;
            var gh = maiheight - otherpm;

            var option1 = {
                height: gh,
                width: mainWidth - 8,
                url: '<%=Url.Action("GetVersionTrackList")%>/' + '<%=Model.PluginCode%>',
                colModel: [
                    { display: '�汾��', name: 'VersionName', width: 100, sortable: false, align: 'left' },
                    { display: '�汾˵��', name: 'VersionSummary', width: 140, sortable: false, align: 'left' },
                    { display: '״̬', name: 'VersionStatus', width: 60, sortable: false, process: GetName, align: 'left' },
			        { display: '������', name: 'CreateUid', width: 100, sortable: false, align: 'left' },
			        { display: '����ʱ��', name: 'CreateTime', width: 130, sortable: false, align: 'left' },
			        { display: '����', name: 'VersionStatus1', width: 100, sortable: false, process: GetFile, align: 'center' }

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
                    return "δ����";
                else if (pid == "1")
                    return "����ʹ��";
                else if (pid == "2")
                    return "�ѹ���";
            }

            function GetFile(pid, obj) {
                var arr = pid.split(',');
                if ('<%=Model.PluginCateCode%>' == '<%=Constants.PluginCateCode%>') {
                    //return "<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >����</a>";
                    return "";
                }
                else {
                    if (arr[0] == "1")
                        return "<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >����</a>&nbsp;&nbsp;<a onclick='delClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >ɾ��</a>";
                    else
                        return "<a onclick='openClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >����</a>&nbsp;&nbsp;<a onclick='delClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >ɾ��</a>&nbsp;&nbsp;<a onclick='resumeClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >�ָ�</a>";
                }
            }


            var p = mainWidth % 2;
            $("#tabs").idTabs({
                start: 0,
                width: mainWidth - (8 + p),
                height: maiheight - 70,
                mode: "showall", //ȫ�ɼ�
                items: [
                           { contentEl: "Pinfo", name: "�����Ϣ" },
                           { contentEl: "Vinfo", name: "�汾��ʷ" }
                        ],
                event: "!click", //"!mouseover",
                click: tabitem_click
            });
            //ѡ��������Ч��
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
                        //����ɾ��
                        if (length > 1) {
                            alerts = "��ȷ��Ҫɾ��" + pluginNames + "����ĵ�ǰ�汾��";
                            if (status == "1") {
                                alerts = "��ȷ��Ҫɾ����ǰ����ʹ�õ�" + pluginNames + "����İ汾��,ɾ����ò����ʹ����һ�汾";
                            }
                            urls = '<%=Url.Action("DelVersions") %>';
                        }
                        else {
                            alert('��ǰ�������һ���汾,��Ҫɾ��,��ɾ�����������');
                            return false;
                        }
                        hiConfirm(alerts, "��ʾ", function(btn) {
                            if (btn == true) {
                                $("#loadingpannel").html("����ɾ��......").show();
                                $.ajax({
                                    type: "POST",
                                    url: urls,
                                    data: { vid: ojb },
                                    dataType: "json",
                                    success: function(data) {
                                        $("#loadingpannel").hide();
                                        if (data.IsSuccess) {
                                            hiAlert("ɾ���ɹ�", '��ʾ', function() { $("#VersionList").flexReload(); });
                                        }
                                        else {
                                            hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                                        }
                                    }
                                }); //end if $.ajax
                            }
                        });   // end of hiConfirm
                        //endendendendendendendendend
                    }
                    else {
                        hiAlert("����ʧ��");
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
                        //����ɾ��
                        alerts = "��ȷ��Ҫ�ָ�" + pluginNames + "����İ汾��";
                        if (status != "2") {
                            alert('��ǰΪ�ѷ����İ汾���߻�δ������,��ѡ����ڰ汾!');
                            return false;
                        }
                        urls = '<%=Url.Action("ResumeVersions") %>';
                        hiConfirm(alerts, "��ʾ", function(btn) {
                            if (btn == true) {
                                $("#loadingpannel").html("���ڻָ�......").show();
                                $.ajax({
                                    type: "POST",
                                    url: urls,
                                    data: { vid: ojb },
                                    dataType: "json",
                                    success: function(data) {
                                        $("#loadingpannel").hide();
                                        if (data.IsSuccess) {
                                            hiAlert("�ָ��ɹ�", '��ʾ', function() { $("#VersionList").flexReload(); });
                                        }
                                        else {
                                            hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                                        }
                                    }
                                }); //end if $.ajax
                            }
                        });   // end of hiConfirm
                        //endendendendendendendendend
                    }
                    else {
                        hiAlert("����ʧ��");
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
            ���ڱ�������...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            �ǳ���Ǹ���޷�ִ�����Ĳ��������Ժ�����</div>
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
                                �����̣�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.CompanyName%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                ��ϵ�ˣ�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.CompanyLinkman%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ��ַ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.CompanyHomePage%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                ��ϵ�绰��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.CompanyTel%>
                        </td>
                    </tr>
                </table>
                <%
                    if (Model.PluginCateCode.Equals(Constants.PluginCateCode))//����web���
                    {%>
                <table width="100%" id="Table1" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �����ʶ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.PluginCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                �������</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.DisplayName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �����ַ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.PluginUrl%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �Ƿ񹫿���</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%if (Model.IsPublic == true)
                              {%>
                            ��
                            <%}
                              else
                              { %>��
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                Ӧ��ϵͳ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.AppCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                Ȩ�ޱ�ʶ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.PrivilegeCode%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ����ʹ�ã�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%if (Model.IsNeed == true)
                              {%>
                            ��
                            <%}
                              else
                              { %>��
                            <%} %>
                        </td>
                        <td class="bbit-form-cell-name tdtop" style="width: 225px">
                            <label>
                                Ĭ��ʹ�ã�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%if (Model.IsDefault == true)
                              {%>
                            ��
                            <%}
                              else
                              { %>��
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ����ţ�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.Sequence%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ���������</label>
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
                                �����ʶ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.PluginCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                �������</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.DisplayName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �Ƿ񹫿���</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%if (Model.IsPublic == true)
                              {%>
                            ��
                            <%}
                              else
                              { %>��
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                Ӧ��ϵͳ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop ">
                            <%=Model.AppCode%>
                        </td>
                        <td class="bbit-form-cell-name tdtop " style="width: 225px">
                            <label>
                                Ȩ�ޱ�ʶ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.PrivilegeCode%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ����ʹ�ã�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%if (Model.IsNeed == true)
                              {%>
                            ��
                            <%}
                              else
                              { %>��
                            <%} %>
                        </td>
                        <td class="bbit-form-cell-name tdtop" style="width: 225px">
                            <label>
                                Ĭ��ʹ�ã�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%if (Model.IsDefault == true)
                              {%>
                            ��
                            <%}
                              else
                              { %>��
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ������������</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.FileName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ����ȫ����</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.TypeFullName%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ������ͣ�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.PCname%>
                        </td>
                        <td class="bbit-form-cell-name tdtop" style="width: 225px">
                            <label>
                                ����ţ�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model.Sequence%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ���������</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                            colspan="3">
                            <%=Model.Summary%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �汾�ţ�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                            <%=Model.Version%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �汾˵����</label>
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
                                ��չ��ʶ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model.ActionCode%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                ��չ������</label>
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
                            �ؼ�ֵ
                        </td>
                        <td class="tdtop" style="width: 70%">
                            �ϴ�ֵ
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
