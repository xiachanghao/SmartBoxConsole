<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.VersionTrack>" %>

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

    <% if (false)
       { %>

    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <%} %>

    <script type="text/javascript">


        $(document).ready(function() {

            //初始化按钮样式
            SimplyButtons.init();
            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            var IsUpdate = '<%=ViewData["IsUpdate"]%>';

            if (IsUpdate == "1" || IsUpdate == "2") //若为修改
            {
                $("#VersionName").attr("disabled", true);
            }
            else {
                $("#VersionName").attr("disabled", false);
            }

            $("#Butsubmit").click(function() { NextC() });

            var options = {
                beforeSubmit: function() {
                    $("#VersionName").attr("disabled", false);
                    $("#loadingpannel").html("正在保存数据......").show();
                    return true;
                },
                dataType: "json",

                success: function(data) {
                    $("#loadingpannel").hide();
                    if (data.IsSuccess) {
                        LoadFileToMem(data.Msg);
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                    }
                }
            };
            //校验
            $.validator.addMethod("safe", function(value, element) {
            return this.optional(element) || /^[^$\<\>\\\/]+$/.test(value);
            }, "不能包含特殊符号");

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

            function LoadFileToMem(msg) {
                parent.$("#IsVersion").val(1);
                parent.$("#dicInfoName").text("2.上传文件");
                parent.$("#AddVid").val(msg);
                var IsUpdate = '<%=ViewData["IsUpdate"]%>';
                if (IsUpdate != "1" && IsUpdate != "2")//若为新增或者升级
                    parent.$("#IsUpdate").val(2);
                var IsAdd = parent.$("#IsAdd").val();
                var Vid = parent.$("#Vids").val();
                var IsUpdates = parent.$("#IsUpdate").val();
                url = '<%=Url.Action("UploadVersionFile") %>/?IsAdd=' + IsAdd + '&Vid=' + Vid + '&IsUpdate=' + IsUpdates + '&AddVid=' + msg;
                window.location = url;
            }
        }); 
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
        .style1
        {
            width: 256px;
        }
    </style>
</head>
<body id="bodyDiv">
    <div>
        <div class="cHead" style="border: none;">
            <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
                正在保存数据...</div>
            <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
                非常抱歉，无法执行您的操作，请稍后再试</div>
        </div>
        <div class="bbit-categorycontainer">
            <% using (Html.BeginForm("SaveVersions", "MainInfoManage", new { IsAdd = ViewData["IsAdd"],IsUpdate = ViewData["IsUpdate"],Vid=ViewData["Vid"] }, FormMethod.Post, new { id = "fmEdit" }))
               {%>
            <table width="100%" id="Table2" class="bbit-form" cellspacing="0" cellpadding="1">
                <tr>
                    <td class="style1" style="border-bottom: #cccccc 1px solid; border-left:#cccccc 1px solid; border-top:#cccccc 1px solid">
                        <label>
                            版本号（必填唯一）：</label>
                    </td>
                    <td class="tdtop tdright" style=" border-bottom:#cccccc 1px solid" align="left">
                        <%=Html.TextBox("VersionName", Model.VersionName, new { style = "width:43%", Class = "required safe" })%>
                    </td>
                </tr>
                <tr>
                    <td class=" tdtop tdleft"  style=" border-right:#cccccc 1px solid"  colspan="2">
                        <label>
                            版本说明：</label>
                    </td>
                </tr>
                <tr>
                    <td class=" tdtop tdright" style="border-left: #cccccc 1px solid;" colspan="2">
                        <%=Html.TextArea("VersionSummary", Model.VersionSummary, new { style = "width:90%", rows = 3 })%>
                    </td>
                </tr>
                <tr>
                    <td class="style1" style="border-bottom: #cccccc 1px solid; border-left:#cccccc 1px solid; border-top:#cccccc 1px solid;">
                        <label>
                            开发公司：</label>
                    </td>
                    <td class="tdtop tdright" align="left" style="border-bottom: #cccccc 1px solid">
                        上海互联网软件
                    </td>
                </tr>
            </table>
            <%=Html.Hidden("compname","上海互联网软件") %>
            <%} %>
        </div>
    </div>
    <div style="text-align: center">
        <input type="button" id="Butsubmit" value="下一步" />
    </div>
</body>
</html>
