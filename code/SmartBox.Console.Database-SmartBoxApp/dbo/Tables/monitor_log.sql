CREATE TABLE [dbo].[monitor_log] (
    [log_id]           INT           IDENTITY (1, 1) NOT NULL,
    [log_df_item]      VARCHAR (128) NULL,
    [log_monitorvalue] INT           NULL,
    [log_datetime]     DATETIME      NULL,
    [log_df_kind]      VARCHAR (128) NULL,
    [log_df_code]      VARCHAR (128) NULL,
    [log_df_lever]     VARCHAR (128) NULL,
    [log_status]       VARCHAR (128) NULL,
    [log_hostip]       VARCHAR (128) NULL,
    [log_hostname]     VARCHAR (128) NULL,
    CONSTRAINT [PK_MONITOR_LOG] PRIMARY KEY CLUSTERED ([log_id] ASC)
);

