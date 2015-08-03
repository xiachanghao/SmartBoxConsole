CREATE TABLE [dbo].[Icon] (
    [IconCode]   VARCHAR (64) NOT NULL,
    [IconType]   VARCHAR (64) DEFAULT ('CustomHeadIcon') NULL,
    [UserUid]    VARCHAR (64) NULL,
    [CreateTime] DATETIME     NULL,
    CONSTRAINT [PK_ICON] PRIMARY KEY CLUSTERED ([IconCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，文件名，guid标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Icon', @level2type = N'COLUMN', @level2name = N'IconCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'类型分类，暂时包括默认头像Default，用户上传头像Custom', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Icon', @level2type = N'COLUMN', @level2name = N'IconType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Icon', @level2type = N'COLUMN', @level2name = N'UserUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上传时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Icon', @level2type = N'COLUMN', @level2name = N'CreateTime';

