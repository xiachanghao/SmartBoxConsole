CREATE TABLE [dbo].[SMC_PackageExtSyncToOutside] (
    [peso_id]        INT           NOT NULL,
    [pe_id]          INT           NOT NULL,
    [sync_bat_no]    INT           NULL,
    [sync_time]      DATETIME      NULL,
    [sync_status]    BIT           NULL,
    [sync_user_uid]  VARCHAR (50)  NULL,
    [sync_user_name] VARCHAR (50)  NULL,
    [description]    VARCHAR (MAX) NULL,
    [pe_name]        VARCHAR (200) NULL,
    CONSTRAINT [PK_SMC_PackageExtSyncToOutside] PRIMARY KEY CLUSTERED ([peso_id] ASC),
    CONSTRAINT [FK_SMC_PackageExtSyncToOutside_SMC_PackageExt] FOREIGN KEY ([pe_id]) REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
);

