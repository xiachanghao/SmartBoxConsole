CREATE TABLE [dbo].[SMC_PackageFAQ] (
    [pf_id]                  INT           NOT NULL,
    [pf_uid]                 VARCHAR (50)  NULL,
    [pf_uname]               VARCHAR (50)  NULL,
    [pf_question]            VARCHAR (500) NULL,
    [pf_answer]              VARCHAR (MAX) NULL,
    [pf_askdate]             DATETIME      NULL,
    [pe_id]                  INT           NULL,
    [pf_askemail]            VARCHAR (50)  NULL,
    [pf_askmobile]           VARCHAR (50)  NULL,
    [pf_peplyman]            VARCHAR (50)  NULL,
    [pf_need_syncto_inside]  BIT           NULL,
    [pf_need_syncto_outside] BIT           NULL,
    CONSTRAINT [PK_SMC_PACKAGEFAQ] PRIMARY KEY CLUSTERED ([pf_id] ASC),
    CONSTRAINT [FK_SMC_PACK_REFERENCE_SMC_PACK] FOREIGN KEY ([pe_id]) REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'问题返馈', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提问用户帐号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_uid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提问用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_uname';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'问题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_question';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'答案', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_answer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提问时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_askdate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提问人邮件地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_askemail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提问人手机号码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_askmobile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'答复人帐号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_PackageFAQ', @level2type = N'COLUMN', @level2name = N'pf_peplyman';

