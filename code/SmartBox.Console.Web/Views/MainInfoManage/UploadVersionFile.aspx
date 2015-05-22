<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EditVerInfo</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/simplybuttons.css")%>" rel="Stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/uploadify.css")%>" rel="stylesheet"
        type="text/css" />

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/SimplyButtons.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

    <style type="text/css">
        .bspan
        {
            background-color: #E8F1F8 !important;
        }
        a
        {
            cursor: pointer !important;
        }
        #txtUrl
        {
            width: 469px;
        }
        .style1
        {
            width: 169px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function() {

            //��ʼ����ť��ʽ
            SimplyButtons.init();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var IsAdd = '<%=ViewData["IsAdd"]%>';
            var IsUpdate = '<%=ViewData["IsUpdate"]%>';

            $("#Butsubmit").click(LoadFileToMem);

            //�ϴ����ڴ���
            function LoadFileToMem() {
                var v = $("#FileUpload1").val();
                if ($("#FileUpload1").val() == "") {
                    hiAlert('��ѡ��Ҫ�ϴ����ļ�');
                    return false;
                }
                else if (/.+\.zip$/i.test(v) == false) {
                    hiAlert("��ѡ��zip��ZIP�����ļ��ϴ�")
                    return false;
                }
                else {
                    hiConfirm("ȷ��Ҫ�ϴ��ļ���,������,ԭ���ļ�����ɾ��", "��ʾ", function(btn) {
                        if (btn == true) {
                            $("#fmEdit").submit();
                        }
                    }); // end of hiConfirm
                }
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
                        openWindowsclick(data.Msg);

                    }
                    else {
                        hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ', function() {
                        });
                    }

                }
            };

            $("#fmEdit").ajaxForm(options);

            $("#FileUpload1").change(function() {
                var v = $(this).val();
                if (/.+\.zip$/i.test(v) == false) {
                    alert("��ѡ��zip��ZIP�ļ��ϴ�");
                    clearFileInput(document.getElementById('FileUpload1'));
                    return false;
                }
            });


        });                                                                  // ready


        //����ļ��ϴ���,fileΪ�ϴ������� 
        function clearFileInput(file) {
            var form = document.createElement('form');
            document.body.appendChild(form);
            //��סfile�ھɱ��еĵ�λ�� 
            var pos = file.nextSibling;
            form.appendChild(file);
            form.reset();
            pos.parentNode.insertBefore(file, pos);
            document.body.removeChild(form);
        }

        function openWindowsclick(Id) {
            parent.$("#IsUpdate").val("1");
            parent.$("#Vids").val(Id);
            parent.$("#dicInfoName").text("3.�޸�������Ϣ");
            var IsAdd = '<%=ViewData["IsAdd"]%>';
            var url = '<%=Url.Action("UpdatePluginInfo") %>/?Vid=' + Id + '&IsAdd=' + IsAdd;
            window.location = url;
        }
  
    </script>

</head>
<body>
    <div>
        <div class="ajaxmsgpanel" style="border: none;">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                ���ڱ�������...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                �ǳ���Ǹ���޷�ִ�����Ĳ��������Ժ�����</div>
        </div>
        <div class="bbit-categorycontainer">
            <% using (Html.BeginForm("SaveFileInfo", "MainInfoManage", new { IsAdd = ViewData["IsAdd"], IsUpdate = ViewData["IsUpdate"] }, FormMethod.Post, new { id = "fmEdit", enctype = "multipart/form-data" }))
               {%>
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr id="trFile">
                    <td align="right" class="style1">
                        ��ѡ���ϴ��ļ���
                    </td>
                    <td>
                        <input type="file" name="FileUpload1" id="FileUpload1" style="width: 70%;" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <input type="button" id="Butsubmit" value="��һ��" />
                    </td>
                </tr>
            </table>
            <%=Html.Hidden("VersionIds", ViewData["Vid"])%>
            <%}%>
        </div>
    </div>
</body>
</html>
