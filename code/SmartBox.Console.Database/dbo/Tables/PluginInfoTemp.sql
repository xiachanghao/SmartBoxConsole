CREATE TABLE [dbo].[PluginInfoTemp] (
    [PluginCode]      VARCHAR (64)    NOT NULL,
    [PluginCateCode]  VARCHAR (64)    NULL,
    [HashCode]        VARCHAR (64)    NULL,
    [Version]         VARCHAR (32)    NULL,
    [VersionSummary]  TEXT            NULL,
    [DisplayName]     NVARCHAR (256)  NULL,
    [DirectoryName]   NVARCHAR (256)  NULL,
    [TypeFullName]    VARCHAR (512)   NULL,
    [FileName]        VARCHAR (256)   NULL,
    [IsNeed]          BIT             NULL,
    [IsDefault]       BIT             NULL,
    [IsUse]           BIT             NULL,
    [isPublic]        BIT             DEFAULT ((1)) NULL,
    [AppCode]         VARCHAR (64)    NULL,
    [PrivilegeCode]   VARCHAR (64)    NULL,
    [IsNew]           BIT             NULL,
    [PreVersionPCs]   NVARCHAR (1024) NULL,
    [Sequence]        INT             NULL,
    [PluginSummary]   TEXT            NULL,
    [PluginUrl]       VARCHAR (1024)  NULL,
    [CompanyName]     NVARCHAR (1024) NULL,
    [CompanyTel]      VARCHAR (64)    NULL,
    [CompanyLinkman]  VARCHAR (64)    NULL,
    [CompanyHomePage] VARCHAR (1024)  NULL,
    [ActionCode]      VARCHAR (64)    NULL,
    [ActionSummary]   TEXT            NULL,
    [IsIgnoreConfig]  BIT             NULL,
    CONSTRAINT [PK_PLUGININFOTEMP] PRIMARY KEY CLUSTERED ([PluginCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'PluginCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件分类标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'PluginCateCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'哈希码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'HashCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前版本号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'版本说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'VersionSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件显示名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存放目录名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'DirectoryName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'属性表示插件实例化的类型全名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'TypeFullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件入口程序文件名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'FileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否必须，参见PluginInfo表中该字段描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'IsNeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否默认，参见PluginInfo表中该字段描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'IsDefault';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否使用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'IsUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用系统标识，若IsPublic为false，则需要验证此应用系统标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'AppCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限标识，若IsPublic为false，则需要验证此权限标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'PrivilegeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否是最新版本', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'IsNew';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上一个版本的插件codes用_$&_分割', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'PreVersionPCs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'Sequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'PluginSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'web插件Url地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'PluginUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'CompanyTel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司联系人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'CompanyLinkman';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司网站首页地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'CompanyHomePage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'ActionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'ActionSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'记录是否忽略配置', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfoTemp', @level2type = N'COLUMN', @level2name = N'IsIgnoreConfig';

