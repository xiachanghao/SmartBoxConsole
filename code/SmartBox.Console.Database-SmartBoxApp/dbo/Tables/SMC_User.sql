CREATE TABLE [dbo].[SMC_User] (
    [u_id]                     INT           NOT NULL,
    [u_uid]                    VARCHAR (50)  NULL,
    [u_name]                   VARCHAR (100) NULL,
    [u_unitcode]               VARCHAR (300) NULL,
    [u_unitname]               VARCHAR (300) NULL,
    [u_password]               VARCHAR (60)  NULL,
    [u_createddate]            DATETIME      NULL,
    [u_auth_submit_time]       DATETIME      NULL,
    [u_auth_time]              DATETIME      NULL,
    [u_disable_time]           DATETIME      NULL,
    [u_enable_status]          INT           NULL,
    [u_enable_time]            DATETIME      NULL,
    [u_lock_expire_time]       DATETIME      NULL,
    [u_lock_status]            INT           NULL,
    [u_lock_time]              DATETIME      NULL,
    [u_unlock_time]            DATETIME      NULL,
    [u_need_sync]              BIT           NULL,
    [u_need_sync_compare_time] DATETIME      NULL,
    [u_update_time]            DATETIME      NULL,
    CONSTRAINT [PK_SMC_USER] PRIMARY KEY CLUSTERED ([u_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，自动编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户帐号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_uid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户单位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_unitcode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_createddate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否需要从统一授权同步', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_need_sync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户比对的时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_need_sync_compare_time';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_User', @level2type = N'COLUMN', @level2name = N'u_update_time';

