CREATE TABLE [dbo].[FriendInviteCache] (
    [Source]  VARCHAR (64)   NOT NULL,
    [Dest]    VARCHAR (64)   NOT NULL,
    [Type]    VARCHAR (16)   NOT NULL,
    [Message] NVARCHAR (512) NULL
);

