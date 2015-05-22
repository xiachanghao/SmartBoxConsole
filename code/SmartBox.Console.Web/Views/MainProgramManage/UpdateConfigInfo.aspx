<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>���������ù���</title>
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
        $(document).ready(function() {

            var pid = '<%=ViewData["pId"]%>';
            var vid = '<%=ViewData["verid"]%>';
            $("#btnNext").click(function() { NextWindowsclick(vid) });
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight;
            var mainWidth = document.documentElement.clientWidth - 2; // ��ȥ�߿����ߵĿ��
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetConfigInfo")%>/' + escape(pid),
                colModel: [
                    { display: '��', name: 'Key', width: 100, sortable: false, align: 'left' },
                    { display: 'ֵ', name: 'Value', width: 100, sortable: false, align: 'left' },
                    { display: '����', name: 'Summary', width: 150, sortable: false, align: 'left' },
			        { display: '����', name: 'ConfigId', width: 70, sortable: false, process: Aclick, align: 'center' }
				],
                sortname: "ConfigId",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                rowhandler: contextmenu,
                showcheckbox: true
            };

            var grid = $("#ManageConfigInfoList").flexigrid(option);

            function Aclick(pid, obj) {
                return "<a onclick='openWindowsclick(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color=blue' >�޸�</a>&nbsp;&nbsp;<a onclick='openWindowsclickdel(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color=blue' >ɾ��</a>";
            }

            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });
            $("#btndel").click(function() { toolbarItem_onclick("del") });

            function contextmenu(row) {
            } //

        });                                                // end of ready

        function toolbarItem_onclick(cmd, grid) {
            var pid = '<%=ViewData["pId"]%>';
            if (cmd == "Add") {
                OpenModelWindow('<%=Url.Action("AddConfigInfo") %>/?pid=' + pid, { width: 400, height: 180, caption: "������Ϣ", onclose: function() { flushGrid(); } });
            }
            else if (cmd == "del") {
                var ids = $("#ManageConfigInfoList").getCheckedRows(); //��ȡѡ�е�checkbox������ѡ�е�id����
                if (ids.length < 1) {
                    hiAlert('��ѡ������Ҫɾ����������Ϣ', '��ʾ');
                    return false;
                }
                var strIds = ids.join(',');
                hiConfirm("��ȷ��Ҫɾ����", "��ʾ", function(btn) {
                    if (btn == true) {
                        $("#loadingpannel").html("����ɾ��......").show();
                        $.ajax({
                            type: "POST",
                            url: '<%=Url.Action("DelSomeConfigInfos") %>',
                            data: { id: strIds },
                            dataType: "json",
                            success: function(data) {
                                $("#loadingpannel").hide();
                                if (data.IsSuccess) {
                                    flushGrid();
                                }
                                else {
                                    hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                                }
                            }
                        }); //end if $.ajax
                    }
                }); // end of hiConfirm
            }
        }  // end of toolbarItem_on

        function flushGrid() {
            $("#ManageConfigInfoList").flexReload();
        } // end of flushGrid
        //�޸�
        function openWindowsclick(Id) {
            var pid = '<%=ViewData["pId"]%>';
            var url = '<%=Url.Action("AddConfigInfo") %>/' + escape(Id) + '/?pid=' + pid;
            OpenModelWindow(url, { width: 400, height: 180, caption: "�޸���Ϣ", onclose: function() {  flushGrid();  } });
        } // contextMenuItem_click

        function NextWindowsclick(Id) {
            var isloadfile = '<%=ViewData["IsLoadFile"]%>';//�Ƿ񵥸��ϴ��ļ�
            var url = '<%=Url.Action("PublishInfo") %>/' + escape(Id) + '/?IsLoadFile=' + isloadfile;
            window.location = url;
        }

        function openWindowsclickdel(Id) {
            hiConfirm("��ȷ��Ҫɾ������Ϣ��", 'ȷ��', function(r) {
                if (r == true) //
                {
                    $("#loadingpannel").html("����ɾ��......").show();
                    $.post('<%=Url.Action("DelSomeConfigInfos") %>', { id: Id },
                                                        function(data) {
                                                            $("#loadingpannel").hide();
                                                            if (data.IsSuccess) {
                                                                hiAlert("ɾ���ɹ�", '��ʾ', function() { flushGrid(); });
                                                            }
                                                            else {
                                                                hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                                                            }
                                                        },
                                                    "json"
                                                    );
                }
            });

        }  // contextMenuItem_clic
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
    <div style="padding: 1px;">
        <%--<div class="cHead">
            <div class="ftitle">
                <span id="departmentName">���������ù���</span>
            </div>
        </div>--%>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='����' class="Add">����</span></div>
            </div>
            <div id="btndel" class="fbutton">
                <div>
                    <span title='ɾ��' class="Delete">ɾ��</span></div>
            </div>
            <div id="Div1" style="float:right">
                <input type="button" id="btnNext" value="��һ��" />
            </div>
        </div>
        <div>
            <table id="ManageConfigInfoList" style="display: none;">
            </table>
        </div>
        <div class="ajaxmsgpanel">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                ���ڱ�������...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                �ǳ���Ǹ���޷�ִ�����Ĳ��������Ժ�����</div>
            <div id="tdiv" style="display: none;">
            </div>
        </div>
    </div>
</body>
</html>
