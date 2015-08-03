CREATE TABLE [dbo].[DeviceUserApply] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [DeviceUserID] INT            NOT NULL,
    [Status]       INT            NOT NULL,
    [ApplyTime]    DATETIME       NOT NULL,
    [CheckUid]     NVARCHAR (64)  NULL,
    [CheckTime]    DATETIME       NULL,
    [Remark]       NVARCHAR (512) NULL,
    CONSTRAINT [PK_DeviceUserApply] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0：申请中；1：已审核通过；2：已审核不通过；3已审核不通过并禁用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DeviceUserApply', @level2type = N'COLUMN', @level2name = N'Status';

