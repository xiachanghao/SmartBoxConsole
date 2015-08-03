CREATE TABLE [dbo].[UserLoginInfo] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [UserUid]    VARCHAR (64)     NOT NULL,
    [Device]     VARCHAR (32)     NULL,
    [DeviceId]   VARCHAR (64)     NULL,
    [Ip]         VARCHAR (32)     NULL,
    [LoginTime]  DATETIME         NULL,
    [LogoutTime] DATETIME         NULL,
    [Result]     VARCHAR (16)     NULL,
    CONSTRAINT [PK_USERLOGININFO] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户登录信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserLoginInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户标识', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserLoginInfo', @level2type = N'COLUMN', @level2name = N'UserUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登录结果 
   成功 SUCCESS
   禁用 DISABLE
   遗失 LOST
   密码错误 PASS_ERROR
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserLoginInfo', @level2type = N'COLUMN', @level2name = N'Result';

