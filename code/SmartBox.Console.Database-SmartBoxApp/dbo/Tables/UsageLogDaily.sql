CREATE TABLE [dbo].[UsageLogDaily] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [AppName]    VARCHAR (128)  NOT NULL,
    [AppTitle]   NVARCHAR (512) NULL,
    [UserUid]    VARCHAR (64)   NOT NULL,
    [UserName]   NVARCHAR (64)  NULL,
    [OrgCode]    VARCHAR (64)   NULL,
    [OrgName]    NVARCHAR (64)  NULL,
    [UnitCode]   VARCHAR (64)   NULL,
    [UnitName]   NVARCHAR (64)  NULL,
    [Device]     VARCHAR (32)   NULL,
    [DeviceId]   VARCHAR (64)   NULL,
    [OpTime]     DATETIME       NOT NULL,
    [UsageCount] INT            NOT NULL,
    CONSTRAINT [PK_USAGELOGDAILY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'app每天的使用统计', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'自增标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily', @level2type = N'COLUMN', @level2name = N'AppName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily', @level2type = N'COLUMN', @level2name = N'UserUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户显示名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily', @level2type = N'COLUMN', @level2name = N'Device';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily', @level2type = N'COLUMN', @level2name = N'DeviceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户打开应该的时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UsageLogDaily', @level2type = N'COLUMN', @level2name = N'OpTime';

