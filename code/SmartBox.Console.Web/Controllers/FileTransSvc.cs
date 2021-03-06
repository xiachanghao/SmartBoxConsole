﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IFileTransSvc")]
public interface IFileTransSvc
{
    
    // CODEGEN: 消息 FileUploadMessage 的包装名称(FileUploadMessage)以后生成的消息协定与默认值(UploadFileMethod)不匹配
    [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="UploadFile")]
    void UploadFileMethod(FileUploadMessage request);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransSvc/GetFilesList", ReplyAction="http://tempuri.org/IFileTransSvc/GetFilesListResponse")]
    string[] GetFilesList(bool isInfoFile);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileTransSvc/DownLoadFile", ReplyAction="http://tempuri.org/IFileTransSvc/DownLoadFileResponse")]
    System.IO.Stream DownLoadFile(bool isInfoFile, string fileName);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(WrapperName="FileUploadMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
public partial class FileUploadMessage
{
    
    [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
    public string FileName;
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
    public System.IO.Stream FileData;
    
    public FileUploadMessage()
    {
    }
    
    public FileUploadMessage(string FileName, System.IO.Stream FileData)
    {
        this.FileName = FileName;
        this.FileData = FileData;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IFileTransSvcChannel : IFileTransSvc, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class FileTransSvcClient : System.ServiceModel.ClientBase<IFileTransSvc>, IFileTransSvc
{
    
    public FileTransSvcClient()
    {
    }
    
    public FileTransSvcClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public FileTransSvcClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public FileTransSvcClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public FileTransSvcClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    void IFileTransSvc.UploadFileMethod(FileUploadMessage request)
    {
        base.Channel.UploadFileMethod(request);
    }
    
    public void UploadFileMethod(string FileName, System.IO.Stream FileData)
    {
        FileUploadMessage inValue = new FileUploadMessage();
        inValue.FileName = FileName;
        inValue.FileData = FileData;
        ((IFileTransSvc)(this)).UploadFileMethod(inValue);
    }
    
    public string[] GetFilesList(bool isInfoFile)
    {
        return base.Channel.GetFilesList(isInfoFile);
    }
    
    public System.IO.Stream DownLoadFile(bool isInfoFile, string fileName)
    {
        return base.Channel.DownLoadFile(isInfoFile, fileName);
    }
}
