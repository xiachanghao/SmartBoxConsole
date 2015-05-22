<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<AccordionItem>>" %>

<%@ Import Namespace="SmartBox.Console.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <link href="<%=Url.Content("~/Themes/home/home.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Javascripts/jquery.tooltip.css") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(window).resize(function () {
            setHomeIframePadding(null);
        });
        window.onload = function () {
            setHomeIframePadding(null);
        }
        function setHomeIframePadding(iframe) {
            var centerwidth = $('#center').width();
            var centerheight = $('#center').height();

            var winW, winH;
            if (window.innerWidth) { // all except IE    
                winW = window.innerWidth;
                winH = window.innerHeight;
            } else if (document.documentElement && document.documentElement.clientWidth) {    // IE 6 Strict Mode    
                winW = document.documentElement.clientWidth;
                winH = document.documentElement.clientHeight;
            } else if (document.body) { // other    
                winW = document.body.clientWidth;
                winH = document.body.clientHeight;
            }


            var width = winW - 170;
            var height = winH - 50;
            var isie = $.browser.msie;

            $('#ifm').width((width - 20) + 'px').css('margin-left', '10px').css('margin-right', '10px');
            $('#ifm').height((height - 8) + 'px').css('margin-top', '8px');
            return;



            var url = document.getElementById('ifm').contentWindow.document.URL.toLowerCase();

            var need = url.indexOf('syncbuausertoinside') != -1;
            var isNewModule = url.indexOf('2') != -1;

            if (need || isNewModule)
                $('iframe').css('padding', '0px').css('width', '100%');
            else
                $('iframe').css('padding-top', '0px');

            //            if (url.indexOf('2') != -1) {
            //                var height = $(document.getElementById('ifm').contentWindow).height();
            //                var het = height - 5;
            //                //alert(het);
            //                $('iframe').css('height', het);
            //            }
        }
    </script>
    <style>
        .menu3 
        {
            border-bottom:dashed #CDCDCD 1px;
            -ms-text-overflow:ellipsis;
            -o-text-overflow:ellipsis;
            -moz-text-overflow:ellipsis;
            -webkit-text-overflow:ellipsis;
            text-overflow:ellipsis;
            overflow:hidden;
            white-space:nowrap;
            margin-left:0px;
            background-color:#f0f0f0;
                }
                
                dt
                {
                    background-image:url(././Themes/home/images/arrow_icon.png);
                    background-position:95% 50%;     
                    background-repeat:no-repeat;                
                    }
                
                .menu3 div.row .l 
                {
                    <%--height:33px;--%>
                    line-height:33px;
                    vertical-align:middle;
                    float:left;                   
                   <%-- width:33px;--%>
                }
                .menu3 div.row .l img 
                {
                    margin-top:5px;
                    margin-left:0px;
                    margin-right:auto;
                }
                .menu3 div.row .r
                {
                    float:none;
                    line-height:33px;
                    height:33px;
                    vertical-align:middle;
                    padding-left:0px;
                    margin-left:25px;
                }
    </style>
