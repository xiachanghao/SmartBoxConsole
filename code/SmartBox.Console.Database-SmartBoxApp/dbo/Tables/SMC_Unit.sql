CREATE TABLE [dbo].[SMC_Unit] (
    [Unit_ID]          VARCHAR (300) NOT NULL,
    [Unit_Name]        VARCHAR (200) NULL,
    [Upper_Unit_ID]    VARCHAR (300) NULL,
    [Unit_Demo]        TEXT          NULL,
    [Unit_Path]        VARCHAR (500) NULL,
    [Unit_CreatedTime] DATETIME      NULL,
    [Unit_CreatedUser] VARCHAR (50)  NULL,
    [Unit_UpdateTime]  DATETIME      NULL,
    [Unit_UpdateUser]  VARCHAR (50)  NULL,
    [Unit_Sequence]    INT           NULL,
    CONSTRAINT [PK_SMC_Unit] PRIMARY KEY CLUSTERED ([Unit_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级代号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Upper_Unit_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_Demo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用..分隔，如1..2..3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_Path';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_CreatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_CreatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_UpdateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_UpdateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Unit', @level2type = N'COLUMN', @level2name = N'Unit_Sequence';

