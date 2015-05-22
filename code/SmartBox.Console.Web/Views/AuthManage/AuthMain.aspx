<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/List.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	主界面
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="HeaderContent" runat="server">
    
<link href="<%=Url.Content("~/Javascripts/ztrees/css/zTreeStyle/zTreeStyle.css")%>" rel="stylesheet" />
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.ifrmdailog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/ztrees/js/jquery.ztree.core-3.5.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/ztrees/js/jquery.ztree.excheck-3.5.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/ztrees/js/jquery.ztree.exedit-3.5.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/flexgrid_autosize.js")%>" type="text/javascript"></script>

    <style>
    div#tree .edit
    {
        display:none!important;
    }
    div#tree .remove
    {
        display:none!important;
    }
    
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="_container">
    <div id="top">        
    <%if (SmartBox.Console.Bo.BoFactory.GetSMC_UserListBo.HasFunction(User.Identity.Name, "unitManage")) {%>
        <div id="dvUnit">组织</div>
        <%} %>
        <%if (SmartBox.Console.Bo.BoFactory.GetSMC_UserListBo.HasFunction(User.Identity.Name, "roleManage")) {%>
        <div id="dvRole">角色</div>
        <%} %>
        <%if (SmartBox.Console.Bo.BoFactory.GetSMC_UserListBo.HasFunction(User.Identity.Name, "userManage")) {%>
        <div id="dvUser">用户</div>
        <%} %>
