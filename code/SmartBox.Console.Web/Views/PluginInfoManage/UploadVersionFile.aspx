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
        function SetDiv(IsUpdate, p) {
            if (IsUpdate == "")//如果是新增或者升级
            {
                $("#PluginCode").attr("disabled", false);
                $("#VersionName").attr("disabled", false);
            }
            else {
                $("#PluginCode").attr("disabled", true);
                $("#VersionName").attr("disabled", true);
                $("#PluginCode").css("background","Silver");
                $("#VersionName").css("background", "Silver");
            }

            if (p == "1") //若为web插件
            {
                $("#trVersion").show();
                $("#trCode").show();
                $("#trUrl").show();
                $("#trFile").hide();
                $("#trCofing").hide();
            }
            else {
                $("#trVersion").hide();
                $("#trCode").hide();
                $("#trUrl").hide();
                $("#trFile").show();
                $("#trCofing").show();
            }
        }


        $(document).ready(function() {

            //初始化按钮样式
            SimplyButtons.init();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var IsCate = '<%=ViewData["IsCate"]%>';
            var IsAdd = '<%=ViewData["IsAdd"]%>';
            var IsUpdate = '<%=ViewData["IsUpdate"]%>';
            var p = '<%=ViewData["PluginCateCode"]%>';
            SetDiv(IsUpdate, p);



            $("#Butsubmit").click(LoadFileToMem);

            //上传到内存中
            function LoadFileToMem() {
                var value = $("#PluginCode").val();
                var value1 = $("#PluginUrl").val();
                var vervalue = $("#VersionName").val();
                if (p == "1") //若为web插件
                {
                    if ($.trim(vervalue) == "") {
                        hiAlert('版本号不能为空');
                        return false;
                    }
                    else if (/^[^$\<\>\\\/]+$/.test(vervalue) == false) {
                        hiAlert('版本号不能包含特殊符号');
                        return false;
                    }
                    else if ($.trim(value) == "") {
                        hiAlert('插件标识不能为空');
                        return false;
                    }
                    else if (/^[^$\<\>\\\/]+$/.test(value) == false) {
                        hiAlert('插件标识不能包含特殊符号');
                        return false;
                    }
                    else if ($.trim(value1) == "") {
                        hiAlert('插件地址不能为空');
                        return false;
                    }
                    else {
                        $("#PluginCode").attr("disabled", false);
                        $("#VersionName").attr("disabled", false);
                        $("#fmEdit").submit();
                    }
                }
                else {
                    var v = $("#FileUpload1").val();
                    if ($("#FileUpload1").val() == "") {
                        hiAlert('请选择要上传的文件');
                        return false;
                    }
                    else if (/.+\.zip$/i.test(v) == false) {
                        hiAlert("请选择zip或ZIP类型文件上传")
                        return false;
                    }
                    else {
                        hiConfirm("确定要上传文件吗,若存在,原有文件将被删除", "提示", function(btn) {
                            if (btn == true) {
                                $("#PluginCode").attr("disabled", false);
                                $("#VersionName").attr("disabled", false);
                                $("#fmEdit").submit();
                            }
                        }); // end of hiConfirm
                    }
                }
            }

            var options = {
                beforeSubmit: function() {
                    $("#loadingpannel").html("正在保存数据......").show();

                    return true;
                },
                dataType: "json",
                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        openWindowsclick(data.Msg);

                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function() {
                        });
                    }

                }
            };

            $("#fmEdit").ajaxForm(options);

            $("#FileUpload1").change(function() {
                var v = $(this).val();
                if (/.+\.zip$/i.test(v) == false) {
                    alert("请选择zip或ZIP文件上传");
                    clearFileInput(document.getElementById('FileUpload1'));
                    return false;
                }
            });


        });                                                                 // ready


        //清空文件上传框,file为上传表单对像 
        function clearFileInput(file) {
            var form = document.createElement('form');
            document.body.appendChild(form);
            //记住file在旧表单中的的位置 
            var pos = file.nextSibling;
            form.appendChild(file);
            form.reset();
            pos.parentNode.insertBefore(file, pos);
            document.body.removeChild(form);
        }

        function openWindowsclick(Id) {
            parent.$("#IsCate").val("0");
            parent.$("#IsUpdate").val("1");
            parent.$("#Vids").val(Id);
            parent.$("#dicInfoName").text("3.修改插件信息");
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
                正在保存数据...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                非常抱歉，无法执行您的操作，请稍后再试</div>
        </div>
        <div class="bbit-categorycontainer">
            <% using (Html.BeginForm("SaveVerInfo", "PluginInfoManage", new { IsAdd = ViewData["IsAdd"], IsUpdate = ViewData["IsUpdate"], PluginCateCode = ViewData["PluginCateCode"] }, FormMethod.Post, new { id = "fmEdit", enctype = "multipart/form-data" }))
               {%>
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="2">
                <tr id="trVersion">
                    <td align="right" class="style1">
                        web插件版本号：
                    </td>
                    <td>
                        <%=Html.TextBox("VersionName", ViewData["ver"], new { })%>
                    </td>
                </tr>
                <tr id="trCode">
                    <td align="right" class="style1">
                        web插件标识：
                    </td>
                    <td>
                        <%=Html.TextBox("PluginCode", ViewData["code"], new { })%>
                    </td>
                </tr>
                <tr id="trUrl">
                    <td align="right" class="style1">
                        web插件地址：
                    </td>
                    <td>
                        <%=Html.TextBox("PluginUrl", ViewData["weburl"], new { style = "width:330px" })%>
                    </td>
                </tr>
                <tr id="trFile">
                    <td align="right" class="style1">
                        请选择上传文件：
                    </td>
                    <td>
                        <input type="file" name="FileUpload1" id="FileUpload1" style="width: 70%;" />
                    </td>
                </tr>
                <tr id="trCofing">
                    <td align="center" colspan="2">
                      <span style="color:Red">是否忽略插件参数信息</span><input name="ck" type="checkbox" value="true" />
                    </td>

                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <input type="button" id="Butsubmit" value="下一步" />
                    </td>
                </tr>
            </table>
            <%=Html.Hidden("VersionIds", ViewData["Vid"])%>
            <%}%>
        </div>
    </div>
</body>
</html>
