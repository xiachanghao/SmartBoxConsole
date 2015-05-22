<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.VersionTrack>" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>plush</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");

            $("#btnNext").click(function() { save() });
            function save() {
                hiConfirm("ȷ��Ҫ�����˳�����", "��ʾ", function(btn) {
                    if (btn == true) {
                        $("#fmEdit").submit();
                    }
                }); // end of hiConfirm
            }


            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("���ڱ�������......").show();
                    return true;
                },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        hiAlert(data.Msg, '��ʾ', function() { window.location = '<%=Url.Action("PluginVersionList") %>'; });
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
                    pos.left = document.documentElement.clientWidth - 156;
                }
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#fmEdit");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }  // end of showerror           
        });                 // ready
    </script>

</head>
<body>
    <div>
        <div class="cHead" style="border: none;">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                ���ڱ�������...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                �ǳ���Ǹ���޷�ִ�����Ĳ��������Ժ�����</div>
        </div>
        <% using (Html.BeginForm("Publishs", "PluginManage", FormMethod.Post, new { id = "fmEdit" }))
           {%>
        <div class="bbit-categorycontainer">
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr>
                    <td align="center">
                        <span style="color: Red; font-size: 15px;">���һ����������ɷ����˳���</span>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="button" id="btnNext" value="��ɷ���" />
                    </td>
                </tr>
            </table>
        </div>
        <%=Html.Hidden("CreateTime", Model.CreateTime)%>
        <%=Html.Hidden("CreateUid", Model.CreateUid)%>
        <%=Html.Hidden("FilePath", Model.FilePath)%>
        <%=Html.Hidden("PluginCode", Model.PluginCode)%>
        <%=Html.Hidden("VersionId", Model.VersionId)%>
        <%=Html.Hidden("VersionName", Model.VersionName)%>
        <%=Html.Hidden("PreVersionId", Model.PreVersionId)%>
        <%}%>
    </div>
</body>
</html>
