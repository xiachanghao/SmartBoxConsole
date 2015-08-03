CREATE TABLE [dbo].[Monitor_Config] (
    [cfg_id]           INT           IDENTITY (1, 1) NOT NULL,
    [cfg_hostname]     VARCHAR (128) NULL,
    [cfg_hostip]       VARCHAR (128) NULL,
    [cfg_file]         TEXT          NULL,
    [cfg_createdate]   DATETIME      NULL,
    [cfg_createman]    VARCHAR (128) NULL,
    [cfg_updatedate]   DATETIME      NULL,
    [cfg_updateman]    VARCHAR (128) NULL,
    [cfg_updatestatus] VARCHAR (128) NULL,
    [cfg_isuse]        VARCHAR (128) NULL,
    [cfg_usedate]      DATETIME      NULL,
    CONSTRAINT [PK_MONITOR_CONFIG] PRIMARY KEY CLUSTERED ([cfg_id] ASC)
);

