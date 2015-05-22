<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	服务参数
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>服务参数</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                <tr>
                        <th width="30%">关键字：</th>
                        <td class=""><input id="tbKey" name="tbKey" type="text" /></td>
                    </tr>
                    <tr>
                    <th>
                        值：
                    </th>
                    <td>
                        <input id="tbVal" name="tbVal" type="text" />
                    </td>
                    
                </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <input type="button" id="btnSave" class="btnskin_b" value="保存" />
                            <input type="button" class="btnskin_b" value="取消" onclick="javascript:closeWin();" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<link href="../../telerik/styles/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="../../telerik/styles/kendo.silver.min.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../telerik/js/jquery.min.js" type="text/javascript"></script>--%>
    <script src="../../telerik/js/kendo.all.min.js" type="text/javascript"></script>

    

<script type="text/javascript">

var entity = <%=ViewData["entity"]%>;
function closeWin() {
    if (parent)
        parent.CloseWind(false);
}
    

    function parseEntity() {
        if (entity.Key)
            $('#tbKey').val(entity.Key);
        if (entity.Value)
            $('#tbVal').val(entity.Value);
    }

    $(document).ready(function () {
        parseEntity();

        $('#btnSave').click(function () {
            var o = {};

            o.key = $('#tbKey').val();
            o.val = $('#tbVal').val();

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("ServiceConfigAddPost") %>',
                data: o,
                dataType: "json",
                success: function (data) {
                      $.messager.alert('提示', data.d, 'info', function() {
                            if (data.r && parent)
                                parent.CloseWind(true);
                            if (!data.r && parent)
                                parent.CloseWind(false);
                        });
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
            return false;
        });

        
    });

</script>
</asp:Content>
