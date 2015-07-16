<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<SmartBox.Console.Common.Entities.PluginInfoTemp>>" %>

<%@ Import Namespace="SmartBox.Console.Common.Entities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�޸Ĳ����Ϣ</title>
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
            Display();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var mainWidth = document.documentElement.clientWidth; // ��ȥ�߿����ߵĿ��     
            var maiheight = document.documentElement.clientHeight - 2;
            var th = parent.$("#cheadtop").outerHeight();
        
            $(".ispublic").click(SetDispalyByPrivilege);
            $(".acc").click(SetDispaly);
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
                        parent.$("#dicInfoName").text("4.����/����");
                        var url = '<%=Url.Action("PublishInfo") %>/?Vid=' + escape(data.Msg);
                        window.location = url;
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
                hiConfirm("ȷ��Ҫ���浱ǰ��Ϣִ����һ����", "��ʾ", function(btn) {
                    if (btn == true) {
                        $("#fmEdit").submit();
                    }
                });  // end of hiConfirm
            }

            $(".Add").click(AddTr);
            
            
            
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
//               var $dv = $("#tabbody");
//               if(id=="#Sd_basicinfo")
//               { 
//                    $dv.animate({"scrollTop":0},"slow");
//               }
//               else
//               {           
//                    var th = $(id).offset().top; 
//                    var dh=  $("#Sd_basicinfo").offset().top;                       
//                    var ch = $dv.attr("clientHeight");                
//                    var ph = $dv.attr("scrollHeight");                   
//                    var th =th-dh;   
//                    if (th < 0) th =0;                 
//                    if (th > ph - ch) th =  ph - ch;
//                   
//                    $dv.animate({"scrollTop":th},"normal");     
//               }               
//                return true;
            }
            
             $('#Sd_'+'<%=Model[0].PluginCode%>').show();
             $('#Sd_'+'<%=Model[0].PluginCode%>'+'_a').show();
             $('#Sd_'+'<%=Model[0].PluginCode%>'+'_c').show();
             $("#tabs").height(maiheight - th );
             
        });       // end of ready

        function SetDispalyByPrivilege()
        {
             var boolSelect = $(this).attr("checked");
             var seq = $(this).attr("name").split('.')[0];//��ʽ��s1.AppCode
             if (boolSelect == true) 
             {
                $("#"+seq+"_AppCode").attr("disabled", true);
                $("#"+seq+"_AppCode").css("background-color", "Silver");
                $("#"+seq+"_PrivilegeCode").attr("disabled", true);
                $("#"+seq+"_PrivilegeCode").css("background-color", "Silver");
             }
             else
             {
                $("#"+seq+"_AppCode").attr("disabled", false);
                $("#"+seq+"_AppCode").css("background-color", "");
                $("#"+seq+"_PrivilegeCode").attr("disabled", false);
                $("#"+seq+"_PrivilegeCode").css("background-color", "");
             }
        }
        
        function SetDispaly() {
            //var boolSelect = $("input[name=IsNeed]:checked").val();
            var boolSelect = $(this).attr("checked");
            var seq = $(this).attr("name").split('.')[0];//��ʽ��s1.IsNeed
            if (boolSelect == true) {
                $("#"+seq+"_IsDefault").attr("disabled", true);
                $("#"+seq+"_IsDefault").attr("checked", "checked");
            }
            else
                $("#"+seq+"_IsDefault").attr("disabled", false);
        }
        
        function Display()
        {
          <% for (int i = 0, j = Model.Count; i < j; i++)
               {
                   var dict = Model[i];
                  %>
                    if('<%=dict.IsNeed%>' == "True")
                    {
                        $("#s"+'<%=i%>'+"_IsDefault").attr("disabled", true);
                        $("#s"+'<%=i%>'+"_IsDefault").attr("checked", "checked");
                    }
                    if('<%=dict.IsPublic%>' == "True")
                    {
                        $("#s"+'<%=i%>'+"_AppCode").attr("disabled", true);
                        $("#s"+'<%=i%>'+"_AppCode").css("background-color", "Silver");
                        $("#s"+'<%=i%>'+"_PrivilegeCode").attr("disabled", true);
                        $("#s"+'<%=i%>'+"_PrivilegeCode").css("background-color", "Silver");
                    }
             <%}%>       
        }



        function del(obj) {
            var obj = "#" + obj;
            $(obj).remove();
        }

            var counts = '<%=(int)ViewData["configList"]%>';
        function AddTr() {
            var pcode = $(this).attr("id").split('|')[1];
            $("#tablist_" + pcode).append("<tr id='" + pcode + "tr" + counts + "'><td class='tdtop tdleft'><input type='text' name='"+ pcode + "|" + counts + ".Key1' /></td><td class='tdtop'><input type='text' name='"+ pcode + "|" + counts + ".Value1' /></td><td class='tdtop'></td><td class='tdtop tdright'><a onclick=\"del('" + pcode + "tr" + counts + "')\" style='cursor:pointer;color:blue' >ɾ��</a></td></tr>");
            counts++;
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
<body id="bodyDiv">
    <div class="cHead" style="border: none;">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            ���ڱ�������...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            �ǳ���Ǹ���޷�ִ�����Ĳ��������Ժ�����</div>
    </div>
    <div id="tabs" class="tabs">
        <div id="tabbody" class="tabsitemcontainer">
            <% using (Html.BeginForm("SavePluginInfos", "PluginInfoManage", new { IsAdd = ViewData["IsAdd"] }, FormMethod.Post, new { id = "fmEdit" }))
               {
                   Html.AntiForgeryToken();
                   Html.ValidationSummary(true);%>
            <fieldset>
                <legend class="title">������Ϣ</legend>
                <table width="100%" id="Table2" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                �����̣�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.TextBox("CompanyName", Model[0].CompanyName, new { style = "", Class = " safe" })%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                ��ϵ�ˣ�</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextBox("CompanyLinkman", Model[0].CompanyLinkman, new { style = "", Class = "safe" })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                ��ַ��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.TextBox("CompanyHomePage", Model[0].CompanyHomePage, new { style = "", Class = " safe" })%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                ��ϵ�绰��</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextBox("CompanyTel", Model[0].CompanyTel, new { style = "", Class = "safe" })%>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%
                for (int i = 0; i < Model.Count; i++)
                {
                    if (Model[i].PluginCateCode.Equals(Constants.PluginCateCode))//����web���
                    {%>
            <%=Html.Hidden("s" + i.ToString() + ".PreVersionPCs", Model[i].PreVersionPCs)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsNew", Model[i].IsNew)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsUse", Model[i].IsUse)%>
            <%=Html.Hidden("s" + i.ToString() + ".PluginCateCode", Model[i].PluginCateCode)%>
            <%=Html.Hidden("s" + i.ToString() + ".HashCode", Model[i].HashCode)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsIgnoreConfig", Model[i].IsIgnoreConfig)%>
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
                                <%=Html.TextBox("s" + i.ToString() + ".PluginCode", Model[i].PluginCode, new { style = "background:Silver", @readonly = true })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop ">
                                <label>
                                    �������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".DisplayName", Model[i].DisplayName, new { style = "", Class = "required safe" })%>
                                <span class="bbit-formItemMust">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �����ַ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".PluginUrl", Model[i].PluginUrl, new { style = "background:Silver;width:320px;", @readonly = true })%>
                            </td>
                        </tr>
                         <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �Ƿ񹫿���</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsPublic", Model[i].IsPublic, new { style = "", Class = "ispublic" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    Ӧ��ϵͳ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.TextBox("s" + i.ToString() + ".AppCode", Model[i].AppCode, new { style = "", Class = " safe" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    Ȩ�ޱ�ʶ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".PrivilegeCode", Model[i].PrivilegeCode, new { style = "", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ����ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsNeed", Model[i].IsNeed, new { style = "", Class = "acc" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    Ĭ��ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsDefault", Model[i].IsDefault)%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ����ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".Sequence", Model[i].Sequence, new { style = "width:15%", Class = " required digits" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ���������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Html.TextArea("s" + i.ToString() + ".PluginSummary", Model[i].PluginSummary, new { style = "width:90%", rows = 3 })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾�ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".Version", Model[i].Version, new { style = "background:Silver", @readonly = true })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾˵����</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Html.TextArea("s" + i.ToString() + ".VersionSummary", Model[i].VersionSummary, new { style = "width:90%", rows = 3 })%>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <%}
                    else
                    { %>
            <%=Html.Hidden("s" + i.ToString() + ".PreVersionPCs", Model[i].PreVersionPCs)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsNew", Model[i].IsNew)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsUse", Model[i].IsUse)%>
            <%=Html.Hidden("s" + i.ToString() + ".HashCode", Model[i].HashCode)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsIgnoreConfig", Model[i].IsIgnoreConfig)%>
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
                                <%=Html.TextBox("s" + i.ToString() + ".PluginCode", Model[i].PluginCode, new { style = "background:Silver", @readonly = true })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop ">
                                <label>
                                    �������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".DisplayName", Model[i].DisplayName, new { style = "", Class = "required safe" })%>
                                <span class="bbit-formItemMust">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �Ƿ񹫿���</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsPublic", Model[i].IsPublic, new { style = "", Class = "ispublic" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    Ӧ��ϵͳ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.TextBox("s" + i.ToString() + ".AppCode", Model[i].AppCode, new { style = "", Class = " safe" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    Ȩ�ޱ�ʶ��</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".PrivilegeCode", Model[i].PrivilegeCode, new { style = "", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ����ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsNeed", Model[i].IsNeed, new { style = "", Class = "acc" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    Ĭ��ʹ�ã�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsDefault", Model[i].IsDefault)%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ������������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".FileName", Model[i].FileName, new { style = "width:320px", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ����ȫ����</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".TypeFullName", Model[i].TypeFullName, new { style = "width:320px", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ������ͣ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.TextBox("PCname", Model[i].PCname, new { style = "background:Silver", @readonly = true })%>
                                <%=Html.Hidden("s" + i.ToString() + ".PluginCateCode", Model[i].PluginCateCode)%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    ����ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".Sequence", Model[i].Sequence, new { style = "width:15%", Class = "required digits" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    ���������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Html.TextArea("s" + i.ToString() + ".PluginSummary", Model[i].PluginSummary, new { style = "width:90%", rows = 2 })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾�ţ�</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".Version", Model[i].Version, new { style = "background:Silver", @readonly = true })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    �汾˵����</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Html.TextArea("s" + i.ToString() + ".VersionSummary", Model[i].VersionSummary, new { style = "width:90%", rows = 2 })%>
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
                                <%=Html.TextBox("s" + i.ToString() + ".ActionCode", Model[i].ActionCode, new { Class = "required safe" })%>
                                <span class="bbit-formItemMust">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    ��չ������</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextArea("s" + i.ToString() + ".ActionSummary", Model[i].ActionSummary, new { style = "width:70%", rows = 3 })%>
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
                    <!--<div>
                        <span id="btnAdd|<%=Model[i].PluginCode%>" title='����' style="cursor: pointer" class="Add">����</span>
                    </div>-->
                    <table width="100%" id="tablist_<%=Model[i].PluginCode%>" style="background-color: White"
                        cellspacing="0" cellpadding="1">
                        <tr style="background: #ccc url(../../Themes/Default/images/flexigrid/gridth.gif) repeat-x left center;">
                            <td class="tdtop tdleft">
                                �ؼ�ֵ
                            </td>
                            <td class="tdtop">
                                �ϴ�ֵ
                            </td>
                            <td class="tdtop tdright">
                                ԭ��ֵ
                            </td>
                        </tr>
                        <%
                            IList<ConfigInfo> list = Model[i].configList;
                            for (int j = 0; j < list.Count; j++)
                            {
                        %>
                        <tr id="<%=Model[i].PluginCode%>tr<%=j%>">
                            <td class="tdtop tdleft">
                                <%=Html.TextBox(Model[i].PluginCode + "|" + j.ToString() + ".Key1", list[j].Key1, new { style = "background:Silver", @readonly = true })%>
                            </td>
                            <td class="tdtop">
                                <%=Html.TextBox(Model[i].PluginCode + "|" + j.ToString() + ".Value1", list[j].Value1, new { style = "" })%>
                            </td>
                            <td class="tdtop">
                                <%=Html.TextBox("k1" + j.ToString() + i.ToString(), list[j].OldValue, new { style = "background:Silver", @readonly = true })%>
                            </td>
                            <td class="tdtop tdright">
                                <a onclick="del('<%=Model[i].PluginCode%>tr<%=j%>')" style='cursor: pointer; color: blue'>
                                    ɾ��</a>
                            </td>
                        </tr>
                        <%} %>
                    </table>
                </fieldset>
            </div>
            <%}//end if
                    }//end else
                }//end for %>
            <%}//end form%>
        </div>
    </div>
    <div style="text-align: center">
        <input type="button" id="Butsubmit" value="��һ��" />
    </div>
</body>
</html>
