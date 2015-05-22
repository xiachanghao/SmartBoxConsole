<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/List.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	管理员
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 1px;">
        <div id="caltoolbar" class="ctoolbar">
        <%if (((bool)this.ViewData["show_button"])) {%>
            <div id="btnAdd" class="fbutton">
                <div>
                    <span title='新增' class="Add">新增</span></div>
                <div>
                    <span title='新增' class="BatchAdd">批量排序</span></div>
            </div>
            <%} %>
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
        url: '<%=Url.Action("GetUserListByUnitCode", new { Unit_ID=Request.QueryString["UNIT_ID"]})%>',
        colModel: [
            { display: 'ID', name: 'UL_ID', hide: false, width: 30, sortable: false,hide: true, align: 'center' },
            { display: '登录帐号', name: 'UL_UID', width: 60, sortable: false, align: 'center' },
            { display: '姓名', name: 'UL_Name', width: 50, sortable: false, align: 'left' },
            { display: '角色', name: 'Role_ID', width: 70, sortable: false, align: 'left' },
            { display: '手机号', name: 'UL_MobilePhone', width: 90, sortable: false, align: 'center' },
            { display: '邮件地址', name: 'UL_MailAddress', width: 100, sortable: false, align: 'left' },
			{ display: '创建时间', name: 'UL_CreatedTime', width: 105, hide: true, sortable: false, align: 'center' },
			<%--{ display: '排序号', name: 'UL_Sequence', width: 40, sortable: false, hide: false, align: 'center' },--%>
            { display: '排序号', name: 'Unit_Sequence', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                var op = new Array();               
                return "<input style='width:60%' type='text' value='" + value + "' id='"+ pid +"' />";
            }
            },
			{ display: '操作', width: 140, sortable: false, process: Aclick, align: 'center' }
		],

        sortname: "UL_Sequence",
        sortorder: "asc",
        usepager: true,
        rp: 10,
        rowbinddata: true,
        rowhandler: contextmenu
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
                    data: { pe_ids: ids,pe_seqs:seqs ,type:'user'},
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
                url = '<%=Url.Action("AddModifyUser", new { Upper_Unit_ID = Request.QueryString["UNIT_ID"] })%>';
                parent.OpenModelWindow(url, { width: 750, height: 370, caption: "新增用户", onclose: function () { $("#UList").flexReload(); } });
            });
    });



    function Aclick(UL_ID, obj) {
        var text = "";
        text = "<a onclick='SetRoleClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >角色</a>";
        text += "&nbsp;<a onclick='DelClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
        text += "&nbsp;<a onclick='SetUserModifyClick(\"" + obj + "\")' style='cursor:pointer;color:blue' >修改</a>";
        text += "&nbsp;<a onclick='SetUserPassWord(\"" + obj + "\")' style='cursor:pointer;color:blue' >重置密码</a>";
                
        return text;
    }

    //设置角色
    function SetRoleClick(UL_ID) {
        var url = "";        
        url = '<%=Url.Content("SetUserListRole?ul_id=")%>'+UL_ID+ '&unitid=<%=Request.QueryString["unit_id"] %>';
        OpenModelWindow(url, { width: 750, height: maiheight - 340, caption: "设置角色", onclose: function() { $("#UList").flexReload(); } });
    }

    //修改用户信息
    function SetUserModifyClick(UL_ID)
    {
        var url = "";
        url = '<%=Url.Content("~/AuthManage/AddModifyUser?UserID=")%>' + UL_ID;
        OpenModelWindow(url, { width: 650, height: maiheight - 90, caption: "修改用户信息", onclose: function() { $("#UList").flexReload(); } });
    }

    //重置密码
    function SetUserPassWord(UL_ID)
    {
        var url = "";
        url = '<%=Url.Content("~/AuthManage/ResetUserPassWord?UserID=")%>' + UL_ID;
        OpenModelWindow(url, { width: 650, height: maiheight-340, caption: "修改密码", onclose: function() { $("#UList").flexReload(); } });
    }

    //删除用户
    function DelClick(ul_id) {
        var rel=confirm("是否将用户删除?")
        if(rel==false)
        {
            return;
        }

         $.ajax({
            type: "post",
            dataType: "json",
            //contentType: "application/json; charset=utf-8",
            url: "<%=Url.Content("~/AuthManage/DelUser")%>",            
            data: {
                ul_id: function () {
                    return ul_id;
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
