CREATE TABLE [dbo].[MessageCache] (
    [MessageId]       NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [MessageBody]     TEXT         NULL,
    [Target]          TEXT         NULL,
    [MessageCategory] VARCHAR (32) DEFAULT ('Default') NULL,
    CONSTRAINT [PK_MESSAGECACHE] PRIMARY KEY CLUSTERED ([MessageId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'信息缓存表： 与其他系统交互时，通过webservice等方式将外部系统数据写入此表中；SmartBoxTESTserver 会定时扫描改表，并处理；', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MessageCache';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'自增长id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MessageCache', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'被序列化好的消息内容。SmartBoxTEST server 会直接将该消息内容发送到SmartBoxTEST 客户端，', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MessageCache', @level2type = N'COLUMN', @level2name = N'MessageBody';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标帐号，PUSH时，为NULL表示发送给所有人，发给多个人uid用英文逗号隔开；REQ时，只支持单个人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MessageCache', @level2type = N'COLUMN', @level2name = N'Target';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息分类，PUSH/REQ PUSH表示将数据推送至客户端，Target为要推送的目标；REQ表示模拟客户端请求（现只支持异步请求）,Target为模拟的客户端uid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MessageCache', @level2type = N'COLUMN', @level2name = N'MessageCategory';

