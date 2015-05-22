<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�������</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/flexigrid.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="stylesheet" type="text/css" />
 <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
 
 
    <link href="<%=Url.Content("~/Themes/Default/contextmenu.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/autocomplete.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/simplybuttons.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>    
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.autocomplete.js")%>" type="text/javascript" defer="defer"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>
     <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>

    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <%} %>

    <script type="text/javascript">
        $(document).ready(function() {
            SimplyButtons.init();
            var maiheight = document.documentElement.clientHeight+26;
            var mainWidth = document.documentElement.clientWidth - 2; // ��ȥ�߿����ߵĿ��
            var otherpm = 210;
            var gh = maiheight - otherpm;
            var option = {
                height: gh,
                width: mainWidth,
                url: '<%=Url.Action("PluginInfoList")%>',
                colModel: [
                    { display: '�����', name: 'DisplayName', width: 80, sortable: false, align: 'left' },
                    { display: '�汾', name: 'Version', width: 80, sortable: false, align: 'left' },
                    { display: '������', name: 'CName', width: 80, sortable: false, align: 'left' },
                    { display: '·����', name: 'PluginCode', width: 80, sortable: false, align: 'left' },
                    { display: '����', name: 'TypeFullName', width: 80, sortable: false, align: 'left' },
                    { display: '�ļ���', name: 'FileName', width: 80, sortable: false, align: 'left' },
                    { display: '����', name: 'IsNeed', width: 50, sortable: false, process: viewIs, align: 'left' },
                    { display: 'Ĭ��', name: 'IsDefault', width: 50, sortable: false, process: viewIs, align: 'left' },
                    { display: '�����ַ', name: 'PluginUrl', width: 80, sortable: false, align: 'left' },
			        { display: '������', name: 'CreateUid', width: 100, sortable: false, hide: true, align: 'left' },
			        { display: '����ʱ��', name: 'CreateTime', width: 130, sortable: false, hide: true, align: 'left' }
				],

                sortname: "CreateTime",
                sortorder: "desc",
                usepager: true,
                rp: 15,
                rowbinddata: true,
                rowhandler: contextmenu
            };

            var grid = $("#ManageVerList").flexigrid(option);

            function viewIs(pid, value) {
                if (value == true)
                    return "��";
                else
                    return "��";
            }



            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });

            function contextmenu(row) {
                var menu = { width: 150, items: [] };
                menu.items.push({ text: "���ò���û�", icon: '<%=Url.Content("~/images/icons/view.png")%>', alias: "contextmenu-update", action: contextMenuItem_click });
                menu.items.push({ text: "ˢ��", icon: '<%=Url.Content("~/images/icons/table_refresh.png")%>', alias: "contextmenu-reflash", action: contextMenuItem_click });

                $(row).contextmenu(menu);
            } //

            function contextMenuItem_click(target) {
                var id = $(target).attr("id").substr(3);
                var chs = $(target).attr("ch").split('_FG$SP_');
                var cmd = this.data.alias;
                if (cmd == "contextmenu-update") {
                    if (chs[6] == "��") {
                        alert("�ò������Ҫ�����û�");
                    }
                    else {
                        var url = '<%=Url.Action("PluginUser") %>/' + encodeURIComponent(id);
                        OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "�����û�", onclose: function() { $("#ManageVerList").flexReload(); } });
                    }
                } else if (cmd == "contextmenu-reflash") {
                    $("#ManageVerList").flexReload();
                }
            }


        });                                            // end of ready

        function toolbarItem_onclick(cmd, grid) {
            if (cmd == "Add") {
              
            }
        }  // end of toolbarItem_on 
        
        function flushGrid() {
            $("#ManageVerList").flexReload();
        } // end of flushGrid
        //�鿴��ϸ��Ϣ

    </script>

    <style type="text/css">
        .tdlabel
        {
            width: 12%;       
            text-align:right;
        }
         .tdinput
         {
              width: 30%;
         }
         .tdquery
         {
             text-align:center;
         }
        .qclass
        {
            border:solid 1px #99bbe8;
            border-top:none;
        }
        .qclass input
        {
            width:90%;
            border:solid 1px #ccc;
        }
       
        input.autocomplete
        {
            border:solid 1px #99bbe8;
         }
         tr.trtop td
         {
              border-top:solid 1px #ccc;
         }
         td.querytd
         {
              border-left:solid 1px #ccc;
              text-align:center;
         }
         .bspan
         {
              background-color:#E8F1F8 !important;
         }
      
    </style>
</head>
<body>
    <div style="padding:1px;">
        <%--<div class="cHead">        
                <div class="ftitle">
                    <span id="departmentName">������汾����</span>
                </div>         
        </div>--%>
        <div>
            <table id="ManageVerList" style="display: none;">
            </table>
        </div>
    </div>
</body>
</html>

