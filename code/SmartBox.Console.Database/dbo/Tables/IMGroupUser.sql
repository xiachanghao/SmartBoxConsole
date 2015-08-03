CREATE TABLE [dbo].[IMGroupUser] (
    [GroupId]    NVARCHAR (128) NOT NULL,
    [Uid]        NVARCHAR (64)  NOT NULL,
    [Role]       NVARCHAR (64)  NOT NULL,
    [Status]     NVARCHAR (50)  CONSTRAINT [DF_IMGroupUser_Status] DEFAULT (N'Joined') NOT NULL,
    [CreateUid]  NVARCHAR (64)  NOT NULL,
    [CreateTime] DATETIME       NOT NULL,
    [UpdateUid]  NVARCHAR (64)  NOT NULL,
    [UpdateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_IMGroupUser] PRIMARY KEY CLUSTERED ([GroupId] ASC, [Uid] ASC),
    CONSTRAINT [FK_IMGroupUser_IMGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[IMGroup] ([GroupId]) ON DELETE CASCADE,
    CONSTRAINT [FK_IMGroupUser_IMGroupUserRole] FOREIGN KEY ([Role]) REFERENCES [dbo].[IMGroupUserRole] ([Role])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Joined,WaitToAudit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IMGroupUser', @level2type = N'COLUMN', @level2name = N'Status';

