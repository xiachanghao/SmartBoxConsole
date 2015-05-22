<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<SmartBox.Console.Common.Entities.PluginInfoTemp>>" %>

<%@ Import Namespace="SmartBox.Console.Common.Entities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>������Ϣ</title>
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
            var th = parent.$("#cheadtop").outerHeight();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var mainWidth = document.documentElement.clientWidth; // ��ȥ�߿����ߵĿ��     
            var maiheight = document.documentElement.clientHeight - 2;
            $("#Butsubmit").click(function() { NextC() });
            
            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("���ڱ�������......").show();
                    return true;
                },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                       hiAlert(data.Msg, '��ʾ', function() {parent.CloseW(); });
                    }
                    else {
                        hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ');
                    }
                }
            };
            //У��
            $.validator.addMethod("safe", function(value, element) {
                return this.optional(element) || /^[^$\<\>]+$/.test(value);
            }, "���ܰ������·���: $<>");

            $("#fmEdit").validate(
             {
                 rules: {

             },
             messages: {

         },
         submitHandler: function(form) {
             $("#fmEdit").ajaxSubmit(options);
         },
         errorElement: "div",
         errorClass: "cusErrorPanel",
         errorPlacement: function(error, element) {
             showerror(error, element);
         }
     });

            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                if (pos.left + 155 >= document.documentElement.clientWidth) {
                    pos.left = document.documentElement.clientWidth - 156 - 4;
                }
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#fmEdit");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:100%;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }  // end of showerror

            function NextC() {
                hiConfirm("ȷ��Ҫ������ǰ�����", "��ʾ", function(btn) {
                    if (btn == true) {
                        $("#fmEdit").submit();
                    }
                });  // end of hiConfirm
           }
       
            var p=mainWidth % 2;
            $("#tabs").idTabs({
                start: 0,
                width: mainWidth-(8+p),
                height:maiheight-70,
                mode: "showall", //ȫ�ɼ�
                items: [
                           <% for (int i = 0, j = Model.Count; i < j; i++)
                           {
                               var dict = Model[i];
                              %>
                              { contentEl:"Sd_<%=dict.PluginCode %>", name: "<%=dict.PluginCode %>���" }
                              <%
                                  if (i != j - 1){%>,<%}
                              }%>                          
                        ],
                event: "!click", //"!mouseover",
                click: tabitem_click              
            });
            //ѡ��������Ч��
            function tabitem_click(id)
            {  
                 <% for (int i = 0, j = Model.Count; i < j; i++)
                   {
                       var dict = Model[i];
                      %>
                     var a=  "Sd_<%=dict.PluginCode %>";
                     $("#"+a).hide();
                     $("#"+a+"_a").hide();
                     $("#"+a+"_c").hide();
                 <%}%>       
                                    
                 $(id).show();
                 $(id+"_a").show();//action
                 $(id+"_c").show();//config

            }
            
             $('#Sd_'+'<%=Model[0].PluginCode%>').show();
             $('#Sd_'+'<%=Model[0].PluginCode%>'+'_a').show();
             $('#Sd_'+'<%=Model[0].PluginCode%>'+'_c').show();
             $("#tabs").height(maiheight - th  );
             
        });       // end of ready

        
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
            <% using (Html.BeginForm("SavePublishInfo", "PluginInfoManage", new { Vid = ViewData["vids"] }, FormMethod.Post, new { id = "fmEdit" }))
               {%>
            <fieldset>
                <legend class="title">������Ϣ</legend>
                <table width="100%" id="Table2" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �����̣�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model[0].CompanyName%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                ��ϵ�ˣ�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model[0].CompanyLinkman%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ��ַ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Model[0].CompanyHomePage%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                ��ϵ�绰��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Model[0].CompanyTel%>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].PluginCateCode.Equals(Constants.PluginCateCode))//����web���
                    {%>
            <div id="Sd_<%=Model[i].PluginCode%>" style="display: none">
                <fieldset>
                    <legend class="title">�����Ϣ</legend>
                    <table width="100%" id="Table1" class="bbit-form" cellspacing="0" cellpadding="1">
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �����ʶ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop ">
                                <%=Model[i].PluginCode%>
                            </td>
                            <td class="bbit-form-cell-name tdtop " style="width:225px">
                                <label>
                                    �������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Model[i].DisplayName%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �����ַ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Model[i].PluginUrl%>
                            </td>
                        </tr>
                         <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �Ƿ񹫿���</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%if (Model[i].IsPublic == true)
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
                                <%=Model[i].AppCode%>
                            </td>
                            <td class="bbit-form-cell-name tdtop " style="width:225px">
                                <label>
                                    Ȩ�ޱ�ʶ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Model[i].PrivilegeCode%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ����ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                              <%if (Model[i].IsNeed == true)
                                {%>
                              ��
                              <%}
                                else
                                { %>��
                              <%} %>
                            </td>
                            <td class="bbit-form-cell-name tdtop" style="width:225px">
                                <label>
                                    Ĭ��ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                 <%if (Model[i].IsDefault == true)
                                   {%>
                              ��
                              <%}
                                   else
                                   { %>��
                              <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft" >
                                <label>
                                    ����ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Model[i].Sequence%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ���������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Model[i].PluginSummary%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾�ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Model[i].Version%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾˵����</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Model[i].VersionSummary%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <%}
                    else
                    { %>
            <div id="Sd_<%=Model[i].PluginCode%>" style="display: none">
                <fieldset>
                    <legend class="title">�����Ϣ</legend>
                    <table width="100%" id="basicinfotable" class="bbit-form" cellspacing="0" cellpadding="1">
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �����ʶ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop ">
                                <%=Model[i].PluginCode%>
                            </td>
                            <td class="bbit-form-cell-name tdtop " style="width:225px">
                                <label>
                                    �������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Model[i].DisplayName%>
                            </td>
                        </tr>
                           <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �Ƿ񹫿���</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%if (Model[i].IsPublic == true)
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
                                <%=Model[i].AppCode%>
                            </td>
                            <td class="bbit-form-cell-name tdtop " style="width:225px">
                                <label>
                                    Ȩ�ޱ�ʶ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Model[i].PrivilegeCode%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ����ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                              <%if (Model[i].IsNeed == true)
                                {%>
                              ��
                              <%}
                                else
                                { %>��
                              <%} %>
                            </td>
                            <td class="bbit-form-cell-name tdtop" style="width:225px">
                                <label>
                                    Ĭ��ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                 <%if (Model[i].IsDefault == true)
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
                                <%=Model[i].FileName%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ����ȫ����</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Model[i].TypeFullName%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ������ͣ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Model[i].PCname%>
                            </td>
                            <td class="bbit-form-cell-name tdtop" style="width:225px">
                                <label>
                                    ����ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Model[i].Sequence%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ���������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Model[i].PluginSummary%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾�ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Model[i].Version%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾˵����</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Model[i].VersionSummary%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <%if (Model[i].PluginCateCode.Equals(Constants.ActionCateCode))
              {%>
            <div id="Sd_<%=Model[i].PluginCode%>_a">
                <fieldset id="actionDiv">
                    <legend class="title">�����չ��Ϣ</legend>
                    <table width="100%" id="Table4" class="bbit-form" cellspacing="0" cellpadding="1">
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ��չ��ʶ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Model[i].ActionCode%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    ��չ������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Model[i].ActionSummary%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <%}
              if (Model[i].IsIgnoreConfig == false)
              { %>
            <div id="Sd_<%=Model[i].PluginCode%>_c">
                <fieldset id="configDiv">
                    <legend class="title">���������Ϣ</legend>
                    <table width="100%" id="tablist" class="bbit-form" style="background-color: White" cellspacing="0"
                        cellpadding="1">
                        <tr style="background: #ccc url(../../Themes/Default/images/flexigrid/gridth.gif) repeat-x left center;">
                            <td class="tdtop tdleft" style="width:30%">
                                �ؼ�ֵ
                            </td>
                            <td class="tdtop" style="width:70%">
                                �ϴ�ֵ
                            </td>
                        </tr>
                        <%
                IList<ConfigInfo> list = Model[i].configList;
                for (int j = 0; j < list.Count; j++)
                {
                        %>
                        <tr id="tr<%=i%>">
                            <td class="tdtop tdleft" style="width:30%">
                                <%=list[j].Key1%>&nbsp;
                            </td>
                            <td class="tdtop" style="width:70%">
                                <%=list[j].Value1%>&nbsp;
                            </td>
                        </tr>
                        <%} %>
                    </table>
                </fieldset>
            </div>
            <%}
                    }
                } %>
            <%} %>
        </div>
        
    </div>
   <div style="text-align:center">
                    <input type="button" id="Butsubmit" value="����" />
        </div>
</body>
</html>
