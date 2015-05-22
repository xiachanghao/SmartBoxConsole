<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�������</title>
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
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>

    <% if (false)
       { %>

    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <%} %>

    <script type="text/javascript">
        var maiheight = document.documentElement.clientHeight;
        var mainWidth = document.documentElement.clientWidth - 2; // ��ȥ�߿����ߵĿ��
        $(document).ready(function() {
            SimplyButtons.init();
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetPluginInfo")%>',
                colModel: [
                    { display: '�������', name: 'DisplayName', width: 80, sortable: false, align: 'left' },
                    { display: '�����ʶ', name: 'PluginCode', width: 80, sortable: false, process: Dclick, align: 'left' },
                    { display: '�汾', name: 'Version', width: 50, sortable: false, align: 'left' },
                    { display: '�汾˵��', name: 'VersionSummary', width: 100, sortable: false, align: 'left' },
                    { display: '�������', name: 'PDisplayName', width: 80, sortable: false, align: 'left' },
                    { display: '������', name: 'CompanyName', width: 80, sortable: false, align: 'left' },
			        { display: '������', name: 'CreateUid', width: 80, sortable: false, align: 'left' },
			        { display: '����ʱ��', name: 'CreateTime', width: 130, sortable: false, hide: true, align: 'left' },
			        { display: '����', name: 'VersionStatuss', width: 180, sortable: false, process: Aclick, align: 'center' }
				],

                sortname: "CreateTime",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                rowhandler: contextmenu
            };
            var grid = $("#ManageVerList").flexigrid(option);
            autosize_flexgrid("#ManageVerList");
            $(window).resize(function () {
                autosize_flexgrid("#ManageVerList");
            });

            function Dclick(pid, obj) {
                return "<a onclick='ViewDetails(\"" + obj + "\")' style='cursor:pointer;color:blue' >" + pid + "</a>";
            }

            function Aclick(pid, obj) {
                var arr = pid.split(',');
                var text = "";
                if (arr[0] == "1")//����ʹ��
                {
                    if (arr[3] == "WebPlugin")
                        text = "<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >�޸�����</a>";
                    else
                        text = "<a onclick='openAddClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >����</a>&nbsp;&nbsp;&nbsp;<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >�޸�����</a>";
                }
                else if (arr[0] == "3")//δ������������
                {
                    text = "<a onclick='UpdateInfoClick(\"" + obj + "\",\"" + true + "\")' style='cursor:pointer;color:blue' >�޸�</a>";
                }
                else if (arr[0] == "0")//δ������������
                {
                    text = "<a onclick='UpdateInfoClick(\"" + obj + "\",\"" + false + "\")' style='cursor:pointer;color:blue' >�޸�</a>&nbsp;&nbsp;&nbsp;<a onclick='UpdateConfigClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >�޸�����</a>";
                }
                else {
                    text = "";
                }
                var Sets = "����";
                if (arr[1] == true) {
                    Sets = "����";
                }
                text += "&nbsp;&nbsp;&nbsp;<a onclick='UpdateIsUseClick(\"" + arr[2] + "\",\"" + Sets + "\",\"" + arr[1] + "\")' style='cursor:pointer;color:blue' >" + Sets + "</a>";
                //2.0.1.0
                text += "&nbsp;&nbsp;&nbsp;<a onclick='DelClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >ɾ��</a>";
                //2.0.1.0
                return text;
            }

            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });

            function contextmenu(row) {
                var ch =row.getAttribute("ch");
                var cell = ch.split("_FG$SP_");
                var kinds=0;
                if (cell&&cell.length>1) {
                    kinds = cell[8].split(',')[0];
                }
                if (kinds == 0 || kinds == 3) {
                    $(row).find("td").css("color", "red");
                }
            } //


        });                                                                         // end of ready

        function DelClick(Id) {
            var pluginNames = "";

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("GetPluginNames") %>',
                data: { vid: Id },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        pluginNames = data.Msg.split('|')[0];
                        urls = '<%=Url.Action("DelAllVersions") %>';
                        hiConfirm("��ȷ��Ҫɾ��" + pluginNames + "�����?", "��ʾ", function(btn) {
                            if (btn == true) {
                                $("#loadingpannel").html("����ɾ��......").show();
                                $.ajax({
                                    type: "POST",
                                    url: urls,
                                    data: { vid: Id },
                                    dataType: "json",
                                    success: function(data) {
                                        $("#loadingpannel").hide();
                                        if (data.IsSuccess) {
                                            hiAlert("ɾ���ɹ�", '��ʾ', function() { $("#ManageVerList").flexReload(); });
                                        }
                                        else {
                                            hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                                        }
                                    }
                                }); //end if $.ajax
                            }
                        });   // end of hiConfirm
                    }
                    else {
                        hiAlert("����ʧ��");
                    }
                }
            });      //end if $.ajax
        }

        //����
        function toolbarItem_onclick(cmd, grid) {
            if (cmd == "Add") {
                url = '<%=Url.Action("PluginWizard") %>/?IsAdd=1&IsCate=1'; //1��ʾ����(��ID)iscate��ʶ�Ƿ��ܹ��޸ķ���
                OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "���������", onclose: function() { $("#ManageVerList").flexReload(); } });
            }
        }  // end of toolbarItem_on


        //����
        function openAddClick(Id) {
            var url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=0&IsCate=0';  //0��ʾ����,iscate��ʶ�Ƿ��ܹ��޸ķ���
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "���������", onclose: function() { $("#ManageVerList").flexReload(); } });
        } // contextMenuItem_click


        //�޸�
        function UpdateInfoClick(Id, isAdd) {
            var url = "";
            if (isAdd == "true")//����
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=1&IsCate=0&IsUpdate=1'; //1��ʾ����
            else
                url = '<%=Url.Action("PluginWizard") %>/?Vid=' + escape(Id) + '&IsAdd=0&IsCate=0&IsUpdate=1'; //0��ʾ����
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "���������", onclose: function() { $("#ManageVerList").flexReload(); } });
        }

        //�޸�����
        function UpdateConfigClick(Id) {
            var url = '<%=Url.Action("UpdateConfigInfo") %>/' + escape(Id) ;  //2��ʾ�޸�����
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "���������", onclose: function() { $("#ManageVerList").flexReload(); } });
        }
        //�鿴�����ϸ��Ϣ
        function ViewDetails(Id) {
            var url = '<%=Url.Action("ViewPluginInfo") %>/' + escape(Id);  
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "�鿴�����ϸ��Ϣ", onclose: function() { $("#ManageVerList").flexReload(); } });
        }
        //�޸Ľ���״̬
        function UpdateIsUseClick(code,str,status) {
            hiConfirm("��ȷ��Ҫ" + str + "�ò����", "��ʾ", function(btn) {
                if (btn == true) {
                    $("#loadingpannel").html("����ִ��......").show();
                    $.ajax({
                        type: "POST",
                        url: '<%=Url.Action("SetDisableStatus") %>',
                        data: { pid: code, status: status },
                        dataType: "json",
                        success: function(data) {
                            $("#loadingpannel").hide();
                            if (data.IsSuccess) {
                                hiAlert("�����ɹ�", true);
                                flushGrid();
                            }
                            else {
                                hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                            }
                        }
                    }); //end if $.ajax
                }
            });   // end of hiConfirm
        }    
        
        function flushGrid() {
            $("#ManageVerList").flexReload();
        } // end of flushGrid
        
    
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
        <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">�������</span>
            </div>
        </div>
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='����' class="Add">����</span></div>
            </div>
        </div>
        <div>
            <table id="ManageVerList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>

