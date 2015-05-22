<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SmartBox.Console.Deploy._Default" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        上传发布文件
    </h2>
    <%--
    <asp:FileUpload ID="FileUpload" runat="server" />
    <asp:Button runat="server" Text="上传" onclick="Upload_Click"  />--%>
    <form id="form1" action="./Deploy.aspx" method="post">
    
    <input id="Submit1" type="submit" value="submit" />
    </form>

   
</asp:Content>
