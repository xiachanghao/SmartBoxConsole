CREATE TABLE [dbo].[SMC_BUAUserSyncToOutside] (
    [buso_id]        INT           NOT NULL,
    [user_uid]       VARCHAR (50)  NULL,
    [sync_bat_no]    INT           NULL,
    [sync_time]      DATETIME      NULL,
    [sync_status]    BIT           NULL,
    [sync_user_uid]  VARCHAR (50)  NULL,
    [sync_user_name] VARCHAR (50)  NULL,
    [description]    VARCHAR (MAX) NULL,
    [user_name]      VARCHAR (50)  NULL,
    CONSTRAINT [PK_SMC_BUAUSERSYNCTOOUTSIDE] PRIMARY KEY CLUSTERED ([buso_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'bua用户同步到外网记录', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_BUAUserSyncToOutside';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1成功0失败', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SMC_BUAUserSyncToOutside', @level2type = N'COLUMN', @level2name = N'sync_status';