</head>
<body>
    <div class="top">
        <div class="xx">
            <a href="javascript:toggleNav();" class="top_icon01" style="display:none"></a>
            <a target="ifm" class="top_icon02">
            </a>
            <div class="xx_text">
                <span class="text01">欢迎您<br />
                    <%=ViewData["CurrentUser"] %>
                </span><span class="text02">
                    <%--=ViewData["UserOrg"]--%>&nbsp;</span>
            </div>
            <div class="xx_menu" style="display:none;">
                <ul>
                    <li><a href='<%=Url.Action("Logout","Account") %>' >登出</a></li>
                </ul>
            </div>
        </div>
        <div class="nav navHeight">
            <ul id="menu">
               <%-- <li><a href="" class="select" onclick="showTitle('home')">首页</a></li>--%>
                <%
                    foreach (AccordionItem item in Model)
                    {
                        
                %>
                <%if (item.Code != "authManage")
                  { %>
                <li><a  code="<%=item.Code %>" target="ifm" onclick="showTitle('<%=item.Code %>')">
                    <%= item.Text%></a></li>
                <%}
                  else
                  { %>
                <li><a href="<%=item.Url %>" code="<%=item.Code %>" target="ifm" onclick="showTitle('<%=item.Code %>')">
                    <%= item.Text%></a></li>
                <%} %>
                <% } %>
            </ul>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="main">
        <div class="main_l">            
                <%
                    foreach (AccordionItem itemc in Model)
                    {                                           
                %>
                
                 <dl id="<%=itemc.Code %>">   
                 <%
                        foreach (var item in itemc.Children)
                        {  
                      %>

                <dt class='root <%=item.Code %>' >
                
                
                <a title="<%= item.Text%>" href="<%= item.Type=="Dir" ? "javascript:void(0)":(item.Children.Count==0 ? item.Url:item.Children[0].Url) %>"  style="background-image:url(../../Images/FuncImages/<%=item.ID %>_SIcon.png); background-position:12% 50%; background-size:16px 16px"
                    cod='<%=item.Code %>' target="ifm" onclick="showFrame('2')">
                    <div style=" text-align:left; margin-left:20px; font-size:13px; font-weight:bold;" >
                    <%= item.Text%></div>
                 </a></dt>
                    <% 
                  foreach (AccordionItem cItem in item.Children)
                  {                             
                    %>                  
                       <dd class="menu3" cod='<%=cItem.Code %>'>
                       <div class="row">
                       <div class="l"><%--<img onerror="this.style.display='none';" src="../../Images/FuncImages/<%=cItem.ID %>_SIcon.png" />--%></div>
                       <div class="r">
                    <a href="<%=cItem.Url %>" title="<%=cItem.Text%>" target='ifm' onclick="showFrame('3')" style="overflow:hidden;text-overflow:ellipsis;-o-text-overflow:ellipsis;white-space:nowrap;width:100%">
                        <%=cItem.Text%>
                    </a>  </div>
                        </div>
                    </dd>
                    <%
                  } %>
              
                    <% } %>
                 </dl>
                <%                   
                    }
                %>
            
        </div>
        <div class="main_r">
            <div id="center" style="">
               <%-- <div class="list_t" id="ifm_title">
                    <span>控制台</span> > 应用</div>--%>
                <iframe onload="javascript:setHomeIframePadding();" id="ifm" name="ifm" style="width: 100%; height: 100%; position: absolute;"
                    src="/Frame/Index" frameborder="0"></iframe>
            </div>
            <div id="Dscr" style="display: none">
                <div class="main_r_t">
                    首页</div>
                <div class="main_r_nr">
                    <div class="main_r_nr01">
                        <div class="text01">
                            <h2>
                                程序管理</h2>
                            <p>
                                设置将在称为"设置"窗格的光取消弹出窗口中显示。该窗格在用户点击"设置"超级按钮时显示，并在点击窗格外其他位置时消失。有关将"设置"超级按钮用于你的应用设置的某些优势在于，你无需在你的应用的
                                UI</p>
                        </div>
                    </div>
                    <div class="main_r_nr01 main_r_nr02">
                        <div class="text01">
                            <h2>
                                应用管理</h2>
                            <p>
                                启动iTunes，切换到"应用程序"界面。在应用程序界面的右下角，有一个"检查更新"按钮，点击检查更新。</p>
                        </div>
                    </div>
                </div>
                <div class="main_r_nr">
                    <div class="main_r_nr01 main_r_nr03">
                        <div class="text01">
                            <h2>
                                数据统计</h2>
                            <p>
                                设置将在称为"设置"窗格的光取消弹出窗口中显示。该窗格在用户点击"设置"超级按钮时显示，并在点击窗格外其他位置时消失。有关将"设置"超级按钮用于你的应用设置的某些优势在于，你无需在你的应用的
                                UI</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/common.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/home.js") %>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/jquery.tooltip.js") %>" type="text/javascript"></script>


    <script type="text/javascript">
        var class1 = '控制台';
        var class2 = '功能';

        
        function adjustMenu() {
            var bodyWidth = $(document.body).width();
            bodyWidth = bodyWidth - 364;           
            $('div.nav').css('width', bodyWidth + 'px').css('height', '50');
            $('div.nav ul').css('width', bodyWidth + 'px').css('height', '50');
            $('div.nav ul li').css('height', '50');
        }

        $(document).ready(function () {
            $(document.body).keyup(function (e) {
                if (e.keyCode == 27) {
                    if (confirm('确定要退出登录吗?')) {
                        $('div.xx_menu a')[0].click();
                    }
                }
            });
            var coke = document.cookie;   //取得cookie 

            var date = new Date();
            date.setTime(date.getTime() - 100);
            document.cookie = 'from' + "=a; expires=" + date.toGMTString(); //删除cookie

            ReSize();
            var mheight = $("#left").height();
            $.createMenuBar("left", mheight + 2);

            if (coke != 'from=func') {
                var dd = document.getElementsByTagName('dd')[0];
                $(".xx .xx_text").click(function () {
                    $(".xx .xx_menu").toggle("fast");
                });

                var e1 = document.getElementById('menu');
                e1.getElementsByTagName('a')[0].click();

                var hasSelect = false;
                $('.main_l dt,.main_l dd').hover(function () {
                    hasSelect = $(this).hasClass('select');

                    if (hasSelect)
                        $(this).removeClass('select');
                    $(this).addClass('hovr');
                    //$('.select').toggleClass('hovr');
                }, function () {
                    $(this).removeClass('hovr');
                    if (hasSelect) {
                        $(this).addClass('select');
                        hasSelect = false;
                    }
                });
                $('.main_l dt,.main_l dd').click(function () {
                    $('.main_l .select').removeClass('select');
                    $(this).addClass('select');
                });
            } else if (coke == 'from=func') {
                var e1 = document.getElementById('menu');
                e1.getElementsByTagName('a')[3].click();

                var e2 = document.getElementById('authManage');
                e2.getElementsByTagName('a')[2].click();
            }

            var a1 = document.getElementById('menu').getElementsByTagName('a')[0]; //首页  
            //var dd1 = document.getElementById('main_l').getElementsByTagName['a'][0];
            $(a1).tooltip({
                bodyHandler: function () {
                    return '统计';
                },
                showURL: false
            });

        });

        $(window).resize(ReSize);
        function ReSize() {
            var mheight = document.documentElement.clientHeight;
            var topheight = $("#top").height() + 2;
            var toolbarheight = $("#toolbar").height() + 2;
            var height = mheight - topheight - toolbarheight - 4;
            $("#left").height(height + 3);
            $("#center").height(height - 43);
            $(".main_l").css("height", mheight - 53);
           adjustMenu();
        }

        function showFrame(level) {
            
            class2 = window.event.srcElement.innerText;
            var dd = window.event.srcElement.parentNode.parentNode;
            class1 = dd.getElementsByTagName('a')[0].innerText;
            $(".main_l").find(".select").removeClass("select");
            $(".main_l").find(".hovr").removeClass("hovr");
            $(window.event.srcElement).parent("h1").addClass("select")
            $("#center").css("display", "block");
            $("#Dscr").css("display", "none");

            if (level == 3) {
                $(dd).addClass("select");
            }
            if (level == 2) {

                //点中二级菜单
                $("dd").css("display", "none");
                
                $(window.event.srcElement.parentNode).addClass("select");
                var tmpDD = $(window.event.srcElement.parentNode.parentNode).next();                
                if ($(window.event.srcElement.parentNode).attr("tagName") == "DT") {
                    tmpDD = $(window.event.srcElement.parentNode).next(); 
                 }         
                //循环查找dt下的所有dd，并且不查找下一个dt的内容
                while (tmpDD.attr("tagName") != null && tmpDD.attr("tagName") != "DT") {
                    $(tmpDD).css("display", "block");
                    tmpDD = tmpDD.next();
                }          

             }
                            
        }

        function showTitle(code) {
            var evt = window.event || arguments[0];
            var e = evt.srcElement;
            //下拉菜单处理
            $(".main_l").find(".select").removeClass("select");
            $(".nav .select").removeClass("select");
            $(e).addClass("select");
            $(".main_l a[cod]").parent().hide();
            $("dt").css("display", "none");
            $("dd").css("display", "none");


            $("dl#" + code + " " + "dt").css("display", "block");   
            //折叠            
            var dt = $("dl#" + code + " " + "dt:eq(0)");
            //找到dt下的dd               
            var tmpDD = dt.next();                     
            //循环查找dt下的所有dd，并且不查找下一个dt的内容
            while (tmpDD.attr("tagName") != null && tmpDD.attr("tagName") != "DT") {                
                $(tmpDD).css("display", "block");               
                tmpDD = tmpDD.next();              
            }          
                      
            var de = document.getElementById(code);
            var ae = de.getElementsByTagName('a')[0];
            if (ae != null) {
                ae.click();
            }
            

        }

        function toggleNav() {
            $("div.nav").toggleClass("navHeight");           
        }



    </script>
</body>
</html>
