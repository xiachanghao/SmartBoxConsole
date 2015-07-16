<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.VersionTrack>" %>

<%@ Import Namespace="SmartBox.Console.Bo" %>
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
             var tha=$("#InfoDiv").outerHeight();
            var th = parent.$("#cheadtop").outerHeight();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var mainWidth = document.documentElement.clientWidth; // ��ȥ�߿����ߵĿ���     
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
                height:maiheight-160,
                mode: "showall", //ȫ�ɼ�
                items: [
                           //����ֵ
                           <% 
                                   int i = 1;                               
                                   IEnumerable<IGrouping<string, ConfigInfo>> listccc = (IEnumerable<IGrouping<string, ConfigInfo>>)ViewData["lists"];
                                   int cou= listccc.Count();
                                   foreach (var ax in listccc)
                                   {
                                      var dict = ax;
                                      var dictValue=  BoFactory.GetVersionTrackBo.GetConfig(dict.Key).DisplayName;//��ȡ����
                              %>
                                    { contentEl:"Sd_<%=dict.Key %>", name: "<%=dictValue %>" }
                              <%
                                      if (i !=cou){%>,<%}
                                      i++;
                                    }
                              %>                         
                        ],
                event: "!click", //"!mouseover",
                click: tabitem_click              
            });
            //ѡ��������Ч��
            function tabitem_click(id)
            {  
                  <%           
                       IEnumerable<IGrouping<string, ConfigInfo>> listccc1 = (IEnumerable<IGrouping<string, ConfigInfo>>)ViewData["lists"];
                       foreach (var ax in listccc1)
                       {
                          var dict = ax;
                          %>
                          var a=  "Sd_<%=dict.Key %>";
                          $("#"+a).hide();
                      <%}
                  %>
                    
                                    
                 $(id).show();

            }
            
            $('#Sd_'+'<%=ViewData["keys"]%>').show();//���õ�һ����ʾĬ�������
            
             
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
    <div id="InfoDiv">
        <fieldset>
            <legend class="title">������Ϣ</legend>
            <table width="100%" id="Table2" class="bbit-form" cellspacing="0" cellpadding="1">
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft">
                        <label>
                            �汾�ţ�</label>
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright">
                        <%=Model.VersionName%>
                    </td>
                </tr>
                 <tr>
                    <td class="bbit-form-cell-name tdtop tdleft">
                        <label>
                            �汾˵����</label>
                    </td>
                     <td class="bbit-form-cell-value tdtop tdright">
                        <%=Model.VersionSummary%>
                    </td>
                </tr>
                <tr>
                    <td class="bbit-form-cell-name tdtop tdleft">
                        <label>
                            ������˾��</label>
                    </td>
                    <td class="bbit-form-cell-value tdtop tdright" style="border-bottom: #cccccc 1px solid">
                        �Ϻ�����������
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="tabs" class="tabs">
        <div id="tabbody" class="tabsitemcontainer">
            <% using (Html.BeginForm("SavePublishInfo", "UpdaterManage", new { Vid = Model.VersionId }, FormMethod.Post, new { id = "fmEdit" }))
               {
                   Html.AntiForgeryToken();
                   Html.ValidationSummary(true);
                   var codes = "";
                   IEnumerable<IGrouping<string, ConfigInfo>> listccc2 = (IEnumerable<IGrouping<string, ConfigInfo>>)ViewData["lists"];
                   foreach (var ax in listccc2)
                   {
                       codes = ax.Key;//��ȡ�����ʶ
            %>
            <div id="Sd_<%=codes%>">
                <fieldset id="configDiv">
                    <legend class="title"></legend>
                    <table width="100%" id="tablist_<%=codes%>" style="background-color: White" cellspacing="0"
                        cellpadding="1">
                        <tr style="background: #ccc url(../../Themes/Default/images/flexigrid/gridth.gif) repeat-x left center;">
                            <td class="tdtop tdleft">
                                �ؼ�ֵ
                            </td>
                            <td class="tdtop">
                                �ϴ�ֵ
                            </td>
                            <td class="tdtop tdright">
                                �����ʶ
                            </td>
                        </tr>
                        <%
                            int j = 0;
                            foreach (var c in ax)
                            {
                                if (c.Key1 != "")
                                {
                        %>
                        <tr id="<%=codes%>tr<%=j%>">
                            <td class="tdtop tdleft">
                                <%=c.Key1%>&nbsp;
                            </td>
                            <td  class="tdtop">
                                <%=c.Value1%>&nbsp;
                            </td>
                            <td class="tdtop tdright">
                                <%=c.ConfigCategoryCode%>&nbsp;
                            </td>
                        </tr>
                        <%
                            j++;
                                }
                            } %>
                    </table>
                </fieldset>
            </div>
            <%  }//end foreach %>
            <%=Html.Hidden("ver",Model.VersionId) %>
            <%  }//end form%>
        </div>
    </div>
    <div style="text-align: center">
        <input type="button" id="Butsubmit" value="����" />
    </div>
</body>
</html>