CREATE TABLE [dbo].[UsageLogTemp] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [AppName]  VARCHAR (128) NOT NULL,
    [UserUid]  VARCHAR (64)  NOT NULL,
    [Device]   VARCHAR (32)  NULL,
    [DeviceId] VARCHAR (64)  NULL,
    [OpTime]   DATETIME      NOT NULL,
    CONSTRAINT [PK_USAGELOGTEMP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'app使用统计临时表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogTemp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'自增标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogTemp', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogTemp', @level2type = N'COLUMN', @level2name = N'AppName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogTemp', @level2type = N'COLUMN', @level2name = N'UserUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogTemp', @level2type = N'COLUMN', @level2name = N'Device';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogTemp', @level2type = N'COLUMN', @level2name = N'DeviceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户打开应该的时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogTemp', @level2type = N'COLUMN', @level2name = N'OpTime';

