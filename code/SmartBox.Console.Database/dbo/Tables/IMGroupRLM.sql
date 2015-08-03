CREATE TABLE [dbo].[IMGroupRLM] (
    [Uid]          NVARCHAR (64)   NOT NULL,
    [GroupId]      NVARCHAR (128)  NOT NULL,
    [SendUid]      NVARCHAR (64)   NOT NULL,
    [SendClientId] NVARCHAR (64)   NOT NULL,
    [Message]      NVARCHAR (4000) NOT NULL,
    [SendTime]     DATETIME        NOT NULL,
    CONSTRAINT [PK_IMGroupRLM] PRIMARY KEY CLUSTERED ([Uid] ASC, [GroupId] ASC),
    CONSTRAINT [FK_IMGroupRLM_IMGroupUser] FOREIGN KEY ([GroupId], [Uid]) REFERENCES [dbo].[IMGroupUser] ([GroupId], [Uid]) ON DELETE CASCADE
);

