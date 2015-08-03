CREATE TABLE [dbo].[UserInfoRef] (
    [UserUid]  VARCHAR (64)  NOT NULL,
    [UserName] NVARCHAR (64) NULL,
    [OrgCode]  VARCHAR (64)  NULL,
    [OrgName]  NVARCHAR (64) NULL,
    [UnitCode] VARCHAR (64)  NULL,
    [UnitName] NVARCHAR (64) NULL,
    CONSTRAINT [PK_USERINFOREF] PRIMARY KEY CLUSTERED ([UserUid] ASC),
    CONSTRAINT [FK_USERINFOREF_USERINFO] FOREIGN KEY ([UserUid]) REFERENCES [dbo].[UserInfo] ([UserUId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户关联表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfoRef';

