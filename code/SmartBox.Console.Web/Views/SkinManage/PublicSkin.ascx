<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div style="border:solid 1px #ccc;background-color:Gray;color:White; font-family:微软雅黑; font-size:12pt; padding:2px 0px 2px 5px;"><span>已发布皮肤:</span></div>
<% 
    var smartBox = SmartBox.Console.Bo.BoFactory.GetVersionTrackBo.GetCurrentSmartBoxInfo();
    //SmartBox根路径
    string smartBoxRootPath = smartBox.FilePath;// @"D:\WorkPlace\SmartBox2\SmartBox.Console\SmartBox.Console\SmartBox.Console.Web\MainSystem\SmartBox\SmartBox_1";     
    //1、皮肤根路径
    string skinRoot = System.IO.Path.Combine(smartBoxRootPath, @"res\themes");//目的文件夹路径 
    string[] allSkinPaths = new string[] { };
    try
    {
        allSkinPaths = System.IO.Directory.GetDirectories(skinRoot);
    }
    catch { }
%>
<ul id="PublicedSkin">
<%foreach (var p in allSkinPaths)
{%>
  <li><%=System.IO.Path.GetFileName(p) %></li>
<%} %>
</ul>