CREATE TABLE [dbo].[VersionTrack] (
    [VersionId]      NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [PluginCode]     VARCHAR (64)   NOT NULL,
    [VersionName]    NVARCHAR (128) NULL,
    [PreVersionId]   INT            NULL,
    [FilePath]       NVARCHAR (512) NULL,
    [VersionStatus]  INT            NULL,
    [VersionSummary] TEXT           NULL,
    [CreateUid]      NVARCHAR (64)  NULL,
    [CreateTime]     DATETIME       NULL,
    [LastModUid]     VARCHAR (64)   NULL,
    [LastModTime]    DATETIME       NULL,
    CONSTRAINT [PK_VERSIONTRACK] PRIMARY KEY CLUSTERED ([VersionId] ASC),
    CONSTRAINT [CKC_VERSIONID_VERSIONT] CHECK ([VersionId]>=(0))
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标识版本主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'VersionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'插件标识，如果是SmartBoxTEST主程序则为 SmartBoxTEST', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'PluginCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户输入的版本号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'VersionName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上一个版本号，如果是第一个版本 与VersionId相同', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'PreVersionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物理文件存放的绝对路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'-1 新增未发布 0升级未发布 1正在使用,2已过期; 标当前版本是不是正在使用的，每个插件和主程序只有一个版本是当前使用的', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'VersionStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'版本的描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'VersionSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后修改人标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'LastModUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VersionTrack', @level2type = N'COLUMN', @level2name = N'LastModTime';

