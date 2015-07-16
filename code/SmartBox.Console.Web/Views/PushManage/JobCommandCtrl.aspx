<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>JobCommandCtrl</title>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content("~/Themes/Default/alert.css") %>" rel="stylesheet" type="text/css" />
    
    <script src="<%=Url.Content("~/Javascripts/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.alert.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Javascripts/Plugins/jquery.hotkeys.js")%>" type="text/javascript"></script>
    <link href="../../Cron/easyui.css" rel="stylesheet" type="text/css"/>
    <%--<link href="../../jquery-easyui-1.3.6/themes/bootstrap/easyui.css" rel="stylesheet"
        type="text/css" />--%>
    <link href="../../Cron/icon.css" rel="stylesheet" type="text/css"/>
    <link href="../../Cron/icon(1).css" rel="stylesheet" type="text/css"/>
    <script src="../../Cron/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../Cron/cron.js" type="text/javascript"></script>
    <% if (false)
       {%>
    <script src="../../Javascripts/intellisense/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <%} %>
    <style type="text/css">
        .bbit-categorycontainer 
        {
            padding-left:0px;
        }
        #TopImagePath, #BottomImagePath, #SplashPath
        {
            width: 370px;
            height: 25px;
            margin: 0px;
            padding: 0px;
        }
        #UpTopImage, #UpBottomImage, #UpSplash
        {
            width: 70px;
            height: 25px;
            margin: 0px;
            padding: 0px;
        }
        .line,.imp
        {
            height:30px;
            line-height:30px;
        }
    </style>
    <script type="text/javascript">
        var args = '';
        function changeCrl(value) {
            if (value == 'SetTargetJobTime') {
                $("#JobInfo").css('display', 'none'); btnFan();
                $("#TriggerInfo").css('display', 'block');

            } else
                if (value == 'PauseTargetJob' || value == 'ContinueTargetJob') {
                    $("#TriggerInfo").css('display', 'none');
                    $("#JobInfo").css('display', 'block');
                } else {
                    $("#TriggerInfo").css('display', 'none');
                }
         }

         $(document).ready(function () {
             $("#TriggerInfo").css('display', 'none');
             $("span.Approve").click(function () {
                 var selectStatus = encodeURI($("#selectStatus").val());
                 var fileName = encodeURI($("#ConfigFile").val());
                 if (selectStatus == 'SetTargetJobTime') {
                     args = '&' + $("#triggeName").val() + '&' + $("#cron").val();
                 }
                 if (selectStatus == 'PauseTargetJob' || selectStatus == 'ContinueTargetJob') {
                     args = '&' + $("#jobname").val();
                 }
                 $("#loadingpannel").html("正在执行......").show();
                 $.ajax({
                     type: "POST",
                     url: '<%=Url.Action("JobCommandCtrl") %>',
                     data: { JosType: selectStatus, ComdArgs: args , pd_id: <%=Request.QueryString["pd_id"] %>},
                     dataType: "json",
                     success: function (data) {
                         $("#loadingpannel").hide();
                         if (data.IsSuccess) {
                             hiAlert(data.Msg, "true");
                             //flushGrid();                          
                         }
                         else {
                             hiAlert("操作失败，可能的原因:" + data.Msg, '提示');
                         }
                     }
                 });
             });
             var op = '<%=Request.QueryString["operate"] %>';
             $('#selectStatus option[value="' + op + '"]').attr('selected', true);
             $('#selectStatus')[0].disabled = true;
             changeCrl(op);
         });             // end of ready
    </script>
</head>
<body>

    <div id="MainBox" class="bbit-main">   
    <div class="cHead">
            <div class="ftitle">
                <span id="departmentName">插运行控制</span>
            </div>
        </div>      
        <% using (Html.BeginForm("JobCommandCtrl", "PushManage", FormMethod.Post, new { id = "TopForm" }))
           {
               Html.AntiForgeryToken();
               Html.ValidationSummary(true); %>
        <div class="bbit-categorycontainer">
            <div id="caltoolbar" class="ctoolbar" style="height:80%">
                <div id="btnQuery" class="fbutton">
                    <div>
                        <span title='执行' class="Approve">执行</span></div>
                </div>
                <div style="padding-top:8px;width:100%;">
                &nbsp;&nbsp;&nbsp 任务类型:
                <select name="selectStatus" id="selectStatus" onchange="changeCrl(value)">
                    <option value="">请选择</option>
                    <option value="AddJobPluginGroup">新增任务插件</option>
                    <option value="RemoveJobPluginGroup">删除任务插件</option>
                    <option value="RestartJobPluginGroup">重新加载任务</option>
                    <option value="SetTargetJobTime">修改任务运行时间</option>
                    <option value="PauseTargetJob">暂停任务</option>
                    <option value="ContinueTargetJob">继续任务</option>
                </select>
                </div>
                <div id="JobInfo" style="display:none">
                <div style="padding-top:8px">
                &nbsp;&nbsp;&nbsp 任务名:
                <input type="text" id="jobname"  value="" style="width: 120px" />
                </div>
                </div>
                <div id="TriggerInfo" style=" display:block;padding-top:8px">
                &nbsp;&nbsp;&nbsp 触发器名:
                <input type="text" id="triggeName"  value="" style="width: 120px" />
                
                 <div class="easyui-layout" style="border-radius: 5px; border: 1px solid rgb(202, 196, 196);
            border-image: none; width: 100%; height: 450px;">
            <div style="height: 90%;">
                <div class="easyui-tabs" data-options="fit:true,border:false">
                    <div title="秒">
                        <div class="line">
                            <input name="second" onclick="everyTime(this)" type="radio">
                            每秒 允许的通配符[, - * /]</div>
                        <div class="line">
                            <input name="second" onclick="cycle(this)" type="radio">
                            周期从
                            <input class="numberspinner" id="secondStart_0" style="width: 60px;" value="1" data-options="min:1,max:58">
                            -
                            <input class="numberspinner" id="secondEnd_0" style="width: 60px;" value="2" data-options="min:2,max:59">
                            秒</div>
                        <div class="line">
                            <input name="second" onclick="startOn(this)" type="radio">
                            从
                            <input class="numberspinner" id="secondStart_1" style="width: 60px;" value="0" data-options="min:0,max:59">
                            秒开始,每
                            <input class="numberspinner" id="secondEnd_1" style="width: 60px;" value="1" data-options="min:1,max:59">
                            秒执行一次</div>
                        <div class="line">
                            <input name="second" id="sencond_appoint" type="radio">
                            指定</div>
                        <div class="imp secondList">
                            <input type="checkbox" value="1">01
                            <input type="checkbox" value="2">02
                            <input type="checkbox" value="3">03
                            <input type="checkbox" value="4">04
                            <input type="checkbox" value="5">05
                            <input type="checkbox" value="6">06
                            <input type="checkbox" value="7">07
                            <input type="checkbox" value="8">08
                            <input type="checkbox" value="9">09
                            <input type="checkbox" value="10">10</div>
                        <div class="imp secondList">
                            <input type="checkbox" value="11">11
                            <input type="checkbox" value="12">12
                            <input type="checkbox" value="13">13
                            <input type="checkbox" value="14">14
                            <input type="checkbox" value="15">15
                            <input type="checkbox" value="16">16
                            <input type="checkbox" value="17">17
                            <input type="checkbox" value="18">18
                            <input type="checkbox" value="19">19
                            <input type="checkbox" value="20">20</div>
                        <div class="imp secondList">
                            <input type="checkbox" value="21">21
                            <input type="checkbox" value="22">22
                            <input type="checkbox" value="23">23
                            <input type="checkbox" value="24">24
                            <input type="checkbox" value="25">25
                            <input type="checkbox" value="26">26
                            <input type="checkbox" value="27">27
                            <input type="checkbox" value="28">28
                            <input type="checkbox" value="29">29
                            <input type="checkbox" value="30">30</div>
                        <div class="imp secondList">
                            <input type="checkbox" value="31">31
                            <input type="checkbox" value="32">32
                            <input type="checkbox" value="33">33
                            <input type="checkbox" value="34">34
                            <input type="checkbox" value="35">35
                            <input type="checkbox" value="36">36
                            <input type="checkbox" value="37">37
                            <input type="checkbox" value="38">38
                            <input type="checkbox" value="39">39
                            <input type="checkbox" value="40">40</div>
                        <div class="imp secondList">
                            <input type="checkbox" value="41">41
                            <input type="checkbox" value="42">42
                            <input type="checkbox" value="43">43
                            <input type="checkbox" value="44">44
                            <input type="checkbox" value="45">45
                            <input type="checkbox" value="46">46
                            <input type="checkbox" value="47">47
                            <input type="checkbox" value="48">48
                            <input type="checkbox" value="49">49
                            <input type="checkbox" value="50">50</div>
                        <div class="imp secondList">
                            <input type="checkbox" value="51">51
                            <input type="checkbox" value="52">52
                            <input type="checkbox" value="53">53
                            <input type="checkbox" value="54">54
                            <input type="checkbox" value="55">55
                            <input type="checkbox" value="56">56
                            <input type="checkbox" value="57">57
                            <input type="checkbox" value="58">58
                            <input type="checkbox" value="59">59
                        </div>
                    </div>
                    <div title="分钟">
                        <div class="line">
                            <input name="min" onclick="everyTime(this)" type="radio">
                            分钟 允许的通配符[, - * /]</div>
                        <div class="line">
                            <input name="min" onclick="cycle(this)" type="radio">
                            周期从
                            <input class="numberspinner" id="minStart_0" style="width: 60px;" value="1" data-options="min:1,max:58">
                            -
                            <input class="numberspinner" id="minEnd_0" style="width: 60px;" value="2" data-options="min:2,max:59">
                            分钟</div>
                        <div class="line">
                            <input name="min" onclick="startOn(this)" type="radio">
                            从
                            <input class="numberspinner" id="minStart_1" style="width: 60px;" value="0" data-options="min:0,max:59">
                            分钟开始,每
                            <input class="numberspinner" id="minEnd_1" style="width: 60px;" value="1" data-options="min:1,max:59">
                            分钟执行一次</div>
                        <div class="line">
                            <input name="min" id="min_appoint" type="radio">
                            指定</div>
                        <div class="imp minList">
                            <input type="checkbox" value="1">01
                            <input type="checkbox" value="2">02
                            <input type="checkbox" value="3">03
                            <input type="checkbox" value="4">04
                            <input type="checkbox" value="5">05
                            <input type="checkbox" value="6">06
                            <input type="checkbox" value="7">07
                            <input type="checkbox" value="8">08
                            <input type="checkbox" value="9">09
                            <input type="checkbox" value="10">10</div>
                        <div class="imp minList">
                            <input type="checkbox" value="11">11
                            <input type="checkbox" value="12">12
                            <input type="checkbox" value="13">13
                            <input type="checkbox" value="14">14
                            <input type="checkbox" value="15">15
                            <input type="checkbox" value="16">16
                            <input type="checkbox" value="17">17
                            <input type="checkbox" value="18">18
                            <input type="checkbox" value="19">19
                            <input type="checkbox" value="20">20</div>
                        <div class="imp minList">
                            <input type="checkbox" value="21">21
                            <input type="checkbox" value="22">22
                            <input type="checkbox" value="23">23
                            <input type="checkbox" value="24">24
                            <input type="checkbox" value="25">25
                            <input type="checkbox" value="26">26
                            <input type="checkbox" value="27">27
                            <input type="checkbox" value="28">28
                            <input type="checkbox" value="29">29
                            <input type="checkbox" value="30">30</div>
                        <div class="imp minList">
                            <input type="checkbox" value="31">31
                            <input type="checkbox" value="32">32
                            <input type="checkbox" value="33">33
                            <input type="checkbox" value="34">34
                            <input type="checkbox" value="35">35
                            <input type="checkbox" value="36">36
                            <input type="checkbox" value="37">37
                            <input type="checkbox" value="38">38
                            <input type="checkbox" value="39">39
                            <input type="checkbox" value="40">40</div>
                        <div class="imp minList">
                            <input type="checkbox" value="41">41
                            <input type="checkbox" value="42">42
                            <input type="checkbox" value="43">43
                            <input type="checkbox" value="44">44
                            <input type="checkbox" value="45">45
                            <input type="checkbox" value="46">46
                            <input type="checkbox" value="47">47
                            <input type="checkbox" value="48">48
                            <input type="checkbox" value="49">49
                            <input type="checkbox" value="50">50</div>
                        <div class="imp minList">
                            <input type="checkbox" value="51">51
                            <input type="checkbox" value="52">52
                            <input type="checkbox" value="53">53
                            <input type="checkbox" value="54">54
                            <input type="checkbox" value="55">55
                            <input type="checkbox" value="56">56
                            <input type="checkbox" value="57">57
                            <input type="checkbox" value="58">58
                            <input type="checkbox" value="59">59
                        </div>
                    </div>
                    <div title="小时">
                        <div class="line">
                            <input name="hour" onclick="everyTime(this)" type="radio">
                            小时 允许的通配符[, - * /]</div>
                        <div class="line">
                            <input name="hour" onclick="cycle(this)" type="radio">
                            周期从
                            <input class="numberspinner" id="hourStart_0" style="width: 60px;" value="0" data-options="min:0,max:23">
                            -
                            <input class="numberspinner" id="hourEnd_1" style="width: 60px;" value="2" data-options="min:2,max:23">
                            小时</div>
                        <div class="line">
                            <input name="hour" onclick="startOn(this)" type="radio">
                            从
                            <input class="numberspinner" id="hourStart_1" style="width: 60px;" value="0" data-options="min:0,max:23">
                            小时开始,每
                            <input class="numberspinner" id="hourEnd_1" style="width: 60px;" value="1" data-options="min:1,max:23">
                            小时执行一次</div>
                        <div class="line">
                            <input name="hour" id="hour_appoint" type="radio">
                            指定</div>
                        <div class="imp hourList">
                            AM:
                            <input type="checkbox" value="0">00
                            <input type="checkbox" value="1">01
                            <input type="checkbox" value="2">02
                            <input type="checkbox" value="3">03
                            <input type="checkbox" value="4">04
                            <input type="checkbox" value="5">05
                            <input type="checkbox" value="6">06
                            <input type="checkbox" value="7">07
                            <input type="checkbox" value="8">08
                            <input type="checkbox" value="9">09
                            <input type="checkbox" value="10">10
                            <input type="checkbox" value="11">11
                        </div>
                        <div class="imp hourList">
                            PM:
                            <input type="checkbox" value="12">12
                            <input type="checkbox" value="13">13
                            <input type="checkbox" value="14">14
                            <input type="checkbox" value="15">15
                            <input type="checkbox" value="16">16
                            <input type="checkbox" value="17">17
                            <input type="checkbox" value="18">18
                            <input type="checkbox" value="19">19
                            <input type="checkbox" value="20">20
                            <input type="checkbox" value="21">21
                            <input type="checkbox" value="22">22
                            <input type="checkbox" value="23">23
                        </div>
                    </div>
                    <div title="日">
                        <div class="line">
                            <input name="day" onclick="everyTime(this)" type="radio">
                            日 允许的通配符[, - * / L W]</div>
                        <div class="line">
                            <input name="day" onclick="unAppoint(this)" type="radio">
                            不指定</div>
                        <div class="line">
                            <input name="day" onclick="cycle(this)" type="radio">
                            周期从
                            <input class="numberspinner" id="dayStart_0" style="width: 60px;" value="1" data-options="min:1,max:31">
                            -
                            <input class="numberspinner" id="dayEnd_0" style="width: 60px;" value="2" data-options="min:2,max:31">
                            日</div>
                        <div class="line">
                            <input name="day" onclick="startOn(this)" type="radio">
                            从
                            <input class="numberspinner" id="dayStart_1" style="width: 60px;" value="1" data-options="min:1,max:31">
                            日开始,每
                            <input class="numberspinner" id="dayEnd_1" style="width: 60px;" value="1" data-options="min:1,max:31">
                            天执行一次</div>
                        <div class="line">
                            <input name="day" onclick="workDay(this)" type="radio">
                            每月
                            <input class="numberspinner" id="dayStart_2" style="width: 60px;" value="1" data-options="min:1,max:31">
                            号最近的那个工作日</div>
                        <div class="line">
                            <input name="day" onclick="lastDay(this)" type="radio">
                            本月最后一天</div>
                        <div class="line">
                            <input name="day" id="day_appoint" type="radio">
                            指定</div>
                        <div class="imp dayList">
                            <input type="checkbox" value="1">1
                            <input type="checkbox" value="2">2
                            <input type="checkbox" value="3">3
                            <input type="checkbox" value="4">4
                            <input type="checkbox" value="5">5
                            <input type="checkbox" value="6">6
                            <input type="checkbox" value="7">7
                            <input type="checkbox" value="8">8
                            <input type="checkbox" value="9">9
                            <input type="checkbox" value="10">10
                            <input type="checkbox" value="11">11
                            <input type="checkbox" value="12">12
                            <input type="checkbox" value="13">13
                            <input type="checkbox" value="14">14
                            <input type="checkbox" value="15">15
                            <input type="checkbox" value="16">16
                        </div>
                        <div class="imp dayList">
                            <input type="checkbox" value="17">17
                            <input type="checkbox" value="18">18
                            <input type="checkbox" value="19">19
                            <input type="checkbox" value="20">20
                            <input type="checkbox" value="21">21
                            <input type="checkbox" value="22">22
                            <input type="checkbox" value="23">23
                            <input type="checkbox" value="24">24
                            <input type="checkbox" value="25">25
                            <input type="checkbox" value="26">26
                            <input type="checkbox" value="27">27
                            <input type="checkbox" value="28">28
                            <input type="checkbox" value="29">29
                            <input type="checkbox" value="30">30
                            <input type="checkbox" value="31">31
                        </div>
                    </div>
                    <div title="月">
                        <div class="line">
                            <input name="mouth" onclick="everyTime(this)" type="radio">
                            月 允许的通配符[, - * /]</div>
                        <div class="line">
                            <input name="mouth" onclick="unAppoint(this)" type="radio">
                            不指定</div>
                        <div class="line">
                            <input name="mouth" onclick="cycle(this)" type="radio">
                            周期从
                            <input class="numberspinner" id="mouthStart_0" style="width: 60px;" value="1" data-options="min:1,max:12">
                            -
                            <input class="numberspinner" id="mouthEnd_0" style="width: 60px;" value="2" data-options="min:2,max:12">
                            月</div>
                        <div class="line">
                            <input name="mouth" onclick="startOn(this)" type="radio">
                            从
                            <input class="numberspinner" id="mouthStart_1" style="width: 60px;" value="1" data-options="min:1,max:12">
                            日开始,每
                            <input class="numberspinner" id="mouthEnd_1" style="width: 60px;" value="1" data-options="min:1,max:12">
                            月执行一次</div>
                        <div class="line">
                            <input name="mouth" id="mouth_appoint" type="radio">
                            指定</div>
                        <div class="imp mouthList">
                            <input type="checkbox" value="1">1
                            <input type="checkbox" value="2">2
                            <input type="checkbox" value="3">3
                            <input type="checkbox" value="4">4
                            <input type="checkbox" value="5">5
                            <input type="checkbox" value="6">6
                            <input type="checkbox" value="7">7
                            <input type="checkbox" value="8">8
                            <input type="checkbox" value="9">9
                            <input type="checkbox" value="10">10
                            <input type="checkbox" value="11">11
                            <input type="checkbox" value="12">12
                        </div>
                    </div>
                    <div title="周">
                        <div class="line">
                            <input name="week" onclick="everyTime(this)" type="radio">
                            周 允许的通配符[, - * / L #]</div>
                        <div class="line">
                            <input name="week" onclick="unAppoint(this)" type="radio">
                            不指定</div>
                        <div class="line">
                            <input name="week" onclick="startOn(this)" type="radio">
                            周期 从星期<input class="numberspinner" id="weekStart_0" style="width: 60px;" value="1"
                                data-options="min:1,max:7">
                            -
                            <input class="numberspinner" id="weekEnd_0" style="width: 60px;" value="2" data-options="min:2,max:7"></div>
                        <div class="line">
                            <input name="week" onclick="weekOfDay(this)" type="radio">
                            第<input class="numberspinner" id="weekStart_1" style="width: 60px;" value="1" data-options="min:1,max:4">
                            周 的星期<input class="numberspinner" id="weekEnd_1" style="width: 60px;" value="1" data-options="min:1,max:7"></div>
                        <div class="line">
                            <input name="week" onclick="lastWeek(this)" type="radio">
                            本月最后一个星期<input class="numberspinner" id="weekStart_2" style="width: 60px;" value="1"
                                data-options="min:1,max:7"></div>
                        <div class="line">
                            <input name="week" id="week_appoint" type="radio">
                            指定</div>
                        <div class="imp weekList">
                            <input type="checkbox" value="1">1
                            <input type="checkbox" value="2">2
                            <input type="checkbox" value="3">3
                            <input type="checkbox" value="4">4
                            <input type="checkbox" value="5">5
                            <input type="checkbox" value="6">6
                            <input type="checkbox" value="7">7
                        </div>
                    </div>
                    <div title="年">
                        <div class="line">
                            <input name="year" onclick="unAppoint(this)" type="radio">
                            不指定 允许的通配符[, - * /] 非必填</div>
                        <div class="line">
                            <input name="year" onclick="everyTime(this)" type="radio">
                            每年</div>
                        <div class="line">
                            <input name="year" onclick="cycle(this)" type="radio">周期 从
                            <input class="numberspinner" id="yearStart_0" style="width: 90px;" value="2013" data-options="min:2013,max:3000">
                            -
                            <input class="numberspinner" id="yearEnd_0" style="width: 90px;" value="2014" data-options="min:2014,max:3000"></div>
                    </div>
                </div>
            </div>
            <div style="height: 200px;" data-options="region:'south',border:false">
                <fieldset style="border-radius: 3px; height: 116px;">
                    <legend>表达式</legend>
                    <table style="height: 100px;">
                        <tbody>
                            <tr>
                                <td >
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="center">
                                    秒
                                </td>
                                <td align="center">
                                    分钟
                                </td>
                                <td align="center">
                                    小时
                                </td>
                                <td align="center">
                                    日
                                </td>
                                <td align="center">
                                    月
                                </td>
                                <td align="center">
                                    星期
                                </td>
                                <td align="center">
                                    年
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    表达式字段:
                                </td>
                                <td >
                                    <input name="v_second" class="col" type="text" readonly="readonly" value="*" style="width:60px"/>
                                </td>
                                <td >
                                    <input name="v_min" class="col" type="text" readonly="readonly" value="*" style="width:60px"/>
                                </td>
                                <td >
                                    <input name="v_hour" class="col" type="text" readonly="readonly" value="*" style="width:60px"/>
                                </td>
                                <td >
                                    <input name="v_day" class="col" type="text" readonly="readonly" value="*" style="width:60px"/>
                                </td>
                                <td >
                                    <input name="v_mouth" class="col" type="text" readonly="readonly" value="*" style="width:60px"/>
                                </td>
                                <td >
                                    <input name="v_week" class="col" type="text" readonly="readonly" value="?" style="width:60px"/>
                                </td>
                                <td >
                                    <input name="v_year" class="col" type="text" readonly="readonly" style="width:60px"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:80px">
                                    Cron 表达式:
                                </td>
                                <td  colspan="3">
                                    <input name="cron" id="cron" style="width: 100%;" type="text" value="* * * * * ?"/>
                                </td>
                                <%--<td>
                                    <input id="btnFan" onclick="btnFan()" type="button" value="反解析到UI ">
                                </td>--%>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <div style="text-align: center; margin-top: 5px;">
                    <script type="text/javascript">
                        /*killIe*/
                        $.parser.parse($("body"));
                        var cpro_id = "u1331261";

                        function btnFan() {
                            //获取参数中表达式的值
                            var txt = $("#cron").val();
                            if (txt) {
                                var regs = txt.split(' ');
                                $("input[name=v_second]").val(regs[0]);
                                $("input[name=v_min]").val(regs[1]);
                                $("input[name=v_hour]").val(regs[2]);
                                $("input[name=v_day]").val(regs[3]);
                                $("input[name=v_mouth]").val(regs[4]);
                                $("input[name=v_week]").val(regs[5]);

                                initObj(regs[0], "second");
                                initObj(regs[1], "min");
                                initObj(regs[2], "hour");
                                initDay(regs[3]);
                                initMonth(regs[4]);
                                initWeek(regs[5]);

                                if (regs.length > 6) {
                                    $("input[name=v_year]").val(regs[6]);
                                    initYear(regs[6]);
                                }
                            }
                        }


                        function initObj(strVal, strid) {
                            var ary = null;
                            var objRadio = $("input[name='" + strid + "'");
                            if (strVal == "*") {
                                objRadio.eq(0).attr("checked", "checked");
                            } else if (strVal.split('-').length > 1) {
                                ary = strVal.split('-');
                                objRadio.eq(1).attr("checked", "checked");
                                $("#" + strid + "Start_0").numberspinner('setValue', ary[0]);
                                $("#" + strid + "End_0").numberspinner('setValue', ary[1]);
                            } else if (strVal.split('/').length > 1) {
                                ary = strVal.split('/');
                                objRadio.eq(2).attr("checked", "checked");
                                $("#" + strid + "Start_1").numberspinner('setValue', ary[0]);
                                $("#" + strid + "End_1").numberspinner('setValue', ary[1]);
                            } else {
                                objRadio.eq(3).attr("checked", "checked");
                                if (strVal != "?") {
                                    ary = strVal.split(",");
                                    for (var i = 0; i < ary.length; i++) {
                                        $("." + strid + "List input[value='" + ary[i] + "']").attr("checked", "checked");
                                    }
                                }
                            }
                        }

                        function initDay(strVal) {
                            var ary = null;
                            var objRadio = $("input[name='day'");
                            if (strVal == "*") {
                                objRadio.eq(0).attr("checked", "checked");
                            } else if (strVal == "?") {
                                objRadio.eq(1).attr("checked", "checked");
                            } else if (strVal.split('-').length > 1) {
                                ary = strVal.split('-');
                                objRadio.eq(2).attr("checked", "checked");
                                $("#dayStart_0").numberspinner('setValue', ary[0]);
                                $("#dayEnd_0").numberspinner('setValue', ary[1]);
                            } else if (strVal.split('/').length > 1) {
                                ary = strVal.split('/');
                                objRadio.eq(3).attr("checked", "checked");
                                $("#dayStart_1").numberspinner('setValue', ary[0]);
                                $("#dayEnd_1").numberspinner('setValue', ary[1]);
                            } else if (strVal.split('W').length > 1) {
                                ary = strVal.split('W');
                                objRadio.eq(4).attr("checked", "checked");
                                $("#dayStart_2").numberspinner('setValue', ary[0]);
                            } else if (strVal == "L") {
                                objRadio.eq(5).attr("checked", "checked");
                            } else {
                                objRadio.eq(6).attr("checked", "checked");
                                ary = strVal.split(",");
                                for (var i = 0; i < ary.length; i++) {
                                    $(".dayList input[value='" + ary[i] + "']").attr("checked", "checked");
                                }
                            }
                        }

                        function initMonth(strVal) {
                            var ary = null;
                            var objRadio = $("input[name='mouth'");
                            if (strVal == "*") {
                                objRadio.eq(0).attr("checked", "checked");
                            } else if (strVal == "?") {
                                objRadio.eq(1).attr("checked", "checked");
                            } else if (strVal.split('-').length > 1) {
                                ary = strVal.split('-');
                                objRadio.eq(2).attr("checked", "checked");
                                $("#mouthStart_0").numberspinner('setValue', ary[0]);
                                $("#mouthEnd_0").numberspinner('setValue', ary[1]);
                            } else if (strVal.split('/').length > 1) {
                                ary = strVal.split('/');
                                objRadio.eq(3).attr("checked", "checked");
                                $("#mouthStart_1").numberspinner('setValue', ary[0]);
                                $("#mouthEnd_1").numberspinner('setValue', ary[1]);

                            } else {
                                objRadio.eq(4).attr("checked", "checked");

                                ary = strVal.split(",");
                                for (var i = 0; i < ary.length; i++) {
                                    $(".mouthList input[value='" + ary[i] + "']").attr("checked", "checked");
                                }
                            }
                        }

                        function initWeek(strVal) {
                            var ary = null;
                            var objRadio = $("input[name='week'");
                            if (strVal == "*") {
                                objRadio.eq(0).attr("checked", "checked");
                            } else if (strVal == "?") {
                                objRadio.eq(1).attr("checked", "checked");
                            } else if (strVal.split('/').length > 1) {
                                ary = strVal.split('/');
                                objRadio.eq(2).attr("checked", "checked");
                                $("#weekStart_0").numberspinner('setValue', ary[0]);
                                $("#weekEnd_0").numberspinner('setValue', ary[1]);
                            } else if (strVal.split('-').length > 1) {
                                ary = strVal.split('-');
                                objRadio.eq(3).attr("checked", "checked");
                                $("#weekStart_1").numberspinner('setValue', ary[0]);
                                $("#weekEnd_1").numberspinner('setValue', ary[1]);
                            } else if (strVal.split('L').length > 1) {
                                ary = strVal.split('L');
                                objRadio.eq(4).attr("checked", "checked");
                                $("#weekStart_2").numberspinner('setValue', ary[0]);
                            } else {
                                objRadio.eq(5).attr("checked", "checked");
                                ary = strVal.split(",");
                                for (var i = 0; i < ary.length; i++) {
                                    $(".weekList input[value='" + ary[i] + "']").attr("checked", "checked");
                                }
                            }
                        }

                        function initYear(strVal) {
                            var ary = null;
                            var objRadio = $("input[name='year'");
                            if (strVal == "*") {
                                objRadio.eq(1).attr("checked", "checked");
                            } else if (strVal.split('-').length > 1) {
                                ary = strVal.split('-');
                                objRadio.eq(2).attr("checked", "checked");
                                $("#yearStart_0").numberspinner('setValue', ary[0]);
                                $("#yearEnd_0").numberspinner('setValue', ary[1]);
                            }
                        }
                    </script>
                    <div>
                    </div>
                </div>
            </div>
            <div>
            </div>
        </div>
                </div>
            </div>
        </div>
        <% } %>
        <%--</form>--%>
    </div>
</body>
</html>
