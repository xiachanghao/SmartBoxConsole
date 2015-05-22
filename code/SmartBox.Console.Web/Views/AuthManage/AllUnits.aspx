<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Tree.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	管理员
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="tree" class="ztree">
</div>
<%--<div id="dvBtn">
<input type="submit" value="保存" id="btnSave" />
</div>--%>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

var setting = {
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
                    var zTree = $.fn.zTree.getZTreeObj("divtree");
                    if (treeNode.isParent) {
                        zTree.expandNode(treeNode);
                        return false;
                    }
                },

                onClick:zTreeOnClick                
            }
        };

        function zTreeOnClick(Unit_ID) {
            alert(Unit_ID);
         }


        function getFont(treeId, node) {
            return node.font ? node.font : {};
        }
 

		 var nodes = [
          {
            id: "aaa",
              name: "首页",
              open: false,
              children: [
                  {
                      name: "首页",
                      url: "/HomePage/AfterLogin2",
                      font: { 'color': 'blue' },
                      target: "ifcontent"
                  }
              ]
          },
          {
          id:"bbb",
              name: "公共信息",
              open: false,
              icon: "",
              children: [
                   { name: "公告发布", url: "/bulletin/bulletinadd", font: { 'color': 'blue' }, target: "ifcontent" }, 
                  { name: "公司介绍", url: "/company/companysview", font: { 'color': 'blue' }, target: "ifcontent" }
              ]
          },
          {
          id:"ccc",
              name: "供应商管理",
              open: false,
              children: [
               { name: "重置供应商密码", url: "/supplier/supplier_list", font: { 'color': 'blue' }, target: "ifcontent" },
               { name: "管理员密码修改", url: "/supplier/AdminPassword", font: { 'color': 'blue' }, target: "ifcontent" }
              ]
          }
         
        ];

        nodes =<%=ViewData["nodesFunctions"]%>;

$(document).ready(function() {    
    $.fn.zTree.init($("#tree"), setting, nodes);    
    });
        
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="StyleContent" runat="server">
#tree {
    border:#dfdfdf 1px solid;
    height:350px;
    overflow:scroll;
}

#dvBtn {
    margin-top:5px;
    text-align:center;
     border:#dfdfdf 1px solid;
}
</asp:Content>
