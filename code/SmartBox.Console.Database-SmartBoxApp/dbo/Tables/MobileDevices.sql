CREATE TABLE [dbo].[MobileDevices] (
    [ID]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [DeviceToken]      VARCHAR (32)  NULL,
    [MacAddr]          VARCHAR (20)  NULL,
    [UniqueIdentifier] VARCHAR (300) NULL,
    CONSTRAINT [PK_MOBILEDEVICES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任何形式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MobileDevices', @level2type = N'COLUMN', @level2name = N'UniqueIdentifier';

