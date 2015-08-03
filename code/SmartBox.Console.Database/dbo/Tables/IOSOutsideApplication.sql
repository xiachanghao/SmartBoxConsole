CREATE TABLE [dbo].[IOSOutsideApplication] (
    [ID]         INT            IDENTITY (100000, 1) NOT NULL,
    [AppID]      INT            NOT NULL,
    [Uri]        NVARCHAR (128) NOT NULL,
    [Scheme]     NVARCHAR (64)  NOT NULL,
    [ClientType] NVARCHAR (64)  NOT NULL,
    [Seq]        INT            NOT NULL,
    [IconUri]    NVARCHAR (128) NULL,
    [CreateUid]  NVARCHAR (64)  NOT NULL,
    [CreateTime] DATETIME       NOT NULL,
    [UpdateUid]  NVARCHAR (64)  NOT NULL,
    [UpdateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_IOSOUTSIDEAPPLICATION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IOSOUTSI_FK_APPLIC_APPLICAT] FOREIGN KEY ([AppID]) REFERENCES [dbo].[Application] ([ID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IOS外部应用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IOS外部应用ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'AppID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用安装地址Uri
   可以是AppStore的iTunes地址
   也可以是其他任意安装页面', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'Uri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IOS打开外部应用时需要的Scheme', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'Scheme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图标地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'IconUri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IOSOutsideApplication', @level2type = N'COLUMN', @level2name = N'UpdateTime';

