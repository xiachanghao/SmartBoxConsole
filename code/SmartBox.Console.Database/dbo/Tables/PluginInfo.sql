CREATE TABLE [dbo].[PluginInfo] (
    [PluginCode]      VARCHAR (64)    NOT NULL,
    [PluginCateCode]  VARCHAR (64)    NULL,
    [HashCode]        VARCHAR (64)    NULL,
    [Version]         VARCHAR (32)    NULL,
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
    [isNew]           BIT             NULL,
    [Sequence]        INT             NULL,
    [Summary]         TEXT            NULL,
    [PluginUrl]       VARCHAR (1024)  NULL,
    [CompanyName]     NVARCHAR (1024) NULL,
    [CompanyTel]      VARCHAR (64)    NULL,
    [CompanyLinkman]  NVARCHAR (64)   NULL,
    [CompanyHomePage] VARCHAR (1024)  NULL,
    [CreateTime]      DATETIME        NULL,
    [CreateUid]       VARCHAR (64)    NULL,
    [LastModTime]     DATETIME        NULL,
    [LastModUid]      VARCHAR (64)    NULL,
    CONSTRAINT [PK_PLUGININFO] PRIMARY KEY CLUSTERED ([PluginCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'PluginCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件分类标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'PluginCateCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'哈希码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'HashCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件的最新版本名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件显示名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件存放的文件夹名称，也作为插件的标识，不能重复', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'DirectoryName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'属性表示插件实例化的类型全名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'TypeFullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件入口程序文件名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'FileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标识插件是否是必须插件，如果是必须插件，则所有用户使用SmartBoxTEST时都必须加载该插件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'IsNeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否是默认插件，用于标识该插件是否默认给所有用户使用，', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'IsDefault';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否可用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'IsUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用系统标识，若IsPublic为false，则需要验证此应用系统标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'AppCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限标识，若IsPublic为false，则需要验证此权限标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'PrivilegeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否有最新版本', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'isNew';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件默认排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'Sequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'Summary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'web插件的Url地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'PluginUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'CompanyTel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司联系人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'CompanyLinkman';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件开发公司网站首页地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'CompanyHomePage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件创建人标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件最后修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'LastModTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件最后修改人标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PluginInfo', @level2type = N'COLUMN', @level2name = N'LastModUid';

