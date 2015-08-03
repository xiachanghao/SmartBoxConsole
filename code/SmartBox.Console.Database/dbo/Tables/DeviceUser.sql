CREATE TABLE [dbo].[DeviceUser] (
    [ID]             INT            IDENTITY (100000, 1) NOT NULL,
    [DeviceID]       NVARCHAR (256) NOT NULL,
    [UID]            NVARCHAR (64)  NOT NULL,
    [Status]         INT            NOT NULL,
    [NoUseReason]    INT            NULL,
    [LastUpdateUID]  NVARCHAR (64)  NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [PK_DeviceUser] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0:默认，1：可以使用，2：不可使用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DeviceUser', @level2type = N'COLUMN', @level2name = N'Status';

