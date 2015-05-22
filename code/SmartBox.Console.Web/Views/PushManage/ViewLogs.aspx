<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>推送通道日志</title>
    <link rel="stylesheet" type="text/css" href="../../jquery-easyui-1.3.6/themes/bootstrap/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../jquery-easyui-1.3.6/themes/icon.css" />
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/purl.min.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/moment.min.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/jquery.min.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../jquery-easyui-1.3.6/datagrid-detailview.min.js"></script>
    <style type="text/css">
        .dg_detail_content {
            width: 700px;
            padding: 5px;
            -ms-word-wrap: break-word;
            word-wrap: break-word;
        }
        * {
    font-size: 14px;
}

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
    <div style="width: 800px; text-align: center; padding: 10px; margin: 0 auto;">
        <div id="toolbar">
            <select id="select_level" class="easyui-combobox" editable="false" panelheight="auto" style="width: 100px">
                <option value="">全部</option>
                <option value="info">信息</option>
                <option value="warn">警告</option>
                <option value="error">错误</option>
            </select>
        </div>
        <table id="dg" title="日志列表" style="width: 800px; height: 600px"></table>
    </div>

</body>
<script type="text/javascript">
    $(function () {
        //var cid = $.url(location.href).param('cid');
        cid = '<%=ViewData["cid"] %>';        
        if (cid == null) return;

        $('#dg').datagrid({
            url: '<%=Url.Action("PushProcess")%>?m=QueryLogs',
            toolbar: '#toolbar',
            queryParams: { ChannelId: cid, Level: 'warn' },
            view: detailview,
            pagination: false,
            rownumbers: true,
            fitColumns: true,
            singleSelect: true,
            idField: 'Id',
            onLoadError: function () {
                $.messager.alert('出错了', '服务异常', 'error');
            },
            columns: [[
                { field: 'Title', title: '标题', width: 100 },
                { field: 'UserUid', title: '用户标识', width: 50 },
                {
                    field: 'Level', title: '记录级别', width: 50, styler: function (value, row, index) {
                        if (value == 'info') {
                            return 'color:green;';
                        }
                        else if (value == 'warn') {
                            return 'color:darkorange;';
                        }
                        else {
                            return 'color:red;';
                        }
                    }
                },
                {
                    field: 'LogTime', title: '记录时间', width: 100, formatter: function (value, row, index) {
                        return moment(value).format('YYYY/MM/DD HH:mm:ss');
                    }
                }
            ]],
            detailFormatter: function (index, row) {
                return '<div class="dg_detail"/>';
            },
            onExpandRow: function (index, row) {
                var dg_detail = $(this).datagrid('getRowDetail', index).find('div.dg_detail');
                if (dg_detail.find('.dg_detail_content').length) return;
                var message = $('<p class="dg_detail_content"/>').text(row.Message);
                dg_detail.append(message);
                if (row.StackTrace != null) {
                    var stacktrace = $('<p class="dg_detail_content" style="background-color:#fafce5;"/>').text(row.StackTrace);
                    dg_detail.append(stacktrace);
                }
                $('#dg').datagrid('fixDetailRowHeight', index);
            }
        });

        $('#select_level').combobox({
            onSelect: function (record) {
                $('#dg').datagrid('load', { ChannelId: cid, Level: record.value });
            }
        }).combobox('setValue', 'warn');

    });
</script>
</html>