CREATE TABLE [dbo].[Camera] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (512) NOT NULL,
    [Category]      NVARCHAR (256) NOT NULL,
    [Longitude]     FLOAT (53)     NOT NULL,
    [Latitude]      FLOAT (53)     NOT NULL,
    [Username]      VARCHAR (128)  NULL,
    [Password]      VARCHAR (128)  NULL,
    [VideoProtocol] VARCHAR (256)  NOT NULL,
    [VideoUrl]      TEXT           NOT NULL,
    [CtrlProtocol]  VARCHAR (256)  NOT NULL,
    [CtrlUrl]       TEXT           NOT NULL,
    CONSTRAINT [PK_CAMERA] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'摄像头列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'摄像头标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'摄像头名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'摄像头分类', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'Category';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'Longitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'Latitude';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'摄像头登录用户名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'Username';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'摄像头登录密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'私有协议', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'VideoProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'摄像头地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Camera', @level2type = N'COLUMN', @level2name = N'VideoUrl';

