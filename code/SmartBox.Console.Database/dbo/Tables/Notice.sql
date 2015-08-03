CREATE TABLE [dbo].[Notice] (
    [NoticeId]    UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (1024)  NULL,
    [PublicScope] CHAR (2)         NOT NULL,
    [Content]     TEXT             NULL,
    [PublishUid]  VARCHAR (64)     NOT NULL,
    [PublishTime] DATETIME         NOT NULL,
    [BeginTime]   DATETIME         NULL,
    [ExpireTime]  DATETIME         NULL,
    CONSTRAINT [PK_NOTICE] PRIMARY KEY CLUSTERED ([NoticeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知通告表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知通告标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'NoticeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知通告标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公布范围，01所有用户，02当前在线用户，03特定用户
   发给所有用户时暂不记录查看状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'PublicScope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知通告内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'Content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布人标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'PublishUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'PublishTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效期开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'BeginTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效期结束时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Notice', @level2type = N'COLUMN', @level2name = N'ExpireTime';

