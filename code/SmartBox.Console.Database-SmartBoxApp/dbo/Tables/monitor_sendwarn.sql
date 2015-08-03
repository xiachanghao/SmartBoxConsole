CREATE TABLE [dbo].[monitor_sendwarn] (
    [sw_id]           INT           IDENTITY (1, 1) NOT NULL,
    [sw_log_id]       INT           NULL,
    [sw_senddate]     DATETIME      NULL,
    [sw_sendresult]   VARCHAR (128) NULL,
    [sw_receptman]    VARCHAR (128) NULL,
    [sw_sendway]      VARCHAR (128) NULL,
    [sw_createdate]   DATETIME      NULL,
    [sw_lastsenddate] DATETIME      NULL,
    [sw_mobile]       VARCHAR (128) NULL,
    [sw_email]        VARCHAR (128) NULL,
    CONSTRAINT [PK_MONITOR_SENDWARN] PRIMARY KEY CLUSTERED ([sw_id] ASC),
    CONSTRAINT [FK_MONITOR__REFERENCE_MONITOR_] FOREIGN KEY ([sw_log_id]) REFERENCES [dbo].[monitor_log] ([log_id])
);

