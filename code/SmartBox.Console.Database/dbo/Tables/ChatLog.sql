CREATE TABLE [dbo].[ChatLog] (
    [ChatLogId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [ChatId]         NVARCHAR (64)   NOT NULL,
    [ChatTitle]      NVARCHAR (64)   NULL,
    [ChatType]       NVARCHAR (64)   NOT NULL,
    [SenderUid]      NVARCHAR (64)   NOT NULL,
    [SenderName]     NVARCHAR (64)   NULL,
    [SendTime]       DATETIME        NOT NULL,
    [SearchContent]  NVARCHAR (4000) NOT NULL,
    [MsgType]        NVARCHAR (64)   NOT NULL,
    [SessionId]      NVARCHAR (256)  NULL,
    [MsgContent]     NVARCHAR (4000) NOT NULL,
    [CreateUser]     NVARCHAR (64)   NOT NULL,
    [CreateDatetime] DATETIME        NOT NULL,
    [TerminalCode]   NVARCHAR (64)   NOT NULL,
    [TerminalLogId]  INT             NOT NULL,
    CONSTRAINT [PK_SingleChatLog] PRIMARY KEY CLUSTERED ([ChatLogId] ASC)
);

