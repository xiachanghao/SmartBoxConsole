CREATE TABLE [dbo].[SMC_PackageExtHistory] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [pe_id]         INT            NULL,
    [pe_Version]    NVARCHAR (50)  NULL,
    [pe_PackageUrl] NVARCHAR (500) NULL,
    [pe_CreateTime] DATETIME       NULL,
    CONSTRAINT [PK_SMC_PackageExtHistory] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'版本号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExtHistory', @level2type = N'COLUMN', @level2name = N'pe_Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包存放地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExtHistory', @level2type = N'COLUMN', @level2name = N'pe_PackageUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExtHistory', @level2type = N'COLUMN', @level2name = N'pe_CreateTime';

