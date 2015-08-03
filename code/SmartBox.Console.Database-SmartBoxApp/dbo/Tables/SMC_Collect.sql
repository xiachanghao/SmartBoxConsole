CREATE TABLE [dbo].[SMC_Collect] (
    [clt_id]          INT           NOT NULL,
    [pe_id]           INT           NULL,
    [ClientType]      VARCHAR (50)  NULL,
    [uid]             VARCHAR (50)  NULL,
    [uname]           VARCHAR (100) NULL,
    [clt_CollectDate] DATETIME      NULL,
    CONSTRAINT [PK_SMC_COLLECT] PRIMARY KEY CLUSTERED ([clt_id] ASC),
    CONSTRAINT [FK_SMC_COLL_REFERENCE_SMC_PACK] FOREIGN KEY ([pe_id]) REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收藏', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Collect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，自动编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Collect', @level2type = N'COLUMN', @level2name = N'clt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安装包扩展编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Collect', @level2type = N'COLUMN', @level2name = N'pe_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Phone/Android、Pad/Android、Phone/iOS、Pad/iOS、Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Collect', @level2type = N'COLUMN', @level2name = N'ClientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收藏用户帐号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Collect', @level2type = N'COLUMN', @level2name = N'uid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收藏用户姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Collect', @level2type = N'COLUMN', @level2name = N'uname';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收藏日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_Collect', @level2type = N'COLUMN', @level2name = N'clt_CollectDate';

