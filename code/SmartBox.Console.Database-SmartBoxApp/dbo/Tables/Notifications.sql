CREATE TABLE [dbo].[Notifications] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [AppName]    NVARCHAR (64)  NOT NULL,
    [Total]      INT            NULL,
    [Title]      NVARCHAR (256) NULL,
    [Data]       TEXT           NULL,
    [Receive]    NVARCHAR (64)  NOT NULL,
    [CreateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_NOTIFICATIONS] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'推送通知', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notifications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Application中的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'AppName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示数字
   Null：保持不变
   0：不显示数字
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Json数据', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'Data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接收者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'Receive';

