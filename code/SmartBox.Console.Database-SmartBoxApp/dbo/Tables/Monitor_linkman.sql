CREATE TABLE [dbo].[Monitor_linkman] (
    [lm_id]     INT           IDENTITY (1, 1) NOT NULL,
    [lm_uid]    VARCHAR (128) NULL,
    [lm_uname]  VARCHAR (128) NULL,
    [lm_udept]  VARCHAR (128) NULL,
    [lm_mobile] VARCHAR (128) NULL,
    [lm_email]  VARCHAR (128) NULL,
    CONSTRAINT [PK_MONITOR_LINKMAN] PRIMARY KEY CLUSTERED ([lm_id] ASC)
);

