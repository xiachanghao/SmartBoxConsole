<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	安装包信息查看
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="table_box" style="">
        	<%--<h4>安装包信息查看</h4>--%>
            <div class="tab_box" style="padding-bottom:0px;">
                <ul class="tab_box_header">
                    <li class="hover"><span><a href="<%=Url.Content("~/ApplicationManage/PackageCode")%>/<%=ViewData["PID"] %>" target="ifr">二维码</a></span></li>
                    <li><span><a href="<%=Url.Content("~/ApplicationManage/PackageGifList")%>/<%=ViewData["PID"] %>" target="ifr">截图</a></span></li>
                    <li><span><a href="<%=Url.Content("~/ApplicationManage/PackageFAQList")%>/<%=ViewData["PID"] %>" target="ifr">反馈</a></span></li>
                    <li><span><a href="<%=Url.Content("~/ApplicationManage/PackageCollectionList")%>/<%=ViewData["PID"] %>" target="ifr">收藏</a></span></li>
                    <li><span><a href="<%=Url.Content("~/ApplicationManage/PackageManualList")%>/<%=ViewData["PID"] %>" target="ifr">手册</a></span></li>
                </ul>                
            </div>

            
         </div>
         <div id="dvIfr" class="easyui-panel" data-options="fit:true" style="border-left-width:0px;border-right-width:0px;">
                <iframe name="ifr" src="<%=Url.Content("~/ApplicationManage/PackageCode")%>/<%=ViewData["PID"] %>" frameborder=0 />
            </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">

    $(function () {
        var height = $('#dvIfr').height();
        var width = $('#dvIfr').width();
        $('#dvIfr iframe').height(height);
        $('#dvIfr iframe').width(width);
        $(".tab_box_header > li").each(function (index) {
            $(this).click(function () {
                
            });
        });
    })

    
   </script>
</asp:Content>
