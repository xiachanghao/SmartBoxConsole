CREATE TABLE [dbo].[SMC_FunctionRole] (
    [FR_ID]          INT          NOT NULL,
    [Role_ID]        INT          NOT NULL,
    [FN_ID]          INT          NULL,
    [FR_CreatedTime] DATETIME     NULL,
    [FR_CreatedUser] VARCHAR (50) NULL,
    [FR_UpdateTime]  DATETIME     NULL,
    [FR_UpdateUser]  VARCHAR (50) NULL,
    [FR_Sequence]    INT          NULL,
    CONSTRAINT [PK_SMC_FUNCTIONROLE] PRIMARY KEY CLUSTERED ([FR_ID] ASC),
    CONSTRAINT [FK_FunctionRole_Role] FOREIGN KEY ([Role_ID]) REFERENCES [dbo].[SMC_Role] ([Role_ID]) NOT FOR REPLICATION
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FR_ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'FR_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'Role_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'FN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'FR_CreatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'FR_CreatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'FR_UpdateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'FR_UpdateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_FunctionRole', @level2type = N'COLUMN', @level2name = N'FR_Sequence';

