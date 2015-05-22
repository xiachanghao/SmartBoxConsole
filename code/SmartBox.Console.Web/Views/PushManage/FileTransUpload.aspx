<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	上传文件
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
	<style>
	html {font-size:12px;}
	.k-upload {background-color:rgb(243, 243, 244);}
	k-state-focused {box-shadow:0px 0px 6px 0px #CDCDCD;}
	h4.file-name-heading {margin-top:20px;}
	.k-upload-button {margin-left:0px!important;padding-left:0px!important;}
	</style>
    <link href="<%=Url.Content("~/Themes/Default/main.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="example" class="k-content">

<div class="cHead">
            <div class="ftitle">
                <span id="departmentName">任务插件上传</span>
            </div>
        </div>  
    <input type="file" name="files" id="files" />

            <script id="fileTemplate" type="text/x-kendo-template">
                <span class='k-progress'></span>
                <div class='file-wrapper'>
                    <span class='file-icon #=addExtensionClass(files[0].extension)#'></span>
                    <h4 class='file-heading file-name-heading'>文件名: #=name#</h4>
                    <h4 class='file-heading file-size-heading'>大小: #=size# bytes</h4>
                    <button type='button' class='k-upload-action'></button>
                </div>
            </script>

            <script>
            function onError(e) {
        // Array with information about the uploaded files
        var files = e.files;

        if (e.operation == "upload") {
            //alert("Failed to upload " + files.length + " files");
        }
    }

    function onCancel(e) {
        // Array with information about the uploaded files
        var files = e.files;

        // Process the Cancel event
    }

    function onComplete(e) {
        // The upload is now idle
        parent.CloseWind();
    }

    function onProgress(e) {
        // Array with information about the uploaded files
        var files = e.files;

        //console.log(e.percentComplete);
    }

    function onRemove(e) {
        // Array with information about the removed files
        var files = e.files;

        // Process the Remove event
        // Optionally cancel the remove operation by calling
        // e.preventDefault()
    }

                $(document).ready(function () {
                    $("#files").kendoUpload({
                        multiple: true,
                        async: {
                            saveUrl: "<%=Url.Content("~/PushManage/UploadFile") %>",
                            removeUrl: "remove",
                            autoUpload: false
                        },
                        error: onError,
                        cancel: onCancel,
                        complete: onComplete,
                        progress: onProgress,
                        remove: onRemove,
                            localization: {
                                select: "选择文件",
                                uploadSelectedFiles: "上传",
                                statusUploading: "文件上传中...",
                                statusUploaded: "文件上传完成！",
                                statusFailed: "上传失败",
                                retry: "重试",
                                remove: "移除",
                                headerStatusUploading: "上传中",
                                headerStatusUploaded: "上传完成",
                                dropFilesHere: "将文件拖放于此",
                                cancel: "取消"
                            },
                            showFileList: true,

                        template: kendo.template($('#fileTemplate').html())
                    });
                });

                function addExtensionClass(extension) {
                    switch (extension) {
                        case '.jpg':
                        case '.img':
                        case '.png':
                        case '.gif':
                            return "img-file";
                        case '.doc':
                        case '.docx':
                            return "doc-file";
                        case '.xls':
                        case '.xlsx':
                            return "xls-file";
                        case '.pdf':
                            return "pdf-file";
                        case '.zip':
                        case '.rar':
                            return "zip-file";
                        case '.dll':
                            return "dll-file";
                        default:
                            return "default-file";
                    }
                }
            </script>

            <style scoped>
                .file-icon
                {
                    display: inline-block;
                    float: left;
                    width: 48px;
                    height: 48px;
                    margin-left: 10px;
                    margin-top: 13.5px;
                }

                .img-file { background-image: url(../../images/upload/jpg.png) }
                .doc-file { background-image: url(../../images/upload/doc.png) }
                .pdf-file { background-image: url(../../images/upload/pdf.png) }
                .xls-file { background-image: url(../../images/upload/xls.png) }
                .zip-file { background-image: url(../../images/upload/zip.png) }
                .dll-file { background-image: url(../../images/upload/dll.png) }
                .default-file { background-image: url(../../images/upload/default.png) }

                #example .file-heading
                {
                    font-family: Arial;
                    font-size: 1.1em;
                    display: inline-block;
                    float: left;
                    width: 450px;
                    margin: 0 0 0 20px;
                    height: 25px;
                    -ms-text-overflow: ellipsis;
                    -o-text-overflow: ellipsis;
                    text-overflow: ellipsis;
                    overflow:hidden;
                    white-space:nowrap;
                }

                    #example .file-name-heading
                    {
                        font-weight: bold;
                        margin-top:20px;
                    }

                     #example .file-size-heading
                    {
                        font-weight: normal;
                        font-style: italic;
                        margin-top:20px;
                    }

                li.k-file .file-wrapper .k-upload-action
                {
                    position: absolute;
                    top: 0;
                    right: 0;
                }

                li.k-file div.file-wrapper
                {
                    position: relative;
                    height: 75px;
                }
                .k-delete 
                {
                    margin-top:28px;
                }
                
                .k-retry 
                {
                    margin-top:28px;
                }
                
                .img-file 
                {
                    margin-top:12px;
                }
                ul.k-upload-files 
                {
                    <%--height:380px;
                    overflow:scroll;--%>
                }
            </style>
</div>
</asp:Content>
