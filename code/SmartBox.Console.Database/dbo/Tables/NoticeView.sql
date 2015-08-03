CREATE TABLE [dbo].[NoticeView] (
    [NoticeId] UNIQUEIDENTIFIER NOT NULL,
    [UserUid]  VARCHAR (64)     NOT NULL,
    [ViewTime] DATETIME         NULL,
    CONSTRAINT [PK_NOTICEVIEW] PRIMARY KEY CLUSTERED ([NoticeId] ASC, [UserUid] ASC),
    CONSTRAINT [FK_NOTICEVI_REFERENCE_NOTICE] FOREIGN KEY ([NoticeId]) REFERENCES [dbo].[Notice] ([NoticeId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知通告查看记录', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NoticeView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NoticeView', @level2type = N'COLUMN', @level2name = N'NoticeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NoticeView', @level2type = N'COLUMN', @level2name = N'UserUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'浏览时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NoticeView', @level2type = N'COLUMN', @level2name = N'ViewTime';

