CREATE TABLE [dbo].[PrivilegeUser] (
    [ID]         INT           NOT NULL,
    [Uid]        NVARCHAR (64) NOT NULL,
    [CreateUid]  NVARCHAR (64) NOT NULL,
    [CreateTime] DATETIME      NOT NULL,
    [UpdateUid]  NVARCHAR (64) NOT NULL,
    [UpdateTime] DATETIME      NOT NULL,
    CONSTRAINT [PK_PRIVILEGEUSER] PRIMARY KEY CLUSTERED ([ID] ASC, [Uid] ASC),
    CONSTRAINT [FK_AppPrivilege_PrivilegeUser] FOREIGN KEY ([ID]) REFERENCES [dbo].[AppPrivilege] ([ID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限对应的用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PrivilegeUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PrivilegeUser', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限对应的用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PrivilegeUser', @level2type = N'COLUMN', @level2name = N'Uid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PrivilegeUser', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PrivilegeUser', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PrivilegeUser', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PrivilegeUser', @level2type = N'COLUMN', @level2name = N'UpdateTime';

