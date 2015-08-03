CREATE TABLE [dbo].[SMC_Functions] (
    [FN_ID]          INT           NOT NULL,
    [FN_Name]        VARCHAR (200) NOT NULL,
    [FN_Code]        VARCHAR (50)  NULL,
    [Upper_FN_ID]    INT           NOT NULL,
    [FN_Url]         VARCHAR (MAX) NULL,
    [FN_Type]        VARCHAR (50)  NULL,
    [Unit_ID]        VARCHAR (300) NULL,
    [FN_Img]         VARCHAR (50)  NULL,
    [FN_Path]        VARCHAR (MAX) NULL,
    [FN_Demo]        TEXT          NULL,
    [FN_IsDefault]   BIT           NULL,
    [FN_CreatedTime] DATETIME      NULL,
    [FN_CreatedUser] VARCHAR (50)  NULL,
    [FN_UpdateTime]  DATETIME      NULL,
    [FN_UpdateUser]  VARCHAR (50)  NULL,
    [FN_Sequence]    INT           NULL,
    [FN_Disabled]    BIT           CONSTRAINT [DF_SMC_Functions_FN_Disabled] DEFAULT ((0)) NULL,
    [FN_VisibleType] INT           CONSTRAINT [DF_SMC_Functions_FN_VisibleType] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Access2] PRIMARY KEY CLUSTERED ([FN_ID] ASC) WITH (FILLFACTOR = 90)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FN_ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上级ID号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'Upper_FN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'超链接', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_Url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'menu为菜单，function哦权限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属单位_Unit_Code', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'Unit_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'正常显示的图标', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_Img';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用..分隔，如1..2..3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_Path';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_Demo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否默认功能（点击模块显示的功能菜单） 1--是', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_IsDefault';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_CreatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_CreatedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_UpdateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_UpdateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_Sequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0所有都可见 1只单位管理员可见 2只系统管理员可见', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Functions', @level2type = N'COLUMN', @level2name = N'FN_VisibleType';

