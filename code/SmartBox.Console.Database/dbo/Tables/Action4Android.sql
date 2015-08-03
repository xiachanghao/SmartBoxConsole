CREATE TABLE [dbo].[Action4Android] (
    [Name]        NVARCHAR (128) NOT NULL,
    [App4AIID]    INT            NOT NULL,
    [DisplayName] NVARCHAR (128) NOT NULL,
    [ShortName]   NVARCHAR (64)  NOT NULL,
    [IsLaunch]    BIT            NOT NULL,
    [IconUri]     NVARCHAR (128) NULL,
    [Seq]         INT            NOT NULL,
    [CreateUid]   NVARCHAR (64)  NOT NULL,
    [CreateTime]  DATETIME       NOT NULL,
    [UpdateUid]   NVARCHAR (64)  NOT NULL,
    [UpdateTime]  DATETIME       NOT NULL,
    CONSTRAINT [PK_ACTION4ANDROID] PRIMARY KEY CLUSTERED ([Name] ASC),
    CONSTRAINT [FK_App4Android_Action4Android] FOREIGN KEY ([App4AIID]) REFERENCES [dbo].[App4AI] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Android打Action设置', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Action名，用于Action的调用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'App4AIID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否为启动Action', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'IsLaunch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'action的图标地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'IconUri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'Seq';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Action4Android', @level2type = N'COLUMN', @level2name = N'UpdateTime';

