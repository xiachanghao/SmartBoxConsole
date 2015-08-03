CREATE TABLE [dbo].[monitor_cmd] (
    [cmd_id]           INT            IDENTITY (1, 1) NOT NULL,
    [cmd_title]        VARCHAR (128)  NULL,
    [cmd_code]         VARCHAR (1024) NULL,
    [cmd_senddate]     DATETIME       NULL,
    [cmd_excudedate]   DATETIME       NULL,
    [cmd_excuderesult] VARCHAR (1024) NULL,
    [cmd_hostname]     VARCHAR (128)  NULL,
    [cmd_hostip]       VARCHAR (128)  NULL,
    [cmd_discription]  VARCHAR (1024) NULL,
    CONSTRAINT [PK_MONITOR_CMD] PRIMARY KEY CLUSTERED ([cmd_id] ASC)
);

