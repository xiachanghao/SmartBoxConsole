CREATE TABLE [dbo].[UserInfo] (
    [UserUId]        VARCHAR (64) NOT NULL,
    [Signature]      TEXT         NULL,
    [UserIconCode]   VARCHAR (64) NULL,
    [Status]         VARCHAR (16) NULL,
    [Lock]           BIT          CONSTRAINT [DF_UserInfo_Lock] DEFAULT ((0)) NOT NULL,
    [LastLockTime]   DATETIME     NULL,
    [LastUnLockTime] DATETIME     NULL,
    [LastLoginIP]    VARCHAR (32) NULL,
    [LastLoginTime]  DATETIME     NULL,
    [LastLogoutTime] DATETIME     NULL,
    CONSTRAINT [PK_USERINFO] PRIMARY KEY CLUSTERED ([UserUId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户基本信息表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo', @level2type = N'COLUMN', @level2name = N'UserUId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户签名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo', @level2type = N'COLUMN', @level2name = N'Signature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前使用的头像标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo', @level2type = N'COLUMN', @level2name = N'UserIconCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态 online/offline', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后登录IP地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo', @level2type = N'COLUMN', @level2name = N'LastLoginIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后登录时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo', @level2type = N'COLUMN', @level2name = N'LastLoginTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后退出时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserInfo', @level2type = N'COLUMN', @level2name = N'LastLogoutTime';

