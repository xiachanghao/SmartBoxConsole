CREATE TABLE [dbo].[DeviceBind] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [UserUid]     VARCHAR (64)     NOT NULL,
    [Device]      VARCHAR (32)     NOT NULL,
    [DeviceId]    VARCHAR (64)     NULL,
    [Status]      VARCHAR (32)     NULL,
    [Description] NVARCHAR (256)   NULL,
    CONSTRAINT [PK_DEVICEBIND] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户设备绑定', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DeviceBind';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态 
   ENABLE 启用
   DISABLE 禁用
   LOST 遗失', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DeviceBind', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DeviceBind', @level2type = N'COLUMN', @level2name = N'Description';

