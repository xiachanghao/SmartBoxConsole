<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.SMC_PackageFAQ>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>回复用户反馈</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dp.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="Stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.datepicker.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.dropdown.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <% if (false)
       { %>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <style type="text/css">
        .title
        {
            background-color: #60a6cf;
            height: 24px;
            line-height: 24px;
            font-size: 12px;
        }
        .title strong
        {
            font-size: 16px;
        }
        table
        {
            border: 1px;
            border-style: solid;
            border-collapse: collapse;
        }
        .form
        {
            width: 100%;
            background-color: #fff;
            margin: 0px;
            padding: 0px;
        }
        .notNull
        {
            color: #F00;
        }
        td.bbit-form-cell-name
        {
            min-height:30px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $("document").ready(function () {

            $(".bbit-form tr:even").addClass("even");
            $(".bbit-form tr:odd").addClass("odd");
            $("a#Save").click(save);
            $("#CloseImgBtn1").click(function () {
                CloseModelWindow(null, true);
            });
            function save() {
                $("#frmReplyPackageFAQ").submit();
            }

            var saveoptions =
            {
                beforeSumbit: function () {
                    $("#loadingpannel").html("正在保存数据...").show();
                    return true;
                },
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess) {
                        $("#loadingpannel").hide();
                        hiAlert(data.Msg, '提示', function () {
                            parent.$.closeIfrm(null, true);
                        });
                    }
                    else {
                        hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示', function () { });
                    }
                }
            };


            //保存之验证
            $("#frmReplyPackageFAQ").validate({
                rules: {
                    Name: { required: true },
                    DisplayName: { required: true },
                    Seq: { number: true, required: true }
                },
                messages: {
                    Name: { required: "请填写权限标识！" },
                    DisplayName: { required: "请填写显示名称！" },
                    Seq: { number: "排序号必须是数字", required: "请填写排序号" }
                },
                submitHandler: function (form) {
                    $("#frmReplyPackageFAQ").ajaxSubmit(saveoptions);
                },
                errorElement: "div",
                errorClass: "cusErrorPanel",
                errorPlacement: showerror
            });

            function showerror(error, target) {
                var pos = target.position();
                var height = target.height();
                var newpos = { left: pos.left, top: pos.top + height + 2 }
                var form = $("#frmReplyPackageFAQ");
                var v = getiev();
                if (v <= 6) {
                    var t = error.text();
                    error.html('<iframe style="position:absolute;z-index:-1;width:100%;height:35px;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe><div class="cusError">' + t + '</div>');
                }
                error.appendTo(form).css(newpos);
            }

        });
    </script>
</head>
<body>
    <div class="ajaxmsgpanel">
        <div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
            正在保存数据...</div>
        <div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
            非常抱歉，无法执行您的操作，请稍后再试</div>
    </div>
    <div class="toolBotton">
        <a id="Save" class="imgbtn"><span class="Save" title="保存">保存</span></a> <a id="CloseImgBtn1"
            class="imgbtn"><span class="Close" title="关闭">关闭</span></a>
    </div>
    <% using (Html.BeginForm("ReplyPackageFAQ", "ApplicationManage", FormMethod.Post, new { id = "frmReplyPackageFAQ" }))
       { %>
    <table class="bbit-form" width="100%" cellspacing="1" cellpadding="0">
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                用户名称：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">           
               <%=Model.pf_uname %>           
            </td>
        </tr>
         <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                反馈时间：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">           
               <%=Model.pf_askdate %>           
            </td>
        </tr>
          <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                问题：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">           
               <%=Model.pf_question %>           
            </td>
        </tr>
        <tr>
            <td class="bbit-form-cell-name tdtop tdleft tdright">
                答复：
            </td>
            <td class="bbit-form-cell-value tdtop tdright">           
                <%= Html.TextArea("pf_answer", Model.pf_answer, new { @Style = "width:95%;rows=6", @Class = "DisplayName" })%>
            </td>
        </tr>
        
    </table>
    <%= Html.Hidden("ID", Model.pf_id)%>
    <% } %>
</body>
</html>

