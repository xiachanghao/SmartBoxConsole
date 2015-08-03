CREATE TABLE [dbo].[FriendGroup] (
    [FriendGroupId] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [GroupName]     NVARCHAR (64) NULL,
    [OwnerUid]      NVARCHAR (64) NULL,
    [GroupType]     NVARCHAR (32) NULL,
    [Sequence]      INT           NULL,
    CONSTRAINT [PK_FRIENDGROUP] PRIMARY KEY CLUSTERED ([FriendGroupId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友组', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FriendGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友组id 自增长主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FriendGroup', @level2type = N'COLUMN', @level2name = N'FriendGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友组名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FriendGroup', @level2type = N'COLUMN', @level2name = N'GroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友组所属用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FriendGroup', @level2type = N'COLUMN', @level2name = N'OwnerUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'好友组类型，目前有默认组和用户增加组两类（Default，UserAdd）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FriendGroup', @level2type = N'COLUMN', @level2name = N'GroupType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'组顺序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FriendGroup', @level2type = N'COLUMN', @level2name = N'Sequence';

