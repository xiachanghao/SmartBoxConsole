CREATE TABLE [dbo].[LogInfo] (
    [LogId]   NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [Msg]     TEXT           NULL,
    [UserUid] NVARCHAR (128) NULL,
    [Time]    DATETIME       NULL,
    [Type]    NVARCHAR (16)  DEFAULT ('INFO') NULL,
    [Ip]      NVARCHAR (32)  NULL,
    CONSTRAINT [PK_LOGINFO] PRIMARY KEY CLUSTERED ([LogId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志类型，包括  INFO  EXCEPTION ERROR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogInfo', @level2type = N'COLUMN', @level2name = N'Type';

