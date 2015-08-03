CREATE TABLE [dbo].[FileStore] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [DisplayName] NVARCHAR (256)   NOT NULL,
    [DataId]      NVARCHAR (64)    NOT NULL,
    [From]        NVARCHAR (64)    NOT NULL,
    [To]          NVARCHAR (128)   NOT NULL,
    [Size]        BIGINT           CONSTRAINT [DF__OfflineFil__Size__3A81B327] DEFAULT ((0)) NOT NULL,
    [CreateUid]   NVARCHAR (64)    NOT NULL,
    [CreateTime]  DATETIME         NOT NULL,
    [UpdateUid]   NVARCHAR (64)    NOT NULL,
    [UpdateTime]  DATETIME         NOT NULL,
    CONSTRAINT [PK_FileStore] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'离线文件存储表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FileStore';

