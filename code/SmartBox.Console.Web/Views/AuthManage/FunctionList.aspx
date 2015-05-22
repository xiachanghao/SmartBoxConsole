<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/List.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	权限管理
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
        url: '<%=Url.Action("GetFunctionListByUpperFNID", new { Upper_FN_ID = Request.QueryString["Upper_FN_ID"] })%>',
        colModel: [
            { display: '编码', name: 'FN_ID',  width: 30, sortable: false, hide: true,align: 'center' },
            { display: '权限名称', name: 'FN_Name', width: 130, sortable: false, align: 'center' },
            { display: '权限Code', name: 'FN_Code', width: 130, sortable: false, align: 'center' },
            { display: '排序号', name: 'Unit_Sequence', width: 60, sortable: false, align: 'left', process: function (value, pid) {
                var op = new Array();               
                return "<input style='width:60%' type='text' value='" + value + "' id='"+ pid +"' />";
            }
            },
            { display: 'Url', name: 'FN_URL', width: 240, sortable: false, align: 'center' },
			{ display: '操作', width: 60, sortable: false, process: Aclick, align: 'center' }
		],

        sortname: "FN_Sequence",
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
                    data: { pe_ids: ids,pe_seqs:seqs ,type:'function'},
                    dataType: "json",
                    success: function (data) {
                        $("#loadingpannel").hide();
                        if (data.IsSuccess) {
                            hiAlert(data.Msg, "操作成功");
                            //$("#UList").flexReload(); 
                            setCookie("from","func",3);
                            document.cookie = "from=module;";
                            parent.parent.location.reload();
                        }
                        else {
                            hiAlert("操作失败，可能的原因:\r\n" + data.Msg, '提示');
                        }
                    }
                });
            });

    $("span.Add").click(function () {
                url = '<%=Url.Action("AddModifyFunction", new { Upper_FN_ID = Request.QueryString["Upper_FN_ID"] })%>';
                parent.OpenModelWindow(url, { width: 750, height: 430, caption: "新增模块", onclose: AfterModFunc });
            });
    });

    function AfterModFunc()
    {
        hiConfirm('是否立即刷新?', '确认框', function(r) {          
           if(r==true)
           {            
            setCookie("from","func",3);
            
            parent.parent.location.reload();
           }
        });

        
    }

    function Aclick(UL_ID, obj) {
        var text = "";
        text = "<a onclick='UpdateClick(\"" + UL_ID + "\",\"" + obj + "\")' style='cursor:pointer;color:blue' >修改</a>";
        text += "&nbsp;<a onclick='DelClick(\"" + UL_ID + "\",\"" + obj + "\")' style='cursor:pointer;color:blue' >删除</a>";
                
        return text;
    }

    //修改信息
    function UpdateClick(UL_ID, obj) {
        var url = "";
        url = '<%=Url.Content("/AuthManage/AddModifyFunction") %>?FN_ID=' + obj + '&Upper_FN_ID=<%=Request.QueryString["upper_fn_id"] %>'; //1表示新增
 
        OpenModelWindow(url, { width: 750, height: maiheight - 30, caption: "修改权限", onclose: AfterModFunc });
    } 

    //设置cookie    
    function setCookie(name, value, seconds) {               
        var expires = "";    
        if (seconds != 0 ) {      //设置cookie生存时间    
         var date = new Date();    
        date.setTime(date.getTime()+(seconds*1000));    
        expires = "; expires="+date.toGMTString();    
        }    
        document.cookie = name+"="+escape(value)+expires+"; path=/";   //转码并赋值    
    }   


    function DelClick(cfg_id, obj) {
        var rel=confirm("是否将权限删除?")
        if(rel==false)
        {
            return;
        }

        $.ajax({
            type: "post",
            dataType: "json",
            //contentType: "application/json; charset=utf-8",
            url: "<%=Url.Content("~/AuthManage/DelFunction")%>",
            //data: '{"userinfo":[{"name":"zs","age":"21"},{"name":"ls","age":"25"}]}',
            data: {
                fn_id: function () {
                    return obj;
                }
            },            
            success: function (json) {
                hiAlert(json.Msg, '提示', function () {
                    if (json.IsSuccess) {
                        AfterModFunc();
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