<%--        <%if (SmartBox.Console.Bo.BoFactory.GetSMC_UserListBo.HasFunction(User.Identity.Name, "addManager")) {%>
        <div id="dvManager">管理员</div>
        <%} %>
        <div id="dvFunction">权限</div>--%>
    </div>
    <div id="c">
        <div id="l">
            <div id="tree" class="ztree">
            </div>
        </div>
        <div id="r" >
        <iframe id="ifrm" name="ifrm" frameborder=0></iframe>
        </div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    var attr='<%=ViewData["url"]%>';

    var setting = {
            edit: {
		        enable: true
	        },
            check: {
                enable: false
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            view: {
                fontCss: getFont,
                dblClickExpand: false,
                showLine: true,
                selectedMulti: false 
            },
            callback: {
                beforeClick: function (treeId, treeNode) {
                    if(treeNode.target=='ifrm'){
                    $('#ifrm').attr('src', treeNode.url);}

                    if (treeNode.isParent) {
                        
                        //return false;
                    }
                },
                onClick: zTreeOnClick,
                onDrag: zTreeOnDrag,
                beforeDrag: zTreeBeforeDrag,
                onDrop: zTreeOnDrop                    
            }
        };
        
        function zTreeOnDrop(event, treeId, treeNodes, targetNode, moveType) {
            //ztree = $.fn.zTree.init($("#tree"), setting, nodes);  
            //var treeObj = $.fn.zTree.getZTreeObj("tree");
            //var nodes = treeObj.getNodes();
            //var nodeParentOld = treeObj.getNodeByTId(treeNodes[0].parentTId);
            //treeObj.moveNode(nodeParentOld, treeNodes[0], "inner");
            //return false;
            if (treeNodes.length == 1) {
                //alert(treeNodes[0].id + ':' + targetNode.id);
                $.ajax({
                    type: "post",
                    dataType: "json",
                    //contentType: "application/json; charset=utf-8",
                    url: '<%=Url.Content("~/authmanage/DragNodeAsChild")%>',
                  data: {
                      childNodeId: treeNodes[0].id,
                      parentNodeId: targetNode.id,
                      t: '<%=Request.QueryString["t"] %>'
                  },
                  success: function (json) {
                      if (json.r) {
                        alert(json.d);
                        ztree = $.fn.zTree.init($("#tree"), setting, json.nodeData);
                        //window.top.location = window.top.location + '?mod1=authManage&mod2=funcManage';
                        //$(window.top.document).find('a[code="authManage"]').click();//showTitle('authManage');
                        
                        //$(window.top.document).find('dt.funcManage').click();
                        setCookie("from","func",3);                       
                        
                        parent.location.reload();
                        
                      } else {
                        alert(json.d);
                        ztree = $.fn.zTree.init($("#tree"), setting, json.nodeData);
                        //var treeObj = $.fn.zTree.getZTreeObj("tree");
                        //var nodes = treeObj.getNodes();
                        //treeObj.moveNode(nodes[0], nodes[1], "inner");
                      }
                  }, failure: function (json) {
                      //showMessage(json.d, function () { window.close(); });
                  }, error: function (xhr, status) {
                      //showMessage("Sorry, there was a problem!", function () { window.close(); });
                  }
              });
            }
            //alert(treeNodes.length + "," + (targetNode ? (targetNode.tId + ", " + targetNode.name) : "isRoot" ));
        };
        function zTreeBeforeDrag(treeId, treeNodes) {
            //alert(treeId);
            //if (!confirm('确定移动模块吗？')) {
            //    return false;
            //}
            var t = '<%=Request.QueryString["t"] %>';
            if (t != 'function') {
                return false;
            } else
                return true;
        };
        function zTreeOnDrag(event, treeId, treeNodes) {
            //alert(treeNodes.length);
        };
        function zTreeOnClick(event, treeId, treeNode) {
            var s=treeNode.url;
            attr=treeNode.url.substr(s.indexOf('=')+1);  //
           
            if(treeNode.target=='_top')
            {
            OpenModelWindow(treeNode.url, { width: 750, height: 500, caption: "修改模块信息",onclose: function() { setCookie("from","func",3);parent.parent.location.reload()} });
            }
          };


         function refreshTree() {
            //var zTree = $.fn.zTree.getZTreeObj("tree");
            //zTree.refresh();
            location.reload();
         }


        function getFont(treeId, node) {
            return node.font ? node.font : {};
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
 

        nodes =<%=ViewData["nodesFunctions"]%>;

$(document).ready(function() {   
    
        var height = parent.$('div#center').css('height');
        
        var ztree = $.fn.zTree.init($("#tree"), setting, nodes);   
        
         
        $('#dvUnit').click(function() {
            location = '<%=Url.Content("~/AuthManage/AuthMain")%>?url='+attr;
        })
        $('#dvUnit').hover(function() {
                $(this).addClass('hover');
            },function() {
                $(this).removeClass('hover');
            });

        $('#dvRole').click(function() {
            location = '<%=Url.Content("~/AuthManage/AuthMain?t=role")%>&url='+attr;
        }).hover(function() {
                $(this).addClass('hover');
            },function() {
                $(this).removeClass('hover');
            });;
        $('#dvUser').click(function() {
            location = '<%=Url.Content("~/AuthManage/AuthMain?t=user")%>&url='+attr;
        }).hover(function() {
                $(this).addClass('hover');
            },function() {
                $(this).removeClass('hover');
            });

            $('#dvFunction').click(function() {
            location = '<%=Url.Content("~/AuthManage/AuthMain?t=function")%>';
        }).hover(function() {
                $(this).addClass('hover');
            },function() {
                $(this).removeClass('hover');
            });

        var t = '<%=Request.QueryString["t"] %>';
        switch (t) {
            case 'role':
                $('#dvRole').css('background-color','rgb(39, 121, 203)').css('color', '#ffffff');
                break;
            case 'user':
                $('#dvUser').css('background-color','rgb(39, 121, 203)').css('color', '#ffffff');
                break;
            case 'function':
                $('#dvFunction').css('background-color','rgb(39, 121, 203)').css('color', '#ffffff');
                break;
            case 'unit':
                $('#dvUnit').css('background-color','rgb(39, 121, 203)').css('color', '#ffffff');
                break;
            case '':
                $('#dvUnit').css('background-color','rgb(39, 121, 203)').css('color', '#ffffff');
                break;
        }
        //var zTree = $.fn.zTree.getZTreeObj("tree");
        //var nodes = zTree.getNodes();
        //nodes[0].click();
        $('div#tree li span#tree_1_span')[0].click();
        var s = '';
        switch(t) {
            case 'unit':
                //$('div#tree li span#tree_1_span').html('组织');
                s = '组织';
                break;
            case 'user':
                //$('div#tree li span#tree_1_span').html('用户');
                s = '用户';
                break;
            case 'role':
                //$('div#tree li span#tree_1_span').html('角色');
                s = '角色';
                break;
            case 'function':
                //$('div#tree li span#tree_1_span').html('模块');
                s = '模块';
                break;
        }
        document.title = s;
        
        <%--var defUrl = $('div#tree a').eq(0).attr("href");
        var urls = defUrl.substr(0,defUrl.indexOf('=')+1)+attr;
        $("div#tree a[href='"+urls+"']").eq(0).click();
        if(t!='function')
        {
        $("#ifrm").attr("src",urls);}
        else
        {
            $('div#tree a')[0].click();
        }--%>
        <%--$('div#tree a').each(function(){
              var urls =  $(this).attr("href");              
              if(urls.indexOf('unit_id')>=0)
              {
                if(attr == urls.substr(urls.indexOf('=')+1))
                    {                                            
                        $(this).click();
                        alert(urls);
                    }
              }else
              {
                $('div#tree a')[0].click();
              }
            });--%>


        resize();
        
        $(window).resize(function() {
            resize();
        });
    });

    function resize() {
        var clientHeight = document.documentElement.clientHeight ||document.body.clientHeight ||  0;
        clientHeight -= 45;
        $('div#c').css('height', clientHeight);

        var cWidth = $('#c').width();
        var clWidth = $('#c #l').width();
        $('#c #r').css('margin-left', clWidth + 1);
    }
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="StyleContent" runat="server">
.hover {
    cursor:pointer;
    background-color:#f0f0f0;
}
#c {
    width:99.8%;
    height:500px;
    border:1px solid #DFDFDF;
    background-color:#FBFBFC;
}

#c #l {
    width:200px;
    float:left;
    border-right:1px solid #DFDFDF;
    background-color: #fcfcfc;
    height:100%;
    overflow:scroll;
}

#c #r {
    
    height:100%;
    margin-left:220px;
}

iframe#ifrm {
    width:100%;
    height:100%;
}

#top {
    width:99.8%;
    height:40px;
    background-color:rgb(245, 245, 245);
    vertical-align:middle;
    line-height:40px;
    border-top:solid #dfdfdf 1px;
    border-left:solid #dfdfdf 1px;
    border-right:solid #dfdfdf 1px;
}


#dvUser,#dvRole,#dvUnit,#dvFunction,#dvManager {
float:right;
height:30px;
line-height:30px;
width:50px;
text-align:center;
margin-top:5px;
margin-right:5px;
border:1px solid #e0e0e0;
}
</asp:Content>
