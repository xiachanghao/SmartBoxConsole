CREATE TABLE [dbo].[Monitor_Defind] (
    [df_id]            INT            IDENTITY (1, 1) NOT NULL,
    [df_kind]          VARCHAR (128)  NULL,
    [df_item]          VARCHAR (128)  NULL,
    [df_maxvalue]      INT            NULL,
    [df_minvalue]      INT            NULL,
    [df_lever]         VARCHAR (128)  NULL,
    [df_code]          VARCHAR (128)  NULL,
    [df_sendway]       VARCHAR (128)  NULL,
    [df_receptman]     VARCHAR (1024) NULL,
    [df_startsenddate] VARCHAR (8)    NULL,
    [df_endsenddate]   VARCHAR (8)    NULL,
    [df_issend]        VARCHAR (128)  NULL,
    CONSTRAINT [PK_MONITOR_DEFIND] PRIMARY KEY CLUSTERED ([df_id] ASC)
);

