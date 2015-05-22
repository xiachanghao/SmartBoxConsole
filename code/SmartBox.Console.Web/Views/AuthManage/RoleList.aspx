<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/List.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	角色管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 1px;">
        <div id="caltoolbar" class="ctoolbar">
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
                <div>
                    <span title='新增' class="BatchAdd">批量排序</span></div>
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
        url: '<%=Url.Action("GetRoleListByUnitCode", new { Unit_ID = Request.QueryString["UNIT_ID"] })%>',
        colModel: [
            //{ display: '编码', name: 'Role_ID', hide: false, width: 30, sortable: false,hide: true, align: 'center' },
            { display: '角色名称', name: 'Role_Name', width: 80, sortable: false, align: 'center' },
            //{ display: '单位ID', name: 'Unit_ID', width: 100, sortable: false, hide: true, align: 'left' },
            //{ display: '说明', name: 'Role_Demo', width: 100, sortable: false, hide: true, align: 'left' },
            { display: '创建时间', name: 'Role_CreatedTime', width: 120, sortable: false, hide: false, align: 'center' },
            //{ display: '创建用户', name: 'Role_CreatedUser', width: 100, sortable: false, hide: true, align: 'left' },
			//{ display: '更新时间', name: 'Role_UpdateTime', width: 105, sortable: false, hide: true, align: 'center' },
			//{ display: '更新用户', name: 'Role_UpdateUser', width: 40, sortable: false, hide: true, align: 'center' },
            { display: '排序号', name: 'Role_Sequence', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                var op = new Array();               
                return "<input style='width:60%' type='text' value='" + value + "' id='"+ pid +"' />";
            }
            },            
			{ display: '操作', width: 120, sortable: false, process: Aclick, align: 'center' },
            { display: 'ID', name: 'ID', width: 30, sortable: false, align: 'left', hide: false }
		],

        sortname: "Role_Sequence",
        sortorder: "asc",
        usepager: true,
        rp: 10,
        rowbinddata: true,
        //rowhandler: contextmenu
    };
    var grid = $("#UList").flexigrid(option);

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
                    data: { pe_ids: ids,pe_seqs:seqs ,type:'role'},
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

    $("span.Add").click(function () {
                url = '<%=Url.Action("AddModifyRole", new { Unit_ID = Request.QueryString["UNIT_ID"] })%>';
                parent.OpenModelWindow(url, { width: 650, height: 175, caption: "新增角色", onclose: function () { $("#UList").flexReload(); } });
            });
    });



    function Aclick(UL_ID, obj) {
        var text = "";
        
        text = "<a onclick='SetRoleModifyClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改</a>";
        text += "&nbsp;<a onclick='SetPersonClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >人员</a>";
        text += "&nbsp;<a onclick='SetRoleClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >权限</a>";
        text += "&nbsp;<a onclick='DelClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
        
                
        return text;
    }

    //修改角色
    function SetRoleModifyClick(Role_ID) {
        var url = "";
        url = '<%=Url.Content("~/AuthManage/AddModifyRole?roleid=")%>' + Role_ID;
        OpenModelWindow(url, { width: 650, height: maiheight - 280, caption: "修改角色", onclose: function() { $("#UList").flexReload(); } });
    }

    //设置人员
    function SetPersonClick(obj,UL_ID) {
        var url = "";
        url = '<%=Url.Content("~/AuthManage/PersonsTreeSelect?roleid=")%>' + obj +'&unitid=<%=Request.QueryString["unit_id"] %>'; 
        OpenModelWindow(url, { width: 300, height: maiheight - 40, caption: "设置人员", onclose: function() { $("#UList").flexReload(); } });
    }

    //设置角色权限
    function SetRoleClick(obj) {
        var url = "";
        url = '<%=Url.Content("~/AuthManage/FunctionsTreeSelect?roleid=")%>' + obj + '&unitid=<%=Request.QueryString["unit_id"] %>'; 
        OpenModelWindow(url, { width: 300, height: maiheight - 40, caption: "设置权限", onclose: function() { $("#UList").flexReload(); } });
    }

    function DelClick(role_id) {
        var rel=confirm("是否将角色删除?")
        if(rel==false)
        {
            return;
        }

    $.ajax({
            type: "post",
            dataType: "json",
            //contentType: "application/json; charset=utf-8",
            url: "<%=Url.Content("~/AuthManage/DelRole")%>",
            //data: '{"userinfo":[{"name":"zs","age":"21"},{"name":"ls","age":"25"}]}',
            data: {
                role_id: function () {
                    return role_id;
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
