CREATE TABLE [dbo].[SMC_Role] (
    [Role_ID]          INT           NOT NULL,
    [Role_Name]        VARCHAR (200) NOT NULL,
    [Unit_ID]          VARCHAR (300) NULL,
    [Role_Demo]        TEXT          NULL,
    [Role_CreatedTime] DATETIME      NULL,
    [Role_CreatedUser] VARCHAR (50)  NULL,
    [Role_UpdateTime]  DATETIME      NULL,
    [Role_UpdateUser]  VARCHAR (50)  NULL,
    [Role_Sequence]    INT           NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Role_ID] ASC) WITH (FILLFACTOR = 90)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属单位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Unit_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_Demo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_CreatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_CreatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_UpdateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_UpdateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Role', @level2type = N'COLUMN', @level2name = N'Role_Sequence';

