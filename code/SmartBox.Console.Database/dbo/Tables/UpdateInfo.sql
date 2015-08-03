CREATE TABLE [dbo].[UpdateInfo] (
    [Device]    VARCHAR (32)  NOT NULL,
    [Version]   VARCHAR (64)  NOT NULL,
    [IsEnable]  BIT           NOT NULL,
    [Url]       VARCHAR (512) NULL,
    [LocalFile] VARCHAR (256) NULL,
    CONSTRAINT [PK_UPDATEINFO] PRIMARY KEY CLUSTERED ([Device] ASC, [Version] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UpdateInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备 ANDROID_PAD ANDROID_PHONE IPHONE IPAD 等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UpdateInfo', @level2type = N'COLUMN', @level2name = N'Device';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'版本标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UpdateInfo', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否启用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UpdateInfo', @level2type = N'COLUMN', @level2name = N'IsEnable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新文件的url', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UpdateInfo', @level2type = N'COLUMN', @level2name = N'Url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'本地文件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UpdateInfo', @level2type = N'COLUMN', @level2name = N'LocalFile';

