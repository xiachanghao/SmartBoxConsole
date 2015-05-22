<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>推送通道配置</title>
    <link rel="stylesheet" type="text/css" href="../../jquery-easyui-1.3.6/themes/bootstrap/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../jquery-easyui-1.3.6/themes/icon.css" />
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/jquery.min.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/locale/easyui-lang-zh_CN.js"></script>
    <style type="text/css">
        
html, body {
    margin: 0;
    padding: 0;
}

body {
    font-family: verdana,helvetica,arial,sans-serif;
    background: #fafafa;
    text-align: center;
}

h3 {
    font-size: 20px;
    font-weight: bold;
    margin: 12px 0;
}

h4 {
    font-size: 16px;
    font-weight: bold;
    color: #CC0000;
    margin: 10px 0;
}

h5 {
    font-size: 14px;
    font-weight: bold;
    color: #990000;
    margin: 5px 0;
}

textarea.html, textarea.js {
    width: 100%;
    height: 80px;
    overflow: hidden;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    -ms-box-sizing: border-box;
    -o-box-sizing: border-box;
    box-sizing: border-box;
}

#header {
    text-align: center;
    background: #666;
    direction: ltr;
}

#header-inner {
    text-align: left;
    margin: 0 auto;
    width: 960px;
    padding: 10px 0;
}

#mainwrap {
    text-align: center;
    margin-top: 5px;
}

#content {
    text-align: left;
    width: 1024px;
    margin: 0 auto;
    padding: 10px;
    border-radius: 5px;
    border: 1px solid #ddd;
    background: #fff;
}

    #content img {
        margin: 0;
    }

#footer {
    text-align: center;
    padding: 10px;
}

#topmenu {
    text-align: right;
}

    #topmenu a {
        display: inline-block;
        padding: 1px 3px;
        text-decoration: none;
        color: #fff;
    }

        #topmenu a:hover {
            text-decoration: underline;
        }

a.download-link {
    color: #0000ff;
}

.doc-table {
    border-collapse: collapse;
    border-spacing: 0;
}

    .doc-table th, .doc-table td {
        border: 1px solid #8CACBB;
        padding: 0.3em 0.7em;
    }

    .doc-table th {
        background: #eee;
    }

    .doc-table pre {
        font-family: Verdana;
        font-size: 12px;
        color: #006600;
        background: #fafafa;
        padding: 5px;
    }

#dlg_form {
    margin: 0;
    padding: 10px 30px;
}

.ftitle {
    font-size: 14px;
    font-weight: bold;
    padding: 5px 0;
    margin-bottom: 10px;
    border-bottom: 1px solid #ccc;
}

.fitem {
    margin-top: 5px;
    margin-bottom: 5px;
}

    .fitem label {
        display: inline-block;
        vertical-align: baseline;
        width: 80px;
    }

    .fitem input {
        width:220px;
    }
    </style>
