CREATE TABLE [dbo].[Friend] (
    [FriendId]      NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [FriendGroupId] NUMERIC (18)  NULL,
    [FriendUid]     NVARCHAR (64) NULL,
    [OwnerUId]      NVARCHAR (64) NULL,
    [Sequence]      INT           NULL,
    CONSTRAINT [PK_FRIEND] PRIMARY KEY CLUSTERED ([FriendId] ASC),
    CONSTRAINT [FK_FRIEND_FRIENDGRO_FRIENDGR] FOREIGN KEY ([FriendGroupId]) REFERENCES [dbo].[FriendGroup] ([FriendGroupId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Friend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友标识，自增长主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Friend', @level2type = N'COLUMN', @level2name = N'FriendId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'组id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Friend', @level2type = N'COLUMN', @level2name = N'FriendGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友登录名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Friend', @level2type = N'COLUMN', @level2name = N'FriendUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友所属用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Friend', @level2type = N'COLUMN', @level2name = N'OwnerUId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友顺序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Friend', @level2type = N'COLUMN', @level2name = N'Sequence';

