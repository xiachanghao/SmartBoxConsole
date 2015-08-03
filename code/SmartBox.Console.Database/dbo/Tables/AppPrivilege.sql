CREATE TABLE [dbo].[AppPrivilege] (
    [ID]               INT            IDENTITY (100000, 1) NOT NULL,
    [Name]             NVARCHAR (64)  NOT NULL,
    [DisplayName]      NVARCHAR (128) NOT NULL,
    [BuaAppCode]       NVARCHAR (128) NOT NULL,
    [BuaPrivilegeCode] NVARCHAR (128) NOT NULL,
    [EnableSync]       BIT            NOT NULL,
    [SyncIntervalTime] INT            NOT NULL,
    [SyncLastTime]     DATETIME       NOT NULL,
    [SyncTime]         DATETIME       NOT NULL,
    [CreateUid]        NVARCHAR (64)  NOT NULL,
    [CreateTime]       DATETIME       NOT NULL,
    [UpdateUid]        NVARCHAR (64)  NOT NULL,
    [UpdateTime]       DATETIME       NOT NULL,
    CONSTRAINT [PK_APPPRIVILEGE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用的权限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限的显示名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'对应的Bua的应用系统标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'BuaAppCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'对应的Bua的权限标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'BuaPrivilegeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'启用同步', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'EnableSync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'同步的间隔时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'SyncIntervalTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上次同步时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'SyncLastTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'同步开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'SyncTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AppPrivilege', @level2type = N'COLUMN', @level2name = N'UpdateTime';

