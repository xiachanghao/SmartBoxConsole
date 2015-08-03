CREATE TABLE [dbo].[Application] (
    [ID]          INT            IDENTITY (100000, 1) NOT NULL,
    [Name]        NVARCHAR (64)  NOT NULL,
    [DisplayName] NVARCHAR (128) NOT NULL,
    [Description] NVARCHAR (512) NULL,
    [Enable]      BIT            NOT NULL,
    [EnableType]  NVARCHAR (64)  NULL,
    [PrivilegeID] INT            NULL,
    [CategoryIDs] NVARCHAR (128) NULL,
    [CreateUid]   NVARCHAR (64)  NOT NULL,
    [CreateTime]  DATETIME       NOT NULL,
    [UpdateUid]   NVARCHAR (64)  NOT NULL,
    [UpdateTime]  DATETIME       NOT NULL,
    [Seq]         INT            CONSTRAINT [DF_Application_Seq] DEFAULT ((1)) NOT NULL,
    [Unit]        VARCHAR (300)  NULL,
    CONSTRAINT [PK_APPLICATION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CKC_Application_EnableType] CHECK ([EnableType] IS NULL OR ([EnableType]='DefaultFalse' OR [EnableType]='DefaultTrue' OR [EnableType]='Need')),
    CONSTRAINT [FK_AppPrivilege_Application] FOREIGN KEY ([PrivilegeID]) REFERENCES [dbo].[AppPrivilege] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DisplayName]
    ON [dbo].[Application]([DisplayName] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Application]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrivilegeID]
    ON [dbo].[Application]([PrivilegeID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用系统的启用，启用方式等设置
   应用系统只单一功能的系统（如短信，邮件，即时通讯），而不是指某一个实现模块
   （例如，PC客户端的一个应用可能由多个插件构成，也有可能一个插件中包含了两个应用的更能）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application的名称（标识）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application的显示名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application的描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application是否启用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'Enable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application的启用模式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'EnableType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application的启用模式为权限绑定的方式，权限的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'PrivilegeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Application', @level2type = N'COLUMN', @level2name = N'UpdateTime';