</head>
<body>
    <div class="easyui-panel" data-options="fit:true" style="text-align: center; border-width:0px; margin: 0 auto;">
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="newChannel()">新增通道</a>
            <a href="javascript:void(0)" class="easyui-linkbutton tool_group3" iconcls="icon-search" plain="true" onclick="viewLog()">查看日志</a>
            <a href="javascript:void(0)" class="easyui-linkbutton tool_group1" iconcls="icon-tip" plain="true" onclick="showTestDlg()">测试通道</a>
            <a href="javascript:void(0)" class="easyui-linkbutton tool_group1" iconcls="icon-cancel" plain="true" onclick="updateChannelState(1)">停止通道</a>
            <a href="javascript:void(0)" class="easyui-linkbutton tool_group2" iconcls="icon-edit" plain="true" onclick="editChannel()">编辑通道</a>
            <a href="javascript:void(0)" class="easyui-linkbutton tool_group2" iconcls="icon-reload" plain="true" onclick="updateChannelState(0)">启用通道</a>
            <a href="javascript:void(0)" class="easyui-linkbutton tool_group2" iconcls="icon-remove" plain="true" onclick="updateChannelState(2)">删除通道</a>
        </div>
        <table id="dg" title="推送通道列表" style="height: 600px"></table>
    </div>

    <div id="dlg" class="easyui-dialog" style="width: 450px; height: 400px; padding: 10px 20px" closed="true" buttons="#dlg-buttons">
        <form id="dlg_form" method="post" enctype="multipart/form-data">
            <div class="ftitle">基本信息</div>
            <div class="fitem">
                <label>名称：</label>
                <input name="Id" style="display: none" hidden="hidden" />
                <input name="Title" class="easyui-validatebox" data-options="required:true" spellcheck="false" />
            </div>
            <div class="fitem">
                <label>应用标识：</label>
                <input name="ApplicationId" class="easyui-validatebox" data-options="required:true" spellcheck="false" />
            </div>
            <div class="fitem">
                <label>平台类型：</label>
                <select id="PlatformType" name="PlatformType" class="easyui-combobox" data-options="editable:false,required:true" style="width: 225px;">
                    <option value="Apple">Apple</option>
                    <option value="Beyondbit">Beyondbit</option>
                    <option value="Google">Google</option>
                    <option value="Windows">Windows</option>
                    <option value="WindowsPhone">WindowsPhone</option>
                </select>
            </div>
            <div class="ftitle">证书相关</div>
            <div class="fitem">
                <label>证书：</label>
                <input name="CertName" spellcheck="false" readonly="readonly" style="border: none" />
            </div>
            <div class="fitem">
                <label>密码：</label>
                <input name="CertPassword" class="easyui-validatebox" />
            </div>
            <input id="Cert" name="Cert" type="file" />
        </form>
    </div>

    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveChannel()">保存</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>

    <div id="test_dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 10px 20px" closed="true" buttons="#test-dlg-buttons">
        <form id="test_dlg_form" method="post">
            <div class="ftitle">推送信息</div>
            <div class="fitem">
                <label>内容：</label>
                <input id="ChannelId" name="ChannelId" style="display: none" hidden="hidden" />
                <input name="Alert" spellcheck="false" />
            </div>
            <div class="fitem">
                <label>数字：</label>
                <input name="Badge" class="easyui-numberspinner" style="width: 80px;" data-options="min:0,editable:true" />
            </div>
            <div class="fitem">
                <label>声音：</label>
                <input name="Sound" spellcheck="false" />
            </div>
            <div class="fitem">
                <label title="多个用户用逗号分隔">用户标识：</label>
                <input name="Uids" class="easyui-validatebox" data-options="required:true" spellcheck="false" />
            </div>
            <div class="fitem">
                <label title="JSON对象的字符串形式">自定义：</label>
                <input name="Custom" spellcheck="false" />
            </div>
        </form>
    </div>

    <div id="test-dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-redo" onclick="testChannel()">发送</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#test_dlg').dialog('close')">取消</a>
    </div>

    <script type="text/javascript">
        $(function () {
            $('#dg').datagrid({
                url: '<%=Url.Action("PushProcess")%>?m=QueryChannels',               
                toolbar: '#toolbar',                
                pagination: false,
                rownumbers: true,
                fitColumns: true,
                singleSelect: true,
                idField: 'Id',
                onLoadError: function () {
                    $.messager.alert('出错了', '服务异常', 'error');
                },
                onLoadSuccess: function (data) {
                    updateTools();
                },
                onClickRow: function (rowIndex, rowData) {
                    updateTools();
                },
                columns: [[
                    { field: 'Title', title: '名称', width: 100 },
                    { field: 'ApplicationId', title: '应用标识', width: 70 },
                    { field: 'PlatformType', title: '平台类型', width: 50 },
                    {
                        field: 'State', title: '状态', width: 50, formatter: function (value, row, index) {
                            return value == 0 ? '启用' : "停用";
                        }, styler: function (value, row, index) {
                            return value == 0 ? 'color:green;' : 'color:red;';
                        }
                    }
                ]]
            });
        });

        function updateTools() {
            var row = $('#dg').datagrid('getSelected');
            var state = 2;
            if (row) {
                state = row.State;
                $('.tool_group3').show();
            }
            else {
                $('.tool_group3').hide();
            }

            if (state == 0) {
                $('.tool_group1').show();
                $('.tool_group2').hide();
            }
            else if (state == 1) {
                $('.tool_group1').hide();
                $('.tool_group2').show();
            }
            else {
                $('.tool_group1').hide();
                $('.tool_group2').hide();
            }
        }

        function newChannel() {
            $('#dlg').dialog('open').dialog('setTitle', '新增通道');
            $('#dlg_form').form('clear');
            $('#Cert').val('');
        }

        function editChannel() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#Cert').val('');
                $('#dlg').dialog('open').dialog('setTitle', '编辑通道');
                $('#dlg_form').form('load', row);
            }
        }

        function saveChannel() {
            $('#dlg_form').form('submit', {
                //url: 'config.ashx?m=SaveChannel',
                url: '<%=Url.Action("PushProcess")%>?m=SaveChannel',  
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    $('#dlg').dialog('close');
                    $('#dg').datagrid('reload');
                }
            });
        }

        function updateChannelState(state) {
            var row = $('#dg').datagrid('getSelected');
            if (!row) return;
            if (state == 2) {
                $.messager.confirm('警告', ' 您确认要删除推送通道 ' + row.Title + ' ？', function (r) {
                    if (r) {
                        updateChannelState2(row, state);
                    }
                });
            }
            else {
                updateChannelState2(row, state);
            }
        }

        function updateChannelState2(row, state) {
            $.ajax({                
                url: '<%=Url.Action("PushProcess")%>?m=UpdateChannelState',   
                type: "POST",
                dataType: 'text',
                data: { Id: row.Id, State: state },
                success: function (data) {
                    row.State = state;
                    $('#dg').datagrid('reload');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.messager.alert('出错了', textStatus, 'error');
                }
            });
        }

        function showTestDlg() {
            var row = $('#dg').datagrid('getSelected');
            if (!row) return;
            $('#test_dlg').dialog('open').dialog('setTitle', '测试通道');
            $('#test_dlg_form #ChannelId').val(row.Id);
        }

        function testChannel() {
            $('#test_dlg_form').form('submit', {
                //url: 'config.ashx?m=TestChannel',
                url: '<%=Url.Action("PushProcess")%>?m=TestChannel',
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    $('#test_dlg').dialog('close');
                }
            });
        }

        function viewLog() {
            var row = $('#dg').datagrid('getSelected');
            if (!row) return;
            //window.open('log.html?cid=' + row.Id, '_blank');
            var url = 'ViewLogs?cid=' + row.Id;
            window.open(url, '_blank');
            //window.showModalDialog(url, { width: 750, height: 550, caption: "查看推送日志" });
        }

    </script>
</body>
</html>
