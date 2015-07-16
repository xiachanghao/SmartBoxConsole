<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<SmartBox.Console.Common.Entities.PluginInfoTemp>>" %>

<%@ Import Namespace="SmartBox.Console.Common.Entities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改插件信息</title>
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
            var mainWidth = document.documentElement.clientWidth; // 减去边框和左边的宽度     
            var maiheight = document.documentElement.clientHeight - 2;
            var th = parent.$("#cheadtop").outerHeight();
        
            $(".ispublic").click(SetDispalyByPrivilege);
            $(".acc").click(SetDispaly);
            $("#Butsubmit").click(function() { NextC() });

            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("正在保存数据......").show();
                    return true;
                },
                dataType: "json",

                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        parent.$("#dicInfoName").text("4.保存/发布");
                        var url = '<%=Url.Action("PublishInfo") %>/?Vid=' + escape(data.Msg);
                        window.location = url;
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            };
            //校验
            $.validator.addMethod("safe", function(value, element) {
                return this.optional(element) || /^[^$\<\>]+$/.test(value);
            }, "不能包含以下符号: $<>");

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
                hiConfirm("确认要保存当前信息执行下一步吗？", "提示", function(btn) {
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
                mode: "showall", //全可见
                items: [
                           <% for (int i = 0, j = Model.Count; i < j; i++)
                           {
                               var dict = Model[i];
                              %>
                              { contentEl:"Sd_<%=dict.PluginCode %>", name: "<%=dict.PluginCode %>插件" }
                              <%
                                  if (i != j - 1){%>,<%}
                              }%>                          
                        ],
                event: "!click", //"!mouseover",
                click: tabitem_click              
            });
            //选项卡点击滑动效果
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
             var seq = $(this).attr("name").split('.')[0];//格式：s1.AppCode
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
            var seq = $(this).attr("name").split('.')[0];//格式：s1.IsNeed
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
            $("#tablist_" + pcode).append("<tr id='" + pcode + "tr" + counts + "'><td class='tdtop tdleft'><input type='text' name='"+ pcode + "|" + counts + ".Key1' /></td><td class='tdtop'><input type='text' name='"+ pcode + "|" + counts + ".Value1' /></td><td class='tdtop'></td><td class='tdtop tdright'><a onclick=\"del('" + pcode + "tr" + counts + "')\" style='cursor:pointer;color:blue' >删除</a></td></tr>");
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
            正在保存数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>
    </div>
    <div id="tabs" class="tabs">
        <div id="tabbody" class="tabsitemcontainer">
            <% using (Html.BeginForm("SavePluginInfos", "PluginInfoManage", new { IsAdd = ViewData["IsAdd"] }, FormMethod.Post, new { id = "fmEdit" }))
               {
                   Html.AntiForgeryToken();
                   Html.ValidationSummary(true);%>
            <fieldset>
                <legend class="title">基本信息</legend>
                <table width="100%" id="Table2" class="bbit-form" cellspacing="0" cellpadding="1">
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                开发商：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.TextBox("CompanyName", Model[0].CompanyName, new { style = "", Class = " safe" })%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                联系人：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop tdright">
                            <%=Html.TextBox("CompanyLinkman", Model[0].CompanyLinkman, new { style = "", Class = "safe" })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="bbit-form-cell-name tdtop tdleft">
                            <label>
                                网址：</label>
                        </td>
                        <td class="bbit-form-cell-value tdtop">
                            <%=Html.TextBox("CompanyHomePage", Model[0].CompanyHomePage, new { style = "", Class = " safe" })%>
                        </td>
                        <td class="bbit-form-cell-name tdtop">
                            <label>
                                联系电话：</label>
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
                    if (Model[i].PluginCateCode.Equals(Constants.PluginCateCode))//若是web插件
                    {%>
            <%=Html.Hidden("s" + i.ToString() + ".PreVersionPCs", Model[i].PreVersionPCs)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsNew", Model[i].IsNew)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsUse", Model[i].IsUse)%>
            <%=Html.Hidden("s" + i.ToString() + ".PluginCateCode", Model[i].PluginCateCode)%>
            <%=Html.Hidden("s" + i.ToString() + ".HashCode", Model[i].HashCode)%>
            <%=Html.Hidden("s" + i.ToString() + ".IsIgnoreConfig", Model[i].IsIgnoreConfig)%>
            <div id="Sd_<%=Model[i].PluginCode%>" style="display: none">
                <fieldset>
                    <legend class="title">插件信息</legend>
                    <table width="100%" id="Table1" class="bbit-form" cellspacing="0" cellpadding="1">
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    插件标识：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop ">
                                <%=Html.TextBox("s" + i.ToString() + ".PluginCode", Model[i].PluginCode, new { style = "background:Silver", @readonly = true })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop ">
                                <label>
                                    插件名：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".DisplayName", Model[i].DisplayName, new { style = "", Class = "required safe" })%>
                                <span class="bbit-formItemMust">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    插件地址：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".PluginUrl", Model[i].PluginUrl, new { style = "background:Silver;width:320px;", @readonly = true })%>
                            </td>
                        </tr>
                         <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    是否公开：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsPublic", Model[i].IsPublic, new { style = "", Class = "ispublic" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    应用系统：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.TextBox("s" + i.ToString() + ".AppCode", Model[i].AppCode, new { style = "", Class = " safe" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    权限标识：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".PrivilegeCode", Model[i].PrivilegeCode, new { style = "", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    必须使用：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsNeed", Model[i].IsNeed, new { style = "", Class = "acc" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    默认使用：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsDefault", Model[i].IsDefault)%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    排序号：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".Sequence", Model[i].Sequence, new { style = "width:15%", Class = " required digits" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    插件描述：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Html.TextArea("s" + i.ToString() + ".PluginSummary", Model[i].PluginSummary, new { style = "width:90%", rows = 3 })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    版本号：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".Version", Model[i].Version, new { style = "background:Silver", @readonly = true })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    版本说明：</label>
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
                    <legend class="title">插件信息</legend>
                    <table width="100%" id="basicinfotable" class="bbit-form" cellspacing="0" cellpadding="1">
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    插件标识：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop ">
                                <%=Html.TextBox("s" + i.ToString() + ".PluginCode", Model[i].PluginCode, new { style = "background:Silver", @readonly = true })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop ">
                                <label>
                                    插件名：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".DisplayName", Model[i].DisplayName, new { style = "", Class = "required safe" })%>
                                <span class="bbit-formItemMust">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    是否公开：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsPublic", Model[i].IsPublic, new { style = "", Class = "ispublic" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    应用系统：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.TextBox("s" + i.ToString() + ".AppCode", Model[i].AppCode, new { style = "", Class = " safe" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    权限标识：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".PrivilegeCode", Model[i].PrivilegeCode, new { style = "", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    必须使用：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsNeed", Model[i].IsNeed, new { style = "", Class = "acc" })%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    默认使用：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.CheckBox("s" + i.ToString() + ".IsDefault", Model[i].IsDefault)%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    启动程序名：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".FileName", Model[i].FileName, new { style = "width:320px", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    程序集全名：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".TypeFullName", Model[i].TypeFullName, new { style = "width:320px", Class = "safe" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    插件类型：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.TextBox("PCname", Model[i].PCname, new { style = "background:Silver", @readonly = true })%>
                                <%=Html.Hidden("s" + i.ToString() + ".PluginCateCode", Model[i].PluginCateCode)%>
                            </td>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    排序号：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright">
                                <%=Html.TextBox("s" + i.ToString() + ".Sequence", Model[i].Sequence, new { style = "width:15%", Class = "required digits" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    插件描述：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid"
                                colspan="3">
                                <%=Html.TextArea("s" + i.ToString() + ".PluginSummary", Model[i].PluginSummary, new { style = "width:90%", rows = 2 })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    版本号：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop tdright" colspan="3">
                                <%=Html.TextBox("s" + i.ToString() + ".Version", Model[i].Version, new { style = "background:Silver", @readonly = true })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    版本说明：</label>
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
                    <legend class="title">插件扩展信息</legend>
                    <table width="100%" id="Table4" class="bbit-form" cellspacing="0" cellpadding="1">
                        <tr>
                            <td class="bbit-form-cell-name tdtop tdleft">
                                <label>
                                    扩展标识：</label>
                            </td>
                            <td class="bbit-form-cell-value tdtop">
                                <%=Html.TextBox("s" + i.ToString() + ".ActionCode", Model[i].ActionCode, new { Class = "required safe" })%>
                                <span class="bbit-formItemMust">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="bbit-form-cell-name tdtop">
                                <label>
                                    扩展描述：</label>
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
                    <legend class="title">插件配置信息</legend>
                    <!--<div>
                        <span id="btnAdd|<%=Model[i].PluginCode%>" title='新增' style="cursor: pointer" class="Add">新增</span>
                    </div>-->
                    <table width="100%" id="tablist_<%=Model[i].PluginCode%>" style="background-color: White"
                        cellspacing="0" cellpadding="1">
                        <tr style="background: #ccc url(../../Themes/Default/images/flexigrid/gridth.gif) repeat-x left center;">
                            <td class="tdtop tdleft">
                                关键值
                            </td>
                            <td class="tdtop">
                                上传值
                            </td>
                            <td class="tdtop tdright">
                                原有值
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
                                    删除</a>
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
        <input type="button" id="Butsubmit" value="下一步" />
    </div>
</body>
</html>
