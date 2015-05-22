<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SkinManage</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />

    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>

    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>

    <% if (false)
       {%>

    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <%} %>
    <style type="text/css">
        ul
        {
            list-style-type: decimal;
        }
        li
        {
            margin: 1px 0px 1px 1px;
            padding: 1px;
        }
        li:hover
        {
            background-color: #BCDDE2;
        }
        #SkinPath
        {
            width: 370px;
            height: 25px;
            margin: 0px;
            padding: 0px;
        }
        #UpSkinBtn
        {
            width: 70px;
            height: 25px;
            margin: 0px;
            padding: 0px;
        }
        .del
        {
            margin-left: 3px;
        }
    </style>

    <script type="text/javascript" language="javascript">
      var _isUpdateSkin = false;
      var _isDelSkin = false; 
      $(document).ready(function(){  
           var pubSuccess = QueryString("IsSuccess");           
           if(pubSuccess)   
           {
              showPrompt("发布成功");
              hidePrompt(2000);
           } 
          //Bind events
          $("#PubBtn").click(PublicSkin); 
          $("#UpSkinBtn").click(function(){
                $("#SkinForm").ajaxSubmit(option);
          });                             
          //Builder async update option
          var option = {
                beforeSubmit: function() {
                    if(_isUpdateSkin) 
                    {
                       hiAlert("正在上传数据...","提示");
                       return false;//正在上传中。。。
                    }
                    if(!vSkin()) return false;//验证
                    _isUpdateSkin =true;                    
                    $("#UpLoading").show();
                    return true;
                },
                dataType: "json",
                success: function(data) {
                    _isUpdateSkin=false;  
                    $("#UpLoading").hide();                   
                    if (data.IsSuccess) {
                        showPrompt("上传成功！");
                        hidePrompt(2000); 
                        var tempSkinName = data.Data; 
                        $("#UpdatedSkin").append('<li id="'+tempSkinName+'">'+tempSkinName+'<a id="'+tempSkinName+'_a" class=\"del\" title="删除"><img src="../../Images/icons/Item.Delete.gif" alt="删除" onclick="DelSkin('+'\''+tempSkinName+'\''+')" /></a></li>');                                                                                        
                    }
                    else {
                        hiAlert(data.Msg,"提示");                        
                    }
                }
            };//end of option
          
            //Validate update data
            function vSkin()
            {
                 var ok = true;
                 var p = $("#SkinPath").val();
                 if(p==""||p.length==0)
                 {
                   hiAlert("请选择上传皮肤包","提示");
                   ok = false;
                 }
                 else
                 {
                     var pt = p.lastIndexOf(".");
                     if(pt>0 && pt+4==p.length)
                     {
                         var type = p.substr(pt).toLowerCase();
                         if(type!=".zip")
                         {                            
                            hiAlert("上传文件必须是Zip压缩格式","提示");
                            ok = false;
                         }
                     }
                     else
                     {                        
                         hiAlert("上传文件必须是Zip压缩格式","提示");
                         ok = false;
                     }                
                 }
                 return ok;
            } //end of vSkin  
            function  QueryString(key){
                    var svalue = location.search.match(new RegExp("[\?\&]" + key + "=([^\&]*)(\&?)","i"));
                    return svalue ? svalue[1] : svalue;
            }                      
          });//end of ready
          function DelSkin(skinName)
          {
                $("#"+skinName+"_a").hide();                
                $("#"+skinName).append("<span class='uploading'><img src='/images/icons/indicator.gif' alt='' border='0' />正在删除...</span>");
                _isDelSkin = true;
                $.post('<%=Url.Action("DeleteSkin","SkinManage") %>',
                {SkinName:skinName},
                function(rst)
                {
                    _isDelSkin = false;
                    if(rst.IsSuccess)
                    {
                        $("#"+rst.Data).remove();
                        showPrompt(rst.Msg);
                        hidePrompt(2000);
                    }
                    else
                    {
                        $("#"+rst.Data).remove();
                        hiAlert(rst.Msg);                        
                    }
                },
                'json'
                );
            }// end of DelSkin
            
            //保存、发布
            function PublicSkin()
            {
                if(_isUpdateSkin||_isDelSkin)
                {
                   hiAlert("正在处理数据，请稍后...","提示");
                   return;
                }
                if($("#UpdatedSkin").children().size()==0)
                {
                   hiAlert("请先上传皮肤！","提示");
                   return;
                }                
                //画蒙层
                var t =document.body.style.marginTop=="" ? 0: parseInt(document.body.style.marginTop,10);
                var b = document.body.style.marginBottom=="" ? 0: parseInt(document.body.style.marginBottom,10);
                var overlayer = $('<div></div>').css({
                    position: 'absolute',
                    left: 0,
                    top: 0,
                    width: Math.max(document.documentElement.clientWidth, document.body.scrollWidth),
                    height: Math.max(document.documentElement.clientHeight, document.body.scrollHeight + t + b),
                    zIndex: '998',
                    "background-color": '#fff',  
                    "background-image":"url(../../Images/UploadFile/indicator_waitanim.gif)",
                    "background-position":"center", 
                    "background-repeat": "no-repeat",                           
                    opacity: '0.5'
                }).bind('contextmenu', function() { return false; }).appendTo(document.body);
                            
                _isSaving = true;    
                showPrompt("正在发布...");         
                $.post('<%=Url.Action("PublicSkin","SkinManage") %>',null,
                  function(data){
                      _isSaving = false;
                      overlayer.remove();
                      if(data.IsSuccess)
                      {
                        //alert("发布成功！");
                        var orientPath = window.location.href;
                        var pt = orientPath.lastIndexOf('?');
                        if(pt>0)
                           orientPath =orientPath.substr(0,pt);
                        window.location.href = orientPath+"?IsSuccess="+true;
                        //window.location.reload();
                      }
                      else
                      {
                          hiAlert(data.Msg);
                      }
                  },
                  "json"
                );               
            } //end of PublicSkin
    </script>

</head>
<body>
    <div id="MainBox" class="bbit-main">
        <div class="toolBotton">
            <a id="PubBtn" class="imgbtn" href="javascript:void(0);"><span class="Save" title="关闭">
                发布</span></a>
        </div>
        <div class="bbit-categorycontainer">
            <div>
                <%Html.RenderPartial("PublicSkin"); %>
            </div>
            <div style="border: solid 1px #ccc; color:White; background-color: Gray; font-family: 微软雅黑; font-size: 12pt;
                padding: 2px 0px 2px 5px;">
                <span>已上传皮肤:</span></div>
            <ul id="UpdatedSkin">
            </ul>
            <form id="SkinForm" method="post" action="<%=Url.Action("UpdateSkin","SkinManage") %>">
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="1" style="border-top: solid 1px #cccccc;">
                <tr class="even">
                    <td class="bbit-form-cell-name tdleft">
                        皮肤：
                    </td>
                    <td id="TopTd" class="bbit-form-cell-value tdright">
                        <input id="SkinPath" name="TopImagePath" type="file" />
                        <input id="UpSkinBtn" name="UpTopImage" type="button" value="上传" />
                        <span id="UpLoading" class='uploading' style='display: none;'>
                            <img src='/images/icons/indicator.gif' alt='' border='0' />
                            正在上传...</span>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
</body>
</html>
