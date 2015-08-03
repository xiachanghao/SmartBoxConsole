CREATE TABLE [dbo].[SMC_PackageManual] (
    [pm_id]          INT           NOT NULL,
    [pm_name]        VARCHAR (200) NULL,
    [pm_url]         VARCHAR (500) NULL,
    [pm_createdtime] DATETIME      NULL,
    [pm_updatetime]  DATETIME      NULL,
    [pe_id]          INT           NULL,
    CONSTRAINT [PK_SMC_PACKAGEMANUAL] PRIMARY KEY CLUSTERED ([pm_id] ASC),
    CONSTRAINT [FK_PackageExt_R_PACKManual] FOREIGN KEY ([pe_id]) REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包手册', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'帮助手册名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageManual', @level2type = N'COLUMN', @level2name = N'pm_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'帮助手册下载地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageManual', @level2type = N'COLUMN', @level2name = N'pm_url';

