CREATE TABLE [dbo].[SMC_Package4Out] (
    [po_ID]       INT            NOT NULL,
    [Name]        NVARCHAR (128) NOT NULL,
    [Type]        NVARCHAR (64)  NOT NULL,
    [ClientType]  NVARCHAR (64)  NOT NULL,
    [DisplayName] NVARCHAR (128) NOT NULL,
    [Description] NVARCHAR (512) NOT NULL,
    [Version]     NVARCHAR (64)  NOT NULL,
    [BuildVer]    INT            NOT NULL,
    [DownloadUri] NVARCHAR (128) NOT NULL,
    [CreateUid]   NVARCHAR (64)  NOT NULL,
    [CreateTime]  DATETIME       NOT NULL,
    [UpdateUid]   NVARCHAR (64)  NOT NULL,
    [UpdateTime]  DATETIME       NOT NULL,
    CONSTRAINT [PK_SMC_PACKAGE4OUT] PRIMARY KEY NONCLUSTERED ([po_ID] ASC),
    CONSTRAINT [CKC_Package4Android_Type] CHECK ([Type]='Plugin' OR [Type]='Main')
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外部应用的发布包信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Package的ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'po_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Package的包名（例如：com.beyondbit.smartbox.phone）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Package的显示名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Package的描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'对外发布的版本号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编译的版本号，解决Bug的快速更新时，发布版本不变，而实际产生变化的情况。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'BuildVer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'下载地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'DownloadUri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Package4Out', @level2type = N'COLUMN', @level2name = N'UpdateTime';

