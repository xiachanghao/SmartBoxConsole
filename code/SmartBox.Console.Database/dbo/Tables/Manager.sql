CREATE TABLE [dbo].[Manager] (
    [UserUid]       VARCHAR (50) NOT NULL,
    [UserPwd]       VARCHAR (64) NULL,
    [IsMain]        BIT          CONSTRAINT [DF_Manager_IsMain] DEFAULT ((0)) NULL,
    [LastLoginTime] DATETIME     NULL,
    [ErrorCount]    INT          NULL,
    [Lock]          BIT          NULL,
    [LastLoginIP]   VARCHAR (64) NULL,
    [LastLockTime]  DATETIME     NULL,
    CONSTRAINT [PK_MANAGER] PRIMARY KEY CLUSTERED ([UserUid] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Web控制台非SSO方式登录时的身份认证表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Manager';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户登录名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Manager', @level2type = N'COLUMN', @level2name = N'UserUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Manager', @level2type = N'COLUMN', @level2name = N'UserPwd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否主管理员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Manager', @level2type = N'COLUMN', @level2name = N'IsMain';

