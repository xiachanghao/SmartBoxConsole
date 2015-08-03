CREATE TABLE [dbo].[App4AI] (
    [ID]           INT            IDENTITY (100000, 1) NOT NULL,
    [Package4AIID] INT            NOT NULL,
    [AppID]        INT            NULL,
    [AppCode]      NVARCHAR (128) NULL,
    [ClientType]   NVARCHAR (64)  NOT NULL,
    [IconUri]      NVARCHAR (128) NULL,
    [Seq]          INT            NOT NULL,
    [CreateUid]    NVARCHAR (64)  NOT NULL,
    [CreateTime]   DATETIME       NOT NULL,
    [UpdateUid]    NVARCHAR (64)  NOT NULL,
    [UpdateTime]   DATETIME       NOT NULL,
    CONSTRAINT [PK_APP4AI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Application_AppForAndroid] FOREIGN KEY ([AppID]) REFERENCES [dbo].[Application] ([ID]),
    CONSTRAINT [FK_Package_AppForAndroid] FOREIGN KEY ([Package4AIID]) REFERENCES [dbo].[Package4AI] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'针对Android和iOS的Application扩展', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'App4Android的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Android的Package的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'Package4AIID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'AppID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接入端类型（用于区分硬件设备）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'ClientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图标地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'IconUri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'Seq';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App4AI', @level2type = N'COLUMN', @level2name = N'UpdateTime';

