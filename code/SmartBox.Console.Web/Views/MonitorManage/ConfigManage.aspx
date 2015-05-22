<%@ Page Language="C#" MasterPageFile="~/Views/Shared/List.Master" Inherits="System.Web.Mvc.ViewPage<SmartBox.Console.Common.Entities.Monitor_Config>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	主机配置管理
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
   //修改
        function UpdateClick(cfg_id, isAdd) {
            var url = "";
            if (isAdd == "true")//新增
                url = '<%=Url.Action("AddConfig")%>/?IsAdd=1'; //1表示新增
            else
                url = '<%=Url.Action("AddConfig")%>/?IsAdd=0&cfg_id=' + cfg_id; //0表示修改
            OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "新增配置信息", onclose: function() { $("#ConfigList").flexReload(); } });
        }

        function DelClick(cfg_id) {
        }

$(document).ready(function() {
    SimplyButtons.init();
    var otherpm = 210;
    var gh = maiheight - otherpm;
    var option = {
        height: gh,
        width: mainWidth,
        url: '<%=Url.Action("GetConfigInfo")%>',
        colModel: [
            { display: 'ID', name: 'cfg_id', width: 30, sortable: false, align: 'center' },
            { display: '主机名', name: 'cfg_hostname', width: 100, sortable: false, align: 'left' },
            { display: '主机IP', name: 'cfg_hostip', width: 100, sortable: false, align: 'center' },
            { display: '配置文件', name: 'cfg_file', width: 100, sortable: false, align: 'left' },
            { display: '创建时间', name: 'cfg_createdate', width: 105, sortable: false, align: 'center' },
            { display: '创建人', name: 'cfg_createman', width: 80, sortable: false, align: 'left' },
			{ display: '更新时间', name: 'cfg_updatedate', width: 105, sortable: false, align: 'center' },
			{ display: '更新人', name: 'cfg_updateman', width: 80, sortable: false, hide: false, align: 'left' },
            { display: '更新状态', name: 'cfg_updatestatus', width: 60, sortable: false, hide: false, align: 'left' },
            { display: '是否启用', name: 'cfg_isuse', width: 60, sortable: false, process: IsUse, hide: false, align: 'center' },
			{ display: '启用时间', name: 'cfg_usedate', width: 105, sortable: false, align: 'center' },
			{ display: '操作', name: 'cfg_usedate', width: 60, sortable: false, process: Aclick, align: 'center' }
		],

        sortname: "cfg_createdate",
        sortorder: "desc",
        usepager: true,
        rp: 15,
        rowbinddata: true,
        rowhandler: contextmenu
    };
    var grid = $("#ConfigList").flexigrid(option);
    function IsUse(isuse) {
    
    return isuse == "0" ? "未启用" : "已启用";
    }

            function Aclick(cfg_id, obj) {
                var text = "";
                text = "<a onclick='UpdateClick(\"" + cfg_id + "\",\"" + false + "\")' style='cursor:pointer;color:blue' >修改</a>";
                text += "&nbsp;<a onclick='DelClick(\"" + cfg_id + "\")' style='cursor:pointer;color:blue' >删除</a>";
                
                return text;
            }

         

            $("#btnAdd").click(function() { toolbarItem_onclick("Add") });

            function contextmenu(row) {
                var ch =row.getAttribute("ch");
                var cell = ch.split("_FG$SP_");
                var kinds=0;
                if (cell&&cell.length>1) {
                    kinds = cell[8].split(',')[0];
                }
                if (kinds == 0 || kinds == 3) {
                    $(row).find("td").css("color", "red");
                }
            } //

            //新增
        function toolbarItem_onclick(cmd, grid) {
            if (cmd == "Add") {
                url = '<%=Url.Action("AddConfig") %>/?IsAdd=1'; //1表示新增(无ID)iscate标识是否能够修改分类
                OpenModelWindow(url, { width: 750, height: maiheight - 100, caption: "新增配置", onclose: function() { $("#ConfigList").flexReload(); } });
            }
        }  // end of toolbarItem_on
});
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="StyleContent" runat="server">
#ConfigList td div {
color:#000000;
}

.pGroup .pButton span {
    padding-left:0px;
    padding-right:0px;
}

.bbit-grid DIV.hDiv {
background:transparent;
}
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 1px;">
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
            </div>
        </div>
        <div>
            <table id="ConfigList" style="display: none;">
            </table>
        </div>
    </div>

</asp:Content>

