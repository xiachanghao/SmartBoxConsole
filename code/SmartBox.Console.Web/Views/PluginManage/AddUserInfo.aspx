<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�û���Ϣ</title>
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

            var pid = '<%=ViewData["pid"]%>';
            SimplyButtons.init();

            $("#suggust").autocomplete('<%=Url.Action("GetNameList")%>', {
                //width: 170,
                scroll: false,
                autoFill: false,
                formatItem: function(row, i, max) {
                    return row[0] + "[" + row[1] + "]";
                }
            }).result(function(event, data, formatted) {
                $("#sugvalue").val(data[1]);
                $("#sugname").val(data[0]);
            });
            
            var maiheight = document.documentElement.clientHeight + 22;
            var mainWidth = document.documentElement.clientWidth - 2; // ��ȥ�߿����ߵĿ��
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("GetUserNotByIdInfo")%>/' + escape(pid),
                colModel: [
                    { display: '�û��ʺ�', name: 'UserUId', width: 100, sortable: false, align: 'left' },
                    { display: '�û��Ա�', name: 'Gender', width: 100, sortable: false, process: Aclick, align: 'left' },
                    { display: '�û�����', name: 'UserName', width: 150, sortable: false, align: 'left' }
				],
                sortname: "UserUId",
                sortorder: "desc",
                usepager: true,
                rp: 10,
                rowbinddata: true,
                rowhandler: contextmenu,
                extParam: [
                        { name: "SID", value: $("#sugvalue").val() },
                        { name: "Sname", value: $("#sugname").val() },
                        { name: "Sgust", value: $("#suggust").val() }
                     ],
                showcheckbox: true
            };

            var grid = $("#ManageUserInfoList").flexigrid(option);

            function Aclick(pid, obj) {
                if (obj == true)
                    return "��";
                else
                    return "Ů";
            }

            $("#okbtn").click(function() { toolbarItem_onclick("Add") });
            //�󶨲�ѯ�¼�
            $("#querybtn").click(bindQueryEvent);
            //��ѯ
            function bindQueryEvent(item) {

                var p = { extParam: [
                        { name: "SID", value: $("#sugvalue").val() },
                        { name: "Sname", value: $("#sugname").val() },
                        { name: "Sgust", value: $("#suggust").val() }
                     ]
                };
                p.newp = 1;
                $("#ManageUserInfoList").flexOptions(p).flexReload();
            }

            function contextmenu(row) {

            } //
        });                                                         // end of ready

        function toolbarItem_onclick(cmd, grid) {
            var pid = '<%=ViewData["pid"]%>';
            if (cmd == "Add") {
                var ids = $("#ManageUserInfoList").getCheckedRows(); //��ȡѡ�е�checkbox������ѡ�е�id����
                if (ids.length < 1) {
                    hiAlert('��ѡ������Ҫ��ӵ��û�', '��ʾ');
                    return false;
                }
                var strIds = ids.join(',');
                hiConfirm("��ȷ��Ҫ�����", "��ʾ", function(btn) {
                    if (btn == true) {
                        $("#loadingpannel").html("�������......").show();
                        $.ajax({
                            type: "POST",
                            url: '<%=Url.Action("AddSomeUserInfos") %>',
                            data: { id: strIds, pid: pid },
                            dataType: "json",
                            success: function(data) {
                                $("#loadingpannel").hide();
                                if (data.IsSuccess) {
                                    hiAlert(data.Msg, '��ʾ', function() { CloseModelWindow(null, true); });
                                }
                                else {
                                    hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                                }
                            }
                        }); //end if $.ajax
                    }
                }); // end of hiConfirm
            }
        }   // end of toolbarItem_on

        function flushGrid() {
            $("#ManageUserInfoList").flexReload();
        } // end of flushGrid
        //�޸�

     
    </script>

    <style type="text/css">
        .tdlabel
        {
            width: 20%;
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
        <div class="qclass">
            <table class="bbit-form" cellspacing="0" style="width: 100%;">
                <tr>
                    <td class="tdlabel">
                        ����/�ʺţ�
                    </td>
                    <td class="tdinput">
                        <input type="text" id="suggust" title="����/�ʺ�" class="autocomplete" />
                        <input type="hidden" id="sugvalue" />
                        <input type="hidden" id="sugname" />
                    </td>
                    <td rowspan="2" class="querytd">
                        <div style="margin-left: 20px;">
                            <a id="querybtn" class="button query"><span><span class="bspan">��ѯ</span></span>
                            </a><a id="okbtn" class="button submit"><span><span class="bspan">���</span></span>
                            </a>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table id="ManageUserInfoList" style="display: none;">
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
