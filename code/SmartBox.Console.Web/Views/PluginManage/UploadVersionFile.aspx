<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.VersionTrack>" %>

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
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            //��ʼ����ť��ʽ
            SimplyButtons.init();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");

            var IsAdd = '<%=ViewData["IsAdd"]%>';


            $("#Butsubmit").click(LoadFileToMem);

            $("#FileUpload1").change(function() {

                var v = $(this).val();
                if (/.+\.zip$/i.test(v) == false) {
                    alert("��ѡ��zip��ZIP�ļ��ϴ�");
                    return false;
                }
            });


            //�ϴ����ڴ���
            function LoadFileToMem() {
                if (IsAdd == "1")//��Ϊ������������ 
                {
                    var v = $("#FileUpload1").val();
                    if ($("#FileUpload1").val() == "") {
                        alert('��ѡ��Ҫ�ϴ����ļ�');
                        return false;
                    }
                    else if (/.+\.zip$/i.test(v) == false) {
                        alert("��ѡ��zip��ZIP�ļ��ϴ�")
                        return false;
                    }
                    else {
                        hiConfirm("ȷ��Ҫ�ϴ��ļ���", "��ʾ", function(btn) {
                            if (btn == true) {
                                $("#fmEdit").submit();
                            }
                        }); // end of hiConfirm
                    }

                }
                else {
                    if ($("#FileUpload1").val() != "") {
                        hiConfirm("ȷ��Ҫ�����ϴ��ļ���,ԭ�ϴ��ļ�����ɾ��", "��ʾ", function(btn) {
                            if (btn == true) {
                                $("#fmEdit").submit();
                            }
                        }); // end of hiConfirm
                    }
                    else {
                        hiConfirm("��û��ѡ���ϴ��ļ�������ʹ������һ���ϴ����ļ� ", "��ʾ", function(btn) {
                            if (btn == true) {
                                $("#fmEdit").submit();
                            }
                        }); // end of hiConfirm
                    }
                }

            }

            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("���ڱ�������......").show();

                    return true;
                },
                dataType: "json",
                error: function(xhr, status, e) {
                    xhr.abort();
                    hiAlert("�ϴ�ʧ��,�ļ�̫��(����10M)���������", '��ʾ');
                    $("#loadingpannel").hide();

                },
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        var vid = "";
                        vid = data.Msg;
                        $("#VID").val(vid); //�����������õ���Ӧ�汾ID���Ӷ����Ի�ȡ���ID
                        openWindowsclick();

                    }
                    else {
                        hiAlert("����ʧ�ܣ����ܵ�ԭ��:\r\n" + data.Msg, '��ʾ', function() {
                        });

                    }
                }
            };
            $("#fmEdit").ajaxForm(options);

        });                        // ready

        function openWindowsclick() {
                var Id = $("#VID").val();
                var url = '<%=Url.Action("UpdatePluginInfo") %>/?verid=' + Id;
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
            <% using (Html.BeginForm("SaveVerInfo", "PluginManage", new { IsAdd = ViewData["IsAdd"] }, FormMethod.Post, new { id = "fmEdit", enctype = "multipart/form-data" }))
               {%>
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr>
                    <td>
                        <span style="color: Red">1.����������������,�����ϴ��ļ�</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">2.���Ѵ����ϴ��ļ���ԭ���ļ�����ɾ��(��ֱ����һ��)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">3.�����ϴ��ļ���ָ����ʽ��ZIP�ļ�����ȷ���ϴ��ļ��е�ָ��������Ϣ�Ƿ���ȫ(XML�ļ��ȸ�ʽ��ȷ)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">4.��ȷ���ϴ��ļ��е�ָ��������Ϣ�Ƿ���ȫ(XML�ļ��ȸ�ʽ��ȷ)</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">5.�ϴ��ļ����ϴ����ļ��������ָ��Ŀ¼�´����ģ��԰汾Ϊ�������ļ���</span>
                    </td>
                </tr>
                 <tr>
                     <td align="center">
                        <div style="width: 79%; float: left">��ѡ���ϴ��ļ���
                            <input type="file" name="FileUpload1" id="FileUpload1" style="width: 40%" />
                        </div>
                        <div style="width: 20%; text-align: center; float:right">
                        
                          <input type="button" id="Butsubmit" value="��һ��" />
                        </div>
                    </td>
                </tr>
            </table>
            <%=Html.Hidden("VID", ViewData["versionId"])%>
            <%=Html.Hidden("VersionId", ViewData["versionId"])%>
            <%}%>
        </div>
    </div>
</body>
</html>
