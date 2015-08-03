CREATE TABLE [dbo].[Device] (
    [ID]              NVARCHAR (256) NOT NULL,
    [Status]          INT            NOT NULL,
    [OS]              NVARCHAR (256) NOT NULL,
    [Model]           NVARCHAR (256) NOT NULL,
    [Description]     NVARCHAR (512) NULL,
    [Remark]          NVARCHAR (512) NULL,
    [LockTime]        DATETIME       NULL,
    [LockExpireHours] INT            CONSTRAINT [DF_Device_LockExpireHours] DEFAULT ((-1)) NULL,
    [UnLockTime]      DATETIME       NULL,
    [LostTime]        DATETIME       NULL,
    [UnLostTime]      DATETIME       NULL,
    CONSTRAINT [PK_Device] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0:默认，1：锁定，2：挂失', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Device', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'锁定时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Device', @level2type = N'COLUMN', @level2name = N'LockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备锁定过期小时数，单位小时', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Device', @level2type = N'COLUMN', @level2name = N'LockExpireHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'解除锁定时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Device', @level2type = N'COLUMN', @level2name = N'UnLockTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'挂失时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Device', @level2type = N'COLUMN', @level2name = N'LostTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'解除挂失时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Device', @level2type = N'COLUMN', @level2name = N'UnLostTime';

