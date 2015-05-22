<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListBUDN.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	单位管理员注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="table_box">
        <h4>单位管理员注册</h4>
        <div class="table_box_data table_box_data_model_details">
            <table  cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th width="30%">单位：</th>
                        <td class=""><select id="selUnit" class="easyui-combobox" data-options="panelHeight:'auto',url:'<%=Url.Action("GetUnitData") %>',
    valueField:'OrgCode',
    textField:'OrgName',onSelect: function(org){
            var url = '<%=Url.Action("GetUnitUserData") %>?unitCode='+org.OrgCode;
            $('#selUnitManager').combobox('reload', url);
            var url2 = '<%=Url.Action("GetUnitUserData2") %>?unitCode='+org.OrgCode;
            $('#selUnitManager2').combotree('reload', url2);
        }" name="state" style="width:184px;">
                        </select><input type="button" id="Button1" class="btnskin_b" onclick="javascript:addNewUnit();" value="新增单位" /></td>
                    </tr>
                    <tr>
                        <th>选择:</th>
                        <td><input id="chkOldUser" checked type=radio name="umode" />已有用户<input id="chkNewUser" type=radio name="umode" />新用户</td>
                    </tr>
                    <tr id="trUser">
                        <th>从现有用户选择:</th>
                        <td>
                        <input id="selUnitManager2" class="easyui-combotree" data-options="panelHeight:'auto',valueField:'id',
    textField:'text',lines:true,loadFilter:selUnitManager2LoadFilter" style="width:200px;">
                        <!-- select id="selUnitManager" class="easyui-combobox" data-options="panelHeight:'auto',
    valueField:'UserUid',
    textField:'UserName',onSelect: function(user){
            var uid = user.UserUid;
            var Name = user.UserName;
            $('#tbUName').val(Name);
            $('#tbUID').val(uid);
            $('#selUnitManager').combobox('reload', url);
        }" name="state" style="width:284px;">
                        <option value="">请选择</option>
		                <option value="ENABLE">周晓军</option>
		                <option value="DISABLE">杨函</option>
                        <option value="DISABLE">邓华</option>
                        <option value="DISABLE">张翰</option>
                        </select--> </td>
                    </tr>
                    <tr id="tr1" style="display:none;">
                        <th>新用户:</th>
                        <td><input id="tbUName" /><span style="color:red">*</span></td>
                    </tr>
                    <tr id="tr2" style="display:none;">
                        <th>新用户账号:</th>
                        <td><input id="tbUID" /><span style="color:red">*</span></td>
                    </tr>
                    <tr id="tr3" style="display:none;">
                        <th>EMail:</th>
                        <td><input id="tbEMail" /><span style="color:red">*</span></td>
                    </tr>
                    <tr id="tr4" style="display:none;">
                        <th>电话号码:</th>
                        <td><input id="tbPhone" /><span style="color:red">*</span></td>
                    </tr>
                    <tr id="tr5" style="display:none;">
                        <th>密码:</th>
                        <td><input id="tbPassword" type="password" /><span style="color:red">*</span></td>
                    </tr>
                    <tr id="tr6" style="display:none;">
                        <th>确认密码:</th>
                        <td><input id="tbPasswordConfirm" type="password" /><span style="color:red">*</span></td>
                    </tr>
                    
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <input type="button" id="btnSave" class="btnskin_b" value="保存" />
                            <input type="button" class="btnskin_b" value="取消" onclick="javascript:closeWin();" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    
    </div><div id="w" class="easyui-window" title="插件" closed="true" modal="true" data-options="minimizable:false,collapsible:false,maximizable:false,onClose:function(){$('#wif').attr('src', ''); return false}" style="width:650px;height:320px;padding:3px;">
		<iframe scrolling="auto" id='wif' frameborder="0"  src="" style="width:100%;height:100%;"></iframe>
	</div>
    <script>
        var usermode = 'old';
        function new_unit() {
            //$('span.combo').hide();
            //$('#trUser').hide();
            //$('#trNewUser').show(); 
            //('#trNewUserAccount').show();
            //$('#tbUnit').show();
            usermode = 'new';
        }

        function new_user() {
            $('#trUser').hide();
            //$('#trNewUser').show();
            //$('#trNewUserAccount').show();
            usermode = 'new';
        }

        function old_user() {
            //$('span.combo').show();
            $('#trUser').show();
            //$('#trNewUser').hide();
            //$('#trNewUserAccount').hide();
            usermode = 'old';
        }

        function addNewUnit() {
            //$.messager.alert('提示', '你好', 'info');
            var url = '<%=Url.Content("~/") %>Loginless/UnitAdd';
            $('#wif')[0].src = url;
            $('#w').window('open');
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<link href="../../telerik/styles/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="../../telerik/styles/kendo.silver.min.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../telerik/js/jquery.min.js" type="text/javascript"></script>--%>
    <script src="../../telerik/js/kendo.all.min.js" type="text/javascript"></script>

    <style>
	html,body {font-size:12px;}
	.k-upload {background-color:transparent;border-width:0px;}
	k-state-focused {box-shadow:0px 0px 6px 0px #CDCDCD;}
	h4.file-name-heading {margin-top:20px;}
	.k-upload-button {margin-left:0px!important;padding-left:0px!important;}
	.k-dropzone {padding-left:0px;}
	.k-file {padding-lft:0px;}
	<!--[if IE]>
	.k-upload-status-total {padding-top:10px;}
	.k-upload-status {padding-top:5px;} 
	<![endif]-->
	
	</style>

<script type="text/javascript">

function closeWin() {
    if (parent)
        parent.CloseWind(false);
}

function CloseWind() {
    $('#w').window('close');
}

function selUnitManager2LoadFilter(data, parent) {
    return data;
}


function reloadUnitList() {
        var url = '<%=Url.Content("~/Loginless/GetUnitData") %>'
        $('#selUnit').combobox('reload', url);        
    }

    function postfrm() {
        var o = {};
        var unitCode = $('#selUnit').combobox('getValue');
        var unitText = $('#selUnit').combobox('getText');

        o.UID = $('#tbUID').val();
            o.UName = $('#tbUName').val();
            o.UnitCode = unitCode;
            o.UnitName = unitText;
            o.EMail = $('#tbEMail').val();
            o.Phone = $('#tbPhone').val();
            o.Password = $('#tbPasswordConfirm').val();
            o.IsFromBUAUser = (usermode == 'old');//true为从bua里的用户选取，false为新增的管理员，在bua里不存在
            if (o.IsFromBUAUser) {
                //o.UID = $('#selUnitManager').combobox('getValue');
                var uid = $('#selUnitManager2').combotree('getValue');
                var uname = $('#selUnitManager2').combotree('getText');
                o.UID = uid.replace('u_', '');
                o.UName = uname;
            }

            $.ajax({
                type: "POST",
                url: '<%=Url.Action("AddUnitManager") %>',
                data: o,
                dataType: "json",
                success: function (data) {
                    if (data.r) {
                        $('#tbUName').val('');
                        $('#tbUID').val('');
                        $('#tbEMail').val('');
                        $('#tbPhone').val('');
                        $('#tbPassword').val('');
                        $('#tbPasswordConfirm').val('');
                    }
                    $.messager.alert('提示', data.d, 'info', function () {
                        if (data.r && parent)
                            parent.CloseWind(true);
                        if (!data.r && parent)
                            parent.CloseWind(false);
                    });
                }
            });
    }

    $(document).ready(function () {
        $('#chkOldUser').click(function () {
            if ($(this)[0].checked) {
                $('#tr1').hide();
                $('#tr2').hide();
                $('#tr3').hide();
                $('#tr4').hide();
                $('#tr5').hide();
                $('#tr6').hide();
                $('#trUser').show();
                usermode = 'old';
            }
        });
        $('#chkNewUser').click(function () {
            if ($(this)[0].checked) {
                $('#tr1').show();
                $('#tr2').show();
                $('#tr3').show();
                $('#tr4').show();
                $('#tr5').show();
                $('#tr6').show();
                $('#trUser').hide();
                usermode = 'new';
            }
        });
        $('#btnSave').click(function () {
            var needReturn = false;
            var needCheckInput = (usermode == 'new');
            if (needCheckInput) {
                if ($('#tbPassword').val().trim() == '') {
                    $.messager.alert('提示', '密码不能为空！', 'info', function () {


                    });
                } else {
                    if ($('#tbPasswordConfirm').val() != $('#tbPassword').val()) {
                        $.messager.alert('提示', '两次输入密码不一致！', 'info', function () {

                        });
                    } else {
                        postfrm();
                    }
                }
            }
            else {
                var uid = $('#selUnitManager2').combotree('getValue');
                var uname = $('#selUnitManager2').combotree('getText');
                if (uid.indexOf('o_') != -1) {
                    $.messager.alert('提示', '只能选择用户，不能选择组织单位！', 'info');
                } else {
                    //var uid = $('#selUnitManager').combobox('getValue');
                    if (uid == '') {
                        $.messager.alert('提示', '请选择用户！', 'info');
                    } else {
                        postfrm();
                    }
                }
            }

            return false;
        });


    });
    
</script>
</asp:Content>
