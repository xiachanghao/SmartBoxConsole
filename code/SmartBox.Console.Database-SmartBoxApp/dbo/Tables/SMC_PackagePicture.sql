CREATE TABLE [dbo].[SMC_PackagePicture] (
    [pp_id]          INT           NOT NULL,
    [pe_id]          INT           NULL,
    [pp_path]        VARCHAR (400) NULL,
    [pp_CreatedDate] DATETIME      NULL,
    [pp_desc]        VARCHAR (MAX) NULL,
    [pp_title]       VARCHAR (100) NULL,
    CONSTRAINT [PK_SMC_PACKAGEPICTURE] PRIMARY KEY CLUSTERED ([pp_id] ASC),
    CONSTRAINT [FK_SMCPackPicture_R_SMCPackExt] FOREIGN KEY ([pe_id]) REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包截图', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackagePicture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，自动编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackagePicture', @level2type = N'COLUMN', @level2name = N'pp_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包扩展编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackagePicture', @level2type = N'COLUMN', @level2name = N'pe_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackagePicture', @level2type = N'COLUMN', @level2name = N'pp_path';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackagePicture', @level2type = N'COLUMN', @level2name = N'pp_CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片说明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackagePicture', @level2type = N'COLUMN', @level2name = N'pp_desc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'截图标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackagePicture', @level2type = N'COLUMN', @level2name = N'pp_title';

