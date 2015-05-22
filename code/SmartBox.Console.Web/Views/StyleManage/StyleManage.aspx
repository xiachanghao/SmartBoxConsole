<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>StyleManage</title>
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
        #TopImagePath, #BottomImagePath,#SplashPath
        {
            width: 370px;
            height: 25px;
            margin: 0px;
            padding: 0px;
        }
        #UpTopImage, #UpBottomImage,#UpSplash
        {
            width: 70px;
            height: 25px;
            margin: 0px;
            padding: 0px;
        }
    </style>

    <script type="text/javascript">   
        $(document).ready(function() {
           var _isSaving = false;
           var _tIsUped = false; //顶部图片是否已上传
           var _bIsUped =false;//底部图片是否已上传
           var _sIsUped = false;//splash是否上传
           var _tIsUping = false;//顶部图片是否上传中
           var _bIsUping =false;//底部图片是否上传中
           var _sIsUping = false;//splash是否上传中
           //注册保存事件
           $("#SaveBtn").click(Save);          
           var tOption = {
                beforeSubmit: function() {
                    if(_tIsUping) 
                    {
                       hiAlert("正在上传数据...","提示");
                       return false;//正在上传中。。。
                    }
                    if(!TopValidate()) return false;//不是合法图片
                    _tIsUping =true;
                    $("#TopUpSuccess").hide();
                    $("#TopUpLoading").show();
                    return true;
                },
                dataType: "json",
                success: function(data) {
                    _tIsUping=false;  
                    $("#TopUpLoading").hide();                    
                    if (data.IsSuccess) {
                        $("#TopUpSuccess").show();  
                        _tIsUped =true;                                             
                    }
                    else {
                        showPrompt("上传失败，可能的原因:\r\n"+data.Msg);                        
                    }
                }
            };
             $("#UpTopImage").click(function(){
                $("#TopForm").ajaxSubmit(tOption);
             });
             
            var bOption = {
                beforeSubmit: function() {
                    if(_bIsUping)
                    { 
                       hiAlert("正在上传数据...","提示");
                       return false;//正在上传中。。。
                    }
                    if(!BotValidate()) return false;//不是合法图片
                    _bIsUping=true;
                    $("#BotUpSuccess").hide();
                    $("#BotUpLoading").show();
                    return true;
                },
                dataType: "json",
                success: function(data) {
                    _bIsUping=false; 
                    $("#BotUpLoading").hide();                  
                    if (data.IsSuccess) {                        
                         $("#BotUpSuccess").show();
                         _bIsUped =true;
                    }
                    else {
                        showPrompt("上传失败，可能的原因:\r\n"+data.Msg);     
                    }
                }
            };
          
            $("#UpBottomImage").click(function(){
              $("#BottomForm").ajaxSubmit(bOption);
            });
            //Splash Upload
            var sOption = {
                beforeSubmit: function() {
                    if(_sIsUping)
                    { 
                       hiAlert("正在上传数据...","提示");
                       return false;//正在上传中。。。
                    }
                    if(!SplashValidate()) return false;//不是合法zip文件
                    _sIsUping = true;
                    $("#SplashUpSuccess").hide();
                    $("#SplashUpLoading").show();
                    return true;
                },
                dataType: "json",
                success: function(data) {
                    _sIsUping = false;
                    $("#SplashUpLoading").hide();                  
                    if (data.IsSuccess) {                        
                         $("#SplashUpSuccess").show();
                         _bIsUped =true;
                    }
                    else {
                        showPrompt("上传失败，可能的原因:\r\n"+data.Msg);     
                    }
                }
            };          
            $("#UpSplash").click(function(){
              $("#SplashForm").ajaxSubmit(sOption);
            });
            
            
            var imageSuffix = [];
            //验证顶部图片上传
            function TopValidate()
            {
                var isValidate = true;
                var path =$("#TopImagePath").val();
                if(path=="") 
                {
                  hiAlert("请选择图片","提示");
                  return false;//没选择图片
                }
                isValidate = checkImage(path);
                return isValidate;
            }
            //验证底部图片上传
            function BotValidate()
            {
                var isValidate = true;
                var path =$("#BottomImagePath").val();
                if(path=="") 
                {
                    hiAlert("请选择图片","提示");
                    return false;//没选择图片
                }
                isValidate = checkImage(path);                
                return isValidate;
            }
            //check splash file
            function SplashValidate()
            {
                 var isValidate = true;
                var path =$("#SplashPath").val();
                if(path=="") 
                {
                    hiAlert("请选择启动画面","提示");
                    return false;//没选择启动画面
                }
                isValidate = checkZip(path);                
                return isValidate;
            }
            //保存、发布
            function Save()
            {
                if(_tIsUping||_bIsUping||_sIsUping)
                {
                   hiAlert("正在上传数据...","提示");
                   return;
                }
                if(!(_tIsUped||_bIsUped||_sIsUped))
                {
                   hiAlert("请先上传数据！","提示");
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
                $.post('<%=Url.Action("SaveStyleSetting","StyleManage") %>',
                  null,//{TopImageIsUsed:topUsed,BotImageIsUsed:botUsed},
                  function(data){
                      _isSaving = false;
                      overlayer.remove();
                      if(data.IsSuccess)
                      {
                        alert("发布成功！");
                        window.location.reload();
                      }
                      else
                      {
                        showPrompt("发布失败，可能原因："+data.Msg);
                      }
                  },
                  "json"
                );               
            } //end of save
            
            function checkImage(path){
      		  var point = path.lastIndexOf(".");          
      		  var type  = path.substr(point).toLowerCase();
      		  if(type==".jpg"||type==".jpeg"||type==".gif"||type==".png"){                                 
        	       return true;
     	          }else{
      			 alert("只能选择 jpg,jpeg,gif,png 格式的图片");
      			 return false;
     	          }
                  return true;
            } //end of checkImage     
            function checkZip(path)
            {
                 var point = path.lastIndexOf(".");          
      		     var type  = path.substr(point).toLowerCase();
      		     if(type!=".zip")
      		     {
      		        hiAlert("请选择Zip包！","提示");
      		        return false;  
      		     }
      		     else      		     
      		      return true;
            }  //end of checkZip                     
        }); // end of ready
    </script>

</head>
<body>
    <div id="MainBox" class="bbit-main">
        <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">页面图片管理</span>
            </div>
        </div>
        <div style="height:15px;"></div>
        <div class="toolBotton">
            <a id="SaveBtn" class="imgbtn" href="javascript:void(0);"><span class="Save" title="关闭">
                发布</span></a>
        </div>
        <div class="bbit-categorycontainer">
            <form id="TopForm" method="post" action="<%=Url.Action("UpLoadImage","StyleManage") %>">
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="1" style="border-top: solid 1px #cccccc;">
                <tr class="even">
                    <td class="bbit-form-cell-name tdleft">
                        顶部图片：
                    </td>
                    <td id="TopTd" class="bbit-form-cell-value tdright">
                        <input id="TopImagePath" name="TopImagePath" type="file" />
                        <input id="UpTopImage" name="UpTopImage" type="button" value="上传" />
                        <span id="TopUpLoading" class='uploading' style='display: none;'>
                            <img src='/images/icons/indicator.gif' alt='' border='0' />
                            正在上传...</span> <span id="TopUpSuccess" style="display: none">已上传</span>
                        <input name="WitchImage" type="hidden" value="Top" />
                    </td>                    
                </tr>
            </table>
            </form>
            <form id="BottomForm" method="post" action="<%=Url.Action("UploadImage","StyleManage") %>">
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="1">
                <tr class="odd">
                    <td class="bbit-form-cell-name tdleft">
                        底部图片：
                    </td>
                    <td id="BottomTd" class="bbit-form-cell-value tdright">
                        <input id="BottomImagePath" name="BottomImagePath" type="file" />
                        <input id="UpBottomImage" name="UpBottomImage" type="button" value="上传" />
                        <span id="BotUpLoading" class='uploading' style='display: none;'>
                            <img src='/images/icons/indicator.gif' alt='' border='0' />
                            正在上传...</span> <span id="BotUpSuccess" style="display: none">已上传</span>
                        <input name="WitchImage" type="hidden" value="Bottom" />
                    </td>                    
                </tr>
            </table>
            </form>
            <form id="SplashForm" method="post" action="<%=Url.Action("UploadSplash","StyleManage") %>">
            <table width="100%" class="bbit-form" cellspacing="0" cellpadding="1">
                <tr class="even">
                    <td class="bbit-form-cell-name tdleft">
                        启动窗体：
                    </td>
                    <td id="Td1" class="bbit-form-cell-value tdright">
                        <input id="SplashPath" name="SplashPath" type="file" />
                        <input id="UpSplash" name="UpSpash" type="button" value="上传" />
                        <span id="SplashUpLoading" class='uploading' style='display: none;'>
                            <img src='/images/icons/indicator.gif' alt='' border='0' />
                            正在上传...</span> <span id="SplashUpSuccess" style="display: none">已上传</span>                        
                    </td>                    
                </tr>
            </table>
            </form>            
        </div>
    </div>
</body>
</html>
