CREATE TABLE [dbo].[SMC_PackageExt] (
    [pe_id]                 INT            NOT NULL,
    [pe_UpdateTime]         DATETIME       NULL,
    [pe_UpdateUid]          VARCHAR (50)   NULL,
    [pe_CreateUid]          VARCHAR (50)   NULL,
    [pe_DownloadUri]        VARCHAR (512)  NULL,
    [pe_BuildVer]           NVARCHAR (50)  NULL,
    [pe_Version]            NVARCHAR (64)  NULL,
    [pe_Description]        NVARCHAR (MAX) NULL,
    [pe_DisplayName]        NVARCHAR (256) NULL,
    [pe_ClientType]         VARCHAR (50)   NULL,
    [pe_IsTJ]               BIT            NULL,
    [pe_IsBB]               BIT            NULL,
    [pe_PictureUrl]         VARCHAR (300)  NULL,
    [pe_2dPictureUrl]       VARCHAR (300)  NULL,
    [pe_DownCount]          INT            NULL,
    [pe_Firmware]           VARCHAR (50)   NULL,
    [pe_Size]               FLOAT (53)     NULL,
    [TableName]             VARCHAR (50)   NULL,
    [TableID]               INT            NULL,
    [pe_UnitCode]           VARCHAR (300)  NULL,
    [pe_UnitName]           VARCHAR (100)  NULL,
    [pe_CreatedTime]        DATETIME       NULL,
    [pe_Category]           VARCHAR (100)  NULL,
    [pe_CategoryID]         VARCHAR (50)   NULL,
    [pe_Name]               VARCHAR (100)  NULL,
    [pe_AuthSubmitTime]     DATETIME       NULL,
    [pe_UsefulStstus]       BIT            NULL,
    [pe_AuthStatus]         INT            CONSTRAINT [DF_SMC_PackageExt_pe_AuthStatus] DEFAULT ((0)) NULL,
    [pe_ApplicationCode]    VARCHAR (100)  NULL,
    [pe_ApplicationName]    VARCHAR (300)  NULL,
    [pe_AuthMan]            VARCHAR (50)   NULL,
    [pe_AuthManUID]         VARCHAR (50)   NULL,
    [pe_AuthSubmitName]     NVARCHAR (50)  NULL,
    [pe_AuthSubmitUID]      NVARCHAR (50)  NULL,
    [pe_AuthTime]           DATETIME       NULL,
    [pe_Direction]          VARCHAR (50)   NULL,
    [pe_LastVersion]        NVARCHAR (64)  NULL,
    [pe_SyncStatus]         INT            NULL,
    [pe_Type]               VARCHAR (50)   NULL,
    [pe_UsefulOperatorUID]  NVARCHAR (50)  NULL,
    [pe_UsefulOperatorName] NVARCHAR (50)  NULL,
    [pe_UsefulTime]         DATETIME       NULL,
    [pe_FileUrl]            NVARCHAR (200) NULL,
    [pe_ExtentInfo]         VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_SMC_PACKAGEEXT] PRIMARY KEY CLUSTERED ([pe_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包扩展', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包扩展编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Phone/Android、Pad/Android、Phone/iOS、Pad/iOS、Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_ClientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否为推荐应用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_IsTJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否为必备应用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_IsBB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'icon图片url', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_PictureUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'二维码图片地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_2dPictureUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'下载次数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_DownCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'支持固件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_Firmware';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包的大小', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包所在的表名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'TableName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包所在的表的id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'TableID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0待审核 1审核通过 2审核不通过', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_AuthStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'审核时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_AuthTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作方向，比如上架、下架', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_Direction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上一版本号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_LastVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0待同步 1同步成功 2同步失败', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_SyncStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上下架操作人UID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_UsefulOperatorUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上下架操作人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_UsefulOperatorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上下架操作时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_UsefulTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件地址：如~/PackageExt/32/ia.ipa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageExt', @level2type = N'COLUMN', @level2name = N'pe_FileUrl';

