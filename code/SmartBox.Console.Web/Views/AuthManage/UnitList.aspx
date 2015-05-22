<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/List.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	单位管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 1px;">
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div><%if ((ViewContext.Controller as SmartBox.Console.Web.Controllers.MyControllerBase).IsSystemManager == false && String.IsNullOrEmpty(Request.QueryString["upper_unit_id"]))
                       {} else { %>
                    <span title='新增' class="Add">新增</span><%} %></div>
                <div>
                    <span title='批量排序' class="BatchAdd">批量排序</span></div><div>
                    <span id="CopyBUA" title='复制BUA单位' class=" ">复制BUA单位</span></div>

                    <div>
                    <span id="syncUnit" title='同步单位至外网' class=" ">同步单位至外网</span></div>
            </div>
        </div>        
        <div>
            <table id="UList" style="display: none;">
            </table>
        </div>       
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
$(document).ready(function() {
    SimplyButtons.init();
    var otherpm = 106;
    var gh = maiheight - otherpm;
    var option = {
        height: gh,
        width: mainWidth,
        url: '<%=Url.Action("GetUnitListByUpperUnitCode", new { Upper_Unit_ID = Request.QueryString["UPPER_UNIT_ID"] })%>',
        colModel: [
            
            //{ display: '编码', name: 'Unit_ID', hide: true, width: 30, sortable: false,hide: true, align: 'center' },
            { display: '单位名称', name: 'Unit_Name', width: 130, sortable: false, align: 'center' },
            //{ display: '上级单位编码', name: 'Upper_Unit_ID', width: 100, hide: true, sortable: false, align: 'left' },
            { display: '说明', name: 'Unit_Demo', width: 100, sortable: false, hide: true, align: 'left' },
            //{ display: '路径', name: 'Unit_Path', width: 105, sortable: false, hide: true, align: 'center' },
            { display: '创建时间', name: 'Unit_CreatedTime', width: 110, sortable: false, hide: false, align: 'left' },
			{ display: '创建用户', name: 'Unit_CreatedUser', width: 105, sortable: false, hide: true, align: 'center' },
			//{ display: '更新时间', name: 'Unit_UpdateTime', width: 40, sortable: false, hide: true, align: 'center' },
            //{ display: '更新用户', name: 'Unit_UpdateUser', width: 40, sortable: false, hide: true, align: 'center' },
            { display: '排序号', name: 'Unit_Sequence', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                var op = new Array();               
                return "<input style='width:60%' type='text' value='" + value + "' id='"+ pid +"' />";
            }
            },
			{ display: '操作', width: 60, sortable: false, process: Aclick, align: 'center' },
            { display: 'ID', name: 'ID', width: 30, sortable: false, align: 'left', hide: false }
		],

        sortname: "Unit_Sequence",
        sortorder: "asc",
        usepager: true,
        rp: 10,
        rowbinddata: true
        //rowhandler: contextmenu
    };
    var grid = $("#UList").flexigrid(option);

    $("span.Add").click(function () {
                url = '<%=Url.Action("AddModifyUnit", new { Upper_Unit_ID = Request.QueryString["upper_UNIT_ID"] })%>';
                parent.OpenModelWindow(url, { width: 750, height: 210, caption: "新增单位", onclose: function () { $("#UList").flexReload(); parent.refreshTree();} });
            });$('#CopyBUA').click(function() {$("#loadingpannel").html("正在处理...").show();$.ajax({
                    type: "POST",
                    url: '<%=Url.Action("CopyBUAUnit")%>',
                    data: { },
                    dataType: "json",
                    success: function (data) {
                        $("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            hiAlert(data.Msg, "操作成功");
                            $("#UList").flexReload(); 
                        }
                        else {
                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });});

$('#syncUnit').click(function() {$("#loadingpannel").html("正在处理...").show();$.ajax({
                    type: "POST",
                    url: '<%=Url.Action("SyncUnitToAppCenter")%>',
                    data: { },
                    dataType: "json",
                    success: function (data) {
                        $("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            hiAlert(data.Msg, "操作成功");
                            $("#UList").flexReload(); 
                        }
                        else {
                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });});
            

             $("span.BatchAdd").click(function () {
                $("#loadingpannel").html("正在处理...").show();
                var ids=',';
                var seqs=',';
                var e= document.getElementById('UList');
                var cb = e.getElementsByTagName("input");  
                          
                for (var i = 0; i < cb.length; i++) {
                    if (cb[i].type == "text") {                       
                            ids += cb[i].id + ','; 
                            seqs+= cb[i].value+',';                     
                    }
                }

                $.ajax({
                    type: "POST",
                    url: '<%=Url.Action("SetSqence")%>',
                    data: { pe_ids: ids,pe_seqs:seqs ,type:'unit'},
                    dataType: "json",
                    success: function (data) {
                        $("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            hiAlert(data.Msg, "操作成功");
                            $("#UList").flexReload(); 
                        }
                        else {
                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });
            });

    });

  

    function Aclick(UL_ID, obj) {
        var text = "";
        text = "<a onclick='UpdateClick(\"" + UL_ID + "\",\"" + obj + "\")' style='cursor:pointer;color:blue' >修改</a>";
        text += "&nbsp;<a onclick='DelClick(\"" + UL_ID + "\",\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
                
        return text;
    }

    //修改单位信息
    function UpdateClick(UL_ID, obj) {
        var url = "";
        url = '<%=Url.Content("/AuthManage/AddModifyUnit") %>?Unit_ID=' + obj + '&Upper_Unit_ID=<%=Request.QueryString["unit_id"] %>'; //1表示新增
 
        OpenModelWindow(url, { width: 750, height: maiheight - 248, caption: "修改单位信息", onclose: function() { $("#UList").flexReload(); parent.refreshTree();} });
    } 

    function DelClick(cfg_id, obj) {
        var rel=confirm("是否将该组织删除?")
        if(rel==false)
        {
            return;
        }

    $.ajax({
            type: "post",
            dataType: "json",
            //contentType: "application/json; charset=utf-8",
            url: "<%=Url.Content("~/AuthManage/DelUnit")%>",
            //data: '{"userinfo":[{"name":"zs","age":"21"},{"name":"ls","age":"25"}]}',
            data: {
                unit_id: function () {
                    return obj;
                }
            },
            success: function (json) {
                hiAlert(json.Msg, '提示', function () {
                    if (json.IsSuccess) {
                        parent.location.reload();
                    }
                });
                
                //showMessage(json.d, function () { window.close(); opener.location.reload(); });
            }, failure: function (json) {
                //showMessage(json.d, function () { window.close(); });
            }, error: function (xhr, status) {
                //showMessage("Sorry, there was a problem!", function () { window.close(); });
            }
        });
    }

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
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="StyleContent" runat="server">
#UList td div {
color:#000000;
}

.pGroup .pButton span {
    padding-left:0px;
    padding-right:0px;
}


div.ctoolbar {
height:35px;
padding-left:0px;
}
.fbutton 
    {
        margin-top:0px!important;
        margin-left:5px!important;
    }
</asp:Content>
