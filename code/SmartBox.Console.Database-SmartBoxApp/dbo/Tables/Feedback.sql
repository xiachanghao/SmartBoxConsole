CREATE TABLE [dbo].[Feedback] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserUID]    VARCHAR (126)  NOT NULL,
    [UserName]   NVARCHAR (126) NULL,
    [OrgCode]    VARCHAR (64)   NULL,
    [OrgName]    NVARCHAR (64)  NULL,
    [UnitCode]   VARCHAR (64)   NULL,
    [UnitName]   NVARCHAR (64)  NULL,
    [Content]    TEXT           NOT NULL,
    [SubmitTime] DATETIME       NOT NULL,
    [Device]     VARCHAR (32)   NOT NULL,
    [DeviceId]   VARCHAR (64)   NULL,
    CONSTRAINT [PK_FEEDBACK] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'意见内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Feedback', @level2type = N'COLUMN', @level2name = N'Content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提交时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Feedback', @level2type = N'COLUMN', @level2name = N'SubmitTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Feedback', @level2type = N'COLUMN', @level2name = N'Device';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设备号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Feedback', @level2type = N'COLUMN', @level2name = N'DeviceId';

