<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/flexigrid.css") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/dailog.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/contextmenu.css") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/tree.css")%>" rel="Stylesheet" type="text/css" />

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.tree.js") %>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.flexigrid.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.contextmenu.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

    <% if (false)
       { %>

    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <%} %>

    <script type="text/javascript">
        var __GridWidth = { expandwidth: 0, foldwidth: 0 };
        $(document).ready(function() {

            var maiheight = document.documentElement.clientHeight - 2;
            var IsCate = $("#IsCate").val();
            var th = $("#cheadtop").outerHeight();
            $("#ifm").height(maiheight - th - 2);
            $("#dictinfotree").height(maiheight - th + 24);


            $("#dictinfotree").treeview(
                      {
                          url: '<%=Url.Action("GetInfoTree") %>/',
                          showcheck: false,
                          onnodeclick: TreeNode_Click,
                          theme: "bbit-tree-no-lines" //bbit-tree-lines ,bbit-tree-no-lines,bbit-tree-arrows
                      }
                    );

            var urls = '<%=Url.Action("SelectType") %>/?IsCate=' + IsCate;
            $("#ifm").attr("src", urls);

            function TreeNode_Click(data) {

                var IsCate = $("#IsCate").val();
                var IsAdd = $("#IsAdd").val();
                var Vid = $("#Vids").val();
                var IsUpdate = $("#IsUpdate").val();
                var PluginCateCode = $("#PluginCateCodes").val();

                var id = data.id;
                var url = "";
                if (id == "0") {
                    $("#dicInfoName").text("1.选择插件类型");
                    var url = '<%=Url.Action("SelectType") %>/?IsCate=' + IsCate;
                    $("#ifm").attr("src", url);
                }
                else if (id == "1") {
                    if (PluginCateCode == "")
                        hiAlert("请先选择类型");
                    else {
                        $("#dicInfoName").text("2.上传插件");
                        url = '<%=Url.Action("UploadVersionFile") %>/?IsAdd=' + IsAdd + '&Vid=' + Vid + '&IsUpdate=' + IsUpdate + '&PluginCateCode=' + PluginCateCode;
                        $("#ifm").attr("src", url);
                    }
                }
                else if (id == "2") {
                    if (Vid == "")//若为还没新增
                        hiAlert("请先上传插件");
                    else //升级或者修改
                    {
                        if (IsUpdate == "1") {
                            $("#dicInfoName").text("3.修改插件信息");
                            url = '<%=Url.Action("UpdatePluginInfo") %>/?Vid=' + Vid + '&IsAdd=' + IsAdd;
                            $("#ifm").attr("src", url);
                        }
                        else
                            hiAlert("请先上传升级文件");
                    }
                }
                else if (id == "3") {
                    if (Vid == "")//若为还没新增
                        hiAlert("4.请先上传插件");
                    else //升级或者修改
                    {
                        if (IsUpdate == "1") {
                            $("#dicInfoName").text("4.保存/发布");
                           
                            url = '<%=Url.Action("PublishInfo") %>/?Vid=' + Vid;
                            $("#ifm").attr("src", url);
                        }
                        else
                            hiAlert("请先上传升级文件");
                    }
                }
            } // end of TreeNode_Click

            $(".bit-tree-ec-icon bbit-tree-elbow").hide();
            $(".bbit-tree-node-icon").hide();
        });

        function CloseW() {
            CloseModelWindow(null, true);
        }
    </script>

</head>
<body>
    <div class="MainContainer">
        <table width="100%; " cellpadding="1" cellspacing="0">
            <tr>
                <td id="lefttd" style="width: 20%; height:100%" valign="top">
                    <div id="dictinfotree" class="tree">
                    </div>
                </td>
                <td valign="top" >
                    <div id="cheadtop" class="cHead">
                        <div style="">
                            <span id="dicInfoName">1.选择插件类型</span>
                        </div>
                    </div>
                    <div>
                    <iframe id="ifm" name="ifm"  src="" style="width:100%;" frameborder="0"></iframe></div>
                </td>
            </tr>
        </table>
        <%=Html.Hidden("IsAdd", ViewData["IsAdd"])%>
        <%=Html.Hidden("Vids", ViewData["Vid"])%>
        <%=Html.Hidden("IsUpdate", ViewData["IsUpdate"])%>
        <%=Html.Hidden("IsCate", ViewData["IsCate"])%>
        <%=Html.Hidden("PluginCateCodes", ViewData["type"])%>
    </div>
</body>
</html>
