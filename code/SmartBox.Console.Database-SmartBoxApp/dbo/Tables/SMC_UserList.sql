CREATE TABLE [dbo].[SMC_UserList] (
    [UL_ID]          INT           NOT NULL,
    [Role_ID]        INT           NULL,
    [UL_UID]         VARCHAR (50)  NOT NULL,
    [Unit_ID]        VARCHAR (300) NULL,
    [UL_PWD]         VARCHAR (50)  NOT NULL,
    [UL_Name]        VARCHAR (50)  NULL,
    [UL_Demo]        TEXT          NULL,
    [UL_MobilePhone] VARCHAR (50)  NULL,
    [UL_MailAddress] VARCHAR (500) NULL,
    [UL_CreatedTime] DATETIME      NULL,
    [UL_CreatedUser] VARCHAR (50)  NULL,
    [UL_UpdateTime]  DATETIME      NULL,
    [UL_UpdateUser]  VARCHAR (50)  NULL,
    [UL_Sequence]    INT           NULL,
    [UL_Gender]      VARCHAR (10)  NULL,
    [UL_IsMain]      BIT           CONSTRAINT [DF_SMC_UserList_UL_IsMain] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_UserList] PRIMARY KEY CLUSTERED ([UL_ID] ASC) WITH (FILLFACTOR = 90)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'Role_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登录名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_UID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'Unit_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_PWD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户全名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_Demo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'手机号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_MobilePhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮件地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_MailAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_CreatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_CreatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_UpdateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_UpdateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_Sequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否是主管理员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_UserList', @level2type = N'COLUMN', @level2name = N'UL_IsMain';

