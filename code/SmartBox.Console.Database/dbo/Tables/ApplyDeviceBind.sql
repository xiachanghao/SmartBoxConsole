CREATE TABLE [dbo].[ApplyDeviceBind] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserUid]     VARCHAR (64)   NOT NULL,
    [DeviceId]    VARCHAR (64)   NOT NULL,
    [Ip]          VARCHAR (32)   NULL,
    [Status]      INT            NOT NULL,
    [ApplyTime]   DATETIME       NULL,
    [CheckTime]   DATETIME       NULL,
    [CheckUser]   VARCHAR (64)   NULL,
    [Description] NVARCHAR (256) NULL,
    CONSTRAINT [PK_APPLYDEVICEBIND] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'申请设备绑定', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标识，自增', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'UserUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'DeviceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'审核状态
   0:待审核
   1:审核通过
   2:审核不通过', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'申请时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'ApplyTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'审核时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'CheckTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'审核人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'CheckUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplyDeviceBind', @level2type = N'COLUMN', @level2name = N'Description';

