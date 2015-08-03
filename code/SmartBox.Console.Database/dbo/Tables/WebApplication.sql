CREATE TABLE [dbo].[WebApplication] (
    [ID]         INT            IDENTITY (100000, 1) NOT NULL,
    [AppID]      INT            NOT NULL,
    [Uri]        NVARCHAR (128) NOT NULL,
    [ShortName]  NVARCHAR (64)  NOT NULL,
    [ClientType] NVARCHAR (64)  NOT NULL,
    [Seq]        INT            NOT NULL,
    [IconUri]    NVARCHAR (128) NULL,
    [CreateUid]  NVARCHAR (64)  NOT NULL,
    [CreateTime] DATETIME       NOT NULL,
    [UpdateUid]  NVARCHAR (64)  NOT NULL,
    [UpdateTime] DATETIME       NOT NULL,
    [Unit]       VARCHAR (300)  NULL,
    CONSTRAINT [PK_WEBAPPLICATION] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Web形式的应用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Web应用ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'AppID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用ID地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'Uri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图标地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'IconUri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebApplication', @level2type = N'COLUMN', @level2name = N'UpdateTime';

