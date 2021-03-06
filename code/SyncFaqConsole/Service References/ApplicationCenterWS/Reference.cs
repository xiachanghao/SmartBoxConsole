﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1022
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncFaqConsole.ApplicationCenterWS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ApplicationCenterWS.WebServiceSoap")]
    public interface WebServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SMC_UnitSync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        bool SMC_UnitSync(SMC_Unit[] units);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SMC_UserSync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        bool SMC_UserSync(SMC_User[] users);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PackagePictuerSync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        bool PackagePictuerSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackagePicture entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PackageManualSync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        bool PackageManualSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackageManual entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PackageFAQSync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        bool PackageFAQSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackageFAQ faq);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetNeedSyncToInsideFAQ", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        SMC_PackageFAQ[] GetNeedSyncToInsideFAQ();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PackageExtSync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        bool PackageExtSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackageExt entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/PackageFilesSync", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntry))]
        bool PackageFilesSync(FileEntity[] files, int pe_id);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SMC_Unit : BaseEntry {
        
        private int unit_IDField;
        
        private string unit_NameField;
        
        private int upper_Unit_IDField;
        
        private string unit_DemoField;
        
        private string unit_PathField;
        
        private System.DateTime unit_CreatedTimeField;
        
        private string unit_CreatedUserField;
        
        private System.DateTime unit_UpdateTimeField;
        
        private string unit_UpdateUserField;
        
        private int unit_SequenceField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Unit_ID {
            get {
                return this.unit_IDField;
            }
            set {
                this.unit_IDField = value;
                this.RaisePropertyChanged("Unit_ID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Unit_Name {
            get {
                return this.unit_NameField;
            }
            set {
                this.unit_NameField = value;
                this.RaisePropertyChanged("Unit_Name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int Upper_Unit_ID {
            get {
                return this.upper_Unit_IDField;
            }
            set {
                this.upper_Unit_IDField = value;
                this.RaisePropertyChanged("Upper_Unit_ID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Unit_Demo {
            get {
                return this.unit_DemoField;
            }
            set {
                this.unit_DemoField = value;
                this.RaisePropertyChanged("Unit_Demo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Unit_Path {
            get {
                return this.unit_PathField;
            }
            set {
                this.unit_PathField = value;
                this.RaisePropertyChanged("Unit_Path");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public System.DateTime Unit_CreatedTime {
            get {
                return this.unit_CreatedTimeField;
            }
            set {
                this.unit_CreatedTimeField = value;
                this.RaisePropertyChanged("Unit_CreatedTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Unit_CreatedUser {
            get {
                return this.unit_CreatedUserField;
            }
            set {
                this.unit_CreatedUserField = value;
                this.RaisePropertyChanged("Unit_CreatedUser");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public System.DateTime Unit_UpdateTime {
            get {
                return this.unit_UpdateTimeField;
            }
            set {
                this.unit_UpdateTimeField = value;
                this.RaisePropertyChanged("Unit_UpdateTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string Unit_UpdateUser {
            get {
                return this.unit_UpdateUserField;
            }
            set {
                this.unit_UpdateUserField = value;
                this.RaisePropertyChanged("Unit_UpdateUser");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public int Unit_Sequence {
            get {
                return this.unit_SequenceField;
            }
            set {
                this.unit_SequenceField = value;
                this.RaisePropertyChanged("Unit_Sequence");
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SMC_PackageExt))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SMC_PackageFAQ))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SMC_PackageManual))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SMC_PackagePicture))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SMC_User))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SMC_Unit))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public abstract partial class BaseEntry : object, System.ComponentModel.INotifyPropertyChanged {
        
        private EnumObjectState objectEntryStateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public EnumObjectState ObjectEntryState {
            get {
                return this.objectEntryStateField;
            }
            set {
                this.objectEntryStateField = value;
                this.RaisePropertyChanged("ObjectEntryState");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum EnumObjectState {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Added,
        
        /// <remarks/>
        Upded,
        
        /// <remarks/>
        Deleted,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class FileEntity : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string fileNameField;
        
        private byte[] contentField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
                this.RaisePropertyChanged("FileName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=1)]
        public byte[] Content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
                this.RaisePropertyChanged("Content");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SMC_PackageExt : BaseEntry {
        
        private int pe_idField;
        
        private System.DateTime pe_UpdateTimeField;
        
        private string pe_UpdateUidField;
        
        private string pe_CreateUidField;
        
        private string pe_DownloadUriField;
        
        private string pe_BuildVerField;
        
        private string pe_VersionField;
        
        private string pe_DescriptionField;
        
        private string pe_DisplayNameField;
        
        private string pe_ClientTypeField;
        
        private string pe_IsTJField;
        
        private string pe_IsBBField;
        
        private string pe_PictureUrlField;
        
        private string pe_2dPictureUrlField;
        
        private string pe_FirmwareField;
        
        private string tableNameField;
        
        private string pe_UnitCodeField;
        
        private string pe_UnitNameField;
        
        private string pe_NameField;
        
        private int tableIDField;
        
        private int pe_DownCountField;
        
        private int pe_SizeField;
        
        private System.DateTime pe_CreatedTimeField;
        
        private string pe_CategoryField;
        
        private string pe_CategoryIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int pe_id {
            get {
                return this.pe_idField;
            }
            set {
                this.pe_idField = value;
                this.RaisePropertyChanged("pe_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public System.DateTime pe_UpdateTime {
            get {
                return this.pe_UpdateTimeField;
            }
            set {
                this.pe_UpdateTimeField = value;
                this.RaisePropertyChanged("pe_UpdateTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string pe_UpdateUid {
            get {
                return this.pe_UpdateUidField;
            }
            set {
                this.pe_UpdateUidField = value;
                this.RaisePropertyChanged("pe_UpdateUid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string pe_CreateUid {
            get {
                return this.pe_CreateUidField;
            }
            set {
                this.pe_CreateUidField = value;
                this.RaisePropertyChanged("pe_CreateUid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string pe_DownloadUri {
            get {
                return this.pe_DownloadUriField;
            }
            set {
                this.pe_DownloadUriField = value;
                this.RaisePropertyChanged("pe_DownloadUri");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string pe_BuildVer {
            get {
                return this.pe_BuildVerField;
            }
            set {
                this.pe_BuildVerField = value;
                this.RaisePropertyChanged("pe_BuildVer");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string pe_Version {
            get {
                return this.pe_VersionField;
            }
            set {
                this.pe_VersionField = value;
                this.RaisePropertyChanged("pe_Version");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string pe_Description {
            get {
                return this.pe_DescriptionField;
            }
            set {
                this.pe_DescriptionField = value;
                this.RaisePropertyChanged("pe_Description");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string pe_DisplayName {
            get {
                return this.pe_DisplayNameField;
            }
            set {
                this.pe_DisplayNameField = value;
                this.RaisePropertyChanged("pe_DisplayName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string pe_ClientType {
            get {
                return this.pe_ClientTypeField;
            }
            set {
                this.pe_ClientTypeField = value;
                this.RaisePropertyChanged("pe_ClientType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string pe_IsTJ {
            get {
                return this.pe_IsTJField;
            }
            set {
                this.pe_IsTJField = value;
                this.RaisePropertyChanged("pe_IsTJ");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string pe_IsBB {
            get {
                return this.pe_IsBBField;
            }
            set {
                this.pe_IsBBField = value;
                this.RaisePropertyChanged("pe_IsBB");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string pe_PictureUrl {
            get {
                return this.pe_PictureUrlField;
            }
            set {
                this.pe_PictureUrlField = value;
                this.RaisePropertyChanged("pe_PictureUrl");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string pe_2dPictureUrl {
            get {
                return this.pe_2dPictureUrlField;
            }
            set {
                this.pe_2dPictureUrlField = value;
                this.RaisePropertyChanged("pe_2dPictureUrl");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public string pe_Firmware {
            get {
                return this.pe_FirmwareField;
            }
            set {
                this.pe_FirmwareField = value;
                this.RaisePropertyChanged("pe_Firmware");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public string TableName {
            get {
                return this.tableNameField;
            }
            set {
                this.tableNameField = value;
                this.RaisePropertyChanged("TableName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public string pe_UnitCode {
            get {
                return this.pe_UnitCodeField;
            }
            set {
                this.pe_UnitCodeField = value;
                this.RaisePropertyChanged("pe_UnitCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=17)]
        public string pe_UnitName {
            get {
                return this.pe_UnitNameField;
            }
            set {
                this.pe_UnitNameField = value;
                this.RaisePropertyChanged("pe_UnitName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=18)]
        public string pe_Name {
            get {
                return this.pe_NameField;
            }
            set {
                this.pe_NameField = value;
                this.RaisePropertyChanged("pe_Name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=19)]
        public int TableID {
            get {
                return this.tableIDField;
            }
            set {
                this.tableIDField = value;
                this.RaisePropertyChanged("TableID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=20)]
        public int pe_DownCount {
            get {
                return this.pe_DownCountField;
            }
            set {
                this.pe_DownCountField = value;
                this.RaisePropertyChanged("pe_DownCount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=21)]
        public int pe_Size {
            get {
                return this.pe_SizeField;
            }
            set {
                this.pe_SizeField = value;
                this.RaisePropertyChanged("pe_Size");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=22)]
        public System.DateTime pe_CreatedTime {
            get {
                return this.pe_CreatedTimeField;
            }
            set {
                this.pe_CreatedTimeField = value;
                this.RaisePropertyChanged("pe_CreatedTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=23)]
        public string pe_Category {
            get {
                return this.pe_CategoryField;
            }
            set {
                this.pe_CategoryField = value;
                this.RaisePropertyChanged("pe_Category");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=24)]
        public string pe_CategoryID {
            get {
                return this.pe_CategoryIDField;
            }
            set {
                this.pe_CategoryIDField = value;
                this.RaisePropertyChanged("pe_CategoryID");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SMC_PackageFAQ : BaseEntry {
        
        private int pf_idField;
        
        private string pf_uidField;
        
        private string pf_unameField;
        
        private string pf_questionField;
        
        private string pf_answerField;
        
        private System.DateTime pf_askdateField;
        
        private string pf_askemailField;
        
        private int pe_idField;
        
        private string pf_askmobileField;
        
        private string pf_peplymanField;
        
        private bool pf_need_syncto_insideField;
        
        private bool pf_need_syncto_outsideField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int pf_id {
            get {
                return this.pf_idField;
            }
            set {
                this.pf_idField = value;
                this.RaisePropertyChanged("pf_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string pf_uid {
            get {
                return this.pf_uidField;
            }
            set {
                this.pf_uidField = value;
                this.RaisePropertyChanged("pf_uid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string pf_uname {
            get {
                return this.pf_unameField;
            }
            set {
                this.pf_unameField = value;
                this.RaisePropertyChanged("pf_uname");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string pf_question {
            get {
                return this.pf_questionField;
            }
            set {
                this.pf_questionField = value;
                this.RaisePropertyChanged("pf_question");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string pf_answer {
            get {
                return this.pf_answerField;
            }
            set {
                this.pf_answerField = value;
                this.RaisePropertyChanged("pf_answer");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public System.DateTime pf_askdate {
            get {
                return this.pf_askdateField;
            }
            set {
                this.pf_askdateField = value;
                this.RaisePropertyChanged("pf_askdate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string pf_askemail {
            get {
                return this.pf_askemailField;
            }
            set {
                this.pf_askemailField = value;
                this.RaisePropertyChanged("pf_askemail");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public int pe_id {
            get {
                return this.pe_idField;
            }
            set {
                this.pe_idField = value;
                this.RaisePropertyChanged("pe_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string pf_askmobile {
            get {
                return this.pf_askmobileField;
            }
            set {
                this.pf_askmobileField = value;
                this.RaisePropertyChanged("pf_askmobile");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string pf_peplyman {
            get {
                return this.pf_peplymanField;
            }
            set {
                this.pf_peplymanField = value;
                this.RaisePropertyChanged("pf_peplyman");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public bool pf_need_syncto_inside {
            get {
                return this.pf_need_syncto_insideField;
            }
            set {
                this.pf_need_syncto_insideField = value;
                this.RaisePropertyChanged("pf_need_syncto_inside");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public bool pf_need_syncto_outside {
            get {
                return this.pf_need_syncto_outsideField;
            }
            set {
                this.pf_need_syncto_outsideField = value;
                this.RaisePropertyChanged("pf_need_syncto_outside");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SMC_PackageManual : BaseEntry {
        
        private int pm_idField;
        
        private string pm_nameField;
        
        private string pm_urlField;
        
        private System.DateTime pm_createdtimeField;
        
        private System.DateTime pm_updatetimeField;
        
        private int pe_idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int pm_id {
            get {
                return this.pm_idField;
            }
            set {
                this.pm_idField = value;
                this.RaisePropertyChanged("pm_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string pm_name {
            get {
                return this.pm_nameField;
            }
            set {
                this.pm_nameField = value;
                this.RaisePropertyChanged("pm_name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string pm_url {
            get {
                return this.pm_urlField;
            }
            set {
                this.pm_urlField = value;
                this.RaisePropertyChanged("pm_url");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public System.DateTime pm_createdtime {
            get {
                return this.pm_createdtimeField;
            }
            set {
                this.pm_createdtimeField = value;
                this.RaisePropertyChanged("pm_createdtime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public System.DateTime pm_updatetime {
            get {
                return this.pm_updatetimeField;
            }
            set {
                this.pm_updatetimeField = value;
                this.RaisePropertyChanged("pm_updatetime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public int pe_id {
            get {
                return this.pe_idField;
            }
            set {
                this.pe_idField = value;
                this.RaisePropertyChanged("pe_id");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SMC_PackagePicture : BaseEntry {
        
        private int pp_idField;
        
        private int pe_idField;
        
        private string pp_pathField;
        
        private System.DateTime pp_CreatedDateField;
        
        private string pp_descField;
        
        private string pp_titleField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int pp_id {
            get {
                return this.pp_idField;
            }
            set {
                this.pp_idField = value;
                this.RaisePropertyChanged("pp_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int pe_id {
            get {
                return this.pe_idField;
            }
            set {
                this.pe_idField = value;
                this.RaisePropertyChanged("pe_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string pp_path {
            get {
                return this.pp_pathField;
            }
            set {
                this.pp_pathField = value;
                this.RaisePropertyChanged("pp_path");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public System.DateTime pp_CreatedDate {
            get {
                return this.pp_CreatedDateField;
            }
            set {
                this.pp_CreatedDateField = value;
                this.RaisePropertyChanged("pp_CreatedDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string pp_desc {
            get {
                return this.pp_descField;
            }
            set {
                this.pp_descField = value;
                this.RaisePropertyChanged("pp_desc");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string pp_title {
            get {
                return this.pp_titleField;
            }
            set {
                this.pp_titleField = value;
                this.RaisePropertyChanged("pp_title");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SMC_User : BaseEntry {
        
        private int u_IDField;
        
        private string u_UIDField;
        
        private string u_NAMEField;
        
        private string u_UNITCODEField;
        
        private string u_PASSWORDField;
        
        private System.DateTime u_CREATEDDATEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int U_ID {
            get {
                return this.u_IDField;
            }
            set {
                this.u_IDField = value;
                this.RaisePropertyChanged("U_ID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string U_UID {
            get {
                return this.u_UIDField;
            }
            set {
                this.u_UIDField = value;
                this.RaisePropertyChanged("U_UID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string U_NAME {
            get {
                return this.u_NAMEField;
            }
            set {
                this.u_NAMEField = value;
                this.RaisePropertyChanged("U_NAME");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string U_UNITCODE {
            get {
                return this.u_UNITCODEField;
            }
            set {
                this.u_UNITCODEField = value;
                this.RaisePropertyChanged("U_UNITCODE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string U_PASSWORD {
            get {
                return this.u_PASSWORDField;
            }
            set {
                this.u_PASSWORDField = value;
                this.RaisePropertyChanged("U_PASSWORD");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public System.DateTime U_CREATEDDATE {
            get {
                return this.u_CREATEDDATEField;
            }
            set {
                this.u_CREATEDDATEField = value;
                this.RaisePropertyChanged("U_CREATEDDATE");
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServiceSoapChannel : SyncFaqConsole.ApplicationCenterWS.WebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceSoapClient : System.ServiceModel.ClientBase<SyncFaqConsole.ApplicationCenterWS.WebServiceSoap>, SyncFaqConsole.ApplicationCenterWS.WebServiceSoap {
        
        public WebServiceSoapClient() {
        }
        
        public WebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool SMC_UnitSync(SMC_Unit[] units) {
            return base.Channel.SMC_UnitSync(units);
        }
        
        public bool SMC_UserSync(SMC_User[] users) {
            return base.Channel.SMC_UserSync(users);
        }
        
        public bool PackagePictuerSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackagePicture entity) {
            return base.Channel.PackagePictuerSync(entity);
        }
        
        public bool PackageManualSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackageManual entity) {
            return base.Channel.PackageManualSync(entity);
        }
        
        public bool PackageFAQSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackageFAQ faq) {
            return base.Channel.PackageFAQSync(faq);
        }
        
        public SMC_PackageFAQ[] GetNeedSyncToInsideFAQ() {
            return base.Channel.GetNeedSyncToInsideFAQ();
        }
        
        public bool PackageExtSync(SyncFaqConsole.ApplicationCenterWS.SMC_PackageExt entity) {
            return base.Channel.PackageExtSync(entity);
        }
        
        public bool PackageFilesSync(FileEntity[] files, int pe_id) {
            return base.Channel.PackageFilesSync(files, pe_id);
        }
    }
}
